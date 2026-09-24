using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000472 RID: 1138
	public class req_guild_battle_member
	{
		// Token: 0x02000473 RID: 1139
		public class request : SprotoTypeBase
		{
			// Token: 0x0600230E RID: 8974 RVA: 0x000A3B98 File Offset: 0x000A1D98
			public request() : base(req_guild_battle_member.request.max_field_count)
			{
			}

			// Token: 0x0600230F RID: 8975 RVA: 0x000A3BA8 File Offset: 0x000A1DA8
			public request(byte[] buffer) : base(req_guild_battle_member.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002311 RID: 8977 RVA: 0x000A3BC0 File Offset: 0x000A1DC0
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002312 RID: 8978 RVA: 0x000A3BFC File Offset: 0x000A1DFC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C28 RID: 7208
			private static int max_field_count;
		}
	}
}
