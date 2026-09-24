using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200021D RID: 541
	[ComVisible(true)]
	[Guid("8A7C1442-A9FB-366B-80D8-4939FFA6DBE0")]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(FieldInfo))]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface _FieldInfo
	{
		// Token: 0x060012CA RID: 4810
		object GetValue(object obj);

		// Token: 0x060012CB RID: 4811
		void SetValue(object obj, object value);

		// Token: 0x060012CC RID: 4812
		void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060012CD RID: 4813
		FieldAttributes Attributes { get; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060012CE RID: 4814
		RuntimeFieldHandle FieldHandle { get; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060012CF RID: 4815
		Type FieldType { get; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060012D0 RID: 4816
		bool IsLiteral { get; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060012D1 RID: 4817
		bool IsNotSerialized { get; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060012D2 RID: 4818
		bool IsPublic { get; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060012D3 RID: 4819
		bool IsStatic { get; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060012D4 RID: 4820
		MemberTypes MemberType { get; }
	}
}
