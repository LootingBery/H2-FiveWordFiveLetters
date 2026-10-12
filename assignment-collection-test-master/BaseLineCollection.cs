namespace CollectionTester;

public class BaseLineCollection<T> : BaseCollection<T>
{
    private List<T> _list = [];
    protected override void FillCollectionInternal(string[] input, Func<string, T> func)
    {
        foreach (var line in input)
            _list.Add(func(line));
    }

    protected override void SortCollectionInternal(Func<T, T> comparer)
    {
        var orderedEnumerable = _list.OrderBy(comparer);
        _list = orderedEnumerable.ToList();
    }

    protected override void PrintCollectionInternal(TextWriter writer)
    {
        foreach (var line in _list)
        {
            writer.WriteLine(line);
        }
    }

    public override T FirstObject()
    {
        return _list.First();
    }

    public override T LastObject()
    {
        return _list.Last();
    }

    public override int Count()
    {
        return _list.Count;
    }
}