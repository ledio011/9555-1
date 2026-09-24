using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200036C RID: 876
	public class daily_reward : SprotoTypeBase
	{
		// Token: 0x06001A30 RID: 6704 RVA: 0x000919B0 File Offset: 0x0008FBB0
		public daily_reward() : base(daily_reward.max_field_count)
		{
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000919C0 File Offset: 0x0008FBC0
		public daily_reward(byte[] buffer) : base(daily_reward.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x000919DC File Offset: 0x0008FBDC
		// (set) Token: 0x06001A34 RID: 6708 RVA: 0x000919E4 File Offset: 0x0008FBE4
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

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x000919FC File Offset: 0x0008FBFC
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x00091A0C File Offset: 0x0008FC0C
		// (set) Token: 0x06001A37 RID: 6711 RVA: 0x00091A14 File Offset: 0x0008FC14
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

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00091A2C File Offset: 0x0008FC2C
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00091A3C File Offset: 0x0008FC3C
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

		// Token: 0x06001A3A RID: 6714 RVA: 0x00091AB4 File Offset: 0x0008FCB4
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

		// Token: 0x040019BA RID: 6586
		private static int max_field_count = 2;

		// Token: 0x040019BB RID: 6587
		private string _ID;

		// Token: 0x040019BC RID: 6588
		private long _state;
	}
}
