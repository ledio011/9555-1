using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005BE RID: 1470
	public class sell_item
	{
		// Token: 0x020005BF RID: 1471
		public class request : SprotoTypeBase
		{
			// Token: 0x06002A6D RID: 10861 RVA: 0x000B1CC0 File Offset: 0x000AFEC0
			public request() : base(sell_item.request.max_field_count)
			{
			}

			// Token: 0x06002A6E RID: 10862 RVA: 0x000B1CD0 File Offset: 0x000AFED0
			public request(byte[] buffer) : base(sell_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C1F RID: 3103
			// (get) Token: 0x06002A70 RID: 10864 RVA: 0x000B1CEC File Offset: 0x000AFEEC
			// (set) Token: 0x06002A71 RID: 10865 RVA: 0x000B1CF4 File Offset: 0x000AFEF4
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

			// Token: 0x17000C20 RID: 3104
			// (get) Token: 0x06002A72 RID: 10866 RVA: 0x000B1D0C File Offset: 0x000AFF0C
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C21 RID: 3105
			// (get) Token: 0x06002A73 RID: 10867 RVA: 0x000B1D1C File Offset: 0x000AFF1C
			// (set) Token: 0x06002A74 RID: 10868 RVA: 0x000B1D24 File Offset: 0x000AFF24
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

			// Token: 0x17000C22 RID: 3106
			// (get) Token: 0x06002A75 RID: 10869 RVA: 0x000B1D3C File Offset: 0x000AFF3C
			public bool HasItemCount
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000C23 RID: 3107
			// (get) Token: 0x06002A76 RID: 10870 RVA: 0x000B1D4C File Offset: 0x000AFF4C
			// (set) Token: 0x06002A77 RID: 10871 RVA: 0x000B1D54 File Offset: 0x000AFF54
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._type = value;
				}
			}

			// Token: 0x17000C24 RID: 3108
			// (get) Token: 0x06002A78 RID: 10872 RVA: 0x000B1D6C File Offset: 0x000AFF6C
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002A79 RID: 10873 RVA: 0x000B1D7C File Offset: 0x000AFF7C
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
						this.type = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002A7A RID: 10874 RVA: 0x000B1E10 File Offset: 0x000B0010
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
					this.serialize.write_integer(this.type, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E0F RID: 7695
			private static int max_field_count = 3;

			// Token: 0x04001E10 RID: 7696
			private long _indexId;

			// Token: 0x04001E11 RID: 7697
			private long _itemCount;

			// Token: 0x04001E12 RID: 7698
			private long _type;
		}
	}
}
