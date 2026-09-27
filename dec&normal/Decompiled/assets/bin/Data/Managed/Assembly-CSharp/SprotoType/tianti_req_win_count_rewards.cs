using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200061C RID: 1564
	public class tianti_req_win_count_rewards
	{
		// Token: 0x0200061D RID: 1565
		public class request : SprotoTypeBase
		{
			// Token: 0x06002D8D RID: 11661 RVA: 0x000B8310 File Offset: 0x000B6510
			public request() : base(tianti_req_win_count_rewards.request.max_field_count)
			{
			}

			// Token: 0x06002D8E RID: 11662 RVA: 0x000B8320 File Offset: 0x000B6520
			public request(byte[] buffer) : base(tianti_req_win_count_rewards.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D63 RID: 3427
			// (get) Token: 0x06002D90 RID: 11664 RVA: 0x000B833C File Offset: 0x000B653C
			// (set) Token: 0x06002D91 RID: 11665 RVA: 0x000B8344 File Offset: 0x000B6544
			public long index
			{
				get
				{
					return this._index;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._index = value;
				}
			}

			// Token: 0x17000D64 RID: 3428
			// (get) Token: 0x06002D92 RID: 11666 RVA: 0x000B835C File Offset: 0x000B655C
			public bool HasIndex
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002D93 RID: 11667 RVA: 0x000B836C File Offset: 0x000B656C
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
						this.index = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002D94 RID: 11668 RVA: 0x000B83C8 File Offset: 0x000B65C8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.index, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001EF1 RID: 7921
			private static int max_field_count = 1;

			// Token: 0x04001EF2 RID: 7922
			private long _index;
		}
	}
}
