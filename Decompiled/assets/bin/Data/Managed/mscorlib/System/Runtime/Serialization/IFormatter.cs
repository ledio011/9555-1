using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000300 RID: 768
	[ComVisible(true)]
	public interface IFormatter
	{
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060017BF RID: 6079
		SerializationBinder Binder { get; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060017C0 RID: 6080
		StreamingContext Context { get; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060017C1 RID: 6081
		// (set) Token: 0x060017C2 RID: 6082
		ISurrogateSelector SurrogateSelector { get; set; }

		// Token: 0x060017C3 RID: 6083
		object Deserialize(Stream serializationStream);

		// Token: 0x060017C4 RID: 6084
		void Serialize(Stream serializationStream, object graph);
	}
}
