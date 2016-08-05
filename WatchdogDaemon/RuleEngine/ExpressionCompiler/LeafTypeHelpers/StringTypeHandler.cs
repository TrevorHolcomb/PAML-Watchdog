using System;
using WatchdogDaemon.Exceptions;

namespace WatchdogDaemon.RuleEngine.ExpressionCompiler.LeafTypeHelpers
{
    internal class StringTypeHandler : ITypeHandler
    {
        private string Equals(string name, string value)
        {
            return @"(" + name + @".Equals(""" + value + @"""))";
        }
        private string NotEquals(string name, string value)
        {
            return @"(!" + name + @".Equals(""" + value + @"""))";
        }
        private string Contains(string name, string value)
        {
            return @"(" + name + @".Contains(""" + value + @"""))";
        }
        private string NotContains(string name, string value)
        {
            return @"(!" + name + @".Contains(""" + value + @"""))";
        }
        private string BeginsWith(string name, string value)
        {
            return @"(" + name + @".StartsWith(""" + value +  @"""))";
        }
        private string NotBeginsWith(string name, string value)
        {
            return @"(!" + name + @".StartsWith(""" + value + @"""))";

        }
        private string In(string name, string value)
        {
            return @"(""" + value + @""".Contains(" + name + @"))";
        }
        private string NotIn(string name, string value)
        {
            return @"(!""" + value + @""".Contains(" + name + @"))";
        }
        private string EndWith(string name, string value)
        {
            return @"(" + name + @".EndsWith(""" + value + @"""))";
        }
        private string NotEndWith(string name, string value)
        {
            return @"(!" + name + @".EndsWith(""" + value + @"""))";
        }
        private string IsEmpty(string name)
        {
            return @"(" + name + @".Count() == 0)";
        }
        private string IsNotEmpty(string name)
        {
            return @"(!" + name + @".Count() == 0)";
        }
        private string IsNull(string name)
        {
            return @"(" + name + " == null)";
        }
        private string IsNotNull(string name)
        {
            return @"(" + name + " != null)";
        }
        public string GetName()
        {
            return "string";
        }

        public string BuildExpression(string name, string operatorString, string value)
        {
            switch (operatorString)
            {
                case "equal":
                    return Equals(name, value);
                case "not_equal":
                    return NotEquals(name, value);
                case "in":
                    return In(name, value);
                case "not_in":
                    return NotIn(name, value);
                case "begins_with":
                    return BeginsWith(name, value);
                case "not_begins_with":
                    return NotBeginsWith(name, value);
                case "contains":
                    return Contains(name, value);
                case "not_contains":
                    return NotContains(name, value);
                case "ends_with":
                    return EndWith(name, value);
                case "not_ends_with":
                    return NotEndWith(name, value);
                case "is_empty":
                    return IsEmpty(name);
                case "is_not_empty":
                    return IsNotEmpty(name);
                case "is_null":
                    return IsNull(name);
                case "is_not_null":
                    return IsNotNull(name);
                default:
                    throw new InvalidParameterException("Invalid Operator: " + operatorString);
            }
        }

        public string BuildExpression(string id, string operatorString, string valueOne, string valueTwo)
        {
            throw new NotImplementedException();
        }
    }
}
