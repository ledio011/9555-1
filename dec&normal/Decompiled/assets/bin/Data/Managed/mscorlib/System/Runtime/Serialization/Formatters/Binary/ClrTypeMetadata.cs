using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020002ED RID: 749
	internal abstract class ClrTypeMetadata : TypeMetadata
	{
		// Token: 0x06001759 RID: 5977 RVA: 0x0005224C File Offset: 0x0005044C
		public ClrTypeMetadata(Type instanceType)
		{
			this.InstanceType = instanceType;
			this.InstanceTypeName = instanceType.FullName;
			this.TypeAssemblyName = instanceType.Assembly.FullName;
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x00052278 File Offset: 0x00050478
		public override bool RequiresTypes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000C0D RID: 3085
		public Type InstanceType;
	}
}
