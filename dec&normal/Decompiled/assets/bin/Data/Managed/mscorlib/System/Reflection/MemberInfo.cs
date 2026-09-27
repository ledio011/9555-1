using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001CF RID: 463
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_MemberInfo))]
	[ComVisible(true)]
	[Serializable]
	public abstract class MemberInfo : ICustomAttributeProvider, _MemberInfo
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001140 RID: 4416
		public abstract Type DeclaringType { get; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001141 RID: 4417
		public abstract MemberTypes MemberType { get; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001142 RID: 4418
		public abstract string Name { get; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001143 RID: 4419
		public abstract Type ReflectedType { get; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x00042294 File Offset: 0x00040494
		public virtual Module Module
		{
			get
			{
				return this.DeclaringType.Module;
			}
		}

		// Token: 0x06001145 RID: 4421
		public abstract bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x06001146 RID: 4422
		public abstract object[] GetCustomAttributes(bool inherit);

		// Token: 0x06001147 RID: 4423
		public abstract object[] GetCustomAttributes(Type attributeType, bool inherit);
	}
}
