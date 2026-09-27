using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000635 RID: 1589
	public class update_client_state
	{
		// Token: 0x02000636 RID: 1590
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E29 RID: 11817 RVA: 0x000B95F0 File Offset: 0x000B77F0
			public request() : base(update_client_state.request.max_field_count)
			{
			}

			// Token: 0x06002E2A RID: 11818 RVA: 0x000B9600 File Offset: 0x000B7800
			public request(byte[] buffer) : base(update_client_state.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D95 RID: 3477
			// (get) Token: 0x06002E2C RID: 11820 RVA: 0x000B961C File Offset: 0x000B781C
			// (set) Token: 0x06002E2D RID: 11821 RVA: 0x000B9624 File Offset: 0x000B7824
			public long id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x17000D96 RID: 3478
			// (get) Token: 0x06002E2E RID: 11822 RVA: 0x000B963C File Offset: 0x000B783C
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000D97 RID: 3479
			// (get) Token: 0x06002E2F RID: 11823 RVA: 0x000B964C File Offset: 0x000B784C
			// (set) Token: 0x06002E30 RID: 11824 RVA: 0x000B9654 File Offset: 0x000B7854
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

			// Token: 0x17000D98 RID: 3480
			// (get) Token: 0x06002E31 RID: 11825 RVA: 0x000B966C File Offset: 0x000B786C
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002E32 RID: 11826 RVA: 0x000B967C File Offset: 0x000B787C
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
						this.id = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002E33 RID: 11827 RVA: 0x000B96F4 File Offset: 0x000B78F4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.state, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F18 RID: 7960
			private static int max_field_count = 2;

			// Token: 0x04001F19 RID: 7961
			private long _id;

			// Token: 0x04001F1A RID: 7962
			private long _state;
		}
	}
}
