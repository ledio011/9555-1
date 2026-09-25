using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200062D RID: 1581
	public class unequip_fashion_item
	{
		// Token: 0x0200062E RID: 1582
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E05 RID: 11781 RVA: 0x000B91E0 File Offset: 0x000B73E0
			public request() : base(unequip_fashion_item.request.max_field_count)
			{
			}

			// Token: 0x06002E06 RID: 11782 RVA: 0x000B91F0 File Offset: 0x000B73F0
			public request(byte[] buffer) : base(unequip_fashion_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D8D RID: 3469
			// (get) Token: 0x06002E08 RID: 11784 RVA: 0x000B920C File Offset: 0x000B740C
			// (set) Token: 0x06002E09 RID: 11785 RVA: 0x000B9214 File Offset: 0x000B7414
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

			// Token: 0x17000D8E RID: 3470
			// (get) Token: 0x06002E0A RID: 11786 RVA: 0x000B922C File Offset: 0x000B742C
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002E0B RID: 11787 RVA: 0x000B923C File Offset: 0x000B743C
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
						this.indexId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002E0C RID: 11788 RVA: 0x000B9298 File Offset: 0x000B7498
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F10 RID: 7952
			private static int max_field_count = 1;

			// Token: 0x04001F11 RID: 7953
			private long _indexId;
		}
	}
}
