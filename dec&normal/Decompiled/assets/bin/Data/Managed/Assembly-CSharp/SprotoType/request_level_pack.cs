using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004B6 RID: 1206
	public class request_level_pack
	{
		// Token: 0x020004B7 RID: 1207
		public class request : SprotoTypeBase
		{
			// Token: 0x0600241F RID: 9247 RVA: 0x000A589C File Offset: 0x000A3A9C
			public request() : base(request_level_pack.request.max_field_count)
			{
			}

			// Token: 0x06002420 RID: 9248 RVA: 0x000A58AC File Offset: 0x000A3AAC
			public request(byte[] buffer) : base(request_level_pack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002422 RID: 9250 RVA: 0x000A58C4 File Offset: 0x000A3AC4
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002423 RID: 9251 RVA: 0x000A5900 File Offset: 0x000A3B00
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C61 RID: 7265
			private static int max_field_count;
		}
	}
}
