using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000251 RID: 593
	[ComVisible(true)]
	[Serializable]
	public sealed class UnknownWrapper
	{
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00046520 File Offset: 0x00044720
		public object WrappedObject
		{
			get
			{
				return this.InternalObject;
			}
		}

		// Token: 0x04000A19 RID: 2585
		private object InternalObject;
	}
}
