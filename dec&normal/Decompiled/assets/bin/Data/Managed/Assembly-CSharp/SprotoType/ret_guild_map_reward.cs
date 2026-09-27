using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200053B RID: 1339
	public class ret_guild_map_reward
	{
		// Token: 0x0200053C RID: 1340
		public class request : SprotoTypeBase
		{
			// Token: 0x0600270A RID: 9994 RVA: 0x000AB148 File Offset: 0x000A9348
			public request() : base(ret_guild_map_reward.request.max_field_count)
			{
			}

			// Token: 0x0600270B RID: 9995 RVA: 0x000AB158 File Offset: 0x000A9358
			public request(byte[] buffer) : base(ret_guild_map_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AFB RID: 2811
			// (get) Token: 0x0600270D RID: 9997 RVA: 0x000AB174 File Offset: 0x000A9374
			// (set) Token: 0x0600270E RID: 9998 RVA: 0x000AB17C File Offset: 0x000A937C
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

			// Token: 0x17000AFC RID: 2812
			// (get) Token: 0x0600270F RID: 9999 RVA: 0x000AB194 File Offset: 0x000A9394
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000AFD RID: 2813
			// (get) Token: 0x06002710 RID: 10000 RVA: 0x000AB1A4 File Offset: 0x000A93A4
			// (set) Token: 0x06002711 RID: 10001 RVA: 0x000AB1AC File Offset: 0x000A93AC
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

			// Token: 0x17000AFE RID: 2814
			// (get) Token: 0x06002712 RID: 10002 RVA: 0x000AB1C4 File Offset: 0x000A93C4
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002713 RID: 10003 RVA: 0x000AB1D4 File Offset: 0x000A93D4
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
						this.id = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002714 RID: 10004 RVA: 0x000AB24C File Offset: 0x000A944C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.state, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D1C RID: 7452
			private static int max_field_count = 2;

			// Token: 0x04001D1D RID: 7453
			private string _id;

			// Token: 0x04001D1E RID: 7454
			private long _state;
		}
	}
}
