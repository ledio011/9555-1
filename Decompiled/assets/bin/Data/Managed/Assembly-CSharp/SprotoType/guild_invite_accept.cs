using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003DD RID: 989
	public class guild_invite_accept
	{
		// Token: 0x020003DE RID: 990
		public class request : SprotoTypeBase
		{
			// Token: 0x06001E9C RID: 7836 RVA: 0x0009ACA8 File Offset: 0x00098EA8
			public request() : base(guild_invite_accept.request.max_field_count)
			{
			}

			// Token: 0x06001E9D RID: 7837 RVA: 0x0009ACB8 File Offset: 0x00098EB8
			public request(byte[] buffer) : base(guild_invite_accept.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000839 RID: 2105
			// (get) Token: 0x06001E9F RID: 7839 RVA: 0x0009ACD4 File Offset: 0x00098ED4
			// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x0009ACDC File Offset: 0x00098EDC
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

			// Token: 0x1700083A RID: 2106
			// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x0009ACF4 File Offset: 0x00098EF4
			public bool HasName
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700083B RID: 2107
			// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x0009AD04 File Offset: 0x00098F04
			// (set) Token: 0x06001EA3 RID: 7843 RVA: 0x0009AD0C File Offset: 0x00098F0C
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

			// Token: 0x1700083C RID: 2108
			// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x0009AD24 File Offset: 0x00098F24
			public bool HasGuildId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x1700083D RID: 2109
			// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x0009AD34 File Offset: 0x00098F34
			// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x0009AD3C File Offset: 0x00098F3C
			public string guildName
			{
				get
				{
					return this._guildName;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._guildName = value;
				}
			}

			// Token: 0x1700083E RID: 2110
			// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x0009AD54 File Offset: 0x00098F54
			public bool HasGuildName
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06001EA8 RID: 7848 RVA: 0x0009AD64 File Offset: 0x00098F64
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
						this.guildName = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001EA9 RID: 7849 RVA: 0x0009ADF8 File Offset: 0x00098FF8
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
					this.serialize.write_string(this.guildName, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001AF9 RID: 6905
			private static int max_field_count = 3;

			// Token: 0x04001AFA RID: 6906
			private string _name;

			// Token: 0x04001AFB RID: 6907
			private long _guildId;

			// Token: 0x04001AFC RID: 6908
			private string _guildName;
		}
	}
}
