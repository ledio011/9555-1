using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004E1 RID: 1249
	public class request_wild_boss_info
	{
		// Token: 0x020004E2 RID: 1250
		public class request : SprotoTypeBase
		{
			// Token: 0x060024BD RID: 9405 RVA: 0x000A68A4 File Offset: 0x000A4AA4
			public request() : base(request_wild_boss_info.request.max_field_count)
			{
			}

			// Token: 0x060024BE RID: 9406 RVA: 0x000A68B4 File Offset: 0x000A4AB4
			public request(byte[] buffer) : base(request_wild_boss_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060024C0 RID: 9408 RVA: 0x000A68CC File Offset: 0x000A4ACC
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060024C1 RID: 9409 RVA: 0x000A6908 File Offset: 0x000A4B08
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C80 RID: 7296
			private static int max_field_count;
		}
	}
}
