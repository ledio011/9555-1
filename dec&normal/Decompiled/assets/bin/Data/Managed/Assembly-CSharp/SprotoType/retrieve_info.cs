using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005B1 RID: 1457
	public class retrieve_info : SprotoTypeBase
	{
		// Token: 0x06002A09 RID: 10761 RVA: 0x000B1030 File Offset: 0x000AF230
		public retrieve_info() : base(retrieve_info.max_field_count)
		{
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x000B1040 File Offset: 0x000AF240
		public retrieve_info(byte[] buffer) : base(retrieve_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06002A0C RID: 10764 RVA: 0x000B105C File Offset: 0x000AF25C
		// (set) Token: 0x06002A0D RID: 10765 RVA: 0x000B1064 File Offset: 0x000AF264
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

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06002A0E RID: 10766 RVA: 0x000B107C File Offset: 0x000AF27C
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06002A0F RID: 10767 RVA: 0x000B108C File Offset: 0x000AF28C
		// (set) Token: 0x06002A10 RID: 10768 RVA: 0x000B1094 File Offset: 0x000AF294
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

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06002A11 RID: 10769 RVA: 0x000B10AC File Offset: 0x000AF2AC
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06002A12 RID: 10770 RVA: 0x000B10BC File Offset: 0x000AF2BC
		// (set) Token: 0x06002A13 RID: 10771 RVA: 0x000B10C4 File Offset: 0x000AF2C4
		public long count
		{
			get
			{
				return this._count;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._count = value;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x000B10DC File Offset: 0x000AF2DC
		public bool HasCount
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000B10EC File Offset: 0x000AF2EC
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
					this.count = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000B1180 File Offset: 0x000AF380
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
				this.serialize.write_integer(this.count, 2);
			}
			return this.serialize.close();
		}

		// Token: 0x04001DF5 RID: 7669
		private static int max_field_count = 3;

		// Token: 0x04001DF6 RID: 7670
		private string _ID;

		// Token: 0x04001DF7 RID: 7671
		private long _state;

		// Token: 0x04001DF8 RID: 7672
		private long _count;
	}
}
