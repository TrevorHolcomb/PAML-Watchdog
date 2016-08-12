using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDaemon.RuleEngine.TreeEngine.LeafTypeHelpers
{
    /// <summary>
    /// The TypeHandlerList acts as a centralized list of all supported TypeHandlers for expression tree conversion, and allows for the building of string expressions.
    /// </summary>
    public class TypeHandlerList
    {
        public static Dictionary<string, AbstractTypeHandler> TypeHandlers = new Dictionary<string, AbstractTypeHandler>
        {
            {
                "string", new StringTypeHandler()
            },
            {
                "integer", new IntegerTypeHandler()
            }
        };

        /// <summary>
        /// BuildExpression delegates the act of building the expression to a TypeHandler in its registry. It delegates to the TypeHandler whose Name is the same as the parameter type.
        /// </summary>
        /// <param name="type">The name of the TypeHandler to be called.</param>
        /// <param name="name">The name of the parameter</param>
        /// <param name="operatorString">The name of the operator being used.</param>
        /// <param name="value">The constant value being compared.</param>
        /// <returns>Returns the Built expression</returns>
        public static bool BuildExpression(string type, string name, string operatorString, string value, Dictionary<string, MessageParameter> parameters)
        {
            return TypeHandlers[type].BuildUnaryExpression(name, operatorString, value, parameters);
        }


        /// <summary>
        /// BuildExpression delegates the act of building the expression to a TypeHandler in its registry. It delegates to the TypeHandler whose Name is the same as the parameter type.
        /// </summary>
        /// <param name="type">The name of the TypeHandler to be called.</param>
        /// <param name="name">The name of the parameter</param>
        /// <param name="operatorString">The name of the operator being used.</param>
        /// <param name="values">The list of parameters for the operator to use.</param>
        /// <returns>Returns the Built expression</returns>
        public static bool BuildExpression(string type, string name, string operatorString, string[] values, Dictionary<string, MessageParameter>  parameters)
        {
            return TypeHandlers[type].BuildPolyadicExpression(name, operatorString, values, parameters);
        }

        /// <summary>
        /// BuildExpression delegates the act of building the expression to a TypeHandler in its registry. It delegates to the TypeHandler whose Name is the same as the parameter type.
        /// </summary>
        /// <param name="type">The name of the TypeHandler to be called.</param>
        /// <param name="name">The name of the parameter</param>
        /// <param name="operatorString">The name of the operator being used.</param>
        /// <returns>Returns the Built expression</returns>
        public static bool BuildExpression(string type, string name, string operatorString, Dictionary<string, MessageParameter> parameters)
        {
            return TypeHandlers[type].BuildNullaryExpression(name, operatorString, parameters);
        }
    }
}
