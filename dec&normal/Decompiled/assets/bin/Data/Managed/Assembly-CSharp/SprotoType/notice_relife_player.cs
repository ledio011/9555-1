using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200042F RID: 1071
	public class notice_relife_player
	{
		// Token: 0x02000430 RID: 1072
		public class request : SprotoTypeBase
		{
			// Token: 0x06002121 RID: 8481 RVA: 0x0009FE2C File Offset: 0x0009E02C
			public request() : base(notice_relife_player.request.max_field_count)
			{
			}

			// Token: 0x06002122 RID: 8482 RVA: 0x0009FE3C File Offset: 0x0009E03C
			public request(byte[] buffer) : base(notice_relife_player.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700092F RID: 2351
			// (get) Token: 0x06002124 RID: 8484 RVA: 0x0009FE58 File Offset: 0x0009E058
			// (set) Token: 0x06002125 RID: 8485 RVA: 0x0009FE60 File Offset: 0x0009E060
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._type = value;
				}
			}

			// Token: 0x17000930 RID: 2352
			// (get) Token: 0x06002126 RID: 8486 RVA: 0x0009FE78 File Offset: 0x0009E078
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000931 RID: 2353
			// (get) Token: 0x06002127 RID: 8487 RVA: 0x0009FE88 File Offset: 0x0009E088
			// (set) Token: 0x06002128 RID: 8488 RVA: 0x0009FE90 File Offset: 0x0009E090
			public long cost
			{
				get
				{
					return this._cost;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._cost = value;
				}
			}

			// Token: 0x17000932 RID: 2354
			// (get) Token: 0x06002129 RID: 8489 RVA: 0x0009FEA8 File Offset: 0x0009E0A8
			public bool HasCost
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000933 RID: 2355
			// (get) Token: 0x0600212A RID: 8490 RVA: 0x0009FEB8 File Offset: 0x0009E0B8
			// (set) Token: 0x0600212B RID: 8491 RVA: 0x0009FEC0 File Offset: 0x0009E0C0
			public string itemId
			{
				get
				{
					return this._itemId;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._itemId = value;
				}
			}

			// Token: 0x17000934 RID: 2356
			// (get) Token: 0x0600212C RID: 8492 RVA: 0x0009FED8 File Offset: 0x0009E0D8
			public bool HasItemId
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000935 RID: 2357
			// (get) Token: 0x0600212D RID: 8493 RVA: 0x0009FEE8 File Offset: 0x0009E0E8
			// (set) Token: 0x0600212E RID: 8494 RVA: 0x0009FEF0 File Offset: 0x0009E0F0
			public long characterid
			{
				get
				{
					return this._characterid;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._characterid = value;
				}
			}

			// Token: 0x17000936 RID: 2358
			// (get) Token: 0x0600212F RID: 8495 RVA: 0x0009FF08 File Offset: 0x0009E108
			public bool HasCharacterid
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000937 RID: 2359
			// (get) Token: 0x06002130 RID: 8496 RVA: 0x0009FF18 File Offset: 0x0009E118
			// (set) Token: 0x06002131 RID: 8497 RVA: 0x0009FF20 File Offset: 0x0009E120
			public string name
			{
				get
				{
					return this._name;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._name = value;
				}
			}

			// Token: 0x17000938 RID: 2360
			// (get) Token: 0x06002132 RID: 8498 RVA: 0x0009FF38 File Offset: 0x0009E138
			public bool HasName
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x06002133 RID: 8499 RVA: 0x0009FF48 File Offset: 0x0009E148
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.type = this.deserialize.read_integer();
						break;
					case 1:
						this.cost = this.deserialize.read_integer();
						break;
					case 2:
						this.itemId = this.deserialize.read_string();
						break;
					case 3:
						this.characterid = this.deserialize.read_integer();
						break;
					case 4:
						this.name = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002134 RID: 8500 RVA: 0x000A0010 File Offset: 0x0009E210
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.cost, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.itemId, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.characterid, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_string(this.name, 4);
				}
				return this.serialize.close();
			}

			// Token: 0x04001BA6 RID: 7078
			private static int max_field_count = 5;

			// Token: 0x04001BA7 RID: 7079
			private long _type;

			// Token: 0x04001BA8 RID: 7080
			private long _cost;

			// Token: 0x04001BA9 RID: 7081
			private string _itemId;

			// Token: 0x04001BAA RID: 7082
			private long _characterid;

			// Token: 0x04001BAB RID: 7083
			private string _name;
		}
	}
}
