using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004D3 RID: 1235
	public class request_special_big_pack
	{
		// Token: 0x020004D4 RID: 1236
		public class request : SprotoTypeBase
		{
			// Token: 0x06002487 RID: 9351 RVA: 0x000A62FC File Offset: 0x000A44FC
			public request() : base(request_special_big_pack.request.max_field_count)
			{
			}

			// Token: 0x06002488 RID: 9352 RVA: 0x000A630C File Offset: 0x000A450C
			public request(byte[] buffer) : base(request_special_big_pack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x0600248A RID: 9354 RVA: 0x000A6324 File Offset: 0x000A4524
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x0600248B RID: 9355 RVA: 0x000A6360 File Offset: 0x000A4560
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C75 RID: 7285
			private static int max_field_count;
		}
	}
}
