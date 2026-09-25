using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200011D RID: 285
	[ComVisible(true)]
	[Serializable]
	public class DirectoryNotFoundException : IOException
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x0002BCCC File Offset: 0x00029ECC
		public DirectoryNotFoundException() : base("Directory not found")
		{
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002BCDC File Offset: 0x00029EDC
		public DirectoryNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002BCE8 File Offset: 0x00029EE8
		protected DirectoryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
