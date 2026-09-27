using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020001DF RID: 479
	[Serializable]
	internal class MonoGenericMethod : MonoMethod
	{
		// Token: 0x060011E8 RID: 4584 RVA: 0x00043F8C File Offset: 0x0004218C
		internal MonoGenericMethod()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060011E9 RID: 4585
		public override extern Type ReflectedType { [MethodImpl(4096)] get; }
	}
}
