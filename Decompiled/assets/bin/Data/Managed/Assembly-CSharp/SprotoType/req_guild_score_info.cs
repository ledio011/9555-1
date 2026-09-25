using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200047C RID: 1148
	public class req_guild_score_info
	{
		// Token: 0x0200047D RID: 1149
		public class request : SprotoTypeBase
		{
			// Token: 0x06002332 RID: 9010 RVA: 0x000A3F40 File Offset: 0x000A2140
			public request() : base(req_guild_score_info.request.max_field_count)
			{
			}

			// Token: 0x06002333 RID: 9011 RVA: 0x000A3F50 File Offset: 0x000A2150
			public request(byte[] buffer) : base(req_guild_score_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002335 RID: 9013 RVA: 0x000A3F68 File Offset: 0x000A2168
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002336 RID: 9014 RVA: 0x000A3FA4 File Offset: 0x000A21A4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C2F RID: 7215
			private static int max_field_count;
		}
	}
}
