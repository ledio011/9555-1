using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001CB RID: 459
	[ComVisible(true)]
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	public interface IReflect
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600112B RID: 4395
		Type UnderlyingSystemType { get; }

		// Token: 0x0600112C RID: 4396
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x0600112D RID: 4397
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x0600112E RID: 4398
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x0600112F RID: 4399
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06001130 RID: 4400
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x06001131 RID: 4401
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x06001132 RID: 4402
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x06001133 RID: 4403
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06001134 RID: 4404
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);
	}
}
