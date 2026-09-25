using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000571 RID: 1393
	public class ret_request_guild_map_info
	{
		// Token: 0x02000572 RID: 1394
		public class request : SprotoTypeBase
		{
			// Token: 0x0600287E RID: 10366 RVA: 0x000ADFA8 File Offset: 0x000AC1A8
			public request() : base(ret_request_guild_map_info.request.max_field_count)
			{
			}

			// Token: 0x0600287F RID: 10367 RVA: 0x000ADFB8 File Offset: 0x000AC1B8
			public request(byte[] buffer) : base(ret_request_guild_map_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B7B RID: 2939
			// (get) Token: 0x06002881 RID: 10369 RVA: 0x000ADFD4 File Offset: 0x000AC1D4
			// (set) Token: 0x06002882 RID: 10370 RVA: 0x000ADFDC File Offset: 0x000AC1DC
			public Dictionary<string, guild_map_info> guild_map_info
			{
				get
				{
					return this._guild_map_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guild_map_info = value;
				}
			}

			// Token: 0x17000B7C RID: 2940
			// (get) Token: 0x06002883 RID: 10371 RVA: 0x000ADFF4 File Offset: 0x000AC1F4
			public bool HasGuild_map_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002884 RID: 10372 RVA: 0x000AE004 File Offset: 0x000AC204
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
						this.guild_map_info = this.deserialize.read_map<string, guild_map_info>((guild_map_info v) => v.id);
					}
				}
			}

			// Token: 0x06002885 RID: 10373 RVA: 0x000AE07C File Offset: 0x000AC27C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, guild_map_info>(this.guild_map_info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D89 RID: 7561
			private static int max_field_count = 1;

			// Token: 0x04001D8A RID: 7562
			private Dictionary<string, guild_map_info> _guild_map_info;
		}
	}
}
