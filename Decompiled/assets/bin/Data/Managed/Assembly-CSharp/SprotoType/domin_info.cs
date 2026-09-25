using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000374 RID: 884
	public class domin_info : SprotoTypeBase
	{
		// Token: 0x06001A9C RID: 6812 RVA: 0x00092810 File Offset: 0x00090A10
		public domin_info() : base(domin_info.max_field_count)
		{
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00092820 File Offset: 0x00090A20
		public domin_info(byte[] buffer) : base(domin_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x00092840 File Offset: 0x00090A40
		// (set) Token: 0x06001AA0 RID: 6816 RVA: 0x00092848 File Offset: 0x00090A48
		public string id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._id = value;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x00092860 File Offset: 0x00090A60
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x00092870 File Offset: 0x00090A70
		// (set) Token: 0x06001AA3 RID: 6819 RVA: 0x00092878 File Offset: 0x00090A78
		public long max_donmin
		{
			get
			{
				return this._max_donmin;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._max_donmin = value;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x00092890 File Offset: 0x00090A90
		public bool HasMax_donmin
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x000928A0 File Offset: 0x00090AA0
		// (set) Token: 0x06001AA6 RID: 6822 RVA: 0x000928A8 File Offset: 0x00090AA8
		public long donmin_time
		{
			get
			{
				return this._donmin_time;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._donmin_time = value;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x000928C0 File Offset: 0x00090AC0
		public bool HasDonmin_time
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x000928D0 File Offset: 0x00090AD0
		// (set) Token: 0x06001AA9 RID: 6825 RVA: 0x000928D8 File Offset: 0x00090AD8
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

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x000928F0 File Offset: 0x00090AF0
		public bool HasEnd_time
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x00092900 File Offset: 0x00090B00
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x00092908 File Offset: 0x00090B08
		public long res_time1
		{
			get
			{
				return this._res_time1;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._res_time1 = value;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x00092920 File Offset: 0x00090B20
		public bool HasRes_time1
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x00092930 File Offset: 0x00090B30
		// (set) Token: 0x06001AAF RID: 6831 RVA: 0x00092938 File Offset: 0x00090B38
		public long res_count1
		{
			get
			{
				return this._res_count1;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._res_count1 = value;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x00092950 File Offset: 0x00090B50
		public bool HasRes_count1
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x00092960 File Offset: 0x00090B60
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x00092968 File Offset: 0x00090B68
		public long res_time2
		{
			get
			{
				return this._res_time2;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._res_time2 = value;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00092980 File Offset: 0x00090B80
		public bool HasRes_time2
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x00092990 File Offset: 0x00090B90
		// (set) Token: 0x06001AB5 RID: 6837 RVA: 0x00092998 File Offset: 0x00090B98
		public long res_count2
		{
			get
			{
				return this._res_count2;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._res_count2 = value;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x000929B0 File Offset: 0x00090BB0
		public bool HasRes_count2
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x000929C0 File Offset: 0x00090BC0
		// (set) Token: 0x06001AB8 RID: 6840 RVA: 0x000929C8 File Offset: 0x00090BC8
		public long state
		{
			get
			{
				return this._state;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._state = value;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x000929E0 File Offset: 0x00090BE0
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x000929F0 File Offset: 0x00090BF0
		// (set) Token: 0x06001ABB RID: 6843 RVA: 0x000929F8 File Offset: 0x00090BF8
		public long serverId
		{
			get
			{
				return this._serverId;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._serverId = value;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x00092A10 File Offset: 0x00090C10
		public bool HasServerId
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x00092A20 File Offset: 0x00090C20
		// (set) Token: 0x06001ABE RID: 6846 RVA: 0x00092A28 File Offset: 0x00090C28
		public long serverType
		{
			get
			{
				return this._serverType;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._serverType = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00092A40 File Offset: 0x00090C40
		public bool HasServerType
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x00092A50 File Offset: 0x00090C50
		// (set) Token: 0x06001AC1 RID: 6849 RVA: 0x00092A58 File Offset: 0x00090C58
		public long res_end_time1
		{
			get
			{
				return this._res_end_time1;
			}
			set
			{
				this.has_field.set_field(11, true);
				this._res_end_time1 = value;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x00092A70 File Offset: 0x00090C70
		public bool HasRes_end_time1
		{
			get
			{
				return this.has_field.has_field(11);
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x00092A80 File Offset: 0x00090C80
		// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x00092A88 File Offset: 0x00090C88
		public long res_end_time2
		{
			get
			{
				return this._res_end_time2;
			}
			set
			{
				this.has_field.set_field(12, true);
				this._res_end_time2 = value;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x00092AA0 File Offset: 0x00090CA0
		public bool HasRes_end_time2
		{
			get
			{
				return this.has_field.has_field(12);
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00092AB0 File Offset: 0x00090CB0
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.id = this.deserialize.read_string();
					break;
				case 1:
					this.max_donmin = this.deserialize.read_integer();
					break;
				case 2:
					this.donmin_time = this.deserialize.read_integer();
					break;
				case 3:
					this.end_time = this.deserialize.read_integer();
					break;
				case 4:
					this.res_time1 = this.deserialize.read_integer();
					break;
				case 5:
					this.res_count1 = this.deserialize.read_integer();
					break;
				case 6:
					this.res_time2 = this.deserialize.read_integer();
					break;
				case 7:
					this.res_count2 = this.deserialize.read_integer();
					break;
				case 8:
					this.state = this.deserialize.read_integer();
					break;
				case 9:
					this.serverId = this.deserialize.read_integer();
					break;
				case 10:
					this.serverType = this.deserialize.read_integer();
					break;
				case 11:
					this.res_end_time1 = this.deserialize.read_integer();
					break;
				case 12:
					this.res_end_time2 = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00092C48 File Offset: 0x00090E48
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.max_donmin, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.donmin_time, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.end_time, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.res_time1, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.res_count1, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.res_time2, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.res_count2, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.state, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_integer(this.serverId, 9);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_integer(this.serverType, 10);
			}
			if (this.has_field.has_field(11))
			{
				this.serialize.write_integer(this.res_end_time1, 11);
			}
			if (this.has_field.has_field(12))
			{
				this.serialize.write_integer(this.res_end_time2, 12);
			}
			return this.serialize.close();
		}

		// Token: 0x040019D9 RID: 6617
		private static int max_field_count = 13;

		// Token: 0x040019DA RID: 6618
		private string _id;

		// Token: 0x040019DB RID: 6619
		private long _max_donmin;

		// Token: 0x040019DC RID: 6620
		private long _donmin_time;

		// Token: 0x040019DD RID: 6621
		private long _end_time;

		// Token: 0x040019DE RID: 6622
		private long _res_time1;

		// Token: 0x040019DF RID: 6623
		private long _res_count1;

		// Token: 0x040019E0 RID: 6624
		private long _res_time2;

		// Token: 0x040019E1 RID: 6625
		private long _res_count2;

		// Token: 0x040019E2 RID: 6626
		private long _state;

		// Token: 0x040019E3 RID: 6627
		private long _serverId;

		// Token: 0x040019E4 RID: 6628
		private long _serverType;

		// Token: 0x040019E5 RID: 6629
		private long _res_end_time1;

		// Token: 0x040019E6 RID: 6630
		private long _res_end_time2;
	}
}
