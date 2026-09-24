using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000460 RID: 1120
	public class real_pvp_state
	{
		// Token: 0x02000461 RID: 1121
		public class request : SprotoTypeBase
		{
			// Token: 0x060022B1 RID: 8881 RVA: 0x000A30D8 File Offset: 0x000A12D8
			public request() : base(real_pvp_state.request.max_field_count)
			{
			}

			// Token: 0x060022B2 RID: 8882 RVA: 0x000A30E8 File Offset: 0x000A12E8
			public request(byte[] buffer) : base(real_pvp_state.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060022B4 RID: 8884 RVA: 0x000A3100 File Offset: 0x000A1300
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060022B5 RID: 8885 RVA: 0x000A313C File Offset: 0x000A133C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C12 RID: 7186
			private static int max_field_count;
		}
	}
}
