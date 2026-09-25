using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000626 RID: 1574
	public class tower_special_reward : SprotoTypeBase
	{
		// Token: 0x06002DDE RID: 11742 RVA: 0x000B8D50 File Offset: 0x000B6F50
		public tower_special_reward() : base(tower_special_reward.max_field_count)
		{
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000B8D60 File Offset: 0x000B6F60
		public tower_special_reward(byte[] buffer) : base(tower_special_reward.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x000B8D7C File Offset: 0x000B6F7C
		// (set) Token: 0x06002DE2 RID: 11746 RVA: 0x000B8D84 File Offset: 0x000B6F84
		public long floor
		{
			get
			{
				return this._floor;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._floor = value;
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x000B8D9C File Offset: 0x000B6F9C
		public bool HasFloor
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x000B8DAC File Offset: 0x000B6FAC
		// (set) Token: 0x06002DE5 RID: 11749 RVA: 0x000B8DB4 File Offset: 0x000B6FB4
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

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x000B8DCC File Offset: 0x000B6FCC
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000B8DDC File Offset: 0x000B6FDC
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
					this.floor = this.deserialize.read_integer();
				}
			}
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000B8E54 File Offset: 0x000B7054
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.floor, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.state, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x04001F07 RID: 7943
		private static int max_field_count = 2;

		// Token: 0x04001F08 RID: 7944
		private long _floor;

		// Token: 0x04001F09 RID: 7945
		private long _state;
	}
}
