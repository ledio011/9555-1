using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200059F RID: 1439
	public class ret_spin_slot
	{
		// Token: 0x020005A0 RID: 1440
		public class request : SprotoTypeBase
		{
			// Token: 0x060029A2 RID: 10658 RVA: 0x000B03A0 File Offset: 0x000AE5A0
			public request() : base(ret_spin_slot.request.max_field_count)
			{
			}

			// Token: 0x060029A3 RID: 10659 RVA: 0x000B03B0 File Offset: 0x000AE5B0
			public request(byte[] buffer) : base(ret_spin_slot.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BDB RID: 3035
			// (get) Token: 0x060029A5 RID: 10661 RVA: 0x000B03CC File Offset: 0x000AE5CC
			// (set) Token: 0x060029A6 RID: 10662 RVA: 0x000B03D4 File Offset: 0x000AE5D4
			public slot_info slot_info
			{
				get
				{
					return this._slot_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._slot_info = value;
				}
			}

			// Token: 0x17000BDC RID: 3036
			// (get) Token: 0x060029A7 RID: 10663 RVA: 0x000B03EC File Offset: 0x000AE5EC
			public bool HasSlot_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000BDD RID: 3037
			// (get) Token: 0x060029A8 RID: 10664 RVA: 0x000B03FC File Offset: 0x000AE5FC
			// (set) Token: 0x060029A9 RID: 10665 RVA: 0x000B0404 File Offset: 0x000AE604
			public Dictionary<string, slot_item> slot_items
			{
				get
				{
					return this._slot_items;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._slot_items = value;
				}
			}

			// Token: 0x17000BDE RID: 3038
			// (get) Token: 0x060029AA RID: 10666 RVA: 0x000B041C File Offset: 0x000AE61C
			public bool HasSlot_items
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060029AB RID: 10667 RVA: 0x000B042C File Offset: 0x000AE62C
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
							this.slot_items = this.deserialize.read_map<string, slot_item>((slot_item v) => v.uuid);
						}
					}
					else
					{
						this.slot_info = this.deserialize.read_obj<slot_info>();
					}
				}
			}

			// Token: 0x060029AC RID: 10668 RVA: 0x000B04C0 File Offset: 0x000AE6C0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.slot_info, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<string, slot_item>(this.slot_items, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DDA RID: 7642
			private static int max_field_count = 2;

			// Token: 0x04001DDB RID: 7643
			private slot_info _slot_info;

			// Token: 0x04001DDC RID: 7644
			private Dictionary<string, slot_item> _slot_items;
		}
	}
}
