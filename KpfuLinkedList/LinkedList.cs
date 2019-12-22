using System;

namespace KpfuLinkedList
{
    public class LinkedList<T> where T : IComparable
    {
        private int _length;

        private readonly Node _head;

        private class Node
        {
            internal T Value;
            internal Node Next;
            internal Node Previous;

            public Node()
            {
            }

            public Node(T value)
            {
                Value = value;
            }
        }

        private class NodeIterator
        {
            private readonly Node _head;
            private Node _current;
            private Node _next;

            public NodeIterator(LinkedList<T> list)
            {
                _head = list._head;
                _current = _head;
                _next = _current.Next;
            }

            public bool HasNext()
            {
                return _next != _head;
            }

            public Node GetNext()
            {
                if (!HasNext()) throw new IndexOutOfRangeException();

                _next = _next.Next;
                _current = _current.Next;
                return _current;
            }
        }

        public LinkedList()
        {
            _head = new Node();
            _head.Next = _head;
            _head.Previous = _head;
            _length = 0;
        }

        public void Add(T value)
        {
            var next = FindNodeGreaterThan(value);
            var newNode = new Node(value);
            AddNodeBefore(next, newNode);
        }

        public void Remove(T value)
        {
            var nodeForRemove = FindNodeGreaterThan(value).Previous;
            if (nodeForRemove.Value.Equals(value))
            {
                RemoveNode(nodeForRemove);
            }
        }

        public void Remove(int index)
        {
            if (index >= _length) throw new IndexOutOfRangeException("index is = " + index + ", length = " + _length);

            var nodeForRemove = FindNodeByIndex(index);
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
            while (iterator.HasNext())
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
            while (iterator.HasNext() &&
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
            while (iterator.HasNext() &&
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
            while (iterator.HasNext())
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
            if (index >= _length) throw new IndexOutOfRangeException("index is = " + index + ", length = " + _length);

            var iterator = Iterator();
            for (var i = 0; i < index; i++)
            {
                iterator.GetNext();
            }

            return iterator.GetNext();
        }

        private Node FindNodeGreaterThan(T value)
        {
            var iterator = Iterator();
            while (iterator.HasNext())
            {
                var current = iterator.GetNext();
                if (current.Value.CompareTo(value) > 0)
                    return current;
            }

            return _head;
        }

        private void RemoveNode(Node nodeForRemove)
        {
            var next = nodeForRemove.Next;
            var previous = nodeForRemove.Previous;
            next.Previous = previous;
            previous.Next = next;
            _length--;
            //dispose nodeForRemove?
        }

        private void AddNodeBefore(Node next, Node newNode)
        {
            var previous = next.Previous;
            previous.Next = newNode;
            next.Previous = newNode;
            newNode.Next = next;
            newNode.Previous = previous;
            _length++;
        }
    }
}