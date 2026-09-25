using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004ED RID: 1261
	public class require_vip_info
	{
		// Token: 0x020004EE RID: 1262
		public class request : SprotoTypeBase
		{
			// Token: 0x060024F6 RID: 9462 RVA: 0x000A6F34 File Offset: 0x000A5134
			public request() : base(require_vip_info.request.max_field_count)
			{
			}

			// Token: 0x060024F7 RID: 9463 RVA: 0x000A6F44 File Offset: 0x000A5144
			public request(byte[] buffer) : base(require_vip_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060024F9 RID: 9465 RVA: 0x000A6F5C File Offset: 0x000A515C
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060024FA RID: 9466 RVA: 0x000A6F98 File Offset: 0x000A5198
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C8D RID: 7309
			private static int max_field_count;
		}
	}
}
