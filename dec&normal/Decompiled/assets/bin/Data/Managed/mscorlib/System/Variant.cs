using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020003DC RID: 988
	[StructLayout(2)]
	internal struct Variant
	{
		// Token: 0x06001E9C RID: 7836 RVA: 0x00072144 File Offset: 0x00070344
		public void SetValue(object obj)
		{
			this.vt = 0;
			if (obj == null)
			{
				return;
			}
			Type type = obj.GetType();
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			if (type == typeof(sbyte))
			{
				this.vt = 16;
				this.cVal = (sbyte)obj;
			}
			else if (type == typeof(byte))
			{
				this.vt = 17;
				this.bVal = (byte)obj;
			}
			else if (type == typeof(short))
			{
				this.vt = 2;
				this.iVal = (short)obj;
			}
			else if (type == typeof(ushort))
			{
				this.vt = 18;
				this.uiVal = (ushort)obj;
			}
			else if (type == typeof(int))
			{
				this.vt = 3;
				this.lVal = (int)obj;
			}
			else if (type == typeof(uint))
			{
				this.vt = 19;
				this.ulVal = (uint)obj;
			}
			else if (type == typeof(long))
			{
				this.vt = 20;
				this.llVal = (long)obj;
			}
			else if (type == typeof(ulong))
			{
				this.vt = 21;
				this.ullVal = (ulong)obj;
			}
			else if (type == typeof(float))
			{
				this.vt = 4;
				this.fltVal = (float)obj;
			}
			else if (type == typeof(double))
			{
				this.vt = 5;
				this.dblVal = (double)obj;
			}
			else if (type == typeof(string))
			{
				this.vt = 8;
				this.bstrVal = Marshal.StringToBSTR((string)obj);
			}
			else if (type == typeof(bool))
			{
				this.vt = 11;
				this.lVal = ((!(bool)obj) ? 0 : -1);
			}
			else if (type == typeof(BStrWrapper))
			{
				this.vt = 8;
				this.bstrVal = Marshal.StringToBSTR(((BStrWrapper)obj).WrappedObject);
			}
			else if (type == typeof(UnknownWrapper))
			{
				this.vt = 13;
				this.pdispVal = Marshal.GetIUnknownForObject(((UnknownWrapper)obj).WrappedObject);
			}
			else if (type == typeof(DispatchWrapper))
			{
				this.vt = 9;
				this.pdispVal = Marshal.GetIDispatchForObject(((DispatchWrapper)obj).WrappedObject);
			}
			else
			{
				try
				{
					this.pdispVal = Marshal.GetIDispatchForObject(obj);
					this.vt = 9;
					return;
				}
				catch
				{
				}
				try
				{
					this.vt = 13;
					this.pdispVal = Marshal.GetIUnknownForObject(obj);
				}
				catch (Exception inner)
				{
					throw new NotImplementedException(string.Format("Variant couldn't handle object of type {0}", obj.GetType()), inner);
				}
			}
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00072480 File Offset: 0x00070680
		public object GetValue()
		{
			object result = null;
			switch (this.vt)
			{
			case 2:
				result = this.iVal;
				break;
			case 3:
				result = this.lVal;
				break;
			case 4:
				result = this.fltVal;
				break;
			case 5:
				result = this.dblVal;
				break;
			case 8:
				result = Marshal.PtrToStringBSTR(this.bstrVal);
				break;
			case 9:
			case 13:
				if (this.pdispVal != IntPtr.Zero)
				{
					result = Marshal.GetObjectForIUnknown(this.pdispVal);
				}
				break;
			case 11:
				result = (this.boolVal != 0);
				break;
			case 16:
				result = this.cVal;
				break;
			case 17:
				result = this.bVal;
				break;
			case 18:
				result = this.uiVal;
				break;
			case 19:
				result = this.ulVal;
				break;
			case 20:
				result = this.llVal;
				break;
			case 21:
				result = this.ullVal;
				break;
			}
			return result;
		}

		// Token: 0x04000FBB RID: 4027
		[FieldOffset(0)]
		public short vt;

		// Token: 0x04000FBC RID: 4028
		[FieldOffset(2)]
		public ushort wReserved1;

		// Token: 0x04000FBD RID: 4029
		[FieldOffset(4)]
		public ushort wReserved2;

		// Token: 0x04000FBE RID: 4030
		[FieldOffset(6)]
		public ushort wReserved3;

		// Token: 0x04000FBF RID: 4031
		[FieldOffset(8)]
		public long llVal;

		// Token: 0x04000FC0 RID: 4032
		[FieldOffset(8)]
		public int lVal;

		// Token: 0x04000FC1 RID: 4033
		[FieldOffset(8)]
		public byte bVal;

		// Token: 0x04000FC2 RID: 4034
		[FieldOffset(8)]
		public short iVal;

		// Token: 0x04000FC3 RID: 4035
		[FieldOffset(8)]
		public float fltVal;

		// Token: 0x04000FC4 RID: 4036
		[FieldOffset(8)]
		public double dblVal;

		// Token: 0x04000FC5 RID: 4037
		[FieldOffset(8)]
		public short boolVal;

		// Token: 0x04000FC6 RID: 4038
		[FieldOffset(8)]
		public IntPtr bstrVal;

		// Token: 0x04000FC7 RID: 4039
		[FieldOffset(8)]
		public sbyte cVal;

		// Token: 0x04000FC8 RID: 4040
		[FieldOffset(8)]
		public ushort uiVal;

		// Token: 0x04000FC9 RID: 4041
		[FieldOffset(8)]
		public uint ulVal;

		// Token: 0x04000FCA RID: 4042
		[FieldOffset(8)]
		public ulong ullVal;

		// Token: 0x04000FCB RID: 4043
		[FieldOffset(8)]
		public int intVal;

		// Token: 0x04000FCC RID: 4044
		[FieldOffset(8)]
		public uint uintVal;

		// Token: 0x04000FCD RID: 4045
		[FieldOffset(8)]
		public IntPtr pdispVal;

		// Token: 0x04000FCE RID: 4046
		[FieldOffset(8)]
		public BRECORD bRecord;
	}
}
