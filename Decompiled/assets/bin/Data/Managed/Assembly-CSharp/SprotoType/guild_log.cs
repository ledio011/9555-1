using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003E7 RID: 999
	public class guild_log
	{
		// Token: 0x020003E8 RID: 1000
		public class request : SprotoTypeBase
		{
			// Token: 0x06001ED5 RID: 7893 RVA: 0x0009B390 File Offset: 0x00099590
			public request() : base(guild_log.request.max_field_count)
			{
			}

			// Token: 0x06001ED6 RID: 7894 RVA: 0x0009B3A0 File Offset: 0x000995A0
			public request(byte[] buffer) : base(guild_log.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06001ED8 RID: 7896 RVA: 0x0009B3B8 File Offset: 0x000995B8
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06001ED9 RID: 7897 RVA: 0x0009B3F4 File Offset: 0x000995F4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001B07 RID: 6919
			private static int max_field_count;
		}
	}
}
