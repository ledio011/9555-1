using System;

namespace UnityEngine
{
	// Token: 0x02000109 RID: 265
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class SpaceAttribute : PropertyAttribute
	{
		// Token: 0x060009C7 RID: 2503 RVA: 0x00015F28 File Offset: 0x00014128
		public SpaceAttribute(float height)
		{
			this.height = height;
		}

		// Token: 0x040003D5 RID: 981
		public readonly float height;
	}
}
