using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000207 RID: 519
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public sealed class RuntimeCompatibilityAttribute : Attribute
	{
		// Token: 0x17000327 RID: 807
		// (set) Token: 0x06001290 RID: 4752 RVA: 0x000452C0 File Offset: 0x000434C0
		public bool WrapNonExceptionThrows
		{
			set
			{
				this.wrap_non_exception_throws = value;
			}
		}

		// Token: 0x040009C6 RID: 2502
		private bool wrap_non_exception_throws;
	}
}
