using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003D6 RID: 982
	public class guild_create
	{
		// Token: 0x020003D7 RID: 983
		public class request : SprotoTypeBase
		{
			// Token: 0x06001E31 RID: 7729 RVA: 0x00099E34 File Offset: 0x00098034
			public request() : base(guild_create.request.max_field_count)
			{
			}

			// Token: 0x06001E32 RID: 7730 RVA: 0x00099E44 File Offset: 0x00098044
			public request(byte[] buffer) : base(guild_create.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000801 RID: 2049
			// (get) Token: 0x06001E34 RID: 7732 RVA: 0x00099E60 File Offset: 0x00098060
			// (set) Token: 0x06001E35 RID: 7733 RVA: 0x00099E68 File Offset: 0x00098068
			public string guildName
			{
				get
				{
					return this._guildName;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guildName = value;
				}
			}

			// Token: 0x17000802 RID: 2050
			// (get) Token: 0x06001E36 RID: 7734 RVA: 0x00099E80 File Offset: 0x00098080
			public bool HasGuildName
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000803 RID: 2051
			// (get) Token: 0x06001E37 RID: 7735 RVA: 0x00099E90 File Offset: 0x00098090
			// (set) Token: 0x06001E38 RID: 7736 RVA: 0x00099E98 File Offset: 0x00098098
			public long Icon
			{
				get
				{
					return this._Icon;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._Icon = value;
				}
			}

			// Token: 0x17000804 RID: 2052
			// (get) Token: 0x06001E39 RID: 7737 RVA: 0x00099EB0 File Offset: 0x000980B0
			public bool HasIcon
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000805 RID: 2053
			// (get) Token: 0x06001E3A RID: 7738 RVA: 0x00099EC0 File Offset: 0x000980C0
			// (set) Token: 0x06001E3B RID: 7739 RVA: 0x00099EC8 File Offset: 0x000980C8
			public string notice
			{
				get
				{
					return this._notice;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._notice = value;
				}
			}

			// Token: 0x17000806 RID: 2054
			// (get) Token: 0x06001E3C RID: 7740 RVA: 0x00099EE0 File Offset: 0x000980E0
			public bool HasNotice
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000807 RID: 2055
			// (get) Token: 0x06001E3D RID: 7741 RVA: 0x00099EF0 File Offset: 0x000980F0
			// (set) Token: 0x06001E3E RID: 7742 RVA: 0x00099EF8 File Offset: 0x000980F8
			public long costType
			{
				get
				{
					return this._costType;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._costType = value;
				}
			}

			// Token: 0x17000808 RID: 2056
			// (get) Token: 0x06001E3F RID: 7743 RVA: 0x00099F10 File Offset: 0x00098110
			public bool HasCostType
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06001E40 RID: 7744 RVA: 0x00099F20 File Offset: 0x00098120
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.guildName = this.deserialize.read_string();
						break;
					case 1:
						this.Icon = this.deserialize.read_integer();
						break;
					case 2:
						this.notice = this.deserialize.read_string();
						break;
					case 3:
						this.costType = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001E41 RID: 7745 RVA: 0x00099FCC File Offset: 0x000981CC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.guildName, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.Icon, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.notice, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.costType, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001AD9 RID: 6873
			private static int max_field_count = 4;

			// Token: 0x04001ADA RID: 6874
			private string _guildName;

			// Token: 0x04001ADB RID: 6875
			private long _Icon;

			// Token: 0x04001ADC RID: 6876
			private string _notice;

			// Token: 0x04001ADD RID: 6877
			private long _costType;
		}
	}
}
