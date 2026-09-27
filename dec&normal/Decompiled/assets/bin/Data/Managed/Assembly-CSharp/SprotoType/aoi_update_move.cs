using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002F6 RID: 758
	public class aoi_update_move
	{
		// Token: 0x020002F7 RID: 759
		public class request : SprotoTypeBase
		{
			// Token: 0x0600153F RID: 5439 RVA: 0x000873C8 File Offset: 0x000855C8
			public request() : base(aoi_update_move.request.max_field_count)
			{
			}

			// Token: 0x06001540 RID: 5440 RVA: 0x000873D8 File Offset: 0x000855D8
			public request(byte[] buffer) : base(aoi_update_move.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000417 RID: 1047
			// (get) Token: 0x06001542 RID: 5442 RVA: 0x000873F4 File Offset: 0x000855F4
			// (set) Token: 0x06001543 RID: 5443 RVA: 0x000873FC File Offset: 0x000855FC
			public character_aoi_move character
			{
				get
				{
					return this._character;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._character = value;
				}
			}

			// Token: 0x17000418 RID: 1048
			// (get) Token: 0x06001544 RID: 5444 RVA: 0x00087414 File Offset: 0x00085614
			public bool HasCharacter
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001545 RID: 5445 RVA: 0x00087424 File Offset: 0x00085624
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
						this.character = this.deserialize.read_obj<character_aoi_move>();
					}
				}
			}

			// Token: 0x06001546 RID: 5446 RVA: 0x00087480 File Offset: 0x00085680
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.character, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x0400184C RID: 6220
			private static int max_field_count = 1;

			// Token: 0x0400184D RID: 6221
			private character_aoi_move _character;
		}
	}
}
