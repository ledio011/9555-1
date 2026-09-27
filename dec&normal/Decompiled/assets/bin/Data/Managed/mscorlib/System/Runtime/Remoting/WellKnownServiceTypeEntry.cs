using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x020002E4 RID: 740
	[ComVisible(true)]
	public class WellKnownServiceTypeEntry : TypeEntry
	{
		// Token: 0x0600172C RID: 5932 RVA: 0x000518A8 File Offset: 0x0004FAA8
		public WellKnownServiceTypeEntry(string typeName, string assemblyName, string objectUri, WellKnownObjectMode mode)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			this.obj_uri = objectUri;
			this.obj_mode = mode;
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x00051910 File Offset: 0x0004FB10
		public WellKnownObjectMode Mode
		{
			get
			{
				return this.obj_mode;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x00051918 File Offset: 0x0004FB18
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x00051920 File Offset: 0x0004FB20
		public string ObjectUri
		{
			get
			{
				return this.obj_uri;
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00051928 File Offset: 0x0004FB28
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.TypeName,
				", ",
				base.AssemblyName,
				" ",
				this.ObjectUri
			});
		}

		// Token: 0x04000BE0 RID: 3040
		private Type obj_type;

		// Token: 0x04000BE1 RID: 3041
		private string obj_uri;

		// Token: 0x04000BE2 RID: 3042
		private WellKnownObjectMode obj_mode;
	}
}
