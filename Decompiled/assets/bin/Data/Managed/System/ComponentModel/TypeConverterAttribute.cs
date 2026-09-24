using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x02000028 RID: 40
	[AttributeUsage(AttributeTargets.All)]
	[ComVisible(true)]
	public sealed class TypeConverterAttribute : Attribute
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public TypeConverterAttribute()
		{
			this.converter_type = string.Empty;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public TypeConverterAttribute(string typeName)
		{
			this.converter_type = typeName;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public TypeConverterAttribute(Type type)
		{
			this.converter_type = type.AssemblyQualifiedName;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002C04 File Offset: 0x00000E04
		public override bool Equals(object obj)
		{
			return obj is TypeConverterAttribute && ((TypeConverterAttribute)obj).ConverterTypeName == this.converter_type;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002C2C File Offset: 0x00000E2C
		public override int GetHashCode()
		{
			return this.converter_type.GetHashCode();
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002C3C File Offset: 0x00000E3C
		public string ConverterTypeName
		{
			get
			{
				return this.converter_type;
			}
		}

		// Token: 0x04000051 RID: 81
		public static readonly TypeConverterAttribute Default = new TypeConverterAttribute();

		// Token: 0x04000052 RID: 82
		private string converter_type;
	}
}
