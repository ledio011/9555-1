using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000537 RID: 1335
	public class ret_guild_log
	{
		// Token: 0x02000538 RID: 1336
		public class request : SprotoTypeBase
		{
			// Token: 0x060026E9 RID: 9961 RVA: 0x000AAD14 File Offset: 0x000A8F14
			public request() : base(ret_guild_log.request.max_field_count)
			{
			}

			// Token: 0x060026EA RID: 9962 RVA: 0x000AAD24 File Offset: 0x000A8F24
			public request(byte[] buffer) : base(ret_guild_log.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AED RID: 2797
			// (get) Token: 0x060026EC RID: 9964 RVA: 0x000AAD40 File Offset: 0x000A8F40
			// (set) Token: 0x060026ED RID: 9965 RVA: 0x000AAD48 File Offset: 0x000A8F48
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

			// Token: 0x17000AEE RID: 2798
			// (get) Token: 0x060026EE RID: 9966 RVA: 0x000AAD60 File Offset: 0x000A8F60
			public bool HasLogs
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060026EF RID: 9967 RVA: 0x000AAD70 File Offset: 0x000A8F70
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

			// Token: 0x060026F0 RID: 9968 RVA: 0x000AADCC File Offset: 0x000A8FCC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.logs, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D13 RID: 7443
			private static int max_field_count = 1;

			// Token: 0x04001D14 RID: 7444
			private List<string> _logs;
		}
	}
}
