using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020001DE RID: 478
	[Serializable]
	internal class MonoGenericCMethod : MonoCMethod
	{
		// Token: 0x060011E6 RID: 4582 RVA: 0x00043F7C File Offset: 0x0004217C
		internal MonoGenericCMethod()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060011E7 RID: 4583
		public override extern Type ReflectedType { [MethodImpl(4096)] get; }
	}
}
