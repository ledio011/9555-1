using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200036B RID: 875
	public class daily_buy : SprotoTypeBase
	{
		// Token: 0x06001A25 RID: 6693 RVA: 0x00091840 File Offset: 0x0008FA40
		public daily_buy() : base(daily_buy.max_field_count)
		{
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00091850 File Offset: 0x0008FA50
		public daily_buy(byte[] buffer) : base(daily_buy.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x0009186C File Offset: 0x0008FA6C
		// (set) Token: 0x06001A29 RID: 6697 RVA: 0x00091874 File Offset: 0x0008FA74
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

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x0009188C File Offset: 0x0008FA8C
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x0009189C File Offset: 0x0008FA9C
		// (set) Token: 0x06001A2C RID: 6700 RVA: 0x000918A4 File Offset: 0x0008FAA4
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

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x000918BC File Offset: 0x0008FABC
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000918CC File Offset: 0x0008FACC
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

		// Token: 0x06001A2F RID: 6703 RVA: 0x00091944 File Offset: 0x0008FB44
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

		// Token: 0x040019B7 RID: 6583
		private static int max_field_count = 2;

		// Token: 0x040019B8 RID: 6584
		private string _ID;

		// Token: 0x040019B9 RID: 6585
		private long _state;
	}
}
