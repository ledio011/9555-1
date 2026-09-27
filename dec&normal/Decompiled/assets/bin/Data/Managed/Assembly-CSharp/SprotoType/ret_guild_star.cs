using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000547 RID: 1351
	public class ret_guild_star
	{
		// Token: 0x02000548 RID: 1352
		public class request : SprotoTypeBase
		{
			// Token: 0x06002767 RID: 10087 RVA: 0x000ABD0C File Offset: 0x000A9F0C
			public request() : base(ret_guild_star.request.max_field_count)
			{
			}

			// Token: 0x06002768 RID: 10088 RVA: 0x000ABD1C File Offset: 0x000A9F1C
			public request(byte[] buffer) : base(ret_guild_star.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B1F RID: 2847
			// (get) Token: 0x0600276A RID: 10090 RVA: 0x000ABD38 File Offset: 0x000A9F38
			// (set) Token: 0x0600276B RID: 10091 RVA: 0x000ABD40 File Offset: 0x000A9F40
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

			// Token: 0x17000B20 RID: 2848
			// (get) Token: 0x0600276C RID: 10092 RVA: 0x000ABD58 File Offset: 0x000A9F58
			public bool HasGuild_stars
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600276D RID: 10093 RVA: 0x000ABD68 File Offset: 0x000A9F68
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

			// Token: 0x0600276E RID: 10094 RVA: 0x000ABDE0 File Offset: 0x000A9FE0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, guild_star>(this.guild_stars, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D37 RID: 7479
			private static int max_field_count = 1;

			// Token: 0x04001D38 RID: 7480
			private Dictionary<string, guild_star> _guild_stars;
		}
	}
}
