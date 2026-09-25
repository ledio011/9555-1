using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005A9 RID: 1449
	public class ret_update_guild_star
	{
		// Token: 0x020005AA RID: 1450
		public class request : SprotoTypeBase
		{
			// Token: 0x060029D9 RID: 10713 RVA: 0x000B0A3C File Offset: 0x000AEC3C
			public request() : base(ret_update_guild_star.request.max_field_count)
			{
			}

			// Token: 0x060029DA RID: 10714 RVA: 0x000B0A4C File Offset: 0x000AEC4C
			public request(byte[] buffer) : base(ret_update_guild_star.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BEB RID: 3051
			// (get) Token: 0x060029DC RID: 10716 RVA: 0x000B0A68 File Offset: 0x000AEC68
			// (set) Token: 0x060029DD RID: 10717 RVA: 0x000B0A70 File Offset: 0x000AEC70
			public Dictionary<string, guild_star> guild_stars
			{
				get
				{
					return this._guild_stars;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guild_stars = value;
				}
			}

			// Token: 0x17000BEC RID: 3052
			// (get) Token: 0x060029DE RID: 10718 RVA: 0x000B0A88 File Offset: 0x000AEC88
			public bool HasGuild_stars
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060029DF RID: 10719 RVA: 0x000B0A98 File Offset: 0x000AEC98
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
						this.guild_stars = this.deserialize.read_map<string, guild_star>((guild_star v) => v.ID);
					}
				}
			}

			// Token: 0x060029E0 RID: 10720 RVA: 0x000B0B10 File Offset: 0x000AED10
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, guild_star>(this.guild_stars, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DE8 RID: 7656
			private static int max_field_count = 1;

			// Token: 0x04001DE9 RID: 7657
			private Dictionary<string, guild_star> _guild_stars;
		}
	}
}
