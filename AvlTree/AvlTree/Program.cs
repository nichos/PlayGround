using System;
using System.Collections.Generic;
namespace AvlTree
{
    static class Program
    {
        static void Main(string[] args)
        {
            Node root = null;
            Console.WriteLine("Start...");

            Console.WriteLine("1 = Add");
            Console.WriteLine("2 = Delete");
            Console.WriteLine("99 = Exit");

            Console.Write("Please select menu: ");
            var menu = Console.ReadLine();
            while (menu != "99")
            {

                switch (menu)
                {
                    case "1":
                        {
                            Console.Write("enter number:");
                            var inputStr = Console.ReadLine();
                            var input = Convert.ToInt32(inputStr);
                            root = Add(root, input);
                        }

                        break;
                    case "2":
                        {
                            Console.Write("enter number:");
                            var inputStr = Console.ReadLine();
                            var input = Convert.ToInt32(inputStr);
                            root = Delete(root, input);
                        }

                        break;
                }

                root.Print();

                Console.Write("Please select menu: ");
                menu = Console.ReadLine();
            }
        }

        static int MaxHeight(Node a, Node b)
        {
            var heightA = a?.Height ?? -1;
            var heightB = b?.Height ?? -1;
            if (heightA > heightB)
            {
                return heightA;
            }
            else
            {
                return heightB;
            }
        }

        static Node Add(Node node, int value)
        {
            if (node == null)
            {
                node = new Node();
                node.Value = value;
                node.Height = 0;
                return node;
            }

            if (value <= node.Value)
            {
                node.Left = Add(node.Left, value);
            }
            else
            {
                node.Right = Add(node.Right, value);
            }

            node = BalancingTree(node);
            node.Height = MaxHeight(node?.Left, node?.Right) + 1;

            return node;
        }

        static Node Delete(Node node, int value)
        {
            if (node == null)
            {
                return null;
            }

            if (value < node.Value)
            {
                node.Left = Delete(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = Delete(node.Right, value);
            }
            else
            {
                if (node.Left == null && node.Right == null)
                {
                    node = null;
                }
                else if (node.Left != null && node.Right != null)
                {
                    var minValue = FindMin(node.Right);
                    node.Value = minValue;
                    node.Right = Delete(node.Right, minValue);
                }
                else
                {
                    if(node.Left != null)
                    {
                        node = node.Left;
                    }
                    else
                    {
                        node = node.Right;
                    }
                }
            }

            if (node != null)
            {
                node = BalancingTree(node);
                node.Height = MaxHeight(node?.Left, node?.Right) + 1;
            }

            return node;
        }

        static int FindMin(Node node)
        {
            var temp = node;
            int min = node.Value;
            while(temp != null)
            {
                min = temp.Value;
                temp = temp.Left;
            }
            return min;
        }

        static Node BalancingTree(Node node)
        {
            var leftHeight = node?.Left?.Height ?? -1;
            var rightHeight = node?.Right?.Height ?? -1;
            var delta = leftHeight - rightHeight;
            if (delta > 1 || delta < -1)
            {
                Console.WriteLine("unbalance tree");
                if (delta > 1)
                {
                    var leftChildHeigh = node?.Left?.Left?.Height ?? -1;
                    var rightChildHeigh = node?.Left?.Right?.Height ?? -1;
                    var childDelta = leftChildHeigh - rightChildHeigh;
                    if (childDelta > 0)
                    {
                        Console.WriteLine("LL");
                        node = RotateLL(node);
                    }
                    else
                    {
                        Console.WriteLine("LR");
                        node = RotateLR(node);
                    }
                }
                else
                {
                    var leftChildHeigh = node?.Right?.Left?.Height ?? -1;
                    var rightChildHeigh = node?.Right?.Right?.Height ?? -1;
                    var childDelta = leftChildHeigh - rightChildHeigh;
                    if (childDelta < 0)
                    {
                        Console.WriteLine("RR");
                        node = RotateRR(node);
                    }
                    else
                    {
                        Console.WriteLine("RL");
                    }
                }
            }
            return node;
        }

        private static Node RotateLL(Node node)
        {
            var pivot = node.Left;
            node.Left = pivot.Right;
            pivot.Right = node;

            node.Height = MaxHeight(node?.Left, node?.Right) + 1;
            return pivot;
        }

        private static Node RotateRR(Node node)
        {
            var pivot = node.Right;
            node.Right = pivot.Left;
            pivot.Left = node;

            node.Height = MaxHeight(node?.Left, node?.Right) + 1;
            return pivot;
        }

        private static Node RotateLR(Node node)
        {
            var pivot = node.Left;
            node.Left = RotateRR(pivot);
            return RotateLL(node);
        }

        private static Node RotateRL(Node node)
        {
            var pivot = node.Right;
            node.Right = RotateLL(pivot);
            return RotateRR(node);
        }

        private static void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }

        public static void Print(this Node root, string textFormat = "0", int spacing = 1, int topMargin = 2, int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo { Node = next, Text = $"{next.Value.ToString(textFormat)}:{next.Height}" };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + spacing;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.Node.Left)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
                    }
                }
                next = next.Left ?? next.Right;
                for (; next == null; item = item.Parent)
                {
                    int top = rootTop + 2 * level;
                    Print(item.Text, top, item.StartPos);
                    if (item.Left != null)
                    {
                        Print("/", top + 1, item.Left.EndPos);
                        Print("_", top, item.Left.EndPos + 1, item.StartPos);
                    }
                    if (item.Right != null)
                    {
                        Print("_", top, item.EndPos, item.Right.StartPos - 1);
                        Print("\\", top + 1, item.Right.StartPos - 1);
                    }
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos + 1;
                        next = item.Parent.Node.Right;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos - 1;
                        else
                            item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }
    }

    class Node
    {
        public int Value;
        public int Height;

        public Node Left;
        public Node Right;
    }

    class NodeInfo
    {
        public Node Node;
        public string Text;
        public int StartPos;
        public int Size { get { return Text.Length; } }
        public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
        public NodeInfo Parent, Left, Right;
    }
}
