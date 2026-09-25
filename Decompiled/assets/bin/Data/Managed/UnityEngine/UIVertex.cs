using System;

namespace UnityEngine
{
	// Token: 0x0200012F RID: 303
	public struct UIVertex
	{
		// Token: 0x040004D9 RID: 1241
		public Vector3 position;

		// Token: 0x040004DA RID: 1242
		public Vector3 normal;

		// Token: 0x040004DB RID: 1243
		public Color32 color;

		// Token: 0x040004DC RID: 1244
		public Vector2 uv0;

		// Token: 0x040004DD RID: 1245
		public Vector2 uv1;

		// Token: 0x040004DE RID: 1246
		public Vector4 tangent;

		// Token: 0x040004DF RID: 1247
		private static readonly Color32 s_DefaultColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x040004E0 RID: 1248
		private static readonly Vector4 s_DefaultTangent = new Vector4(1f, 0f, 0f, -1f);

		// Token: 0x040004E1 RID: 1249
		public static UIVertex simpleVert = new UIVertex
		{
			position = Vector3.zero,
			normal = Vector3.back,
			tangent = UIVertex.s_DefaultTangent,
			color = UIVertex.s_DefaultColor,
			uv0 = Vector2.zero,
			uv1 = Vector2.zero
		};
	}
}
