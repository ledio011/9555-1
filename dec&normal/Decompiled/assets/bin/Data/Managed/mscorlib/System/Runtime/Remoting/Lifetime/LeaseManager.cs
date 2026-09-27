using System;
using System.Collections;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000294 RID: 660
	internal class LeaseManager
	{
		// Token: 0x0600150F RID: 5391 RVA: 0x0004A264 File Offset: 0x00048464
		public void SetPollTime(TimeSpan timeSpan)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				if (this._timer != null)
				{
					this._timer.Change(timeSpan, timeSpan);
				}
			}
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0004A2B8 File Offset: 0x000484B8
		public void TrackLifetime(ServerIdentity identity)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				identity.Lease.Activate();
				this._objects.Add(identity);
				if (this._timer == null)
				{
					this.StartManager();
				}
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0004A31C File Offset: 0x0004851C
		public void StopTrackingLifetime(ServerIdentity identity)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				this._objects.Remove(identity);
			}
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0004A364 File Offset: 0x00048564
		public void StartManager()
		{
			this._timer = new Timer(new TimerCallback(this.ManageLeases), null, LifetimeServices.LeaseManagerPollTime, LifetimeServices.LeaseManagerPollTime);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0004A388 File Offset: 0x00048588
		public void StopManager()
		{
			Timer timer = this._timer;
			this._timer = null;
			timer.Dispose();
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0004A3AC File Offset: 0x000485AC
		public void ManageLeases(object state)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				int i = 0;
				while (i < this._objects.Count)
				{
					ServerIdentity serverIdentity = (ServerIdentity)this._objects[i];
					serverIdentity.Lease.UpdateState();
					if (serverIdentity.Lease.CurrentState == LeaseState.Expired)
					{
						this._objects.RemoveAt(i);
						serverIdentity.OnLifetimeExpired();
					}
					else
					{
						i++;
					}
				}
				if (this._objects.Count == 0)
				{
					this.StopManager();
				}
			}
		}

		// Token: 0x04000AD6 RID: 2774
		private ArrayList _objects = new ArrayList();

		// Token: 0x04000AD7 RID: 2775
		private Timer _timer;
	}
}
