# Test af collections
I skal teste forskellige implementeringer af collections. Hvilke er hurtigst, hvilke
fordel/ulemper har de forskellige
I skal bruge koden Collection:
1. I skal for alle de collection I vil afprøve lave en klasser der nedarver fra BaseCollection<T>
2. Sammenlign de forskellige resultater.
3. Ekstra: Omskriv testen til at bruge et objekt med en property for int og string.
4. Er der andre test der vil være relevante?
5. Hvordan er ConcurrentCollection til sammenligning?

```mermaid
classDiagram
    class BaseCollection~T~ {
        <<abstract>>
        - long TotalElapsedMilliseconds
        + long FillCollection(string[] input, Func~string, T~ func)
        + long SortCollection(Func~T, T~ comparer)
        + long PrintCollection(TextWriter writer)
        # abstract void FillCollectionInternal(string[] input, Func~string, T~ func)
        # abstract void SortCollectionInternal(Func~T, T~ comparer)
        # abstract void PrintCollectionInternal(TextWriter writer)
        + abstract T FirstObject()
        + abstract T LastObject()
        + abstract int Count()
    }

    class BaseLineCollection~T~ {
        - List~T~ _list
        + T FirstObject()
        + T LastObject()
        + int Count()
        # void FillCollectionInternal(string[] input, Func~string, T~ func)
        # void SortCollectionInternal(Func~T, T~ comparer)
        # void PrintCollectionInternal(TextWriter writer)
    }
    BaseLineCollection~T~ --|> BaseCollection~T~

    class ArrayCollection~T~ {
        - T[] array
        + T FirstObject()
        + T LastObject()
        + int Count()
        # void FillCollectionInternal(string[] input, Func~string, T~ func)
        # void SortCollectionInternal(Func~T, T~ comparer)
        # void PrintCollectionInternal(TextWriter writer)
    }
    ArrayCollection~T~ --|> BaseCollection~T~

    class CollectionTester~T~ {
        - List~BaseCollection~T~~ _collection
        + void Add(BaseCollection~T~ collection)
        + int Count()
        + void RunAllTest()
    }

    class Rng {
        + static int Range(int lower, int higher)
    }

    class Program {
        main()
    }

    CollectionTester~T~ *-- BaseCollection~T~
    Program --> CollectionTester~T~
    Program --> Rng

```