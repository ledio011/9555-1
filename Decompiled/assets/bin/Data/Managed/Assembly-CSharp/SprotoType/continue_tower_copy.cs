using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000361 RID: 865
	public class continue_tower_copy
	{
		// Token: 0x02000362 RID: 866
		public class request : SprotoTypeBase
		{
			// Token: 0x060019BC RID: 6588 RVA: 0x00090A7C File Offset: 0x0008EC7C
			public request() : base(continue_tower_copy.request.max_field_count)
			{
			}

			// Token: 0x060019BD RID: 6589 RVA: 0x00090A8C File Offset: 0x0008EC8C
			public request(byte[] buffer) : base(continue_tower_copy.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060019BF RID: 6591 RVA: 0x00090AA4 File Offset: 0x0008ECA4
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060019C0 RID: 6592 RVA: 0x00090AE0 File Offset: 0x0008ECE0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001999 RID: 6553
			private static int max_field_count;
		}
	}
}
