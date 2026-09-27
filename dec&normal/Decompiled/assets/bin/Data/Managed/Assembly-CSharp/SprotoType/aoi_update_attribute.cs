using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002F4 RID: 756
	public class aoi_update_attribute
	{
		// Token: 0x020002F5 RID: 757
		public class request : SprotoTypeBase
		{
			// Token: 0x06001536 RID: 5430 RVA: 0x000872C0 File Offset: 0x000854C0
			public request() : base(aoi_update_attribute.request.max_field_count)
			{
			}

			// Token: 0x06001537 RID: 5431 RVA: 0x000872D0 File Offset: 0x000854D0
			public request(byte[] buffer) : base(aoi_update_attribute.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000415 RID: 1045
			// (get) Token: 0x06001539 RID: 5433 RVA: 0x000872EC File Offset: 0x000854EC
			// (set) Token: 0x0600153A RID: 5434 RVA: 0x000872F4 File Offset: 0x000854F4
			public character_aoi_attribute character
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

			// Token: 0x17000416 RID: 1046
			// (get) Token: 0x0600153B RID: 5435 RVA: 0x0008730C File Offset: 0x0008550C
			public bool HasCharacter
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600153C RID: 5436 RVA: 0x0008731C File Offset: 0x0008551C
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
						this.character = this.deserialize.read_obj<character_aoi_attribute>();
					}
				}
			}

			// Token: 0x0600153D RID: 5437 RVA: 0x00087378 File Offset: 0x00085578
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.character, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x0400184A RID: 6218
			private static int max_field_count = 1;

			// Token: 0x0400184B RID: 6219
			private character_aoi_attribute _character;
		}
	}
}
