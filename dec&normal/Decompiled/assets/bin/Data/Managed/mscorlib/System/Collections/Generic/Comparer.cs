using System;

namespace System.Collections.Generic
{
	// Token: 0x02000085 RID: 133
	[Serializable]
	public abstract class Comparer<T> : IComparer<T>, IComparer
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x00014770 File Offset: 0x00012970
		static Comparer()
		{
			if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
			{
				Comparer<T>._default = (Comparer<T>)Activator.CreateInstance(typeof(GenericComparer<>).MakeGenericType(new Type[]
				{
					typeof(T)
				}));
			}
			else
			{
				Comparer<T>._default = new Comparer<T>.DefaultComparer();
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000147DC File Offset: 0x000129DC
		int IComparer.Compare(object x, object y)
		{
			if (x == null)
			{
				return (y != null) ? -1 : 0;
			}
			if (y == null)
			{
				return 1;
			}
			if (x is T && y is T)
			{
				return this.Compare((T)((object)x), (T)((object)y));
			}
			throw new ArgumentException();
		}

		// Token: 0x0600047B RID: 1147
		public abstract int Compare(T x, T y);

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00014834 File Offset: 0x00012A34
		public static Comparer<T> Default
		{
			get
			{
				return Comparer<T>._default;
			}
		}

		// Token: 0x040001EA RID: 490
		private static readonly Comparer<T> _default;

		// Token: 0x02000086 RID: 134
		private sealed class DefaultComparer : Comparer<T>
		{
			// Token: 0x0600047E RID: 1150 RVA: 0x00014844 File Offset: 0x00012A44
			public override int Compare(T x, T y)
			{
				if (x == null)
				{
					return (y != null) ? -1 : 0;
				}
				if (y == null)
				{
					return 1;
				}
				if (x is IComparable<T>)
				{
					return ((IComparable<T>)((object)x)).CompareTo(y);
				}
				if (x is IComparable)
				{
					return ((IComparable)((object)x)).CompareTo(y);
				}
				throw new ArgumentException("does not implement right interface");
			}
		}
	}
}
