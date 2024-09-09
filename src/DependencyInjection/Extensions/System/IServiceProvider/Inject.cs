namespace ReillyDigital.DependencyInjection;

public static partial class IServiceProviderExtensions
{
	/// <summary>
	/// Extension method to inject a class instance with values pulled from this <see cref="IServiceProvider" /> for any
	/// property having the <see cref="InjectAttribute" />.
	/// </summary>
	/// <inheritdoc cref="Functions.Inject(object, IServiceProvider)" />
	public static void Inject(this IServiceProvider self, object instance) => Functions.Inject(instance, self);
}
