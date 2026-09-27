using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200036E RID: 878
	public class damage_list : SprotoTypeBase
	{
		// Token: 0x06001A46 RID: 6726 RVA: 0x00091C90 File Offset: 0x0008FE90
		public damage_list() : base(damage_list.max_field_count)
		{
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00091CA0 File Offset: 0x0008FEA0
		public damage_list(byte[] buffer) : base(damage_list.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x00091CBC File Offset: 0x0008FEBC
		// (set) Token: 0x06001A4A RID: 6730 RVA: 0x00091CC4 File Offset: 0x0008FEC4
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._name = value;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x00091CDC File Offset: 0x0008FEDC
		public bool HasName
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00091CEC File Offset: 0x0008FEEC
		// (set) Token: 0x06001A4D RID: 6733 RVA: 0x00091CF4 File Offset: 0x0008FEF4
		public long damage
		{
			get
			{
				return this._damage;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._damage = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x00091D0C File Offset: 0x0008FF0C
		public bool HasDamage
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x00091D1C File Offset: 0x0008FF1C
		// (set) Token: 0x06001A50 RID: 6736 RVA: 0x00091D24 File Offset: 0x0008FF24
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._id = value;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x00091D3C File Offset: 0x0008FF3C
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00091D4C File Offset: 0x0008FF4C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.name = this.deserialize.read_string();
					break;
				case 1:
					this.damage = this.deserialize.read_integer();
					break;
				case 2:
					this.id = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00091DE0 File Offset: 0x0008FFE0
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.name, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.damage, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.id, 2);
			}
			return this.serialize.close();
		}

		// Token: 0x040019C0 RID: 6592
		private static int max_field_count = 3;

		// Token: 0x040019C1 RID: 6593
		private string _name;

		// Token: 0x040019C2 RID: 6594
		private long _damage;

		// Token: 0x040019C3 RID: 6595
		private long _id;
	}
}
