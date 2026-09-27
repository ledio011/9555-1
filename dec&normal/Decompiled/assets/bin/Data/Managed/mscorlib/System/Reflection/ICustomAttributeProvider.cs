using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001C9 RID: 457
	[ComVisible(true)]
	public interface ICustomAttributeProvider
	{
		// Token: 0x06001128 RID: 4392
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06001129 RID: 4393
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x0600112A RID: 4394
		bool IsDefined(Type attributeType, bool inherit);
	}
}
