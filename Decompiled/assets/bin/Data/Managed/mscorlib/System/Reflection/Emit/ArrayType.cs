using System;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000191 RID: 401
	internal class ArrayType : DerivedType
	{
		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003A6CC File Offset: 0x000388CC
		internal ArrayType(Type elementType, int rank) : base(elementType)
		{
			this.rank = rank;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003A6DC File Offset: 0x000388DC
		protected override bool IsArrayImpl()
		{
			return true;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0003A6E0 File Offset: 0x000388E0
		public override int GetArrayRank()
		{
			return (this.rank != 0) ? this.rank : 1;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0003A6FC File Offset: 0x000388FC
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003A708 File Offset: 0x00038908
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			if (((ModuleBuilder)this.elementType.Module).assemblyb.IsCompilerContext)
			{
				return (this.elementType.Attributes & TypeAttributes.VisibilityMask) | TypeAttributes.Sealed | TypeAttributes.Serializable;
			}
			return this.elementType.Attributes;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003A75C File Offset: 0x0003895C
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(elementName);
			stringBuilder.Append("[");
			for (int i = 1; i < this.rank; i++)
			{
				stringBuilder.Append(",");
			}
			if (this.rank == 1)
			{
				stringBuilder.Append("*");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400062B RID: 1579
		private int rank;
	}
}
