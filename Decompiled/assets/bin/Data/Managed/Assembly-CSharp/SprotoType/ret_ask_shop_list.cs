using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004F9 RID: 1273
	public class ret_ask_shop_list
	{
		// Token: 0x020004FA RID: 1274
		public class request : SprotoTypeBase
		{
			// Token: 0x06002535 RID: 9525 RVA: 0x000A768C File Offset: 0x000A588C
			public request() : base(ret_ask_shop_list.request.max_field_count)
			{
			}

			// Token: 0x06002536 RID: 9526 RVA: 0x000A769C File Offset: 0x000A589C
			public request(byte[] buffer) : base(ret_ask_shop_list.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A4B RID: 2635
			// (get) Token: 0x06002538 RID: 9528 RVA: 0x000A76B8 File Offset: 0x000A58B8
			// (set) Token: 0x06002539 RID: 9529 RVA: 0x000A76C0 File Offset: 0x000A58C0
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

			// Token: 0x17000A4C RID: 2636
			// (get) Token: 0x0600253A RID: 9530 RVA: 0x000A76D8 File Offset: 0x000A58D8
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A4D RID: 2637
			// (get) Token: 0x0600253B RID: 9531 RVA: 0x000A76E8 File Offset: 0x000A58E8
			// (set) Token: 0x0600253C RID: 9532 RVA: 0x000A76F0 File Offset: 0x000A58F0
			public long curPage
			{
				get
				{
					return this._curPage;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._curPage = value;
				}
			}

			// Token: 0x17000A4E RID: 2638
			// (get) Token: 0x0600253D RID: 9533 RVA: 0x000A7708 File Offset: 0x000A5908
			public bool HasCurPage
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000A4F RID: 2639
			// (get) Token: 0x0600253E RID: 9534 RVA: 0x000A7718 File Offset: 0x000A5918
			// (set) Token: 0x0600253F RID: 9535 RVA: 0x000A7720 File Offset: 0x000A5920
			public long maxPage
			{
				get
				{
					return this._maxPage;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._maxPage = value;
				}
			}

			// Token: 0x17000A50 RID: 2640
			// (get) Token: 0x06002540 RID: 9536 RVA: 0x000A7738 File Offset: 0x000A5938
			public bool HasMaxPage
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000A51 RID: 2641
			// (get) Token: 0x06002541 RID: 9537 RVA: 0x000A7748 File Offset: 0x000A5948
			// (set) Token: 0x06002542 RID: 9538 RVA: 0x000A7750 File Offset: 0x000A5950
			public List<shop_item> shop_list
			{
				get
				{
					return this._shop_list;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._shop_list = value;
				}
			}

			// Token: 0x17000A52 RID: 2642
			// (get) Token: 0x06002543 RID: 9539 RVA: 0x000A7768 File Offset: 0x000A5968
			public bool HasShop_list
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000A53 RID: 2643
			// (get) Token: 0x06002544 RID: 9540 RVA: 0x000A7778 File Offset: 0x000A5978
			// (set) Token: 0x06002545 RID: 9541 RVA: 0x000A7780 File Offset: 0x000A5980
			public long subType
			{
				get
				{
					return this._subType;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._subType = value;
				}
			}

			// Token: 0x17000A54 RID: 2644
			// (get) Token: 0x06002546 RID: 9542 RVA: 0x000A7798 File Offset: 0x000A5998
			public bool HasSubType
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x06002547 RID: 9543 RVA: 0x000A77A8 File Offset: 0x000A59A8
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
						this.curPage = this.deserialize.read_integer();
						break;
					case 2:
						this.maxPage = this.deserialize.read_integer();
						break;
					case 3:
						this.shop_list = this.deserialize.read_obj_list<shop_item>();
						break;
					case 4:
						this.subType = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002548 RID: 9544 RVA: 0x000A7870 File Offset: 0x000A5A70
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.curPage, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.maxPage, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_obj<shop_item>(this.shop_list, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.subType, 4);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C9C RID: 7324
			private static int max_field_count = 5;

			// Token: 0x04001C9D RID: 7325
			private long _type;

			// Token: 0x04001C9E RID: 7326
			private long _curPage;

			// Token: 0x04001C9F RID: 7327
			private long _maxPage;

			// Token: 0x04001CA0 RID: 7328
			private List<shop_item> _shop_list;

			// Token: 0x04001CA1 RID: 7329
			private long _subType;
		}
	}
}
