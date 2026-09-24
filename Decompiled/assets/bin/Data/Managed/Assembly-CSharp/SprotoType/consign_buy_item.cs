using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200035A RID: 858
	public class consign_buy_item
	{
		// Token: 0x0200035B RID: 859
		public class request : SprotoTypeBase
		{
			// Token: 0x06001975 RID: 6517 RVA: 0x00090140 File Offset: 0x0008E340
			public request() : base(consign_buy_item.request.max_field_count)
			{
			}

			// Token: 0x06001976 RID: 6518 RVA: 0x00090150 File Offset: 0x0008E350
			public request(byte[] buffer) : base(consign_buy_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06001978 RID: 6520 RVA: 0x0009016C File Offset: 0x0008E36C
			// (set) Token: 0x06001979 RID: 6521 RVA: 0x00090174 File Offset: 0x0008E374
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

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x0600197A RID: 6522 RVA: 0x0009018C File Offset: 0x0008E38C
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x0600197B RID: 6523 RVA: 0x0009019C File Offset: 0x0008E39C
			// (set) Token: 0x0600197C RID: 6524 RVA: 0x000901A4 File Offset: 0x0008E3A4
			public string itemId
			{
				get
				{
					return this._itemId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._itemId = value;
				}
			}

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x0600197D RID: 6525 RVA: 0x000901BC File Offset: 0x0008E3BC
			public bool HasItemId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600197E RID: 6526 RVA: 0x000901CC File Offset: 0x0008E3CC
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
							this.itemId = this.deserialize.read_string();
						}
					}
					else
					{
						this.id = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600197F RID: 6527 RVA: 0x00090244 File Offset: 0x0008E444
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.itemId, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001985 RID: 6533
			private static int max_field_count = 2;

			// Token: 0x04001986 RID: 6534
			private long _id;

			// Token: 0x04001987 RID: 6535
			private string _itemId;
		}
	}
}
