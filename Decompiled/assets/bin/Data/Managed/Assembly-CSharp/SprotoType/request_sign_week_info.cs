using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004CB RID: 1227
	public class request_sign_week_info
	{
		// Token: 0x020004CC RID: 1228
		public class request : SprotoTypeBase
		{
			// Token: 0x0600246C RID: 9324 RVA: 0x000A605C File Offset: 0x000A425C
			public request() : base(request_sign_week_info.request.max_field_count)
			{
			}

			// Token: 0x0600246D RID: 9325 RVA: 0x000A606C File Offset: 0x000A426C
			public request(byte[] buffer) : base(request_sign_week_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x0600246F RID: 9327 RVA: 0x000A6084 File Offset: 0x000A4284
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002470 RID: 9328 RVA: 0x000A60C0 File Offset: 0x000A42C0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C70 RID: 7280
			private static int max_field_count;
		}
	}
}
