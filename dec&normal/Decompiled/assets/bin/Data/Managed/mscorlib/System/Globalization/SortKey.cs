using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x02000101 RID: 257
	[ComVisible(true)]
	[Serializable]
	public class SortKey
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x00027040 File Offset: 0x00025240
		internal SortKey(int lcid, string source, CompareOptions opt)
		{
			this.lcid = lcid;
			this.source = source;
			this.options = opt;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00027060 File Offset: 0x00025260
		internal SortKey(int lcid, string source, byte[] buffer, CompareOptions opt, int lv1Length, int lv2Length, int lv3Length, int kanaSmallLength, int markTypeLength, int katakanaLength, int kanaWidthLength, int identLength)
		{
			this.lcid = lcid;
			this.source = source;
			this.key = buffer;
			this.options = opt;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00027088 File Offset: 0x00025288
		public static int Compare(SortKey sortkey1, SortKey sortkey2)
		{
			if (sortkey1 == null)
			{
				throw new ArgumentNullException("sortkey1");
			}
			if (sortkey2 == null)
			{
				throw new ArgumentNullException("sortkey2");
			}
			if (object.ReferenceEquals(sortkey1, sortkey2) || object.ReferenceEquals(sortkey1.OriginalString, sortkey2.OriginalString))
			{
				return 0;
			}
			byte[] keyData = sortkey1.KeyData;
			byte[] keyData2 = sortkey2.KeyData;
			int num = (keyData.Length <= keyData2.Length) ? keyData.Length : keyData2.Length;
			for (int i = 0; i < num; i++)
			{
				if (keyData[i] != keyData2[i])
				{
					return (keyData[i] >= keyData2[i]) ? 1 : -1;
				}
			}
			return (keyData.Length != keyData2.Length) ? ((keyData.Length >= keyData2.Length) ? 1 : -1) : 0;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00027154 File Offset: 0x00025354
		public virtual string OriginalString
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0002715C File Offset: 0x0002535C
		public virtual byte[] KeyData
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00027164 File Offset: 0x00025364
		public override bool Equals(object value)
		{
			SortKey sortKey = value as SortKey;
			return sortKey != null && this.lcid == sortKey.lcid && this.options == sortKey.options && SortKey.Compare(this, sortKey) == 0;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000271B0 File Offset: 0x000253B0
		public override int GetHashCode()
		{
			if (this.key.Length == 0)
			{
				return 0;
			}
			int num = (int)this.key[0];
			for (int i = 1; i < this.key.Length; i++)
			{
				num ^= (int)this.key[i] << (i & 3);
			}
			return num;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00027204 File Offset: 0x00025404
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"SortKey - ",
				this.lcid,
				", ",
				this.options,
				", ",
				this.source
			});
		}

		// Token: 0x04000414 RID: 1044
		private readonly string source;

		// Token: 0x04000415 RID: 1045
		private readonly CompareOptions options;

		// Token: 0x04000416 RID: 1046
		private readonly byte[] key;

		// Token: 0x04000417 RID: 1047
		private readonly int lcid;
	}
}
