using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004A4 RID: 1188
	public class request_dance_state_info
	{
		// Token: 0x020004A5 RID: 1189
		public class request : SprotoTypeBase
		{
			// Token: 0x060023DD RID: 9181 RVA: 0x000A51D4 File Offset: 0x000A33D4
			public request() : base(request_dance_state_info.request.max_field_count)
			{
			}

			// Token: 0x060023DE RID: 9182 RVA: 0x000A51E4 File Offset: 0x000A33E4
			public request(byte[] buffer) : base(request_dance_state_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A11 RID: 2577
			// (get) Token: 0x060023E0 RID: 9184 RVA: 0x000A5200 File Offset: 0x000A3400
			// (set) Token: 0x060023E1 RID: 9185 RVA: 0x000A5208 File Offset: 0x000A3408
			public long detail
			{
				get
				{
					return this._detail;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._detail = value;
				}
			}

			// Token: 0x17000A12 RID: 2578
			// (get) Token: 0x060023E2 RID: 9186 RVA: 0x000A5220 File Offset: 0x000A3420
			public bool HasDetail
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060023E3 RID: 9187 RVA: 0x000A5230 File Offset: 0x000A3430
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
						this.detail = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060023E4 RID: 9188 RVA: 0x000A528C File Offset: 0x000A348C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.detail, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C54 RID: 7252
			private static int max_field_count = 1;

			// Token: 0x04001C55 RID: 7253
			private long _detail;
		}
	}
}
