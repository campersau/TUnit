﻿using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using TUnit.Analyzers.Helpers;

namespace TUnit.Analyzers.Extensions;

public static class AttributeExtensions
{
    public static AttributeData? Get(this ImmutableArray<AttributeData> attributeDatas, string fullyQualifiedName)
    {
        if (!fullyQualifiedName.StartsWith("global::"))
        {
            fullyQualifiedName = $"global::{fullyQualifiedName}";
        }
        
        return attributeDatas.FirstOrDefault(x =>
            x.AttributeClass?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
            == fullyQualifiedName);
    }
    
    public static Location? GetLocation(this AttributeData attributeData)
    {
        return attributeData.ApplicationSyntaxReference?.GetSyntax().GetLocation();
    }
    
    public static bool IsNonGlobalHook(this AttributeData attributeData, Compilation compilation)
    {
        return SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass,
                   compilation.GetTypeByMetadataName(WellKnown.AttributeFullyQualifiedClasses.BeforeAttribute
                       .WithoutGlobalPrefix)) ||
               SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass,
                   compilation.GetTypeByMetadataName(WellKnown.AttributeFullyQualifiedClasses.AfterAttribute
                       .WithoutGlobalPrefix));
    }
    
    public static bool IsGlobalHook(this AttributeData attributeData, Compilation compilation)
    {
        return SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass,
                   compilation.GetTypeByMetadataName(WellKnown.AttributeFullyQualifiedClasses.BeforeEveryAttribute
                       .WithoutGlobalPrefix)) ||
               SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass,
                   compilation.GetTypeByMetadataName(WellKnown.AttributeFullyQualifiedClasses.AfterEveryAttribute
                       .WithoutGlobalPrefix));
    }
    
    public static bool IsMatrixAttribute(this AttributeData attributeData, Compilation compilation)
    {
        return SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass,
                   compilation.GetTypeByMetadataName(WellKnown.AttributeFullyQualifiedClasses.Matrix
                       .WithoutGlobalPrefix));
    }
    
    public static bool IsDataSourceAttribute(this AttributeData? attributeData, Compilation compilation)
    {
        if (attributeData?.AttributeClass is null)
        {
            return false;
        }
        
        var dataAttributeInterface = compilation
            .GetTypeByMetadataName(WellKnown.AttributeFullyQualifiedClasses.IDataAttribute.WithoutGlobalPrefix);

        return attributeData.AttributeClass.AllInterfaces.Contains(dataAttributeInterface, SymbolEqualityComparer.Default);
    }

    public static string GetHookType(this AttributeData attributeData)
    {
        return attributeData.ConstructorArguments[0].ToCSharpString();
    }
}