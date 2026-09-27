using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x020002E0 RID: 736
	[ComVisible(true)]
	public class TypeEntry
	{
		// Token: 0x0600171E RID: 5918 RVA: 0x0005155C File Offset: 0x0004F75C
		protected TypeEntry()
		{
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x00051564 File Offset: 0x0004F764
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x0005156C File Offset: 0x0004F76C
		public string AssemblyName
		{
			get
			{
				return this.assembly_name;
			}
			set
			{
				this.assembly_name = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x00051578 File Offset: 0x0004F778
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x00051580 File Offset: 0x0004F780
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
			set
			{
				this.type_name = value;
			}
		}

		// Token: 0x04000BD5 RID: 3029
		private string assembly_name;

		// Token: 0x04000BD6 RID: 3030
		private string type_name;
	}
}
