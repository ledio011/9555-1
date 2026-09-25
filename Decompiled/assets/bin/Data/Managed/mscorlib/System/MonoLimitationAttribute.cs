using System;

namespace System
{
	// Token: 0x0200015C RID: 348
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x06000D24 RID: 3364 RVA: 0x00032DE0 File Offset: 0x00030FE0
		public MonoLimitationAttribute(string comment) : base(comment)
		{
		}
	}
}
