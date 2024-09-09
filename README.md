# ReillyDigital.DependencyInjection

A .NET library for expanding dependency injection capabilities.

## Usage

### Self-Inject

Having a class that self-injects its dependencies upon construction is useful for scenarios where a class requires a parameterless constructor but dependency injection is still desired.

In this scenario, the class will call the Inject extension method inside its own parameterless constructor to inject its dependencies using an already constructed service provider.

Given a dependency service:
```csharp
public class MyService
{
	public string Greeting { get; } = "Hello, world.";
}
```

Given a service provider constructed earlier in the application lifecycle:
```csharp
var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<MyService>();
var serviceProvider = serviceCollection.BuildServiceProvider();
```

Store the service provider in a place where it can be referenced later:
```csharp
AppContext.SetData("ServiceProvider", serviceProvider);
```

Given a class that will utilize earlier dependency injection:
```csharp
public class MyClass
{
	...
}
```

Define a property within the class that will be injected with the value of a dependency, indicated by the InjectAttribute:
```csharp
[Inject]
private MyService MyService { get; set; } = default!;
```

Inside the constructor, call the Inject extension method to perform injection on itself:
```csharp
public MyClass() =>
	(AppContext.GetData("ServiceProvider") as IServiceProvider)!.Inject(this);
```

Define a method within the class that makes use of the dependency that was injected:
```csharp
public void DoStuff() => Console.WriteLine(MyService.Greeting);
```

Get a new instance of the class that has a parameterless constructor and self-injected dependencies:
```csharp
var myClass = new MyClass();
```

It will function properly when calling methods that reference those dependencies:
```csharp
myClass.DoStuff();
```

### Transient Instances

In this scenario, the class will be initialized using the Transient extension method which will handle injecting those dependencies using an already constructed service provider.

The primary difference here compared to the previous example is that the class requiring the dependencies does not inject them within its own constructor but instead by the Transient extension method.

This might be preferred over the previous example in scenarios where the service provider may not be accessible within the constructor of the class requiring the dependency injection, so it can use a service provider accessible instead at the time of initializing the new class instance. Or perhaps if a class needs to be able to be constructed using a multitude of available service providers.

Given a dependency service:
```csharp
public class MyService
{
	public string Greeting { get; } = "Hello, world.";
}
```

Given a service provider constructed earlier in the application lifecycle:
```csharp
var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<MyService>();
var serviceProvider = serviceCollection.BuildServiceProvider();
```

Store the service provider in a place where it can be referenced later:
```csharp
AppContext.SetData("ServiceProvider", serviceProvider);
```

Given a class that will utilize earlier dependency injection:
```csharp
public class MyClass
{
	...
}
```

Define a property within the class that will be injected with the value of a dependency, indicated by the InjectAttribute:
```csharp
[Inject]
private MyService MyService { get; set; } = default!;
```

No additional logic is needed in the class constructor:
```csharp
public MyClass() { }
```

Define a method within the class that makes use of the dependency that was injected:
```csharp
public void DoStuff() => Console.WriteLine(MyService.Greeting);
```

Get a new instance of a class that has a parameterless class constructor and also dependency properties marked with a InjectAttribute:
```csharp
var myClass =
	(AppContext.GetData("ServiceProvider") as IServiceProvider)!
		.Transient<MyClass>()!;
```

It will function properly when calling methods that reference those dependencies:
```csharp
myClass.DoStuff();
```

## Links

Sample Project:
https://gitlab.com/reilly-digital/dotnet/dependencyinjection/-/tree/main/src/DependencyInjection

NuGet:
https://www.nuget.org/packages/ReillyDigital.DependencyInjection
