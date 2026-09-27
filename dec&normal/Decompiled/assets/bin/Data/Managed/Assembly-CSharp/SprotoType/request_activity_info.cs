using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000492 RID: 1170
	public class request_activity_info
	{
		// Token: 0x02000493 RID: 1171
		public class request : SprotoTypeBase
		{
			// Token: 0x0600239E RID: 9118 RVA: 0x000A4B8C File Offset: 0x000A2D8C
			public request() : base(request_activity_info.request.max_field_count)
			{
			}

			// Token: 0x0600239F RID: 9119 RVA: 0x000A4B9C File Offset: 0x000A2D9C
			public request(byte[] buffer) : base(request_activity_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A0B RID: 2571
			// (get) Token: 0x060023A1 RID: 9121 RVA: 0x000A4BB8 File Offset: 0x000A2DB8
			// (set) Token: 0x060023A2 RID: 9122 RVA: 0x000A4BC0 File Offset: 0x000A2DC0
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._type = value;
				}
			}

			// Token: 0x17000A0C RID: 2572
			// (get) Token: 0x060023A3 RID: 9123 RVA: 0x000A4BD8 File Offset: 0x000A2DD8
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060023A4 RID: 9124 RVA: 0x000A4BE8 File Offset: 0x000A2DE8
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
						this.type = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060023A5 RID: 9125 RVA: 0x000A4C44 File Offset: 0x000A2E44
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C48 RID: 7240
			private static int max_field_count = 1;

			// Token: 0x04001C49 RID: 7241
			private long _type;
		}
	}
}
