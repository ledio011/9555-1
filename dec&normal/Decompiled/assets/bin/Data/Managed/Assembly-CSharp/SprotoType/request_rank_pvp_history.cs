using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004C3 RID: 1219
	public class request_rank_pvp_history
	{
		// Token: 0x020004C4 RID: 1220
		public class request : SprotoTypeBase
		{
			// Token: 0x0600244E RID: 9294 RVA: 0x000A5D4C File Offset: 0x000A3F4C
			public request() : base(request_rank_pvp_history.request.max_field_count)
			{
			}

			// Token: 0x0600244F RID: 9295 RVA: 0x000A5D5C File Offset: 0x000A3F5C
			public request(byte[] buffer) : base(request_rank_pvp_history.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002451 RID: 9297 RVA: 0x000A5D74 File Offset: 0x000A3F74
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002452 RID: 9298 RVA: 0x000A5DB0 File Offset: 0x000A3FB0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C6A RID: 7274
			private static int max_field_count;
		}
	}
}
