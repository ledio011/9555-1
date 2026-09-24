using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003F2 RID: 1010
	public class guild_star : SprotoTypeBase
	{
		// Token: 0x06001F46 RID: 8006 RVA: 0x0009C258 File Offset: 0x0009A458
		public guild_star() : base(guild_star.max_field_count)
		{
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x0009C268 File Offset: 0x0009A468
		public guild_star(byte[] buffer) : base(guild_star.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x0009C284 File Offset: 0x0009A484
		// (set) Token: 0x06001F4A RID: 8010 RVA: 0x0009C28C File Offset: 0x0009A48C
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

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0009C2A4 File Offset: 0x0009A4A4
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x0009C2B4 File Offset: 0x0009A4B4
		// (set) Token: 0x06001F4D RID: 8013 RVA: 0x0009C2BC File Offset: 0x0009A4BC
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

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x0009C2D4 File Offset: 0x0009A4D4
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0009C2E4 File Offset: 0x0009A4E4
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

		// Token: 0x06001F50 RID: 8016 RVA: 0x0009C35C File Offset: 0x0009A55C
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

		// Token: 0x04001B27 RID: 6951
		private static int max_field_count = 2;

		// Token: 0x04001B28 RID: 6952
		private string _ID;

		// Token: 0x04001B29 RID: 6953
		private long _state;
	}
}
