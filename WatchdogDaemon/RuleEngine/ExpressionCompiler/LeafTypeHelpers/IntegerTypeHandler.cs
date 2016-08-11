using WatchdogDaemon.Exceptions;

namespace WatchdogDaemon.RuleEngine.ExpressionCompiler.LeafTypeHelpers
{
    internal class IntegerTypeHandler : AbstractTypeHandler
    {
        private static string Equals(string name, string value)
        {
            return $"({name}.Equals(\"{value}\"))";
        }
        private static string NotEquals(string name, string value)
        {
            return $"(!{name}.Equals(\"{value}\"))";
        }
        private static string In(string name, string value)
        {
            return $"(\"{value}\".Contains({name}))";
        }
        private static string NotIn(string name, string value)
        {
            return $"(!\"{value}\".Contains({name}))";
        }
        private static string IsNull(string name)
        {
            return $"({name} == null)";
        }
        private static string IsNotNull(string name)
        {
            return $"({name} != null)";
        }

        private static string Less(string name, string value)
        {
            return $"({name} < {value})";
        }

        private static string LessOrEqual(string name, string value)
        {
            return $"({name} <= {value})";
        }
        private static string Greater(string name, string value)
        {
            return $"({name} > {value})";
        }

        public string GreaterOrEqual(string name, string value)
        {
            return $"({name} >= {value})";
        }
        public string GetName()
        {
            return "integer";
        }

        private static string Between(string name, string valueOne, string valueTwo)
        {
            return $"({valueOne} <= {name} && {name} <= {valueTwo})";
        }

        private static string NotBetween(string name, string valueOne, string valueTwo)
        {
            return $"(!{Between(name,valueOne,valueTwo)})";
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
                case "less":
                    return Less(name, value);
                case "less_or_equal":
                    return LessOrEqual(name, value);
                case "greater":
                    return Greater(name, value);
                case "greater_or_equal":
                    return GreaterOrEqual(name, value);
                default:
                    throw new InvalidParameterException("Invalid Operator: " + operatorString);
            }
        }

        public string BuildExpression(string id, string operatorString, string[] values)
        {
            switch (operatorString)
            {
                case "between":
                    return Between(id, values[0], values[1]);
                case "not_between":
                    return NotBetween(id, values[0], values[1]);
                default:
                    throw new InvalidParameterException("Invalid Operator: " + operatorString);
            }
        }

        public string BuildExpression(string id, string operatorString)
        {
            switch (operatorString)
            {
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
