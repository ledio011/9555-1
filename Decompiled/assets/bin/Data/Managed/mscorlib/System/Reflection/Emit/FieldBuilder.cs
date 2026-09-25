using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200019F RID: 415
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_FieldBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class FieldBuilder : FieldInfo, _FieldBuilder
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x0003C4B0 File Offset: 0x0003A6B0
		public override FieldAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0003C4B8 File Offset: 0x0003A6B8
		public override Type DeclaringType
		{
			get
			{
				return this.typeb;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0003C4C0 File Offset: 0x0003A6C0
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw this.CreateNotSupportedException();
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0003C4C8 File Offset: 0x0003A6C8
		public override Type FieldType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0003C4D0 File Offset: 0x0003A6D0
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0003C4D8 File Offset: 0x0003A6D8
		public override Type ReflectedType
		{
			get
			{
				return this.typeb;
			}
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0003C4E0 File Offset: 0x0003A6E0
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.typeb.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, inherit);
			}
			throw this.CreateNotSupportedException();
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0003C500 File Offset: 0x0003A700
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.typeb.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
			}
			throw this.CreateNotSupportedException();
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003C524 File Offset: 0x0003A724
		public override object GetValue(object obj)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003C52C File Offset: 0x0003A72C
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003C534 File Offset: 0x0003A734
		internal override int GetFieldOffset()
		{
			return 0;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0003C538 File Offset: 0x0003A738
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x0003C540 File Offset: 0x0003A740
		internal override UnmanagedMarshal UMarshal
		{
			get
			{
				return this.marshal_info;
			}
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0003C548 File Offset: 0x0003A748
		private Exception CreateNotSupportedException()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x0003C554 File Offset: 0x0003A754
		public override Module Module
		{
			get
			{
				return base.Module;
			}
		}

		// Token: 0x0400068F RID: 1679
		private FieldAttributes attrs;

		// Token: 0x04000690 RID: 1680
		private Type type;

		// Token: 0x04000691 RID: 1681
		private string name;

		// Token: 0x04000692 RID: 1682
		private object def_value;

		// Token: 0x04000693 RID: 1683
		private int offset;

		// Token: 0x04000694 RID: 1684
		private int table_idx;

		// Token: 0x04000695 RID: 1685
		internal TypeBuilder typeb;

		// Token: 0x04000696 RID: 1686
		private byte[] rva_data;

		// Token: 0x04000697 RID: 1687
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000698 RID: 1688
		private UnmanagedMarshal marshal_info;

		// Token: 0x04000699 RID: 1689
		private RuntimeFieldHandle handle;

		// Token: 0x0400069A RID: 1690
		private Type[] modReq;

		// Token: 0x0400069B RID: 1691
		private Type[] modOpt;
	}
}
