using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200049E RID: 1182
	public class request_daily_buy
	{
		// Token: 0x0200049F RID: 1183
		public class request : SprotoTypeBase
		{
			// Token: 0x060023C8 RID: 9160 RVA: 0x000A4FBC File Offset: 0x000A31BC
			public request() : base(request_daily_buy.request.max_field_count)
			{
			}

			// Token: 0x060023C9 RID: 9161 RVA: 0x000A4FCC File Offset: 0x000A31CC
			public request(byte[] buffer) : base(request_daily_buy.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060023CB RID: 9163 RVA: 0x000A4FE4 File Offset: 0x000A31E4
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060023CC RID: 9164 RVA: 0x000A5020 File Offset: 0x000A3220
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C50 RID: 7248
			private static int max_field_count;
		}
	}
}
