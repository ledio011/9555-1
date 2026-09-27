using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000488 RID: 1160
	public class req_offline_chat
	{
		// Token: 0x02000489 RID: 1161
		public class request : SprotoTypeBase
		{
			// Token: 0x06002374 RID: 9076 RVA: 0x000A46F4 File Offset: 0x000A28F4
			public request() : base(req_offline_chat.request.max_field_count)
			{
			}

			// Token: 0x06002375 RID: 9077 RVA: 0x000A4704 File Offset: 0x000A2904
			public request(byte[] buffer) : base(req_offline_chat.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002377 RID: 9079 RVA: 0x000A471C File Offset: 0x000A291C
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002378 RID: 9080 RVA: 0x000A4758 File Offset: 0x000A2958
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C3F RID: 7231
			private static int max_field_count;
		}
	}
}
