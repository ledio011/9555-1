using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000559 RID: 1369
	public class ret_open_item_package
	{
		// Token: 0x0200055A RID: 1370
		public class request : SprotoTypeBase
		{
			// Token: 0x060027D0 RID: 10192 RVA: 0x000AC9CC File Offset: 0x000AABCC
			public request() : base(ret_open_item_package.request.max_field_count)
			{
			}

			// Token: 0x060027D1 RID: 10193 RVA: 0x000AC9DC File Offset: 0x000AABDC
			public request(byte[] buffer) : base(ret_open_item_package.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B3D RID: 2877
			// (get) Token: 0x060027D3 RID: 10195 RVA: 0x000AC9F8 File Offset: 0x000AABF8
			// (set) Token: 0x060027D4 RID: 10196 RVA: 0x000ACA00 File Offset: 0x000AAC00
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

			// Token: 0x17000B3E RID: 2878
			// (get) Token: 0x060027D5 RID: 10197 RVA: 0x000ACA18 File Offset: 0x000AAC18
			public bool HasItems
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060027D6 RID: 10198 RVA: 0x000ACA28 File Offset: 0x000AAC28
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.items = this.deserialize.read_obj_list<item>();
					}
				}
			}

			// Token: 0x060027D7 RID: 10199 RVA: 0x000ACA84 File Offset: 0x000AAC84
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<item>(this.items, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D55 RID: 7509
			private static int max_field_count = 1;

			// Token: 0x04001D56 RID: 7510
			private List<item> _items;
		}
	}
}
