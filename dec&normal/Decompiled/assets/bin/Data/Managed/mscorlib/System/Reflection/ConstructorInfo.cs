using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200018F RID: 399
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ConstructorInfo))]
	[ComVisible(true)]
	[Serializable]
	public abstract class ConstructorInfo : MethodBase, _ConstructorInfo
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0003A690 File Offset: 0x00038890
		[ComVisible(true)]
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Constructor;
			}
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0003A694 File Offset: 0x00038894
		[DebuggerHidden]
		[DebuggerStepThrough]
		public object Invoke(object[] parameters)
		{
			if (parameters == null)
			{
				parameters = new object[0];
			}
			return this.Invoke(BindingFlags.CreateInstance, null, parameters, null);
		}

		// Token: 0x06000ECD RID: 3789
		public abstract object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x04000628 RID: 1576
		[ComVisible(true)]
		public static readonly string ConstructorName = ".ctor";

		// Token: 0x04000629 RID: 1577
		[ComVisible(true)]
		public static readonly string TypeConstructorName = ".cctor";
	}
}
