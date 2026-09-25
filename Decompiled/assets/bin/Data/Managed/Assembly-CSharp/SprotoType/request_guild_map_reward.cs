using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004B0 RID: 1200
	public class request_guild_map_reward
	{
		// Token: 0x020004B1 RID: 1201
		public class request : SprotoTypeBase
		{
			// Token: 0x06002407 RID: 9223 RVA: 0x000A5604 File Offset: 0x000A3804
			public request() : base(request_guild_map_reward.request.max_field_count)
			{
			}

			// Token: 0x06002408 RID: 9224 RVA: 0x000A5614 File Offset: 0x000A3814
			public request(byte[] buffer) : base(request_guild_map_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A15 RID: 2581
			// (get) Token: 0x0600240A RID: 9226 RVA: 0x000A5630 File Offset: 0x000A3830
			// (set) Token: 0x0600240B RID: 9227 RVA: 0x000A5638 File Offset: 0x000A3838
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

			// Token: 0x17000A16 RID: 2582
			// (get) Token: 0x0600240C RID: 9228 RVA: 0x000A5650 File Offset: 0x000A3850
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600240D RID: 9229 RVA: 0x000A5660 File Offset: 0x000A3860
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

			// Token: 0x0600240E RID: 9230 RVA: 0x000A56BC File Offset: 0x000A38BC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C5C RID: 7260
			private static int max_field_count = 1;

			// Token: 0x04001C5D RID: 7261
			private string _id;
		}
	}
}
