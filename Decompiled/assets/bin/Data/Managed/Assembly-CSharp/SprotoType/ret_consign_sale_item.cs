using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000513 RID: 1299
	public class ret_consign_sale_item
	{
		// Token: 0x02000514 RID: 1300
		public class request : SprotoTypeBase
		{
			// Token: 0x06002604 RID: 9732 RVA: 0x000A90DC File Offset: 0x000A72DC
			public request() : base(ret_consign_sale_item.request.max_field_count)
			{
			}

			// Token: 0x06002605 RID: 9733 RVA: 0x000A90EC File Offset: 0x000A72EC
			public request(byte[] buffer) : base(ret_consign_sale_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A9F RID: 2719
			// (get) Token: 0x06002607 RID: 9735 RVA: 0x000A9108 File Offset: 0x000A7308
			// (set) Token: 0x06002608 RID: 9736 RVA: 0x000A9110 File Offset: 0x000A7310
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

			// Token: 0x17000AA0 RID: 2720
			// (get) Token: 0x06002609 RID: 9737 RVA: 0x000A9128 File Offset: 0x000A7328
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000AA1 RID: 2721
			// (get) Token: 0x0600260A RID: 9738 RVA: 0x000A9138 File Offset: 0x000A7338
			// (set) Token: 0x0600260B RID: 9739 RVA: 0x000A9140 File Offset: 0x000A7340
			public long success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._success = value;
				}
			}

			// Token: 0x17000AA2 RID: 2722
			// (get) Token: 0x0600260C RID: 9740 RVA: 0x000A9158 File Offset: 0x000A7358
			public bool HasSuccess
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000AA3 RID: 2723
			// (get) Token: 0x0600260D RID: 9741 RVA: 0x000A9168 File Offset: 0x000A7368
			// (set) Token: 0x0600260E RID: 9742 RVA: 0x000A9170 File Offset: 0x000A7370
			public gameitem gameitem
			{
				get
				{
					return this._gameitem;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._gameitem = value;
				}
			}

			// Token: 0x17000AA4 RID: 2724
			// (get) Token: 0x0600260F RID: 9743 RVA: 0x000A9188 File Offset: 0x000A7388
			public bool HasGameitem
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000AA5 RID: 2725
			// (get) Token: 0x06002610 RID: 9744 RVA: 0x000A9198 File Offset: 0x000A7398
			// (set) Token: 0x06002611 RID: 9745 RVA: 0x000A91A0 File Offset: 0x000A73A0
			public long itemType
			{
				get
				{
					return this._itemType;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._itemType = value;
				}
			}

			// Token: 0x17000AA6 RID: 2726
			// (get) Token: 0x06002612 RID: 9746 RVA: 0x000A91B8 File Offset: 0x000A73B8
			public bool HasItemType
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06002613 RID: 9747 RVA: 0x000A91C8 File Offset: 0x000A73C8
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
						this.success = this.deserialize.read_integer();
						break;
					case 2:
						this.gameitem = this.deserialize.read_obj<gameitem>();
						break;
					case 3:
						this.itemType = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002614 RID: 9748 RVA: 0x000A9274 File Offset: 0x000A7474
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.success, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_obj(this.gameitem, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.itemType, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CD6 RID: 7382
			private static int max_field_count = 4;

			// Token: 0x04001CD7 RID: 7383
			private long _indexId;

			// Token: 0x04001CD8 RID: 7384
			private long _success;

			// Token: 0x04001CD9 RID: 7385
			private gameitem _gameitem;

			// Token: 0x04001CDA RID: 7386
			private long _itemType;
		}
	}
}
