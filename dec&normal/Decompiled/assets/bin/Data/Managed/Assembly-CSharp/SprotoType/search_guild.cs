using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005B8 RID: 1464
	public class search_guild
	{
		// Token: 0x020005B9 RID: 1465
		public class request : SprotoTypeBase
		{
			// Token: 0x06002A4C RID: 10828 RVA: 0x000B18C8 File Offset: 0x000AFAC8
			public request() : base(search_guild.request.max_field_count)
			{
			}

			// Token: 0x06002A4D RID: 10829 RVA: 0x000B18D8 File Offset: 0x000AFAD8
			public request(byte[] buffer) : base(search_guild.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C15 RID: 3093
			// (get) Token: 0x06002A4F RID: 10831 RVA: 0x000B18F4 File Offset: 0x000AFAF4
			// (set) Token: 0x06002A50 RID: 10832 RVA: 0x000B18FC File Offset: 0x000AFAFC
			public string name
			{
				get
				{
					return this._name;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._name = value;
				}
			}

			// Token: 0x17000C16 RID: 3094
			// (get) Token: 0x06002A51 RID: 10833 RVA: 0x000B1914 File Offset: 0x000AFB14
			public bool HasName
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C17 RID: 3095
			// (get) Token: 0x06002A52 RID: 10834 RVA: 0x000B1924 File Offset: 0x000AFB24
			// (set) Token: 0x06002A53 RID: 10835 RVA: 0x000B192C File Offset: 0x000AFB2C
			public long guildId
			{
				get
				{
					return this._guildId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._guildId = value;
				}
			}

			// Token: 0x17000C18 RID: 3096
			// (get) Token: 0x06002A54 RID: 10836 RVA: 0x000B1944 File Offset: 0x000AFB44
			public bool HasGuildId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000C19 RID: 3097
			// (get) Token: 0x06002A55 RID: 10837 RVA: 0x000B1954 File Offset: 0x000AFB54
			// (set) Token: 0x06002A56 RID: 10838 RVA: 0x000B195C File Offset: 0x000AFB5C
			public long rank
			{
				get
				{
					return this._rank;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._rank = value;
				}
			}

			// Token: 0x17000C1A RID: 3098
			// (get) Token: 0x06002A57 RID: 10839 RVA: 0x000B1974 File Offset: 0x000AFB74
			public bool HasRank
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002A58 RID: 10840 RVA: 0x000B1984 File Offset: 0x000AFB84
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.name = this.deserialize.read_string();
						break;
					case 1:
						this.guildId = this.deserialize.read_integer();
						break;
					case 2:
						this.rank = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002A59 RID: 10841 RVA: 0x000B1A18 File Offset: 0x000AFC18
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.name, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.guildId, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.rank, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E07 RID: 7687
			private static int max_field_count = 3;

			// Token: 0x04001E08 RID: 7688
			private string _name;

			// Token: 0x04001E09 RID: 7689
			private long _guildId;

			// Token: 0x04001E0A RID: 7690
			private long _rank;
		}
	}
}
