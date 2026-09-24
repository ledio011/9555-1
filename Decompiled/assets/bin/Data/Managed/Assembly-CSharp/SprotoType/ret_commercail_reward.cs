using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000507 RID: 1287
	public class ret_commercail_reward
	{
		// Token: 0x02000508 RID: 1288
		public class request : SprotoTypeBase
		{
			// Token: 0x06002599 RID: 9625 RVA: 0x000A8310 File Offset: 0x000A6510
			public request() : base(ret_commercail_reward.request.max_field_count)
			{
			}

			// Token: 0x0600259A RID: 9626 RVA: 0x000A8320 File Offset: 0x000A6520
			public request(byte[] buffer) : base(ret_commercail_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A71 RID: 2673
			// (get) Token: 0x0600259C RID: 9628 RVA: 0x000A833C File Offset: 0x000A653C
			// (set) Token: 0x0600259D RID: 9629 RVA: 0x000A8344 File Offset: 0x000A6544
			public List<item> items
			{
				get
				{
					return this._items;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._items = value;
				}
			}

			// Token: 0x17000A72 RID: 2674
			// (get) Token: 0x0600259E RID: 9630 RVA: 0x000A835C File Offset: 0x000A655C
			public bool HasItems
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A73 RID: 2675
			// (get) Token: 0x0600259F RID: 9631 RVA: 0x000A836C File Offset: 0x000A656C
			// (set) Token: 0x060025A0 RID: 9632 RVA: 0x000A8374 File Offset: 0x000A6574
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._type = value;
				}
			}

			// Token: 0x17000A74 RID: 2676
			// (get) Token: 0x060025A1 RID: 9633 RVA: 0x000A838C File Offset: 0x000A658C
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000A75 RID: 2677
			// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000A839C File Offset: 0x000A659C
			// (set) Token: 0x060025A3 RID: 9635 RVA: 0x000A83A4 File Offset: 0x000A65A4
			public long parm1
			{
				get
				{
					return this._parm1;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._parm1 = value;
				}
			}

			// Token: 0x17000A76 RID: 2678
			// (get) Token: 0x060025A4 RID: 9636 RVA: 0x000A83BC File Offset: 0x000A65BC
			public bool HasParm1
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000A77 RID: 2679
			// (get) Token: 0x060025A5 RID: 9637 RVA: 0x000A83CC File Offset: 0x000A65CC
			// (set) Token: 0x060025A6 RID: 9638 RVA: 0x000A83D4 File Offset: 0x000A65D4
			public string parm2
			{
				get
				{
					return this._parm2;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._parm2 = value;
				}
			}

			// Token: 0x17000A78 RID: 2680
			// (get) Token: 0x060025A7 RID: 9639 RVA: 0x000A83EC File Offset: 0x000A65EC
			public bool HasParm2
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000A79 RID: 2681
			// (get) Token: 0x060025A8 RID: 9640 RVA: 0x000A83FC File Offset: 0x000A65FC
			// (set) Token: 0x060025A9 RID: 9641 RVA: 0x000A8404 File Offset: 0x000A6604
			public string productId
			{
				get
				{
					return this._productId;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._productId = value;
				}
			}

			// Token: 0x17000A7A RID: 2682
			// (get) Token: 0x060025AA RID: 9642 RVA: 0x000A841C File Offset: 0x000A661C
			public bool HasProductId
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000A7B RID: 2683
			// (get) Token: 0x060025AB RID: 9643 RVA: 0x000A842C File Offset: 0x000A662C
			// (set) Token: 0x060025AC RID: 9644 RVA: 0x000A8434 File Offset: 0x000A6634
			public string token
			{
				get
				{
					return this._token;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._token = value;
				}
			}

			// Token: 0x17000A7C RID: 2684
			// (get) Token: 0x060025AD RID: 9645 RVA: 0x000A844C File Offset: 0x000A664C
			public bool HasToken
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x17000A7D RID: 2685
			// (get) Token: 0x060025AE RID: 9646 RVA: 0x000A845C File Offset: 0x000A665C
			// (set) Token: 0x060025AF RID: 9647 RVA: 0x000A8464 File Offset: 0x000A6664
			public string payload
			{
				get
				{
					return this._payload;
				}
				set
				{
					this.has_field.set_field(6, true);
					this._payload = value;
				}
			}

			// Token: 0x17000A7E RID: 2686
			// (get) Token: 0x060025B0 RID: 9648 RVA: 0x000A847C File Offset: 0x000A667C
			public bool HasPayload
			{
				get
				{
					return this.has_field.has_field(6);
				}
			}

			// Token: 0x17000A7F RID: 2687
			// (get) Token: 0x060025B1 RID: 9649 RVA: 0x000A848C File Offset: 0x000A668C
			// (set) Token: 0x060025B2 RID: 9650 RVA: 0x000A8494 File Offset: 0x000A6694
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(7, true);
					this._state = value;
				}
			}

			// Token: 0x17000A80 RID: 2688
			// (get) Token: 0x060025B3 RID: 9651 RVA: 0x000A84AC File Offset: 0x000A66AC
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(7);
				}
			}

			// Token: 0x060025B4 RID: 9652 RVA: 0x000A84BC File Offset: 0x000A66BC
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.items = this.deserialize.read_obj_list<item>();
						break;
					case 1:
						this.type = this.deserialize.read_integer();
						break;
					case 2:
						this.parm1 = this.deserialize.read_integer();
						break;
					case 3:
						this.parm2 = this.deserialize.read_string();
						break;
					case 4:
						this.productId = this.deserialize.read_string();
						break;
					case 5:
						this.token = this.deserialize.read_string();
						break;
					case 6:
						this.payload = this.deserialize.read_string();
						break;
					case 7:
						this.state = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060025B5 RID: 9653 RVA: 0x000A85D0 File Offset: 0x000A67D0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<item>(this.items, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.type, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.parm1, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_string(this.parm2, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_string(this.productId, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_string(this.token, 5);
				}
				if (this.has_field.has_field(6))
				{
					this.serialize.write_string(this.payload, 6);
				}
				if (this.has_field.has_field(7))
				{
					this.serialize.write_integer(this.state, 7);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CB7 RID: 7351
			private static int max_field_count = 8;

			// Token: 0x04001CB8 RID: 7352
			private List<item> _items;

			// Token: 0x04001CB9 RID: 7353
			private long _type;

			// Token: 0x04001CBA RID: 7354
			private long _parm1;

			// Token: 0x04001CBB RID: 7355
			private string _parm2;

			// Token: 0x04001CBC RID: 7356
			private string _productId;

			// Token: 0x04001CBD RID: 7357
			private string _token;

			// Token: 0x04001CBE RID: 7358
			private string _payload;

			// Token: 0x04001CBF RID: 7359
			private long _state;
		}
	}
}
