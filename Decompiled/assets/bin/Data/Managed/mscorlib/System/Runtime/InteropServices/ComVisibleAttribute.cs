using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000239 RID: 569
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	public sealed class ComVisibleAttribute : Attribute
	{
		// Token: 0x06001335 RID: 4917 RVA: 0x00045464 File Offset: 0x00043664
		public ComVisibleAttribute(bool visibility)
		{
			this.Visible = visibility;
		}

		// Token: 0x040009EF RID: 2543
		private bool Visible;
	}
}
