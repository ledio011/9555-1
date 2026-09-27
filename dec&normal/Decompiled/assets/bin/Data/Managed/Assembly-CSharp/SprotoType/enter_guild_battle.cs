using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000383 RID: 899
	public class enter_guild_battle
	{
		// Token: 0x02000384 RID: 900
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B24 RID: 6948 RVA: 0x00093970 File Offset: 0x00091B70
			public request() : base(enter_guild_battle.request.max_field_count)
			{
			}

			// Token: 0x06001B25 RID: 6949 RVA: 0x00093980 File Offset: 0x00091B80
			public request(byte[] buffer) : base(enter_guild_battle.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06001B27 RID: 6951 RVA: 0x00093998 File Offset: 0x00091B98
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06001B28 RID: 6952 RVA: 0x000939D4 File Offset: 0x00091BD4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x040019FE RID: 6654
			private static int max_field_count;
		}
	}
}
