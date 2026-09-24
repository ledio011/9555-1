using System;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	public struct BoneWeight
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600027F RID: 639 RVA: 0x000072FC File Offset: 0x000054FC
		public float weight0
		{
			get
			{
				return this.m_Weight0;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00007304 File Offset: 0x00005504
		public float weight1
		{
			get
			{
				return this.m_Weight1;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000730C File Offset: 0x0000550C
		public float weight2
		{
			get
			{
				return this.m_Weight2;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00007314 File Offset: 0x00005514
		public float weight3
		{
			get
			{
				return this.m_Weight3;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000731C File Offset: 0x0000551C
		public int boneIndex0
		{
			get
			{
				return this.m_BoneIndex0;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00007324 File Offset: 0x00005524
		public int boneIndex1
		{
			get
			{
				return this.m_BoneIndex1;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000732C File Offset: 0x0000552C
		public int boneIndex2
		{
			get
			{
				return this.m_BoneIndex2;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00007334 File Offset: 0x00005534
		public int boneIndex3
		{
			get
			{
				return this.m_BoneIndex3;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000733C File Offset: 0x0000553C
		public override int GetHashCode()
		{
			return this.boneIndex0.GetHashCode() ^ this.boneIndex1.GetHashCode() << 2 ^ this.boneIndex2.GetHashCode() >> 2 ^ this.boneIndex3.GetHashCode() >> 1 ^ this.weight0.GetHashCode() << 5 ^ this.weight1.GetHashCode() << 4 ^ this.weight2.GetHashCode() >> 4 ^ this.weight3.GetHashCode() >> 3;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000073D4 File Offset: 0x000055D4
		public override bool Equals(object other)
		{
			if (!(other is BoneWeight))
			{
				return false;
			}
			BoneWeight boneWeight = (BoneWeight)other;
			bool result;
			if (this.boneIndex0.Equals(boneWeight.boneIndex0) && this.boneIndex1.Equals(boneWeight.boneIndex1) && this.boneIndex2.Equals(boneWeight.boneIndex2) && this.boneIndex3.Equals(boneWeight.boneIndex3))
			{
				Vector4 vector = new Vector4(this.weight0, this.weight1, this.weight2, this.weight3);
				result = vector.Equals(new Vector4(boneWeight.weight0, boneWeight.weight1, boneWeight.weight2, boneWeight.weight3));
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0400002E RID: 46
		private float m_Weight0;

		// Token: 0x0400002F RID: 47
		private float m_Weight1;

		// Token: 0x04000030 RID: 48
		private float m_Weight2;

		// Token: 0x04000031 RID: 49
		private float m_Weight3;

		// Token: 0x04000032 RID: 50
		private int m_BoneIndex0;

		// Token: 0x04000033 RID: 51
		private int m_BoneIndex1;

		// Token: 0x04000034 RID: 52
		private int m_BoneIndex2;

		// Token: 0x04000035 RID: 53
		private int m_BoneIndex3;
	}
}
