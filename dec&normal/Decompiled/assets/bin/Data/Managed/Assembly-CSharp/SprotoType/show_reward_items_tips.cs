using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005D5 RID: 1493
	public class show_reward_items_tips
	{
		// Token: 0x020005D6 RID: 1494
		public class request : SprotoTypeBase
		{
			// Token: 0x06002B17 RID: 11031 RVA: 0x000B323C File Offset: 0x000B143C
			public request() : base(show_reward_items_tips.request.max_field_count)
			{
			}

			// Token: 0x06002B18 RID: 11032 RVA: 0x000B324C File Offset: 0x000B144C
			public request(byte[] buffer) : base(show_reward_items_tips.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C61 RID: 3169
			// (get) Token: 0x06002B1A RID: 11034 RVA: 0x000B3268 File Offset: 0x000B1468
			// (set) Token: 0x06002B1B RID: 11035 RVA: 0x000B3270 File Offset: 0x000B1470
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

			// Token: 0x17000C62 RID: 3170
			// (get) Token: 0x06002B1C RID: 11036 RVA: 0x000B3288 File Offset: 0x000B1488
			public bool HasItems
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002B1D RID: 11037 RVA: 0x000B3298 File Offset: 0x000B1498
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

			// Token: 0x06002B1E RID: 11038 RVA: 0x000B32F4 File Offset: 0x000B14F4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<item>(this.items, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E3C RID: 7740
			private static int max_field_count = 1;

			// Token: 0x04001E3D RID: 7741
			private List<item> _items;
		}
	}
}
