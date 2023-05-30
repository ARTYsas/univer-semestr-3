using System;
using System.Collections.Generic;

class RealNumberSet
{
    private SortedSet<double> set;

    public RealNumberSet()
    {
        set = new SortedSet<double>();
    }

    public RealNumberSet(IEnumerable<double> collection)
    {
        set = new SortedSet<double>(collection);
    }

    public static RealNumberSet operator +(RealNumberSet lhs, double rhs)
    {
        RealNumberSet result = new RealNumberSet(lhs.set);
        result.Add(rhs);
        return result;
    }

    public static RealNumberSet operator -(RealNumberSet lhs, double rhs)
    {
        RealNumberSet result = new RealNumberSet(lhs.set);
        result.Remove(rhs);
        return result;
    }

    public static RealNumberSet operator &(RealNumberSet lhs, RealNumberSet rhs)
    {
        RealNumberSet result = new RealNumberSet();
        foreach (double element in lhs.set)
        {
            if (rhs.Contains(element))
            {
                result.Add(element);
            }
        }
        return result;
    }

    public static RealNumberSet operator |(RealNumberSet lhs, RealNumberSet rhs)
    {
        RealNumberSet result = new RealNumberSet(lhs.set);
        foreach (double element in rhs.set)
        {
            result.Add(element);
        }
        return result;
    }

    public void Add(double element)
    {
        set.Add(element);
    }

    public void Remove(double element)
    {
        set.Remove(element);
    }

    public bool Contains(double element)
    {
        return set.Contains(element);
    }

    public void Clear()
    {
        set.Clear();
    }

    public void Display()
    {
        Console.Write("{ ");
        foreach (double element in set)
        {
            Console.Write($"{element} ");
        }
        Console.WriteLine("}");
    }
}


class Program
{
    static void Main(string[] args)
    {
        RealNumberSet set = new RealNumberSet();

        while (true)
        {
            Console.WriteLine("RealNumberSet Menu:");
            Console.WriteLine("1. Add element");
            Console.WriteLine("2. Remove element");
            Console.WriteLine("3. Check membership");
            Console.WriteLine("4. Clear set");
            Console.WriteLine("5. Intersection");
            Console.WriteLine("6. Union");
            Console.WriteLine("7. Display set");
            Console.WriteLine("0. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 0:
                    Console.WriteLine("Exiting program...");
                    return;
                case 1:
                    Console.Write("Enter element to add: ");
                    double elementToAdd = double.Parse(Console.ReadLine());
                    set.Add(elementToAdd);
                    Console.WriteLine($"{elementToAdd} added to the set.");
                    break;
                case 2:
                    Console.Write("Enter element to remove: ");
                    double elementToRemove = double.Parse(Console.ReadLine());
                    set.Remove(elementToRemove);
                    Console.WriteLine($"{elementToRemove} removed from the set.");
                    break;
                case 3:
                    Console.Write("Enter element to check: ");
                    double elementToCheck = double.Parse(Console.ReadLine());
                    if (set.Contains(elementToCheck))
                    {
                        Console.WriteLine($"{elementToCheck} is in the set.");
                    }
                    else
                    {
                        Console.WriteLine($"{elementToCheck} is not in the set.");
                    }
                    break;
                case 4:
                    set.Clear();
                    Console.WriteLine("Set cleared.");
                    break;
                case 5:
                    Console.Write("Enter elements of second set (separated by spaces): ");
                    RealNumberSet otherSet = new RealNumberSet(Array.ConvertAll(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries), double.Parse));
                    RealNumberSet intersection = set & otherSet;
                    Console.Write("Intersection: ");
                    intersection.Display();
                    break;
                case 6:
                    Console.Write("Enter elements of second set (separated by spaces): ");
                    RealNumberSet unionSet = new RealNumberSet(Array.ConvertAll(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries), double.Parse));
                    RealNumberSet union = set | unionSet;
                    Console.Write("Union: ");
                    union.Display();
                    break;
                case 7:
                    set.Display();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

