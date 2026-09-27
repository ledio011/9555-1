using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004B4 RID: 1204
	public class request_invest_pack
	{
		// Token: 0x020004B5 RID: 1205
		public class request : SprotoTypeBase
		{
			// Token: 0x06002419 RID: 9241 RVA: 0x000A5814 File Offset: 0x000A3A14
			public request() : base(request_invest_pack.request.max_field_count)
			{
			}

			// Token: 0x0600241A RID: 9242 RVA: 0x000A5824 File Offset: 0x000A3A24
			public request(byte[] buffer) : base(request_invest_pack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x0600241C RID: 9244 RVA: 0x000A583C File Offset: 0x000A3A3C
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x0600241D RID: 9245 RVA: 0x000A5878 File Offset: 0x000A3A78
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C60 RID: 7264
			private static int max_field_count;
		}
	}
}
