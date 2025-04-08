// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using TreeWorks;

Console.WriteLine("Hello, World!");

var tr = new TreeClass();
/*tr.AddNode(10);
tr.AddNode(8);
tr.AddNode(7);
tr.AddNode(9);

tr.AddNode(11);
tr.AddNode(15);
tr.AddNode(13);
tr.AddNode(12);

tr.AddNode(25);

tr.AddNode(19);*/

/*tr.Insert(3);
tr.Insert(2);

tr.Insert(5);
tr.Insert(9);*/

Test();

static void Test()
{
    Node root = null;
    TreeClass bst = new TreeClass();
    //int SIZE = 2000000;
    int SIZE = 100;
    int[] a = new int[SIZE];

    Console.WriteLine("Generating random array with {0} values...", SIZE);

    Random random = new Random();

    Stopwatch watch = Stopwatch.StartNew();

    int x=-11;

    for (int i = 0; i < SIZE; i++)
    {
        a[i] = random.Next(10000);        
        if (i == 30) x = a[i];
    }

    watch.Stop();

    


    Console.WriteLine("Done. Took {0} seconds", (double)watch.ElapsedMilliseconds / 1000.0);
    Console.WriteLine();
    Console.WriteLine("Filling the tree with {0} nodes...", SIZE);

    watch = Stopwatch.StartNew();

    for (int i = 0; i < SIZE; i++)
    {
        // root = bst.Insert( a[i]);
        root = bst.Add(a[i]);
    }

    Console.WriteLine("Найдено :" + bst.FindNode(x)?.data);

    watch.Stop();
    Console.WriteLine($"Всего  {bst.Count}");

    Console.WriteLine("Done. Took {0} seconds", (double)watch.ElapsedMilliseconds / 1000.0);
    Console.WriteLine();
    Console.WriteLine("Traversing all {0} nodes in tree...", SIZE);

    watch = Stopwatch.StartNew();

    //bst.Traverse(bst.Root);
    bst.PrintTree();

    watch.Stop();

    Console.WriteLine("Done. Took {0} seconds", (double)watch.ElapsedMilliseconds / 1000.0);
    Console.WriteLine();

    Console.ReadKey();
}