using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005ED RID: 1517
	public class start_download
	{
		// Token: 0x020005EE RID: 1518
		public class request : SprotoTypeBase
		{
			// Token: 0x06002C02 RID: 11266 RVA: 0x000B50A0 File Offset: 0x000B32A0
			public request() : base(start_download.request.max_field_count)
			{
			}

			// Token: 0x06002C03 RID: 11267 RVA: 0x000B50B0 File Offset: 0x000B32B0
			public request(byte[] buffer) : base(start_download.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002C05 RID: 11269 RVA: 0x000B50C8 File Offset: 0x000B32C8
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002C06 RID: 11270 RVA: 0x000B5104 File Offset: 0x000B3304
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001E7E RID: 7806
			private static int max_field_count;
		}
	}
}
