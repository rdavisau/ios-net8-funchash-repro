using OneOf;

namespace FuncHash;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
	public override UIWindow? Window { get; set; }

	public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
	{
		Window = new UIWindow { RootViewController = new UIViewController { View = { BackgroundColor = UIColor.Green } }};
		Window.MakeKeyAndVisible();

		var x = new NSObject[]
		{
			// 1. create using "new" works for int -> string (and various other combinations)
			// new Identifier<int, string>(1, _ => "1"),

			// 2. create using helper method works for int -> string
			// Identifier(2, _ => "2"),

			// 3. create using "new" works for OneOf<string,int>
			// new Identifier<OneOf<string, int>, string>("hello", _ => "3"),

			// 4. create using helper method works for MyDoubleGenericType<string,int>
			// Identifier(new MyDoubleGenericType<string, int>(), _ => "2"),

			// 5. create using helper method does not work for OneOf<string,int>
			Identifier((OneOf<string, int>) 1, _ => "2")
		}
	   .ToArray();

		Console.WriteLine("About to make a set 😎");
		var set = new NSOrderedSet(x);
		Console.WriteLine("Made a set 😎");

		return true;
	}

	public Identifier<T,U> Identifier<T,U>(T item, Func<T, U> getIdentifier) => new(item, getIdentifier);
}

public class Identifier<T,U> : NSObject
{
	private readonly T _item;
	private readonly Func<T, U> _identifierFunc;

	public Identifier(T item, Func<T,U> identifierFunc)
	{
		_item = item;
		_identifierFunc = identifierFunc;
	}

	// invoke the provided func and hash the result
	public override nuint GetNativeHash()
		=> (uint)_identifierFunc(_item).GetHashCode();

	public override bool IsEqual(NSObject anObject)
		=> GetNativeHash() == anObject.GetNativeHash();
}

public class MyDoubleGenericType<T1, T2> { }
