using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200047E RID: 1150
	public class req_guild_skill
	{
		// Token: 0x0200047F RID: 1151
		public class request : SprotoTypeBase
		{
			// Token: 0x06002338 RID: 9016 RVA: 0x000A3FC8 File Offset: 0x000A21C8
			public request() : base(req_guild_skill.request.max_field_count)
			{
			}

			// Token: 0x06002339 RID: 9017 RVA: 0x000A3FD8 File Offset: 0x000A21D8
			public request(byte[] buffer) : base(req_guild_skill.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x0600233B RID: 9019 RVA: 0x000A3FF0 File Offset: 0x000A21F0
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x0600233C RID: 9020 RVA: 0x000A402C File Offset: 0x000A222C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C30 RID: 7216
			private static int max_field_count;
		}
	}
}
