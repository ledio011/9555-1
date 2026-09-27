using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000624 RID: 1572
	public class tower_reset
	{
		// Token: 0x02000625 RID: 1573
		public class request : SprotoTypeBase
		{
			// Token: 0x06002DD9 RID: 11737 RVA: 0x000B8CD0 File Offset: 0x000B6ED0
			public request() : base(tower_reset.request.max_field_count)
			{
			}

			// Token: 0x06002DDA RID: 11738 RVA: 0x000B8CE0 File Offset: 0x000B6EE0
			public request(byte[] buffer) : base(tower_reset.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002DDC RID: 11740 RVA: 0x000B8CF8 File Offset: 0x000B6EF8
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002DDD RID: 11741 RVA: 0x000B8D34 File Offset: 0x000B6F34
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001F06 RID: 7942
			private static int max_field_count;
		}
	}
}
