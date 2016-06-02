﻿using ExpressionEvaluator;

namespace WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine.TypeHandlers
{
    internal interface ITypeHandler
    {
        string GetTypeName();
        void RegisterValue(string name, string value, TypeRegistry registry);
        bool IsValid(string value);
    }
}