using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000541 RID: 1345
	public class ret_guild_req_list
	{
		// Token: 0x02000542 RID: 1346
		public class request : SprotoTypeBase
		{
			// Token: 0x06002736 RID: 10038 RVA: 0x000AB6D0 File Offset: 0x000A98D0
			public request() : base(ret_guild_req_list.request.max_field_count)
			{
			}

			// Token: 0x06002737 RID: 10039 RVA: 0x000AB6E0 File Offset: 0x000A98E0
			public request(byte[] buffer) : base(ret_guild_req_list.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B0B RID: 2827
			// (get) Token: 0x06002739 RID: 10041 RVA: 0x000AB6FC File Offset: 0x000A98FC
			// (set) Token: 0x0600273A RID: 10042 RVA: 0x000AB704 File Offset: 0x000A9904
			public Dictionary<long, guild_info> guild_info
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

			// Token: 0x17000B0C RID: 2828
			// (get) Token: 0x0600273B RID: 10043 RVA: 0x000AB71C File Offset: 0x000A991C
			public bool HasGuild_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B0D RID: 2829
			// (get) Token: 0x0600273C RID: 10044 RVA: 0x000AB72C File Offset: 0x000A992C
			// (set) Token: 0x0600273D RID: 10045 RVA: 0x000AB734 File Offset: 0x000A9934
			public List<long> applyGuildId
			{
				get
				{
					return this._applyGuildId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._applyGuildId = value;
				}
			}

			// Token: 0x17000B0E RID: 2830
			// (get) Token: 0x0600273E RID: 10046 RVA: 0x000AB74C File Offset: 0x000A994C
			public bool HasApplyGuildId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000B0F RID: 2831
			// (get) Token: 0x0600273F RID: 10047 RVA: 0x000AB75C File Offset: 0x000A995C
			// (set) Token: 0x06002740 RID: 10048 RVA: 0x000AB764 File Offset: 0x000A9964
			public long curPage
			{
				get
				{
					return this._curPage;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._curPage = value;
				}
			}

			// Token: 0x17000B10 RID: 2832
			// (get) Token: 0x06002741 RID: 10049 RVA: 0x000AB77C File Offset: 0x000A997C
			public bool HasCurPage
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000B11 RID: 2833
			// (get) Token: 0x06002742 RID: 10050 RVA: 0x000AB78C File Offset: 0x000A998C
			// (set) Token: 0x06002743 RID: 10051 RVA: 0x000AB794 File Offset: 0x000A9994
			public long maxPage
			{
				get
				{
					return this._maxPage;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._maxPage = value;
				}
			}

			// Token: 0x17000B12 RID: 2834
			// (get) Token: 0x06002744 RID: 10052 RVA: 0x000AB7AC File Offset: 0x000A99AC
			public bool HasMaxPage
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000B13 RID: 2835
			// (get) Token: 0x06002745 RID: 10053 RVA: 0x000AB7BC File Offset: 0x000A99BC
			// (set) Token: 0x06002746 RID: 10054 RVA: 0x000AB7C4 File Offset: 0x000A99C4
			public long leave_time
			{
				get
				{
					return this._leave_time;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._leave_time = value;
				}
			}

			// Token: 0x17000B14 RID: 2836
			// (get) Token: 0x06002747 RID: 10055 RVA: 0x000AB7DC File Offset: 0x000A99DC
			public bool HasLeave_time
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x06002748 RID: 10056 RVA: 0x000AB7EC File Offset: 0x000A99EC
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.guild_info = this.deserialize.read_map<long, guild_info>((guild_info v) => v.guildId);
						break;
					case 1:
						this.applyGuildId = this.deserialize.read_integer_list();
						break;
					case 2:
						this.curPage = this.deserialize.read_integer();
						break;
					case 3:
						this.maxPage = this.deserialize.read_integer();
						break;
					case 4:
						this.leave_time = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002749 RID: 10057 RVA: 0x000AB8D0 File Offset: 0x000A9AD0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<long, guild_info>(this.guild_info, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.applyGuildId, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.curPage, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.maxPage, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.leave_time, 4);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D29 RID: 7465
			private static int max_field_count = 5;

			// Token: 0x04001D2A RID: 7466
			private Dictionary<long, guild_info> _guild_info;

			// Token: 0x04001D2B RID: 7467
			private List<long> _applyGuildId;

			// Token: 0x04001D2C RID: 7468
			private long _curPage;

			// Token: 0x04001D2D RID: 7469
			private long _maxPage;

			// Token: 0x04001D2E RID: 7470
			private long _leave_time;
		}
	}
}
