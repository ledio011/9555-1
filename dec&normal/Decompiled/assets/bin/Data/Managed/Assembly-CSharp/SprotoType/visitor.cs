using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000665 RID: 1637
	public class visitor
	{
		// Token: 0x02000666 RID: 1638
		public class request : SprotoTypeBase
		{
			// Token: 0x06002F63 RID: 12131 RVA: 0x000BBC9C File Offset: 0x000B9E9C
			public request() : base(visitor.request.max_field_count)
			{
			}

			// Token: 0x06002F64 RID: 12132 RVA: 0x000BBCAC File Offset: 0x000B9EAC
			public request(byte[] buffer) : base(visitor.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002F66 RID: 12134 RVA: 0x000BBCC4 File Offset: 0x000B9EC4
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002F67 RID: 12135 RVA: 0x000BBD00 File Offset: 0x000B9F00
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001F68 RID: 8040
			private static int max_field_count;
		}

		// Token: 0x02000667 RID: 1639
		public class response : SprotoTypeBase
		{
			// Token: 0x06002F68 RID: 12136 RVA: 0x000BBD1C File Offset: 0x000B9F1C
			public response() : base(visitor.response.max_field_count)
			{
			}

			// Token: 0x06002F69 RID: 12137 RVA: 0x000BBD2C File Offset: 0x000B9F2C
			public response(byte[] buffer) : base(visitor.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000E01 RID: 3585
			// (get) Token: 0x06002F6B RID: 12139 RVA: 0x000BBD48 File Offset: 0x000B9F48
			// (set) Token: 0x06002F6C RID: 12140 RVA: 0x000BBD50 File Offset: 0x000B9F50
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x17000E02 RID: 3586
			// (get) Token: 0x06002F6D RID: 12141 RVA: 0x000BBD68 File Offset: 0x000B9F68
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000E03 RID: 3587
			// (get) Token: 0x06002F6E RID: 12142 RVA: 0x000BBD78 File Offset: 0x000B9F78
			// (set) Token: 0x06002F6F RID: 12143 RVA: 0x000BBD80 File Offset: 0x000B9F80
			public string key
			{
				get
				{
					return this._key;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._key = value;
				}
			}

			// Token: 0x17000E04 RID: 3588
			// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000BBD98 File Offset: 0x000B9F98
			public bool HasKey
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000E05 RID: 3589
			// (get) Token: 0x06002F71 RID: 12145 RVA: 0x000BBDA8 File Offset: 0x000B9FA8
			// (set) Token: 0x06002F72 RID: 12146 RVA: 0x000BBDB0 File Offset: 0x000B9FB0
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._state = value;
				}
			}

			// Token: 0x17000E06 RID: 3590
			// (get) Token: 0x06002F73 RID: 12147 RVA: 0x000BBDC8 File Offset: 0x000B9FC8
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002F74 RID: 12148 RVA: 0x000BBDD8 File Offset: 0x000B9FD8
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.id = this.deserialize.read_string();
						break;
					case 1:
						this.key = this.deserialize.read_string();
						break;
					case 2:
						this.state = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002F75 RID: 12149 RVA: 0x000BBE6C File Offset: 0x000BA06C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.key, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.state, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F69 RID: 8041
			private static int max_field_count = 3;

			// Token: 0x04001F6A RID: 8042
			private string _id;

			// Token: 0x04001F6B RID: 8043
			private string _key;

			// Token: 0x04001F6C RID: 8044
			private long _state;
		}
	}
}
