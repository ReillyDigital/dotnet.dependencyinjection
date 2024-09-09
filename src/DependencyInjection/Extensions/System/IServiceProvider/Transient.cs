namespace ReillyDigital.DependencyInjection;

public static partial class IServiceProviderExtensions
{
	/// <summary>
	/// Extension method to return a new instance of the provided generic type where that new instance will be
	/// constructed using dependency injection from this <see cref="IServiceProvider" />.
	/// </summary>
	/// <typeparam name="T">The type to return a new instance of.</typeparam>
	/// <returns>A new instance of the provided generic type.</returns>
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
