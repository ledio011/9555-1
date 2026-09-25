using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200031D RID: 797
	[MonoTODO("Serialization needs tests")]
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeMethodHandle : ISerializable
	{
		// Token: 0x06001839 RID: 6201 RVA: 0x000581D8 File Offset: 0x000563D8
		internal RuntimeMethodHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000581E4 File Offset: 0x000563E4
		private RuntimeMethodHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MonoMethod monoMethod = (MonoMethod)info.GetValue("MethodObj", typeof(MonoMethod));
			this.value = monoMethod.MethodHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException(Locale.GetText("Insufficient state."));
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x00058258 File Offset: 0x00056458
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00058260 File Offset: 0x00056460
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
			info.AddValue("MethodObj", (MonoMethod)MethodBase.GetMethodFromHandle(this), typeof(MonoMethod));
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x000582C4 File Offset: 0x000564C4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType() && this.value == ((RuntimeMethodHandle)obj).Value;
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00058310 File Offset: 0x00056510
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x04000C9E RID: 3230
		private IntPtr value;
	}
}
