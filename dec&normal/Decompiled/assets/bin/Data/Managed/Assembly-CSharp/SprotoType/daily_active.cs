using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200036A RID: 874
	public class daily_active : SprotoTypeBase
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x00091660 File Offset: 0x0008F860
		public daily_active() : base(daily_active.max_field_count)
		{
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00091670 File Offset: 0x0008F870
		public daily_active(byte[] buffer) : base(daily_active.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x0009168C File Offset: 0x0008F88C
		// (set) Token: 0x06001A1B RID: 6683 RVA: 0x00091694 File Offset: 0x0008F894
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

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x000916AC File Offset: 0x0008F8AC
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x000916BC File Offset: 0x0008F8BC
		// (set) Token: 0x06001A1E RID: 6686 RVA: 0x000916C4 File Offset: 0x0008F8C4
		public long count
		{
			get
			{
				return this._count;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._count = value;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x000916DC File Offset: 0x0008F8DC
		public bool HasCount
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x000916EC File Offset: 0x0008F8EC
		// (set) Token: 0x06001A21 RID: 6689 RVA: 0x000916F4 File Offset: 0x0008F8F4
		public long Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._Type = value;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x0009170C File Offset: 0x0008F90C
		public bool HasType
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0009171C File Offset: 0x0008F91C
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
					this.count = this.deserialize.read_integer();
					break;
				case 2:
					this.Type = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x000917B0 File Offset: 0x0008F9B0
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.ID, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.count, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.Type, 2);
			}
			return this.serialize.close();
		}

		// Token: 0x040019B3 RID: 6579
		private static int max_field_count = 3;

		// Token: 0x040019B4 RID: 6580
		private string _ID;

		// Token: 0x040019B5 RID: 6581
		private long _count;

		// Token: 0x040019B6 RID: 6582
		private long _Type;
	}
}
