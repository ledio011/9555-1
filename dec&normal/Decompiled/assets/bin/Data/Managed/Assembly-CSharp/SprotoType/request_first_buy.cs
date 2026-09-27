using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004A8 RID: 1192
	public class request_first_buy
	{
		// Token: 0x020004A9 RID: 1193
		public class request : SprotoTypeBase
		{
			// Token: 0x060023EC RID: 9196 RVA: 0x000A5364 File Offset: 0x000A3564
			public request() : base(request_first_buy.request.max_field_count)
			{
			}

			// Token: 0x060023ED RID: 9197 RVA: 0x000A5374 File Offset: 0x000A3574
			public request(byte[] buffer) : base(request_first_buy.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060023EF RID: 9199 RVA: 0x000A538C File Offset: 0x000A358C
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060023F0 RID: 9200 RVA: 0x000A53C8 File Offset: 0x000A35C8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C57 RID: 7255
			private static int max_field_count;
		}
	}
}
