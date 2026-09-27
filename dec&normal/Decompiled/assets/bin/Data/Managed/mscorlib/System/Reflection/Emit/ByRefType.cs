using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000194 RID: 404
	internal class ByRefType : DerivedType
	{
		// Token: 0x06000EF5 RID: 3829 RVA: 0x0003B2E0 File Offset: 0x000394E0
		internal ByRefType(Type elementType) : base(elementType)
		{
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003B2EC File Offset: 0x000394EC
		protected override bool IsByRefImpl()
		{
			return true;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x0003B2F0 File Offset: 0x000394F0
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003B2FC File Offset: 0x000394FC
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "&";
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003B314 File Offset: 0x00039514
		public override Type MakeArrayType()
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0003B320 File Offset: 0x00039520
		public override Type MakeArrayType(int rank)
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0003B32C File Offset: 0x0003952C
		public override Type MakeByRefType()
		{
			throw new ArgumentException("Cannot create a byref type of an already byref type");
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0003B338 File Offset: 0x00039538
		public override Type MakePointerType()
		{
			throw new ArgumentException("Cannot create a pointer type of a byref type");
		}
	}
}
