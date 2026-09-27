using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004DF RID: 1247
	public class request_update_storagepack
	{
		// Token: 0x020004E0 RID: 1248
		public class request : SprotoTypeBase
		{
			// Token: 0x060024B7 RID: 9399 RVA: 0x000A681C File Offset: 0x000A4A1C
			public request() : base(request_update_storagepack.request.max_field_count)
			{
			}

			// Token: 0x060024B8 RID: 9400 RVA: 0x000A682C File Offset: 0x000A4A2C
			public request(byte[] buffer) : base(request_update_storagepack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060024BA RID: 9402 RVA: 0x000A6844 File Offset: 0x000A4A44
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060024BB RID: 9403 RVA: 0x000A6880 File Offset: 0x000A4A80
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C7F RID: 7295
			private static int max_field_count;
		}
	}
}
