using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000080 RID: 128
	internal class QuickSearch
	{
		// Token: 0x06000273 RID: 627 RVA: 0x0000B398 File Offset: 0x00009598
		public QuickSearch(string str, bool ignore, bool reverse)
		{
			this.str = str;
			this.len = str.Length;
			this.ignore = ignore;
			this.reverse = reverse;
			if (ignore)
			{
				str = str.ToLower();
			}
			if (this.len > QuickSearch.THRESHOLD)
			{
				this.SetupShiftTable();
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000B3F8 File Offset: 0x000095F8
		public int Length
		{
			get
			{
				return this.len;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000B400 File Offset: 0x00009600
		public int Search(string text, int start, int end)
		{
			int i = start;
			if (this.reverse)
			{
				if (start < end)
				{
					return -1;
				}
				if (i > text.Length)
				{
					i = text.Length;
				}
				if (this.len == 1)
				{
					while (--i >= end)
					{
						if (this.str[0] == this.GetChar(text[i]))
						{
							return i;
						}
					}
					return -1;
				}
				if (end < this.len)
				{
					end = this.len - 1;
				}
				for (i--; i >= end; i -= this.GetShiftDistance(text[i - this.len]))
				{
					int num = this.len - 1;
					while (this.str[num] == this.GetChar(text[i - this.len + 1 + num]))
					{
						if (--num < 0)
						{
							return i - this.len + 1;
						}
					}
					if (i <= end)
					{
						break;
					}
				}
			}
			else
			{
				if (this.len == 1)
				{
					while (i <= end)
					{
						if (this.str[0] == this.GetChar(text[i]))
						{
							return i;
						}
						i++;
					}
					return -1;
				}
				if (end > text.Length - this.len)
				{
					end = text.Length - this.len;
				}
				while (i <= end)
				{
					int num2 = this.len - 1;
					while (this.str[num2] == this.GetChar(text[i + num2]))
					{
						if (--num2 < 0)
						{
							return i;
						}
					}
					if (i >= end)
					{
						break;
					}
					i += this.GetShiftDistance(text[i + this.len]);
				}
			}
			return -1;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000B5E4 File Offset: 0x000097E4
		private void SetupShiftTable()
		{
			bool flag = this.len > 254;
			byte b = 0;
			for (int i = 0; i < this.len; i++)
			{
				char c = this.str[i];
				if (c <= 'ÿ')
				{
					if ((byte)c > b)
					{
						b = (byte)c;
					}
				}
				else
				{
					flag = true;
				}
			}
			this.shift = new byte[(int)(b + 1)];
			if (flag)
			{
				this.shiftExtended = new Hashtable();
			}
			int j = 0;
			int num = this.len;
			while (j < this.len)
			{
				char c2 = this.str[this.reverse ? (num - 1) : j];
				if ((int)c2 >= this.shift.Length)
				{
					goto IL_DD;
				}
				if (num >= 255)
				{
					this.shift[(int)c2] = byte.MaxValue;
					goto IL_DD;
				}
				this.shift[(int)c2] = (byte)num;
				IL_F6:
				j++;
				num--;
				continue;
				IL_DD:
				this.shiftExtended[c2] = num;
				goto IL_F6;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000B700 File Offset: 0x00009900
		private int GetShiftDistance(char c)
		{
			if (this.shift == null)
			{
				return 1;
			}
			c = this.GetChar(c);
			if ((int)c < this.shift.Length)
			{
				int num = (int)this.shift[(int)c];
				if (num == 0)
				{
					return this.len + 1;
				}
				if (num != 255)
				{
					return num;
				}
			}
			else if (c < 'ÿ')
			{
				return this.len + 1;
			}
			if (this.shiftExtended == null)
			{
				return this.len + 1;
			}
			object obj = this.shiftExtended[c];
			return (obj == null) ? (this.len + 1) : ((int)obj);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000B7B0 File Offset: 0x000099B0
		private char GetChar(char c)
		{
			return this.ignore ? char.ToLower(c) : c;
		}

		// Token: 0x040009EB RID: 2539
		private string str;

		// Token: 0x040009EC RID: 2540
		private int len;

		// Token: 0x040009ED RID: 2541
		private bool ignore;

		// Token: 0x040009EE RID: 2542
		private bool reverse;

		// Token: 0x040009EF RID: 2543
		private byte[] shift;

		// Token: 0x040009F0 RID: 2544
		private Hashtable shiftExtended;

		// Token: 0x040009F1 RID: 2545
		private static readonly int THRESHOLD = 5;
	}
}
