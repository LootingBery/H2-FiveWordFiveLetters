
using System.Reflection;
using CollectionTester;

var dynamicSize = Rng.Range(50000, 250000);
var input = new string[dynamicSize];
for(var i = 0; i < dynamicSize; i++)
{
    input[i] =  Rng.Range(0, dynamicSize+1).ToString();
}

var tester = new CollectionTester<int>(input, StringToInt, x => x);

//Find all SubClass of baseCollection and add them to the test.
var assembly = Assembly.GetExecutingAssembly();
var derivedTypes = assembly.GetTypes().
        Where(t => t.IsClass && !t.IsAbstract && t.IsGenericType && t.GetGenericTypeDefinition().BaseType.ToString() == typeof(BaseCollection<>).ToString());
foreach (var type in derivedTypes)
{
    var instance = Activator.CreateInstance(type.MakeGenericType(typeof(int))) as BaseCollection<int>;
    if (instance != null)
        tester.Add(instance);
}

//Run test
Console.WriteLine($"Testing collection size is {dynamicSize}.");
tester.RunAllTest();
Console.WriteLine("Press any key...");
Console.ReadKey();
return;

int StringToInt (string s)
{
    return int.Parse (s);
}