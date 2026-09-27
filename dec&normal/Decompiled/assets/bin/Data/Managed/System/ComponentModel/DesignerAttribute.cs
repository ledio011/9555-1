using System;
using System.ComponentModel.Design;

namespace System.ComponentModel
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerAttribute : Attribute
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000027AC File Offset: 0x000009AC
		public DesignerAttribute(string designerTypeName)
		{
			if (designerTypeName == null)
			{
				throw new NullReferenceException();
			}
			this.name = designerTypeName;
			this.basetypename = typeof(System.ComponentModel.Design.IDesigner).FullName;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000027DC File Offset: 0x000009DC
		public string DesignerBaseTypeName
		{
			get
			{
				return this.basetypename;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000027E4 File Offset: 0x000009E4
		public string DesignerTypeName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000027EC File Offset: 0x000009EC
		public override bool Equals(object obj)
		{
			return obj is DesignerAttribute && ((DesignerAttribute)obj).DesignerBaseTypeName.Equals(this.basetypename) && ((DesignerAttribute)obj).DesignerTypeName.Equals(this.name);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000283C File Offset: 0x00000A3C
		public override int GetHashCode()
		{
			return (this.name + this.basetypename).GetHashCode();
		}

		// Token: 0x04000027 RID: 39
		private string name;

		// Token: 0x04000028 RID: 40
		private string basetypename;
	}
}
