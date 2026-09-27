using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x02000385 RID: 901
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class SuppressUnmanagedCodeSecurityAttribute : Attribute
	{
	}
}
