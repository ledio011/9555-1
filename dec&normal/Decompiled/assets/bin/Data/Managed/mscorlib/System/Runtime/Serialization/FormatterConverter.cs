using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020002E9 RID: 745
	[ComVisible(true)]
	public class FormatterConverter : IFormatterConverter
	{
		// Token: 0x0600173B RID: 5947 RVA: 0x00051AE0 File Offset: 0x0004FCE0
		public object Convert(object value, Type type)
		{
			return System.Convert.ChangeType(value, type);
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00051AEC File Offset: 0x0004FCEC
		public bool ToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToBoolean(value);
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00051B08 File Offset: 0x0004FD08
		public short ToInt16(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToInt16(value);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00051B24 File Offset: 0x0004FD24
		public int ToInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToInt32(value);
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00051B40 File Offset: 0x0004FD40
		public long ToInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToInt64(value);
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00051B5C File Offset: 0x0004FD5C
		public string ToString(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToString(value);
		}
	}
}
