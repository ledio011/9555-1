using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000433 RID: 1075
	public class notify_confirm_state
	{
		// Token: 0x02000434 RID: 1076
		public class request : SprotoTypeBase
		{
			// Token: 0x06002142 RID: 8514 RVA: 0x000A0264 File Offset: 0x0009E464
			public request() : base(notify_confirm_state.request.max_field_count)
			{
			}

			// Token: 0x06002143 RID: 8515 RVA: 0x000A0274 File Offset: 0x0009E474
			public request(byte[] buffer) : base(notify_confirm_state.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700093D RID: 2365
			// (get) Token: 0x06002145 RID: 8517 RVA: 0x000A0290 File Offset: 0x0009E490
			// (set) Token: 0x06002146 RID: 8518 RVA: 0x000A0298 File Offset: 0x0009E498
			public long characterId
			{
				get
				{
					return this._characterId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._characterId = value;
				}
			}

			// Token: 0x1700093E RID: 2366
			// (get) Token: 0x06002147 RID: 8519 RVA: 0x000A02B0 File Offset: 0x0009E4B0
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700093F RID: 2367
			// (get) Token: 0x06002148 RID: 8520 RVA: 0x000A02C0 File Offset: 0x0009E4C0
			// (set) Token: 0x06002149 RID: 8521 RVA: 0x000A02C8 File Offset: 0x0009E4C8
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._state = value;
				}
			}

			// Token: 0x17000940 RID: 2368
			// (get) Token: 0x0600214A RID: 8522 RVA: 0x000A02E0 File Offset: 0x0009E4E0
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600214B RID: 8523 RVA: 0x000A02F0 File Offset: 0x0009E4F0
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 != 1)
						{
							this.deserialize.read_unknow_data();
						}
						else
						{
							this.state = this.deserialize.read_integer();
						}
					}
					else
					{
						this.characterId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600214C RID: 8524 RVA: 0x000A0368 File Offset: 0x0009E568
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.state, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001BAF RID: 7087
			private static int max_field_count = 2;

			// Token: 0x04001BB0 RID: 7088
			private long _characterId;

			// Token: 0x04001BB1 RID: 7089
			private long _state;
		}
	}
}
