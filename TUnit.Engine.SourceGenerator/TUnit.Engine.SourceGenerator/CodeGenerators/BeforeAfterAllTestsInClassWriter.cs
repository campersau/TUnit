﻿using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TUnit.Engine.SourceGenerator.CodeGenerators;


[Generator]
public class GlobalBeforeAfterEachTestWriter : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var testMethods = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
            .Where(static m => m is not null)
            .Collect();

        context.RegisterSourceOutput(testMethods, Execute);
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode node)
    {
        return node is MethodDeclarationSyntax { AttributeLists.Count: > 0 };
    }

    static IMethodSymbol? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        if (context.Node is not MethodDeclarationSyntax)
        {
            return null;
        }

        var symbol = context.SemanticModel.GetDeclaredSymbol(context.Node);

        if (symbol is not IMethodSymbol methodSymbol)
        {
            return null;
        }

        if (!methodSymbol.IsStatic)
        {
            return null;
        }

        var attributes = methodSymbol.GetAttributes();

        if (!attributes.Any(x =>
                x.AttributeClass?.ToDisplayString(
                    DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                is WellKnownFullyQualifiedClassNames.GlobalBeforeEachTestAttribute
                or WellKnownFullyQualifiedClassNames.GlobalAfterEachTestAttribute))
        {
            return null;
        }

        return methodSymbol;
    }

    private void Execute(SourceProductionContext context, ImmutableArray<IMethodSymbol?> methodSymbols)
    {
        foreach (var method in methodSymbols.OfType<IMethodSymbol>())
        {
            var classContainingMethod =
                method.ContainingType.ToDisplayString(DisplayFormats.FullyQualifiedGenericWithGlobalPrefix);
            
            if (method.GetAttributes().Any(x =>
                    x.AttributeClass?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                    == WellKnownFullyQualifiedClassNames.GlobalBeforeEachTestAttribute))
            {
                var className = $"GlobalBeforeEachTest_{method.ContainingType.Name}_{Guid.NewGuid():N}";

                using var sourceBuilder = new SourceCodeWriter();
                
                sourceBuilder.WriteLine("// <auto-generated/>");
                sourceBuilder.WriteLine("using System.Linq;");
                sourceBuilder.WriteLine("using System.Reflection;");
                sourceBuilder.WriteLine("using System.Runtime.CompilerServices;");
                sourceBuilder.WriteLine();
                sourceBuilder.WriteLine("namespace TUnit.Engine;");
                sourceBuilder.WriteLine();
                sourceBuilder.WriteLine($"file class {className}");
                sourceBuilder.WriteLine("{");
                sourceBuilder.WriteLine("[ModuleInitializer]");
                sourceBuilder.WriteLine("public static void Initialise()");
                sourceBuilder.WriteLine("{");
                
                sourceBuilder.WriteLine($"global::TUnit.Engine.GlobalTestHookOrchestrator.RegisterSetUp(() => global::TUnit.Core.Helpers.RunHelpers.RunAsync(() => {method.ContainingType.ToDisplayString(DisplayFormats.FullyQualifiedGenericWithGlobalPrefix)}.{method.Name}{MethodParenthesisGenerator.WriteParenthesis(method)}));");
                
                sourceBuilder.WriteLine("}");
                sourceBuilder.WriteLine("}");
                
                context.AddSource($"{className}.g.cs", sourceBuilder.ToString());
            }
            
            if (method.GetAttributes().Any(x =>
                    x.AttributeClass?.ToDisplayString(DisplayFormats.FullyQualifiedNonGenericWithGlobalPrefix)
                    == WellKnownFullyQualifiedClassNames.GlobalAfterEachTestAttribute))
            {
                var className = $"GlobalAfterEachTest_{method.ContainingType.Name}_{Guid.NewGuid():N}";

                using var sourceBuilder = new SourceCodeWriter();
                
                sourceBuilder.WriteLine("// <auto-generated/>");
                sourceBuilder.WriteLine("using System.Linq;");
                sourceBuilder.WriteLine("using System.Reflection;");
                sourceBuilder.WriteLine("using System.Runtime.CompilerServices;");
                sourceBuilder.WriteLine();
                sourceBuilder.WriteLine("namespace TUnit.Engine;");
                sourceBuilder.WriteLine();
                sourceBuilder.WriteLine($"file class {className}");
                sourceBuilder.WriteLine("{");
                sourceBuilder.WriteLine("[ModuleInitializer]");
                sourceBuilder.WriteLine("public static void Initialise()");
                sourceBuilder.WriteLine("{");
                
                sourceBuilder.WriteLine($"global::TUnit.Engine.GlobalTestHookOrchestrator.RegisterCleanUp(() => global::TUnit.Core.Helpers.RunHelpers.RunAsync(() => {method.ContainingType.ToDisplayString(DisplayFormats.FullyQualifiedGenericWithGlobalPrefix)}.{method.Name}{MethodParenthesisGenerator.WriteParenthesis(method)}));");
                
                sourceBuilder.WriteLine("}");
                sourceBuilder.WriteLine("}");

                context.AddSource($"{className}.g.cs", sourceBuilder.ToString());
            }
        }
    }
}