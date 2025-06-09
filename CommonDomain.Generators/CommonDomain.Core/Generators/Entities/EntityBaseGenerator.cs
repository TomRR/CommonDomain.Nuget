namespace CommonDomain.Core.Generators.Entities;

/// <summary>
/// A sample source generator that creates a custom report based on class properties. The target class should be annotated with the 'Generators.ReportAttribute' attribute.
/// When using the source code as a baseline, an incremental source generator is preferable because it reduces the performance overhead.
/// </summary>
[Generator]
public class EntityBaseGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Filter classes annotated with the [Report] attribute. Only filtered Syntax Nodes can trigger code generation.
        var provider = context.SyntaxProvider
            .CreateSyntaxProvider(
                (s, _) => s is ClassDeclarationSyntax,
                (ctx, _) => GetClassDeclarationForSourceGen(ctx))
            .Where(t => t.reportAttributeFound)
            .Select((t, _) => t.Item1);

        // Generate the source code.
        context.RegisterSourceOutput(context.CompilationProvider.Combine(provider.Collect()),
            ((ctx, t) => GenerateCode(ctx, t.Left, t.Right)));
    }

    /// <summary>
    /// Checks whether the Node is annotated with the [Report] attribute and maps syntax context to the specific node type (ClassDeclarationSyntax).
    /// </summary>
    /// <param name="context">Syntax context, based on CreateSyntaxProvider predicate</param>
    /// <returns>The specific cast and whether the attribute was found.</returns>
    private static (ClassDeclarationSyntax, bool reportAttributeFound) GetClassDeclarationForSourceGen(
        GeneratorSyntaxContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

        // Go through all attributes of the class.
        foreach (AttributeListSyntax attributeListSyntax in classDeclarationSyntax.AttributeLists)
        foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
        {
            if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
            {
                continue; // if we can't get the symbol, ignore it
            } 

            string attributeName = attributeSymbol.ContainingType.ToDisplayString();

            // filter
            if (attributeName.Contains("Entity"))
            {
                return (classDeclarationSyntax, true);
            }
        }

        return (classDeclarationSyntax, false);
    }

    /// <summary>
    /// Generate code action.
    /// It will be executed on specific nodes (ClassDeclarationSyntax annotated with the [Report] attribute) changed by the user.
    /// </summary>
    /// <param name="context">Source generation context used to add source files.</param>
    /// <param name="compilation">Compilation used to provide access to the Semantic Model.</param>
    /// <param name="classDeclarations">Nodes annotated with the [Report] attribute that trigger the generate action.</param>
    private void GenerateCode(SourceProductionContext context, Compilation compilation,
        ImmutableArray<ClassDeclarationSyntax> classDeclarations)
    {
        // Go through all filtered class declarations.
        foreach (var classDeclarationSyntax in classDeclarations)
        {
            // We need to get semantic model of the class to retrieve metadata.
            var semanticModel = compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);

            // Symbols allow us to get the compile-time information.
            if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol classSymbol)
            {
                continue;
            }

            var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();

            // 'Identifier' means the token of the node. Get class name from the syntax node.
            var className = classDeclarationSyntax.Identifier.Text;

            // Find properties with the [Updater] attribute
            string boddy = "";
            foreach (var member in classSymbol.GetMembers())
            {
                if (member is not IPropertySymbol propertySymbol)
                {
                    continue;
                }

                AttributeData? updaterAttributeData = null;
                foreach (var attr in propertySymbol.GetAttributes())
                {
                    const string UpdaterAttributeFullName =
                        "CommonDomain.Core.UpdatePropertyGenerators.UpdaterAttribute"; // ADJUST THIS NAMESPACE if needed
                    INamedTypeSymbol? updaterAttributeSymbol =
                        compilation.GetTypeByMetadataName(UpdaterAttributeFullName);
                    if (SymbolEqualityComparer.Default.Equals(attr.AttributeClass, updaterAttributeSymbol))
                    {
                        updaterAttributeData = attr;
                        boddy = "YESS"!;
                        break;
                    }
                }
            }



            var methodBody2 = boddy;
            // Go through all class members with a particular type (property) to generate method lines.
            var methodBody = classSymbol.GetMembers()
                .OfType<IPropertySymbol>()
                .Select(p =>
                    $@"        yield return $""{p.Name}:{{this.{p.Name}}}"";"); // e.g. yield return $"Id:{this.Id}";

            // Build up the source code
            var code = GetCode(namespaceName, className, methodBody2);

            // Add the source code to the compilation.
            context.AddSource($"{className}Base.g.cs", SourceText.From(code, Encoding.UTF8));
        }
    }
    private static string GetCode(string namespaceName, string className, string? methodBody = null)
    {
        return string.IsNullOrWhiteSpace(methodBody)
            ? DefaultCode(namespaceName, className)
            : DefaultCodeBase(namespaceName, className, methodBody!);
    }

    private static string DefaultCode(string namespaceName, string className) => $@"// <auto-generated/>
using Generators.Domain.Common;

namespace {namespaceName};

public sealed partial class {className} : Entity<{className}Id>
{{
    private {className}({className}Id id) : base(id) {{ }}
}}";
    
    private static string DefaultCodeBase(string namespaceName, string className, string methodBody) => $@"// <auto-generated/>
using Generators.Domain.Common;

namespace {namespaceName};

public sealed partial class {className} : Entity<{className}Id>
{{
{methodBody}
}}";
}

