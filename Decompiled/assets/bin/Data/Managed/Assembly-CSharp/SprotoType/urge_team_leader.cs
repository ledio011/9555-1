using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000655 RID: 1621
	public class urge_team_leader
	{
		// Token: 0x02000656 RID: 1622
		public class request : SprotoTypeBase
		{
			// Token: 0x06002EE1 RID: 12001 RVA: 0x000BAC28 File Offset: 0x000B8E28
			public request() : base(urge_team_leader.request.max_field_count)
			{
			}

			// Token: 0x06002EE2 RID: 12002 RVA: 0x000BAC38 File Offset: 0x000B8E38
			public request(byte[] buffer) : base(urge_team_leader.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002EE4 RID: 12004 RVA: 0x000BAC50 File Offset: 0x000B8E50
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002EE5 RID: 12005 RVA: 0x000BAC8C File Offset: 0x000B8E8C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001F45 RID: 8005
			private static int max_field_count;
		}
	}
}
