using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200065D RID: 1629
	public class use_mount
	{
		// Token: 0x0200065E RID: 1630
		public class request : SprotoTypeBase
		{
			// Token: 0x06002F0E RID: 12046 RVA: 0x000BB188 File Offset: 0x000B9388
			public request() : base(use_mount.request.max_field_count)
			{
			}

			// Token: 0x06002F0F RID: 12047 RVA: 0x000BB198 File Offset: 0x000B9398
			public request(byte[] buffer) : base(use_mount.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002F11 RID: 12049 RVA: 0x000BB1B0 File Offset: 0x000B93B0
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002F12 RID: 12050 RVA: 0x000BB1EC File Offset: 0x000B93EC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001F50 RID: 8016
			private static int max_field_count;
		}
	}
}
