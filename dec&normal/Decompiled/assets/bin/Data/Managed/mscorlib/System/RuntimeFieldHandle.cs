using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200031C RID: 796
	[MonoTODO("Serialization needs tests")]
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeFieldHandle : ISerializable
	{
		// Token: 0x06001834 RID: 6196 RVA: 0x0005809C File Offset: 0x0005629C
		private RuntimeFieldHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MonoField monoField = (MonoField)info.GetValue("FieldObj", typeof(MonoField));
			this.value = monoField.FieldHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException(Locale.GetText("Insufficient state."));
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x00058110 File Offset: 0x00056310
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00058118 File Offset: 0x00056318
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
			info.AddValue("FieldObj", (MonoField)FieldInfo.GetFieldFromHandle(this), typeof(MonoField));
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0005817C File Offset: 0x0005637C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType() && this.value == ((RuntimeFieldHandle)obj).Value;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x000581C8 File Offset: 0x000563C8
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x04000C9D RID: 3229
		private IntPtr value;
	}
}
