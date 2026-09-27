using System;

namespace UnityEngine
{
	// Token: 0x02000114 RID: 276
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class TextAreaAttribute : PropertyAttribute
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x00016654 File Offset: 0x00014854
		public TextAreaAttribute(int minLines, int maxLines)
		{
			this.minLines = minLines;
			this.maxLines = maxLines;
		}

		// Token: 0x0400040C RID: 1036
		public readonly int minLines;

		// Token: 0x0400040D RID: 1037
		public readonly int maxLines;
	}
}
