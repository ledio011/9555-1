using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200022D RID: 557
	[ComVisible(true)]
	[Serializable]
	public sealed class BStrWrapper
	{
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x00045380 File Offset: 0x00043580
		public string WrappedObject
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040009D5 RID: 2517
		private string _value;
	}
}
