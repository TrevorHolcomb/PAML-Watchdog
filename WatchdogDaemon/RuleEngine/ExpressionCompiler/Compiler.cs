using Newtonsoft.Json.Linq;

namespace WatchdogDaemon.RuleEngine.ExpressionCompiler
{
    /// <summary>
    /// The compiler is used to compile a valid c# expression from a expression tree stored in a JSON structure.
    /// FROM THIS
    /// {
    ///     "condition":"AND",
    ///     "rules":[
    ///         {
    ///             "id":"exception",
    ///             "field":"exception",
    ///             "type":"string",
    ///             "input":"text",
    ///             "operator":"contains",
    ///             "value":"foo"
    ///         }
    ///     ]
    /// }
    /// 
    /// TO THIS
    /// ((exception.Contains(""foo"")))
    /// </summary>
    public class Compiler
    {
        /// <summary>
        /// Converts a given JSON expression tree into a C# expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string Convert(string expression)
        {
            var tree = JToken.Parse(expression);
            return new InternalNode(tree).Evaluate();
        }
    }
}
