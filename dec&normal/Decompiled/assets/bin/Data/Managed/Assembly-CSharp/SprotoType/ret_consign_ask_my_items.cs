using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200050D RID: 1293
	public class ret_consign_ask_my_items
	{
		// Token: 0x0200050E RID: 1294
		public class request : SprotoTypeBase
		{
			// Token: 0x060025D9 RID: 9689 RVA: 0x000A8B70 File Offset: 0x000A6D70
			public request() : base(ret_consign_ask_my_items.request.max_field_count)
			{
			}

			// Token: 0x060025DA RID: 9690 RVA: 0x000A8B80 File Offset: 0x000A6D80
			public request(byte[] buffer) : base(ret_consign_ask_my_items.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A8F RID: 2703
			// (get) Token: 0x060025DC RID: 9692 RVA: 0x000A8B9C File Offset: 0x000A6D9C
			// (set) Token: 0x060025DD RID: 9693 RVA: 0x000A8BA4 File Offset: 0x000A6DA4
			public Dictionary<long, consign_item> consign_items
			{
				get
				{
					return this._consign_items;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._consign_items = value;
				}
			}

			// Token: 0x17000A90 RID: 2704
			// (get) Token: 0x060025DE RID: 9694 RVA: 0x000A8BBC File Offset: 0x000A6DBC
			public bool HasConsign_items
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A91 RID: 2705
			// (get) Token: 0x060025DF RID: 9695 RVA: 0x000A8BCC File Offset: 0x000A6DCC
			// (set) Token: 0x060025E0 RID: 9696 RVA: 0x000A8BD4 File Offset: 0x000A6DD4
			public long success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._success = value;
				}
			}

			// Token: 0x17000A92 RID: 2706
			// (get) Token: 0x060025E1 RID: 9697 RVA: 0x000A8BEC File Offset: 0x000A6DEC
			public bool HasSuccess
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000A93 RID: 2707
			// (get) Token: 0x060025E2 RID: 9698 RVA: 0x000A8BFC File Offset: 0x000A6DFC
			// (set) Token: 0x060025E3 RID: 9699 RVA: 0x000A8C04 File Offset: 0x000A6E04
			public long serverTime
			{
				get
				{
					return this._serverTime;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._serverTime = value;
				}
			}

			// Token: 0x17000A94 RID: 2708
			// (get) Token: 0x060025E4 RID: 9700 RVA: 0x000A8C1C File Offset: 0x000A6E1C
			public bool HasServerTime
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x060025E5 RID: 9701 RVA: 0x000A8C2C File Offset: 0x000A6E2C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.consign_items = this.deserialize.read_map<long, consign_item>((consign_item v) => v.id);
						break;
					case 1:
						this.success = this.deserialize.read_integer();
						break;
					case 2:
						this.serverTime = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060025E6 RID: 9702 RVA: 0x000A8CDC File Offset: 0x000A6EDC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<long, consign_item>(this.consign_items, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.success, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.serverTime, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CCA RID: 7370
			private static int max_field_count = 3;

			// Token: 0x04001CCB RID: 7371
			private Dictionary<long, consign_item> _consign_items;

			// Token: 0x04001CCC RID: 7372
			private long _success;

			// Token: 0x04001CCD RID: 7373
			private long _serverTime;
		}
	}
}
