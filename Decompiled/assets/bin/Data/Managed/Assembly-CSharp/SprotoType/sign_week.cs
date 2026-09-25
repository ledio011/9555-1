using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005DB RID: 1499
	public class sign_week
	{
		// Token: 0x020005DC RID: 1500
		public class request : SprotoTypeBase
		{
			// Token: 0x06002B32 RID: 11058 RVA: 0x000B3554 File Offset: 0x000B1754
			public request() : base(sign_week.request.max_field_count)
			{
			}

			// Token: 0x06002B33 RID: 11059 RVA: 0x000B3564 File Offset: 0x000B1764
			public request(byte[] buffer) : base(sign_week.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C67 RID: 3175
			// (get) Token: 0x06002B35 RID: 11061 RVA: 0x000B3580 File Offset: 0x000B1780
			// (set) Token: 0x06002B36 RID: 11062 RVA: 0x000B3588 File Offset: 0x000B1788
			public long day
			{
				get
				{
					return this._day;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._day = value;
				}
			}

			// Token: 0x17000C68 RID: 3176
			// (get) Token: 0x06002B37 RID: 11063 RVA: 0x000B35A0 File Offset: 0x000B17A0
			public bool HasDay
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002B38 RID: 11064 RVA: 0x000B35B0 File Offset: 0x000B17B0
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.day = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002B39 RID: 11065 RVA: 0x000B360C File Offset: 0x000B180C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.day, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E42 RID: 7746
			private static int max_field_count = 1;

			// Token: 0x04001E43 RID: 7747
			private long _day;
		}
	}
}
