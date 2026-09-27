using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005FF RID: 1535
	public class sync_badgepack_item
	{
		// Token: 0x02000600 RID: 1536
		public class request : SprotoTypeBase
		{
			// Token: 0x06002C6A RID: 11370 RVA: 0x000B5D0C File Offset: 0x000B3F0C
			public request() : base(sync_badgepack_item.request.max_field_count)
			{
			}

			// Token: 0x06002C6B RID: 11371 RVA: 0x000B5D1C File Offset: 0x000B3F1C
			public request(byte[] buffer) : base(sync_badgepack_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000CE5 RID: 3301
			// (get) Token: 0x06002C6D RID: 11373 RVA: 0x000B5D38 File Offset: 0x000B3F38
			// (set) Token: 0x06002C6E RID: 11374 RVA: 0x000B5D40 File Offset: 0x000B3F40
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

			// Token: 0x17000CE6 RID: 3302
			// (get) Token: 0x06002C6F RID: 11375 RVA: 0x000B5D58 File Offset: 0x000B3F58
			public bool HasGameitems
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002C70 RID: 11376 RVA: 0x000B5D68 File Offset: 0x000B3F68
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

			// Token: 0x06002C71 RID: 11377 RVA: 0x000B5DE0 File Offset: 0x000B3FE0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<long, gameitem>(this.gameitems, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E99 RID: 7833
			private static int max_field_count = 1;

			// Token: 0x04001E9A RID: 7834
			private Dictionary<long, gameitem> _gameitems;
		}
	}
}
