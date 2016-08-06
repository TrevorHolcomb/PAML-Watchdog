using System;
using WatchdogDaemon.Exceptions;

namespace WatchdogDaemon.RuleEngine.ExpressionCompiler.LeafTypeHelpers
{
    internal class StringTypeHandler : ITypeHandler
    {
        private static string Equals(string name, string value)
        {
            return $"({name}.Equals(\"{value}\"))";
        }
        private static string NotEquals(string name, string value)
        {
            return $"(!{name}.Equals(\"{value}\"))";
        }
        private static string Contains(string name, string value)
        {
            return $"({name}.Contains(\"{value}\"))";
        }
        private static string NotContains(string name, string value)
        {
            return $"(!{name}.Contains(\"{value}\"))";
        }
        private static string BeginsWith(string name, string value)
        {
            return $"({name}.StartsWith(\"{value}\"))";
        }
        private static string NotBeginsWith(string name, string value)
        {
            return $"(!{name}.StartsWith(\"{value}\"))";

        }
        private static string In(string name, string value)
        {
            return $"(\"{value}\".Contains({name}))";
        }
        private static string NotIn(string name, string value)
        {
            return $"(!\"{value}\".Contains({name}))";
        }
        private static string EndWith(string name, string value)
        {
            return $"({name}.EndsWith(\"{value}\"))";
        }
        private static string NotEndWith(string name, string value)
        {
            return $"(!{name}.EndsWith(\"{value}\"))";
        }
        private static string IsEmpty(string name)
        {
            return $"({name}.Count() == 0)";
        }
        private static string IsNotEmpty(string name)
        {
            return $"(!{name}.Count() == 0)";
        }
        private static string IsNull(string name)
        {
            return $"({name} == null)";
        }
        private static string IsNotNull(string name)
        {
            return $"({name} != null)";
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
                default:
                    throw new InvalidParameterException("Invalid Operator: " + operatorString);
            }
        }

        public string BuildExpression(string id, string operatorString, string[] values)
        {
            throw new NotImplementedException();
        }

        public string BuildExpression(string id, string operatorString)
        {
            switch (operatorString)
            {
                case "is_empty":
                    return IsEmpty(id);
                case "is_not_empty":
                    return IsNotEmpty(id);
                case "is_null":
                    return IsNull(id);
                case "is_not_null":
                    return IsNotNull(id);
                default:
                    throw new InvalidParameterException("Invalid Operator: " + operatorString);
            }
        }
    }
}
