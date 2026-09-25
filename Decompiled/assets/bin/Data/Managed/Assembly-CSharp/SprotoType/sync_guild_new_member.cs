using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000609 RID: 1545
	public class sync_guild_new_member
	{
		// Token: 0x0200060A RID: 1546
		public class request : SprotoTypeBase
		{
			// Token: 0x06002CC9 RID: 11465 RVA: 0x000B696C File Offset: 0x000B4B6C
			public request() : base(sync_guild_new_member.request.max_field_count)
			{
			}

			// Token: 0x06002CCA RID: 11466 RVA: 0x000B697C File Offset: 0x000B4B7C
			public request(byte[] buffer) : base(sync_guild_new_member.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D0D RID: 3341
			// (get) Token: 0x06002CCC RID: 11468 RVA: 0x000B6998 File Offset: 0x000B4B98
			// (set) Token: 0x06002CCD RID: 11469 RVA: 0x000B69A0 File Offset: 0x000B4BA0
			public guild_member_info guild_member_info
			{
				get
				{
					return this._guild_member_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guild_member_info = value;
				}
			}

			// Token: 0x17000D0E RID: 3342
			// (get) Token: 0x06002CCE RID: 11470 RVA: 0x000B69B8 File Offset: 0x000B4BB8
			public bool HasGuild_member_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002CCF RID: 11471 RVA: 0x000B69C8 File Offset: 0x000B4BC8
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
						this.guild_member_info = this.deserialize.read_obj<guild_member_info>();
					}
				}
			}

			// Token: 0x06002CD0 RID: 11472 RVA: 0x000B6A24 File Offset: 0x000B4C24
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.guild_member_info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001EB7 RID: 7863
			private static int max_field_count = 1;

			// Token: 0x04001EB8 RID: 7864
			private guild_member_info _guild_member_info;
		}
	}
}
