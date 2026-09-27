using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000450 RID: 1104
	public class rank_pvp_history
	{
		// Token: 0x02000451 RID: 1105
		public class request : SprotoTypeBase
		{
			// Token: 0x06002272 RID: 8818 RVA: 0x000A2A28 File Offset: 0x000A0C28
			public request() : base(rank_pvp_history.request.max_field_count)
			{
			}

			// Token: 0x06002273 RID: 8819 RVA: 0x000A2A38 File Offset: 0x000A0C38
			public request(byte[] buffer) : base(rank_pvp_history.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009C7 RID: 2503
			// (get) Token: 0x06002275 RID: 8821 RVA: 0x000A2A54 File Offset: 0x000A0C54
			// (set) Token: 0x06002276 RID: 8822 RVA: 0x000A2A5C File Offset: 0x000A0C5C
			public List<string> logs
			{
				get
				{
					return this._logs;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._logs = value;
				}
			}

			// Token: 0x170009C8 RID: 2504
			// (get) Token: 0x06002277 RID: 8823 RVA: 0x000A2A74 File Offset: 0x000A0C74
			public bool HasLogs
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002278 RID: 8824 RVA: 0x000A2A84 File Offset: 0x000A0C84
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.logs = this.deserialize.read_string_list();
					}
				}
			}

			// Token: 0x06002279 RID: 8825 RVA: 0x000A2AE0 File Offset: 0x000A0CE0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.logs, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C05 RID: 7173
			private static int max_field_count = 1;

			// Token: 0x04001C06 RID: 7174
			private List<string> _logs;
		}
	}
}
