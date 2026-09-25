using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003C4 RID: 964
	public class grant_daily_mission_reward
	{
		// Token: 0x020003C5 RID: 965
		public class request : SprotoTypeBase
		{
			// Token: 0x06001D6A RID: 7530 RVA: 0x00098434 File Offset: 0x00096634
			public request() : base(grant_daily_mission_reward.request.max_field_count)
			{
			}

			// Token: 0x06001D6B RID: 7531 RVA: 0x00098444 File Offset: 0x00096644
			public request(byte[] buffer) : base(grant_daily_mission_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170007A9 RID: 1961
			// (get) Token: 0x06001D6D RID: 7533 RVA: 0x00098460 File Offset: 0x00096660
			// (set) Token: 0x06001D6E RID: 7534 RVA: 0x00098468 File Offset: 0x00096668
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

			// Token: 0x170007AA RID: 1962
			// (get) Token: 0x06001D6F RID: 7535 RVA: 0x00098480 File Offset: 0x00096680
			public bool HasItems
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170007AB RID: 1963
			// (get) Token: 0x06001D70 RID: 7536 RVA: 0x00098490 File Offset: 0x00096690
			// (set) Token: 0x06001D71 RID: 7537 RVA: 0x00098498 File Offset: 0x00096698
			public List<item> items2
			{
				get
				{
					return this._items2;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._items2 = value;
				}
			}

			// Token: 0x170007AC RID: 1964
			// (get) Token: 0x06001D72 RID: 7538 RVA: 0x000984B0 File Offset: 0x000966B0
			public bool HasItems2
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001D73 RID: 7539 RVA: 0x000984C0 File Offset: 0x000966C0
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
							this.items2 = this.deserialize.read_obj_list<item>();
						}
					}
					else
					{
						this.items = this.deserialize.read_obj_list<item>();
					}
				}
			}

			// Token: 0x06001D74 RID: 7540 RVA: 0x00098538 File Offset: 0x00096738
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<item>(this.items, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<item>(this.items2, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001AA0 RID: 6816
			private static int max_field_count = 2;

			// Token: 0x04001AA1 RID: 6817
			private List<item> _items;

			// Token: 0x04001AA2 RID: 6818
			private List<item> _items2;
		}
	}
}
