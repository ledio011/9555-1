using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200046A RID: 1130
	public class req_buy_guild_goods
	{
		// Token: 0x0200046B RID: 1131
		public class request : SprotoTypeBase
		{
			// Token: 0x060022E1 RID: 8929 RVA: 0x000A3650 File Offset: 0x000A1850
			public request() : base(req_buy_guild_goods.request.max_field_count)
			{
			}

			// Token: 0x060022E2 RID: 8930 RVA: 0x000A3660 File Offset: 0x000A1860
			public request(byte[] buffer) : base(req_buy_guild_goods.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009DD RID: 2525
			// (get) Token: 0x060022E4 RID: 8932 RVA: 0x000A367C File Offset: 0x000A187C
			// (set) Token: 0x060022E5 RID: 8933 RVA: 0x000A3684 File Offset: 0x000A1884
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

			// Token: 0x170009DE RID: 2526
			// (get) Token: 0x060022E6 RID: 8934 RVA: 0x000A369C File Offset: 0x000A189C
			public bool HasItemId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170009DF RID: 2527
			// (get) Token: 0x060022E7 RID: 8935 RVA: 0x000A36AC File Offset: 0x000A18AC
			// (set) Token: 0x060022E8 RID: 8936 RVA: 0x000A36B4 File Offset: 0x000A18B4
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

			// Token: 0x170009E0 RID: 2528
			// (get) Token: 0x060022E9 RID: 8937 RVA: 0x000A36CC File Offset: 0x000A18CC
			public bool HasBuyCount
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060022EA RID: 8938 RVA: 0x000A36DC File Offset: 0x000A18DC
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 != 1)
						{
							this.deserialize.read_unknow_data();
						}
						else
						{
							this.buyCount = this.deserialize.read_integer();
						}
					}
					else
					{
						this.itemId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x060022EB RID: 8939 RVA: 0x000A3754 File Offset: 0x000A1954
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
				return this.serialize.close();
			}

			// Token: 0x04001C1D RID: 7197
			private static int max_field_count = 2;

			// Token: 0x04001C1E RID: 7198
			private string _itemId;

			// Token: 0x04001C1F RID: 7199
			private long _buyCount;
		}
	}
}
