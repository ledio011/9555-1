using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005EF RID: 1519
	public class start_enter_game
	{
		// Token: 0x020005F0 RID: 1520
		public class request : SprotoTypeBase
		{
			// Token: 0x06002C08 RID: 11272 RVA: 0x000B5128 File Offset: 0x000B3328
			public request() : base(start_enter_game.request.max_field_count)
			{
			}

			// Token: 0x06002C09 RID: 11273 RVA: 0x000B5138 File Offset: 0x000B3338
			public request(byte[] buffer) : base(start_enter_game.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000CC5 RID: 3269
			// (get) Token: 0x06002C0B RID: 11275 RVA: 0x000B5154 File Offset: 0x000B3354
			// (set) Token: 0x06002C0C RID: 11276 RVA: 0x000B515C File Offset: 0x000B335C
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

			// Token: 0x17000CC6 RID: 3270
			// (get) Token: 0x06002C0D RID: 11277 RVA: 0x000B5174 File Offset: 0x000B3374
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002C0E RID: 11278 RVA: 0x000B5184 File Offset: 0x000B3384
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

			// Token: 0x06002C0F RID: 11279 RVA: 0x000B51E0 File Offset: 0x000B33E0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E7F RID: 7807
			private static int max_field_count = 1;

			// Token: 0x04001E80 RID: 7808
			private long _state;
		}
	}
}
