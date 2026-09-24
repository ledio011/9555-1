using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000452 RID: 1106
	public class rank_pvp_other_player_die
	{
		// Token: 0x02000453 RID: 1107
		public class request : SprotoTypeBase
		{
			// Token: 0x0600227B RID: 8827 RVA: 0x000A2B30 File Offset: 0x000A0D30
			public request() : base(rank_pvp_other_player_die.request.max_field_count)
			{
			}

			// Token: 0x0600227C RID: 8828 RVA: 0x000A2B40 File Offset: 0x000A0D40
			public request(byte[] buffer) : base(rank_pvp_other_player_die.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x0600227E RID: 8830 RVA: 0x000A2B58 File Offset: 0x000A0D58
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x0600227F RID: 8831 RVA: 0x000A2B94 File Offset: 0x000A0D94
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C07 RID: 7175
			private static int max_field_count;
		}
	}
}
