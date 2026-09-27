using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005D7 RID: 1495
	public class sign_30_day
	{
		// Token: 0x020005D8 RID: 1496
		public class request : SprotoTypeBase
		{
			// Token: 0x06002B20 RID: 11040 RVA: 0x000B3344 File Offset: 0x000B1544
			public request() : base(sign_30_day.request.max_field_count)
			{
			}

			// Token: 0x06002B21 RID: 11041 RVA: 0x000B3354 File Offset: 0x000B1554
			public request(byte[] buffer) : base(sign_30_day.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C63 RID: 3171
			// (get) Token: 0x06002B23 RID: 11043 RVA: 0x000B3370 File Offset: 0x000B1570
			// (set) Token: 0x06002B24 RID: 11044 RVA: 0x000B3378 File Offset: 0x000B1578
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

			// Token: 0x17000C64 RID: 3172
			// (get) Token: 0x06002B25 RID: 11045 RVA: 0x000B3390 File Offset: 0x000B1590
			public bool HasDay
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002B26 RID: 11046 RVA: 0x000B33A0 File Offset: 0x000B15A0
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

			// Token: 0x06002B27 RID: 11047 RVA: 0x000B33FC File Offset: 0x000B15FC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.day, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E3E RID: 7742
			private static int max_field_count = 1;

			// Token: 0x04001E3F RID: 7743
			private long _day;
		}
	}
}
