namespace ReillyDigital.DependencyInjection;

public static partial class IServiceProviderExtensions
{
	public static T? Transient<T>(this IServiceProvider self) where T : class
	{
		var type = typeof(T);
		if (
			Activator.CreateInstance(
				type,
				type
					.GetConstructors()
					.First()
					.GetParameters()
					.Select((parameterInfo) => self.GetService(parameterInfo.ParameterType))
					.ToArray()
			)
			is not T returnValue
		)
		{
			return null;
		}
		self.Inject(returnValue);
		return returnValue;
	}
}
