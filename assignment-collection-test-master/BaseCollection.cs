namespace CollectionTester;

public abstract class BaseCollection<T>
{
    public long TotalElapsedMilliseconds { get; private set; } = 0;
    private static System.Diagnostics.Stopwatch StartTime()
    {
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        return watch;
    }

    private long StopAndAddTime(System.Diagnostics.Stopwatch watch)
    {
        watch.Stop();
        TotalElapsedMilliseconds += watch.ElapsedMilliseconds;
        return watch.ElapsedMilliseconds;
    }

    public long FillCollection(string[] input, Func<string, T> func)
    {
        var watch = StartTime();
        FillCollectionInternal(input, func);
        return StopAndAddTime(watch);
    }

    public long SortCollection(Func<T, T> comparer)
    {
        var watch = StartTime();
        SortCollectionInternal(comparer);
        return StopAndAddTime(watch);
    }

    public long PrintCollection(TextWriter writer)
    {
        var watch = StartTime();
        PrintCollectionInternal(writer);
        return StopAndAddTime(watch);
    }

    protected abstract void FillCollectionInternal(string[] input, Func<string, T> func);
    protected abstract void SortCollectionInternal(Func<T, T> comparer);
    protected abstract void PrintCollectionInternal(TextWriter writer);

    public abstract T FirstObject();
    public abstract T LastObject();
    public abstract int Count();
}
