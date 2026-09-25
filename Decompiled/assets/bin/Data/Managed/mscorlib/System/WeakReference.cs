using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020003DF RID: 991
	[ComVisible(true)]
	[Serializable]
	public class WeakReference : ISerializable
	{
		// Token: 0x06001EB2 RID: 7858 RVA: 0x00072B60 File Offset: 0x00070D60
		protected WeakReference()
		{
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00072B68 File Offset: 0x00070D68
		public WeakReference(object target) : this(target, false)
		{
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00072B74 File Offset: 0x00070D74
		public WeakReference(object target, bool trackResurrection)
		{
			this.isLongReference = trackResurrection;
			this.AllocateHandle(target);
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00072B8C File Offset: 0x00070D8C
		protected WeakReference(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.isLongReference = info.GetBoolean("TrackResurrection");
			object value = info.GetValue("TrackedObject", typeof(object));
			this.AllocateHandle(value);
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00072BE0 File Offset: 0x00070DE0
		private void AllocateHandle(object target)
		{
			if (this.isLongReference)
			{
				this.gcHandle = GCHandle.Alloc(target, GCHandleType.WeakTrackResurrection);
			}
			else
			{
				this.gcHandle = GCHandle.Alloc(target, GCHandleType.Weak);
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x00072C0C File Offset: 0x00070E0C
		public virtual object Target
		{
			get
			{
				return this.gcHandle.Target;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x00072C1C File Offset: 0x00070E1C
		public virtual bool TrackResurrection
		{
			get
			{
				return this.isLongReference;
			}
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00072C24 File Offset: 0x00070E24
		~WeakReference()
		{
			this.gcHandle.Free();
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00072C58 File Offset: 0x00070E58
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("TrackResurrection", this.TrackResurrection);
			try
			{
				info.AddValue("TrackedObject", this.Target);
			}
			catch (Exception)
			{
				info.AddValue("TrackedObject", null);
			}
		}

		// Token: 0x04000FD4 RID: 4052
		private bool isLongReference;

		// Token: 0x04000FD5 RID: 4053
		private GCHandle gcHandle;
	}
}
