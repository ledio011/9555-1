using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;

namespace System
{
	// Token: 0x0200014C RID: 332
	[ComVisible(true)]
	[Serializable]
	public abstract class MarshalByRefObject
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x00031E18 File Offset: 0x00030018
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x00031E20 File Offset: 0x00030020
		internal ServerIdentity ObjectIdentity
		{
			get
			{
				return this._identity;
			}
			set
			{
				this._identity = value;
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00031E2C File Offset: 0x0003002C
		public virtual ObjRef CreateObjRef(Type requestedType)
		{
			if (this._identity == null)
			{
				throw new RemotingException(Locale.GetText("No remoting information was found for the object."));
			}
			return this._identity.CreateObjRef(requestedType);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00031E58 File Offset: 0x00030058
		public object GetLifetimeService()
		{
			if (this._identity == null)
			{
				return null;
			}
			return this._identity.Lease;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00031E74 File Offset: 0x00030074
		public virtual object InitializeLifetimeService()
		{
			if (this._identity != null && this._identity.Lease != null)
			{
				return this._identity.Lease;
			}
			return new Lease();
		}

		// Token: 0x04000556 RID: 1366
		[NonSerialized]
		private ServerIdentity _identity;
	}
}
