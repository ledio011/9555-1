using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200054B RID: 1355
	public class ret_level_reward
	{
		// Token: 0x0200054C RID: 1356
		public class request : SprotoTypeBase
		{
			// Token: 0x0600277D RID: 10109 RVA: 0x000ABFB0 File Offset: 0x000AA1B0
			public request() : base(ret_level_reward.request.max_field_count)
			{
			}

			// Token: 0x0600277E RID: 10110 RVA: 0x000ABFC0 File Offset: 0x000AA1C0
			public request(byte[] buffer) : base(ret_level_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B25 RID: 2853
			// (get) Token: 0x06002780 RID: 10112 RVA: 0x000ABFDC File Offset: 0x000AA1DC
			// (set) Token: 0x06002781 RID: 10113 RVA: 0x000ABFE4 File Offset: 0x000AA1E4
			public Dictionary<string, level_reward> level_reward
			{
				get
				{
					return this._level_reward;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._level_reward = value;
				}
			}

			// Token: 0x17000B26 RID: 2854
			// (get) Token: 0x06002782 RID: 10114 RVA: 0x000ABFFC File Offset: 0x000AA1FC
			public bool HasLevel_reward
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002783 RID: 10115 RVA: 0x000AC00C File Offset: 0x000AA20C
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
						this.level_reward = this.deserialize.read_map<string, level_reward>((level_reward v) => v.ID);
					}
				}
			}

			// Token: 0x06002784 RID: 10116 RVA: 0x000AC084 File Offset: 0x000AA284
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, level_reward>(this.level_reward, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D3D RID: 7485
			private static int max_field_count = 1;

			// Token: 0x04001D3E RID: 7486
			private Dictionary<string, level_reward> _level_reward;
		}
	}
}
