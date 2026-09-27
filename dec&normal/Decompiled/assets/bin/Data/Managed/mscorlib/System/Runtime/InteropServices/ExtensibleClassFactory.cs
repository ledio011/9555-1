using System;
using System.Collections;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200023D RID: 573
	[ComVisible(true)]
	public sealed class ExtensibleClassFactory
	{
		// Token: 0x06001339 RID: 4921 RVA: 0x00045498 File Offset: 0x00043698
		internal static ObjectCreationDelegate GetObjectCreationCallback(Type t)
		{
			return ExtensibleClassFactory.hashtable[t] as ObjectCreationDelegate;
		}

		// Token: 0x040009FB RID: 2555
		private static Hashtable hashtable = new Hashtable();
	}
}
