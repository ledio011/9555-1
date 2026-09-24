using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002F2 RID: 754
	public class aoi_stop_move
	{
		// Token: 0x020002F3 RID: 755
		public class request : SprotoTypeBase
		{
			// Token: 0x0600152D RID: 5421 RVA: 0x000871B8 File Offset: 0x000853B8
			public request() : base(aoi_stop_move.request.max_field_count)
			{
			}

			// Token: 0x0600152E RID: 5422 RVA: 0x000871C8 File Offset: 0x000853C8
			public request(byte[] buffer) : base(aoi_stop_move.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000413 RID: 1043
			// (get) Token: 0x06001530 RID: 5424 RVA: 0x000871E4 File Offset: 0x000853E4
			// (set) Token: 0x06001531 RID: 5425 RVA: 0x000871EC File Offset: 0x000853EC
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

			// Token: 0x17000414 RID: 1044
			// (get) Token: 0x06001532 RID: 5426 RVA: 0x00087204 File Offset: 0x00085404
			public bool HasCharacter
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001533 RID: 5427 RVA: 0x00087214 File Offset: 0x00085414
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

			// Token: 0x06001534 RID: 5428 RVA: 0x00087270 File Offset: 0x00085470
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.character, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001848 RID: 6216
			private static int max_field_count = 1;

			// Token: 0x04001849 RID: 6217
			private character_aoi_move _character;
		}
	}
}
