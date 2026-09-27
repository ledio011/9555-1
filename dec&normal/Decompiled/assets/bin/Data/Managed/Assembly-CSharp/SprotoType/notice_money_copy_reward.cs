using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200042D RID: 1069
	public class notice_money_copy_reward
	{
		// Token: 0x0200042E RID: 1070
		public class request : SprotoTypeBase
		{
			// Token: 0x06002118 RID: 8472 RVA: 0x0009FD24 File Offset: 0x0009DF24
			public request() : base(notice_money_copy_reward.request.max_field_count)
			{
			}

			// Token: 0x06002119 RID: 8473 RVA: 0x0009FD34 File Offset: 0x0009DF34
			public request(byte[] buffer) : base(notice_money_copy_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700092D RID: 2349
			// (get) Token: 0x0600211B RID: 8475 RVA: 0x0009FD50 File Offset: 0x0009DF50
			// (set) Token: 0x0600211C RID: 8476 RVA: 0x0009FD58 File Offset: 0x0009DF58
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

			// Token: 0x1700092E RID: 2350
			// (get) Token: 0x0600211D RID: 8477 RVA: 0x0009FD70 File Offset: 0x0009DF70
			public bool HasItems
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600211E RID: 8478 RVA: 0x0009FD80 File Offset: 0x0009DF80
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

			// Token: 0x0600211F RID: 8479 RVA: 0x0009FDDC File Offset: 0x0009DFDC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<item>(this.items, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001BA4 RID: 7076
			private static int max_field_count = 1;

			// Token: 0x04001BA5 RID: 7077
			private List<item> _items;
		}
	}
}
