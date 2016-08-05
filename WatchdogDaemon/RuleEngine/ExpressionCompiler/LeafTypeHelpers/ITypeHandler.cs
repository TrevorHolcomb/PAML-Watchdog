namespace WatchdogDaemon.RuleEngine.ExpressionCompiler.LeafTypeHelpers
{
    public interface ITypeHandler
    {
        /// <summary>
        /// Returns the name of the type handler. ( Used to match a type in the expression tree to the related typehandler. )
        /// </summary>
        /// <returns>The name type handler's name</returns>
        string GetName();
        string BuildExpression(string id, string operatorString, string value);
        string BuildExpression(string id, string operatorString, string valueOne, string valueTwo);
    }
}