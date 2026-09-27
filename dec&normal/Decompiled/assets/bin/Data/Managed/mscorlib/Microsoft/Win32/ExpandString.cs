using System;
using System.Text;

namespace Microsoft.Win32
{
	// Token: 0x02000021 RID: 33
	internal class ExpandString
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public ExpandString(string s)
		{
			this.value = s;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		public override string ToString()
		{
			return this.value;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002078 File Offset: 0x00000278
		public string Expand()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.value.Length; i++)
			{
				if (this.value[i] == '%')
				{
					int j;
					for (j = i + 1; j < this.value.Length; j++)
					{
						if (this.value[j] == '%')
						{
							string variable = this.value.Substring(i + 1, j - i - 1);
							stringBuilder.Append(Environment.GetEnvironmentVariable(variable));
							i += j;
							break;
						}
					}
					if (j == this.value.Length)
					{
						stringBuilder.Append('%');
					}
				}
				else
				{
					stringBuilder.Append(this.value[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000046 RID: 70
		private string value;
	}
}
