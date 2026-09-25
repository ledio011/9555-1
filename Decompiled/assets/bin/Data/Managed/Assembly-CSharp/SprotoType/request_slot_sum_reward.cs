using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004D1 RID: 1233
	public class request_slot_sum_reward
	{
		// Token: 0x020004D2 RID: 1234
		public class request : SprotoTypeBase
		{
			// Token: 0x06002481 RID: 9345 RVA: 0x000A6274 File Offset: 0x000A4474
			public request() : base(request_slot_sum_reward.request.max_field_count)
			{
			}

			// Token: 0x06002482 RID: 9346 RVA: 0x000A6284 File Offset: 0x000A4484
			public request(byte[] buffer) : base(request_slot_sum_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002484 RID: 9348 RVA: 0x000A629C File Offset: 0x000A449C
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002485 RID: 9349 RVA: 0x000A62D8 File Offset: 0x000A44D8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C74 RID: 7284
			private static int max_field_count;
		}
	}
}
