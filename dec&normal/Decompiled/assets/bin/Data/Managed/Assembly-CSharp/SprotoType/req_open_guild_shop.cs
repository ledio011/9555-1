using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200048A RID: 1162
	public class req_open_guild_shop
	{
		// Token: 0x0200048B RID: 1163
		public class request : SprotoTypeBase
		{
			// Token: 0x0600237A RID: 9082 RVA: 0x000A477C File Offset: 0x000A297C
			public request() : base(req_open_guild_shop.request.max_field_count)
			{
			}

			// Token: 0x0600237B RID: 9083 RVA: 0x000A478C File Offset: 0x000A298C
			public request(byte[] buffer) : base(req_open_guild_shop.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x0600237D RID: 9085 RVA: 0x000A47A4 File Offset: 0x000A29A4
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x0600237E RID: 9086 RVA: 0x000A47E0 File Offset: 0x000A29E0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C40 RID: 7232
			private static int max_field_count;
		}
	}
}
