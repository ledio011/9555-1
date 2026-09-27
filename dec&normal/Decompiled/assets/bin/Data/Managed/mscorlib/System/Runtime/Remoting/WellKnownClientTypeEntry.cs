using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x020002E2 RID: 738
	[ComVisible(true)]
	public class WellKnownClientTypeEntry : TypeEntry
	{
		// Token: 0x06001727 RID: 5927 RVA: 0x000517E0 File Offset: 0x0004F9E0
		public WellKnownClientTypeEntry(string typeName, string assemblyName, string objectUrl)
		{
			this.obj_url = objectUrl;
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x00051840 File Offset: 0x0004FA40
		public string ApplicationUrl
		{
			get
			{
				return this.app_url;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x00051848 File Offset: 0x0004FA48
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x00051850 File Offset: 0x0004FA50
		public string ObjectUrl
		{
			get
			{
				return this.obj_url;
			}
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00051858 File Offset: 0x0004FA58
		public override string ToString()
		{
			if (this.ApplicationUrl != null)
			{
				return base.TypeName + base.AssemblyName + this.ObjectUrl + this.ApplicationUrl;
			}
			return base.TypeName + base.AssemblyName + this.ObjectUrl;
		}

		// Token: 0x04000BDA RID: 3034
		private Type obj_type;

		// Token: 0x04000BDB RID: 3035
		private string obj_url;

		// Token: 0x04000BDC RID: 3036
		private string app_url;
	}
}
