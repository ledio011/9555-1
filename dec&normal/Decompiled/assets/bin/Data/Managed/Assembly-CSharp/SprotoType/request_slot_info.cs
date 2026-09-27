using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004CD RID: 1229
	public class request_slot_info
	{
		// Token: 0x020004CE RID: 1230
		public class request : SprotoTypeBase
		{
			// Token: 0x06002472 RID: 9330 RVA: 0x000A60E4 File Offset: 0x000A42E4
			public request() : base(request_slot_info.request.max_field_count)
			{
			}

			// Token: 0x06002473 RID: 9331 RVA: 0x000A60F4 File Offset: 0x000A42F4
			public request(byte[] buffer) : base(request_slot_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002475 RID: 9333 RVA: 0x000A610C File Offset: 0x000A430C
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002476 RID: 9334 RVA: 0x000A6148 File Offset: 0x000A4348
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C71 RID: 7281
			private static int max_field_count;
		}
	}
}
