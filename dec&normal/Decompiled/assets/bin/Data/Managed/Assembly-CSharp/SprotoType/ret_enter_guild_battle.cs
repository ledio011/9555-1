using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000519 RID: 1305
	public class ret_enter_guild_battle
	{
		// Token: 0x0200051A RID: 1306
		public class request : SprotoTypeBase
		{
			// Token: 0x0600262D RID: 9773 RVA: 0x000A95F8 File Offset: 0x000A77F8
			public request() : base(ret_enter_guild_battle.request.max_field_count)
			{
			}

			// Token: 0x0600262E RID: 9774 RVA: 0x000A9608 File Offset: 0x000A7808
			public request(byte[] buffer) : base(ret_enter_guild_battle.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AAD RID: 2733
			// (get) Token: 0x06002630 RID: 9776 RVA: 0x000A9624 File Offset: 0x000A7824
			// (set) Token: 0x06002631 RID: 9777 RVA: 0x000A962C File Offset: 0x000A782C
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._state = value;
				}
			}

			// Token: 0x17000AAE RID: 2734
			// (get) Token: 0x06002632 RID: 9778 RVA: 0x000A9644 File Offset: 0x000A7844
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002633 RID: 9779 RVA: 0x000A9654 File Offset: 0x000A7854
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
						this.state = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002634 RID: 9780 RVA: 0x000A96B0 File Offset: 0x000A78B0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CE2 RID: 7394
			private static int max_field_count = 1;

			// Token: 0x04001CE3 RID: 7395
			private long _state;
		}
	}
}
