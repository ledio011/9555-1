using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005D1 RID: 1489
	public class show_damage_board
	{
		// Token: 0x020005D2 RID: 1490
		public class request : SprotoTypeBase
		{
			// Token: 0x06002B02 RID: 11010 RVA: 0x000B2FBC File Offset: 0x000B11BC
			public request() : base(show_damage_board.request.max_field_count)
			{
			}

			// Token: 0x06002B03 RID: 11011 RVA: 0x000B2FCC File Offset: 0x000B11CC
			public request(byte[] buffer) : base(show_damage_board.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C5B RID: 3163
			// (get) Token: 0x06002B05 RID: 11013 RVA: 0x000B2FE8 File Offset: 0x000B11E8
			// (set) Token: 0x06002B06 RID: 11014 RVA: 0x000B2FF0 File Offset: 0x000B11F0
			public List<acceptdamge> damges
			{
				get
				{
					return this._damges;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._damges = value;
				}
			}

			// Token: 0x17000C5C RID: 3164
			// (get) Token: 0x06002B07 RID: 11015 RVA: 0x000B3008 File Offset: 0x000B1208
			public bool HasDamges
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002B08 RID: 11016 RVA: 0x000B3018 File Offset: 0x000B1218
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
						this.damges = this.deserialize.read_obj_list<acceptdamge>();
					}
				}
			}

			// Token: 0x06002B09 RID: 11017 RVA: 0x000B3074 File Offset: 0x000B1274
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<acceptdamge>(this.damges, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E37 RID: 7735
			private static int max_field_count = 1;

			// Token: 0x04001E38 RID: 7736
			private List<acceptdamge> _damges;
		}
	}
}
