using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000521 RID: 1313
	public class ret_guild_battle_guess
	{
		// Token: 0x02000522 RID: 1314
		public class request : SprotoTypeBase
		{
			// Token: 0x06002664 RID: 9828 RVA: 0x000A9CD4 File Offset: 0x000A7ED4
			public request() : base(ret_guild_battle_guess.request.max_field_count)
			{
			}

			// Token: 0x06002665 RID: 9829 RVA: 0x000A9CE4 File Offset: 0x000A7EE4
			public request(byte[] buffer) : base(ret_guild_battle_guess.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AC1 RID: 2753
			// (get) Token: 0x06002667 RID: 9831 RVA: 0x000A9D00 File Offset: 0x000A7F00
			// (set) Token: 0x06002668 RID: 9832 RVA: 0x000A9D08 File Offset: 0x000A7F08
			public long guildId
			{
				get
				{
					return this._guildId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guildId = value;
				}
			}

			// Token: 0x17000AC2 RID: 2754
			// (get) Token: 0x06002669 RID: 9833 RVA: 0x000A9D20 File Offset: 0x000A7F20
			public bool HasGuildId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600266A RID: 9834 RVA: 0x000A9D30 File Offset: 0x000A7F30
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.guildId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600266B RID: 9835 RVA: 0x000A9D8C File Offset: 0x000A7F8C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.guildId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CF1 RID: 7409
			private static int max_field_count = 1;

			// Token: 0x04001CF2 RID: 7410
			private long _guildId;
		}
	}
}
