using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000607 RID: 1543
	public class sync_fashion_backpack_item
	{
		// Token: 0x02000608 RID: 1544
		public class request : SprotoTypeBase
		{
			// Token: 0x06002CBF RID: 11455 RVA: 0x000B6840 File Offset: 0x000B4A40
			public request() : base(sync_fashion_backpack_item.request.max_field_count)
			{
			}

			// Token: 0x06002CC0 RID: 11456 RVA: 0x000B6850 File Offset: 0x000B4A50
			public request(byte[] buffer) : base(sync_fashion_backpack_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D0B RID: 3339
			// (get) Token: 0x06002CC2 RID: 11458 RVA: 0x000B686C File Offset: 0x000B4A6C
			// (set) Token: 0x06002CC3 RID: 11459 RVA: 0x000B6874 File Offset: 0x000B4A74
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

			// Token: 0x17000D0C RID: 3340
			// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x000B688C File Offset: 0x000B4A8C
			public bool HasGameitems
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002CC5 RID: 11461 RVA: 0x000B689C File Offset: 0x000B4A9C
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

			// Token: 0x06002CC6 RID: 11462 RVA: 0x000B6914 File Offset: 0x000B4B14
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<long, gameitem>(this.gameitems, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001EB4 RID: 7860
			private static int max_field_count = 1;

			// Token: 0x04001EB5 RID: 7861
			private Dictionary<long, gameitem> _gameitems;
		}
	}
}
