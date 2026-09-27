using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000242 RID: 578
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	public sealed class GuidAttribute : Attribute
	{
		// Token: 0x06001349 RID: 4937 RVA: 0x000455D0 File Offset: 0x000437D0
		public GuidAttribute(string guid)
		{
			this.guidValue = guid;
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x000455E0 File Offset: 0x000437E0
		public string Value
		{
			get
			{
				return this.guidValue;
			}
		}

		// Token: 0x04000A03 RID: 2563
		private string guidValue;
	}
}
