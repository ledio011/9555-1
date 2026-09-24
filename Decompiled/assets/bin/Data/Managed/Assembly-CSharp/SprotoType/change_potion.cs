using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000332 RID: 818
	public class change_potion
	{
		// Token: 0x02000333 RID: 819
		public class request : SprotoTypeBase
		{
			// Token: 0x0600177B RID: 6011 RVA: 0x0008BE00 File Offset: 0x0008A000
			public request() : base(change_potion.request.max_field_count)
			{
			}

			// Token: 0x0600177C RID: 6012 RVA: 0x0008BE10 File Offset: 0x0008A010
			public request(byte[] buffer) : base(change_potion.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x0600177E RID: 6014 RVA: 0x0008BE2C File Offset: 0x0008A02C
			// (set) Token: 0x0600177F RID: 6015 RVA: 0x0008BE34 File Offset: 0x0008A034
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

			// Token: 0x17000510 RID: 1296
			// (get) Token: 0x06001780 RID: 6016 RVA: 0x0008BE4C File Offset: 0x0008A04C
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001781 RID: 6017 RVA: 0x0008BE5C File Offset: 0x0008A05C
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

			// Token: 0x06001782 RID: 6018 RVA: 0x0008BEB8 File Offset: 0x0008A0B8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040018EB RID: 6379
			private static int max_field_count = 1;

			// Token: 0x040018EC RID: 6380
			private long _indexId;
		}
	}
}
