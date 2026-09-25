using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001D3 RID: 467
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_MethodBase))]
	[ComVisible(true)]
	[Serializable]
	public abstract class MethodBase : MemberInfo, _MethodBase
	{
		// Token: 0x0600114E RID: 4430 RVA: 0x00042630 File Offset: 0x00040830
		internal static MethodBase GetMethodFromHandleNoGenericCheck(RuntimeMethodHandle handle)
		{
			return MethodBase.GetMethodFromIntPtr(handle.Value, IntPtr.Zero);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00042644 File Offset: 0x00040844
		private static MethodBase GetMethodFromIntPtr(IntPtr handle, IntPtr declaringType)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			MethodBase methodFromHandleInternalType = MethodBase.GetMethodFromHandleInternalType(handle, declaringType);
			if (methodFromHandleInternalType == null)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			return methodFromHandleInternalType;
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00042688 File Offset: 0x00040888
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle)
		{
			MethodBase methodFromIntPtr = MethodBase.GetMethodFromIntPtr(handle.Value, IntPtr.Zero);
			Type declaringType = methodFromIntPtr.DeclaringType;
			if (declaringType.IsGenericType || declaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException("Cannot resolve method because it's declared in a generic class.");
			}
			return methodFromIntPtr;
		}

		// Token: 0x06001151 RID: 4433
		[MethodImpl(4096)]
		private static extern MethodBase GetMethodFromHandleInternalType(IntPtr method_handle, IntPtr type_handle);

		// Token: 0x06001152 RID: 4434
		public abstract MethodImplAttributes GetMethodImplementationFlags();

		// Token: 0x06001153 RID: 4435
		public abstract ParameterInfo[] GetParameters();

		// Token: 0x06001154 RID: 4436 RVA: 0x000426D0 File Offset: 0x000408D0
		internal virtual int GetParameterCount()
		{
			ParameterInfo[] parameters = this.GetParameters();
			if (parameters == null)
			{
				return 0;
			}
			return parameters.Length;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000426F0 File Offset: 0x000408F0
		[DebuggerHidden]
		[DebuggerStepThrough]
		public object Invoke(object obj, object[] parameters)
		{
			return this.Invoke(obj, BindingFlags.Default, null, parameters, null);
		}

		// Token: 0x06001156 RID: 4438
		public abstract object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001157 RID: 4439
		public abstract RuntimeMethodHandle MethodHandle { get; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001158 RID: 4440
		public abstract MethodAttributes Attributes { get; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00042700 File Offset: 0x00040900
		public virtual CallingConventions CallingConvention
		{
			get
			{
				return CallingConventions.Standard;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x00042704 File Offset: 0x00040904
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x00042714 File Offset: 0x00040914
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x00042728 File Offset: 0x00040928
		public bool IsVirtual
		{
			get
			{
				return (this.Attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x0004273C File Offset: 0x0004093C
		public bool IsAbstract
		{
			get
			{
				return (this.Attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00042750 File Offset: 0x00040950
		internal virtual int get_next_table_index(object obj, int table, bool inc)
		{
			if (this is MethodBuilder)
			{
				MethodBuilder methodBuilder = (MethodBuilder)this;
				return methodBuilder.get_next_table_index(obj, table, inc);
			}
			if (this is ConstructorBuilder)
			{
				ConstructorBuilder constructorBuilder = (ConstructorBuilder)this;
				return constructorBuilder.get_next_table_index(obj, table, inc);
			}
			throw new Exception("Method is not a builder method");
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000427A0 File Offset: 0x000409A0
		[ComVisible(true)]
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x000427A8 File Offset: 0x000409A8
		public virtual bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x000427AC File Offset: 0x000409AC
		public virtual bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x000427B0 File Offset: 0x000409B0
		public virtual bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}
	}
}
