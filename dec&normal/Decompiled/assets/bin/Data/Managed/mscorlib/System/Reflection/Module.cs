using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020001D7 RID: 471
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Module))]
	[Serializable]
	public class Module : ICustomAttributeProvider, _Module, ISerializable
	{
		// Token: 0x0600116F RID: 4463 RVA: 0x00042804 File Offset: 0x00040A04
		internal Module()
		{
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x00042830 File Offset: 0x00040A30
		public Assembly Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x00042838 File Offset: 0x00040A38
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x00042840 File Offset: 0x00040A40
		public string ScopeName
		{
			get
			{
				return this.scopename;
			}
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00042848 File Offset: 0x00040A48
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00042854 File Offset: 0x00040A54
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00042860 File Offset: 0x00040A60
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetModuleData(this, info, context);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0004287C File Offset: 0x00040A7C
		[ComVisible(true)]
		public virtual Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00042888 File Offset: 0x00040A88
		[ComVisible(true)]
		public virtual Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			if (className == string.Empty)
			{
				throw new ArgumentException("Type name can't be empty");
			}
			return this.assembly.InternalGetType(this, className, throwOnError, ignoreCase);
		}

		// Token: 0x06001179 RID: 4473
		[MethodImpl(4096)]
		private extern Type[] InternalGetTypes();

		// Token: 0x0600117A RID: 4474 RVA: 0x000428C8 File Offset: 0x00040AC8
		public virtual Type[] GetTypes()
		{
			return this.InternalGetTypes();
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000428D0 File Offset: 0x00040AD0
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000428DC File Offset: 0x00040ADC
		public bool IsResource()
		{
			return this.is_resource;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000428E4 File Offset: 0x00040AE4
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000428EC File Offset: 0x00040AEC
		private static bool filter_by_type_name(Type m, object filterCriteria)
		{
			string text = (string)filterCriteria;
			if (text.EndsWith("*"))
			{
				return m.Name.StartsWith(text.Substring(0, text.Length - 1));
			}
			return m.Name == text;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00042938 File Offset: 0x00040B38
		private static bool filter_by_type_name_ignore_case(Type m, object filterCriteria)
		{
			string text = (string)filterCriteria;
			if (text.EndsWith("*"))
			{
				return m.Name.ToLower().StartsWith(text.Substring(0, text.Length - 1).ToLower());
			}
			return string.Compare(m.Name, text, true) == 0;
		}

		// Token: 0x06001180 RID: 4480
		[MethodImpl(4096)]
		internal extern IntPtr GetHINSTANCE();

		// Token: 0x040008F5 RID: 2293
		private const BindingFlags defaultBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		// Token: 0x040008F6 RID: 2294
		public static readonly TypeFilter FilterTypeName = new TypeFilter(Module.filter_by_type_name);

		// Token: 0x040008F7 RID: 2295
		public static readonly TypeFilter FilterTypeNameIgnoreCase = new TypeFilter(Module.filter_by_type_name_ignore_case);

		// Token: 0x040008F8 RID: 2296
		private IntPtr _impl;

		// Token: 0x040008F9 RID: 2297
		internal Assembly assembly;

		// Token: 0x040008FA RID: 2298
		internal string fqname;

		// Token: 0x040008FB RID: 2299
		internal string name;

		// Token: 0x040008FC RID: 2300
		internal string scopename;

		// Token: 0x040008FD RID: 2301
		internal bool is_resource;

		// Token: 0x040008FE RID: 2302
		internal int token;
	}
}
