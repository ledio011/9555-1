using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004C1 RID: 1217
	public class request_rank_pvp_data
	{
		// Token: 0x020004C2 RID: 1218
		public class request : SprotoTypeBase
		{
			// Token: 0x06002448 RID: 9288 RVA: 0x000A5CC4 File Offset: 0x000A3EC4
			public request() : base(request_rank_pvp_data.request.max_field_count)
			{
			}

			// Token: 0x06002449 RID: 9289 RVA: 0x000A5CD4 File Offset: 0x000A3ED4
			public request(byte[] buffer) : base(request_rank_pvp_data.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x0600244B RID: 9291 RVA: 0x000A5CEC File Offset: 0x000A3EEC
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x0600244C RID: 9292 RVA: 0x000A5D28 File Offset: 0x000A3F28
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C69 RID: 7273
			private static int max_field_count;
		}
	}
}
