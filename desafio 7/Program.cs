using System;
using System.Collections.Generic;
using System.Linq;

public class Node
{
    public int Key;
    public Node Left;
    public Node Right;
    public int Height;

    public Node(int key)
    {
        Key = key;
        Height = 1;
    }
}

public class BST
{
    public Node Root;

    public Node InsertRec(Node node, int value)
    {
        if (node == null)
            return new Node(value);

        if (value < node.Key)
            node.Left = InsertRec(node.Left, value);
        else
            node.Right = InsertRec(node.Right, value);

        return node;
    }

    public void Insert(int value)
    {
        Root = InsertRec(Root, value);
    }

    public int Altura(Node node)
    {
        if (node == null) return 0;

        int esq = Altura(node.Left);
        int dir = Altura(node.Right);

        return 1 + Math.Max(esq, dir);
    }

    public int GetAltura()
    {
        return Altura(Root);
    }
}


public class AVL
{
    public Node Root;

    int Height(Node n)
    {
        return n == null ? 0 : n.Height;
    }

    int GetBalance(Node n)
    {
        return n == null ? 0 : Height(n.Left) - Height(n.Right);
    }

    Node RotateRight(Node y)
    {
        Node x = y.Left;
        Node T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = 1 + Math.Max(Height(y.Left), Height(y.Right));
        x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));

        return x;
    }

    Node RotateLeft(Node x)
    {
        Node y = x.Right;
        Node T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));
        y.Height = 1 + Math.Max(Height(y.Left), Height(y.Right));

        return y;
    }

    Node InsertRec(Node node, int key)
    {
        if (node == null)
            return new Node(key);

        if (key < node.Key)
            node.Left = InsertRec(node.Left, key);
        else if (key > node.Key)
            node.Right = InsertRec(node.Right, key);
        else
            return node;

        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

        int balance = GetBalance(node);

        if (balance > 1 && key < node.Left.Key)
            return RotateRight(node);

        if (balance < -1 && key > node.Right.Key)
            return RotateLeft(node);

       
        if (balance > 1 && key > node.Left.Key)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

    
        if (balance < -1 && key < node.Right.Key)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    public void Insert(int key)
    {
        Root = InsertRec(Root, key);
    }

    public int Altura(Node node)
    {
        if (node == null) return 0;
        return 1 + Math.Max(Altura(node.Left), Altura(node.Right));
    }

    public int GetAltura()
    {
        return Altura(Root);
    }
}

public class Program
{
    static List<int> GerarNumerosUnicos(int N)
    {
        Random rand = new Random();
        HashSet<int> set = new HashSet<int>();

        while (set.Count < N)
        {
            set.Add(rand.Next(1, 1000));
        }

        return set.ToList();
    }

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Menu: 1) nova simulação ou 2) sair");
            string op = Console.ReadLine();

            if (op == "2")
            {
                Console.WriteLine("Tchau!");
                break;
            }

            Console.Write("Digite a quantidade de amostras: ");
            int A = int.Parse(Console.ReadLine());

            Console.Write("Digite a quantidade de elementos: ");
            int N = int.Parse(Console.ReadLine());

            double somaBST = 0;
            double somaAVL = 0;

            for (int i = 0; i < A; i++)
            {
                BST bst = new BST();
                AVL avl = new AVL();

                List<int> numeros = GerarNumerosUnicos(N);

                foreach (int num in numeros)
                {
                    bst.Insert(num);
                    avl.Insert(num);
                }

                int alturaBST = bst.GetAltura();
                int alturaAVL = avl.GetAltura();

                somaBST += alturaBST;
                somaAVL += alturaAVL;
            }

            double mediaBST = somaBST / A;
            double mediaAVL = somaAVL / A;
            double mediaGeral = (somaBST + somaAVL) / (2 * A);

            Console.WriteLine("\nExperimento:");
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Altura média geral:     {mediaGeral:F2}");
            Console.WriteLine($"Altura média BST comum: {mediaBST:F2}");
            Console.WriteLine($"Altura média AVL:       {mediaAVL:F2}");
            Console.WriteLine("----------------------------------\n");
        }
    }
}