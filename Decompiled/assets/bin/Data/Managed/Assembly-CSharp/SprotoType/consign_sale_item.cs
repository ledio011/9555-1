using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200035F RID: 863
	public class consign_sale_item
	{
		// Token: 0x02000360 RID: 864
		public class request : SprotoTypeBase
		{
			// Token: 0x060019A7 RID: 6567 RVA: 0x000907BC File Offset: 0x0008E9BC
			public request() : base(consign_sale_item.request.max_field_count)
			{
			}

			// Token: 0x060019A8 RID: 6568 RVA: 0x000907CC File Offset: 0x0008E9CC
			public request(byte[] buffer) : base(consign_sale_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700060F RID: 1551
			// (get) Token: 0x060019AA RID: 6570 RVA: 0x000907E8 File Offset: 0x0008E9E8
			// (set) Token: 0x060019AB RID: 6571 RVA: 0x000907F0 File Offset: 0x0008E9F0
			public long indexId
			{
				get
				{
					return this._indexId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._indexId = value;
				}
			}

			// Token: 0x17000610 RID: 1552
			// (get) Token: 0x060019AC RID: 6572 RVA: 0x00090808 File Offset: 0x0008EA08
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000611 RID: 1553
			// (get) Token: 0x060019AD RID: 6573 RVA: 0x00090818 File Offset: 0x0008EA18
			// (set) Token: 0x060019AE RID: 6574 RVA: 0x00090820 File Offset: 0x0008EA20
			public long itemCount
			{
				get
				{
					return this._itemCount;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._itemCount = value;
				}
			}

			// Token: 0x17000612 RID: 1554
			// (get) Token: 0x060019AF RID: 6575 RVA: 0x00090838 File Offset: 0x0008EA38
			public bool HasItemCount
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000613 RID: 1555
			// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00090848 File Offset: 0x0008EA48
			// (set) Token: 0x060019B1 RID: 6577 RVA: 0x00090850 File Offset: 0x0008EA50
			public long price
			{
				get
				{
					return this._price;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._price = value;
				}
			}

			// Token: 0x17000614 RID: 1556
			// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00090868 File Offset: 0x0008EA68
			public bool HasPrice
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000615 RID: 1557
			// (get) Token: 0x060019B3 RID: 6579 RVA: 0x00090878 File Offset: 0x0008EA78
			// (set) Token: 0x060019B4 RID: 6580 RVA: 0x00090880 File Offset: 0x0008EA80
			public long timeType
			{
				get
				{
					return this._timeType;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._timeType = value;
				}
			}

			// Token: 0x17000616 RID: 1558
			// (get) Token: 0x060019B5 RID: 6581 RVA: 0x00090898 File Offset: 0x0008EA98
			public bool HasTimeType
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000617 RID: 1559
			// (get) Token: 0x060019B6 RID: 6582 RVA: 0x000908A8 File Offset: 0x0008EAA8
			// (set) Token: 0x060019B7 RID: 6583 RVA: 0x000908B0 File Offset: 0x0008EAB0
			public long itemType
			{
				get
				{
					return this._itemType;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._itemType = value;
				}
			}

			// Token: 0x17000618 RID: 1560
			// (get) Token: 0x060019B8 RID: 6584 RVA: 0x000908C8 File Offset: 0x0008EAC8
			public bool HasItemType
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x060019B9 RID: 6585 RVA: 0x000908D8 File Offset: 0x0008EAD8
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.indexId = this.deserialize.read_integer();
						break;
					case 1:
						this.itemCount = this.deserialize.read_integer();
						break;
					case 2:
						this.price = this.deserialize.read_integer();
						break;
					case 3:
						this.timeType = this.deserialize.read_integer();
						break;
					case 4:
						this.itemType = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060019BA RID: 6586 RVA: 0x000909A0 File Offset: 0x0008EBA0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.itemCount, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.price, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.timeType, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.itemType, 4);
				}
				return this.serialize.close();
			}

			// Token: 0x04001993 RID: 6547
			private static int max_field_count = 5;

			// Token: 0x04001994 RID: 6548
			private long _indexId;

			// Token: 0x04001995 RID: 6549
			private long _itemCount;

			// Token: 0x04001996 RID: 6550
			private long _price;

			// Token: 0x04001997 RID: 6551
			private long _timeType;

			// Token: 0x04001998 RID: 6552
			private long _itemType;
		}
	}
}
