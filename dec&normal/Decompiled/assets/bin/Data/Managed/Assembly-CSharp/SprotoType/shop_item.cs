using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005D0 RID: 1488
	public class shop_item : SprotoTypeBase
	{
		// Token: 0x06002AE1 RID: 10977 RVA: 0x000B2B40 File Offset: 0x000B0D40
		public shop_item() : base(shop_item.max_field_count)
		{
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000B2B50 File Offset: 0x000B0D50
		public shop_item(byte[] buffer) : base(shop_item.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x000B2B70 File Offset: 0x000B0D70
		// (set) Token: 0x06002AE5 RID: 10981 RVA: 0x000B2B78 File Offset: 0x000B0D78
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

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x000B2B90 File Offset: 0x000B0D90
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x000B2BA0 File Offset: 0x000B0DA0
		// (set) Token: 0x06002AE8 RID: 10984 RVA: 0x000B2BA8 File Offset: 0x000B0DA8
		public string ItemID
		{
			get
			{
				return this._ItemID;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._ItemID = value;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x000B2BC0 File Offset: 0x000B0DC0
		public bool HasItemID
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x000B2BD0 File Offset: 0x000B0DD0
		// (set) Token: 0x06002AEB RID: 10987 RVA: 0x000B2BD8 File Offset: 0x000B0DD8
		public long Quality
		{
			get
			{
				return this._Quality;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._Quality = value;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x000B2BF0 File Offset: 0x000B0DF0
		public bool HasQuality
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x000B2C00 File Offset: 0x000B0E00
		// (set) Token: 0x06002AEE RID: 10990 RVA: 0x000B2C08 File Offset: 0x000B0E08
		public long PriceType
		{
			get
			{
				return this._PriceType;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._PriceType = value;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x000B2C20 File Offset: 0x000B0E20
		public bool HasPriceType
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x000B2C30 File Offset: 0x000B0E30
		// (set) Token: 0x06002AF1 RID: 10993 RVA: 0x000B2C38 File Offset: 0x000B0E38
		public long Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._Price = value;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000B2C50 File Offset: 0x000B0E50
		public bool HasPrice
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x000B2C60 File Offset: 0x000B0E60
		// (set) Token: 0x06002AF4 RID: 10996 RVA: 0x000B2C68 File Offset: 0x000B0E68
		public long Limit
		{
			get
			{
				return this._Limit;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._Limit = value;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x000B2C80 File Offset: 0x000B0E80
		public bool HasLimit
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000B2C90 File Offset: 0x000B0E90
		// (set) Token: 0x06002AF7 RID: 10999 RVA: 0x000B2C98 File Offset: 0x000B0E98
		public long curNum
		{
			get
			{
				return this._curNum;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._curNum = value;
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x000B2CB0 File Offset: 0x000B0EB0
		public bool HasCurNum
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06002AF9 RID: 11001 RVA: 0x000B2CC0 File Offset: 0x000B0EC0
		// (set) Token: 0x06002AFA RID: 11002 RVA: 0x000B2CC8 File Offset: 0x000B0EC8
		public long Discount
		{
			get
			{
				return this._Discount;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._Discount = value;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x000B2CE0 File Offset: 0x000B0EE0
		public bool HasDiscount
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x000B2CF0 File Offset: 0x000B0EF0
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x000B2CF8 File Offset: 0x000B0EF8
		public long Class
		{
			get
			{
				return this._Class;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._Class = value;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000B2D10 File Offset: 0x000B0F10
		public bool HasClass
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000B2D20 File Offset: 0x000B0F20
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.ID = this.deserialize.read_string();
					continue;
				case 1:
					this.ItemID = this.deserialize.read_string();
					continue;
				case 2:
					this.Quality = this.deserialize.read_integer();
					continue;
				case 3:
					this.PriceType = this.deserialize.read_integer();
					continue;
				case 4:
					this.Price = this.deserialize.read_integer();
					continue;
				case 5:
					this.Limit = this.deserialize.read_integer();
					continue;
				case 7:
					this.curNum = this.deserialize.read_integer();
					continue;
				case 8:
					this.Discount = this.deserialize.read_integer();
					continue;
				case 9:
					this.Class = this.deserialize.read_integer();
					continue;
				}
				this.deserialize.read_unknow_data();
			}
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000B2E54 File Offset: 0x000B1054
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.ID, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.ItemID, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.Quality, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.PriceType, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.Price, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.Limit, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.curNum, 7);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.Discount, 8);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.Class, 9);
			}
			return this.serialize.close();
		}

		// Token: 0x04001E2D RID: 7725
		private static int max_field_count = 10;

		// Token: 0x04001E2E RID: 7726
		private string _ID;

		// Token: 0x04001E2F RID: 7727
		private string _ItemID;

		// Token: 0x04001E30 RID: 7728
		private long _Quality;

		// Token: 0x04001E31 RID: 7729
		private long _PriceType;

		// Token: 0x04001E32 RID: 7730
		private long _Price;

		// Token: 0x04001E33 RID: 7731
		private long _Limit;

		// Token: 0x04001E34 RID: 7732
		private long _curNum;

		// Token: 0x04001E35 RID: 7733
		private long _Discount;

		// Token: 0x04001E36 RID: 7734
		private long _Class;
	}
}
