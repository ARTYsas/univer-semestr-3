using System;
using System.Linq;

class StringArray
{
    private string[] array;

    // Constructor
    public StringArray(int size)
    {
        array = new string[size];
    }

    // Indexer to access individual strings by index
    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= array.Length)
            {
                throw new IndexOutOfRangeException();
            }
            return array[index];
        }
        set
        {
            if (index < 0 || index >= array.Length)
            {
                throw new IndexOutOfRangeException();
            }
            array[index] = value;
        }
    }

    // Access strings by initial character combination
    public string[] GetByPrefix(string prefix)
    {
        return array.Where(s => s.StartsWith(prefix)).ToArray();
    }

    // Link two arrays element by element to form a new array
    public static StringArray Link(StringArray a, StringArray b)
    {
        if (a.array.Length != b.array.Length)
        {
            throw new ArgumentException("Arrays must have the same length.");
        }
        StringArray result = new StringArray(a.array.Length);
        for (int i = 0; i < a.array.Length; i++)
        {
            result[i] = a[i] + b[i];
        }
        return result;
    }

    // Merge two arrays with elimination of repeating elements
    public static StringArray Merge(StringArray a, StringArray b)
    {
        string[] merged = a.array.Concat(b.array).Distinct().ToArray();
        StringArray result = new StringArray(merged.Length);
        Array.Copy(merged, result.array, merged.Length);
        return result;
    }

    // Intersection of two arrays
    public static StringArray Intersect(StringArray a, StringArray b)
    {
        string[] intersect = a.array.Intersect(b.array).ToArray();
        StringArray result = new StringArray(intersect.Length);
        Array.Copy(intersect, result.array, intersect.Length);
        return result;
    }

    // Display individual string by index
    public void Display(int index)
    {
        Console.WriteLine(this[index]);
    }

    // Display the whole array
    public void DisplayAll()
    {
        Console.WriteLine(string.Join(", ", array));
    }
}

class Program
{
    static void Main(string[] args)
    {
        StringArray array = new StringArray(5);
        array[0] = "apple";
        array[1] = "banana";
        array[2] = "carrot";
        array[3] = "dog";
        array[4] = "elephant";

        while (true)
        {
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. Access individual string by index");
            Console.WriteLine("2. Access strings by prefix");
            Console.WriteLine("3. Link two arrays element by element");
            Console.WriteLine("4. Merge two arrays with elimination of repeating elements");
            Console.WriteLine("5. Intersection of two arrays");
            Console.WriteLine("6. Display individual string by index");
            Console.WriteLine("7. Display the whole array");
            Console.WriteLine("8. Exit");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter the index:");
                    int index = int.Parse(Console.ReadLine());
                    Console.WriteLine(array[index]);
                    break;
                case 2:
                    Console.WriteLine("Enter the prefix:");
                    string prefix = Console.ReadLine();
                    string[] result = array.GetByPrefix(prefix);
                    Console.WriteLine(string.Join(", ", result));
                    break;
                case 3:
                    Console.WriteLine("Enter the size of the new array:(it must be 5 or u will have eror, them have be same lengh)");
                    int size = int.Parse(Console.ReadLine());
                    StringArray other = new StringArray(size);
                    Console.WriteLine("Enter the strings of the new array:");
                    for (int i = 0; i < size; i++)
                    {
                        other[i] = Console.ReadLine();
                    }
                    StringArray linked = StringArray.Link(array, other);
                    Console.WriteLine("Linked array:");
                    linked.DisplayAll();
                    break;
                case 4:
                    Console.WriteLine("Enter the size of the new array:");
                    size = int.Parse(Console.ReadLine());
                    other = new StringArray(size);
                    Console.WriteLine("Enter the strings of the new array:");
                    for (int i = 0; i < size; i++)
                    {
                        other[i] = Console.ReadLine();
                    }
                    StringArray merged = StringArray.Merge(array, other);
                    Console.WriteLine("Merged array:");
                    merged.DisplayAll();
                    break;
                case 5:
                    Console.WriteLine("Enter the size of the new array:");
                    size = int.Parse(Console.ReadLine());
                    other = new StringArray(size);
                    Console.WriteLine("Enter the strings of the new array:");
                    for (int i = 0; i < size; i++)
                    {
                        other[i] = Console.ReadLine();
                    }
                    StringArray intersect = StringArray.Intersect(array, other);
                    Console.WriteLine("Intersection array:");
                    intersect.DisplayAll();
                    break;
                case 6:
                    Console.WriteLine("Enter the index:");
                    index = int.Parse(Console.ReadLine());
                    array.Display(index);
                    break;
                case 7:
                    array.DisplayAll();
                    break;
                case 8:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

