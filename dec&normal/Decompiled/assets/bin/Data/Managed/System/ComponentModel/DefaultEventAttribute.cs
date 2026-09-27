using System;

namespace System.ComponentModel
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultEventAttribute : Attribute
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000025F4 File Offset: 0x000007F4
		public DefaultEventAttribute(string name)
		{
			this.eventName = name;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002614 File Offset: 0x00000814
		public override bool Equals(object o)
		{
			return o is DefaultEventAttribute && ((DefaultEventAttribute)o).eventName == this.eventName;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000263C File Offset: 0x0000083C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000020 RID: 32
		private string eventName;

		// Token: 0x04000021 RID: 33
		public static readonly DefaultEventAttribute Default = new DefaultEventAttribute(null);
	}
}
