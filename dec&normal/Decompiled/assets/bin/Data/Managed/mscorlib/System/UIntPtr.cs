using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020003D5 RID: 981
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public struct UIntPtr : ISerializable
	{
		// Token: 0x06001E70 RID: 7792 RVA: 0x00071CA4 File Offset: 0x0006FEA4
		public UIntPtr(ulong value)
		{
			if (value > (ulong)-1 && UIntPtr.Size < 8)
			{
				throw new OverflowException(Locale.GetText("This isn't a 64bits machine."));
			}
			this._pointer = value;
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x00071CD4 File Offset: 0x0006FED4
		public UIntPtr(uint value)
		{
			this._pointer = value;
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x00071CE0 File Offset: 0x0006FEE0
		[CLSCompliant(false)]
		public unsafe UIntPtr(void* value)
		{
			this._pointer = value;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x00071CFC File Offset: 0x0006FEFC
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("pointer", this._pointer);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x00071D24 File Offset: 0x0006FF24
		public override bool Equals(object obj)
		{
			if (obj is UIntPtr)
			{
				UIntPtr uintPtr = (UIntPtr)obj;
				return this._pointer == uintPtr._pointer;
			}
			return false;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x00071D54 File Offset: 0x0006FF54
		public override int GetHashCode()
		{
			return this._pointer;
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x00071D60 File Offset: 0x0006FF60
		public uint ToUInt32()
		{
			return this._pointer;
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x00071D6C File Offset: 0x0006FF6C
		public ulong ToUInt64()
		{
			return this._pointer;
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x00071D78 File Offset: 0x0006FF78
		[CLSCompliant(false)]
		public unsafe void* ToPointer()
		{
			return this._pointer;
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x00071D80 File Offset: 0x0006FF80
		public override string ToString()
		{
			return this._pointer.ToString();
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x00071D9C File Offset: 0x0006FF9C
		public unsafe static int Size
		{
			get
			{
				return sizeof(void*);
			}
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x00071DA4 File Offset: 0x0006FFA4
		public static bool operator ==(UIntPtr value1, UIntPtr value2)
		{
			return value1._pointer == value2._pointer;
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x00071DB8 File Offset: 0x0006FFB8
		public static bool operator !=(UIntPtr value1, UIntPtr value2)
		{
			return value1._pointer != value2._pointer;
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x00071DD0 File Offset: 0x0006FFD0
		public static explicit operator ulong(UIntPtr value)
		{
			return value._pointer;
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x00071DDC File Offset: 0x0006FFDC
		public static explicit operator uint(UIntPtr value)
		{
			return value._pointer;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00071DE8 File Offset: 0x0006FFE8
		public static explicit operator UIntPtr(ulong value)
		{
			return new UIntPtr(value);
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00071DF0 File Offset: 0x0006FFF0
		[CLSCompliant(false)]
		public unsafe static explicit operator UIntPtr(void* value)
		{
			return new UIntPtr(value);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00071DF8 File Offset: 0x0006FFF8
		[CLSCompliant(false)]
		public unsafe static explicit operator void*(UIntPtr value)
		{
			return value.ToPointer();
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x00071E04 File Offset: 0x00070004
		public static explicit operator UIntPtr(uint value)
		{
			return new UIntPtr(value);
		}

		// Token: 0x04000FAE RID: 4014
		public static readonly UIntPtr Zero = new UIntPtr(0U);

		// Token: 0x04000FAF RID: 4015
		private unsafe void* _pointer;
	}
}
