using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000525 RID: 1317
	public class ret_guild_battle_member
	{
		// Token: 0x02000526 RID: 1318
		public class request : SprotoTypeBase
		{
			// Token: 0x06002676 RID: 9846 RVA: 0x000A9EE4 File Offset: 0x000A80E4
			public request() : base(ret_guild_battle_member.request.max_field_count)
			{
			}

			// Token: 0x06002677 RID: 9847 RVA: 0x000A9EF4 File Offset: 0x000A80F4
			public request(byte[] buffer) : base(ret_guild_battle_member.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AC5 RID: 2757
			// (get) Token: 0x06002679 RID: 9849 RVA: 0x000A9F10 File Offset: 0x000A8110
			// (set) Token: 0x0600267A RID: 9850 RVA: 0x000A9F18 File Offset: 0x000A8118
			public List<guild_member_info> guild_member_info
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

			// Token: 0x17000AC6 RID: 2758
			// (get) Token: 0x0600267B RID: 9851 RVA: 0x000A9F30 File Offset: 0x000A8130
			public bool HasGuild_member_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600267C RID: 9852 RVA: 0x000A9F40 File Offset: 0x000A8140
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
						this.guild_member_info = this.deserialize.read_obj_list<guild_member_info>();
					}
				}
			}

			// Token: 0x0600267D RID: 9853 RVA: 0x000A9F9C File Offset: 0x000A819C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<guild_member_info>(this.guild_member_info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CF5 RID: 7413
			private static int max_field_count = 1;

			// Token: 0x04001CF6 RID: 7414
			private List<guild_member_info> _guild_member_info;
		}
	}
}
