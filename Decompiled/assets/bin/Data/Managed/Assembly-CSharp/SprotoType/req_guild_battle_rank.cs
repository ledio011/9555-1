using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000474 RID: 1140
	public class req_guild_battle_rank
	{
		// Token: 0x02000475 RID: 1141
		public class request : SprotoTypeBase
		{
			// Token: 0x06002314 RID: 8980 RVA: 0x000A3C20 File Offset: 0x000A1E20
			public request() : base(req_guild_battle_rank.request.max_field_count)
			{
			}

			// Token: 0x06002315 RID: 8981 RVA: 0x000A3C30 File Offset: 0x000A1E30
			public request(byte[] buffer) : base(req_guild_battle_rank.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002317 RID: 8983 RVA: 0x000A3C48 File Offset: 0x000A1E48
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002318 RID: 8984 RVA: 0x000A3C84 File Offset: 0x000A1E84
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C29 RID: 7209
			private static int max_field_count;
		}
	}
}
