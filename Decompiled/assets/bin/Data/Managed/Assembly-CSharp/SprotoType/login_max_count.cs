using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200040E RID: 1038
	public class login_max_count
	{
		// Token: 0x0200040F RID: 1039
		public class request : SprotoTypeBase
		{
			// Token: 0x06002035 RID: 8245 RVA: 0x0009E0B0 File Offset: 0x0009C2B0
			public request() : base(login_max_count.request.max_field_count)
			{
			}

			// Token: 0x06002036 RID: 8246 RVA: 0x0009E0C0 File Offset: 0x0009C2C0
			public request(byte[] buffer) : base(login_max_count.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002038 RID: 8248 RVA: 0x0009E0D8 File Offset: 0x0009C2D8
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002039 RID: 8249 RVA: 0x0009E114 File Offset: 0x0009C314
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001B67 RID: 7015
			private static int max_field_count;
		}
	}
}
