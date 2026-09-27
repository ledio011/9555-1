using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200046E RID: 1134
	public class req_guild_battle_guess
	{
		// Token: 0x0200046F RID: 1135
		public class request : SprotoTypeBase
		{
			// Token: 0x06002302 RID: 8962 RVA: 0x000A3A88 File Offset: 0x000A1C88
			public request() : base(req_guild_battle_guess.request.max_field_count)
			{
			}

			// Token: 0x06002303 RID: 8963 RVA: 0x000A3A98 File Offset: 0x000A1C98
			public request(byte[] buffer) : base(req_guild_battle_guess.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002305 RID: 8965 RVA: 0x000A3AB0 File Offset: 0x000A1CB0
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002306 RID: 8966 RVA: 0x000A3AEC File Offset: 0x000A1CEC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C26 RID: 7206
			private static int max_field_count;
		}
	}
}
