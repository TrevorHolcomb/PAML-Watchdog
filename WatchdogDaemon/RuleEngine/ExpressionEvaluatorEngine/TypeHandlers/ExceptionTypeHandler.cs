using System;
using ExpressionEvaluator;


//TODO: Implement exception type handler
namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers
{
    internal class ExceptionTypeHandler : ITypeHandler
    {
        public string GetTypeName()
        {
            return "Exception";
        }

        public bool IsValid(string value)
        {
            try
            {
                //Exception doubleTest = System.Exception.Parse(value);
                //return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public void RegisterValue(string name, string value, TypeRegistry registry)
        {
            throw new NotImplementedException();
        }
    }
}