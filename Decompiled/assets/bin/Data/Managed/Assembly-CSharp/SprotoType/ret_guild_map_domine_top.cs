using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000539 RID: 1337
	public class ret_guild_map_domine_top
	{
		// Token: 0x0200053A RID: 1338
		public class request : SprotoTypeBase
		{
			// Token: 0x060026F2 RID: 9970 RVA: 0x000AAE1C File Offset: 0x000A901C
			public request() : base(ret_guild_map_domine_top.request.max_field_count)
			{
			}

			// Token: 0x060026F3 RID: 9971 RVA: 0x000AAE2C File Offset: 0x000A902C
			public request(byte[] buffer) : base(ret_guild_map_domine_top.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AEF RID: 2799
			// (get) Token: 0x060026F5 RID: 9973 RVA: 0x000AAE48 File Offset: 0x000A9048
			// (set) Token: 0x060026F6 RID: 9974 RVA: 0x000AAE50 File Offset: 0x000A9050
			public List<damage_list> damage_list
			{
				get
				{
					return this._damage_list;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._damage_list = value;
				}
			}

			// Token: 0x17000AF0 RID: 2800
			// (get) Token: 0x060026F7 RID: 9975 RVA: 0x000AAE68 File Offset: 0x000A9068
			public bool HasDamage_list
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000AF1 RID: 2801
			// (get) Token: 0x060026F8 RID: 9976 RVA: 0x000AAE78 File Offset: 0x000A9078
			// (set) Token: 0x060026F9 RID: 9977 RVA: 0x000AAE80 File Offset: 0x000A9080
			public List<damage_list> guild_damage_list
			{
				get
				{
					return this._guild_damage_list;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._guild_damage_list = value;
				}
			}

			// Token: 0x17000AF2 RID: 2802
			// (get) Token: 0x060026FA RID: 9978 RVA: 0x000AAE98 File Offset: 0x000A9098
			public bool HasGuild_damage_list
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000AF3 RID: 2803
			// (get) Token: 0x060026FB RID: 9979 RVA: 0x000AAEA8 File Offset: 0x000A90A8
			// (set) Token: 0x060026FC RID: 9980 RVA: 0x000AAEB0 File Offset: 0x000A90B0
			public long my_rank
			{
				get
				{
					return this._my_rank;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._my_rank = value;
				}
			}

			// Token: 0x17000AF4 RID: 2804
			// (get) Token: 0x060026FD RID: 9981 RVA: 0x000AAEC8 File Offset: 0x000A90C8
			public bool HasMy_rank
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000AF5 RID: 2805
			// (get) Token: 0x060026FE RID: 9982 RVA: 0x000AAED8 File Offset: 0x000A90D8
			// (set) Token: 0x060026FF RID: 9983 RVA: 0x000AAEE0 File Offset: 0x000A90E0
			public long my_damage
			{
				get
				{
					return this._my_damage;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._my_damage = value;
				}
			}

			// Token: 0x17000AF6 RID: 2806
			// (get) Token: 0x06002700 RID: 9984 RVA: 0x000AAEF8 File Offset: 0x000A90F8
			public bool HasMy_damage
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000AF7 RID: 2807
			// (get) Token: 0x06002701 RID: 9985 RVA: 0x000AAF08 File Offset: 0x000A9108
			// (set) Token: 0x06002702 RID: 9986 RVA: 0x000AAF10 File Offset: 0x000A9110
			public long my_rank2
			{
				get
				{
					return this._my_rank2;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._my_rank2 = value;
				}
			}

			// Token: 0x17000AF8 RID: 2808
			// (get) Token: 0x06002703 RID: 9987 RVA: 0x000AAF28 File Offset: 0x000A9128
			public bool HasMy_rank2
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000AF9 RID: 2809
			// (get) Token: 0x06002704 RID: 9988 RVA: 0x000AAF38 File Offset: 0x000A9138
			// (set) Token: 0x06002705 RID: 9989 RVA: 0x000AAF40 File Offset: 0x000A9140
			public long my_damage2
			{
				get
				{
					return this._my_damage2;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._my_damage2 = value;
				}
			}

			// Token: 0x17000AFA RID: 2810
			// (get) Token: 0x06002706 RID: 9990 RVA: 0x000AAF58 File Offset: 0x000A9158
			public bool HasMy_damage2
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x06002707 RID: 9991 RVA: 0x000AAF68 File Offset: 0x000A9168
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.damage_list = this.deserialize.read_obj_list<damage_list>();
						break;
					case 1:
						this.guild_damage_list = this.deserialize.read_obj_list<damage_list>();
						break;
					case 2:
						this.my_rank = this.deserialize.read_integer();
						break;
					case 3:
						this.my_damage = this.deserialize.read_integer();
						break;
					case 4:
						this.my_rank2 = this.deserialize.read_integer();
						break;
					case 5:
						this.my_damage2 = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002708 RID: 9992 RVA: 0x000AB048 File Offset: 0x000A9248
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<damage_list>(this.damage_list, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<damage_list>(this.guild_damage_list, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.my_rank, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.my_damage, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.my_rank2, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.my_damage2, 5);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D15 RID: 7445
			private static int max_field_count = 6;

			// Token: 0x04001D16 RID: 7446
			private List<damage_list> _damage_list;

			// Token: 0x04001D17 RID: 7447
			private List<damage_list> _guild_damage_list;

			// Token: 0x04001D18 RID: 7448
			private long _my_rank;

			// Token: 0x04001D19 RID: 7449
			private long _my_damage;

			// Token: 0x04001D1A RID: 7450
			private long _my_rank2;

			// Token: 0x04001D1B RID: 7451
			private long _my_damage2;
		}
	}
}
