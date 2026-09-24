using System;
using System.IO;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020002F8 RID: 760
	internal class SerializableTypeMetadata : TypeMetadata
	{
		// Token: 0x060017AD RID: 6061 RVA: 0x0005658C File Offset: 0x0005478C
		public SerializableTypeMetadata(Type itype, SerializationInfo info)
		{
			this.types = new Type[info.MemberCount];
			this.names = new string[info.MemberCount];
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				this.types[num] = enumerator.ObjectType;
				this.names[num] = enumerator.Name;
				num++;
			}
			this.TypeAssemblyName = info.AssemblyName;
			this.InstanceTypeName = info.FullTypeName;
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00056614 File Offset: 0x00054814
		public override bool IsCompatible(TypeMetadata other)
		{
			if (!(other is SerializableTypeMetadata))
			{
				return false;
			}
			SerializableTypeMetadata serializableTypeMetadata = (SerializableTypeMetadata)other;
			if (this.types.Length != serializableTypeMetadata.types.Length)
			{
				return false;
			}
			if (this.TypeAssemblyName != serializableTypeMetadata.TypeAssemblyName)
			{
				return false;
			}
			if (this.InstanceTypeName != serializableTypeMetadata.InstanceTypeName)
			{
				return false;
			}
			for (int i = 0; i < this.types.Length; i++)
			{
				if (this.types[i] != serializableTypeMetadata.types[i])
				{
					return false;
				}
				if (this.names[i] != serializableTypeMetadata.names[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x000566CC File Offset: 0x000548CC
		public override void WriteAssemblies(ObjectWriter ow, BinaryWriter writer)
		{
			foreach (Type type in this.types)
			{
				Type type2 = type;
				while (type2.IsArray)
				{
					type2 = type2.GetElementType();
				}
				ow.WriteAssembly(writer, type2.Assembly);
			}
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00056720 File Offset: 0x00054920
		public override void WriteTypeData(ObjectWriter ow, BinaryWriter writer, bool writeTypes)
		{
			writer.Write(this.types.Length);
			foreach (string value in this.names)
			{
				writer.Write(value);
			}
			foreach (Type type in this.types)
			{
				ObjectWriter.WriteTypeCode(writer, type);
			}
			foreach (Type type2 in this.types)
			{
				ow.WriteTypeSpec(writer, type2);
			}
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000567C0 File Offset: 0x000549C0
		public override void WriteObjectData(ObjectWriter ow, BinaryWriter writer, object data)
		{
			SerializationInfo serializationInfo = (SerializationInfo)data;
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ow.WriteValue(writer, enumerator.ObjectType, enumerator.Value);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x00056800 File Offset: 0x00054A00
		public override bool RequiresTypes
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000C44 RID: 3140
		private Type[] types;

		// Token: 0x04000C45 RID: 3141
		private string[] names;
	}
}
