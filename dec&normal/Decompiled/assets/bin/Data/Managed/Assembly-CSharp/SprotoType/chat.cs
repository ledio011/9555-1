using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200034C RID: 844
	public class chat
	{
		// Token: 0x0200034D RID: 845
		public class request : SprotoTypeBase
		{
			// Token: 0x060018D2 RID: 6354 RVA: 0x0008EBC4 File Offset: 0x0008CDC4
			public request() : base(chat.request.max_field_count)
			{
			}

			// Token: 0x060018D3 RID: 6355 RVA: 0x0008EBD4 File Offset: 0x0008CDD4
			public request(byte[] buffer) : base(chat.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170005AB RID: 1451
			// (get) Token: 0x060018D5 RID: 6357 RVA: 0x0008EBF0 File Offset: 0x0008CDF0
			// (set) Token: 0x060018D6 RID: 6358 RVA: 0x0008EBF8 File Offset: 0x0008CDF8
			public long tellId
			{
				get
				{
					return this._tellId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._tellId = value;
				}
			}

			// Token: 0x170005AC RID: 1452
			// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0008EC10 File Offset: 0x0008CE10
			public bool HasTellId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170005AD RID: 1453
			// (get) Token: 0x060018D8 RID: 6360 RVA: 0x0008EC20 File Offset: 0x0008CE20
			// (set) Token: 0x060018D9 RID: 6361 RVA: 0x0008EC28 File Offset: 0x0008CE28
			public string tellName
			{
				get
				{
					return this._tellName;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._tellName = value;
				}
			}

			// Token: 0x170005AE RID: 1454
			// (get) Token: 0x060018DA RID: 6362 RVA: 0x0008EC40 File Offset: 0x0008CE40
			public bool HasTellName
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170005AF RID: 1455
			// (get) Token: 0x060018DB RID: 6363 RVA: 0x0008EC50 File Offset: 0x0008CE50
			// (set) Token: 0x060018DC RID: 6364 RVA: 0x0008EC58 File Offset: 0x0008CE58
			public string chatInfo
			{
				get
				{
					return this._chatInfo;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._chatInfo = value;
				}
			}

			// Token: 0x170005B0 RID: 1456
			// (get) Token: 0x060018DD RID: 6365 RVA: 0x0008EC70 File Offset: 0x0008CE70
			public bool HasChatInfo
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170005B1 RID: 1457
			// (get) Token: 0x060018DE RID: 6366 RVA: 0x0008EC80 File Offset: 0x0008CE80
			// (set) Token: 0x060018DF RID: 6367 RVA: 0x0008EC88 File Offset: 0x0008CE88
			public long chattype
			{
				get
				{
					return this._chattype;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._chattype = value;
				}
			}

			// Token: 0x170005B2 RID: 1458
			// (get) Token: 0x060018E0 RID: 6368 RVA: 0x0008ECA0 File Offset: 0x0008CEA0
			public bool HasChattype
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x170005B3 RID: 1459
			// (get) Token: 0x060018E1 RID: 6369 RVA: 0x0008ECB0 File Offset: 0x0008CEB0
			// (set) Token: 0x060018E2 RID: 6370 RVA: 0x0008ECB8 File Offset: 0x0008CEB8
			public long linktype
			{
				get
				{
					return this._linktype;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._linktype = value;
				}
			}

			// Token: 0x170005B4 RID: 1460
			// (get) Token: 0x060018E3 RID: 6371 RVA: 0x0008ECD0 File Offset: 0x0008CED0
			public bool HasLinktype
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x170005B5 RID: 1461
			// (get) Token: 0x060018E4 RID: 6372 RVA: 0x0008ECE0 File Offset: 0x0008CEE0
			// (set) Token: 0x060018E5 RID: 6373 RVA: 0x0008ECE8 File Offset: 0x0008CEE8
			public List<long> intdata
			{
				get
				{
					return this._intdata;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._intdata = value;
				}
			}

			// Token: 0x170005B6 RID: 1462
			// (get) Token: 0x060018E6 RID: 6374 RVA: 0x0008ED00 File Offset: 0x0008CF00
			public bool HasIntdata
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x170005B7 RID: 1463
			// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0008ED10 File Offset: 0x0008CF10
			// (set) Token: 0x060018E8 RID: 6376 RVA: 0x0008ED18 File Offset: 0x0008CF18
			public List<string> stringdata
			{
				get
				{
					return this._stringdata;
				}
				set
				{
					this.has_field.set_field(6, true);
					this._stringdata = value;
				}
			}

			// Token: 0x170005B8 RID: 1464
			// (get) Token: 0x060018E9 RID: 6377 RVA: 0x0008ED30 File Offset: 0x0008CF30
			public bool HasStringdata
			{
				get
				{
					return this.has_field.has_field(6);
				}
			}

			// Token: 0x060018EA RID: 6378 RVA: 0x0008ED40 File Offset: 0x0008CF40
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.tellId = this.deserialize.read_integer();
						break;
					case 1:
						this.tellName = this.deserialize.read_string();
						break;
					case 2:
						this.chatInfo = this.deserialize.read_string();
						break;
					case 3:
						this.chattype = this.deserialize.read_integer();
						break;
					case 4:
						this.linktype = this.deserialize.read_integer();
						break;
					case 5:
						this.intdata = this.deserialize.read_integer_list();
						break;
					case 6:
						this.stringdata = this.deserialize.read_string_list();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060018EB RID: 6379 RVA: 0x0008EE3C File Offset: 0x0008D03C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.tellId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.tellName, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.chatInfo, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.chattype, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.linktype, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.intdata, 5);
				}
				if (this.has_field.has_field(6))
				{
					this.serialize.write_string(this.stringdata, 6);
				}
				return this.serialize.close();
			}

			// Token: 0x04001956 RID: 6486
			private static int max_field_count = 7;

			// Token: 0x04001957 RID: 6487
			private long _tellId;

			// Token: 0x04001958 RID: 6488
			private string _tellName;

			// Token: 0x04001959 RID: 6489
			private string _chatInfo;

			// Token: 0x0400195A RID: 6490
			private long _chattype;

			// Token: 0x0400195B RID: 6491
			private long _linktype;

			// Token: 0x0400195C RID: 6492
			private List<long> _intdata;

			// Token: 0x0400195D RID: 6493
			private List<string> _stringdata;
		}
	}
}
