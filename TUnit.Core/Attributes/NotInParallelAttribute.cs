﻿namespace TUnit.Core;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class NotInParallelAttribute : Attribute
{
    public string[]? ConstraintKeys { get; }

    public NotInParallelAttribute()
    {
    }
    
    public NotInParallelAttribute(string constraintKey) : this([constraintKey])
    {
        ArgumentException.ThrowIfNullOrEmpty(constraintKey);
    }
    
    public NotInParallelAttribute(string[] constraintKeys)
    {
        ConstraintKeys = constraintKeys;
    }
}