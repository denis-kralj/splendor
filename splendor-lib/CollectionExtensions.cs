using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace splendor_lib
{
    public static class CollectionExtensions
    {
        public static Stack<T> ToStack<T>(this IEnumerable<T> collection)
            => new Stack<T>(collection);
        public static ReadOnlyDictionary<T, T2> AsReadOnly<T, T2>(this Dictionary<T, T2> source)
            => new ReadOnlyDictionary<T, T2>(source);
        public static List<T> Pop<T>(this Stack<T> stack, uint amount)
        {
            var result = new List<T>((int)amount);

            for (int i = 0; i < amount; i++)
                result.Add(stack.Pop());

            return result;
        }
    }
}