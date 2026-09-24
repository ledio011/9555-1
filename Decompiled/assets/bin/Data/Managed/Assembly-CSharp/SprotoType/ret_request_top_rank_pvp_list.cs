using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200057F RID: 1407
	public class ret_request_top_rank_pvp_list
	{
		// Token: 0x02000580 RID: 1408
		public class request : SprotoTypeBase
		{
			// Token: 0x060028D6 RID: 10454 RVA: 0x000AEA78 File Offset: 0x000ACC78
			public request() : base(ret_request_top_rank_pvp_list.request.max_field_count)
			{
			}

			// Token: 0x060028D7 RID: 10455 RVA: 0x000AEA88 File Offset: 0x000ACC88
			public request(byte[] buffer) : base(ret_request_top_rank_pvp_list.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B97 RID: 2967
			// (get) Token: 0x060028D9 RID: 10457 RVA: 0x000AEAA4 File Offset: 0x000ACCA4
			// (set) Token: 0x060028DA RID: 10458 RVA: 0x000AEAAC File Offset: 0x000ACCAC
			public long curPage
			{
				get
				{
					return this._curPage;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._curPage = value;
				}
			}

			// Token: 0x17000B98 RID: 2968
			// (get) Token: 0x060028DB RID: 10459 RVA: 0x000AEAC4 File Offset: 0x000ACCC4
			public bool HasCurPage
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B99 RID: 2969
			// (get) Token: 0x060028DC RID: 10460 RVA: 0x000AEAD4 File Offset: 0x000ACCD4
			// (set) Token: 0x060028DD RID: 10461 RVA: 0x000AEADC File Offset: 0x000ACCDC
			public long maxPage
			{
				get
				{
					return this._maxPage;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._maxPage = value;
				}
			}

			// Token: 0x17000B9A RID: 2970
			// (get) Token: 0x060028DE RID: 10462 RVA: 0x000AEAF4 File Offset: 0x000ACCF4
			public bool HasMaxPage
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000B9B RID: 2971
			// (get) Token: 0x060028DF RID: 10463 RVA: 0x000AEB04 File Offset: 0x000ACD04
			// (set) Token: 0x060028E0 RID: 10464 RVA: 0x000AEB0C File Offset: 0x000ACD0C
			public List<sort_item> sort_items
			{
				get
				{
					return this._sort_items;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._sort_items = value;
				}
			}

			// Token: 0x17000B9C RID: 2972
			// (get) Token: 0x060028E1 RID: 10465 RVA: 0x000AEB24 File Offset: 0x000ACD24
			public bool HasSort_items
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x060028E2 RID: 10466 RVA: 0x000AEB34 File Offset: 0x000ACD34
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.curPage = this.deserialize.read_integer();
						break;
					case 1:
						this.maxPage = this.deserialize.read_integer();
						break;
					case 2:
						this.sort_items = this.deserialize.read_obj_list<sort_item>();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060028E3 RID: 10467 RVA: 0x000AEBC8 File Offset: 0x000ACDC8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.curPage, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.maxPage, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_obj<sort_item>(this.sort_items, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DA2 RID: 7586
			private static int max_field_count = 3;

			// Token: 0x04001DA3 RID: 7587
			private long _curPage;

			// Token: 0x04001DA4 RID: 7588
			private long _maxPage;

			// Token: 0x04001DA5 RID: 7589
			private List<sort_item> _sort_items;
		}
	}
}
