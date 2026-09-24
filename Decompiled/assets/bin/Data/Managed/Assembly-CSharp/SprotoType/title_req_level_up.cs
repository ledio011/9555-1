using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000620 RID: 1568
	public class title_req_level_up
	{
		// Token: 0x02000621 RID: 1569
		public class request : SprotoTypeBase
		{
			// Token: 0x06002DAE RID: 11694 RVA: 0x000B8744 File Offset: 0x000B6944
			public request() : base(title_req_level_up.request.max_field_count)
			{
			}

			// Token: 0x06002DAF RID: 11695 RVA: 0x000B8754 File Offset: 0x000B6954
			public request(byte[] buffer) : base(title_req_level_up.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002DB1 RID: 11697 RVA: 0x000B876C File Offset: 0x000B696C
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002DB2 RID: 11698 RVA: 0x000B87A8 File Offset: 0x000B69A8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001EFA RID: 7930
			private static int max_field_count;
		}
	}
}
