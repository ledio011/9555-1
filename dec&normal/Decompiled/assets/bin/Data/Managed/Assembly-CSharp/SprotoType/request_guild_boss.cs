using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004AA RID: 1194
	public class request_guild_boss
	{
		// Token: 0x020004AB RID: 1195
		public class request : SprotoTypeBase
		{
			// Token: 0x060023F2 RID: 9202 RVA: 0x000A53EC File Offset: 0x000A35EC
			public request() : base(request_guild_boss.request.max_field_count)
			{
			}

			// Token: 0x060023F3 RID: 9203 RVA: 0x000A53FC File Offset: 0x000A35FC
			public request(byte[] buffer) : base(request_guild_boss.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060023F5 RID: 9205 RVA: 0x000A5414 File Offset: 0x000A3614
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060023F6 RID: 9206 RVA: 0x000A5450 File Offset: 0x000A3650
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C58 RID: 7256
			private static int max_field_count;
		}
	}
}
