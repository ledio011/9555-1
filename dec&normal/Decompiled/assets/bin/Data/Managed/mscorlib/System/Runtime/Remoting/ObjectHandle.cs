using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x020002CE RID: 718
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class ObjectHandle : MarshalByRefObject, IObjectHandle
	{
		// Token: 0x0600166F RID: 5743 RVA: 0x0004E700 File Offset: 0x0004C900
		public ObjectHandle(object o)
		{
			this._wrapped = o;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0004E710 File Offset: 0x0004C910
		public override object InitializeLifetimeService()
		{
			return base.InitializeLifetimeService();
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0004E718 File Offset: 0x0004C918
		public object Unwrap()
		{
			return this._wrapped;
		}

		// Token: 0x04000B92 RID: 2962
		private object _wrapped;
	}
}
