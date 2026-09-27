using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000326 RID: 806
	public class buy_shop_item
	{
		// Token: 0x02000327 RID: 807
		public class request : SprotoTypeBase
		{
			// Token: 0x06001724 RID: 5924 RVA: 0x0008B30C File Offset: 0x0008950C
			public request() : base(buy_shop_item.request.max_field_count)
			{
			}

			// Token: 0x06001725 RID: 5925 RVA: 0x0008B31C File Offset: 0x0008951C
			public request(byte[] buffer) : base(buy_shop_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170004ED RID: 1261
			// (get) Token: 0x06001727 RID: 5927 RVA: 0x0008B338 File Offset: 0x00089538
			// (set) Token: 0x06001728 RID: 5928 RVA: 0x0008B340 File Offset: 0x00089540
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

			// Token: 0x170004EE RID: 1262
			// (get) Token: 0x06001729 RID: 5929 RVA: 0x0008B358 File Offset: 0x00089558
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170004EF RID: 1263
			// (get) Token: 0x0600172A RID: 5930 RVA: 0x0008B368 File Offset: 0x00089568
			// (set) Token: 0x0600172B RID: 5931 RVA: 0x0008B370 File Offset: 0x00089570
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

			// Token: 0x170004F0 RID: 1264
			// (get) Token: 0x0600172C RID: 5932 RVA: 0x0008B388 File Offset: 0x00089588
			public bool HasItemCount
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170004F1 RID: 1265
			// (get) Token: 0x0600172D RID: 5933 RVA: 0x0008B398 File Offset: 0x00089598
			// (set) Token: 0x0600172E RID: 5934 RVA: 0x0008B3A0 File Offset: 0x000895A0
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

			// Token: 0x170004F2 RID: 1266
			// (get) Token: 0x0600172F RID: 5935 RVA: 0x0008B3B8 File Offset: 0x000895B8
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06001730 RID: 5936 RVA: 0x0008B3C8 File Offset: 0x000895C8
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

			// Token: 0x06001731 RID: 5937 RVA: 0x0008B45C File Offset: 0x0008965C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
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

			// Token: 0x040018D4 RID: 6356
			private static int max_field_count = 3;

			// Token: 0x040018D5 RID: 6357
			private string _ID;

			// Token: 0x040018D6 RID: 6358
			private long _itemCount;

			// Token: 0x040018D7 RID: 6359
			private long _type;
		}
	}
}
