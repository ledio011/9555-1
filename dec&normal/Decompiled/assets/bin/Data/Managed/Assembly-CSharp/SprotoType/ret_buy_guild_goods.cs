using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004FF RID: 1279
	public class ret_buy_guild_goods
	{
		// Token: 0x02000500 RID: 1280
		public class request : SprotoTypeBase
		{
			// Token: 0x06002562 RID: 9570 RVA: 0x000A7C3C File Offset: 0x000A5E3C
			public request() : base(ret_buy_guild_goods.request.max_field_count)
			{
			}

			// Token: 0x06002563 RID: 9571 RVA: 0x000A7C4C File Offset: 0x000A5E4C
			public request(byte[] buffer) : base(ret_buy_guild_goods.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A5D RID: 2653
			// (get) Token: 0x06002565 RID: 9573 RVA: 0x000A7C68 File Offset: 0x000A5E68
			// (set) Token: 0x06002566 RID: 9574 RVA: 0x000A7C70 File Offset: 0x000A5E70
			public string itemId
			{
				get
				{
					return this._itemId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._itemId = value;
				}
			}

			// Token: 0x17000A5E RID: 2654
			// (get) Token: 0x06002567 RID: 9575 RVA: 0x000A7C88 File Offset: 0x000A5E88
			public bool HasItemId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A5F RID: 2655
			// (get) Token: 0x06002568 RID: 9576 RVA: 0x000A7C98 File Offset: 0x000A5E98
			// (set) Token: 0x06002569 RID: 9577 RVA: 0x000A7CA0 File Offset: 0x000A5EA0
			public long buyCount
			{
				get
				{
					return this._buyCount;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._buyCount = value;
				}
			}

			// Token: 0x17000A60 RID: 2656
			// (get) Token: 0x0600256A RID: 9578 RVA: 0x000A7CB8 File Offset: 0x000A5EB8
			public bool HasBuyCount
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000A61 RID: 2657
			// (get) Token: 0x0600256B RID: 9579 RVA: 0x000A7CC8 File Offset: 0x000A5EC8
			// (set) Token: 0x0600256C RID: 9580 RVA: 0x000A7CD0 File Offset: 0x000A5ED0
			public long cost
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

			// Token: 0x17000A62 RID: 2658
			// (get) Token: 0x0600256D RID: 9581 RVA: 0x000A7CE8 File Offset: 0x000A5EE8
			public bool HasCost
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000A63 RID: 2659
			// (get) Token: 0x0600256E RID: 9582 RVA: 0x000A7CF8 File Offset: 0x000A5EF8
			// (set) Token: 0x0600256F RID: 9583 RVA: 0x000A7D00 File Offset: 0x000A5F00
			public long leftNum
			{
				get
				{
					return this._leftNum;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._leftNum = value;
				}
			}

			// Token: 0x17000A64 RID: 2660
			// (get) Token: 0x06002570 RID: 9584 RVA: 0x000A7D18 File Offset: 0x000A5F18
			public bool HasLeftNum
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06002571 RID: 9585 RVA: 0x000A7D28 File Offset: 0x000A5F28
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.itemId = this.deserialize.read_string();
						break;
					case 1:
						this.buyCount = this.deserialize.read_integer();
						break;
					case 2:
						this.cost = this.deserialize.read_integer();
						break;
					case 3:
						this.leftNum = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002572 RID: 9586 RVA: 0x000A7DD4 File Offset: 0x000A5FD4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.itemId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.buyCount, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.cost, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.leftNum, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CA8 RID: 7336
			private static int max_field_count = 4;

			// Token: 0x04001CA9 RID: 7337
			private string _itemId;

			// Token: 0x04001CAA RID: 7338
			private long _buyCount;

			// Token: 0x04001CAB RID: 7339
			private long _cost;

			// Token: 0x04001CAC RID: 7340
			private long _leftNum;
		}
	}
}
