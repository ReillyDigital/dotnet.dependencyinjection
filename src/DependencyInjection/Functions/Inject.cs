namespace ReillyDigital.DependencyInjection;

using System.Reflection;

public static partial class Functions
{
	public static void Inject(object instance, IServiceProvider iServiceProvider)
	{
		var instanceType = instance.GetType();
		foreach (
			var propertyInfo in
			instanceType
				.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
				.Where((propertyInfo) => Attribute.IsDefined(propertyInfo, typeof(InjectAttribute), true))
		)
		{
			var injectType = propertyInfo.PropertyType;
			var injectValue =
				iServiceProvider.GetService(injectType)
				?? throw new TypeInitializationException(
					instanceType.FullName,
					new Exception($"Type [{injectType}] is not registered with IServiceProvider.")
				);
			propertyInfo.SetValue(instance, injectValue);
		}
	}
}
