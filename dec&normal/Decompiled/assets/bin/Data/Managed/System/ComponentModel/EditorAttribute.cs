using System;

namespace System.ComponentModel
{
	// Token: 0x02000017 RID: 23
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	public sealed class EditorAttribute : Attribute
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002974 File Offset: 0x00000B74
		public EditorAttribute(string typeName, string baseTypeName)
		{
			this.name = typeName;
			this.basename = baseTypeName;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000298C File Offset: 0x00000B8C
		public string EditorBaseTypeName
		{
			get
			{
				return this.basename;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002994 File Offset: 0x00000B94
		public string EditorTypeName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000299C File Offset: 0x00000B9C
		public override bool Equals(object obj)
		{
			return obj is EditorAttribute && ((EditorAttribute)obj).EditorBaseTypeName.Equals(this.basename) && ((EditorAttribute)obj).EditorTypeName.Equals(this.name);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000029EC File Offset: 0x00000BEC
		public override int GetHashCode()
		{
			return (this.name + this.basename).GetHashCode();
		}

		// Token: 0x0400003A RID: 58
		private string name;

		// Token: 0x0400003B RID: 59
		private string basename;
	}
}
