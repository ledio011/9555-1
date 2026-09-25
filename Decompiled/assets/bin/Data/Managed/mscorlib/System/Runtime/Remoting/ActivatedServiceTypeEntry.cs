using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x02000255 RID: 597
	[ComVisible(true)]
	public class ActivatedServiceTypeEntry : TypeEntry
	{
		// Token: 0x06001415 RID: 5141 RVA: 0x000465B8 File Offset: 0x000447B8
		public ActivatedServiceTypeEntry(string typeName, string assemblyName)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00046610 File Offset: 0x00044810
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00046618 File Offset: 0x00044818
		public override string ToString()
		{
			return base.AssemblyName + base.TypeName;
		}

		// Token: 0x04000A6D RID: 2669
		private Type obj_type;
	}
}
