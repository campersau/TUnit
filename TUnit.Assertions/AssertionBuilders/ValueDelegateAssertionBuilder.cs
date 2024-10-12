﻿using TUnit.Assertions.AssertConditions.Interfaces;
using TUnit.Assertions.AssertConditions.Operators;
using TUnit.Assertions.Extensions;

namespace TUnit.Assertions.AssertionBuilders;

public class ValueDelegateAssertionBuilder<TActual> 
    : AssertionBuilder<TActual>, 
        IDelegateSource<TActual>, 
        IValueSource<TActual>
{
    internal ValueDelegateAssertionBuilder(Func<TActual> function, string expressionBuilder) : base(function.AsAssertionData(expressionBuilder), expressionBuilder)
    {
    }
    
    AssertionBuilder<TActual> ISource<TActual>.AssertionBuilder => this;
    
    public InvokableValueAssertionBuilder<TActual> IsTypeOf(Type type)
    {
        return new ValueSource<TActual>(this).IsTypeOf(type);
    }

    public CastableAssertionBuilder<TActual, TExpected> IsTypeOf<TExpected>()
    {
        return new ValueSource<TActual>(this).IsTypeOf<TExpected>();
    }

    public InvokableValueAssertionBuilder<TActual> IsAssignableTo(Type type)
    {
        return new ValueSource<TActual>(this).IsAssignableTo(type);
    }

    public CastableAssertionBuilder<TActual, TExpected> IsAssignableTo<TExpected>()
    {
        return new ValueSource<TActual>(this).IsAssignableTo<TExpected>();
    }

    public InvokableValueAssertionBuilder<TActual> IsAssignableFrom(Type type)
    {
        return new ValueSource<TActual>(this).IsAssignableFrom(type);
    }

    public InvokableValueAssertionBuilder<TActual> IsAssignableFrom<TExpected>()
    {
        return new ValueSource<TActual>(this).IsAssignableFrom<TExpected>();
    }
}