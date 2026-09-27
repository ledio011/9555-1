using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005F3 RID: 1523
	public class stop_leave_copy
	{
		// Token: 0x020005F4 RID: 1524
		public class request : SprotoTypeBase
		{
			// Token: 0x06002C1E RID: 11294 RVA: 0x000B53CC File Offset: 0x000B35CC
			public request() : base(stop_leave_copy.request.max_field_count)
			{
			}

			// Token: 0x06002C1F RID: 11295 RVA: 0x000B53DC File Offset: 0x000B35DC
			public request(byte[] buffer) : base(stop_leave_copy.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002C21 RID: 11297 RVA: 0x000B53F4 File Offset: 0x000B35F4
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002C22 RID: 11298 RVA: 0x000B5430 File Offset: 0x000B3630
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001E85 RID: 7813
			private static int max_field_count;
		}
	}
}
