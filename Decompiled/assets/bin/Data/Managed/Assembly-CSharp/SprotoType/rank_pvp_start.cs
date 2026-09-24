using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000458 RID: 1112
	public class rank_pvp_start
	{
		// Token: 0x02000459 RID: 1113
		public class request : SprotoTypeBase
		{
			// Token: 0x06002293 RID: 8851 RVA: 0x000A2DB8 File Offset: 0x000A0FB8
			public request() : base(rank_pvp_start.request.max_field_count)
			{
			}

			// Token: 0x06002294 RID: 8852 RVA: 0x000A2DC8 File Offset: 0x000A0FC8
			public request(byte[] buffer) : base(rank_pvp_start.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002296 RID: 8854 RVA: 0x000A2DE0 File Offset: 0x000A0FE0
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002297 RID: 8855 RVA: 0x000A2E1C File Offset: 0x000A101C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C0C RID: 7180
			private static int max_field_count;
		}
	}
}
