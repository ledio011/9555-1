using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000476 RID: 1142
	public class req_guild_battle_state
	{
		// Token: 0x02000477 RID: 1143
		public class request : SprotoTypeBase
		{
			// Token: 0x0600231A RID: 8986 RVA: 0x000A3CA8 File Offset: 0x000A1EA8
			public request() : base(req_guild_battle_state.request.max_field_count)
			{
			}

			// Token: 0x0600231B RID: 8987 RVA: 0x000A3CB8 File Offset: 0x000A1EB8
			public request(byte[] buffer) : base(req_guild_battle_state.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x0600231D RID: 8989 RVA: 0x000A3CD0 File Offset: 0x000A1ED0
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x0600231E RID: 8990 RVA: 0x000A3D0C File Offset: 0x000A1F0C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C2A RID: 7210
			private static int max_field_count;
		}
	}
}
