using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020000D2 RID: 210
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ConditionalAttribute : Attribute
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x00020DD8 File Offset: 0x0001EFD8
		public ConditionalAttribute(string conditionString)
		{
			this.myCondition = conditionString;
		}

		// Token: 0x040002CD RID: 717
		private string myCondition;
	}
}
