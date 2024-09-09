namespace ReillyDigital.DependencyInjection.Sample;

using Microsoft.Extensions.DependencyInjection;

public static class SelfInjectScenario
{
	public static void Run()
	{
		// Given a service provider constructed earlier in the application lifecycle:
		var serviceCollection = new ServiceCollection();
		serviceCollection.AddScoped<MyService>();
		var serviceProvider = serviceCollection.BuildServiceProvider();

		// Store the service provider in a place where it can be referenced later:
		AppContext.SetData("ServiceProvider", serviceProvider);

		// Get a new instance of the class that has a parameterless constructor and self-injected dependencies:
		var myClass = new MyClass();

		// It will function properly when calling methods that reference those dependencies:
		myClass.DoStuff();
	}

	// Given a class that will utilize earlier dependency injection:
	public class MyClass
	{
		// Define a property within the class that will be injected with the value of a dependency, indicated by the
		// InjectAttribute:
		[Inject]
		private MyService MyService { get; set; } = default!;

		// Inside the constructor, call the Inject extension method to perform injection on itself:
		public MyClass() =>
			(AppContext.GetData("ServiceProvider") as IServiceProvider)!.Inject(this);

		// Define a method within the class that makes use of the dependency that was injected:
		public void DoStuff() => Console.WriteLine(MyService.Greeting);
	}

	// Define the dependency:
	public class MyService
	{
		public string Greeting { get; } = "Hello, world.";
	}
}
