using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000527 RID: 1319
	public class ret_guild_battle_rank
	{
		// Token: 0x02000528 RID: 1320
		public class request : SprotoTypeBase
		{
			// Token: 0x0600267F RID: 9855 RVA: 0x000A9FEC File Offset: 0x000A81EC
			public request() : base(ret_guild_battle_rank.request.max_field_count)
			{
			}

			// Token: 0x06002680 RID: 9856 RVA: 0x000A9FFC File Offset: 0x000A81FC
			public request(byte[] buffer) : base(ret_guild_battle_rank.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AC7 RID: 2759
			// (get) Token: 0x06002682 RID: 9858 RVA: 0x000AA018 File Offset: 0x000A8218
			// (set) Token: 0x06002683 RID: 9859 RVA: 0x000AA020 File Offset: 0x000A8220
			public List<guild_info> guild_info
			{
				get
				{
					return this._guild_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guild_info = value;
				}
			}

			// Token: 0x17000AC8 RID: 2760
			// (get) Token: 0x06002684 RID: 9860 RVA: 0x000AA038 File Offset: 0x000A8238
			public bool HasGuild_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002685 RID: 9861 RVA: 0x000AA048 File Offset: 0x000A8248
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
						this.guild_info = this.deserialize.read_obj_list<guild_info>();
					}
				}
			}

			// Token: 0x06002686 RID: 9862 RVA: 0x000AA0A4 File Offset: 0x000A82A4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<guild_info>(this.guild_info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CF7 RID: 7415
			private static int max_field_count = 1;

			// Token: 0x04001CF8 RID: 7416
			private List<guild_info> _guild_info;
		}
	}
}
