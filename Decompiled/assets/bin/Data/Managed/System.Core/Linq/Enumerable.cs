using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000005 RID: 5
	public static class Enumerable
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002074 File Offset: 0x00000274
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return Enumerable.CreateWhereIterator<TSource>(source, predicate);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002084 File Offset: 0x00000284
		private static IEnumerable<TSource> CreateWhereIterator<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			foreach (TSource element in source)
			{
				if (predicate(element))
				{
					yield return element;
				}
			}
			yield break;
		}
	}
}
