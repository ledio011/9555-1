using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003CC RID: 972
	public class guild_battle_guess
	{
		// Token: 0x020003CD RID: 973
		public class request : SprotoTypeBase
		{
			// Token: 0x06001D97 RID: 7575 RVA: 0x000989A4 File Offset: 0x00096BA4
			public request() : base(guild_battle_guess.request.max_field_count)
			{
			}

			// Token: 0x06001D98 RID: 7576 RVA: 0x000989B4 File Offset: 0x00096BB4
			public request(byte[] buffer) : base(guild_battle_guess.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170007B7 RID: 1975
			// (get) Token: 0x06001D9A RID: 7578 RVA: 0x000989D0 File Offset: 0x00096BD0
			// (set) Token: 0x06001D9B RID: 7579 RVA: 0x000989D8 File Offset: 0x00096BD8
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

			// Token: 0x170007B8 RID: 1976
			// (get) Token: 0x06001D9C RID: 7580 RVA: 0x000989F0 File Offset: 0x00096BF0
			public bool HasGuildId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001D9D RID: 7581 RVA: 0x00098A00 File Offset: 0x00096C00
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

			// Token: 0x06001D9E RID: 7582 RVA: 0x00098A5C File Offset: 0x00096C5C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.guildId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001AAB RID: 6827
			private static int max_field_count = 1;

			// Token: 0x04001AAC RID: 6828
			private long _guildId;
		}
	}
}
