using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDaemon.RuleEngine.TreeEngine.LeafTypeHelpers
{
    internal class IntegerTypeHandler : AbstractTypeHandler
    {
        private static int UnwrapVariable(string name, IDictionary<string, MessageParameter> parameters)
        {
            return int.Parse(parameters[name].Value);
        }

        private static bool Equals(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return UnwrapVariable(name, parameters) == int.Parse(value);
        }
        private static bool NotEquals(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return !Equals(name, value, parameters);
        }
        private static bool Less(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return UnwrapVariable(name, parameters) < int.Parse(value);
        }
        private static bool LessOrEqual(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return UnwrapVariable(name, parameters) <= int.Parse(value);
        }
        private static bool Greater(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return UnwrapVariable(name, parameters) > int.Parse(value);
        }
        public static bool GreaterOrEqual(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return UnwrapVariable(name, parameters) >= int.Parse(value);
        }
        private static bool Between(string name, string[] values, IDictionary<string, MessageParameter> parameters)
        {
            return GreaterOrEqual(name, values[0], parameters) && LessOrEqual(name, values[1], parameters);
        }
        private static bool NotBetween(string name, string[] values,  IDictionary<string, MessageParameter> parameters)
        {
            return !Between(name, values, parameters);
        }

        public override Dictionary<string, Func<string, string, IDictionary<string, MessageParameter>, bool>> BuildUnaryHash()
        {
            return new Dictionary<string, Func<string, string, IDictionary<string, MessageParameter>, bool>>
            {
                {
                    "equal", Equals
                },
                {
                    "not_equal", NotEquals
                },
                {
                    "less", Less
                },
                {
                    "less_or_equal", LessOrEqual
                },
                {
                    "greater", Greater
                },
                {
                    "greater_or_equal", GreaterOrEqual
                }
            };
        }
        public override Dictionary<string, Func<string, IDictionary<string, MessageParameter>, bool>> BuildNullaryHash()
        {
            return new Dictionary<string, Func<string, IDictionary<string, MessageParameter>, bool>>
            {
                
            };
        }
        public override Dictionary<string, Func<string, string[], IDictionary<string, MessageParameter>, bool>> BuildPolyadicHash()
        {
            return new Dictionary<string, Func<string, string[], IDictionary<string, MessageParameter>, bool>>
            {
                {
                    "between", Between
                },
                {
                    "not_between", NotBetween
                }
            };
        }

        public override bool IsValid(string value)
        {
            long _;
            return long.TryParse(value, out _);
        }
    }
}
