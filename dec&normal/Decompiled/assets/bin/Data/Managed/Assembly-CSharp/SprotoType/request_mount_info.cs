using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004BA RID: 1210
	public class request_mount_info
	{
		// Token: 0x020004BB RID: 1211
		public class request : SprotoTypeBase
		{
			// Token: 0x0600242B RID: 9259 RVA: 0x000A59AC File Offset: 0x000A3BAC
			public request() : base(request_mount_info.request.max_field_count)
			{
			}

			// Token: 0x0600242C RID: 9260 RVA: 0x000A59BC File Offset: 0x000A3BBC
			public request(byte[] buffer) : base(request_mount_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x0600242E RID: 9262 RVA: 0x000A59D4 File Offset: 0x000A3BD4
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x0600242F RID: 9263 RVA: 0x000A5A10 File Offset: 0x000A3C10
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C63 RID: 7267
			private static int max_field_count;
		}
	}
}
