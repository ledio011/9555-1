using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	public class Capture
	{
		// Token: 0x06000190 RID: 400 RVA: 0x0000733C File Offset: 0x0000553C
		internal Capture(string text) : this(text, 0, 0)
		{
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007348 File Offset: 0x00005548
		internal Capture(string text, int index, int length)
		{
			this.text = text;
			this.index = index;
			this.length = length;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007368 File Offset: 0x00005568
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007370 File Offset: 0x00005570
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007378 File Offset: 0x00005578
		public string Value
		{
			get
			{
				return (this.text != null) ? this.text.Substring(this.index, this.length) : string.Empty;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000073A8 File Offset: 0x000055A8
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000073B0 File Offset: 0x000055B0
		internal string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x040008E7 RID: 2279
		internal int index;

		// Token: 0x040008E8 RID: 2280
		internal int length;

		// Token: 0x040008E9 RID: 2281
		internal string text;
	}
}
