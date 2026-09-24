using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000290 RID: 656
	[ComVisible(true)]
	public interface ILease
	{
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060014F4 RID: 5364
		TimeSpan CurrentLeaseTime { get; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060014F5 RID: 5365
		LeaseState CurrentState { get; }

		// Token: 0x170003AE RID: 942
		// (set) Token: 0x060014F6 RID: 5366
		TimeSpan InitialLeaseTime { set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060014F7 RID: 5367
		// (set) Token: 0x060014F8 RID: 5368
		TimeSpan RenewOnCallTime { get; set; }

		// Token: 0x170003B0 RID: 944
		// (set) Token: 0x060014F9 RID: 5369
		TimeSpan SponsorshipTimeout { set; }

		// Token: 0x060014FA RID: 5370
		TimeSpan Renew(TimeSpan renewalTime);

		// Token: 0x060014FB RID: 5371
		void Unregister(ISponsor obj);
	}
}
