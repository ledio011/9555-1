using System;

namespace UnityEngine
{
	// Token: 0x020000BC RID: 188
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class MultilineAttribute : PropertyAttribute
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x00012EE8 File Offset: 0x000110E8
		public MultilineAttribute(int lines)
		{
			this.lines = lines;
		}

		// Token: 0x04000308 RID: 776
		public readonly int lines;
	}
}
