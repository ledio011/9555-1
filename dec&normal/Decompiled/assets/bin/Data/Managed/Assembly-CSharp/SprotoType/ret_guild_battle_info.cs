using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000523 RID: 1315
	public class ret_guild_battle_info
	{
		// Token: 0x02000524 RID: 1316
		public class request : SprotoTypeBase
		{
			// Token: 0x0600266D RID: 9837 RVA: 0x000A9DDC File Offset: 0x000A7FDC
			public request() : base(ret_guild_battle_info.request.max_field_count)
			{
			}

			// Token: 0x0600266E RID: 9838 RVA: 0x000A9DEC File Offset: 0x000A7FEC
			public request(byte[] buffer) : base(ret_guild_battle_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AC3 RID: 2755
			// (get) Token: 0x06002670 RID: 9840 RVA: 0x000A9E08 File Offset: 0x000A8008
			// (set) Token: 0x06002671 RID: 9841 RVA: 0x000A9E10 File Offset: 0x000A8010
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

			// Token: 0x17000AC4 RID: 2756
			// (get) Token: 0x06002672 RID: 9842 RVA: 0x000A9E28 File Offset: 0x000A8028
			public bool HasBattle_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002673 RID: 9843 RVA: 0x000A9E38 File Offset: 0x000A8038
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

			// Token: 0x06002674 RID: 9844 RVA: 0x000A9E94 File Offset: 0x000A8094
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.battle_info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CF3 RID: 7411
			private static int max_field_count = 1;

			// Token: 0x04001CF4 RID: 7412
			private guild_battle_info _battle_info;
		}
	}
}
