using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000494 RID: 1172
	public class request_bar_fight
	{
		// Token: 0x02000495 RID: 1173
		public class request : SprotoTypeBase
		{
			// Token: 0x060023A7 RID: 9127 RVA: 0x000A4C94 File Offset: 0x000A2E94
			public request() : base(request_bar_fight.request.max_field_count)
			{
			}

			// Token: 0x060023A8 RID: 9128 RVA: 0x000A4CA4 File Offset: 0x000A2EA4
			public request(byte[] buffer) : base(request_bar_fight.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060023AA RID: 9130 RVA: 0x000A4CBC File Offset: 0x000A2EBC
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060023AB RID: 9131 RVA: 0x000A4CF8 File Offset: 0x000A2EF8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C4A RID: 7242
			private static int max_field_count;
		}
	}
}
