using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004BF RID: 1215
	public class request_random_rank_pvp_opponent
	{
		// Token: 0x020004C0 RID: 1216
		public class request : SprotoTypeBase
		{
			// Token: 0x06002442 RID: 9282 RVA: 0x000A5C3C File Offset: 0x000A3E3C
			public request() : base(request_random_rank_pvp_opponent.request.max_field_count)
			{
			}

			// Token: 0x06002443 RID: 9283 RVA: 0x000A5C4C File Offset: 0x000A3E4C
			public request(byte[] buffer) : base(request_random_rank_pvp_opponent.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002445 RID: 9285 RVA: 0x000A5C64 File Offset: 0x000A3E64
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002446 RID: 9286 RVA: 0x000A5CA0 File Offset: 0x000A3EA0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C68 RID: 7272
			private static int max_field_count;
		}
	}
}
