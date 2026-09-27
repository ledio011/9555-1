using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000449 RID: 1097
	public class random_attri : SprotoTypeBase
	{
		// Token: 0x06002239 RID: 8761 RVA: 0x000A2304 File Offset: 0x000A0504
		public random_attri() : base(random_attri.max_field_count)
		{
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000A2314 File Offset: 0x000A0514
		public random_attri(byte[] buffer) : base(random_attri.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x000A2330 File Offset: 0x000A0530
		// (set) Token: 0x0600223D RID: 8765 RVA: 0x000A2338 File Offset: 0x000A0538
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

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x000A2350 File Offset: 0x000A0550
		public bool HasIndex
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x000A2360 File Offset: 0x000A0560
		// (set) Token: 0x06002240 RID: 8768 RVA: 0x000A2368 File Offset: 0x000A0568
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._id = value;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x000A2380 File Offset: 0x000A0580
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x000A2390 File Offset: 0x000A0590
		// (set) Token: 0x06002243 RID: 8771 RVA: 0x000A2398 File Offset: 0x000A0598
		public long value
		{
			get
			{
				return this._value;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._value = value;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x000A23B0 File Offset: 0x000A05B0
		public bool HasValue
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x000A23C0 File Offset: 0x000A05C0
		// (set) Token: 0x06002246 RID: 8774 RVA: 0x000A23C8 File Offset: 0x000A05C8
		public long quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._quality = value;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x000A23E0 File Offset: 0x000A05E0
		public bool HasQuality
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x000A23F0 File Offset: 0x000A05F0
		// (set) Token: 0x06002249 RID: 8777 RVA: 0x000A23F8 File Offset: 0x000A05F8
		public string skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._skillId = value;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x000A2410 File Offset: 0x000A0610
		public bool HasSkillId
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x000A2420 File Offset: 0x000A0620
		// (set) Token: 0x0600224C RID: 8780 RVA: 0x000A2428 File Offset: 0x000A0628
		public string qualityId
		{
			get
			{
				return this._qualityId;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._qualityId = value;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x000A2440 File Offset: 0x000A0640
		public bool HasQualityId
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x000A2450 File Offset: 0x000A0650
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.index = this.deserialize.read_integer();
					break;
				case 1:
					this.id = this.deserialize.read_integer();
					break;
				case 2:
					this.value = this.deserialize.read_integer();
					break;
				case 3:
					this.quality = this.deserialize.read_integer();
					break;
				case 4:
					this.skillId = this.deserialize.read_string();
					break;
				case 5:
					this.qualityId = this.deserialize.read_string();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000A2530 File Offset: 0x000A0730
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.index, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.id, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.value, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.quality, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_string(this.skillId, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_string(this.qualityId, 5);
			}
			return this.serialize.close();
		}

		// Token: 0x04001BF6 RID: 7158
		private static int max_field_count = 6;

		// Token: 0x04001BF7 RID: 7159
		private long _index;

		// Token: 0x04001BF8 RID: 7160
		private long _id;

		// Token: 0x04001BF9 RID: 7161
		private long _value;

		// Token: 0x04001BFA RID: 7162
		private long _quality;

		// Token: 0x04001BFB RID: 7163
		private string _skillId;

		// Token: 0x04001BFC RID: 7164
		private string _qualityId;
	}
}
