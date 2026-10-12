namespace CollectionTester;

public class ArrayCollection<T> : BaseCollection<T>
{
    private T[] array;

    public ArrayCollection()
    {
        this.array = [];
    }

    protected override void FillCollectionInternal(string[] input, Func<string, T> func)
    {
        foreach (var line in input)
        {
            array = array.Append(func(line)).ToArray();
        }
    }

    protected override void SortCollectionInternal(Func<T, T> comparer)
    {
        array = array.OrderBy(comparer).ToArray();
    }

    protected override void PrintCollectionInternal(TextWriter writer)
    {
        foreach (var line in array)
        {
            writer.WriteLine(line);
        }
    }

    public override T FirstObject()
    {
        return array[0];
    }

    public override T LastObject()
    {
        return array[^1];
    }

    public override int Count()
    {
        return array.Length;
    }
}