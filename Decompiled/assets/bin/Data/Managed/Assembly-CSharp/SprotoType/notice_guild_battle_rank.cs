using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200042B RID: 1067
	public class notice_guild_battle_rank
	{
		// Token: 0x0200042C RID: 1068
		public class request : SprotoTypeBase
		{
			// Token: 0x0600210F RID: 8463 RVA: 0x0009FC1C File Offset: 0x0009DE1C
			public request() : base(notice_guild_battle_rank.request.max_field_count)
			{
			}

			// Token: 0x06002110 RID: 8464 RVA: 0x0009FC2C File Offset: 0x0009DE2C
			public request(byte[] buffer) : base(notice_guild_battle_rank.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700092B RID: 2347
			// (get) Token: 0x06002112 RID: 8466 RVA: 0x0009FC48 File Offset: 0x0009DE48
			// (set) Token: 0x06002113 RID: 8467 RVA: 0x0009FC50 File Offset: 0x0009DE50
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

			// Token: 0x1700092C RID: 2348
			// (get) Token: 0x06002114 RID: 8468 RVA: 0x0009FC68 File Offset: 0x0009DE68
			public bool HasGuildId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002115 RID: 8469 RVA: 0x0009FC78 File Offset: 0x0009DE78
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

			// Token: 0x06002116 RID: 8470 RVA: 0x0009FCD4 File Offset: 0x0009DED4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.guildId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001BA2 RID: 7074
			private static int max_field_count = 1;

			// Token: 0x04001BA3 RID: 7075
			private long _guildId;
		}
	}
}
