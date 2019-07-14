using System.Collections.Generic;

namespace splendor_lib
{
    public static class IEnumerableExtensions
    {
        public static Stack<T> ToStack<T>(this IEnumerable<T> collection)
        {
            return new Stack<T>(collection);
        }
    }
}