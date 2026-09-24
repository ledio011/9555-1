using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005EB RID: 1515
	public class start_battle
	{
		// Token: 0x020005EC RID: 1516
		public class request : SprotoTypeBase
		{
			// Token: 0x06002BFC RID: 11260 RVA: 0x000B5018 File Offset: 0x000B3218
			public request() : base(start_battle.request.max_field_count)
			{
			}

			// Token: 0x06002BFD RID: 11261 RVA: 0x000B5028 File Offset: 0x000B3228
			public request(byte[] buffer) : base(start_battle.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002BFF RID: 11263 RVA: 0x000B5040 File Offset: 0x000B3240
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002C00 RID: 11264 RVA: 0x000B507C File Offset: 0x000B327C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001E7D RID: 7805
			private static int max_field_count;
		}
	}
}
