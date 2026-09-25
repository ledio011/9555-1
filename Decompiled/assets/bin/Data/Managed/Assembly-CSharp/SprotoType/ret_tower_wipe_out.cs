using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005A7 RID: 1447
	public class ret_tower_wipe_out
	{
		// Token: 0x020005A8 RID: 1448
		public class request : SprotoTypeBase
		{
			// Token: 0x060029D0 RID: 10704 RVA: 0x000B0934 File Offset: 0x000AEB34
			public request() : base(ret_tower_wipe_out.request.max_field_count)
			{
			}

			// Token: 0x060029D1 RID: 10705 RVA: 0x000B0944 File Offset: 0x000AEB44
			public request(byte[] buffer) : base(ret_tower_wipe_out.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BE9 RID: 3049
			// (get) Token: 0x060029D3 RID: 10707 RVA: 0x000B0960 File Offset: 0x000AEB60
			// (set) Token: 0x060029D4 RID: 10708 RVA: 0x000B0968 File Offset: 0x000AEB68
			public tower_info tower_info
			{
				get
				{
					return this._tower_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._tower_info = value;
				}
			}

			// Token: 0x17000BEA RID: 3050
			// (get) Token: 0x060029D5 RID: 10709 RVA: 0x000B0980 File Offset: 0x000AEB80
			public bool HasTower_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060029D6 RID: 10710 RVA: 0x000B0990 File Offset: 0x000AEB90
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
						this.tower_info = this.deserialize.read_obj<tower_info>();
					}
				}
			}

			// Token: 0x060029D7 RID: 10711 RVA: 0x000B09EC File Offset: 0x000AEBEC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.tower_info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DE6 RID: 7654
			private static int max_field_count = 1;

			// Token: 0x04001DE7 RID: 7655
			private tower_info _tower_info;
		}
	}
}
