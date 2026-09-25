using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x02000034 RID: 52
	[System.ComponentModel.Designer("System.Diagnostics.Design.ProcessModuleDesigner, System.Design, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ProcessModule : System.ComponentModel.Component
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003B54 File Offset: 0x00001D54
		[MonitoringDescription("The name of this module")]
		public string ModuleName
		{
			get
			{
				return this.modulename;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003B5C File Offset: 0x00001D5C
		public override string ToString()
		{
			return this.ModuleName;
		}

		// Token: 0x040000B3 RID: 179
		private IntPtr baseaddr;

		// Token: 0x040000B4 RID: 180
		private IntPtr entryaddr;

		// Token: 0x040000B5 RID: 181
		private string filename;

		// Token: 0x040000B6 RID: 182
		private FileVersionInfo version_info;

		// Token: 0x040000B7 RID: 183
		private int memory_size;

		// Token: 0x040000B8 RID: 184
		private string modulename;
	}
}
