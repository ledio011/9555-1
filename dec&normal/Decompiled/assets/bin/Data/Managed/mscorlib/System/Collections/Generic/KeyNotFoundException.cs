using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x0200009A RID: 154
	[ComVisible(true)]
	[Serializable]
	public class KeyNotFoundException : SystemException, ISerializable
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x000160B8 File Offset: 0x000142B8
		public KeyNotFoundException() : base("The given key was not present in the dictionary.")
		{
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000160C8 File Offset: 0x000142C8
		protected KeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
