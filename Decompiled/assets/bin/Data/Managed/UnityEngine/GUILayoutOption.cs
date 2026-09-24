using System;

namespace UnityEngine
{
	// Token: 0x0200007A RID: 122
	public sealed class GUILayoutOption
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		internal GUILayoutOption(GUILayoutOption.Type type, object value)
		{
			this.type = type;
			this.value = value;
		}

		// Token: 0x04000137 RID: 311
		internal GUILayoutOption.Type type;

		// Token: 0x04000138 RID: 312
		internal object value;

		// Token: 0x0200007B RID: 123
		internal enum Type
		{
			// Token: 0x0400013A RID: 314
			fixedWidth,
			// Token: 0x0400013B RID: 315
			fixedHeight,
			// Token: 0x0400013C RID: 316
			minWidth,
			// Token: 0x0400013D RID: 317
			maxWidth,
			// Token: 0x0400013E RID: 318
			minHeight,
			// Token: 0x0400013F RID: 319
			maxHeight,
			// Token: 0x04000140 RID: 320
			stretchWidth,
			// Token: 0x04000141 RID: 321
			stretchHeight,
			// Token: 0x04000142 RID: 322
			alignStart,
			// Token: 0x04000143 RID: 323
			alignMiddle,
			// Token: 0x04000144 RID: 324
			alignEnd,
			// Token: 0x04000145 RID: 325
			alignJustify,
			// Token: 0x04000146 RID: 326
			equalSize,
			// Token: 0x04000147 RID: 327
			spacing
		}
	}
}
