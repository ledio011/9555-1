using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020000D7 RID: 215
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[Serializable]
	public sealed class DebuggerStepThroughAttribute : Attribute
	{
	}
}
