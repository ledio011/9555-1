using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005FB RID: 1531
	public class syn_rank_pvp_data
	{
		// Token: 0x020005FC RID: 1532
		public class request : SprotoTypeBase
		{
			// Token: 0x06002C42 RID: 11330 RVA: 0x000B57DC File Offset: 0x000B39DC
			public request() : base(syn_rank_pvp_data.request.max_field_count)
			{
			}

			// Token: 0x06002C43 RID: 11331 RVA: 0x000B57EC File Offset: 0x000B39EC
			public request(byte[] buffer) : base(syn_rank_pvp_data.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000CD3 RID: 3283
			// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000B5808 File Offset: 0x000B3A08
			// (set) Token: 0x06002C46 RID: 11334 RVA: 0x000B5810 File Offset: 0x000B3A10
			public long combValue
			{
				get
				{
					return this._combValue;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._combValue = value;
				}
			}

			// Token: 0x17000CD4 RID: 3284
			// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000B5828 File Offset: 0x000B3A28
			public bool HasCombValue
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000CD5 RID: 3285
			// (get) Token: 0x06002C48 RID: 11336 RVA: 0x000B5838 File Offset: 0x000B3A38
			// (set) Token: 0x06002C49 RID: 11337 RVA: 0x000B5840 File Offset: 0x000B3A40
			public long times
			{
				get
				{
					return this._times;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._times = value;
				}
			}

			// Token: 0x17000CD6 RID: 3286
			// (get) Token: 0x06002C4A RID: 11338 RVA: 0x000B5858 File Offset: 0x000B3A58
			public bool HasTimes
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000CD7 RID: 3287
			// (get) Token: 0x06002C4B RID: 11339 RVA: 0x000B5868 File Offset: 0x000B3A68
			// (set) Token: 0x06002C4C RID: 11340 RVA: 0x000B5870 File Offset: 0x000B3A70
			public long rankPos
			{
				get
				{
					return this._rankPos;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._rankPos = value;
				}
			}

			// Token: 0x17000CD8 RID: 3288
			// (get) Token: 0x06002C4D RID: 11341 RVA: 0x000B5888 File Offset: 0x000B3A88
			public bool HasRankPos
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000CD9 RID: 3289
			// (get) Token: 0x06002C4E RID: 11342 RVA: 0x000B5898 File Offset: 0x000B3A98
			// (set) Token: 0x06002C4F RID: 11343 RVA: 0x000B58A0 File Offset: 0x000B3AA0
			public long bestRankPos
			{
				get
				{
					return this._bestRankPos;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._bestRankPos = value;
				}
			}

			// Token: 0x17000CDA RID: 3290
			// (get) Token: 0x06002C50 RID: 11344 RVA: 0x000B58B8 File Offset: 0x000B3AB8
			public bool HasBestRankPos
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000CDB RID: 3291
			// (get) Token: 0x06002C51 RID: 11345 RVA: 0x000B58C8 File Offset: 0x000B3AC8
			// (set) Token: 0x06002C52 RID: 11346 RVA: 0x000B58D0 File Offset: 0x000B3AD0
			public long rewards
			{
				get
				{
					return this._rewards;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._rewards = value;
				}
			}

			// Token: 0x17000CDC RID: 3292
			// (get) Token: 0x06002C53 RID: 11347 RVA: 0x000B58E8 File Offset: 0x000B3AE8
			public bool HasRewards
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000CDD RID: 3293
			// (get) Token: 0x06002C54 RID: 11348 RVA: 0x000B58F8 File Offset: 0x000B3AF8
			// (set) Token: 0x06002C55 RID: 11349 RVA: 0x000B5900 File Offset: 0x000B3B00
			public long preRankPos
			{
				get
				{
					return this._preRankPos;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._preRankPos = value;
				}
			}

			// Token: 0x17000CDE RID: 3294
			// (get) Token: 0x06002C56 RID: 11350 RVA: 0x000B5918 File Offset: 0x000B3B18
			public bool HasPreRankPos
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x17000CDF RID: 3295
			// (get) Token: 0x06002C57 RID: 11351 RVA: 0x000B5928 File Offset: 0x000B3B28
			// (set) Token: 0x06002C58 RID: 11352 RVA: 0x000B5930 File Offset: 0x000B3B30
			public long winCount
			{
				get
				{
					return this._winCount;
				}
				set
				{
					this.has_field.set_field(6, true);
					this._winCount = value;
				}
			}

			// Token: 0x17000CE0 RID: 3296
			// (get) Token: 0x06002C59 RID: 11353 RVA: 0x000B5948 File Offset: 0x000B3B48
			public bool HasWinCount
			{
				get
				{
					return this.has_field.has_field(6);
				}
			}

			// Token: 0x17000CE1 RID: 3297
			// (get) Token: 0x06002C5A RID: 11354 RVA: 0x000B5958 File Offset: 0x000B3B58
			// (set) Token: 0x06002C5B RID: 11355 RVA: 0x000B5960 File Offset: 0x000B3B60
			public long winRewards
			{
				get
				{
					return this._winRewards;
				}
				set
				{
					this.has_field.set_field(7, true);
					this._winRewards = value;
				}
			}

			// Token: 0x17000CE2 RID: 3298
			// (get) Token: 0x06002C5C RID: 11356 RVA: 0x000B5978 File Offset: 0x000B3B78
			public bool HasWinRewards
			{
				get
				{
					return this.has_field.has_field(7);
				}
			}

			// Token: 0x06002C5D RID: 11357 RVA: 0x000B5988 File Offset: 0x000B3B88
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.combValue = this.deserialize.read_integer();
						break;
					case 1:
						this.times = this.deserialize.read_integer();
						break;
					case 2:
						this.rankPos = this.deserialize.read_integer();
						break;
					case 3:
						this.bestRankPos = this.deserialize.read_integer();
						break;
					case 4:
						this.rewards = this.deserialize.read_integer();
						break;
					case 5:
						this.preRankPos = this.deserialize.read_integer();
						break;
					case 6:
						this.winCount = this.deserialize.read_integer();
						break;
					case 7:
						this.winRewards = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002C5E RID: 11358 RVA: 0x000B5A9C File Offset: 0x000B3C9C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.combValue, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.times, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.rankPos, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.bestRankPos, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.rewards, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.preRankPos, 5);
				}
				if (this.has_field.has_field(6))
				{
					this.serialize.write_integer(this.winCount, 6);
				}
				if (this.has_field.has_field(7))
				{
					this.serialize.write_integer(this.winRewards, 7);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E8D RID: 7821
			private static int max_field_count = 8;

			// Token: 0x04001E8E RID: 7822
			private long _combValue;

			// Token: 0x04001E8F RID: 7823
			private long _times;

			// Token: 0x04001E90 RID: 7824
			private long _rankPos;

			// Token: 0x04001E91 RID: 7825
			private long _bestRankPos;

			// Token: 0x04001E92 RID: 7826
			private long _rewards;

			// Token: 0x04001E93 RID: 7827
			private long _preRankPos;

			// Token: 0x04001E94 RID: 7828
			private long _winCount;

			// Token: 0x04001E95 RID: 7829
			private long _winRewards;
		}
	}
}
