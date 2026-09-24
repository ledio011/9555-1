using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200034E RID: 846
	public class chat_item : SprotoTypeBase
	{
		// Token: 0x060018EC RID: 6380 RVA: 0x0008EF58 File Offset: 0x0008D158
		public chat_item() : base(chat_item.max_field_count)
		{
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0008EF68 File Offset: 0x0008D168
		public chat_item(byte[] buffer) : base(chat_item.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0008EF88 File Offset: 0x0008D188
		// (set) Token: 0x060018F0 RID: 6384 RVA: 0x0008EF90 File Offset: 0x0008D190
		public long senderId
		{
			get
			{
				return this._senderId;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._senderId = value;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0008EFA8 File Offset: 0x0008D1A8
		public bool HasSenderId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x0008EFB8 File Offset: 0x0008D1B8
		// (set) Token: 0x060018F3 RID: 6387 RVA: 0x0008EFC0 File Offset: 0x0008D1C0
		public string senderName
		{
			get
			{
				return this._senderName;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._senderName = value;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0008EFD8 File Offset: 0x0008D1D8
		public bool HasSenderName
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0008EFE8 File Offset: 0x0008D1E8
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x0008EFF0 File Offset: 0x0008D1F0
		public long tellId
		{
			get
			{
				return this._tellId;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._tellId = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0008F008 File Offset: 0x0008D208
		public bool HasTellId
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x0008F018 File Offset: 0x0008D218
		// (set) Token: 0x060018F9 RID: 6393 RVA: 0x0008F020 File Offset: 0x0008D220
		public string tellName
		{
			get
			{
				return this._tellName;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._tellName = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0008F038 File Offset: 0x0008D238
		public bool HasTellName
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x0008F048 File Offset: 0x0008D248
		// (set) Token: 0x060018FC RID: 6396 RVA: 0x0008F050 File Offset: 0x0008D250
		public string chatInfo
		{
			get
			{
				return this._chatInfo;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._chatInfo = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0008F068 File Offset: 0x0008D268
		public bool HasChatInfo
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x0008F078 File Offset: 0x0008D278
		// (set) Token: 0x060018FF RID: 6399 RVA: 0x0008F080 File Offset: 0x0008D280
		public long chattype
		{
			get
			{
				return this._chattype;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._chattype = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0008F098 File Offset: 0x0008D298
		public bool HasChattype
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0008F0A8 File Offset: 0x0008D2A8
		// (set) Token: 0x06001902 RID: 6402 RVA: 0x0008F0B0 File Offset: 0x0008D2B0
		public long linktype
		{
			get
			{
				return this._linktype;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._linktype = value;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0008F0C8 File Offset: 0x0008D2C8
		public bool HasLinktype
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0008F0D8 File Offset: 0x0008D2D8
		// (set) Token: 0x06001905 RID: 6405 RVA: 0x0008F0E0 File Offset: 0x0008D2E0
		public List<long> intdata
		{
			get
			{
				return this._intdata;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._intdata = value;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0008F0F8 File Offset: 0x0008D2F8
		public bool HasIntdata
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x0008F108 File Offset: 0x0008D308
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x0008F110 File Offset: 0x0008D310
		public List<string> stringdata
		{
			get
			{
				return this._stringdata;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._stringdata = value;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0008F128 File Offset: 0x0008D328
		public bool HasStringdata
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x0008F138 File Offset: 0x0008D338
		// (set) Token: 0x0600190B RID: 6411 RVA: 0x0008F140 File Offset: 0x0008D340
		public long senderProfession
		{
			get
			{
				return this._senderProfession;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._senderProfession = value;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x0008F158 File Offset: 0x0008D358
		public bool HasSenderProfession
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0008F168 File Offset: 0x0008D368
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x0008F170 File Offset: 0x0008D370
		public long level
		{
			get
			{
				return this._level;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._level = value;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x0008F188 File Offset: 0x0008D388
		public bool HasLevel
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x0008F198 File Offset: 0x0008D398
		// (set) Token: 0x06001911 RID: 6417 RVA: 0x0008F1A0 File Offset: 0x0008D3A0
		public long combValue
		{
			get
			{
				return this._combValue;
			}
			set
			{
				this.has_field.set_field(11, true);
				this._combValue = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0008F1B8 File Offset: 0x0008D3B8
		public bool HasCombValue
		{
			get
			{
				return this.has_field.has_field(11);
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x0008F1C8 File Offset: 0x0008D3C8
		// (set) Token: 0x06001914 RID: 6420 RVA: 0x0008F1D0 File Offset: 0x0008D3D0
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this.has_field.set_field(12, true);
				this._guildId = value;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x0008F1E8 File Offset: 0x0008D3E8
		public bool HasGuildId
		{
			get
			{
				return this.has_field.has_field(12);
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x0008F1F8 File Offset: 0x0008D3F8
		// (set) Token: 0x06001917 RID: 6423 RVA: 0x0008F200 File Offset: 0x0008D400
		public string guildName
		{
			get
			{
				return this._guildName;
			}
			set
			{
				this.has_field.set_field(13, true);
				this._guildName = value;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x0008F218 File Offset: 0x0008D418
		public bool HasGuildName
		{
			get
			{
				return this.has_field.has_field(13);
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x0008F228 File Offset: 0x0008D428
		// (set) Token: 0x0600191A RID: 6426 RVA: 0x0008F230 File Offset: 0x0008D430
		public string chatInfo2
		{
			get
			{
				return this._chatInfo2;
			}
			set
			{
				this.has_field.set_field(14, true);
				this._chatInfo2 = value;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x0008F248 File Offset: 0x0008D448
		public bool HasChatInfo2
		{
			get
			{
				return this.has_field.has_field(14);
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0008F258 File Offset: 0x0008D458
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.senderId = this.deserialize.read_integer();
					break;
				case 1:
					this.senderName = this.deserialize.read_string();
					break;
				case 2:
					this.tellId = this.deserialize.read_integer();
					break;
				case 3:
					this.tellName = this.deserialize.read_string();
					break;
				case 4:
					this.chatInfo = this.deserialize.read_string();
					break;
				case 5:
					this.chattype = this.deserialize.read_integer();
					break;
				case 6:
					this.linktype = this.deserialize.read_integer();
					break;
				case 7:
					this.intdata = this.deserialize.read_integer_list();
					break;
				case 8:
					this.stringdata = this.deserialize.read_string_list();
					break;
				case 9:
					this.senderProfession = this.deserialize.read_integer();
					break;
				case 10:
					this.level = this.deserialize.read_integer();
					break;
				case 11:
					this.combValue = this.deserialize.read_integer();
					break;
				case 12:
					this.guildId = this.deserialize.read_integer();
					break;
				case 13:
					this.guildName = this.deserialize.read_string();
					break;
				case 14:
					this.chatInfo2 = this.deserialize.read_string();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0008F424 File Offset: 0x0008D624
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.senderId, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.senderName, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.tellId, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_string(this.tellName, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_string(this.chatInfo, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.chattype, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.linktype, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.intdata, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_string(this.stringdata, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_integer(this.senderProfession, 9);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_integer(this.level, 10);
			}
			if (this.has_field.has_field(11))
			{
				this.serialize.write_integer(this.combValue, 11);
			}
			if (this.has_field.has_field(12))
			{
				this.serialize.write_integer(this.guildId, 12);
			}
			if (this.has_field.has_field(13))
			{
				this.serialize.write_string(this.guildName, 13);
			}
			if (this.has_field.has_field(14))
			{
				this.serialize.write_string(this.chatInfo2, 14);
			}
			return this.serialize.close();
		}

		// Token: 0x0400195E RID: 6494
		private static int max_field_count = 15;

		// Token: 0x0400195F RID: 6495
		private long _senderId;

		// Token: 0x04001960 RID: 6496
		private string _senderName;

		// Token: 0x04001961 RID: 6497
		private long _tellId;

		// Token: 0x04001962 RID: 6498
		private string _tellName;

		// Token: 0x04001963 RID: 6499
		private string _chatInfo;

		// Token: 0x04001964 RID: 6500
		private long _chattype;

		// Token: 0x04001965 RID: 6501
		private long _linktype;

		// Token: 0x04001966 RID: 6502
		private List<long> _intdata;

		// Token: 0x04001967 RID: 6503
		private List<string> _stringdata;

		// Token: 0x04001968 RID: 6504
		private long _senderProfession;

		// Token: 0x04001969 RID: 6505
		private long _level;

		// Token: 0x0400196A RID: 6506
		private long _combValue;

		// Token: 0x0400196B RID: 6507
		private long _guildId;

		// Token: 0x0400196C RID: 6508
		private string _guildName;

		// Token: 0x0400196D RID: 6509
		private string _chatInfo2;
	}
}
