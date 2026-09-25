using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200031E RID: 798
	[ComVisible(true)]
	[MonoTODO("Serialization needs tests")]
	[Serializable]
	public struct RuntimeTypeHandle : ISerializable
	{
		// Token: 0x0600183F RID: 6207 RVA: 0x00058320 File Offset: 0x00056520
		private RuntimeTypeHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MonoType monoType = (MonoType)info.GetValue("TypeObj", typeof(MonoType));
			this.value = monoType.TypeHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException(Locale.GetText("Insufficient state."));
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00058394 File Offset: 0x00056594
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0005839C File Offset: 0x0005659C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Object fields may not be properly initialized");
			}
			info.AddValue("TypeObj", Type.GetTypeHandle(this), typeof(MonoType));
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00058404 File Offset: 0x00056604
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType() && this.value == ((RuntimeTypeHandle)obj).Value;
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00058450 File Offset: 0x00056650
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x04000C9F RID: 3231
		private IntPtr value;
	}
}
