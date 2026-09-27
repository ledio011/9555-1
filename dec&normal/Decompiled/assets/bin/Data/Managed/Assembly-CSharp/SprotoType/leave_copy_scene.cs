using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003FF RID: 1023
	public class leave_copy_scene
	{
		// Token: 0x02000400 RID: 1024
		public class request : SprotoTypeBase
		{
			// Token: 0x06001FBA RID: 8122 RVA: 0x0009D12C File Offset: 0x0009B32C
			public request() : base(leave_copy_scene.request.max_field_count)
			{
			}

			// Token: 0x06001FBB RID: 8123 RVA: 0x0009D13C File Offset: 0x0009B33C
			public request(byte[] buffer) : base(leave_copy_scene.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06001FBD RID: 8125 RVA: 0x0009D154 File Offset: 0x0009B354
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06001FBE RID: 8126 RVA: 0x0009D190 File Offset: 0x0009B390
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001B46 RID: 6982
			private static int max_field_count;
		}
	}
}
