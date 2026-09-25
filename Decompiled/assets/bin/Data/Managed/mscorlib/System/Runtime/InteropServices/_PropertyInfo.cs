using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000229 RID: 553
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibImportClass(typeof(PropertyInfo))]
	[Guid("F59ED4E4-E68F-3218-BD77-061AA82824BF")]
	[ComVisible(true)]
	[CLSCompliant(false)]
	public interface _PropertyInfo
	{
		// Token: 0x060012E7 RID: 4839
		MethodInfo GetGetMethod();

		// Token: 0x060012E8 RID: 4840
		MethodInfo GetGetMethod(bool nonPublic);

		// Token: 0x060012E9 RID: 4841
		ParameterInfo[] GetIndexParameters();

		// Token: 0x060012EA RID: 4842
		MethodInfo GetSetMethod(bool nonPublic);

		// Token: 0x060012EB RID: 4843
		object GetValue(object obj, object[] index);

		// Token: 0x060012EC RID: 4844
		object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x060012ED RID: 4845
		void SetValue(object obj, object value, object[] index);

		// Token: 0x060012EE RID: 4846
		void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060012EF RID: 4847
		PropertyAttributes Attributes { get; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060012F0 RID: 4848
		bool CanRead { get; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060012F1 RID: 4849
		bool CanWrite { get; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060012F2 RID: 4850
		MemberTypes MemberType { get; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060012F3 RID: 4851
		Type PropertyType { get; }
	}
}
