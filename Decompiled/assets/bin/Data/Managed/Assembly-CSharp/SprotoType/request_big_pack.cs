using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000498 RID: 1176
	public class request_big_pack
	{
		// Token: 0x02000499 RID: 1177
		public class request : SprotoTypeBase
		{
			// Token: 0x060023B3 RID: 9139 RVA: 0x000A4DA4 File Offset: 0x000A2FA4
			public request() : base(request_big_pack.request.max_field_count)
			{
			}

			// Token: 0x060023B4 RID: 9140 RVA: 0x000A4DB4 File Offset: 0x000A2FB4
			public request(byte[] buffer) : base(request_big_pack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060023B6 RID: 9142 RVA: 0x000A4DCC File Offset: 0x000A2FCC
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060023B7 RID: 9143 RVA: 0x000A4E08 File Offset: 0x000A3008
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C4C RID: 7244
			private static int max_field_count;
		}
	}
}
