using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004AE RID: 1198
	public class request_guild_map_info
	{
		// Token: 0x020004AF RID: 1199
		public class request : SprotoTypeBase
		{
			// Token: 0x06002401 RID: 9217 RVA: 0x000A557C File Offset: 0x000A377C
			public request() : base(request_guild_map_info.request.max_field_count)
			{
			}

			// Token: 0x06002402 RID: 9218 RVA: 0x000A558C File Offset: 0x000A378C
			public request(byte[] buffer) : base(request_guild_map_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002404 RID: 9220 RVA: 0x000A55A4 File Offset: 0x000A37A4
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002405 RID: 9221 RVA: 0x000A55E0 File Offset: 0x000A37E0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C5B RID: 7259
			private static int max_field_count;
		}
	}
}
