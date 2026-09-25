using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000116 RID: 278
	[ComVisible(true)]
	[Serializable]
	public struct IntPtr : ISerializable
	{
		// Token: 0x06000B02 RID: 2818 RVA: 0x0002A454 File Offset: 0x00028654
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public IntPtr(int value)
		{
			this.m_value = value;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002A460 File Offset: 0x00028660
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public IntPtr(long value)
		{
			this.m_value = value;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002A46C File Offset: 0x0002866C
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe IntPtr(void* value)
		{
			this.m_value = value;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002A478 File Offset: 0x00028678
		private IntPtr(SerializationInfo info, StreamingContext context)
		{
			long @int = info.GetInt64("value");
			this.m_value = @int;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002A49C File Offset: 0x0002869C
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("value", this.ToInt64());
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0002A4C0 File Offset: 0x000286C0
		public unsafe static int Size
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return sizeof(void*);
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002A4C8 File Offset: 0x000286C8
		public override bool Equals(object obj)
		{
			return obj is IntPtr && ((IntPtr)obj).m_value == this.m_value;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002A4F8 File Offset: 0x000286F8
		public override int GetHashCode()
		{
			return this.m_value;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002A504 File Offset: 0x00028704
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public int ToInt32()
		{
			return this.m_value;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002A510 File Offset: 0x00028710
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public long ToInt64()
		{
			if (IntPtr.Size == 4)
			{
				return (long)this.m_value;
			}
			return this.m_value;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002A530 File Offset: 0x00028730
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002A53C File Offset: 0x0002873C
		public string ToString(string format)
		{
			if (IntPtr.Size == 4)
			{
				return this.m_value.ToString(format);
			}
			return this.m_value.ToString(format);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002A578 File Offset: 0x00028778
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool operator ==(IntPtr value1, IntPtr value2)
		{
			return value1.m_value == value2.m_value;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002A58C File Offset: 0x0002878C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool operator !=(IntPtr value1, IntPtr value2)
		{
			return value1.m_value != value2.m_value;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002A5A4 File Offset: 0x000287A4
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static explicit operator IntPtr(int value)
		{
			return new IntPtr(value);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002A5AC File Offset: 0x000287AC
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static explicit operator IntPtr(long value)
		{
			return new IntPtr(value);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002A5B4 File Offset: 0x000287B4
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe static explicit operator IntPtr(void* value)
		{
			return new IntPtr(value);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0002A5BC File Offset: 0x000287BC
		public static explicit operator int(IntPtr value)
		{
			return value.m_value;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002A5C8 File Offset: 0x000287C8
		public static explicit operator long(IntPtr value)
		{
			return value.ToInt64();
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002A5D4 File Offset: 0x000287D4
		[CLSCompliant(false)]
		public unsafe static explicit operator void*(IntPtr value)
		{
			return value.m_value;
		}

		// Token: 0x04000464 RID: 1124
		private unsafe void* m_value;

		// Token: 0x04000465 RID: 1125
		public static readonly IntPtr Zero;
	}
}
