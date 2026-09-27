using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200030C RID: 780
	public class ask_shop_list
	{
		// Token: 0x0200030D RID: 781
		public class request : SprotoTypeBase
		{
			// Token: 0x060015BE RID: 5566 RVA: 0x00088310 File Offset: 0x00086510
			public request() : base(ask_shop_list.request.max_field_count)
			{
			}

			// Token: 0x060015BF RID: 5567 RVA: 0x00088320 File Offset: 0x00086520
			public request(byte[] buffer) : base(ask_shop_list.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700043D RID: 1085
			// (get) Token: 0x060015C1 RID: 5569 RVA: 0x0008833C File Offset: 0x0008653C
			// (set) Token: 0x060015C2 RID: 5570 RVA: 0x00088344 File Offset: 0x00086544
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._type = value;
				}
			}

			// Token: 0x1700043E RID: 1086
			// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0008835C File Offset: 0x0008655C
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700043F RID: 1087
			// (get) Token: 0x060015C4 RID: 5572 RVA: 0x0008836C File Offset: 0x0008656C
			// (set) Token: 0x060015C5 RID: 5573 RVA: 0x00088374 File Offset: 0x00086574
			public long curPage
			{
				get
				{
					return this._curPage;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._curPage = value;
				}
			}

			// Token: 0x17000440 RID: 1088
			// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0008838C File Offset: 0x0008658C
			public bool HasCurPage
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000441 RID: 1089
			// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0008839C File Offset: 0x0008659C
			// (set) Token: 0x060015C8 RID: 5576 RVA: 0x000883A4 File Offset: 0x000865A4
			public string itemId
			{
				get
				{
					return this._itemId;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._itemId = value;
				}
			}

			// Token: 0x17000442 RID: 1090
			// (get) Token: 0x060015C9 RID: 5577 RVA: 0x000883BC File Offset: 0x000865BC
			public bool HasItemId
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000443 RID: 1091
			// (get) Token: 0x060015CA RID: 5578 RVA: 0x000883CC File Offset: 0x000865CC
			// (set) Token: 0x060015CB RID: 5579 RVA: 0x000883D4 File Offset: 0x000865D4
			public long subType
			{
				get
				{
					return this._subType;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._subType = value;
				}
			}

			// Token: 0x17000444 RID: 1092
			// (get) Token: 0x060015CC RID: 5580 RVA: 0x000883EC File Offset: 0x000865EC
			public bool HasSubType
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000445 RID: 1093
			// (get) Token: 0x060015CD RID: 5581 RVA: 0x000883FC File Offset: 0x000865FC
			// (set) Token: 0x060015CE RID: 5582 RVA: 0x00088404 File Offset: 0x00086604
			public long class1
			{
				get
				{
					return this._class1;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._class1 = value;
				}
			}

			// Token: 0x17000446 RID: 1094
			// (get) Token: 0x060015CF RID: 5583 RVA: 0x0008841C File Offset: 0x0008661C
			public bool HasClass1
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000447 RID: 1095
			// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0008842C File Offset: 0x0008662C
			// (set) Token: 0x060015D1 RID: 5585 RVA: 0x00088434 File Offset: 0x00086634
			public long special
			{
				get
				{
					return this._special;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._special = value;
				}
			}

			// Token: 0x17000448 RID: 1096
			// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0008844C File Offset: 0x0008664C
			public bool HasSpecial
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x060015D3 RID: 5587 RVA: 0x0008845C File Offset: 0x0008665C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.type = this.deserialize.read_integer();
						break;
					case 1:
						this.curPage = this.deserialize.read_integer();
						break;
					case 2:
						this.itemId = this.deserialize.read_string();
						break;
					case 3:
						this.subType = this.deserialize.read_integer();
						break;
					case 4:
						this.class1 = this.deserialize.read_integer();
						break;
					case 5:
						this.special = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060015D4 RID: 5588 RVA: 0x0008853C File Offset: 0x0008673C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.curPage, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.itemId, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.subType, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.class1, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.special, 5);
				}
				return this.serialize.close();
			}

			// Token: 0x0400186B RID: 6251
			private static int max_field_count = 6;

			// Token: 0x0400186C RID: 6252
			private long _type;

			// Token: 0x0400186D RID: 6253
			private long _curPage;

			// Token: 0x0400186E RID: 6254
			private string _itemId;

			// Token: 0x0400186F RID: 6255
			private long _subType;

			// Token: 0x04001870 RID: 6256
			private long _class1;

			// Token: 0x04001871 RID: 6257
			private long _special;
		}
	}
}
