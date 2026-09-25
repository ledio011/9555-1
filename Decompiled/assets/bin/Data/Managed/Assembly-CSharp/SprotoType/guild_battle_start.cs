using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003D2 RID: 978
	public class guild_battle_start
	{
		// Token: 0x020003D3 RID: 979
		public class request : SprotoTypeBase
		{
			// Token: 0x06001E06 RID: 7686 RVA: 0x000998AC File Offset: 0x00097AAC
			public request() : base(guild_battle_start.request.max_field_count)
			{
			}

			// Token: 0x06001E07 RID: 7687 RVA: 0x000998BC File Offset: 0x00097ABC
			public request(byte[] buffer) : base(guild_battle_start.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06001E09 RID: 7689 RVA: 0x000998D4 File Offset: 0x00097AD4
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06001E0A RID: 7690 RVA: 0x00099910 File Offset: 0x00097B10
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001ACD RID: 6861
			private static int max_field_count;
		}
	}
}
