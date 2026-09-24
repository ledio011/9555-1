using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200050B RID: 1291
	public class ret_consign_ask_items_info
	{
		// Token: 0x0200050C RID: 1292
		public class request : SprotoTypeBase
		{
			// Token: 0x060025C3 RID: 9667 RVA: 0x000A888C File Offset: 0x000A6A8C
			public request() : base(ret_consign_ask_items_info.request.max_field_count)
			{
			}

			// Token: 0x060025C4 RID: 9668 RVA: 0x000A889C File Offset: 0x000A6A9C
			public request(byte[] buffer) : base(ret_consign_ask_items_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A85 RID: 2693
			// (get) Token: 0x060025C6 RID: 9670 RVA: 0x000A88B8 File Offset: 0x000A6AB8
			// (set) Token: 0x060025C7 RID: 9671 RVA: 0x000A88C0 File Offset: 0x000A6AC0
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

			// Token: 0x17000A86 RID: 2694
			// (get) Token: 0x060025C8 RID: 9672 RVA: 0x000A88D8 File Offset: 0x000A6AD8
			public bool HasConsign_items
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A87 RID: 2695
			// (get) Token: 0x060025C9 RID: 9673 RVA: 0x000A88E8 File Offset: 0x000A6AE8
			// (set) Token: 0x060025CA RID: 9674 RVA: 0x000A88F0 File Offset: 0x000A6AF0
			public long curPage
			{
				get
				{
					return this._curPage;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._curPage = value;
				}
			}

			// Token: 0x17000A88 RID: 2696
			// (get) Token: 0x060025CB RID: 9675 RVA: 0x000A8908 File Offset: 0x000A6B08
			public bool HasCurPage
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000A89 RID: 2697
			// (get) Token: 0x060025CC RID: 9676 RVA: 0x000A8918 File Offset: 0x000A6B18
			// (set) Token: 0x060025CD RID: 9677 RVA: 0x000A8920 File Offset: 0x000A6B20
			public long maxPage
			{
				get
				{
					return this._maxPage;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._maxPage = value;
				}
			}

			// Token: 0x17000A8A RID: 2698
			// (get) Token: 0x060025CE RID: 9678 RVA: 0x000A8938 File Offset: 0x000A6B38
			public bool HasMaxPage
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000A8B RID: 2699
			// (get) Token: 0x060025CF RID: 9679 RVA: 0x000A8948 File Offset: 0x000A6B48
			// (set) Token: 0x060025D0 RID: 9680 RVA: 0x000A8950 File Offset: 0x000A6B50
			public long success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._success = value;
				}
			}

			// Token: 0x17000A8C RID: 2700
			// (get) Token: 0x060025D1 RID: 9681 RVA: 0x000A8968 File Offset: 0x000A6B68
			public bool HasSuccess
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000A8D RID: 2701
			// (get) Token: 0x060025D2 RID: 9682 RVA: 0x000A8978 File Offset: 0x000A6B78
			// (set) Token: 0x060025D3 RID: 9683 RVA: 0x000A8980 File Offset: 0x000A6B80
			public long serverTime
			{
				get
				{
					return this._serverTime;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._serverTime = value;
				}
			}

			// Token: 0x17000A8E RID: 2702
			// (get) Token: 0x060025D4 RID: 9684 RVA: 0x000A8998 File Offset: 0x000A6B98
			public bool HasServerTime
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x060025D5 RID: 9685 RVA: 0x000A89A8 File Offset: 0x000A6BA8
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
						this.curPage = this.deserialize.read_integer();
						break;
					case 2:
						this.maxPage = this.deserialize.read_integer();
						break;
					case 3:
						this.success = this.deserialize.read_integer();
						break;
					case 4:
						this.serverTime = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060025D6 RID: 9686 RVA: 0x000A8A8C File Offset: 0x000A6C8C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<long, consign_item>(this.consign_items, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.curPage, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.maxPage, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.success, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.serverTime, 4);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CC3 RID: 7363
			private static int max_field_count = 5;

			// Token: 0x04001CC4 RID: 7364
			private Dictionary<long, consign_item> _consign_items;

			// Token: 0x04001CC5 RID: 7365
			private long _curPage;

			// Token: 0x04001CC6 RID: 7366
			private long _maxPage;

			// Token: 0x04001CC7 RID: 7367
			private long _success;

			// Token: 0x04001CC8 RID: 7368
			private long _serverTime;
		}
	}
}
