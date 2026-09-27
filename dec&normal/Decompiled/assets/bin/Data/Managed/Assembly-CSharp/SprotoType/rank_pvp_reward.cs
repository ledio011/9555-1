using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000456 RID: 1110
	public class rank_pvp_reward
	{
		// Token: 0x02000457 RID: 1111
		public class request : SprotoTypeBase
		{
			// Token: 0x0600228D RID: 8845 RVA: 0x000A2D30 File Offset: 0x000A0F30
			public request() : base(rank_pvp_reward.request.max_field_count)
			{
			}

			// Token: 0x0600228E RID: 8846 RVA: 0x000A2D40 File Offset: 0x000A0F40
			public request(byte[] buffer) : base(rank_pvp_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002290 RID: 8848 RVA: 0x000A2D58 File Offset: 0x000A0F58
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002291 RID: 8849 RVA: 0x000A2D94 File Offset: 0x000A0F94
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C0B RID: 7179
			private static int max_field_count;
		}
	}
}
