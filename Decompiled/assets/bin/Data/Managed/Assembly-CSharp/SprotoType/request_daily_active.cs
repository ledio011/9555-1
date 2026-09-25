using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200049C RID: 1180
	public class request_daily_active
	{
		// Token: 0x0200049D RID: 1181
		public class request : SprotoTypeBase
		{
			// Token: 0x060023C2 RID: 9154 RVA: 0x000A4F34 File Offset: 0x000A3134
			public request() : base(request_daily_active.request.max_field_count)
			{
			}

			// Token: 0x060023C3 RID: 9155 RVA: 0x000A4F44 File Offset: 0x000A3144
			public request(byte[] buffer) : base(request_daily_active.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060023C5 RID: 9157 RVA: 0x000A4F5C File Offset: 0x000A315C
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060023C6 RID: 9158 RVA: 0x000A4F98 File Offset: 0x000A3198
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C4F RID: 7247
			private static int max_field_count;
		}
	}
}
