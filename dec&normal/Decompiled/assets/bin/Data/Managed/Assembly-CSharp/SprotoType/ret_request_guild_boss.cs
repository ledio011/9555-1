using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200056F RID: 1391
	public class ret_request_guild_boss
	{
		// Token: 0x02000570 RID: 1392
		public class request : SprotoTypeBase
		{
			// Token: 0x06002867 RID: 10343 RVA: 0x000ADCA0 File Offset: 0x000ABEA0
			public request() : base(ret_request_guild_boss.request.max_field_count)
			{
			}

			// Token: 0x06002868 RID: 10344 RVA: 0x000ADCB0 File Offset: 0x000ABEB0
			public request(byte[] buffer) : base(ret_request_guild_boss.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B71 RID: 2929
			// (get) Token: 0x0600286A RID: 10346 RVA: 0x000ADCCC File Offset: 0x000ABECC
			// (set) Token: 0x0600286B RID: 10347 RVA: 0x000ADCD4 File Offset: 0x000ABED4
			public Dictionary<string, guild_boss> guild_boss
			{
				get
				{
					return this._guild_boss;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guild_boss = value;
				}
			}

			// Token: 0x17000B72 RID: 2930
			// (get) Token: 0x0600286C RID: 10348 RVA: 0x000ADCEC File Offset: 0x000ABEEC
			public bool HasGuild_boss
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B73 RID: 2931
			// (get) Token: 0x0600286D RID: 10349 RVA: 0x000ADCFC File Offset: 0x000ABEFC
			// (set) Token: 0x0600286E RID: 10350 RVA: 0x000ADD04 File Offset: 0x000ABF04
			public guild_battle_info guild_battle_info
			{
				get
				{
					return this._guild_battle_info;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._guild_battle_info = value;
				}
			}

			// Token: 0x17000B74 RID: 2932
			// (get) Token: 0x0600286F RID: 10351 RVA: 0x000ADD1C File Offset: 0x000ABF1C
			public bool HasGuild_battle_info
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000B75 RID: 2933
			// (get) Token: 0x06002870 RID: 10352 RVA: 0x000ADD2C File Offset: 0x000ABF2C
			// (set) Token: 0x06002871 RID: 10353 RVA: 0x000ADD34 File Offset: 0x000ABF34
			public long level
			{
				get
				{
					return this._level;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._level = value;
				}
			}

			// Token: 0x17000B76 RID: 2934
			// (get) Token: 0x06002872 RID: 10354 RVA: 0x000ADD4C File Offset: 0x000ABF4C
			public bool HasLevel
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000B77 RID: 2935
			// (get) Token: 0x06002873 RID: 10355 RVA: 0x000ADD5C File Offset: 0x000ABF5C
			// (set) Token: 0x06002874 RID: 10356 RVA: 0x000ADD64 File Offset: 0x000ABF64
			public dance_state_info dance_state_info
			{
				get
				{
					return this._dance_state_info;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._dance_state_info = value;
				}
			}

			// Token: 0x17000B78 RID: 2936
			// (get) Token: 0x06002875 RID: 10357 RVA: 0x000ADD7C File Offset: 0x000ABF7C
			public bool HasDance_state_info
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000B79 RID: 2937
			// (get) Token: 0x06002876 RID: 10358 RVA: 0x000ADD8C File Offset: 0x000ABF8C
			// (set) Token: 0x06002877 RID: 10359 RVA: 0x000ADD94 File Offset: 0x000ABF94
			public Dictionary<string, guild_map_info> guild_map_info
			{
				get
				{
					return this._guild_map_info;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._guild_map_info = value;
				}
			}

			// Token: 0x17000B7A RID: 2938
			// (get) Token: 0x06002878 RID: 10360 RVA: 0x000ADDAC File Offset: 0x000ABFAC
			public bool HasGuild_map_info
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x06002879 RID: 10361 RVA: 0x000ADDBC File Offset: 0x000ABFBC
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.guild_boss = this.deserialize.read_map<string, guild_boss>((guild_boss v) => v.id);
						break;
					case 1:
						this.guild_battle_info = this.deserialize.read_obj<guild_battle_info>();
						break;
					case 2:
						this.level = this.deserialize.read_integer();
						break;
					case 3:
						this.dance_state_info = this.deserialize.read_obj<dance_state_info>();
						break;
					case 4:
						this.guild_map_info = this.deserialize.read_map<string, guild_map_info>((guild_map_info v) => v.id);
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x0600287A RID: 10362 RVA: 0x000ADEBC File Offset: 0x000AC0BC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, guild_boss>(this.guild_boss, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj(this.guild_battle_info, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.level, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_obj(this.dance_state_info, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_obj<string, guild_map_info>(this.guild_map_info, 4);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D81 RID: 7553
			private static int max_field_count = 5;

			// Token: 0x04001D82 RID: 7554
			private Dictionary<string, guild_boss> _guild_boss;

			// Token: 0x04001D83 RID: 7555
			private guild_battle_info _guild_battle_info;

			// Token: 0x04001D84 RID: 7556
			private long _level;

			// Token: 0x04001D85 RID: 7557
			private dance_state_info _dance_state_info;

			// Token: 0x04001D86 RID: 7558
			private Dictionary<string, guild_map_info> _guild_map_info;
		}
	}
}
