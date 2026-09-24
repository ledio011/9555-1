using System;

namespace System.ComponentModel
{
	// Token: 0x02000018 RID: 24
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
	public sealed class EditorBrowsableAttribute : Attribute
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002A04 File Offset: 0x00000C04
		public EditorBrowsableAttribute(EditorBrowsableState state)
		{
			this.state = state;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002A14 File Offset: 0x00000C14
		public EditorBrowsableState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002A1C File Offset: 0x00000C1C
		public override bool Equals(object obj)
		{
			return obj is EditorBrowsableAttribute && (obj == this || ((EditorBrowsableAttribute)obj).State == this.state);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002A48 File Offset: 0x00000C48
		public override int GetHashCode()
		{
			return this.state.GetHashCode();
		}

		// Token: 0x0400003C RID: 60
		private EditorBrowsableState state;
	}
}
