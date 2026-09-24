using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000308 RID: 776
	public class ask_copyscenes_info
	{
		// Token: 0x02000309 RID: 777
		public class request : SprotoTypeBase
		{
			// Token: 0x060015AF RID: 5551 RVA: 0x00088180 File Offset: 0x00086380
			public request() : base(ask_copyscenes_info.request.max_field_count)
			{
			}

			// Token: 0x060015B0 RID: 5552 RVA: 0x00088190 File Offset: 0x00086390
			public request(byte[] buffer) : base(ask_copyscenes_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060015B2 RID: 5554 RVA: 0x000881A8 File Offset: 0x000863A8
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060015B3 RID: 5555 RVA: 0x000881E4 File Offset: 0x000863E4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001868 RID: 6248
			private static int max_field_count;
		}
	}
}
