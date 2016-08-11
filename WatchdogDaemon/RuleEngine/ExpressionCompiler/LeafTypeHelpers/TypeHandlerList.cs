using System.Collections.Generic;
using System.Linq;

namespace WatchdogDaemon.RuleEngine.ExpressionCompiler.LeafTypeHelpers
{
    /// <summary>
    /// The TypeHandlerList acts as a centralized list of all supported TypeHandlers for expression tree conversion, and allows for the building of string expressions.
    /// </summary>
    public class TypeHandlerList
    {
        public static ICollection<AbstractTypeHandler> Types = new List<AbstractTypeHandler>
        {
            new StringTypeHandler(),
            new IntegerTypeHandler()
        };

        /// <summary>
        /// BuildExpression delegates the act of building the expression to a TypeHandler in its registry. It delegates to the TypeHandler whose Name is the same as the parameter type.
        /// </summary>
        /// <param name="type">The name of the TypeHandler to be called.</param>
        /// <param name="name">The name of the parameter</param>
        /// <param name="operatorString">The name of the operator being used.</param>
        /// <param name="value">The constant value being compared.</param>
        /// <returns>Returns the Built expression</returns>
        public static bool BuildExpression(string type, string name, string operatorString, string value)
        {
            return Types.Single(e => e.GetName().Equals(type)).BuildUnaryExpression(name, operatorString, value);
        }


        /// <summary>
        /// BuildExpression delegates the act of building the expression to a TypeHandler in its registry. It delegates to the TypeHandler whose Name is the same as the parameter type.
        /// </summary>
        /// <param name="type">The name of the TypeHandler to be called.</param>
        /// <param name="name">The name of the parameter</param>
        /// <param name="operatorString">The name of the operator being used.</param>
        /// <param name="values">The list of parameters for the operator to use.</param>
        /// <returns>Returns the Built expression</returns>
        public static bool BuildExpression(string type, string name, string operatorString, string[] values)
        {
            return Types.Single(e => e.GetName().Equals(type)).BuildPolyadicExpression(name, operatorString, values);
        }

        /// <summary>
        /// BuildExpression delegates the act of building the expression to a TypeHandler in its registry. It delegates to the TypeHandler whose Name is the same as the parameter type.
        /// </summary>
        /// <param name="type">The name of the TypeHandler to be called.</param>
        /// <param name="name">The name of the parameter</param>
        /// <param name="operatorString">The name of the operator being used.</param>
        /// <returns>Returns the Built expression</returns>
        public static bool BuildExpression(string type, string name, string operatorString)
        {
            return Types.Single(e => e.GetName().Equals(type)).BuildNullaryExpression(name, operatorString);
        }
    }
}
