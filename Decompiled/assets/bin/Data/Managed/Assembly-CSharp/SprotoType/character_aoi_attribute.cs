using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200033F RID: 831
	public class character_aoi_attribute : SprotoTypeBase
	{
		// Token: 0x0600182D RID: 6189 RVA: 0x0008D600 File Offset: 0x0008B800
		public character_aoi_attribute() : base(character_aoi_attribute.max_field_count)
		{
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0008D610 File Offset: 0x0008B810
		public character_aoi_attribute(byte[] buffer) : base(character_aoi_attribute.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x0008D62C File Offset: 0x0008B82C
		// (set) Token: 0x06001831 RID: 6193 RVA: 0x0008D634 File Offset: 0x0008B834
		public long id
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

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0008D64C File Offset: 0x0008B84C
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x0008D65C File Offset: 0x0008B85C
		// (set) Token: 0x06001834 RID: 6196 RVA: 0x0008D664 File Offset: 0x0008B864
		public attribute_other attribute_other
		{
			get
			{
				return this._attribute_other;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._attribute_other = value;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0008D67C File Offset: 0x0008B87C
		public bool HasAttribute_other
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x0008D68C File Offset: 0x0008B88C
		// (set) Token: 0x06001837 RID: 6199 RVA: 0x0008D694 File Offset: 0x0008B894
		public attribute attribute
		{
			get
			{
				return this._attribute;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._attribute = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x0008D6AC File Offset: 0x0008B8AC
		public bool HasAttribute
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x0008D6BC File Offset: 0x0008B8BC
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x0008D6C4 File Offset: 0x0008B8C4
		public attribute attribute_all
		{
			get
			{
				return this._attribute_all;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._attribute_all = value;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x0008D6DC File Offset: 0x0008B8DC
		public bool HasAttribute_all
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x0008D6EC File Offset: 0x0008B8EC
		// (set) Token: 0x0600183D RID: 6205 RVA: 0x0008D6F4 File Offset: 0x0008B8F4
		public characterVisual visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._visual = value;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x0008D70C File Offset: 0x0008B90C
		public bool HasVisual
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x0008D71C File Offset: 0x0008B91C
		// (set) Token: 0x06001840 RID: 6208 RVA: 0x0008D724 File Offset: 0x0008B924
		public property property
		{
			get
			{
				return this._property;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._property = value;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x0008D73C File Offset: 0x0008B93C
		public bool HasProperty
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0008D74C File Offset: 0x0008B94C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.id = this.deserialize.read_integer();
					break;
				case 1:
					this.attribute_other = this.deserialize.read_obj<attribute_other>();
					break;
				case 2:
					this.attribute = this.deserialize.read_obj<attribute>();
					break;
				case 3:
					this.attribute_all = this.deserialize.read_obj<attribute>();
					break;
				case 4:
					this.visual = this.deserialize.read_obj<characterVisual>();
					break;
				case 5:
					this.property = this.deserialize.read_obj<property>();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0008D82C File Offset: 0x0008BA2C
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_obj(this.attribute_other, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_obj(this.attribute, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_obj(this.attribute_all, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_obj(this.visual, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_obj(this.property, 5);
			}
			return this.serialize.close();
		}

		// Token: 0x04001923 RID: 6435
		private static int max_field_count = 6;

		// Token: 0x04001924 RID: 6436
		private long _id;

		// Token: 0x04001925 RID: 6437
		private attribute_other _attribute_other;

		// Token: 0x04001926 RID: 6438
		private attribute _attribute;

		// Token: 0x04001927 RID: 6439
		private attribute _attribute_all;

		// Token: 0x04001928 RID: 6440
		private characterVisual _visual;

		// Token: 0x04001929 RID: 6441
		private property _property;
	}
}
