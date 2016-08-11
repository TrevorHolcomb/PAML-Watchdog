using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDaemon.RuleEngine.TreeEngine.LeafTypeHelpers
{
    public abstract class AbstractTypeHandler
    {
        protected AbstractTypeHandler()
        {
            _unaryFunctions = BuildUnaryHash();
            _nullaryFunctions = BuildNullaryHash();
            _polyadicFunctions = BuildPolyadicHash();
        }

        public abstract Dictionary<string, Func<string, string, IDictionary<string, MessageParameter>, bool>> BuildUnaryHash();
        public abstract Dictionary<string, Func<string, IDictionary<string, MessageParameter>, bool>> BuildNullaryHash();
        public abstract Dictionary<string, Func<string, string[], IDictionary<string, MessageParameter>, bool>> BuildPolyadicHash();

        private readonly Dictionary<string, Func<string, string, IDictionary<string, MessageParameter>, bool>> _unaryFunctions;
        private readonly Dictionary<string, Func<string, IDictionary<string, MessageParameter>, bool>> _nullaryFunctions;
        private readonly Dictionary<string, Func<string, string[], IDictionary<string, MessageParameter>, bool>> _polyadicFunctions;

        public abstract bool IsValid(string value);


        public bool BuildNullaryExpression(string id, string operatorString, Dictionary<string, MessageParameter> parameters)
        {
            return _nullaryFunctions[operatorString](id, parameters);
        }

        public bool BuildUnaryExpression(string id, string operatorString, string value, Dictionary<string, MessageParameter> parameters)
        {
            return _unaryFunctions[operatorString](id, value, parameters);
        }

        public bool BuildPolyadicExpression(string id, string operatorString, string[] values, Dictionary<string, MessageParameter> parameters)
        {
            return _polyadicFunctions[operatorString](id, values, parameters);
        }
    }
}