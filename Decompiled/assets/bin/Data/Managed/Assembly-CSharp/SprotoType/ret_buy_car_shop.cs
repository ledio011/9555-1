using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004FD RID: 1277
	public class ret_buy_car_shop
	{
		// Token: 0x020004FE RID: 1278
		public class request : SprotoTypeBase
		{
			// Token: 0x06002556 RID: 9558 RVA: 0x000A7AC4 File Offset: 0x000A5CC4
			public request() : base(ret_buy_car_shop.request.max_field_count)
			{
			}

			// Token: 0x06002557 RID: 9559 RVA: 0x000A7AD4 File Offset: 0x000A5CD4
			public request(byte[] buffer) : base(ret_buy_car_shop.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A59 RID: 2649
			// (get) Token: 0x06002559 RID: 9561 RVA: 0x000A7AF0 File Offset: 0x000A5CF0
			// (set) Token: 0x0600255A RID: 9562 RVA: 0x000A7AF8 File Offset: 0x000A5CF8
			public string mountId
			{
				get
				{
					return this._mountId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._mountId = value;
				}
			}

			// Token: 0x17000A5A RID: 2650
			// (get) Token: 0x0600255B RID: 9563 RVA: 0x000A7B10 File Offset: 0x000A5D10
			public bool HasMountId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A5B RID: 2651
			// (get) Token: 0x0600255C RID: 9564 RVA: 0x000A7B20 File Offset: 0x000A5D20
			// (set) Token: 0x0600255D RID: 9565 RVA: 0x000A7B28 File Offset: 0x000A5D28
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._state = value;
				}
			}

			// Token: 0x17000A5C RID: 2652
			// (get) Token: 0x0600255E RID: 9566 RVA: 0x000A7B40 File Offset: 0x000A5D40
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600255F RID: 9567 RVA: 0x000A7B50 File Offset: 0x000A5D50
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
							this.state = this.deserialize.read_integer();
						}
					}
					else
					{
						this.mountId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002560 RID: 9568 RVA: 0x000A7BC8 File Offset: 0x000A5DC8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.mountId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.state, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CA5 RID: 7333
			private static int max_field_count = 2;

			// Token: 0x04001CA6 RID: 7334
			private string _mountId;

			// Token: 0x04001CA7 RID: 7335
			private long _state;
		}
	}
}
