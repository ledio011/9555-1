using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020001E1 RID: 481
	internal struct MonoMethodInfo
	{
		// Token: 0x06001208 RID: 4616
		[MethodImpl(4096)]
		private static extern void get_method_info(IntPtr handle, out MonoMethodInfo info);

		// Token: 0x06001209 RID: 4617 RVA: 0x00044578 File Offset: 0x00042778
		internal static MonoMethodInfo GetMethodInfo(IntPtr handle)
		{
			MonoMethodInfo result;
			MonoMethodInfo.get_method_info(handle, out result);
			return result;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00044590 File Offset: 0x00042790
		internal static Type GetDeclaringType(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).parent;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000445AC File Offset: 0x000427AC
		internal static Type GetReturnType(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).ret;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000445C8 File Offset: 0x000427C8
		internal static MethodAttributes GetAttributes(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).attrs;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x000445E4 File Offset: 0x000427E4
		internal static CallingConventions GetCallingConvention(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).callconv;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00044600 File Offset: 0x00042800
		internal static MethodImplAttributes GetMethodImplementationFlags(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).iattrs;
		}

		// Token: 0x0600120F RID: 4623
		[MethodImpl(4096)]
		private static extern ParameterInfo[] get_parameter_info(IntPtr handle, MemberInfo member);

		// Token: 0x06001210 RID: 4624 RVA: 0x0004461C File Offset: 0x0004281C
		internal static ParameterInfo[] GetParametersInfo(IntPtr handle, MemberInfo member)
		{
			return MonoMethodInfo.get_parameter_info(handle, member);
		}

		// Token: 0x0400091C RID: 2332
		private Type parent;

		// Token: 0x0400091D RID: 2333
		private Type ret;

		// Token: 0x0400091E RID: 2334
		internal MethodAttributes attrs;

		// Token: 0x0400091F RID: 2335
		internal MethodImplAttributes iattrs;

		// Token: 0x04000920 RID: 2336
		private CallingConventions callconv;
	}
}
