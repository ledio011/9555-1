using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000543 RID: 1347
	public class ret_guild_score_info
	{
		// Token: 0x02000544 RID: 1348
		public class request : SprotoTypeBase
		{
			// Token: 0x0600274C RID: 10060 RVA: 0x000AB9B4 File Offset: 0x000A9BB4
			public request() : base(ret_guild_score_info.request.max_field_count)
			{
			}

			// Token: 0x0600274D RID: 10061 RVA: 0x000AB9C4 File Offset: 0x000A9BC4
			public request(byte[] buffer) : base(ret_guild_score_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B15 RID: 2837
			// (get) Token: 0x0600274F RID: 10063 RVA: 0x000AB9E0 File Offset: 0x000A9BE0
			// (set) Token: 0x06002750 RID: 10064 RVA: 0x000AB9E8 File Offset: 0x000A9BE8
			public guild_battle_score_info guild_battle_score_info
			{
				get
				{
					return this._guild_battle_score_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guild_battle_score_info = value;
				}
			}

			// Token: 0x17000B16 RID: 2838
			// (get) Token: 0x06002751 RID: 10065 RVA: 0x000ABA00 File Offset: 0x000A9C00
			public bool HasGuild_battle_score_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002752 RID: 10066 RVA: 0x000ABA10 File Offset: 0x000A9C10
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
						this.guild_battle_score_info = this.deserialize.read_obj<guild_battle_score_info>();
					}
				}
			}

			// Token: 0x06002753 RID: 10067 RVA: 0x000ABA6C File Offset: 0x000A9C6C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.guild_battle_score_info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D30 RID: 7472
			private static int max_field_count = 1;

			// Token: 0x04001D31 RID: 7473
			private guild_battle_score_info _guild_battle_score_info;
		}
	}
}
