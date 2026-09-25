using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004EF RID: 1263
	public class require_vip_reward
	{
		// Token: 0x020004F0 RID: 1264
		public class request : SprotoTypeBase
		{
			// Token: 0x060024FC RID: 9468 RVA: 0x000A6FBC File Offset: 0x000A51BC
			public request() : base(require_vip_reward.request.max_field_count)
			{
			}

			// Token: 0x060024FD RID: 9469 RVA: 0x000A6FCC File Offset: 0x000A51CC
			public request(byte[] buffer) : base(require_vip_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060024FF RID: 9471 RVA: 0x000A6FE4 File Offset: 0x000A51E4
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002500 RID: 9472 RVA: 0x000A7020 File Offset: 0x000A5220
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C8E RID: 7310
			private static int max_field_count;
		}
	}
}
