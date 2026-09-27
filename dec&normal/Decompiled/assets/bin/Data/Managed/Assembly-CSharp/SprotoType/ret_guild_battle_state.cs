using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000529 RID: 1321
	public class ret_guild_battle_state
	{
		// Token: 0x0200052A RID: 1322
		public class request : SprotoTypeBase
		{
			// Token: 0x06002688 RID: 9864 RVA: 0x000AA0F4 File Offset: 0x000A82F4
			public request() : base(ret_guild_battle_state.request.max_field_count)
			{
			}

			// Token: 0x06002689 RID: 9865 RVA: 0x000AA104 File Offset: 0x000A8304
			public request(byte[] buffer) : base(ret_guild_battle_state.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AC9 RID: 2761
			// (get) Token: 0x0600268B RID: 9867 RVA: 0x000AA120 File Offset: 0x000A8320
			// (set) Token: 0x0600268C RID: 9868 RVA: 0x000AA128 File Offset: 0x000A8328
			public guild_battle_info battle_info
			{
				get
				{
					return this._battle_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._battle_info = value;
				}
			}

			// Token: 0x17000ACA RID: 2762
			// (get) Token: 0x0600268D RID: 9869 RVA: 0x000AA140 File Offset: 0x000A8340
			public bool HasBattle_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600268E RID: 9870 RVA: 0x000AA150 File Offset: 0x000A8350
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
						this.battle_info = this.deserialize.read_obj<guild_battle_info>();
					}
				}
			}

			// Token: 0x0600268F RID: 9871 RVA: 0x000AA1AC File Offset: 0x000A83AC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.battle_info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CF9 RID: 7417
			private static int max_field_count = 1;

			// Token: 0x04001CFA RID: 7418
			private guild_battle_info _battle_info;
		}
	}
}
