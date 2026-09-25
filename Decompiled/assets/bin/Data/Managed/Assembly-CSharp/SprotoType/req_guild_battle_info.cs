using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000470 RID: 1136
	public class req_guild_battle_info
	{
		// Token: 0x02000471 RID: 1137
		public class request : SprotoTypeBase
		{
			// Token: 0x06002308 RID: 8968 RVA: 0x000A3B10 File Offset: 0x000A1D10
			public request() : base(req_guild_battle_info.request.max_field_count)
			{
			}

			// Token: 0x06002309 RID: 8969 RVA: 0x000A3B20 File Offset: 0x000A1D20
			public request(byte[] buffer) : base(req_guild_battle_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x0600230B RID: 8971 RVA: 0x000A3B38 File Offset: 0x000A1D38
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x0600230C RID: 8972 RVA: 0x000A3B74 File Offset: 0x000A1D74
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C27 RID: 7207
			private static int max_field_count;
		}
	}
}
