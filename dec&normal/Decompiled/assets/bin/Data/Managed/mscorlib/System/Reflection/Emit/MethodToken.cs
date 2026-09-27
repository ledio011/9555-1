using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001AC RID: 428
	[ComVisible(true)]
	[Serializable]
	public struct MethodToken
	{
		// Token: 0x06001057 RID: 4183 RVA: 0x0003E0C8 File Offset: 0x0003C2C8
		internal MethodToken(int val)
		{
			this.tokValue = val;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0003E0F0 File Offset: 0x0003C2F0
		public override bool Equals(object obj)
		{
			bool flag = obj is MethodToken;
			if (flag)
			{
				MethodToken methodToken = (MethodToken)obj;
				flag = (this.tokValue == methodToken.tokValue);
			}
			return flag;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0003E128 File Offset: 0x0003C328
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x0003E130 File Offset: 0x0003C330
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x040006F6 RID: 1782
		internal int tokValue;

		// Token: 0x040006F7 RID: 1783
		public static readonly MethodToken Empty = default(MethodToken);
	}
}
