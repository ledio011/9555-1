using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000367 RID: 871
	public class copyscene_info : SprotoTypeBase
	{
		// Token: 0x060019EE RID: 6638 RVA: 0x000910EC File Offset: 0x0008F2EC
		public copyscene_info() : base(copyscene_info.max_field_count)
		{
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000910FC File Offset: 0x0008F2FC
		public copyscene_info(byte[] buffer) : base(copyscene_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00091118 File Offset: 0x0008F318
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x00091120 File Offset: 0x0008F320
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._ID = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x00091138 File Offset: 0x0008F338
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x00091148 File Offset: 0x0008F348
		// (set) Token: 0x060019F5 RID: 6645 RVA: 0x00091150 File Offset: 0x0008F350
		public long CurNum
		{
			get
			{
				return this._CurNum;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._CurNum = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x00091168 File Offset: 0x0008F368
		public bool HasCurNum
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x00091178 File Offset: 0x0008F378
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x00091180 File Offset: 0x0008F380
		public long BestGrade
		{
			get
			{
				return this._BestGrade;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._BestGrade = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x00091198 File Offset: 0x0008F398
		public bool HasBestGrade
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x000911A8 File Offset: 0x0008F3A8
		// (set) Token: 0x060019FB RID: 6651 RVA: 0x000911B0 File Offset: 0x0008F3B0
		public long Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._Type = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x000911C8 File Offset: 0x0008F3C8
		public bool HasType
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x000911D8 File Offset: 0x0008F3D8
		// (set) Token: 0x060019FE RID: 6654 RVA: 0x000911E0 File Offset: 0x0008F3E0
		public string str
		{
			get
			{
				return this._str;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._str = value;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x000911F8 File Offset: 0x0008F3F8
		public bool HasStr
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x00091208 File Offset: 0x0008F408
		// (set) Token: 0x06001A01 RID: 6657 RVA: 0x00091210 File Offset: 0x0008F410
		public bool enable
		{
			get
			{
				return this._enable;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._enable = value;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00091228 File Offset: 0x0008F428
		public bool HasEnable
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x00091238 File Offset: 0x0008F438
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x00091240 File Offset: 0x0008F440
		public long state
		{
			get
			{
				return this._state;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._state = value;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x00091258 File Offset: 0x0008F458
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x00091268 File Offset: 0x0008F468
		// (set) Token: 0x06001A07 RID: 6663 RVA: 0x00091270 File Offset: 0x0008F470
		public long Type2
		{
			get
			{
				return this._Type2;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._Type2 = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x00091288 File Offset: 0x0008F488
		public bool HasType2
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00091298 File Offset: 0x0008F498
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.ID = this.deserialize.read_string();
					break;
				case 1:
					this.CurNum = this.deserialize.read_integer();
					break;
				case 2:
					this.BestGrade = this.deserialize.read_integer();
					break;
				case 3:
					this.Type = this.deserialize.read_integer();
					break;
				case 4:
					this.str = this.deserialize.read_string();
					break;
				case 5:
					this.enable = this.deserialize.read_boolean();
					break;
				case 6:
					this.state = this.deserialize.read_integer();
					break;
				case 7:
					this.Type2 = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000913AC File Offset: 0x0008F5AC
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.ID, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.CurNum, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.BestGrade, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.Type, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_string(this.str, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_boolean(this.enable, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.state, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.Type2, 7);
			}
			return this.serialize.close();
		}

		// Token: 0x040019A7 RID: 6567
		private static int max_field_count = 8;

		// Token: 0x040019A8 RID: 6568
		private string _ID;

		// Token: 0x040019A9 RID: 6569
		private long _CurNum;

		// Token: 0x040019AA RID: 6570
		private long _BestGrade;

		// Token: 0x040019AB RID: 6571
		private long _Type;

		// Token: 0x040019AC RID: 6572
		private string _str;

		// Token: 0x040019AD RID: 6573
		private bool _enable;

		// Token: 0x040019AE RID: 6574
		private long _state;

		// Token: 0x040019AF RID: 6575
		private long _Type2;
	}
}
