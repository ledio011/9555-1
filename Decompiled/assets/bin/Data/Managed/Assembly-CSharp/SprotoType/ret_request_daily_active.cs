using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000567 RID: 1383
	public class ret_request_daily_active
	{
		// Token: 0x02000568 RID: 1384
		public class request : SprotoTypeBase
		{
			// Token: 0x06002830 RID: 10288 RVA: 0x000AD5C0 File Offset: 0x000AB7C0
			public request() : base(ret_request_daily_active.request.max_field_count)
			{
			}

			// Token: 0x06002831 RID: 10289 RVA: 0x000AD5D0 File Offset: 0x000AB7D0
			public request(byte[] buffer) : base(ret_request_daily_active.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B5F RID: 2911
			// (get) Token: 0x06002833 RID: 10291 RVA: 0x000AD5EC File Offset: 0x000AB7EC
			// (set) Token: 0x06002834 RID: 10292 RVA: 0x000AD5F4 File Offset: 0x000AB7F4
			public Dictionary<string, daily_active> daily_actives
			{
				get
				{
					return this._daily_actives;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._daily_actives = value;
				}
			}

			// Token: 0x17000B60 RID: 2912
			// (get) Token: 0x06002835 RID: 10293 RVA: 0x000AD60C File Offset: 0x000AB80C
			public bool HasDaily_actives
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B61 RID: 2913
			// (get) Token: 0x06002836 RID: 10294 RVA: 0x000AD61C File Offset: 0x000AB81C
			// (set) Token: 0x06002837 RID: 10295 RVA: 0x000AD624 File Offset: 0x000AB824
			public Dictionary<string, daily_reward> daily_rewards
			{
				get
				{
					return this._daily_rewards;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._daily_rewards = value;
				}
			}

			// Token: 0x17000B62 RID: 2914
			// (get) Token: 0x06002838 RID: 10296 RVA: 0x000AD63C File Offset: 0x000AB83C
			public bool HasDaily_rewards
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000B63 RID: 2915
			// (get) Token: 0x06002839 RID: 10297 RVA: 0x000AD64C File Offset: 0x000AB84C
			// (set) Token: 0x0600283A RID: 10298 RVA: 0x000AD654 File Offset: 0x000AB854
			public long score
			{
				get
				{
					return this._score;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._score = value;
				}
			}

			// Token: 0x17000B64 RID: 2916
			// (get) Token: 0x0600283B RID: 10299 RVA: 0x000AD66C File Offset: 0x000AB86C
			public bool HasScore
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x0600283C RID: 10300 RVA: 0x000AD67C File Offset: 0x000AB87C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.daily_actives = this.deserialize.read_map<string, daily_active>((daily_active v) => v.ID);
						break;
					case 1:
						this.daily_rewards = this.deserialize.read_map<string, daily_reward>((daily_reward v) => v.ID);
						break;
					case 2:
						this.score = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x0600283D RID: 10301 RVA: 0x000AD748 File Offset: 0x000AB948
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, daily_active>(this.daily_actives, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<string, daily_reward>(this.daily_rewards, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.score, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D70 RID: 7536
			private static int max_field_count = 3;

			// Token: 0x04001D71 RID: 7537
			private Dictionary<string, daily_active> _daily_actives;

			// Token: 0x04001D72 RID: 7538
			private Dictionary<string, daily_reward> _daily_rewards;

			// Token: 0x04001D73 RID: 7539
			private long _score;
		}
	}
}
