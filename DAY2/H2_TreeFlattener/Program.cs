using System;
using System.Collections.Generic;

// H2 - Tree Flattener
// Uses: recursion, local function, ref parameter for depth, params for multiple roots

class TreeNode
{
    public string Value { get; set; }
    public List<TreeNode> Children { get; } = new();

    public TreeNode(string value, params TreeNode[] children)
    {
        Value = value;
        foreach (var child in children)
            Children.Add(child);
    }
}

static class TreeProcessor
{
    // Flattens one or more trees into a single list using depth-first order
    public static List<string> FlattenTree(params TreeNode[] roots)
    {
        var result = new List<string>();

        // Local function: only visible inside FlattenTree, handles recursion
        // ref depth tracks how deep we are so we can print it for each node
        void Traverse(TreeNode node, ref int depth)
        {
            if (node == null) return;

            result.Add(node.Value);
            Console.WriteLine($"  {node.Value} (depth {depth})");

            depth++;
            foreach (var child in node.Children)
                Traverse(child, ref depth);
            depth--; // go back up one level after visiting all children
        }

        foreach (var root in roots)
        {
            int depth = 0;
            Traverse(root, ref depth);
        }

        return result;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== H2: Tree Flattener ===\n");

        // Root1: A -> A1, A2
        var root1 = new TreeNode("A",
            new TreeNode("A1"),
            new TreeNode("A2")
        );

        // Root2: B -> B1 -> B1a, B1b
        var root2 = new TreeNode("B",
            new TreeNode("B1",
                new TreeNode("B1a"),
                new TreeNode("B1b")
            )
        );

        // Root3: C has no children
        var root3 = new TreeNode("C");

        Console.WriteLine("Traversal order:");
        List<string> flat = TreeProcessor.FlattenTree(root1, root2, root3);

        Console.WriteLine();
        Console.WriteLine("Flattened: " + string.Join(", ", flat));
    }
}
