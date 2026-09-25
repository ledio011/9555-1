using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004D5 RID: 1237
	public class request_survive_top
	{
		// Token: 0x020004D6 RID: 1238
		public class request : SprotoTypeBase
		{
			// Token: 0x0600248D RID: 9357 RVA: 0x000A6384 File Offset: 0x000A4584
			public request() : base(request_survive_top.request.max_field_count)
			{
			}

			// Token: 0x0600248E RID: 9358 RVA: 0x000A6394 File Offset: 0x000A4594
			public request(byte[] buffer) : base(request_survive_top.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002490 RID: 9360 RVA: 0x000A63AC File Offset: 0x000A45AC
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002491 RID: 9361 RVA: 0x000A63E8 File Offset: 0x000A45E8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C76 RID: 7286
			private static int max_field_count;
		}
	}
}
