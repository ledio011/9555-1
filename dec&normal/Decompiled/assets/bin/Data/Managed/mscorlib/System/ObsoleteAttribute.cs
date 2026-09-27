using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200016E RID: 366
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[Serializable]
	public sealed class ObsoleteAttribute : Attribute
	{
		// Token: 0x06000DFF RID: 3583 RVA: 0x00037CFC File Offset: 0x00035EFC
		public ObsoleteAttribute()
		{
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00037D04 File Offset: 0x00035F04
		public ObsoleteAttribute(string message)
		{
			this._message = message;
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00037D14 File Offset: 0x00035F14
		public ObsoleteAttribute(string message, bool error)
		{
			this._message = message;
			this._error = error;
		}

		// Token: 0x040005C7 RID: 1479
		private string _message;

		// Token: 0x040005C8 RID: 1480
		private bool _error;
	}
}
