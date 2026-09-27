using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000464 RID: 1124
	public class refresh_online_misison
	{
		// Token: 0x02000465 RID: 1125
		public class request : SprotoTypeBase
		{
			// Token: 0x060022C3 RID: 8899 RVA: 0x000A32D8 File Offset: 0x000A14D8
			public request() : base(refresh_online_misison.request.max_field_count)
			{
			}

			// Token: 0x060022C4 RID: 8900 RVA: 0x000A32E8 File Offset: 0x000A14E8
			public request(byte[] buffer) : base(refresh_online_misison.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060022C6 RID: 8902 RVA: 0x000A3300 File Offset: 0x000A1500
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060022C7 RID: 8903 RVA: 0x000A333C File Offset: 0x000A153C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001C16 RID: 7190
			private static int max_field_count;
		}
	}
}
