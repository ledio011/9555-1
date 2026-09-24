using System;

namespace UnityEngine
{
	// Token: 0x020000DA RID: 218
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class RangeAttribute : PropertyAttribute
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x00013AF0 File Offset: 0x00011CF0
		public RangeAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04000342 RID: 834
		public readonly float min;

		// Token: 0x04000343 RID: 835
		public readonly float max;
	}
}
