using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001F4 RID: 500
	[ComVisible(true)]
	[Serializable]
	public class TypeDelegator : Type
	{
		// Token: 0x06001257 RID: 4695 RVA: 0x00044EF8 File Offset: 0x000430F8
		protected TypeDelegator()
		{
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x00044F00 File Offset: 0x00043100
		public override Assembly Assembly
		{
			get
			{
				return this.typeImpl.Assembly;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00044F10 File Offset: 0x00043110
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.typeImpl.AssemblyQualifiedName;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x00044F20 File Offset: 0x00043120
		public override Type BaseType
		{
			get
			{
				return this.typeImpl.BaseType;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00044F30 File Offset: 0x00043130
		public override string FullName
		{
			get
			{
				return this.typeImpl.FullName;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x00044F40 File Offset: 0x00043140
		public override Guid GUID
		{
			get
			{
				return this.typeImpl.GUID;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00044F50 File Offset: 0x00043150
		public override Module Module
		{
			get
			{
				return this.typeImpl.Module;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x00044F60 File Offset: 0x00043160
		public override string Name
		{
			get
			{
				return this.typeImpl.Name;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00044F70 File Offset: 0x00043170
		public override string Namespace
		{
			get
			{
				return this.typeImpl.Namespace;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x00044F80 File Offset: 0x00043180
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this.typeImpl.TypeHandle;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00044F90 File Offset: 0x00043190
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.typeImpl.UnderlyingSystemType;
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00044FA0 File Offset: 0x000431A0
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.typeImpl.Attributes;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00044FB0 File Offset: 0x000431B0
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeImpl.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00044FC4 File Offset: 0x000431C4
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetConstructors(bindingAttr);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00044FD4 File Offset: 0x000431D4
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(inherit);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00044FE4 File Offset: 0x000431E4
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00044FF4 File Offset: 0x000431F4
		public override Type GetElementType()
		{
			return this.typeImpl.GetElementType();
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00045004 File Offset: 0x00043204
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvent(name, bindingAttr);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00045014 File Offset: 0x00043214
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvents(bindingAttr);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00045024 File Offset: 0x00043224
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetField(name, bindingAttr);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00045034 File Offset: 0x00043234
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetFields(bindingAttr);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00045044 File Offset: 0x00043244
		public override Type[] GetInterfaces()
		{
			return this.typeImpl.GetInterfaces();
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00045054 File Offset: 0x00043254
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeImpl.GetMethodImplInternal(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0004506C File Offset: 0x0004326C
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMethods(bindingAttr);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0004507C File Offset: 0x0004327C
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetProperties(bindingAttr);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0004508C File Offset: 0x0004328C
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeImpl.GetPropertyImplInternal(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x000450A4 File Offset: 0x000432A4
		protected override bool HasElementTypeImpl()
		{
			return this.typeImpl.HasElementType;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x000450B4 File Offset: 0x000432B4
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this.typeImpl.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000450DC File Offset: 0x000432DC
		protected override bool IsArrayImpl()
		{
			return this.typeImpl.IsArray;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000450EC File Offset: 0x000432EC
		protected override bool IsByRefImpl()
		{
			return this.typeImpl.IsByRef;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000450FC File Offset: 0x000432FC
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.typeImpl.IsDefined(attributeType, inherit);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0004510C File Offset: 0x0004330C
		protected override bool IsPointerImpl()
		{
			return this.typeImpl.IsPointer;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0004511C File Offset: 0x0004331C
		protected override bool IsPrimitiveImpl()
		{
			return this.typeImpl.IsPrimitive;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0004512C File Offset: 0x0004332C
		protected override bool IsValueTypeImpl()
		{
			return this.typeImpl.IsValueType;
		}

		// Token: 0x04000987 RID: 2439
		protected Type typeImpl;
	}
}
