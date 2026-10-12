using System.Text;

namespace CollectionTester;
internal class CollectionTester<T>(string[] input, Func<string, T> stringToT, Func<T, T> comparer)
{
	private readonly List<BaseCollection<T>> _collection = [];

    public void Add(BaseCollection<T> collection)
	{
		_collection.Add(collection);
	}
	public int Count()
	{
		return _collection.Count;
	}

	private long RunTest(BaseCollection<T> collection)
	{
		Console.WriteLine($"Test {collection.GetType()}.");
		Console.WriteLine($"Fill collection in {collection.FillCollection(input, stringToT)} ms");
		Console.WriteLine($"Sort collection in {collection.SortCollection(comparer)} ms.");
		using (var writer = new StreamWriter("dump.txt", false, Encoding.UTF8))
		{
			Console.WriteLine($"Print collection in {collection.PrintCollection(writer)} ms.");
        }
		Console.WriteLine($"Total time {collection.TotalElapsedMilliseconds} ms.");
		Console.WriteLine($"For {collection.Count()} objects, first object: {collection.FirstObject()?.ToString()}, last object: {collection.LastObject()?.ToString()}");

        Console.WriteLine();
		return 0;
	}

	public void RunAllTest()
	{
		foreach (var collection in _collection)
		{
			RunTest(collection);
		}
	}
}
