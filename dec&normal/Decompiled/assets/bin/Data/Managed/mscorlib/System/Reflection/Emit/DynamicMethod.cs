using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000199 RID: 409
	[ComVisible(true)]
	public sealed class DynamicMethod : MethodInfo
	{
		// Token: 0x06000F5B RID: 3931 RVA: 0x0003BB54 File Offset: 0x00039D54
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner) : this(name, returnType, parameterTypes, owner, false)
		{
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0003BB64 File Offset: 0x00039D64
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility) : this(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, owner, skipVisibility)
		{
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0003BB84 File Offset: 0x00039D84
		public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility) : this(name, attributes, callingConvention, returnType, parameterTypes, owner, owner.Module, skipVisibility, false)
		{
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0003BBAC File Offset: 0x00039DAC
		private DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type owner, Module m, bool skipVisibility, bool anonHosted)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (returnType == null)
			{
				returnType = typeof(void);
			}
			if (m == null && !anonHosted)
			{
				throw new ArgumentNullException("m");
			}
			if (returnType.IsByRef)
			{
				throw new ArgumentException("Return type can't be a byref type", "returnType");
			}
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentException("Parameter " + i + " is null", "parameterTypes");
					}
				}
			}
			if (m == null)
			{
				m = DynamicMethod.AnonHostModuleHolder.anon_host_module;
			}
			this.name = name;
			this.attributes = (attributes | MethodAttributes.Static);
			this.callingConvention = callingConvention;
			this.returnType = returnType;
			this.parameters = parameterTypes;
			this.owner = owner;
			this.module = m;
			this.skipVisibility = skipVisibility;
		}

		// Token: 0x06000F5F RID: 3935
		[MethodImpl(4096)]
		private extern void create_dynamic_method(DynamicMethod m);

		// Token: 0x06000F60 RID: 3936
		[MethodImpl(4096)]
		private extern void destroy_dynamic_method(DynamicMethod m);

		// Token: 0x06000F61 RID: 3937 RVA: 0x0003BCB4 File Offset: 0x00039EB4
		private void CreateDynMethod()
		{
			if (this.mhandle.Value == IntPtr.Zero)
			{
				if (this.ilgen == null || ILGenerator.Mono_GetCurrentOffset(this.ilgen) == 0)
				{
					throw new InvalidOperationException("Method '" + this.name + "' does not have a method body.");
				}
				this.ilgen.label_fixup();
				try
				{
					this.creating = true;
					if (this.refs != null)
					{
						for (int i = 0; i < this.refs.Length; i++)
						{
							if (this.refs[i] is DynamicMethod)
							{
								DynamicMethod dynamicMethod = (DynamicMethod)this.refs[i];
								if (!dynamicMethod.creating)
								{
									dynamicMethod.CreateDynMethod();
								}
							}
						}
					}
				}
				finally
				{
					this.creating = false;
				}
				this.create_dynamic_method(this);
			}
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0003BD9C File Offset: 0x00039F9C
		~DynamicMethod()
		{
			this.destroy_dynamic_method(this);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003BDCC File Offset: 0x00039FCC
		[ComVisible(true)]
		public Delegate CreateDelegate(Type delegateType)
		{
			if (delegateType == null)
			{
				throw new ArgumentNullException("delegateType");
			}
			if (this.deleg != null)
			{
				return this.deleg;
			}
			this.CreateDynMethod();
			this.deleg = Delegate.CreateDelegate(delegateType, this);
			return this.deleg;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0003BE0C File Offset: 0x0003A00C
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0003BE10 File Offset: 0x0003A010
		[MonoTODO("Not implemented")]
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0003BE18 File Offset: 0x0003A018
		[MonoTODO("Not implemented")]
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0003BE20 File Offset: 0x0003A020
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0003BE2C File Offset: 0x0003A02C
		public ILGenerator GetILGenerator(int streamSize)
		{
			if ((this.GetMethodImplementationFlags() & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.GetMethodImplementationFlags() & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL)
			{
				throw new InvalidOperationException("Method body should not exist.");
			}
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.Module, new DynamicMethodTokenGenerator(this), streamSize);
			return this.ilgen;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0003BE90 File Offset: 0x0003A090
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MethodImplAttributes.IL;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0003BE94 File Offset: 0x0003A094
		public override ParameterInfo[] GetParameters()
		{
			if (this.parameters == null)
			{
				return new ParameterInfo[0];
			}
			ParameterInfo[] array = new ParameterInfo[this.parameters.Length];
			for (int i = 0; i < this.parameters.Length; i++)
			{
				array[i] = new ParameterInfo((this.pinfo != null) ? this.pinfo[i + 1] : null, this.parameters[i], this, i + 1);
			}
			return array;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0003BF0C File Offset: 0x0003A10C
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			object result;
			try
			{
				this.CreateDynMethod();
				if (this.method == null)
				{
					this.method = new MonoMethod(this.mhandle);
				}
				result = this.method.Invoke(obj, parameters);
			}
			catch (MethodAccessException inner)
			{
				throw new TargetInvocationException("Method cannot be invoked.", inner);
			}
			return result;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0003BF78 File Offset: 0x0003A178
		[MonoTODO("Not implemented")]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0003BF80 File Offset: 0x0003A180
		public override string ToString()
		{
			string text = string.Empty;
			ParameterInfo[] array = this.GetParameters();
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					text += ", ";
				}
				text += array[i].ParameterType.Name;
			}
			return string.Concat(new string[]
			{
				this.ReturnType.Name,
				" ",
				this.Name,
				"(",
				text,
				")"
			});
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0003C014 File Offset: 0x0003A214
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0003C01C File Offset: 0x0003A21C
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.callingConvention;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0003C024 File Offset: 0x0003A224
		public override Type DeclaringType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0003C028 File Offset: 0x0003A228
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.mhandle;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0003C030 File Offset: 0x0003A230
		public override Module Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0003C038 File Offset: 0x0003A238
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0003C040 File Offset: 0x0003A240
		public override Type ReflectedType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0003C044 File Offset: 0x0003A244
		public override Type ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003C04C File Offset: 0x0003A24C
		internal int AddRef(object reference)
		{
			if (this.refs == null)
			{
				this.refs = new object[4];
			}
			if (this.nrefs >= this.refs.Length - 1)
			{
				object[] destinationArray = new object[this.refs.Length * 2];
				Array.Copy(this.refs, destinationArray, this.refs.Length);
				this.refs = destinationArray;
			}
			this.refs[this.nrefs] = reference;
			this.refs[this.nrefs + 1] = null;
			this.nrefs += 2;
			return this.nrefs - 1;
		}

		// Token: 0x0400066C RID: 1644
		private RuntimeMethodHandle mhandle;

		// Token: 0x0400066D RID: 1645
		private string name;

		// Token: 0x0400066E RID: 1646
		private Type returnType;

		// Token: 0x0400066F RID: 1647
		private Type[] parameters;

		// Token: 0x04000670 RID: 1648
		private MethodAttributes attributes;

		// Token: 0x04000671 RID: 1649
		private CallingConventions callingConvention;

		// Token: 0x04000672 RID: 1650
		private Module module;

		// Token: 0x04000673 RID: 1651
		private bool skipVisibility;

		// Token: 0x04000674 RID: 1652
		private bool init_locals = true;

		// Token: 0x04000675 RID: 1653
		private ILGenerator ilgen;

		// Token: 0x04000676 RID: 1654
		private int nrefs;

		// Token: 0x04000677 RID: 1655
		private object[] refs;

		// Token: 0x04000678 RID: 1656
		private IntPtr referenced_by;

		// Token: 0x04000679 RID: 1657
		private Type owner;

		// Token: 0x0400067A RID: 1658
		private Delegate deleg;

		// Token: 0x0400067B RID: 1659
		private MonoMethod method;

		// Token: 0x0400067C RID: 1660
		private ParameterBuilder[] pinfo;

		// Token: 0x0400067D RID: 1661
		internal bool creating;

		// Token: 0x0200019A RID: 410
		private class AnonHostModuleHolder
		{
			// Token: 0x06000F77 RID: 3959 RVA: 0x0003C0E4 File Offset: 0x0003A2E4
			static AnonHostModuleHolder()
			{
				AssemblyName assemblyName = new AssemblyName();
				assemblyName.Name = "Anonymously Hosted DynamicMethods Assembly";
				AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
				DynamicMethod.AnonHostModuleHolder.anon_host_module = assemblyBuilder.GetManifestModule();
			}

			// Token: 0x0400067E RID: 1662
			public static Module anon_host_module;
		}
	}
}
