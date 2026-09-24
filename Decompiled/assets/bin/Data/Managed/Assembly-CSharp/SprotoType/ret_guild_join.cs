using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000531 RID: 1329
	public class ret_guild_join
	{
		// Token: 0x02000532 RID: 1330
		public class request : SprotoTypeBase
		{
			// Token: 0x060026C8 RID: 9928 RVA: 0x000AA91C File Offset: 0x000A8B1C
			public request() : base(ret_guild_join.request.max_field_count)
			{
			}

			// Token: 0x060026C9 RID: 9929 RVA: 0x000AA92C File Offset: 0x000A8B2C
			public request(byte[] buffer) : base(ret_guild_join.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AE3 RID: 2787
			// (get) Token: 0x060026CB RID: 9931 RVA: 0x000AA948 File Offset: 0x000A8B48
			// (set) Token: 0x060026CC RID: 9932 RVA: 0x000AA950 File Offset: 0x000A8B50
			public long guildId
			{
				get
				{
					return this._guildId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guildId = value;
				}
			}

			// Token: 0x17000AE4 RID: 2788
			// (get) Token: 0x060026CD RID: 9933 RVA: 0x000AA968 File Offset: 0x000A8B68
			public bool HasGuildId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060026CE RID: 9934 RVA: 0x000AA978 File Offset: 0x000A8B78
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
						this.guildId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060026CF RID: 9935 RVA: 0x000AA9D4 File Offset: 0x000A8BD4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.guildId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D0B RID: 7435
			private static int max_field_count = 1;

			// Token: 0x04001D0C RID: 7436
			private long _guildId;
		}
	}
}
