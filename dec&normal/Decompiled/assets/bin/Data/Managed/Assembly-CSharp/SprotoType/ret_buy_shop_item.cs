using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000503 RID: 1283
	public class ret_buy_shop_item
	{
		// Token: 0x02000504 RID: 1284
		public class request : SprotoTypeBase
		{
			// Token: 0x0600257E RID: 9598 RVA: 0x000A7FB8 File Offset: 0x000A61B8
			public request() : base(ret_buy_shop_item.request.max_field_count)
			{
			}

			// Token: 0x0600257F RID: 9599 RVA: 0x000A7FC8 File Offset: 0x000A61C8
			public request(byte[] buffer) : base(ret_buy_shop_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A67 RID: 2663
			// (get) Token: 0x06002581 RID: 9601 RVA: 0x000A7FE4 File Offset: 0x000A61E4
			// (set) Token: 0x06002582 RID: 9602 RVA: 0x000A7FEC File Offset: 0x000A61EC
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

			// Token: 0x17000A68 RID: 2664
			// (get) Token: 0x06002583 RID: 9603 RVA: 0x000A8004 File Offset: 0x000A6204
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A69 RID: 2665
			// (get) Token: 0x06002584 RID: 9604 RVA: 0x000A8014 File Offset: 0x000A6214
			// (set) Token: 0x06002585 RID: 9605 RVA: 0x000A801C File Offset: 0x000A621C
			public shop_item shop_item
			{
				get
				{
					return this._shop_item;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._shop_item = value;
				}
			}

			// Token: 0x17000A6A RID: 2666
			// (get) Token: 0x06002586 RID: 9606 RVA: 0x000A8034 File Offset: 0x000A6234
			public bool HasShop_item
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000A6B RID: 2667
			// (get) Token: 0x06002587 RID: 9607 RVA: 0x000A8044 File Offset: 0x000A6244
			// (set) Token: 0x06002588 RID: 9608 RVA: 0x000A804C File Offset: 0x000A624C
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

			// Token: 0x17000A6C RID: 2668
			// (get) Token: 0x06002589 RID: 9609 RVA: 0x000A8064 File Offset: 0x000A6264
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000A6D RID: 2669
			// (get) Token: 0x0600258A RID: 9610 RVA: 0x000A8074 File Offset: 0x000A6274
			// (set) Token: 0x0600258B RID: 9611 RVA: 0x000A807C File Offset: 0x000A627C
			public long count
			{
				get
				{
					return this._count;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._count = value;
				}
			}

			// Token: 0x17000A6E RID: 2670
			// (get) Token: 0x0600258C RID: 9612 RVA: 0x000A8094 File Offset: 0x000A6294
			public bool HasCount
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x0600258D RID: 9613 RVA: 0x000A80A4 File Offset: 0x000A62A4
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
						this.shop_item = this.deserialize.read_obj<shop_item>();
						break;
					case 2:
						this.type = this.deserialize.read_integer();
						break;
					case 3:
						this.count = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x0600258E RID: 9614 RVA: 0x000A8150 File Offset: 0x000A6350
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj(this.shop_item, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.type, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.count, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CB0 RID: 7344
			private static int max_field_count = 4;

			// Token: 0x04001CB1 RID: 7345
			private long _state;

			// Token: 0x04001CB2 RID: 7346
			private shop_item _shop_item;

			// Token: 0x04001CB3 RID: 7347
			private long _type;

			// Token: 0x04001CB4 RID: 7348
			private long _count;
		}
	}
}
