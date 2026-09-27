using System;

namespace System.Collections.Generic
{
	// Token: 0x0200008F RID: 143
	[Serializable]
	public abstract class EqualityComparer<T> : IEqualityComparer<T>, IEqualityComparer
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x00015F40 File Offset: 0x00014140
		static EqualityComparer()
		{
			if (typeof(IEquatable<T>).IsAssignableFrom(typeof(T)))
			{
				EqualityComparer<T>._default = (EqualityComparer<T>)Activator.CreateInstance(typeof(GenericEqualityComparer<>).MakeGenericType(new Type[]
				{
					typeof(T)
				}));
			}
			else
			{
				EqualityComparer<T>._default = new EqualityComparer<T>.DefaultComparer();
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00015FAC File Offset: 0x000141AC
		int IEqualityComparer.GetHashCode(object obj)
		{
			return this.GetHashCode((T)((object)obj));
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00015FBC File Offset: 0x000141BC
		bool IEqualityComparer.Equals(object x, object y)
		{
			return this.Equals((T)((object)x), (T)((object)y));
		}

		// Token: 0x060004FA RID: 1274
		public abstract int GetHashCode(T obj);

		// Token: 0x060004FB RID: 1275
		public abstract bool Equals(T x, T y);

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00015FD0 File Offset: 0x000141D0
		public static EqualityComparer<T> Default
		{
			get
			{
				return EqualityComparer<T>._default;
			}
		}

		// Token: 0x04000204 RID: 516
		private static readonly EqualityComparer<T> _default;

		// Token: 0x02000090 RID: 144
		[Serializable]
		private sealed class DefaultComparer : EqualityComparer<T>
		{
			// Token: 0x060004FE RID: 1278 RVA: 0x00015FE0 File Offset: 0x000141E0
			public override int GetHashCode(T obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return obj.GetHashCode();
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x00015FFC File Offset: 0x000141FC
			public override bool Equals(T x, T y)
			{
				if (x == null)
				{
					return y == null;
				}
				return x.Equals(y);
			}
		}
	}
}
