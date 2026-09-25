using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000557 RID: 1367
	public class ret_open_guild_shop
	{
		// Token: 0x02000558 RID: 1368
		public class request : SprotoTypeBase
		{
			// Token: 0x060027C3 RID: 10179 RVA: 0x000AC830 File Offset: 0x000AAA30
			public request() : base(ret_open_guild_shop.request.max_field_count)
			{
			}

			// Token: 0x060027C4 RID: 10180 RVA: 0x000AC840 File Offset: 0x000AAA40
			public request(byte[] buffer) : base(ret_open_guild_shop.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B39 RID: 2873
			// (get) Token: 0x060027C6 RID: 10182 RVA: 0x000AC85C File Offset: 0x000AAA5C
			// (set) Token: 0x060027C7 RID: 10183 RVA: 0x000AC864 File Offset: 0x000AAA64
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

			// Token: 0x17000B3A RID: 2874
			// (get) Token: 0x060027C8 RID: 10184 RVA: 0x000AC87C File Offset: 0x000AAA7C
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B3B RID: 2875
			// (get) Token: 0x060027C9 RID: 10185 RVA: 0x000AC88C File Offset: 0x000AAA8C
			// (set) Token: 0x060027CA RID: 10186 RVA: 0x000AC894 File Offset: 0x000AAA94
			public Dictionary<string, shop_item> shop_list
			{
				get
				{
					return this._shop_list;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._shop_list = value;
				}
			}

			// Token: 0x17000B3C RID: 2876
			// (get) Token: 0x060027CB RID: 10187 RVA: 0x000AC8AC File Offset: 0x000AAAAC
			public bool HasShop_list
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060027CC RID: 10188 RVA: 0x000AC8BC File Offset: 0x000AAABC
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
							this.shop_list = this.deserialize.read_map<string, shop_item>((shop_item v) => v.ID);
						}
					}
					else
					{
						this.type = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060027CD RID: 10189 RVA: 0x000AC950 File Offset: 0x000AAB50
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<string, shop_item>(this.shop_list, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D51 RID: 7505
			private static int max_field_count = 2;

			// Token: 0x04001D52 RID: 7506
			private long _type;

			// Token: 0x04001D53 RID: 7507
			private Dictionary<string, shop_item> _shop_list;
		}
	}
}
