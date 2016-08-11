using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDaemon.RuleEngine.TreeEngine.LeafTypeHelpers
{
    internal class StringTypeHandler : AbstractTypeHandler
    {
        private static string UnwrapVariable(string name, IDictionary<string, MessageParameter> parameters)
        {
            return parameters[name].Value;
        }

        private static bool Equals(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return UnwrapVariable(name, parameters).Equals(value);
        }
        private static bool NotEquals(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return !Equals(name, value);
        }
        private static bool Contains(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return UnwrapVariable(name, parameters).Contains(value);
        }
        private static bool NotContains(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return !Contains(name, value, parameters);
        }
        private static bool BeginsWith(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return UnwrapVariable(name, parameters).StartsWith(value);
        }
        private static bool NotBeginsWith(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return !BeginsWith(name, value, parameters);

        }
        private static bool In(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return UnwrapVariable(name, parameters).Contains(name);
        }
        private static bool NotIn(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return !In(name, value, parameters);
        }
        private static bool EndWith(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return UnwrapVariable(name, parameters).EndsWith(value);
        }
        private static bool NotEndWith(string name, string value, IDictionary<string, MessageParameter> parameters)
        {
            return !EndWith(name, value, parameters);
        }
        private static bool IsEmpty(string name, IDictionary<string, MessageParameter> parameters)
        {
            return UnwrapVariable(name, parameters).Length == 0;
        }
        private static bool IsNotEmpty(string name, IDictionary<string, MessageParameter> parameters)
        {
            return !IsEmpty(name, parameters);
        }

        public override bool IsValid(string value)
        {
            return true;
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
                    "in", In
                },
                {
                    "not_in", NotIn
                },
                {
                    "begins_with", BeginsWith
                },
                {
                    "not_begins_with", NotBeginsWith
                },
                {
                    "contains", Contains
                },
                {
                    "not_contains", NotContains
                },
                {
                    "ends_with", EndWith
                },
                {
                    "not_ends_with", NotEndWith
                }
            };
        }

        public override Dictionary<string, Func<string, IDictionary<string, MessageParameter>, bool>> BuildNullaryHash()
        {
            return new Dictionary<string, Func<string, IDictionary<string, MessageParameter>, bool>>
            {
                {
                    "is_empty", IsEmpty
                },
                {
                    "is_not_empty", IsNotEmpty
                }
            };
        }

        public override Dictionary<string, Func<string, string[], IDictionary<string, MessageParameter>, bool>> BuildPolyadicHash()
        {
            return new Dictionary<string, Func<string, string[], IDictionary<string, MessageParameter>, bool>>
            {

            };
        }
    }
}
