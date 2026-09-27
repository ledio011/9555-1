using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003CA RID: 970
	public class guild_battle_finish_info
	{
		// Token: 0x020003CB RID: 971
		public class request : SprotoTypeBase
		{
			// Token: 0x06001D8E RID: 7566 RVA: 0x0009889C File Offset: 0x00096A9C
			public request() : base(guild_battle_finish_info.request.max_field_count)
			{
			}

			// Token: 0x06001D8F RID: 7567 RVA: 0x000988AC File Offset: 0x00096AAC
			public request(byte[] buffer) : base(guild_battle_finish_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170007B5 RID: 1973
			// (get) Token: 0x06001D91 RID: 7569 RVA: 0x000988C8 File Offset: 0x00096AC8
			// (set) Token: 0x06001D92 RID: 7570 RVA: 0x000988D0 File Offset: 0x00096AD0
			public guild_battle_score_info guild_battle_score_info
			{
				get
				{
					return this._guild_battle_score_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guild_battle_score_info = value;
				}
			}

			// Token: 0x170007B6 RID: 1974
			// (get) Token: 0x06001D93 RID: 7571 RVA: 0x000988E8 File Offset: 0x00096AE8
			public bool HasGuild_battle_score_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001D94 RID: 7572 RVA: 0x000988F8 File Offset: 0x00096AF8
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
						this.guild_battle_score_info = this.deserialize.read_obj<guild_battle_score_info>();
					}
				}
			}

			// Token: 0x06001D95 RID: 7573 RVA: 0x00098954 File Offset: 0x00096B54
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.guild_battle_score_info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001AA9 RID: 6825
			private static int max_field_count = 1;

			// Token: 0x04001AAA RID: 6826
			private guild_battle_score_info _guild_battle_score_info;
		}
	}
}
