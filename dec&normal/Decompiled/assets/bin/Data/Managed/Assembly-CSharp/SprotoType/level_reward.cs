using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000406 RID: 1030
	public class level_reward : SprotoTypeBase
	{
		// Token: 0x06001FDC RID: 8156 RVA: 0x0009D51C File Offset: 0x0009B71C
		public level_reward() : base(level_reward.max_field_count)
		{
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0009D52C File Offset: 0x0009B72C
		public level_reward(byte[] buffer) : base(level_reward.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x0009D548 File Offset: 0x0009B748
		// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x0009D550 File Offset: 0x0009B750
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

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x0009D568 File Offset: 0x0009B768
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x0009D578 File Offset: 0x0009B778
		// (set) Token: 0x06001FE3 RID: 8163 RVA: 0x0009D580 File Offset: 0x0009B780
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

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x0009D598 File Offset: 0x0009B798
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x0009D5A8 File Offset: 0x0009B7A8
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
					this.ID = this.deserialize.read_string();
				}
			}
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0009D620 File Offset: 0x0009B820
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
			return this.serialize.close();
		}

		// Token: 0x04001B4E RID: 6990
		private static int max_field_count = 2;

		// Token: 0x04001B4F RID: 6991
		private string _ID;

		// Token: 0x04001B50 RID: 6992
		private long _state;
	}
}
