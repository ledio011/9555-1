using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003BB RID: 955
	public class gather_team
	{
		// Token: 0x020003BC RID: 956
		public class request : SprotoTypeBase
		{
			// Token: 0x06001D1F RID: 7455 RVA: 0x00097AB4 File Offset: 0x00095CB4
			public request() : base(gather_team.request.max_field_count)
			{
			}

			// Token: 0x06001D20 RID: 7456 RVA: 0x00097AC4 File Offset: 0x00095CC4
			public request(byte[] buffer) : base(gather_team.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06001D22 RID: 7458 RVA: 0x00097ADC File Offset: 0x00095CDC
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06001D23 RID: 7459 RVA: 0x00097B18 File Offset: 0x00095D18
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001A8B RID: 6795
			private static int max_field_count;
		}
	}
}
