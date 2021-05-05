using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiQuasar.Services.Utils
{
    public static class Zips
    {
        public static IEnumerable<TResult> Zip3<T1, T2, T3, TResult>(
            this IEnumerable<T1> seq1,
            IEnumerable<T2> seq2,
            IEnumerable<T3> seq3,
            Func<T1, T2, T3, TResult> selector) => seq1
                .Zip(seq2, (x, y) => (x, y))
                .Zip(seq3, (x, y) => (x.x, x.y, y))
                .Select(x => selector(x.x, x.Item2, x.Item3));

    }
}
