using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000197 RID: 407
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_CustomAttributeBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	public class CustomAttributeBuilder : _CustomAttributeBuilder
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0003B8B8 File Offset: 0x00039AB8
		internal ConstructorInfo Ctor
		{
			get
			{
				return this.ctor;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0003B8C0 File Offset: 0x00039AC0
		internal byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0003B8C8 File Offset: 0x00039AC8
		internal static int decode_len(byte[] data, int pos, out int rpos)
		{
			int result;
			if ((data[pos] & 128) == 0)
			{
				result = (int)(data[pos++] & 127);
			}
			else if ((data[pos] & 64) == 0)
			{
				result = ((int)(data[pos] & 63) << 8) + (int)data[pos + 1];
				pos += 2;
			}
			else
			{
				result = ((int)(data[pos] & 31) << 24) + ((int)data[pos + 1] << 16) + ((int)data[pos + 2] << 8) + (int)data[pos + 3];
				pos += 4;
			}
			rpos = pos;
			return result;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0003B944 File Offset: 0x00039B44
		internal static string string_from_bytes(byte[] data, int pos, int len)
		{
			return Encoding.UTF8.GetString(data, pos, len);
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0003B954 File Offset: 0x00039B54
		internal string string_arg()
		{
			int pos = 2;
			int len = CustomAttributeBuilder.decode_len(this.data, pos, out pos);
			return CustomAttributeBuilder.string_from_bytes(this.data, pos, len);
		}

		// Token: 0x04000668 RID: 1640
		private ConstructorInfo ctor;

		// Token: 0x04000669 RID: 1641
		private byte[] data;
	}
}
