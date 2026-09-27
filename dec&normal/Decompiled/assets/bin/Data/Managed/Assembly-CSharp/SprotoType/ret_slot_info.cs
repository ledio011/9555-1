using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000599 RID: 1433
	public class ret_slot_info
	{
		// Token: 0x0200059A RID: 1434
		public class request : SprotoTypeBase
		{
			// Token: 0x0600297B RID: 10619 RVA: 0x000AFECC File Offset: 0x000AE0CC
			public request() : base(ret_slot_info.request.max_field_count)
			{
			}

			// Token: 0x0600297C RID: 10620 RVA: 0x000AFEDC File Offset: 0x000AE0DC
			public request(byte[] buffer) : base(ret_slot_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BCF RID: 3023
			// (get) Token: 0x0600297E RID: 10622 RVA: 0x000AFEF8 File Offset: 0x000AE0F8
			// (set) Token: 0x0600297F RID: 10623 RVA: 0x000AFF00 File Offset: 0x000AE100
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

			// Token: 0x17000BD0 RID: 3024
			// (get) Token: 0x06002980 RID: 10624 RVA: 0x000AFF18 File Offset: 0x000AE118
			public bool HasSlot_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000BD1 RID: 3025
			// (get) Token: 0x06002981 RID: 10625 RVA: 0x000AFF28 File Offset: 0x000AE128
			// (set) Token: 0x06002982 RID: 10626 RVA: 0x000AFF30 File Offset: 0x000AE130
			public Dictionary<string, slot_data> slot_datas
			{
				get
				{
					return this._slot_datas;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._slot_datas = value;
				}
			}

			// Token: 0x17000BD2 RID: 3026
			// (get) Token: 0x06002983 RID: 10627 RVA: 0x000AFF48 File Offset: 0x000AE148
			public bool HasSlot_datas
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000BD3 RID: 3027
			// (get) Token: 0x06002984 RID: 10628 RVA: 0x000AFF58 File Offset: 0x000AE158
			// (set) Token: 0x06002985 RID: 10629 RVA: 0x000AFF60 File Offset: 0x000AE160
			public Dictionary<string, slot_item> slot_items
			{
				get
				{
					return this._slot_items;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._slot_items = value;
				}
			}

			// Token: 0x17000BD4 RID: 3028
			// (get) Token: 0x06002986 RID: 10630 RVA: 0x000AFF78 File Offset: 0x000AE178
			public bool HasSlot_items
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002987 RID: 10631 RVA: 0x000AFF88 File Offset: 0x000AE188
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.slot_info = this.deserialize.read_obj<slot_info>();
						break;
					case 1:
						this.slot_datas = this.deserialize.read_map<string, slot_data>((slot_data v) => v.ID);
						break;
					case 2:
						this.slot_items = this.deserialize.read_map<string, slot_item>((slot_item v) => v.uuid);
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002988 RID: 10632 RVA: 0x000B0054 File Offset: 0x000AE254
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.slot_info, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<string, slot_data>(this.slot_datas, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_obj<string, slot_item>(this.slot_items, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DCE RID: 7630
			private static int max_field_count = 3;

			// Token: 0x04001DCF RID: 7631
			private slot_info _slot_info;

			// Token: 0x04001DD0 RID: 7632
			private Dictionary<string, slot_data> _slot_datas;

			// Token: 0x04001DD1 RID: 7633
			private Dictionary<string, slot_item> _slot_items;
		}
	}
}
