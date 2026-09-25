using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004A2 RID: 1186
	public class request_dance_info
	{
		// Token: 0x020004A3 RID: 1187
		public class request : SprotoTypeBase
		{
			// Token: 0x060023D4 RID: 9172 RVA: 0x000A50CC File Offset: 0x000A32CC
			public request() : base(request_dance_info.request.max_field_count)
			{
			}

			// Token: 0x060023D5 RID: 9173 RVA: 0x000A50DC File Offset: 0x000A32DC
			public request(byte[] buffer) : base(request_dance_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A0F RID: 2575
			// (get) Token: 0x060023D7 RID: 9175 RVA: 0x000A50F8 File Offset: 0x000A32F8
			// (set) Token: 0x060023D8 RID: 9176 RVA: 0x000A5100 File Offset: 0x000A3300
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

			// Token: 0x17000A10 RID: 2576
			// (get) Token: 0x060023D9 RID: 9177 RVA: 0x000A5118 File Offset: 0x000A3318
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060023DA RID: 9178 RVA: 0x000A5128 File Offset: 0x000A3328
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

			// Token: 0x060023DB RID: 9179 RVA: 0x000A5184 File Offset: 0x000A3384
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C52 RID: 7250
			private static int max_field_count = 1;

			// Token: 0x04001C53 RID: 7251
			private long _type;
		}
	}
}
