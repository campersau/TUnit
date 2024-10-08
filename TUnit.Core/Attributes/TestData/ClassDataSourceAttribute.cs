﻿using System.Diagnostics.CodeAnalysis;

namespace TUnit.Core;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class ClassDataSourceAttribute<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> 
    : TUnitAttribute where T : new()
{
    public ClassDataSourceAttribute()
    {
        if (!typeof(T).GetConstructors().Any(x => x.IsPublic && x.GetParameters().Length == 0))
        {
            throw new ArgumentException($"{typeof(T).FullName} cannot be used within [ClassData] as it does not have a public constructor.");
        }
    }
    
    public SharedType Shared { get; set; } = SharedType.None;
    public string? Key { get; set; }
}

public enum SharedType
{
    None,
    ForClass,
    Globally,
    Keyed,
}