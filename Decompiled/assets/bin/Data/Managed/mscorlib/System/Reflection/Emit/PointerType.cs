using System;

namespace System.Reflection.Emit
{
	// Token: 0x020001B9 RID: 441
	internal class PointerType : DerivedType
	{
		// Token: 0x0600108B RID: 4235 RVA: 0x00040464 File Offset: 0x0003E664
		internal PointerType(Type elementType) : base(elementType)
		{
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00040470 File Offset: 0x0003E670
		protected override bool IsPointerImpl()
		{
			return true;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x00040474 File Offset: 0x0003E674
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00040480 File Offset: 0x0003E680
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "*";
		}
	}
}
