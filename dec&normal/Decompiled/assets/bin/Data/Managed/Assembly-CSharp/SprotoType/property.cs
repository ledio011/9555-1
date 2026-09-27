using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000446 RID: 1094
	public class property : SprotoTypeBase
	{
		// Token: 0x06002219 RID: 8729 RVA: 0x000A1ED0 File Offset: 0x000A00D0
		public property() : base(property.max_field_count)
		{
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000A1EE0 File Offset: 0x000A00E0
		public property(byte[] buffer) : base(property.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x000A1EFC File Offset: 0x000A00FC
		// (set) Token: 0x0600221D RID: 8733 RVA: 0x000A1F04 File Offset: 0x000A0104
		public long money1
		{
			get
			{
				return this._money1;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._money1 = value;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x000A1F1C File Offset: 0x000A011C
		public bool HasMoney1
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x000A1F2C File Offset: 0x000A012C
		// (set) Token: 0x06002220 RID: 8736 RVA: 0x000A1F34 File Offset: 0x000A0134
		public long money2
		{
			get
			{
				return this._money2;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._money2 = value;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x000A1F4C File Offset: 0x000A014C
		public bool HasMoney2
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x000A1F5C File Offset: 0x000A015C
		// (set) Token: 0x06002223 RID: 8739 RVA: 0x000A1F64 File Offset: 0x000A0164
		public long money3
		{
			get
			{
				return this._money3;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._money3 = value;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x000A1F7C File Offset: 0x000A017C
		public bool HasMoney3
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x000A1F8C File Offset: 0x000A018C
		// (set) Token: 0x06002226 RID: 8742 RVA: 0x000A1F94 File Offset: 0x000A0194
		public long money4
		{
			get
			{
				return this._money4;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._money4 = value;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x000A1FAC File Offset: 0x000A01AC
		public bool HasMoney4
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x000A1FBC File Offset: 0x000A01BC
		// (set) Token: 0x06002229 RID: 8745 RVA: 0x000A1FC4 File Offset: 0x000A01C4
		public long money5
		{
			get
			{
				return this._money5;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._money5 = value;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x000A1FDC File Offset: 0x000A01DC
		public bool HasMoney5
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x000A1FEC File Offset: 0x000A01EC
		// (set) Token: 0x0600222C RID: 8748 RVA: 0x000A1FF4 File Offset: 0x000A01F4
		public long money6
		{
			get
			{
				return this._money6;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._money6 = value;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x000A200C File Offset: 0x000A020C
		public bool HasMoney6
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000A201C File Offset: 0x000A021C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 13:
					this.money1 = this.deserialize.read_integer();
					break;
				case 14:
					this.money2 = this.deserialize.read_integer();
					break;
				case 15:
					this.money3 = this.deserialize.read_integer();
					break;
				case 16:
					this.money4 = this.deserialize.read_integer();
					break;
				case 17:
					this.money5 = this.deserialize.read_integer();
					break;
				case 18:
					this.money6 = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x000A2100 File Offset: 0x000A0300
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.money1, 13);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.money2, 14);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.money3, 15);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.money4, 16);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.money5, 17);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.money6, 18);
			}
			return this.serialize.close();
		}

		// Token: 0x04001BED RID: 7149
		private static int max_field_count = 7;

		// Token: 0x04001BEE RID: 7150
		private long _money1;

		// Token: 0x04001BEF RID: 7151
		private long _money2;

		// Token: 0x04001BF0 RID: 7152
		private long _money3;

		// Token: 0x04001BF1 RID: 7153
		private long _money4;

		// Token: 0x04001BF2 RID: 7154
		private long _money5;

		// Token: 0x04001BF3 RID: 7155
		private long _money6;
	}
}
