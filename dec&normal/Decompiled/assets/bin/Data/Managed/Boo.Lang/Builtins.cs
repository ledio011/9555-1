using System;
using System.Collections;
using System.Text;

namespace Boo.Lang
{
	// Token: 0x02000002 RID: 2
	public class Builtins
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static string join(IEnumerable enumerable, string separator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IEnumerator enumerator = enumerable.GetEnumerator();
			using (enumerator as IDisposable)
			{
				if (enumerator.MoveNext())
				{
					stringBuilder.Append(enumerator.Current);
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(separator);
						stringBuilder.Append(enumerator.Current);
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
