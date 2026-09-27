using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004B8 RID: 1208
	public class request_line_state
	{
		// Token: 0x020004B9 RID: 1209
		public class request : SprotoTypeBase
		{
			// Token: 0x06002425 RID: 9253 RVA: 0x000A5924 File Offset: 0x000A3B24
			public request() : base(request_line_state.request.max_field_count)
			{
			}

			// Token: 0x06002426 RID: 9254 RVA: 0x000A5934 File Offset: 0x000A3B34
			public request(byte[] buffer) : base(request_line_state.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002428 RID: 9256 RVA: 0x000A594C File Offset: 0x000A3B4C
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002429 RID: 9257 RVA: 0x000A5988 File Offset: 0x000A3B88
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C62 RID: 7266
			private static int max_field_count;
		}
	}
}
