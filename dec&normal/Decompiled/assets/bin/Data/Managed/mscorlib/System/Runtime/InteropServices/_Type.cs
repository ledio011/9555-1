using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200022B RID: 555
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibImportClass(typeof(Type))]
	[Guid("BCA8B44D-AAD6-3A86-8AB7-03349F4F2DA2")]
	public interface _Type
	{
		// Token: 0x060012F8 RID: 4856
		bool Equals(object other);

		// Token: 0x060012F9 RID: 4857
		bool Equals(Type o);

		// Token: 0x060012FA RID: 4858
		int GetArrayRank();

		// Token: 0x060012FB RID: 4859
		ConstructorInfo GetConstructor(Type[] types);

		// Token: 0x060012FC RID: 4860
		ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060012FD RID: 4861
		ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060012FE RID: 4862
		ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

		// Token: 0x060012FF RID: 4863
		Type GetElementType();

		// Token: 0x06001300 RID: 4864
		EventInfo GetEvent(string name, BindingFlags bindingAttr);

		// Token: 0x06001301 RID: 4865
		EventInfo[] GetEvents(BindingFlags bindingAttr);

		// Token: 0x06001302 RID: 4866
		FieldInfo GetField(string name);

		// Token: 0x06001303 RID: 4867
		int GetHashCode();

		// Token: 0x06001304 RID: 4868
		Type[] GetInterfaces();

		// Token: 0x06001305 RID: 4869
		MethodInfo GetMethod(string name);

		// Token: 0x06001306 RID: 4870
		MethodInfo GetMethod(string name, Type[] types);

		// Token: 0x06001307 RID: 4871
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06001308 RID: 4872
		PropertyInfo GetProperty(string name);

		// Token: 0x06001309 RID: 4873
		PropertyInfo GetProperty(string name, Type returnType);

		// Token: 0x0600130A RID: 4874
		PropertyInfo GetProperty(string name, Type returnType, Type[] types);

		// Token: 0x0600130B RID: 4875
		Type GetType();

		// Token: 0x0600130C RID: 4876
		bool IsAssignableFrom(Type c);

		// Token: 0x0600130D RID: 4877
		bool IsInstanceOfType(object o);

		// Token: 0x0600130E RID: 4878
		bool IsSubclassOf(Type c);

		// Token: 0x0600130F RID: 4879
		string ToString();

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001310 RID: 4880
		Assembly Assembly { get; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001311 RID: 4881
		string AssemblyQualifiedName { get; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001312 RID: 4882
		TypeAttributes Attributes { get; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001313 RID: 4883
		Type BaseType { get; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001314 RID: 4884
		Type DeclaringType { get; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001315 RID: 4885
		string FullName { get; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001316 RID: 4886
		Guid GUID { get; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001317 RID: 4887
		bool HasElementType { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001318 RID: 4888
		bool IsAbstract { get; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001319 RID: 4889
		bool IsArray { get; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x0600131A RID: 4890
		bool IsByRef { get; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600131B RID: 4891
		bool IsClass { get; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x0600131C RID: 4892
		bool IsContextful { get; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600131D RID: 4893
		bool IsEnum { get; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600131E RID: 4894
		bool IsExplicitLayout { get; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600131F RID: 4895
		bool IsImport { get; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001320 RID: 4896
		bool IsInterface { get; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001321 RID: 4897
		bool IsMarshalByRef { get; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001322 RID: 4898
		bool IsPointer { get; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001323 RID: 4899
		bool IsPrimitive { get; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001324 RID: 4900
		bool IsSealed { get; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001325 RID: 4901
		bool IsSerializable { get; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001326 RID: 4902
		bool IsValueType { get; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001327 RID: 4903
		MemberTypes MemberType { get; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001328 RID: 4904
		Module Module { get; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001329 RID: 4905
		string Namespace { get; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x0600132A RID: 4906
		Type ReflectedType { get; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600132B RID: 4907
		RuntimeTypeHandle TypeHandle { get; }
	}
}
