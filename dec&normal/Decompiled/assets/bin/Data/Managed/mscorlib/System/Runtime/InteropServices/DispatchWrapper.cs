using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200023A RID: 570
	[ComVisible(true)]
	[Serializable]
	public sealed class DispatchWrapper
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00045474 File Offset: 0x00043674
		public object WrappedObject
		{
			get
			{
				return this.wrappedObject;
			}
		}

		// Token: 0x040009F0 RID: 2544
		private object wrappedObject;
	}
}
