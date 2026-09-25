using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000496 RID: 1174
	public class request_battle_info
	{
		// Token: 0x02000497 RID: 1175
		public class request : SprotoTypeBase
		{
			// Token: 0x060023AD RID: 9133 RVA: 0x000A4D1C File Offset: 0x000A2F1C
			public request() : base(request_battle_info.request.max_field_count)
			{
			}

			// Token: 0x060023AE RID: 9134 RVA: 0x000A4D2C File Offset: 0x000A2F2C
			public request(byte[] buffer) : base(request_battle_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060023B0 RID: 9136 RVA: 0x000A4D44 File Offset: 0x000A2F44
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060023B1 RID: 9137 RVA: 0x000A4D80 File Offset: 0x000A2F80
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C4B RID: 7243
			private static int max_field_count;
		}
	}
}
