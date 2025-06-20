namespace CommonDomain.Core.Generators.Attributes;

[Generator]
public class AttributeGenerator  : IIncrementalGenerator
{
    private const string Namespace = $"{Constance.NamespaceBase}.Attributes";
    
    
    private const string AggregateRootAttributeSourceCode = $@"// <auto-generated/>

namespace {Namespace};

[System.AttributeUsage(System.AttributeTargets.Class)]
public sealed partial class AggregateRootAttribute : System.Attribute;";
    
    
    private const string EntityAttributeSourceCode = $@"// <auto-generated/>
namespace {Namespace};

[System.AttributeUsage(System.AttributeTargets.Class)]
public sealed partial class EntityAttribute : System.Attribute;";
    
    
    private const string ValueObjectAttributeSourceCode = $@"// <auto-generated/>
namespace {Namespace};

[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
public sealed partial class ValueObjectAttribute : System.Attribute;";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Add the marker attribute to the compilation.
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            $"AggregateRootAttribute.g.cs",
            SourceText.From(AggregateRootAttributeSourceCode, Encoding.UTF8)));
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            $"EntityAttribute.g.cs",
            SourceText.From(EntityAttributeSourceCode, Encoding.UTF8)));
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            $"ValueObjectAttribute.g.cs",
            SourceText.From(ValueObjectAttributeSourceCode, Encoding.UTF8)));
    }
}