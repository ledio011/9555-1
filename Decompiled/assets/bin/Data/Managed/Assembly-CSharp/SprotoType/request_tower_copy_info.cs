using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004DB RID: 1243
	public class request_tower_copy_info
	{
		// Token: 0x020004DC RID: 1244
		public class request : SprotoTypeBase
		{
			// Token: 0x060024A5 RID: 9381 RVA: 0x000A661C File Offset: 0x000A481C
			public request() : base(request_tower_copy_info.request.max_field_count)
			{
			}

			// Token: 0x060024A6 RID: 9382 RVA: 0x000A662C File Offset: 0x000A482C
			public request(byte[] buffer) : base(request_tower_copy_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060024A8 RID: 9384 RVA: 0x000A6644 File Offset: 0x000A4844
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060024A9 RID: 9385 RVA: 0x000A6680 File Offset: 0x000A4880
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C7B RID: 7291
			private static int max_field_count;
		}
	}
}
