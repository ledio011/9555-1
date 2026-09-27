using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000283 RID: 643
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	[Serializable]
	public class SynchronizationAttribute : ContextAttribute, IContributeClientContextSink, IContributeServerContextSink
	{
		// Token: 0x060014C4 RID: 5316 RVA: 0x00049740 File Offset: 0x00047940
		public SynchronizationAttribute() : this(8, false)
		{
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0004974C File Offset: 0x0004794C
		public SynchronizationAttribute(int flag, bool reEntrant) : base("Synchronization")
		{
			if (flag != 1 && flag != 4 && flag != 8 && flag != 2)
			{
				throw new ArgumentException("flag");
			}
			this._bReEntrant = reEntrant;
			this._flavor = flag;
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x000497A8 File Offset: 0x000479A8
		public virtual bool IsReEntrant
		{
			get
			{
				return this._bReEntrant;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x000497B0 File Offset: 0x000479B0
		public virtual bool Locked
		{
			set
			{
				if (value)
				{
					this._mutex.WaitOne();
					lock (this)
					{
						this._lockCount++;
						if (this._lockCount > 1)
						{
							this.ReleaseLock();
						}
						this._ownerThread = Thread.CurrentThread;
					}
				}
				else
				{
					lock (this)
					{
						while (this._lockCount > 0 && this._ownerThread == Thread.CurrentThread)
						{
							this._lockCount--;
							this._mutex.ReleaseMutex();
							this._ownerThread = null;
						}
					}
				}
			}
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00049884 File Offset: 0x00047A84
		internal void AcquireLock()
		{
			this._mutex.WaitOne();
			lock (this)
			{
				this._ownerThread = Thread.CurrentThread;
				this._lockCount++;
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x000498DC File Offset: 0x00047ADC
		internal void ReleaseLock()
		{
			lock (this)
			{
				if (this._lockCount > 0 && this._ownerThread == Thread.CurrentThread)
				{
					this._lockCount--;
					this._mutex.ReleaseMutex();
					this._ownerThread = null;
				}
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0004994C File Offset: 0x00047B4C
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (this._flavor != 1)
			{
				ctorMsg.ContextProperties.Add(this);
			}
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00049968 File Offset: 0x00047B68
		public virtual IMessageSink GetClientContextSink(IMessageSink nextSink)
		{
			return new SynchronizedClientContextSink(nextSink, this);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00049974 File Offset: 0x00047B74
		public virtual IMessageSink GetServerContextSink(IMessageSink nextSink)
		{
			return new SynchronizedServerContextSink(nextSink, this);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00049980 File Offset: 0x00047B80
		[ComVisible(true)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			SynchronizationAttribute synchronizationAttribute = ctx.GetProperty("Synchronization") as SynchronizationAttribute;
			switch (this._flavor)
			{
			case 1:
				return synchronizationAttribute == null;
			case 2:
				return true;
			case 4:
				return synchronizationAttribute != null;
			case 8:
				return false;
			}
			return false;
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x000499E4 File Offset: 0x00047BE4
		internal static void ExitContext()
		{
			if (Thread.CurrentContext.IsDefaultContext)
			{
				return;
			}
			SynchronizationAttribute synchronizationAttribute = Thread.CurrentContext.GetProperty("Synchronization") as SynchronizationAttribute;
			if (synchronizationAttribute == null)
			{
				return;
			}
			synchronizationAttribute.Locked = false;
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00049A24 File Offset: 0x00047C24
		internal static void EnterContext()
		{
			if (Thread.CurrentContext.IsDefaultContext)
			{
				return;
			}
			SynchronizationAttribute synchronizationAttribute = Thread.CurrentContext.GetProperty("Synchronization") as SynchronizationAttribute;
			if (synchronizationAttribute == null)
			{
				return;
			}
			synchronizationAttribute.Locked = true;
		}

		// Token: 0x04000AB2 RID: 2738
		public const int NOT_SUPPORTED = 1;

		// Token: 0x04000AB3 RID: 2739
		public const int SUPPORTED = 2;

		// Token: 0x04000AB4 RID: 2740
		public const int REQUIRED = 4;

		// Token: 0x04000AB5 RID: 2741
		public const int REQUIRES_NEW = 8;

		// Token: 0x04000AB6 RID: 2742
		private bool _bReEntrant;

		// Token: 0x04000AB7 RID: 2743
		private int _flavor;

		// Token: 0x04000AB8 RID: 2744
		[NonSerialized]
		private bool _locked;

		// Token: 0x04000AB9 RID: 2745
		[NonSerialized]
		private int _lockCount;

		// Token: 0x04000ABA RID: 2746
		[NonSerialized]
		private Mutex _mutex = new Mutex(false);

		// Token: 0x04000ABB RID: 2747
		[NonSerialized]
		private Thread _ownerThread;
	}
}
