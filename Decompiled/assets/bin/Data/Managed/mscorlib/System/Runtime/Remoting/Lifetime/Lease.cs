using System;
using System.Collections;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000292 RID: 658
	internal class Lease : MarshalByRefObject, ILease
	{
		// Token: 0x060014FD RID: 5373 RVA: 0x00049E94 File Offset: 0x00048094
		public Lease()
		{
			this._currentState = LeaseState.Initial;
			this._initialLeaseTime = LifetimeServices.LeaseTime;
			this._renewOnCallTime = LifetimeServices.RenewOnCallTime;
			this._sponsorshipTimeout = LifetimeServices.SponsorshipTimeout;
			this._leaseExpireTime = DateTime.Now + this._initialLeaseTime;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00049EE8 File Offset: 0x000480E8
		public TimeSpan CurrentLeaseTime
		{
			get
			{
				return this._leaseExpireTime - DateTime.Now;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00049EFC File Offset: 0x000480FC
		public LeaseState CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00049F04 File Offset: 0x00048104
		public void Activate()
		{
			this._currentState = LeaseState.Active;
		}

		// Token: 0x170003B3 RID: 947
		// (set) Token: 0x06001501 RID: 5377 RVA: 0x00049F10 File Offset: 0x00048110
		public TimeSpan InitialLeaseTime
		{
			set
			{
				if (this._currentState != LeaseState.Initial)
				{
					throw new RemotingException("InitialLeaseTime property can only be set when the lease is in initial state; state is " + this._currentState + ".");
				}
				this._initialLeaseTime = value;
				this._leaseExpireTime = DateTime.Now + this._initialLeaseTime;
				if (value == TimeSpan.Zero)
				{
					this._currentState = LeaseState.Null;
				}
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00049F80 File Offset: 0x00048180
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x00049F88 File Offset: 0x00048188
		public TimeSpan RenewOnCallTime
		{
			get
			{
				return this._renewOnCallTime;
			}
			set
			{
				if (this._currentState != LeaseState.Initial)
				{
					throw new RemotingException("RenewOnCallTime property can only be set when the lease is in initial state; state is " + this._currentState + ".");
				}
				this._renewOnCallTime = value;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (set) Token: 0x06001504 RID: 5380 RVA: 0x00049FC0 File Offset: 0x000481C0
		public TimeSpan SponsorshipTimeout
		{
			set
			{
				if (this._currentState != LeaseState.Initial)
				{
					throw new RemotingException("SponsorshipTimeout property can only be set when the lease is in initial state; state is " + this._currentState + ".");
				}
				this._sponsorshipTimeout = value;
			}
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00049FF8 File Offset: 0x000481F8
		public TimeSpan Renew(TimeSpan renewalTime)
		{
			DateTime dateTime = DateTime.Now + renewalTime;
			if (dateTime > this._leaseExpireTime)
			{
				this._leaseExpireTime = dateTime;
			}
			return this.CurrentLeaseTime;
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0004A030 File Offset: 0x00048230
		public void Unregister(ISponsor obj)
		{
			lock (this)
			{
				if (this._sponsors != null)
				{
					for (int i = 0; i < this._sponsors.Count; i++)
					{
						if (object.ReferenceEquals(this._sponsors[i], obj))
						{
							this._sponsors.RemoveAt(i);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0004A0B8 File Offset: 0x000482B8
		internal void UpdateState()
		{
			if (this._currentState != LeaseState.Active)
			{
				return;
			}
			if (this.CurrentLeaseTime > TimeSpan.Zero)
			{
				return;
			}
			if (this._sponsors != null)
			{
				this._currentState = LeaseState.Renewing;
				lock (this)
				{
					this._renewingSponsors = new Queue(this._sponsors);
				}
				this.CheckNextSponsor();
			}
			else
			{
				this._currentState = LeaseState.Expired;
			}
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0004A144 File Offset: 0x00048344
		private void CheckNextSponsor()
		{
			if (this._renewingSponsors.Count == 0)
			{
				this._currentState = LeaseState.Expired;
				this._renewingSponsors = null;
				return;
			}
			ISponsor @object = (ISponsor)this._renewingSponsors.Peek();
			this._renewalDelegate = new Lease.RenewalDelegate(@object.Renewal);
			IAsyncResult asyncResult = this._renewalDelegate.BeginInvoke(this, null, null);
			ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, new WaitOrTimerCallback(this.ProcessSponsorResponse), asyncResult, this._sponsorshipTimeout, true);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0004A1C4 File Offset: 0x000483C4
		private void ProcessSponsorResponse(object state, bool timedOut)
		{
			if (!timedOut)
			{
				try
				{
					IAsyncResult result = (IAsyncResult)state;
					TimeSpan timeSpan = this._renewalDelegate.EndInvoke(result);
					if (timeSpan != TimeSpan.Zero)
					{
						this.Renew(timeSpan);
						this._currentState = LeaseState.Active;
						this._renewingSponsors = null;
						return;
					}
				}
				catch
				{
				}
			}
			this.Unregister((ISponsor)this._renewingSponsors.Dequeue());
			this.CheckNextSponsor();
		}

		// Token: 0x04000ACE RID: 2766
		private DateTime _leaseExpireTime;

		// Token: 0x04000ACF RID: 2767
		private LeaseState _currentState;

		// Token: 0x04000AD0 RID: 2768
		private TimeSpan _initialLeaseTime;

		// Token: 0x04000AD1 RID: 2769
		private TimeSpan _renewOnCallTime;

		// Token: 0x04000AD2 RID: 2770
		private TimeSpan _sponsorshipTimeout;

		// Token: 0x04000AD3 RID: 2771
		private ArrayList _sponsors;

		// Token: 0x04000AD4 RID: 2772
		private Queue _renewingSponsors;

		// Token: 0x04000AD5 RID: 2773
		private Lease.RenewalDelegate _renewalDelegate;

		// Token: 0x02000293 RID: 659
		// (Invoke) Token: 0x0600150B RID: 5387
		private delegate TimeSpan RenewalDelegate(ILease lease);
	}
}
