using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200059D RID: 1437
	public class ret_special_big_pack
	{
		// Token: 0x0200059E RID: 1438
		public class request : SprotoTypeBase
		{
			// Token: 0x06002998 RID: 10648 RVA: 0x000B0274 File Offset: 0x000AE474
			public request() : base(ret_special_big_pack.request.max_field_count)
			{
			}

			// Token: 0x06002999 RID: 10649 RVA: 0x000B0284 File Offset: 0x000AE484
			public request(byte[] buffer) : base(ret_special_big_pack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BD9 RID: 3033
			// (get) Token: 0x0600299B RID: 10651 RVA: 0x000B02A0 File Offset: 0x000AE4A0
			// (set) Token: 0x0600299C RID: 10652 RVA: 0x000B02A8 File Offset: 0x000AE4A8
			public Dictionary<string, special_big_pack> special_big_packs
			{
				get
				{
					return this._special_big_packs;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._special_big_packs = value;
				}
			}

			// Token: 0x17000BDA RID: 3034
			// (get) Token: 0x0600299D RID: 10653 RVA: 0x000B02C0 File Offset: 0x000AE4C0
			public bool HasSpecial_big_packs
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600299E RID: 10654 RVA: 0x000B02D0 File Offset: 0x000AE4D0
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
						this.special_big_packs = this.deserialize.read_map<string, special_big_pack>((special_big_pack v) => v.ID);
					}
				}
			}

			// Token: 0x0600299F RID: 10655 RVA: 0x000B0348 File Offset: 0x000AE548
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, special_big_pack>(this.special_big_packs, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DD7 RID: 7639
			private static int max_field_count = 1;

			// Token: 0x04001DD8 RID: 7640
			private Dictionary<string, special_big_pack> _special_big_packs;
		}
	}
}
