using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005BC RID: 1468
	public class select_pk_character
	{
		// Token: 0x020005BD RID: 1469
		public class request : SprotoTypeBase
		{
			// Token: 0x06002A64 RID: 10852 RVA: 0x000B1BB8 File Offset: 0x000AFDB8
			public request() : base(select_pk_character.request.max_field_count)
			{
			}

			// Token: 0x06002A65 RID: 10853 RVA: 0x000B1BC8 File Offset: 0x000AFDC8
			public request(byte[] buffer) : base(select_pk_character.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C1D RID: 3101
			// (get) Token: 0x06002A67 RID: 10855 RVA: 0x000B1BE4 File Offset: 0x000AFDE4
			// (set) Token: 0x06002A68 RID: 10856 RVA: 0x000B1BEC File Offset: 0x000AFDEC
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

			// Token: 0x17000C1E RID: 3102
			// (get) Token: 0x06002A69 RID: 10857 RVA: 0x000B1C04 File Offset: 0x000AFE04
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002A6A RID: 10858 RVA: 0x000B1C14 File Offset: 0x000AFE14
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

			// Token: 0x06002A6B RID: 10859 RVA: 0x000B1C70 File Offset: 0x000AFE70
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E0D RID: 7693
			private static int max_field_count = 1;

			// Token: 0x04001E0E RID: 7694
			private long _characterId;
		}
	}
}
