using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000370 RID: 880
	public class dance_state_info : SprotoTypeBase
	{
		// Token: 0x06001A65 RID: 6757 RVA: 0x000920B8 File Offset: 0x000902B8
		public dance_state_info() : base(dance_state_info.max_field_count)
		{
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000920C8 File Offset: 0x000902C8
		public dance_state_info(byte[] buffer) : base(dance_state_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x000920E8 File Offset: 0x000902E8
		// (set) Token: 0x06001A69 RID: 6761 RVA: 0x000920F0 File Offset: 0x000902F0
		public long uuid
		{
			get
			{
				return this._uuid;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._uuid = value;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x00092108 File Offset: 0x00090308
		public bool HasUuid
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x00092118 File Offset: 0x00090318
		// (set) Token: 0x06001A6C RID: 6764 RVA: 0x00092120 File Offset: 0x00090320
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._ID = value;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x00092138 File Offset: 0x00090338
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x00092148 File Offset: 0x00090348
		// (set) Token: 0x06001A6F RID: 6767 RVA: 0x00092150 File Offset: 0x00090350
		public long start_time
		{
			get
			{
				return this._start_time;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._start_time = value;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x00092168 File Offset: 0x00090368
		public bool HasStart_time
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x00092178 File Offset: 0x00090378
		// (set) Token: 0x06001A72 RID: 6770 RVA: 0x00092180 File Offset: 0x00090380
		public long end_time
		{
			get
			{
				return this._end_time;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._end_time = value;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x00092198 File Offset: 0x00090398
		public bool HasEnd_time
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x000921A8 File Offset: 0x000903A8
		// (set) Token: 0x06001A75 RID: 6773 RVA: 0x000921B0 File Offset: 0x000903B0
		public long state
		{
			get
			{
				return this._state;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._state = value;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x000921C8 File Offset: 0x000903C8
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x000921D8 File Offset: 0x000903D8
		// (set) Token: 0x06001A78 RID: 6776 RVA: 0x000921E0 File Offset: 0x000903E0
		public long parm
		{
			get
			{
				return this._parm;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._parm = value;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x000921F8 File Offset: 0x000903F8
		public bool HasParm
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x00092208 File Offset: 0x00090408
		// (set) Token: 0x06001A7B RID: 6779 RVA: 0x00092210 File Offset: 0x00090410
		public long duration
		{
			get
			{
				return this._duration;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._duration = value;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x00092228 File Offset: 0x00090428
		public bool HasDuration
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x00092238 File Offset: 0x00090438
		// (set) Token: 0x06001A7E RID: 6782 RVA: 0x00092240 File Offset: 0x00090440
		public long reset_time
		{
			get
			{
				return this._reset_time;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._reset_time = value;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x00092258 File Offset: 0x00090458
		public bool HasReset_time
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00092268 File Offset: 0x00090468
		// (set) Token: 0x06001A81 RID: 6785 RVA: 0x00092270 File Offset: 0x00090470
		public long parm2
		{
			get
			{
				return this._parm2;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._parm2 = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x00092288 File Offset: 0x00090488
		public bool HasParm2
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00092298 File Offset: 0x00090498
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.uuid = this.deserialize.read_integer();
					break;
				case 1:
					this.ID = this.deserialize.read_string();
					break;
				case 2:
					this.start_time = this.deserialize.read_integer();
					break;
				case 3:
					this.end_time = this.deserialize.read_integer();
					break;
				case 4:
					this.state = this.deserialize.read_integer();
					break;
				case 5:
					this.parm = this.deserialize.read_integer();
					break;
				case 6:
					this.duration = this.deserialize.read_integer();
					break;
				case 7:
					this.reset_time = this.deserialize.read_integer();
					break;
				case 8:
					this.parm2 = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x000923C8 File Offset: 0x000905C8
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.uuid, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.ID, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.start_time, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.end_time, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.state, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.parm, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.duration, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.reset_time, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.parm2, 8);
			}
			return this.serialize.close();
		}

		// Token: 0x040019C9 RID: 6601
		private static int max_field_count = 9;

		// Token: 0x040019CA RID: 6602
		private long _uuid;

		// Token: 0x040019CB RID: 6603
		private string _ID;

		// Token: 0x040019CC RID: 6604
		private long _start_time;

		// Token: 0x040019CD RID: 6605
		private long _end_time;

		// Token: 0x040019CE RID: 6606
		private long _state;

		// Token: 0x040019CF RID: 6607
		private long _parm;

		// Token: 0x040019D0 RID: 6608
		private long _duration;

		// Token: 0x040019D1 RID: 6609
		private long _reset_time;

		// Token: 0x040019D2 RID: 6610
		private long _parm2;
	}
}
