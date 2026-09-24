using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004B2 RID: 1202
	public class request_guild_reward
	{
		// Token: 0x020004B3 RID: 1203
		public class request : SprotoTypeBase
		{
			// Token: 0x06002410 RID: 9232 RVA: 0x000A570C File Offset: 0x000A390C
			public request() : base(request_guild_reward.request.max_field_count)
			{
			}

			// Token: 0x06002411 RID: 9233 RVA: 0x000A571C File Offset: 0x000A391C
			public request(byte[] buffer) : base(request_guild_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A17 RID: 2583
			// (get) Token: 0x06002413 RID: 9235 RVA: 0x000A5738 File Offset: 0x000A3938
			// (set) Token: 0x06002414 RID: 9236 RVA: 0x000A5740 File Offset: 0x000A3940
			public string id
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

			// Token: 0x17000A18 RID: 2584
			// (get) Token: 0x06002415 RID: 9237 RVA: 0x000A5758 File Offset: 0x000A3958
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002416 RID: 9238 RVA: 0x000A5768 File Offset: 0x000A3968
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
						this.id = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002417 RID: 9239 RVA: 0x000A57C4 File Offset: 0x000A39C4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C5E RID: 7262
			private static int max_field_count = 1;

			// Token: 0x04001C5F RID: 7263
			private string _id;
		}
	}
}
