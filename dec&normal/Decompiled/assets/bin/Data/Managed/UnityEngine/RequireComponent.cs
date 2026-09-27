using System;

namespace UnityEngine
{
	// Token: 0x020000EE RID: 238
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class RequireComponent : Attribute
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x00014C00 File Offset: 0x00012E00
		public RequireComponent(Type requiredComponent)
		{
			this.m_Type0 = requiredComponent;
		}

		// Token: 0x04000385 RID: 901
		public Type m_Type0;

		// Token: 0x04000386 RID: 902
		public Type m_Type1;

		// Token: 0x04000387 RID: 903
		public Type m_Type2;
	}
}
