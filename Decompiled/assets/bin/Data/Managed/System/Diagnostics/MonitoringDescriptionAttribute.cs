using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x0200002E RID: 46
	[AttributeUsage(AttributeTargets.All)]
	public class MonitoringDescriptionAttribute : System.ComponentModel.DescriptionAttribute
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00002EEC File Offset: 0x000010EC
		public MonitoringDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002EF8 File Offset: 0x000010F8
		public override string Description
		{
			get
			{
				return base.Description;
			}
		}
	}
}
