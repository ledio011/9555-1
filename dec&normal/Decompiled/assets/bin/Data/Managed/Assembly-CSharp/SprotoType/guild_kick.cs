using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003E3 RID: 995
	public class guild_kick
	{
		// Token: 0x020003E4 RID: 996
		public class request : SprotoTypeBase
		{
			// Token: 0x06001EC0 RID: 7872 RVA: 0x0009B110 File Offset: 0x00099310
			public request() : base(guild_kick.request.max_field_count)
			{
			}

			// Token: 0x06001EC1 RID: 7873 RVA: 0x0009B120 File Offset: 0x00099320
			public request(byte[] buffer) : base(guild_kick.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000845 RID: 2117
			// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x0009B13C File Offset: 0x0009933C
			// (set) Token: 0x06001EC4 RID: 7876 RVA: 0x0009B144 File Offset: 0x00099344
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

			// Token: 0x17000846 RID: 2118
			// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x0009B15C File Offset: 0x0009935C
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001EC6 RID: 7878 RVA: 0x0009B16C File Offset: 0x0009936C
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

			// Token: 0x06001EC7 RID: 7879 RVA: 0x0009B1C8 File Offset: 0x000993C8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B02 RID: 6914
			private static int max_field_count = 1;

			// Token: 0x04001B03 RID: 6915
			private long _characterId;
		}
	}
}
