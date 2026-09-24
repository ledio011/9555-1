using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200047A RID: 1146
	public class req_guild_notice
	{
		// Token: 0x0200047B RID: 1147
		public class request : SprotoTypeBase
		{
			// Token: 0x06002329 RID: 9001 RVA: 0x000A3E38 File Offset: 0x000A2038
			public request() : base(req_guild_notice.request.max_field_count)
			{
			}

			// Token: 0x0600232A RID: 9002 RVA: 0x000A3E48 File Offset: 0x000A2048
			public request(byte[] buffer) : base(req_guild_notice.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009ED RID: 2541
			// (get) Token: 0x0600232C RID: 9004 RVA: 0x000A3E64 File Offset: 0x000A2064
			// (set) Token: 0x0600232D RID: 9005 RVA: 0x000A3E6C File Offset: 0x000A206C
			public string notice
			{
				get
				{
					return this._notice;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._notice = value;
				}
			}

			// Token: 0x170009EE RID: 2542
			// (get) Token: 0x0600232E RID: 9006 RVA: 0x000A3E84 File Offset: 0x000A2084
			public bool HasNotice
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600232F RID: 9007 RVA: 0x000A3E94 File Offset: 0x000A2094
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
						this.notice = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002330 RID: 9008 RVA: 0x000A3EF0 File Offset: 0x000A20F0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.notice, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C2D RID: 7213
			private static int max_field_count = 1;

			// Token: 0x04001C2E RID: 7214
			private string _notice;
		}
	}
}
