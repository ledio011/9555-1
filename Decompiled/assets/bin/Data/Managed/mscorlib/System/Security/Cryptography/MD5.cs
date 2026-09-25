using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000328 RID: 808
	[ComVisible(true)]
	public abstract class MD5 : HashAlgorithm
	{
		// Token: 0x06001884 RID: 6276 RVA: 0x000593D4 File Offset: 0x000575D4
		protected MD5()
		{
			this.HashSizeValue = 128;
		}
	}
}
