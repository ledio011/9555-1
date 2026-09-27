using System;
using System.IO;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020002EF RID: 751
	internal class MemberTypeMetadata : ClrTypeMetadata
	{
		// Token: 0x06001764 RID: 5988 RVA: 0x00052FAC File Offset: 0x000511AC
		public MemberTypeMetadata(Type type, StreamingContext context) : base(type)
		{
			this.members = FormatterServices.GetSerializableMembers(type, context);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00052FC4 File Offset: 0x000511C4
		public override void WriteAssemblies(ObjectWriter ow, BinaryWriter writer)
		{
			foreach (FieldInfo fieldInfo in this.members)
			{
				Type type = fieldInfo.FieldType;
				while (type.IsArray)
				{
					type = type.GetElementType();
				}
				ow.WriteAssembly(writer, type.Assembly);
			}
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00053024 File Offset: 0x00051224
		public override void WriteTypeData(ObjectWriter ow, BinaryWriter writer, bool writeTypes)
		{
			writer.Write(this.members.Length);
			foreach (FieldInfo fieldInfo in this.members)
			{
				writer.Write(fieldInfo.Name);
			}
			if (writeTypes)
			{
				foreach (FieldInfo fieldInfo2 in this.members)
				{
					ObjectWriter.WriteTypeCode(writer, fieldInfo2.FieldType);
				}
				foreach (FieldInfo fieldInfo3 in this.members)
				{
					ow.WriteTypeSpec(writer, fieldInfo3.FieldType);
				}
			}
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000530E8 File Offset: 0x000512E8
		public override void WriteObjectData(ObjectWriter ow, BinaryWriter writer, object data)
		{
			object[] objectData = FormatterServices.GetObjectData(data, this.members);
			for (int i = 0; i < objectData.Length; i++)
			{
				ow.WriteValue(writer, ((FieldInfo)this.members[i]).FieldType, objectData[i]);
			}
		}

		// Token: 0x04000C10 RID: 3088
		private MemberInfo[] members;
	}
}
