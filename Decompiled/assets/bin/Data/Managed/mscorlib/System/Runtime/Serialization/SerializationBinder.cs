using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000310 RID: 784
	[ComVisible(true)]
	[Serializable]
	public abstract class SerializationBinder
	{
		// Token: 0x060017F5 RID: 6133
		public abstract Type BindToType(string assemblyName, string typeName);
	}
}
