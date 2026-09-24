using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004A0 RID: 1184
	public class request_daily_mission
	{
		// Token: 0x020004A1 RID: 1185
		public class request : SprotoTypeBase
		{
			// Token: 0x060023CE RID: 9166 RVA: 0x000A5044 File Offset: 0x000A3244
			public request() : base(request_daily_mission.request.max_field_count)
			{
			}

			// Token: 0x060023CF RID: 9167 RVA: 0x000A5054 File Offset: 0x000A3254
			public request(byte[] buffer) : base(request_daily_mission.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060023D1 RID: 9169 RVA: 0x000A506C File Offset: 0x000A326C
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060023D2 RID: 9170 RVA: 0x000A50A8 File Offset: 0x000A32A8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C51 RID: 7249
			private static int max_field_count;
		}
	}
}
