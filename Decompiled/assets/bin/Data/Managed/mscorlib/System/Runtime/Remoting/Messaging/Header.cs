using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002A9 RID: 681
	[ComVisible(true)]
	[Serializable]
	public class Header
	{
		// Token: 0x06001576 RID: 5494 RVA: 0x0004BBD8 File Offset: 0x00049DD8
		public Header(string _Name, object _Value) : this(_Name, _Value, true)
		{
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0004BBE4 File Offset: 0x00049DE4
		public Header(string _Name, object _Value, bool _MustUnderstand) : this(_Name, _Value, _MustUnderstand, null)
		{
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0004BBF0 File Offset: 0x00049DF0
		public Header(string _Name, object _Value, bool _MustUnderstand, string _HeaderNamespace)
		{
			this.Name = _Name;
			this.Value = _Value;
			this.MustUnderstand = _MustUnderstand;
			this.HeaderNamespace = _HeaderNamespace;
		}

		// Token: 0x04000B1C RID: 2844
		public string HeaderNamespace;

		// Token: 0x04000B1D RID: 2845
		public bool MustUnderstand;

		// Token: 0x04000B1E RID: 2846
		public string Name;

		// Token: 0x04000B1F RID: 2847
		public object Value;
	}
}
