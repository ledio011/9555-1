using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000376 RID: 886
	public class download_finish
	{
		// Token: 0x02000377 RID: 887
		public class request : SprotoTypeBase
		{
			// Token: 0x06001AD4 RID: 6868 RVA: 0x00092FB4 File Offset: 0x000911B4
			public request() : base(download_finish.request.max_field_count)
			{
			}

			// Token: 0x06001AD5 RID: 6869 RVA: 0x00092FC4 File Offset: 0x000911C4
			public request(byte[] buffer) : base(download_finish.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06001AD7 RID: 6871 RVA: 0x00092FDC File Offset: 0x000911DC
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06001AD8 RID: 6872 RVA: 0x00093018 File Offset: 0x00091218
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x040019EA RID: 6634
			private static int max_field_count;
		}
	}
}
