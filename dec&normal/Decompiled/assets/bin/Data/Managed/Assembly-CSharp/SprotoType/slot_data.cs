using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005E4 RID: 1508
	public class slot_data : SprotoTypeBase
	{
		// Token: 0x06002B8A RID: 11146 RVA: 0x000B40E0 File Offset: 0x000B22E0
		public slot_data() : base(slot_data.max_field_count)
		{
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000B40F0 File Offset: 0x000B22F0
		public slot_data(byte[] buffer) : base(slot_data.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x000B410C File Offset: 0x000B230C
		// (set) Token: 0x06002B8E RID: 11150 RVA: 0x000B4114 File Offset: 0x000B2314
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._ID = value;
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x000B412C File Offset: 0x000B232C
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x000B413C File Offset: 0x000B233C
		// (set) Token: 0x06002B91 RID: 11153 RVA: 0x000B4144 File Offset: 0x000B2344
		public string RewardMap
		{
			get
			{
				return this._RewardMap;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._RewardMap = value;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x000B415C File Offset: 0x000B235C
		public bool HasRewardMap
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x000B416C File Offset: 0x000B236C
		// (set) Token: 0x06002B94 RID: 11156 RVA: 0x000B4174 File Offset: 0x000B2374
		public string RewardEffect
		{
			get
			{
				return this._RewardEffect;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._RewardEffect = value;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x000B418C File Offset: 0x000B238C
		public bool HasRewardEffect
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06002B96 RID: 11158 RVA: 0x000B419C File Offset: 0x000B239C
		// (set) Token: 0x06002B97 RID: 11159 RVA: 0x000B41A4 File Offset: 0x000B23A4
		public string Desc
		{
			get
			{
				return this._Desc;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._Desc = value;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x000B41BC File Offset: 0x000B23BC
		public bool HasDesc
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06002B99 RID: 11161 RVA: 0x000B41CC File Offset: 0x000B23CC
		// (set) Token: 0x06002B9A RID: 11162 RVA: 0x000B41D4 File Offset: 0x000B23D4
		public long Rank
		{
			get
			{
				return this._Rank;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._Rank = value;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x000B41EC File Offset: 0x000B23EC
		public bool HasRank
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x000B41FC File Offset: 0x000B23FC
		// (set) Token: 0x06002B9D RID: 11165 RVA: 0x000B4204 File Offset: 0x000B2404
		public string ShowRewardID
		{
			get
			{
				return this._ShowRewardID;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._ShowRewardID = value;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x000B421C File Offset: 0x000B241C
		public bool HasShowRewardID
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06002B9F RID: 11167 RVA: 0x000B422C File Offset: 0x000B242C
		// (set) Token: 0x06002BA0 RID: 11168 RVA: 0x000B4234 File Offset: 0x000B2434
		public long PriceType
		{
			get
			{
				return this._PriceType;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._PriceType = value;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x000B424C File Offset: 0x000B244C
		public bool HasPriceType
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x000B425C File Offset: 0x000B245C
		// (set) Token: 0x06002BA3 RID: 11171 RVA: 0x000B4264 File Offset: 0x000B2464
		public long PriceCost
		{
			get
			{
				return this._PriceCost;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._PriceCost = value;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x000B427C File Offset: 0x000B247C
		public bool HasPriceCost
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000B428C File Offset: 0x000B248C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.ID = this.deserialize.read_string();
					break;
				case 1:
					this.RewardMap = this.deserialize.read_string();
					break;
				case 2:
					this.RewardEffect = this.deserialize.read_string();
					break;
				case 3:
					this.Desc = this.deserialize.read_string();
					break;
				case 4:
					this.Rank = this.deserialize.read_integer();
					break;
				case 5:
					this.ShowRewardID = this.deserialize.read_string();
					break;
				case 6:
					this.PriceType = this.deserialize.read_integer();
					break;
				case 7:
					this.PriceCost = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000B43A0 File Offset: 0x000B25A0
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.ID, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.RewardMap, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_string(this.RewardEffect, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_string(this.Desc, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.Rank, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_string(this.ShowRewardID, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.PriceType, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.PriceCost, 7);
			}
			return this.serialize.close();
		}

		// Token: 0x04001E5B RID: 7771
		private static int max_field_count = 8;

		// Token: 0x04001E5C RID: 7772
		private string _ID;

		// Token: 0x04001E5D RID: 7773
		private string _RewardMap;

		// Token: 0x04001E5E RID: 7774
		private string _RewardEffect;

		// Token: 0x04001E5F RID: 7775
		private string _Desc;

		// Token: 0x04001E60 RID: 7776
		private long _Rank;

		// Token: 0x04001E61 RID: 7777
		private string _ShowRewardID;

		// Token: 0x04001E62 RID: 7778
		private long _PriceType;

		// Token: 0x04001E63 RID: 7779
		private long _PriceCost;
	}
}
