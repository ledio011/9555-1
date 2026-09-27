using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000616 RID: 1558
	public class take_item_storagepack
	{
		// Token: 0x02000617 RID: 1559
		public class request : SprotoTypeBase
		{
			// Token: 0x06002D1F RID: 11551 RVA: 0x000B7418 File Offset: 0x000B5618
			public request() : base(take_item_storagepack.request.max_field_count)
			{
			}

			// Token: 0x06002D20 RID: 11552 RVA: 0x000B7428 File Offset: 0x000B5628
			public request(byte[] buffer) : base(take_item_storagepack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D29 RID: 3369
			// (get) Token: 0x06002D22 RID: 11554 RVA: 0x000B7444 File Offset: 0x000B5644
			// (set) Token: 0x06002D23 RID: 11555 RVA: 0x000B744C File Offset: 0x000B564C
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

			// Token: 0x17000D2A RID: 3370
			// (get) Token: 0x06002D24 RID: 11556 RVA: 0x000B7464 File Offset: 0x000B5664
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002D25 RID: 11557 RVA: 0x000B7474 File Offset: 0x000B5674
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

			// Token: 0x06002D26 RID: 11558 RVA: 0x000B74D0 File Offset: 0x000B56D0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001ECF RID: 7887
			private static int max_field_count = 1;

			// Token: 0x04001ED0 RID: 7888
			private long _indexId;
		}
	}
}
