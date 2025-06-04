namespace CommonDomain.Generators.UpdatePropertyGenerators;

[System.AttributeUsage(System.AttributeTargets.Property)]
public sealed partial class UpdateAttribute : System.Attribute
{
    public UpdaterValidationType Validator { get; set; }
    public string ErrorDescription  { get; set; }
    public string Error  { get; set; }
}