using System;
using System.Collections.Generic;

namespace Boo.Lang.Runtime.DynamicDispatching
{
	// Token: 0x0200000A RID: 10
	public class DispatcherKey
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002868 File Offset: 0x00000A68
		public DispatcherKey(Type type, string name, Type[] arguments)
		{
			this._type = type;
			this._name = name;
			this._arguments = arguments;
		}

		// Token: 0x0400000D RID: 13
		public static readonly IEqualityComparer<DispatcherKey> EqualityComparer = new DispatcherKey._EqualityComparer();

		// Token: 0x0400000E RID: 14
		private readonly Type _type;

		// Token: 0x0400000F RID: 15
		private readonly string _name;

		// Token: 0x04000010 RID: 16
		private readonly Type[] _arguments;

		// Token: 0x0200000B RID: 11
		private sealed class _EqualityComparer : IEqualityComparer<DispatcherKey>
		{
			// Token: 0x0600004F RID: 79 RVA: 0x0000289C File Offset: 0x00000A9C
			public int GetHashCode(DispatcherKey key)
			{
				return key._type.GetHashCode() ^ key._name.GetHashCode() ^ key._arguments.Length;
			}

			// Token: 0x06000050 RID: 80 RVA: 0x000028C0 File Offset: 0x00000AC0
			public bool Equals(DispatcherKey x, DispatcherKey y)
			{
				if (x._type != y._type)
				{
					return false;
				}
				if (x._arguments.Length != y._arguments.Length)
				{
					return false;
				}
				if (x._name != y._name)
				{
					return false;
				}
				for (int i = 0; i < x._arguments.Length; i++)
				{
					if (x._arguments[i] != y._arguments[i])
					{
						return false;
					}
				}
				return true;
			}
		}
	}
}
