using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200059B RID: 1435
	public class ret_slot_sum_reward
	{
		// Token: 0x0200059C RID: 1436
		public class request : SprotoTypeBase
		{
			// Token: 0x0600298C RID: 10636 RVA: 0x000B00FC File Offset: 0x000AE2FC
			public request() : base(ret_slot_sum_reward.request.max_field_count)
			{
			}

			// Token: 0x0600298D RID: 10637 RVA: 0x000B010C File Offset: 0x000AE30C
			public request(byte[] buffer) : base(ret_slot_sum_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BD5 RID: 3029
			// (get) Token: 0x0600298F RID: 10639 RVA: 0x000B0128 File Offset: 0x000AE328
			// (set) Token: 0x06002990 RID: 10640 RVA: 0x000B0130 File Offset: 0x000AE330
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

			// Token: 0x17000BD6 RID: 3030
			// (get) Token: 0x06002991 RID: 10641 RVA: 0x000B0148 File Offset: 0x000AE348
			public bool HasItems
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000BD7 RID: 3031
			// (get) Token: 0x06002992 RID: 10642 RVA: 0x000B0158 File Offset: 0x000AE358
			// (set) Token: 0x06002993 RID: 10643 RVA: 0x000B0160 File Offset: 0x000AE360
			public long sumNum
			{
				get
				{
					return this._sumNum;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._sumNum = value;
				}
			}

			// Token: 0x17000BD8 RID: 3032
			// (get) Token: 0x06002994 RID: 10644 RVA: 0x000B0178 File Offset: 0x000AE378
			public bool HasSumNum
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002995 RID: 10645 RVA: 0x000B0188 File Offset: 0x000AE388
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
							this.sumNum = this.deserialize.read_integer();
						}
					}
					else
					{
						this.items = this.deserialize.read_obj_list<item>();
					}
				}
			}

			// Token: 0x06002996 RID: 10646 RVA: 0x000B0200 File Offset: 0x000AE400
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<item>(this.items, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.sumNum, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DD4 RID: 7636
			private static int max_field_count = 2;

			// Token: 0x04001DD5 RID: 7637
			private List<item> _items;

			// Token: 0x04001DD6 RID: 7638
			private long _sumNum;
		}
	}
}
