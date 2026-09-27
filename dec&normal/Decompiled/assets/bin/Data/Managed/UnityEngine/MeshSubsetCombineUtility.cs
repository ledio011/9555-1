using System;

namespace UnityEngine
{
	// Token: 0x020000B6 RID: 182
	internal class MeshSubsetCombineUtility
	{
		// Token: 0x020000B7 RID: 183
		public struct MeshInstance
		{
			// Token: 0x040002FA RID: 762
			public int meshInstanceID;

			// Token: 0x040002FB RID: 763
			public Matrix4x4 transform;

			// Token: 0x040002FC RID: 764
			public Vector4 lightmapTilingOffset;
		}

		// Token: 0x020000B8 RID: 184
		public struct SubMeshInstance
		{
			// Token: 0x040002FD RID: 765
			public int meshInstanceID;

			// Token: 0x040002FE RID: 766
			public int vertexOffset;

			// Token: 0x040002FF RID: 767
			public int gameObjectInstanceID;

			// Token: 0x04000300 RID: 768
			public int subMeshIndex;

			// Token: 0x04000301 RID: 769
			public Matrix4x4 transform;
		}
	}
}
