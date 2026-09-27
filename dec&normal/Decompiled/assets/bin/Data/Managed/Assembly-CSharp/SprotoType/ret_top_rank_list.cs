using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005A3 RID: 1443
	public class ret_top_rank_list
	{
		// Token: 0x020005A4 RID: 1444
		public class request : SprotoTypeBase
		{
			// Token: 0x060029BB RID: 10683 RVA: 0x000B06B4 File Offset: 0x000AE8B4
			public request() : base(ret_top_rank_list.request.max_field_count)
			{
			}

			// Token: 0x060029BC RID: 10684 RVA: 0x000B06C4 File Offset: 0x000AE8C4
			public request(byte[] buffer) : base(ret_top_rank_list.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BE3 RID: 3043
			// (get) Token: 0x060029BE RID: 10686 RVA: 0x000B06E0 File Offset: 0x000AE8E0
			// (set) Token: 0x060029BF RID: 10687 RVA: 0x000B06E8 File Offset: 0x000AE8E8
			public List<sort_item> sort_items
			{
				get
				{
					return this._sort_items;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._sort_items = value;
				}
			}

			// Token: 0x17000BE4 RID: 3044
			// (get) Token: 0x060029C0 RID: 10688 RVA: 0x000B0700 File Offset: 0x000AE900
			public bool HasSort_items
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000BE5 RID: 3045
			// (get) Token: 0x060029C1 RID: 10689 RVA: 0x000B0710 File Offset: 0x000AE910
			// (set) Token: 0x060029C2 RID: 10690 RVA: 0x000B0718 File Offset: 0x000AE918
			public long sortType
			{
				get
				{
					return this._sortType;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._sortType = value;
				}
			}

			// Token: 0x17000BE6 RID: 3046
			// (get) Token: 0x060029C3 RID: 10691 RVA: 0x000B0730 File Offset: 0x000AE930
			public bool HasSortType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060029C4 RID: 10692 RVA: 0x000B0740 File Offset: 0x000AE940
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
							this.sortType = this.deserialize.read_integer();
						}
					}
					else
					{
						this.sort_items = this.deserialize.read_obj_list<sort_item>();
					}
				}
			}

			// Token: 0x060029C5 RID: 10693 RVA: 0x000B07B8 File Offset: 0x000AE9B8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<sort_item>(this.sort_items, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.sortType, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DE1 RID: 7649
			private static int max_field_count = 2;

			// Token: 0x04001DE2 RID: 7650
			private List<sort_item> _sort_items;

			// Token: 0x04001DE3 RID: 7651
			private long _sortType;
		}
	}
}
