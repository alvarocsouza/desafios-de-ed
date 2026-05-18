public class Node {
    public int Key { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }
    
    public Node(int key) {
        this.Key = key;
    }
}

public class BST {
    private Node root;
    
    public BST() {
        root = null;
    }
    
    public void Insert(int value) {
        root = InsertRecursive(root, value);
    }
    private Node InsertRecursive(Node current, int value) {
        
    if (current == null) {
        return new Node(value);
    }
        
        
    if (value == current.Key) {
        return current;
    }
        
        
    if (value < current.Key) {
        current.Left = InsertRecursive(current.Left, value);
    } 
    else {
        current.Right = InsertRecursive(current.Right, value);
    }
        
        return current;
    }
    
    public Node Search(int value) {
        return SearchRecursive(root, value);
    }
    
    private Node SearchRecursive(Node current, int value) {
        
        if (current == null || current.Key == value) {
            return current;
        }
       
        if (value < current.Key) {
            return SearchRecursive(current.Left, value);
        }
        else {
            return SearchRecursive(current.Right, value);
        }
    }
}