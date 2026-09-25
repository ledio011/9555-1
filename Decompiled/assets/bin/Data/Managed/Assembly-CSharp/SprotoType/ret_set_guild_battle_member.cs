using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000591 RID: 1425
	public class ret_set_guild_battle_member
	{
		// Token: 0x02000592 RID: 1426
		public class request : SprotoTypeBase
		{
			// Token: 0x06002939 RID: 10553 RVA: 0x000AF660 File Offset: 0x000AD860
			public request() : base(ret_set_guild_battle_member.request.max_field_count)
			{
			}

			// Token: 0x0600293A RID: 10554 RVA: 0x000AF670 File Offset: 0x000AD870
			public request(byte[] buffer) : base(ret_set_guild_battle_member.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BB3 RID: 2995
			// (get) Token: 0x0600293C RID: 10556 RVA: 0x000AF68C File Offset: 0x000AD88C
			// (set) Token: 0x0600293D RID: 10557 RVA: 0x000AF694 File Offset: 0x000AD894
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

			// Token: 0x17000BB4 RID: 2996
			// (get) Token: 0x0600293E RID: 10558 RVA: 0x000AF6AC File Offset: 0x000AD8AC
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600293F RID: 10559 RVA: 0x000AF6BC File Offset: 0x000AD8BC
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

			// Token: 0x06002940 RID: 10560 RVA: 0x000AF718 File Offset: 0x000AD918
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DBC RID: 7612
			private static int max_field_count = 1;

			// Token: 0x04001DBD RID: 7613
			private long _state;
		}
	}
}
