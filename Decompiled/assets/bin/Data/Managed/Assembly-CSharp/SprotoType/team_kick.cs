using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000619 RID: 1561
	public class team_kick
	{
		// Token: 0x0200061A RID: 1562
		public class request : SprotoTypeBase
		{
			// Token: 0x06002D49 RID: 11593 RVA: 0x000B79B4 File Offset: 0x000B5BB4
			public request() : base(team_kick.request.max_field_count)
			{
			}

			// Token: 0x06002D4A RID: 11594 RVA: 0x000B79C4 File Offset: 0x000B5BC4
			public request(byte[] buffer) : base(team_kick.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D3D RID: 3389
			// (get) Token: 0x06002D4C RID: 11596 RVA: 0x000B79E0 File Offset: 0x000B5BE0
			// (set) Token: 0x06002D4D RID: 11597 RVA: 0x000B79E8 File Offset: 0x000B5BE8
			public long characterId
			{
				get
				{
					return this._characterId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._characterId = value;
				}
			}

			// Token: 0x17000D3E RID: 3390
			// (get) Token: 0x06002D4E RID: 11598 RVA: 0x000B7A00 File Offset: 0x000B5C00
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002D4F RID: 11599 RVA: 0x000B7A10 File Offset: 0x000B5C10
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.characterId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002D50 RID: 11600 RVA: 0x000B7A6C File Offset: 0x000B5C6C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001EDC RID: 7900
			private static int max_field_count = 1;

			// Token: 0x04001EDD RID: 7901
			private long _characterId;
		}
	}
}
