using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting
{
	// Token: 0x02000254 RID: 596
	[ComVisible(true)]
	public class ActivatedClientTypeEntry : TypeEntry
	{
		// Token: 0x06001410 RID: 5136 RVA: 0x00046528 File Offset: 0x00044728
		public ActivatedClientTypeEntry(string typeName, string assemblyName, string appUrl)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			this.applicationUrl = appUrl;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00046588 File Offset: 0x00044788
		public string ApplicationUrl
		{
			get
			{
				return this.applicationUrl;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x00046590 File Offset: 0x00044790
		public IContextAttribute[] ContextAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00046594 File Offset: 0x00044794
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0004659C File Offset: 0x0004479C
		public override string ToString()
		{
			return base.TypeName + base.AssemblyName + this.ApplicationUrl;
		}

		// Token: 0x04000A6B RID: 2667
		private string applicationUrl;

		// Token: 0x04000A6C RID: 2668
		private Type obj_type;
	}
}
