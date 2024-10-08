﻿#nullable disable

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TUnit.Assertions.AssertConditions;
using TUnit.Assertions.AssertConditions.Interfaces;
using TUnit.Assertions.AssertConditions.String;
using TUnit.Assertions.AssertionBuilders;

namespace TUnit.Assertions.Extensions;

public static partial class DoesExtensions
{
    public static InvokableValueAssertionBuilder<string> Contains(this IValueSource<string> valueSource, string expected, [CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
    {
        return Contains(valueSource, expected, StringComparison.Ordinal, doNotPopulateThisValue);
    }
    
    public static InvokableValueAssertionBuilder<string> Contains(this IValueSource<string> valueSource, string expected, StringComparison stringComparison, [CallerArgumentExpression("expected")] string doNotPopulateThisValue1 = "", [CallerArgumentExpression("stringComparison")] string doNotPopulateThisValue2 = "")
    {
        return valueSource.RegisterAssertion(new StringContainsAssertCondition(expected, stringComparison)
            , [doNotPopulateThisValue1, doNotPopulateThisValue2]);
    }
    
    public static InvokableValueAssertionBuilder<string> StartsWith(this IValueSource<string> valueSource, string expected, [CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
    {
        return StartsWith(valueSource, expected, StringComparison.Ordinal, doNotPopulateThisValue);
    }
    
    public static InvokableValueAssertionBuilder<string> StartsWith(this IValueSource<string> valueSource, string expected, StringComparison stringComparison, [CallerArgumentExpression("expected")] string doNotPopulateThisValue1 = "", [CallerArgumentExpression("stringComparison")] string doNotPopulateThisValue2 = "")
    {
        return valueSource.RegisterAssertion(new DelegateAssertCondition<string, string>(expected,
            (actual, _, _, self) =>
            {
                if (actual is null)
                {
                    self.OverriddenMessage = "Actual string is null";
                    return false;
                }
                
                return actual.StartsWith(expected, stringComparison);
            },
            (actual, _, _) => $"\"{actual}\" does not start with \"{expected}\"")
            , [doNotPopulateThisValue1, doNotPopulateThisValue2]);
    }
    
        
    public static InvokableValueAssertionBuilder<string> EndsWith(this IValueSource<string> valueSource, string expected, [CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
    {
        return EndsWith(valueSource, expected, StringComparison.Ordinal, doNotPopulateThisValue);
    }
    
    public static InvokableValueAssertionBuilder<string> EndsWith(this IValueSource<string> valueSource, string expected, StringComparison stringComparison, [CallerArgumentExpression("expected")] string doNotPopulateThisValue1 = "", [CallerArgumentExpression("stringComparison")] string doNotPopulateThisValue2 = "")
    {
        return valueSource.RegisterAssertion(new DelegateAssertCondition<string, string>(expected,
            (actual, _, _, _) =>
            {
                ArgumentNullException.ThrowIfNull(actual);
                return actual.EndsWith(expected, stringComparison);
            },
            (actual, _, _) => $"\"{actual}\" does not end with \"{expected}\"")
            , [doNotPopulateThisValue1, doNotPopulateThisValue2]);
    }
    
    public static InvokableValueAssertionBuilder<string> Matches(this IValueSource<string> valueSource, string regex, [CallerArgumentExpression("regex")] string expression = "")
    {
        return Matches(valueSource, new Regex(regex), expression);
    }
    
    public static InvokableValueAssertionBuilder<string> Matches(this IValueSource<string> valueSource, Regex regex, [CallerArgumentExpression("regex")] string expression = "")
    {
        return valueSource.RegisterAssertion(new DelegateAssertCondition<string, Regex>(regex,
            (actual, _, _, _) =>
            {
                ArgumentNullException.ThrowIfNull(actual);
                return regex.IsMatch(actual);
            },
            (actual, _, _) => $"The regex \"{regex}\" does not match with \"{actual}\"")
            , [expression]);
    }
}