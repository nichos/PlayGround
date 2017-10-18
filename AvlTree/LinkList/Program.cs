using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkList
{
    class Program
    {
        static void Main(string[] args)
        {
            Node head = null;
            AddNode(ref head, 10); 
            AddNode(ref head, 4);
            AddNode(ref head, 5);
            AddNode(ref head, 3);
            AddNode(ref head, 6);

            head = ModifyList(head);
			

            print(head);

            Console.ReadLine();
        }
		
		static Node ModifyList(Node head){
			
			Node firstList = null;
			Node secondList = null;
			
			SplitList(head, ref firstList, ref secondList);
			RevertList(ref secondList);
            

            Node result = null;
            var firstListNode = firstList;
            var secondListNode = secondList;
            while (secondListNode != null)
            {
                firstListNode.Value -= secondListNode.Value;
                firstListNode = firstListNode.Next;
                secondListNode = secondListNode.Next;
            }

            RevertList(ref secondList);
            print(firstList);
            Console.WriteLine("============");
            print(secondList);
            Console.WriteLine("============");

            firstListNode = firstList;
            while(firstListNode.Next != null)
            {
                firstListNode = firstListNode.Next;
            }

            firstListNode.Next = secondList;

            return firstList;

            //secondListNode = secondList;
            //while(secondListNode != null)
            //{
            //    AddNode(ref result, secondListNode.Value);
            //    secondListNode = secondListNode.Next;
            //}

            //return head;
		}

        static void RevertList(ref Node node)
        {
            if(node == null)
            {
                return;
            }

            Node cur = node;
            Node prev = null;
			Node next = null;
			while(cur != null){
				next = cur.Next;
                cur.Next = prev;
				prev = cur;
                cur = next;
			}
			
			node = prev;
        }
		
		static void SplitList(Node head, ref Node firstList, ref Node secondList){
			var slow = head;
			var fast = head.Next;
			
			while(fast != null){
				fast = fast.Next;
				if(fast != null){
					slow = slow.Next;
					fast = fast.Next;
				}
			}
			
			firstList = head;
			secondList = slow.Next;
			slow.Next = null;
		}

        static void AddNode(ref Node node, int value)
        {
            if(node == null)
            {
                node = new Node { Value = value };
                return;
            }

            var temp = node;
            while(temp.Next != null)
            {
                temp = temp.Next;
            }

            temp.Next = new Node { Value = value };
        }

        static void print(Node node)
        {
            while(node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }
        }
    }

    class Node
    {
        public int Value;
        public Node Next;
    }
}
