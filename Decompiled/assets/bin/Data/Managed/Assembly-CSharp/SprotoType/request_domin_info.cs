using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004A6 RID: 1190
	public class request_domin_info
	{
		// Token: 0x020004A7 RID: 1191
		public class request : SprotoTypeBase
		{
			// Token: 0x060023E6 RID: 9190 RVA: 0x000A52DC File Offset: 0x000A34DC
			public request() : base(request_domin_info.request.max_field_count)
			{
			}

			// Token: 0x060023E7 RID: 9191 RVA: 0x000A52EC File Offset: 0x000A34EC
			public request(byte[] buffer) : base(request_domin_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060023E9 RID: 9193 RVA: 0x000A5304 File Offset: 0x000A3504
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060023EA RID: 9194 RVA: 0x000A5340 File Offset: 0x000A3540
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C56 RID: 7254
			private static int max_field_count;
		}
	}
}
