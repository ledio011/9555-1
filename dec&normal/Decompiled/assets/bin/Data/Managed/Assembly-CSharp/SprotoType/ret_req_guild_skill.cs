using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200055F RID: 1375
	public class ret_req_guild_skill
	{
		// Token: 0x02000560 RID: 1376
		public class request : SprotoTypeBase
		{
			// Token: 0x060027EE RID: 10222 RVA: 0x000ACD54 File Offset: 0x000AAF54
			public request() : base(ret_req_guild_skill.request.max_field_count)
			{
			}

			// Token: 0x060027EF RID: 10223 RVA: 0x000ACD64 File Offset: 0x000AAF64
			public request(byte[] buffer) : base(ret_req_guild_skill.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B45 RID: 2885
			// (get) Token: 0x060027F1 RID: 10225 RVA: 0x000ACD80 File Offset: 0x000AAF80
			// (set) Token: 0x060027F2 RID: 10226 RVA: 0x000ACD88 File Offset: 0x000AAF88
			public Dictionary<long, guild_skill> guild_skill
			{
				get
				{
					return this._guild_skill;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guild_skill = value;
				}
			}

			// Token: 0x17000B46 RID: 2886
			// (get) Token: 0x060027F3 RID: 10227 RVA: 0x000ACDA0 File Offset: 0x000AAFA0
			public bool HasGuild_skill
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060027F4 RID: 10228 RVA: 0x000ACDB0 File Offset: 0x000AAFB0
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
						this.guild_skill = this.deserialize.read_map<long, guild_skill>((guild_skill v) => v.skillType);
					}
				}
			}

			// Token: 0x060027F5 RID: 10229 RVA: 0x000ACE28 File Offset: 0x000AB028
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<long, guild_skill>(this.guild_skill, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D5C RID: 7516
			private static int max_field_count = 1;

			// Token: 0x04001D5D RID: 7517
			private Dictionary<long, guild_skill> _guild_skill;
		}
	}
}
