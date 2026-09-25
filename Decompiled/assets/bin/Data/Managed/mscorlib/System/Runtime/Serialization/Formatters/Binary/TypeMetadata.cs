using System;
using System.IO;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020002F9 RID: 761
	internal abstract class TypeMetadata
	{
		// Token: 0x060017B4 RID: 6068
		public abstract void WriteAssemblies(ObjectWriter ow, BinaryWriter writer);

		// Token: 0x060017B5 RID: 6069
		public abstract void WriteTypeData(ObjectWriter ow, BinaryWriter writer, bool writeTypes);

		// Token: 0x060017B6 RID: 6070
		public abstract void WriteObjectData(ObjectWriter ow, BinaryWriter writer, object data);

		// Token: 0x060017B7 RID: 6071 RVA: 0x0005680C File Offset: 0x00054A0C
		public virtual bool IsCompatible(TypeMetadata other)
		{
			return true;
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060017B8 RID: 6072
		public abstract bool RequiresTypes { get; }

		// Token: 0x04000C46 RID: 3142
		public string TypeAssemblyName;

		// Token: 0x04000C47 RID: 3143
		public string InstanceTypeName;
	}
}
