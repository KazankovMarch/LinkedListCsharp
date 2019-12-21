using System;

namespace KpfuLinkedList
{
    public class LinkedList<T> where T: IComparable
    {
        private int length;

        private Node head;
        private class Node
        {
            internal T Value;
            internal Node Next;
            internal Node Previous;

            public Node() { }
            public Node(T value)
            {
                Value = value;
            }
        }
        
        private class NodeIterator
        {
            private Node Head;
            private Node Current;
            private Node Next;
            public NodeIterator(LinkedList<T> list)
            {
                Head = list.head;
                Current = Head;
                Next = Current.Next;
            }

            public bool hasNext()
            {
                return Next != Head;
            }

            public Node GetNext()
            {
                if (!hasNext()) throw new IndexOutOfRangeException();
                
                Next = Next.Next;
                Current = Current.Next;
                return Current;
            }
        }

        public LinkedList()
        {
            head = new Node();
            head.Next = head;
            head.Previous = head;
            length = 0;
        }

        public void Add(T value)
        {
            Node next = FindNodeGreaterThan(value);
            Node newNode = new Node(value);
            AddNodeBefore(next, newNode);
        }

        public void Remove(T value)
        {
            Node nodeForRemove = FindNodeGreaterThan(value).Previous;
            if (nodeForRemove.Value.Equals(value))
            {
                RemoveNode(nodeForRemove);
            }
        }

        public void Remove(int index)
        {
            if (index >= length) throw new IndexOutOfRangeException("index is = " + index + ", length = " + length);

            Node nodeForRemove = FindNodeByIndex(index);
            RemoveNode(nodeForRemove);
        }
        
        public void Clear()
        {
            DoWithEachNode(RemoveNode);
        }

        public override string ToString()
        {
            var iterator = Iterator();
            string result = "{";
            while (iterator.hasNext())
            {
                result += iterator.GetNext().Value + " ";
            }

            return result + "}";
        }

        public bool Contains(T value)
        {
            return IndexOf(value) != -1;
        }

        public int IndexOf(T value)
        {
            var iterator = Iterator();
            var index = 0;
            T nextValue;
            while (iterator.hasNext() && 
                   (nextValue = iterator.GetNext().Value).CompareTo(value) <= 0)
            {
                if (nextValue.Equals(value))
                    return index;
                
                index++;
            }

            return -1;
        }

        public int Count(T value)
        {
            var iterator = Iterator();
            var result = 0;
            T nextValue;
            while (iterator.hasNext() && 
                   (nextValue = iterator.GetNext().Value).CompareTo(value) <= 0)
            {
                if (nextValue.Equals(value))
                    result++;
            }

            return result;
        }
        
        private void DoWithEachNode(Action<Node> action)
        {
            var iterator = Iterator();
            while (iterator.hasNext())
            {
                action(iterator.GetNext());
            }
        }

        private NodeIterator Iterator()
        {
            return new NodeIterator(this);
        }

        private Node FindNodeByIndex(int index)
        {
            if (index >= length) throw new IndexOutOfRangeException("index is = " + index + ", length = " + length);
            
            var iterator = Iterator();
            for (int i = 0; i < index; i++)
            {
                iterator.GetNext();
            }

            return iterator.GetNext();
        }
        
        private Node FindNodeGreaterThan(T value)
        {
            var iterator = Iterator();
            while (iterator.hasNext())
            {
                var current = iterator.GetNext();
                if (current.Value.CompareTo(value) > 0)
                    return current;
            }

            return head;
        }
        
        private void RemoveNode(Node nodeForRemove)
        {
            var next = nodeForRemove.Next;
            var previous = nodeForRemove.Previous;
            next.Previous = previous;
            previous.Next = next;
            length--;
            //dispose nodeForRemove?
        }

        private void AddNodeBefore(Node next, Node newNode)
        {
            Node previous = next.Previous;
            previous.Next = newNode;
            next.Previous = newNode;
            newNode.Next = next;
            newNode.Previous = previous;
            length++;
        }
    }
}