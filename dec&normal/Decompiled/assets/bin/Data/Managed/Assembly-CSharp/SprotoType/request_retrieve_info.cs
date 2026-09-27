using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004C7 RID: 1223
	public class request_retrieve_info
	{
		// Token: 0x020004C8 RID: 1224
		public class request : SprotoTypeBase
		{
			// Token: 0x06002460 RID: 9312 RVA: 0x000A5F4C File Offset: 0x000A414C
			public request() : base(request_retrieve_info.request.max_field_count)
			{
			}

			// Token: 0x06002461 RID: 9313 RVA: 0x000A5F5C File Offset: 0x000A415C
			public request(byte[] buffer) : base(request_retrieve_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002463 RID: 9315 RVA: 0x000A5F74 File Offset: 0x000A4174
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002464 RID: 9316 RVA: 0x000A5FB0 File Offset: 0x000A41B0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C6E RID: 7278
			private static int max_field_count;
		}
	}
}
