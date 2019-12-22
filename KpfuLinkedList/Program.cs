using System;

namespace KpfuLinkedList
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var list = new LinkedList<int>();
            list.Add(4);
            list.Add(41);
            list.Add(56);
            list.Add(-43);
            list.Add(0);
            list.Add(33);
            list.Add(33);
            list.Add(33);
            list.Add(76);
            list.Add(65);
            list.Remove(value: 0);

            Console.WriteLine(list.ToString());
            Console.WriteLine("contains 56: " + list.Contains(56));
            Console.WriteLine("contains 222: " + list.Contains(222));
            Console.WriteLine("index Of 65: " + list.IndexOf(65));
            Console.WriteLine("index Of -435: " + list.IndexOf(-435));
            Console.WriteLine("count of 33: " + list.Count(33));
            Console.WriteLine("count of 543: " + list.Count(543));
            list.Remove(index: 5);
            Console.WriteLine(list.ToString());
            list.Clear();
            Console.WriteLine(list.ToString());
        }
    }
}