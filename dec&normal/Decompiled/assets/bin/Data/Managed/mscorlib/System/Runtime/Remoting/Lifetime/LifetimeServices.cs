using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000297 RID: 663
	[ComVisible(true)]
	public sealed class LifetimeServices
	{
		// Token: 0x06001519 RID: 5401 RVA: 0x0004A4EC File Offset: 0x000486EC
		static LifetimeServices()
		{
			LifetimeServices._leaseManagerPollTime = TimeSpan.FromSeconds(10.0);
			LifetimeServices._leaseTime = TimeSpan.FromMinutes(5.0);
			LifetimeServices._renewOnCallTime = TimeSpan.FromMinutes(2.0);
			LifetimeServices._sponsorshipTimeout = TimeSpan.FromMinutes(2.0);
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0004A550 File Offset: 0x00048750
		// (set) Token: 0x0600151B RID: 5403 RVA: 0x0004A558 File Offset: 0x00048758
		public static TimeSpan LeaseManagerPollTime
		{
			get
			{
				return LifetimeServices._leaseManagerPollTime;
			}
			set
			{
				LifetimeServices._leaseManagerPollTime = value;
				LifetimeServices._leaseManager.SetPollTime(value);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0004A56C File Offset: 0x0004876C
		// (set) Token: 0x0600151D RID: 5405 RVA: 0x0004A574 File Offset: 0x00048774
		public static TimeSpan LeaseTime
		{
			get
			{
				return LifetimeServices._leaseTime;
			}
			set
			{
				LifetimeServices._leaseTime = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0004A57C File Offset: 0x0004877C
		// (set) Token: 0x0600151F RID: 5407 RVA: 0x0004A584 File Offset: 0x00048784
		public static TimeSpan RenewOnCallTime
		{
			get
			{
				return LifetimeServices._renewOnCallTime;
			}
			set
			{
				LifetimeServices._renewOnCallTime = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0004A58C File Offset: 0x0004878C
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x0004A594 File Offset: 0x00048794
		public static TimeSpan SponsorshipTimeout
		{
			get
			{
				return LifetimeServices._sponsorshipTimeout;
			}
			set
			{
				LifetimeServices._sponsorshipTimeout = value;
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0004A59C File Offset: 0x0004879C
		internal static void TrackLifetime(ServerIdentity identity)
		{
			LifetimeServices._leaseManager.TrackLifetime(identity);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0004A5AC File Offset: 0x000487AC
		internal static void StopTrackingLifetime(ServerIdentity identity)
		{
			LifetimeServices._leaseManager.StopTrackingLifetime(identity);
		}

		// Token: 0x04000ADF RID: 2783
		private static TimeSpan _leaseManagerPollTime;

		// Token: 0x04000AE0 RID: 2784
		private static TimeSpan _leaseTime;

		// Token: 0x04000AE1 RID: 2785
		private static TimeSpan _renewOnCallTime;

		// Token: 0x04000AE2 RID: 2786
		private static TimeSpan _sponsorshipTimeout;

		// Token: 0x04000AE3 RID: 2787
		private static LeaseManager _leaseManager = new LeaseManager();
	}
}
