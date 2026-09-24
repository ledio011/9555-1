using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005DD RID: 1501
	public class single_copy_scene_npc_die
	{
		// Token: 0x020005DE RID: 1502
		public class request : SprotoTypeBase
		{
			// Token: 0x06002B3B RID: 11067 RVA: 0x000B365C File Offset: 0x000B185C
			public request() : base(single_copy_scene_npc_die.request.max_field_count)
			{
			}

			// Token: 0x06002B3C RID: 11068 RVA: 0x000B366C File Offset: 0x000B186C
			public request(byte[] buffer) : base(single_copy_scene_npc_die.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C69 RID: 3177
			// (get) Token: 0x06002B3E RID: 11070 RVA: 0x000B3688 File Offset: 0x000B1888
			// (set) Token: 0x06002B3F RID: 11071 RVA: 0x000B3690 File Offset: 0x000B1890
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

			// Token: 0x17000C6A RID: 3178
			// (get) Token: 0x06002B40 RID: 11072 RVA: 0x000B36A8 File Offset: 0x000B18A8
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C6B RID: 3179
			// (get) Token: 0x06002B41 RID: 11073 RVA: 0x000B36B8 File Offset: 0x000B18B8
			// (set) Token: 0x06002B42 RID: 11074 RVA: 0x000B36C0 File Offset: 0x000B18C0
			public string npcdataid
			{
				get
				{
					return this._npcdataid;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._npcdataid = value;
				}
			}

			// Token: 0x17000C6C RID: 3180
			// (get) Token: 0x06002B43 RID: 11075 RVA: 0x000B36D8 File Offset: 0x000B18D8
			public bool HasNpcdataid
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000C6D RID: 3181
			// (get) Token: 0x06002B44 RID: 11076 RVA: 0x000B36E8 File Offset: 0x000B18E8
			// (set) Token: 0x06002B45 RID: 11077 RVA: 0x000B36F0 File Offset: 0x000B18F0
			public long pos_x
			{
				get
				{
					return this._pos_x;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._pos_x = value;
				}
			}

			// Token: 0x17000C6E RID: 3182
			// (get) Token: 0x06002B46 RID: 11078 RVA: 0x000B3708 File Offset: 0x000B1908
			public bool HasPos_x
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000C6F RID: 3183
			// (get) Token: 0x06002B47 RID: 11079 RVA: 0x000B3718 File Offset: 0x000B1918
			// (set) Token: 0x06002B48 RID: 11080 RVA: 0x000B3720 File Offset: 0x000B1920
			public long pos_z
			{
				get
				{
					return this._pos_z;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._pos_z = value;
				}
			}

			// Token: 0x17000C70 RID: 3184
			// (get) Token: 0x06002B49 RID: 11081 RVA: 0x000B3738 File Offset: 0x000B1938
			public bool HasPos_z
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000C71 RID: 3185
			// (get) Token: 0x06002B4A RID: 11082 RVA: 0x000B3748 File Offset: 0x000B1948
			// (set) Token: 0x06002B4B RID: 11083 RVA: 0x000B3750 File Offset: 0x000B1950
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._type = value;
				}
			}

			// Token: 0x17000C72 RID: 3186
			// (get) Token: 0x06002B4C RID: 11084 RVA: 0x000B3768 File Offset: 0x000B1968
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x06002B4D RID: 11085 RVA: 0x000B3778 File Offset: 0x000B1978
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.characterId = this.deserialize.read_integer();
						break;
					case 1:
						this.npcdataid = this.deserialize.read_string();
						break;
					case 2:
						this.pos_x = this.deserialize.read_integer();
						break;
					case 3:
						this.pos_z = this.deserialize.read_integer();
						break;
					case 4:
						this.type = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002B4E RID: 11086 RVA: 0x000B3840 File Offset: 0x000B1A40
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.npcdataid, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.pos_x, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.pos_z, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.type, 4);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E44 RID: 7748
			private static int max_field_count = 5;

			// Token: 0x04001E45 RID: 7749
			private long _characterId;

			// Token: 0x04001E46 RID: 7750
			private string _npcdataid;

			// Token: 0x04001E47 RID: 7751
			private long _pos_x;

			// Token: 0x04001E48 RID: 7752
			private long _pos_z;

			// Token: 0x04001E49 RID: 7753
			private long _type;
		}
	}
}
