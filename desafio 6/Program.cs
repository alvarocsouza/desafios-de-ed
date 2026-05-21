public class Node {
    public int Key { get; set; }
    public Node? Left { get; set; }
    public Node? Right { get; set; }
    
    public Node(int key) {
        Key = key;
        Left = null;
        Right = null;
    }
}

public class BST {
    private Node? root;
    
    public BST() {
        root = null;
    }
    
    public void Insert(int value) {
        root = InsertRecursivo(root, value);
    }
    
    private Node InsertRecursivo(Node? current, int value) {
        if (current == null) {
            return new Node(value);
        }
        
        if (value == current.Key) {
            return current;
        }
        
        if (value < current.Key) {
            current.Left = InsertRecursivo(current.Left, value);
        } 
        else {
            current.Right = InsertRecursivo(current.Right, value);
        }
        
        return current;
    }
    
    public Node? Search(int value) {
        return SearchRecursivo(root, value);
    }
    
    private Node? SearchRecursivo(Node? current, int value) {
        if (current == null || current.Key == value) {
            return current;
        }
        
        if (value < current.Key) {
            return SearchRecursivo(current.Left, value);
        }
        else {
            return SearchRecursivo(current.Right, value);
        }
    }
    
    public Node? Maximo() {
        if (root == null) return null;
        
        Node current = root;
        while (current.Right != null) {
            current = current.Right;
        }
        return current;
    }
    
    public Node? maximoRecursivo() {
        return MaximoRecursivo(root);
    }
    
    private Node? MaximoRecursivo(Node? current) {
        if (current == null) return null;
        if (current.Right == null) return current;
        return MaximoRecursivo(current.Right);
    }
    
    public Node? Minimo() {
        if (root == null) return null;
        
        Node current = root;
        while (current.Left != null) {
            current = current.Left;
        }
        return current;
    }
    
    public Node? minimoRecursivo() {
        return MinimoRecursivo(root);
    }
    
    private Node? MinimoRecursivo(Node? current) {
        if (current == null) return null;
        if (current.Left == null) return current;
        return MinimoRecursivo(current.Left);
    }
        public void PrintInOrder() {
        PrintInOrderRecursivo(root);
        Console.WriteLine();
    }
    
    private void PrintInOrderRecursivo(Node? node) {
        if (node != null) {
            PrintInOrderRecursivo(node.Left);
            Console.Write(node.Key + " ");
            PrintInOrderRecursivo(node.Right);
        }
    }
    
    public void PrintInOrderIterativo() {
        if (root == null) return;
        
        Stack<Node> stack = new Stack<Node>();
        Node? current = root;
        
        while (current != null || stack.Count > 0) {
            while (current != null) {
                stack.Push(current);
                current = current.Left;
            }
            
            current = stack.Pop();
            Console.Write(current.Key + " ");
            current = current.Right;
        }
        Console.WriteLine();
    }
  
    public void CoolPrint() {
        CoolPrintRecursivo(root, "");
    }
    
    private void CoolPrintRecursivo(Node? node, string indent) {
        if (node == null) return;
        
        Console.WriteLine(indent + node.Key);
        CoolPrintRecursivo(node.Left, indent + "    ");
        CoolPrintRecursivo(node.Right, indent + "    ");
    }
}


public class Program {
    public static void Main(string[] args) {
        BST bst = new BST();
        
        bst.Insert(15);
        bst.Insert(10);
        bst.Insert(8);
        bst.Insert(12);
        bst.Insert(20);
        bst.Insert(21);
        
        Console.WriteLine("In-order traversal (Sorted keys):");
        bst.PrintInOrder();
        Console.WriteLine("Visualização mais legal:");
        bst.CoolPrint();
    }
}
