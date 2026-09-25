using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002EC RID: 748
	public class aoi_relife_player
	{
		// Token: 0x020002ED RID: 749
		public class request : SprotoTypeBase
		{
			// Token: 0x0600150F RID: 5391 RVA: 0x00086E30 File Offset: 0x00085030
			public request() : base(aoi_relife_player.request.max_field_count)
			{
			}

			// Token: 0x06001510 RID: 5392 RVA: 0x00086E40 File Offset: 0x00085040
			public request(byte[] buffer) : base(aoi_relife_player.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700040B RID: 1035
			// (get) Token: 0x06001512 RID: 5394 RVA: 0x00086E5C File Offset: 0x0008505C
			// (set) Token: 0x06001513 RID: 5395 RVA: 0x00086E64 File Offset: 0x00085064
			public character_relife character
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

			// Token: 0x1700040C RID: 1036
			// (get) Token: 0x06001514 RID: 5396 RVA: 0x00086E7C File Offset: 0x0008507C
			public bool HasCharacter
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001515 RID: 5397 RVA: 0x00086E8C File Offset: 0x0008508C
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
						this.character = this.deserialize.read_obj<character_relife>();
					}
				}
			}

			// Token: 0x06001516 RID: 5398 RVA: 0x00086EE8 File Offset: 0x000850E8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.character, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001841 RID: 6209
			private static int max_field_count = 1;

			// Token: 0x04001842 RID: 6210
			private character_relife _character;
		}
	}
}
