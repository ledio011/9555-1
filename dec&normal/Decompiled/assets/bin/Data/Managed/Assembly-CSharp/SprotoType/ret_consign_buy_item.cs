using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200050F RID: 1295
	public class ret_consign_buy_item
	{
		// Token: 0x02000510 RID: 1296
		public class request : SprotoTypeBase
		{
			// Token: 0x060025E9 RID: 9705 RVA: 0x000A8D7C File Offset: 0x000A6F7C
			public request() : base(ret_consign_buy_item.request.max_field_count)
			{
			}

			// Token: 0x060025EA RID: 9706 RVA: 0x000A8D8C File Offset: 0x000A6F8C
			public request(byte[] buffer) : base(ret_consign_buy_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A95 RID: 2709
			// (get) Token: 0x060025EC RID: 9708 RVA: 0x000A8DA8 File Offset: 0x000A6FA8
			// (set) Token: 0x060025ED RID: 9709 RVA: 0x000A8DB0 File Offset: 0x000A6FB0
			public long id
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

			// Token: 0x17000A96 RID: 2710
			// (get) Token: 0x060025EE RID: 9710 RVA: 0x000A8DC8 File Offset: 0x000A6FC8
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A97 RID: 2711
			// (get) Token: 0x060025EF RID: 9711 RVA: 0x000A8DD8 File Offset: 0x000A6FD8
			// (set) Token: 0x060025F0 RID: 9712 RVA: 0x000A8DE0 File Offset: 0x000A6FE0
			public long success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._success = value;
				}
			}

			// Token: 0x17000A98 RID: 2712
			// (get) Token: 0x060025F1 RID: 9713 RVA: 0x000A8DF8 File Offset: 0x000A6FF8
			public bool HasSuccess
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000A99 RID: 2713
			// (get) Token: 0x060025F2 RID: 9714 RVA: 0x000A8E08 File Offset: 0x000A7008
			// (set) Token: 0x060025F3 RID: 9715 RVA: 0x000A8E10 File Offset: 0x000A7010
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

			// Token: 0x17000A9A RID: 2714
			// (get) Token: 0x060025F4 RID: 9716 RVA: 0x000A8E28 File Offset: 0x000A7028
			public bool HasItemId
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x060025F5 RID: 9717 RVA: 0x000A8E38 File Offset: 0x000A7038
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.id = this.deserialize.read_integer();
						break;
					case 1:
						this.success = this.deserialize.read_integer();
						break;
					case 2:
						this.itemId = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060025F6 RID: 9718 RVA: 0x000A8ECC File Offset: 0x000A70CC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.success, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.itemId, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CCF RID: 7375
			private static int max_field_count = 3;

			// Token: 0x04001CD0 RID: 7376
			private long _id;

			// Token: 0x04001CD1 RID: 7377
			private long _success;

			// Token: 0x04001CD2 RID: 7378
			private string _itemId;
		}
	}
}
