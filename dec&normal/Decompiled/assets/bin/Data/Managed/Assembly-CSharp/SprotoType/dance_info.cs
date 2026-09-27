using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200036F RID: 879
	public class dance_info : SprotoTypeBase
	{
		// Token: 0x06001A54 RID: 6740 RVA: 0x00091E70 File Offset: 0x00090070
		public dance_info() : base(dance_info.max_field_count)
		{
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00091E80 File Offset: 0x00090080
		public dance_info(byte[] buffer) : base(dance_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x00091E9C File Offset: 0x0009009C
		// (set) Token: 0x06001A58 RID: 6744 RVA: 0x00091EA4 File Offset: 0x000900A4
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

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x00091EBC File Offset: 0x000900BC
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x00091ECC File Offset: 0x000900CC
		// (set) Token: 0x06001A5B RID: 6747 RVA: 0x00091ED4 File Offset: 0x000900D4
		public bool enable
		{
			get
			{
				return this._enable;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._enable = value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x00091EEC File Offset: 0x000900EC
		public bool HasEnable
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x00091EFC File Offset: 0x000900FC
		// (set) Token: 0x06001A5E RID: 6750 RVA: 0x00091F04 File Offset: 0x00090104
		public long useType
		{
			get
			{
				return this._useType;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._useType = value;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x00091F1C File Offset: 0x0009011C
		public bool HasUseType
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x00091F2C File Offset: 0x0009012C
		// (set) Token: 0x06001A61 RID: 6753 RVA: 0x00091F34 File Offset: 0x00090134
		public long endTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._endTime = value;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x00091F4C File Offset: 0x0009014C
		public bool HasEndTime
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00091F5C File Offset: 0x0009015C
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
					this.enable = this.deserialize.read_boolean();
					break;
				case 2:
					this.useType = this.deserialize.read_integer();
					break;
				case 3:
					this.endTime = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00092008 File Offset: 0x00090208
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.ID, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_boolean(this.enable, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.useType, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.endTime, 3);
			}
			return this.serialize.close();
		}

		// Token: 0x040019C4 RID: 6596
		private static int max_field_count = 4;

		// Token: 0x040019C5 RID: 6597
		private string _ID;

		// Token: 0x040019C6 RID: 6598
		private bool _enable;

		// Token: 0x040019C7 RID: 6599
		private long _useType;

		// Token: 0x040019C8 RID: 6600
		private long _endTime;
	}
}
