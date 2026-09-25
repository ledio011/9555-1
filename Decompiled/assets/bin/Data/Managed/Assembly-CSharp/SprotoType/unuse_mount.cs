using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000633 RID: 1587
	public class unuse_mount
	{
		// Token: 0x02000634 RID: 1588
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E23 RID: 11811 RVA: 0x000B9568 File Offset: 0x000B7768
			public request() : base(unuse_mount.request.max_field_count)
			{
			}

			// Token: 0x06002E24 RID: 11812 RVA: 0x000B9578 File Offset: 0x000B7778
			public request(byte[] buffer) : base(unuse_mount.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002E26 RID: 11814 RVA: 0x000B9590 File Offset: 0x000B7790
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002E27 RID: 11815 RVA: 0x000B95CC File Offset: 0x000B77CC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001F17 RID: 7959
			private static int max_field_count;
		}
	}
}
