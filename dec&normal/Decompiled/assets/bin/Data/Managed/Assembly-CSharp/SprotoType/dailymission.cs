using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200036D RID: 877
	public class dailymission : SprotoTypeBase
	{
		// Token: 0x06001A3B RID: 6715 RVA: 0x00091B20 File Offset: 0x0008FD20
		public dailymission() : base(dailymission.max_field_count)
		{
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00091B30 File Offset: 0x0008FD30
		public dailymission(byte[] buffer) : base(dailymission.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00091B4C File Offset: 0x0008FD4C
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x00091B54 File Offset: 0x0008FD54
		public string missionId
		{
			get
			{
				return this._missionId;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._missionId = value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x00091B6C File Offset: 0x0008FD6C
		public bool HasMissionId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x00091B7C File Offset: 0x0008FD7C
		// (set) Token: 0x06001A42 RID: 6722 RVA: 0x00091B84 File Offset: 0x0008FD84
		public long missionstate
		{
			get
			{
				return this._missionstate;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._missionstate = value;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x00091B9C File Offset: 0x0008FD9C
		public bool HasMissionstate
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00091BAC File Offset: 0x0008FDAC
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
						this.missionstate = this.deserialize.read_integer();
					}
				}
				else
				{
					this.missionId = this.deserialize.read_string();
				}
			}
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00091C24 File Offset: 0x0008FE24
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.missionId, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.missionstate, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x040019BD RID: 6589
		private static int max_field_count = 2;

		// Token: 0x040019BE RID: 6590
		private string _missionId;

		// Token: 0x040019BF RID: 6591
		private long _missionstate;
	}
}
