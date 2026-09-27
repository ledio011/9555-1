using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200045C RID: 1116
	public class real_pvp_register
	{
		// Token: 0x0200045D RID: 1117
		public class request : SprotoTypeBase
		{
			// Token: 0x060022A2 RID: 8866 RVA: 0x000A2F48 File Offset: 0x000A1148
			public request() : base(real_pvp_register.request.max_field_count)
			{
			}

			// Token: 0x060022A3 RID: 8867 RVA: 0x000A2F58 File Offset: 0x000A1158
			public request(byte[] buffer) : base(real_pvp_register.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009CF RID: 2511
			// (get) Token: 0x060022A5 RID: 8869 RVA: 0x000A2F74 File Offset: 0x000A1174
			// (set) Token: 0x060022A6 RID: 8870 RVA: 0x000A2F7C File Offset: 0x000A117C
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._state = value;
				}
			}

			// Token: 0x170009D0 RID: 2512
			// (get) Token: 0x060022A7 RID: 8871 RVA: 0x000A2F94 File Offset: 0x000A1194
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060022A8 RID: 8872 RVA: 0x000A2FA4 File Offset: 0x000A11A4
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
						this.state = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060022A9 RID: 8873 RVA: 0x000A3000 File Offset: 0x000A1200
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C0F RID: 7183
			private static int max_field_count = 1;

			// Token: 0x04001C10 RID: 7184
			private long _state;
		}
	}
}
