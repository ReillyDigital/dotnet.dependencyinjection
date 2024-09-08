namespace ReillyDigital.DependencyInjection;

public static partial class IServiceProviderExtensions
{
	public static void Inject(this IServiceProvider self, object instance) => Functions.Inject(instance, self);
}
