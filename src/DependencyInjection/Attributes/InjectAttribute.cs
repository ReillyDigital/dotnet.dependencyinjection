namespace ReillyDigital.DependencyInjection;

/// <summary>
/// This attribute should be assigned to any property that will be used with the
/// <see cref="IServiceProvider.Inject" /> extension method.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class InjectAttribute : Attribute { }
