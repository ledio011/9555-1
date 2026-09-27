using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000441 RID: 1089
	public class pause_participate_dance
	{
		// Token: 0x02000442 RID: 1090
		public class request : SprotoTypeBase
		{
			// Token: 0x060021F7 RID: 8695 RVA: 0x000A1A90 File Offset: 0x0009FC90
			public request() : base(pause_participate_dance.request.max_field_count)
			{
			}

			// Token: 0x060021F8 RID: 8696 RVA: 0x000A1AA0 File Offset: 0x0009FCA0
			public request(byte[] buffer) : base(pause_participate_dance.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x060021FA RID: 8698 RVA: 0x000A1AB8 File Offset: 0x0009FCB8
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060021FB RID: 8699 RVA: 0x000A1AF4 File Offset: 0x0009FCF4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001BE4 RID: 7140
			private static int max_field_count;
		}
	}
}
