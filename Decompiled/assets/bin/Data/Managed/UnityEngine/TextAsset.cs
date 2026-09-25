using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000115 RID: 277
	public class TextAsset : Object
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060009EE RID: 2542
		public extern string text { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060009EF RID: 2543
		public extern byte[] bytes { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x060009F0 RID: 2544 RVA: 0x0001666C File Offset: 0x0001486C
		public override string ToString()
		{
			return this.text;
		}
	}
}
