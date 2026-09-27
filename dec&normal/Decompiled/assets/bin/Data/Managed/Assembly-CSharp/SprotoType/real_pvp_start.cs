using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200045E RID: 1118
	public class real_pvp_start
	{
		// Token: 0x0200045F RID: 1119
		public class request : SprotoTypeBase
		{
			// Token: 0x060022AB RID: 8875 RVA: 0x000A3050 File Offset: 0x000A1250
			public request() : base(real_pvp_start.request.max_field_count)
			{
			}

			// Token: 0x060022AC RID: 8876 RVA: 0x000A3060 File Offset: 0x000A1260
			public request(byte[] buffer) : base(real_pvp_start.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060022AE RID: 8878 RVA: 0x000A3078 File Offset: 0x000A1278
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060022AF RID: 8879 RVA: 0x000A30B4 File Offset: 0x000A12B4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C11 RID: 7185
			private static int max_field_count;
		}
	}
}
