using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001D5 RID: 469
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_MethodInfo))]
	[Serializable]
	public abstract class MethodInfo : MethodBase, _MethodInfo
	{
		// Token: 0x06001164 RID: 4452
		public abstract MethodInfo GetBaseDefinition();

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x000427BC File Offset: 0x000409BC
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Method;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x000427C0 File Offset: 0x000409C0
		public virtual Type ReturnType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x000427C4 File Offset: 0x000409C4
		public virtual MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			throw new NotSupportedException(base.GetType().ToString());
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x000427D8 File Offset: 0x000409D8
		[ComVisible(true)]
		public override Type[] GetGenericArguments()
		{
			return Type.EmptyTypes;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x000427E0 File Offset: 0x000409E0
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x000427E4 File Offset: 0x000409E4
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x000427E8 File Offset: 0x000409E8
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}
	}
}
