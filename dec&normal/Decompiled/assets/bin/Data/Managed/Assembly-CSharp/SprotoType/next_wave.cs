using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000423 RID: 1059
	public class next_wave
	{
		// Token: 0x02000424 RID: 1060
		public class request : SprotoTypeBase
		{
			// Token: 0x060020D9 RID: 8409 RVA: 0x0009F568 File Offset: 0x0009D768
			public request() : base(next_wave.request.max_field_count)
			{
			}

			// Token: 0x060020DA RID: 8410 RVA: 0x0009F578 File Offset: 0x0009D778
			public request(byte[] buffer) : base(next_wave.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000917 RID: 2327
			// (get) Token: 0x060020DC RID: 8412 RVA: 0x0009F594 File Offset: 0x0009D794
			// (set) Token: 0x060020DD RID: 8413 RVA: 0x0009F59C File Offset: 0x0009D79C
			public long waveid
			{
				get
				{
					return this._waveid;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._waveid = value;
				}
			}

			// Token: 0x17000918 RID: 2328
			// (get) Token: 0x060020DE RID: 8414 RVA: 0x0009F5B4 File Offset: 0x0009D7B4
			public bool HasWaveid
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060020DF RID: 8415 RVA: 0x0009F5C4 File Offset: 0x0009D7C4
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
						this.waveid = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060020E0 RID: 8416 RVA: 0x0009F620 File Offset: 0x0009D820
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.waveid, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B94 RID: 7060
			private static int max_field_count = 1;

			// Token: 0x04001B95 RID: 7061
			private long _waveid;
		}
	}
}
