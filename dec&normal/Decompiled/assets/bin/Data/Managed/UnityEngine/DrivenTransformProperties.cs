using System;

namespace UnityEngine
{
	// Token: 0x0200004D RID: 77
	[Flags]
	public enum DrivenTransformProperties
	{
		// Token: 0x0400007A RID: 122
		None = 0,
		// Token: 0x0400007B RID: 123
		All = -1,
		// Token: 0x0400007C RID: 124
		AnchoredPositionX = 2,
		// Token: 0x0400007D RID: 125
		AnchoredPositionY = 4,
		// Token: 0x0400007E RID: 126
		AnchoredPositionZ = 8,
		// Token: 0x0400007F RID: 127
		Rotation = 16,
		// Token: 0x04000080 RID: 128
		ScaleX = 32,
		// Token: 0x04000081 RID: 129
		ScaleY = 64,
		// Token: 0x04000082 RID: 130
		ScaleZ = 128,
		// Token: 0x04000083 RID: 131
		AnchorMinX = 256,
		// Token: 0x04000084 RID: 132
		AnchorMinY = 512,
		// Token: 0x04000085 RID: 133
		AnchorMaxX = 1024,
		// Token: 0x04000086 RID: 134
		AnchorMaxY = 2048,
		// Token: 0x04000087 RID: 135
		SizeDeltaX = 4096,
		// Token: 0x04000088 RID: 136
		SizeDeltaY = 8192,
		// Token: 0x04000089 RID: 137
		PivotX = 16384,
		// Token: 0x0400008A RID: 138
		PivotY = 32768,
		// Token: 0x0400008B RID: 139
		AnchoredPosition = 6,
		// Token: 0x0400008C RID: 140
		AnchoredPosition3D = 14,
		// Token: 0x0400008D RID: 141
		Scale = 224,
		// Token: 0x0400008E RID: 142
		AnchorMin = 768,
		// Token: 0x0400008F RID: 143
		AnchorMax = 3072,
		// Token: 0x04000090 RID: 144
		Anchors = 3840,
		// Token: 0x04000091 RID: 145
		SizeDelta = 12288,
		// Token: 0x04000092 RID: 146
		Pivot = 49152
	}
}
