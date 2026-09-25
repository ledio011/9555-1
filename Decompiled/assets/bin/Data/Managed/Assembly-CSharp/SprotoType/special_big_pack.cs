using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005E8 RID: 1512
	public class special_big_pack : SprotoTypeBase
	{
		// Token: 0x06002BE1 RID: 11233 RVA: 0x000B4CC0 File Offset: 0x000B2EC0
		public special_big_pack() : base(special_big_pack.max_field_count)
		{
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000B4CD0 File Offset: 0x000B2ED0
		public special_big_pack(byte[] buffer) : base(special_big_pack.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x000B4CEC File Offset: 0x000B2EEC
		// (set) Token: 0x06002BE5 RID: 11237 RVA: 0x000B4CF4 File Offset: 0x000B2EF4
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

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000B4D0C File Offset: 0x000B2F0C
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x000B4D1C File Offset: 0x000B2F1C
		// (set) Token: 0x06002BE8 RID: 11240 RVA: 0x000B4D24 File Offset: 0x000B2F24
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

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06002BE9 RID: 11241 RVA: 0x000B4D3C File Offset: 0x000B2F3C
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000B4D4C File Offset: 0x000B2F4C
		// (set) Token: 0x06002BEB RID: 11243 RVA: 0x000B4D54 File Offset: 0x000B2F54
		public long remain_times
		{
			get
			{
				return this._remain_times;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._remain_times = value;
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x000B4D6C File Offset: 0x000B2F6C
		public bool HasRemain_times
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x000B4D7C File Offset: 0x000B2F7C
		// (set) Token: 0x06002BEE RID: 11246 RVA: 0x000B4D84 File Offset: 0x000B2F84
		public long end_time
		{
			get
			{
				return this._end_time;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._end_time = value;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06002BEF RID: 11247 RVA: 0x000B4D9C File Offset: 0x000B2F9C
		public bool HasEnd_time
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000B4DAC File Offset: 0x000B2FAC
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
					this.state = this.deserialize.read_integer();
					break;
				case 2:
					this.remain_times = this.deserialize.read_integer();
					break;
				case 3:
					this.end_time = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000B4E58 File Offset: 0x000B3058
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.ID, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.state, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.remain_times, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.end_time, 3);
			}
			return this.serialize.close();
		}

		// Token: 0x04001E76 RID: 7798
		private static int max_field_count = 4;

		// Token: 0x04001E77 RID: 7799
		private string _ID;

		// Token: 0x04001E78 RID: 7800
		private long _state;

		// Token: 0x04001E79 RID: 7801
		private long _remain_times;

		// Token: 0x04001E7A RID: 7802
		private long _end_time;
	}
}
