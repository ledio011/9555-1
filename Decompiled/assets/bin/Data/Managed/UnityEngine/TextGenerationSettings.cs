using System;

namespace UnityEngine
{
	// Token: 0x0200011A RID: 282
	public struct TextGenerationSettings
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x0001906C File Offset: 0x0001726C
		private bool CompareColors(Color left, Color right)
		{
			Color32 color = left;
			Color32 color2 = right;
			return color.Equals(color2);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00019098 File Offset: 0x00017298
		private bool CompareVector2(Vector2 left, Vector2 right)
		{
			return Mathf.Approximately(left.x, right.x) && Mathf.Approximately(left.y, right.y);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000190C8 File Offset: 0x000172C8
		public bool Equals(TextGenerationSettings other)
		{
			return this.CompareColors(this.color, other.color) && this.fontSize == other.fontSize && Mathf.Approximately(this.scaleFactor, other.scaleFactor) && this.resizeTextMinSize == other.resizeTextMinSize && this.resizeTextMaxSize == other.resizeTextMaxSize && Mathf.Approximately(this.lineSpacing, other.lineSpacing) && this.fontStyle == other.fontStyle && this.richText == other.richText && this.textAnchor == other.textAnchor && this.resizeTextForBestFit == other.resizeTextForBestFit && this.resizeTextMinSize == other.resizeTextMinSize && this.resizeTextMaxSize == other.resizeTextMaxSize && this.resizeTextForBestFit == other.resizeTextForBestFit && this.updateBounds == other.updateBounds && this.horizontalOverflow == other.horizontalOverflow && this.verticalOverflow == other.verticalOverflow && this.CompareVector2(this.generationExtents, other.generationExtents) && this.CompareVector2(this.pivot, other.pivot) && this.font == other.font;
		}

		// Token: 0x04000461 RID: 1121
		public Font font;

		// Token: 0x04000462 RID: 1122
		public Color color;

		// Token: 0x04000463 RID: 1123
		public int fontSize;

		// Token: 0x04000464 RID: 1124
		public float lineSpacing;

		// Token: 0x04000465 RID: 1125
		public bool richText;

		// Token: 0x04000466 RID: 1126
		public float scaleFactor;

		// Token: 0x04000467 RID: 1127
		public FontStyle fontStyle;

		// Token: 0x04000468 RID: 1128
		public TextAnchor textAnchor;

		// Token: 0x04000469 RID: 1129
		public bool resizeTextForBestFit;

		// Token: 0x0400046A RID: 1130
		public int resizeTextMinSize;

		// Token: 0x0400046B RID: 1131
		public int resizeTextMaxSize;

		// Token: 0x0400046C RID: 1132
		public bool updateBounds;

		// Token: 0x0400046D RID: 1133
		public VerticalWrapMode verticalOverflow;

		// Token: 0x0400046E RID: 1134
		public HorizontalWrapMode horizontalOverflow;

		// Token: 0x0400046F RID: 1135
		public Vector2 generationExtents;

		// Token: 0x04000470 RID: 1136
		public Vector2 pivot;

		// Token: 0x04000471 RID: 1137
		public bool generateOutOfBounds;
	}
}
