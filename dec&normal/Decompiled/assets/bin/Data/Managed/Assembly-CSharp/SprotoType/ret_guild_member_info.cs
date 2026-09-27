using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200053D RID: 1341
	public class ret_guild_member_info
	{
		// Token: 0x0200053E RID: 1342
		public class request : SprotoTypeBase
		{
			// Token: 0x06002716 RID: 10006 RVA: 0x000AB2C0 File Offset: 0x000A94C0
			public request() : base(ret_guild_member_info.request.max_field_count)
			{
			}

			// Token: 0x06002717 RID: 10007 RVA: 0x000AB2D0 File Offset: 0x000A94D0
			public request(byte[] buffer) : base(ret_guild_member_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AFF RID: 2815
			// (get) Token: 0x06002719 RID: 10009 RVA: 0x000AB2EC File Offset: 0x000A94EC
			// (set) Token: 0x0600271A RID: 10010 RVA: 0x000AB2F4 File Offset: 0x000A94F4
			public Dictionary<long, guild_member_info> guild_member_info
			{
				get
				{
					return this._guild_member_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guild_member_info = value;
				}
			}

			// Token: 0x17000B00 RID: 2816
			// (get) Token: 0x0600271B RID: 10011 RVA: 0x000AB30C File Offset: 0x000A950C
			public bool HasGuild_member_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600271C RID: 10012 RVA: 0x000AB31C File Offset: 0x000A951C
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
						this.guild_member_info = this.deserialize.read_map<long, guild_member_info>((guild_member_info v) => v.characterId);
					}
				}
			}

			// Token: 0x0600271D RID: 10013 RVA: 0x000AB394 File Offset: 0x000A9594
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<long, guild_member_info>(this.guild_member_info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D1F RID: 7455
			private static int max_field_count = 1;

			// Token: 0x04001D20 RID: 7456
			private Dictionary<long, guild_member_info> _guild_member_info;
		}
	}
}
