using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020001D6 RID: 470
	[ComVisible(true)]
	[Serializable]
	public sealed class Missing : ISerializable
	{
		// Token: 0x0600116C RID: 4460 RVA: 0x000427EC File Offset: 0x000409EC
		internal Missing()
		{
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00042800 File Offset: 0x00040A00
		[MonoTODO]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x040008F4 RID: 2292
		public static readonly Missing Value = new Missing();
	}
}
