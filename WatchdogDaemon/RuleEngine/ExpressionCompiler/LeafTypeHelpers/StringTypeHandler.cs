using System;
using System.Collections.Generic;
using WatchdogDaemon.Exceptions;

namespace WatchdogDaemon.RuleEngine.ExpressionCompiler.LeafTypeHelpers
{
    internal class StringTypeHandler : AbstractTypeHandler
    {
        private static bool Equals(string name, string value)
        {
            return name.Equals(value);
        }
        private static bool NotEquals(string name, string value)
        {
            return !Equals(name, value);
        }
        private static bool Contains(string name, string value)
        {
            return name.Contains(value);
        }
        private static bool NotContains(string name, string value)
        {
            return !Contains(name, value);
        }
        private static bool BeginsWith(string name, string value)
        {
            return name.StartsWith(value);
        }
        private static bool NotBeginsWith(string name, string value)
        {
            return !BeginsWith(name, value);

        }
        private static bool In(string name, string value)
        {
            return value.Contains(name);
        }
        private static bool NotIn(string name, string value)
        {
            return !In(name, value);
        }
        private static bool EndWith(string name, string value)
        {
            return name.EndsWith(value);
        }
        private static bool NotEndWith(string name, string value)
        {
            return !EndWith(name, value);
        }
        private static bool IsEmpty(string name)
        {
            return name.Length == 0;
        }
        private static bool IsNotEmpty(string name)
        {
            return !IsEmpty(name);
        }
        private static bool IsNull(string name)
        {
            return name == null;
        }
        private static bool IsNotNull(string name)
        {
            return !IsNull(name);
        }
        public override string GetName()
        {
            return "string";
        }


        public override Dictionary<string, Func<string, string, bool>> BuildUnaryHash()
        {
            return new Dictionary<string, Func<string, string, bool>>
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

        public override Dictionary<string, Func<string, bool>> BuildNullaryHash()
        {
            return new Dictionary<string, Func<string, bool>>
            {
                {
                    "is_empty", IsEmpty
                },
                {
                    "is_not_empty", IsNotEmpty
                },
                {
                    "is_null", IsNull
                },
                {
                    "is_not_null", IsNotNull
                }
            };
        }

        public override Dictionary<string, Func<string, string[], bool>> BuildPolyadicHash()
        {
            return new Dictionary<string, Func<string, string[], bool>>
            {

            };
        }
    }
}
