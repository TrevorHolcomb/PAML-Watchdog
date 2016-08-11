using System;
using System.Collections.Generic;

namespace WatchdogDaemon.RuleEngine.ExpressionCompiler.LeafTypeHelpers
{
    public abstract class AbstractTypeHandler
    {

        public AbstractTypeHandler()
        {
            unaryFunctions = BuildUnaryHash();
            nullaryFunctions = BuildNullaryHash();
            polyadicFunctions = BuildPolyadicHash();
        }

        public abstract Dictionary<string, Func<string, string, bool>> BuildUnaryHash();
        public abstract Dictionary<string, Func<string, bool>> BuildNullaryHash();
        public abstract Dictionary<string, Func<string, string[], bool>> BuildPolyadicHash();

        Dictionary<string, Func<string, string, bool>> unaryFunctions;
        Dictionary<string, Func<string, bool>> nullaryFunctions;
        Dictionary<string, Func<string, string[], bool>> polyadicFunctions;

        /// <summary>
        /// Returns the name of the type handler. ( Used to match a type in the expression tree to the related typehandler. )
        /// </summary>
        /// <returns>The name type handler's name</returns>
        public abstract string GetName();

        public bool BuildNullaryExpression(string id, string operatorString)
        {
            return nullaryFunctions[operatorString](id);
        }

        public bool BuildUnaryExpression(string id, string operatorString, string value)
        {
            return unaryFunctions[operatorString](id, value);
        }

        public bool BuildPolyadicExpression(string id, string operatorString, string[] values)
        {
            return polyadicFunctions[operatorString](id, values);
        }
    }
}