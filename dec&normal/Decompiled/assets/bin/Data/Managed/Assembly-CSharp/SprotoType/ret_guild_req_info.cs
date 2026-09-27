using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200053F RID: 1343
	public class ret_guild_req_info
	{
		// Token: 0x02000540 RID: 1344
		public class request : SprotoTypeBase
		{
			// Token: 0x06002720 RID: 10016 RVA: 0x000AB3EC File Offset: 0x000A95EC
			public request() : base(ret_guild_req_info.request.max_field_count)
			{
			}

			// Token: 0x06002721 RID: 10017 RVA: 0x000AB3FC File Offset: 0x000A95FC
			public request(byte[] buffer) : base(ret_guild_req_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B01 RID: 2817
			// (get) Token: 0x06002723 RID: 10019 RVA: 0x000AB418 File Offset: 0x000A9618
			// (set) Token: 0x06002724 RID: 10020 RVA: 0x000AB420 File Offset: 0x000A9620
			public guild_info guild_info
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

			// Token: 0x17000B02 RID: 2818
			// (get) Token: 0x06002725 RID: 10021 RVA: 0x000AB438 File Offset: 0x000A9638
			public bool HasGuild_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B03 RID: 2819
			// (get) Token: 0x06002726 RID: 10022 RVA: 0x000AB448 File Offset: 0x000A9648
			// (set) Token: 0x06002727 RID: 10023 RVA: 0x000AB450 File Offset: 0x000A9650
			public Dictionary<string, donate_record> donate_records
			{
				get
				{
					return this._donate_records;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._donate_records = value;
				}
			}

			// Token: 0x17000B04 RID: 2820
			// (get) Token: 0x06002728 RID: 10024 RVA: 0x000AB468 File Offset: 0x000A9668
			public bool HasDonate_records
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000B05 RID: 2821
			// (get) Token: 0x06002729 RID: 10025 RVA: 0x000AB478 File Offset: 0x000A9678
			// (set) Token: 0x0600272A RID: 10026 RVA: 0x000AB480 File Offset: 0x000A9680
			public long all_contribute
			{
				get
				{
					return this._all_contribute;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._all_contribute = value;
				}
			}

			// Token: 0x17000B06 RID: 2822
			// (get) Token: 0x0600272B RID: 10027 RVA: 0x000AB498 File Offset: 0x000A9698
			public bool HasAll_contribute
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000B07 RID: 2823
			// (get) Token: 0x0600272C RID: 10028 RVA: 0x000AB4A8 File Offset: 0x000A96A8
			// (set) Token: 0x0600272D RID: 10029 RVA: 0x000AB4B0 File Offset: 0x000A96B0
			public bool exist
			{
				get
				{
					return this._exist;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._exist = value;
				}
			}

			// Token: 0x17000B08 RID: 2824
			// (get) Token: 0x0600272E RID: 10030 RVA: 0x000AB4C8 File Offset: 0x000A96C8
			public bool HasExist
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000B09 RID: 2825
			// (get) Token: 0x0600272F RID: 10031 RVA: 0x000AB4D8 File Offset: 0x000A96D8
			// (set) Token: 0x06002730 RID: 10032 RVA: 0x000AB4E0 File Offset: 0x000A96E0
			public long contribute
			{
				get
				{
					return this._contribute;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._contribute = value;
				}
			}

			// Token: 0x17000B0A RID: 2826
			// (get) Token: 0x06002731 RID: 10033 RVA: 0x000AB4F8 File Offset: 0x000A96F8
			public bool HasContribute
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x06002732 RID: 10034 RVA: 0x000AB508 File Offset: 0x000A9708
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.guild_info = this.deserialize.read_obj<guild_info>();
						break;
					case 1:
						this.donate_records = this.deserialize.read_map<string, donate_record>((donate_record v) => v.id);
						break;
					case 2:
						this.all_contribute = this.deserialize.read_integer();
						break;
					case 3:
						this.exist = this.deserialize.read_boolean();
						break;
					case 4:
						this.contribute = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002733 RID: 10035 RVA: 0x000AB5EC File Offset: 0x000A97EC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.guild_info, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<string, donate_record>(this.donate_records, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.all_contribute, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_boolean(this.exist, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.contribute, 4);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D22 RID: 7458
			private static int max_field_count = 5;

			// Token: 0x04001D23 RID: 7459
			private guild_info _guild_info;

			// Token: 0x04001D24 RID: 7460
			private Dictionary<string, donate_record> _donate_records;

			// Token: 0x04001D25 RID: 7461
			private long _all_contribute;

			// Token: 0x04001D26 RID: 7462
			private bool _exist;

			// Token: 0x04001D27 RID: 7463
			private long _contribute;
		}
	}
}
