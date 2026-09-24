using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000648 RID: 1608
	public class update_player_map_info
	{
		// Token: 0x02000649 RID: 1609
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E94 RID: 11924 RVA: 0x000BA2C8 File Offset: 0x000B84C8
			public request() : base(update_player_map_info.request.max_field_count)
			{
			}

			// Token: 0x06002E95 RID: 11925 RVA: 0x000BA2D8 File Offset: 0x000B84D8
			public request(byte[] buffer) : base(update_player_map_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DB5 RID: 3509
			// (get) Token: 0x06002E97 RID: 11927 RVA: 0x000BA2F4 File Offset: 0x000B84F4
			// (set) Token: 0x06002E98 RID: 11928 RVA: 0x000BA2FC File Offset: 0x000B84FC
			public long characterId
			{
				get
				{
					return this._characterId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._characterId = value;
				}
			}

			// Token: 0x17000DB6 RID: 3510
			// (get) Token: 0x06002E99 RID: 11929 RVA: 0x000BA314 File Offset: 0x000B8514
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002E9A RID: 11930 RVA: 0x000BA324 File Offset: 0x000B8524
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
						this.characterId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002E9B RID: 11931 RVA: 0x000BA380 File Offset: 0x000B8580
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F32 RID: 7986
			private static int max_field_count = 1;

			// Token: 0x04001F33 RID: 7987
			private long _characterId;
		}

		// Token: 0x0200064A RID: 1610
		public class response : SprotoTypeBase
		{
			// Token: 0x06002E9C RID: 11932 RVA: 0x000BA3C8 File Offset: 0x000B85C8
			public response() : base(update_player_map_info.response.max_field_count)
			{
			}

			// Token: 0x06002E9D RID: 11933 RVA: 0x000BA3D8 File Offset: 0x000B85D8
			public response(byte[] buffer) : base(update_player_map_info.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DB7 RID: 3511
			// (get) Token: 0x06002E9F RID: 11935 RVA: 0x000BA3F4 File Offset: 0x000B85F4
			// (set) Token: 0x06002EA0 RID: 11936 RVA: 0x000BA3FC File Offset: 0x000B85FC
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

			// Token: 0x17000DB8 RID: 3512
			// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x000BA414 File Offset: 0x000B8614
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000DB9 RID: 3513
			// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x000BA424 File Offset: 0x000B8624
			// (set) Token: 0x06002EA3 RID: 11939 RVA: 0x000BA42C File Offset: 0x000B862C
			public string mapid
			{
				get
				{
					return this._mapid;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._mapid = value;
				}
			}

			// Token: 0x17000DBA RID: 3514
			// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x000BA444 File Offset: 0x000B8644
			public bool HasMapid
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000DBB RID: 3515
			// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x000BA454 File Offset: 0x000B8654
			// (set) Token: 0x06002EA6 RID: 11942 RVA: 0x000BA45C File Offset: 0x000B865C
			public position pos
			{
				get
				{
					return this._pos;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._pos = value;
				}
			}

			// Token: 0x17000DBC RID: 3516
			// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x000BA474 File Offset: 0x000B8674
			public bool HasPos
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002EA8 RID: 11944 RVA: 0x000BA484 File Offset: 0x000B8684
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
						this.mapid = this.deserialize.read_string();
						break;
					case 2:
						this.pos = this.deserialize.read_obj<position>();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002EA9 RID: 11945 RVA: 0x000BA518 File Offset: 0x000B8718
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.mapid, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_obj(this.pos, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F34 RID: 7988
			private static int max_field_count = 3;

			// Token: 0x04001F35 RID: 7989
			private long _state;

			// Token: 0x04001F36 RID: 7990
			private string _mapid;

			// Token: 0x04001F37 RID: 7991
			private position _pos;
		}
	}
}
