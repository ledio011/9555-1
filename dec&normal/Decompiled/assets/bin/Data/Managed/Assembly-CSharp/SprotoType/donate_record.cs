using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000375 RID: 885
	public class donate_record : SprotoTypeBase
	{
		// Token: 0x06001AC8 RID: 6856 RVA: 0x00092E3C File Offset: 0x0009103C
		public donate_record() : base(donate_record.max_field_count)
		{
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00092E4C File Offset: 0x0009104C
		public donate_record(byte[] buffer) : base(donate_record.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x00092E68 File Offset: 0x00091068
		// (set) Token: 0x06001ACC RID: 6860 RVA: 0x00092E70 File Offset: 0x00091070
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

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x00092E88 File Offset: 0x00091088
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00092E98 File Offset: 0x00091098
		// (set) Token: 0x06001ACF RID: 6863 RVA: 0x00092EA0 File Offset: 0x000910A0
		public long DonateCount
		{
			get
			{
				return this._DonateCount;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._DonateCount = value;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00092EB8 File Offset: 0x000910B8
		public bool HasDonateCount
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00092EC8 File Offset: 0x000910C8
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 != 1)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.DonateCount = this.deserialize.read_integer();
					}
				}
				else
				{
					this.id = this.deserialize.read_string();
				}
			}
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00092F40 File Offset: 0x00091140
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.DonateCount, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x040019E7 RID: 6631
		private static int max_field_count = 2;

		// Token: 0x040019E8 RID: 6632
		private string _id;

		// Token: 0x040019E9 RID: 6633
		private long _DonateCount;
	}
}
