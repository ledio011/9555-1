using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004C9 RID: 1225
	public class request_sign_30_day_info
	{
		// Token: 0x020004CA RID: 1226
		public class request : SprotoTypeBase
		{
			// Token: 0x06002466 RID: 9318 RVA: 0x000A5FD4 File Offset: 0x000A41D4
			public request() : base(request_sign_30_day_info.request.max_field_count)
			{
			}

			// Token: 0x06002467 RID: 9319 RVA: 0x000A5FE4 File Offset: 0x000A41E4
			public request(byte[] buffer) : base(request_sign_30_day_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002469 RID: 9321 RVA: 0x000A5FFC File Offset: 0x000A41FC
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x0600246A RID: 9322 RVA: 0x000A6038 File Offset: 0x000A4238
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C6F RID: 7279
			private static int max_field_count;
		}
	}
}
