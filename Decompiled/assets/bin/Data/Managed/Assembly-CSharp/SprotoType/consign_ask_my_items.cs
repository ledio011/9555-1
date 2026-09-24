using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000358 RID: 856
	public class consign_ask_my_items
	{
		// Token: 0x02000359 RID: 857
		public class request : SprotoTypeBase
		{
			// Token: 0x0600196F RID: 6511 RVA: 0x000900B8 File Offset: 0x0008E2B8
			public request() : base(consign_ask_my_items.request.max_field_count)
			{
			}

			// Token: 0x06001970 RID: 6512 RVA: 0x000900C8 File Offset: 0x0008E2C8
			public request(byte[] buffer) : base(consign_ask_my_items.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06001972 RID: 6514 RVA: 0x000900E0 File Offset: 0x0008E2E0
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06001973 RID: 6515 RVA: 0x0009011C File Offset: 0x0008E31C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001984 RID: 6532
			private static int max_field_count;
		}
	}
}
