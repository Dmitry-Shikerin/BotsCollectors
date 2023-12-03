using System.Collections.Generic;
using System.Linq;

namespace Sources.Utils
{
    public static partial class ListExtensions
    {
        public static (IEnumerable<T> added, IEnumerable<T> removed) Diff<T>
            (this IEnumerable<T> sourceCollection, IEnumerable<T> changedCollection)
        {
            IEnumerable<T> removed = sourceCollection.Except(changedCollection);
            IEnumerable<T> added = changedCollection.Except(sourceCollection);

            return (added, removed);
        }
    }
}