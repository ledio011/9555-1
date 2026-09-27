using System;

namespace System
{
	// Token: 0x0200015E RID: 350
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x06000D25 RID: 3365 RVA: 0x00032DEC File Offset: 0x00030FEC
		public MonoNotSupportedAttribute(string comment) : base(comment)
		{
		}
	}
}
