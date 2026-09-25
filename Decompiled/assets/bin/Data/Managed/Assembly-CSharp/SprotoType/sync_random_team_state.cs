using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200060F RID: 1551
	public class sync_random_team_state
	{
		// Token: 0x02000610 RID: 1552
		public class request : SprotoTypeBase
		{
			// Token: 0x06002CEC RID: 11500 RVA: 0x000B6DAC File Offset: 0x000B4FAC
			public request() : base(sync_random_team_state.request.max_field_count)
			{
			}

			// Token: 0x06002CED RID: 11501 RVA: 0x000B6DBC File Offset: 0x000B4FBC
			public request(byte[] buffer) : base(sync_random_team_state.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D17 RID: 3351
			// (get) Token: 0x06002CEF RID: 11503 RVA: 0x000B6DD8 File Offset: 0x000B4FD8
			// (set) Token: 0x06002CF0 RID: 11504 RVA: 0x000B6DE0 File Offset: 0x000B4FE0
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

			// Token: 0x17000D18 RID: 3352
			// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x000B6DF8 File Offset: 0x000B4FF8
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000D19 RID: 3353
			// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x000B6E08 File Offset: 0x000B5008
			// (set) Token: 0x06002CF3 RID: 11507 RVA: 0x000B6E10 File Offset: 0x000B5010
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._id = value;
				}
			}

			// Token: 0x17000D1A RID: 3354
			// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000B6E28 File Offset: 0x000B5028
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000D1B RID: 3355
			// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x000B6E38 File Offset: 0x000B5038
			// (set) Token: 0x06002CF6 RID: 11510 RVA: 0x000B6E40 File Offset: 0x000B5040
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._type = value;
				}
			}

			// Token: 0x17000D1C RID: 3356
			// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x000B6E58 File Offset: 0x000B5058
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002CF8 RID: 11512 RVA: 0x000B6E68 File Offset: 0x000B5068
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
						this.id = this.deserialize.read_string();
						break;
					case 2:
						this.type = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002CF9 RID: 11513 RVA: 0x000B6EFC File Offset: 0x000B50FC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.id, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.type, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001EC1 RID: 7873
			private static int max_field_count = 3;

			// Token: 0x04001EC2 RID: 7874
			private long _state;

			// Token: 0x04001EC3 RID: 7875
			private string _id;

			// Token: 0x04001EC4 RID: 7876
			private long _type;
		}
	}
}
