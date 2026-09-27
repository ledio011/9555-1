using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000545 RID: 1349
	public class ret_guild_skill_level
	{
		// Token: 0x02000546 RID: 1350
		public class request : SprotoTypeBase
		{
			// Token: 0x06002755 RID: 10069 RVA: 0x000ABABC File Offset: 0x000A9CBC
			public request() : base(ret_guild_skill_level.request.max_field_count)
			{
			}

			// Token: 0x06002756 RID: 10070 RVA: 0x000ABACC File Offset: 0x000A9CCC
			public request(byte[] buffer) : base(ret_guild_skill_level.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B17 RID: 2839
			// (get) Token: 0x06002758 RID: 10072 RVA: 0x000ABAE8 File Offset: 0x000A9CE8
			// (set) Token: 0x06002759 RID: 10073 RVA: 0x000ABAF0 File Offset: 0x000A9CF0
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._state = value;
				}
			}

			// Token: 0x17000B18 RID: 2840
			// (get) Token: 0x0600275A RID: 10074 RVA: 0x000ABB08 File Offset: 0x000A9D08
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B19 RID: 2841
			// (get) Token: 0x0600275B RID: 10075 RVA: 0x000ABB18 File Offset: 0x000A9D18
			// (set) Token: 0x0600275C RID: 10076 RVA: 0x000ABB20 File Offset: 0x000A9D20
			public long guildSkillType
			{
				get
				{
					return this._guildSkillType;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._guildSkillType = value;
				}
			}

			// Token: 0x17000B1A RID: 2842
			// (get) Token: 0x0600275D RID: 10077 RVA: 0x000ABB38 File Offset: 0x000A9D38
			public bool HasGuildSkillType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000B1B RID: 2843
			// (get) Token: 0x0600275E RID: 10078 RVA: 0x000ABB48 File Offset: 0x000A9D48
			// (set) Token: 0x0600275F RID: 10079 RVA: 0x000ABB50 File Offset: 0x000A9D50
			public long contribute
			{
				get
				{
					return this._contribute;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._contribute = value;
				}
			}

			// Token: 0x17000B1C RID: 2844
			// (get) Token: 0x06002760 RID: 10080 RVA: 0x000ABB68 File Offset: 0x000A9D68
			public bool HasContribute
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000B1D RID: 2845
			// (get) Token: 0x06002761 RID: 10081 RVA: 0x000ABB78 File Offset: 0x000A9D78
			// (set) Token: 0x06002762 RID: 10082 RVA: 0x000ABB80 File Offset: 0x000A9D80
			public long level
			{
				get
				{
					return this._level;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._level = value;
				}
			}

			// Token: 0x17000B1E RID: 2846
			// (get) Token: 0x06002763 RID: 10083 RVA: 0x000ABB98 File Offset: 0x000A9D98
			public bool HasLevel
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06002764 RID: 10084 RVA: 0x000ABBA8 File Offset: 0x000A9DA8
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.state = this.deserialize.read_integer();
						break;
					case 1:
						this.guildSkillType = this.deserialize.read_integer();
						break;
					case 2:
						this.contribute = this.deserialize.read_integer();
						break;
					case 3:
						this.level = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002765 RID: 10085 RVA: 0x000ABC54 File Offset: 0x000A9E54
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.guildSkillType, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.contribute, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.level, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D32 RID: 7474
			private static int max_field_count = 4;

			// Token: 0x04001D33 RID: 7475
			private long _state;

			// Token: 0x04001D34 RID: 7476
			private long _guildSkillType;

			// Token: 0x04001D35 RID: 7477
			private long _contribute;

			// Token: 0x04001D36 RID: 7478
			private long _level;
		}
	}
}
