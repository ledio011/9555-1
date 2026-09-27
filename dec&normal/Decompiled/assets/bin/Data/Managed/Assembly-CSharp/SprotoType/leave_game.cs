using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000401 RID: 1025
	public class leave_game
	{
		// Token: 0x02000402 RID: 1026
		public class request : SprotoTypeBase
		{
			// Token: 0x06001FC0 RID: 8128 RVA: 0x0009D1B4 File Offset: 0x0009B3B4
			public request() : base(leave_game.request.max_field_count)
			{
			}

			// Token: 0x06001FC1 RID: 8129 RVA: 0x0009D1C4 File Offset: 0x0009B3C4
			public request(byte[] buffer) : base(leave_game.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06001FC3 RID: 8131 RVA: 0x0009D1DC File Offset: 0x0009B3DC
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06001FC4 RID: 8132 RVA: 0x0009D218 File Offset: 0x0009B418
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001B47 RID: 6983
			private static int max_field_count;
		}
	}
}
