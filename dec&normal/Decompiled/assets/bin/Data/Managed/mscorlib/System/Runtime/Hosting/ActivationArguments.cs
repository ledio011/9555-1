using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Hosting
{
	// Token: 0x0200020E RID: 526
	[ComVisible(true)]
	[Serializable]
	public sealed class ActivationArguments
	{
		// Token: 0x06001298 RID: 4760 RVA: 0x00045354 File Offset: 0x00043554
		public ActivationArguments(ActivationContext activationData)
		{
			if (activationData == null)
			{
				throw new ArgumentNullException("activationData");
			}
			this._context = activationData;
			this._identity = activationData.Identity;
		}

		// Token: 0x040009D2 RID: 2514
		private ActivationContext _context;

		// Token: 0x040009D3 RID: 2515
		private ApplicationIdentity _identity;

		// Token: 0x040009D4 RID: 2516
		private string[] _data;
	}
}
