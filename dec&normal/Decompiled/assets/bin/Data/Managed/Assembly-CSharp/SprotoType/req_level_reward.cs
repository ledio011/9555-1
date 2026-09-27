using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000486 RID: 1158
	public class req_level_reward
	{
		// Token: 0x02000487 RID: 1159
		public class request : SprotoTypeBase
		{
			// Token: 0x0600236E RID: 9070 RVA: 0x000A466C File Offset: 0x000A286C
			public request() : base(req_level_reward.request.max_field_count)
			{
			}

			// Token: 0x0600236F RID: 9071 RVA: 0x000A467C File Offset: 0x000A287C
			public request(byte[] buffer) : base(req_level_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002371 RID: 9073 RVA: 0x000A4694 File Offset: 0x000A2894
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002372 RID: 9074 RVA: 0x000A46D0 File Offset: 0x000A28D0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C3E RID: 7230
			private static int max_field_count;
		}
	}
}
