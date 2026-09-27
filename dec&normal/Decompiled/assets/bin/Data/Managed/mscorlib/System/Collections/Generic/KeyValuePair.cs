using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x0200009B RID: 155
	[DebuggerDisplay("{value}", Name = "[{key}]")]
	[Serializable]
	public struct KeyValuePair<TKey, TValue>
	{
		// Token: 0x0600051E RID: 1310 RVA: 0x000160D4 File Offset: 0x000142D4
		public KeyValuePair(TKey key, TValue value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000160E4 File Offset: 0x000142E4
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x000160EC File Offset: 0x000142EC
		public TKey Key
		{
			get
			{
				return this.key;
			}
			private set
			{
				this.key = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000160F8 File Offset: 0x000142F8
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x00016100 File Offset: 0x00014300
		public TValue Value
		{
			get
			{
				return this.value;
			}
			private set
			{
				this.value = value;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001610C File Offset: 0x0001430C
		public override string ToString()
		{
			string[] array = new string[5];
			array[0] = "[";
			int num = 1;
			string text;
			if (this.Key != null)
			{
				TKey tkey = this.Key;
				text = tkey.ToString();
			}
			else
			{
				text = string.Empty;
			}
			array[num] = text;
			array[2] = ", ";
			int num2 = 3;
			string text2;
			if (this.Value != null)
			{
				TValue tvalue = this.Value;
				text2 = tvalue.ToString();
			}
			else
			{
				text2 = string.Empty;
			}
			array[num2] = text2;
			array[4] = "]";
			return string.Concat(array);
		}

		// Token: 0x04000205 RID: 517
		private TKey key;

		// Token: 0x04000206 RID: 518
		private TValue value;
	}
}
