using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200060B RID: 1547
	public class sync_item_pack
	{
		// Token: 0x0200060C RID: 1548
		public class request : SprotoTypeBase
		{
			// Token: 0x06002CD2 RID: 11474 RVA: 0x000B6A74 File Offset: 0x000B4C74
			public request() : base(sync_item_pack.request.max_field_count)
			{
			}

			// Token: 0x06002CD3 RID: 11475 RVA: 0x000B6A84 File Offset: 0x000B4C84
			public request(byte[] buffer) : base(sync_item_pack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D0F RID: 3343
			// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x000B6AA0 File Offset: 0x000B4CA0
			// (set) Token: 0x06002CD6 RID: 11478 RVA: 0x000B6AA8 File Offset: 0x000B4CA8
			public Dictionary<long, gameitem> gameitems
			{
				get
				{
					return this._gameitems;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._gameitems = value;
				}
			}

			// Token: 0x17000D10 RID: 3344
			// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x000B6AC0 File Offset: 0x000B4CC0
			public bool HasGameitems
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002CD8 RID: 11480 RVA: 0x000B6AD0 File Offset: 0x000B4CD0
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.gameitems = this.deserialize.read_map<long, gameitem>((gameitem v) => v.indexId);
					}
				}
			}

			// Token: 0x06002CD9 RID: 11481 RVA: 0x000B6B48 File Offset: 0x000B4D48
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<long, gameitem>(this.gameitems, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001EB9 RID: 7865
			private static int max_field_count = 1;

			// Token: 0x04001EBA RID: 7866
			private Dictionary<long, gameitem> _gameitems;
		}
	}
}
