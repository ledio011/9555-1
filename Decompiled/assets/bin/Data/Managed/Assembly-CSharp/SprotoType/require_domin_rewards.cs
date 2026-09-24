using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004E5 RID: 1253
	public class require_domin_rewards
	{
		// Token: 0x020004E6 RID: 1254
		public class request : SprotoTypeBase
		{
			// Token: 0x060024CC RID: 9420 RVA: 0x000A6A34 File Offset: 0x000A4C34
			public request() : base(require_domin_rewards.request.max_field_count)
			{
			}

			// Token: 0x060024CD RID: 9421 RVA: 0x000A6A44 File Offset: 0x000A4C44
			public request(byte[] buffer) : base(require_domin_rewards.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A2D RID: 2605
			// (get) Token: 0x060024CF RID: 9423 RVA: 0x000A6A60 File Offset: 0x000A4C60
			// (set) Token: 0x060024D0 RID: 9424 RVA: 0x000A6A68 File Offset: 0x000A4C68
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

			// Token: 0x17000A2E RID: 2606
			// (get) Token: 0x060024D1 RID: 9425 RVA: 0x000A6A80 File Offset: 0x000A4C80
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A2F RID: 2607
			// (get) Token: 0x060024D2 RID: 9426 RVA: 0x000A6A90 File Offset: 0x000A4C90
			// (set) Token: 0x060024D3 RID: 9427 RVA: 0x000A6A98 File Offset: 0x000A4C98
			public long index
			{
				get
				{
					return this._index;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._index = value;
				}
			}

			// Token: 0x17000A30 RID: 2608
			// (get) Token: 0x060024D4 RID: 9428 RVA: 0x000A6AB0 File Offset: 0x000A4CB0
			public bool HasIndex
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000A31 RID: 2609
			// (get) Token: 0x060024D5 RID: 9429 RVA: 0x000A6AC0 File Offset: 0x000A4CC0
			// (set) Token: 0x060024D6 RID: 9430 RVA: 0x000A6AC8 File Offset: 0x000A4CC8
			public bool cost
			{
				get
				{
					return this._cost;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._cost = value;
				}
			}

			// Token: 0x17000A32 RID: 2610
			// (get) Token: 0x060024D7 RID: 9431 RVA: 0x000A6AE0 File Offset: 0x000A4CE0
			public bool HasCost
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x060024D8 RID: 9432 RVA: 0x000A6AF0 File Offset: 0x000A4CF0
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
						this.index = this.deserialize.read_integer();
						break;
					case 2:
						this.cost = this.deserialize.read_boolean();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060024D9 RID: 9433 RVA: 0x000A6B84 File Offset: 0x000A4D84
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.index, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_boolean(this.cost, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C83 RID: 7299
			private static int max_field_count = 3;

			// Token: 0x04001C84 RID: 7300
			private string _id;

			// Token: 0x04001C85 RID: 7301
			private long _index;

			// Token: 0x04001C86 RID: 7302
			private bool _cost;
		}
	}
}
