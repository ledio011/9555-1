using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005D3 RID: 1491
	public class show_player_damage_board
	{
		// Token: 0x020005D4 RID: 1492
		public class request : SprotoTypeBase
		{
			// Token: 0x06002B0B RID: 11019 RVA: 0x000B30C4 File Offset: 0x000B12C4
			public request() : base(show_player_damage_board.request.max_field_count)
			{
			}

			// Token: 0x06002B0C RID: 11020 RVA: 0x000B30D4 File Offset: 0x000B12D4
			public request(byte[] buffer) : base(show_player_damage_board.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C5D RID: 3165
			// (get) Token: 0x06002B0E RID: 11022 RVA: 0x000B30F0 File Offset: 0x000B12F0
			// (set) Token: 0x06002B0F RID: 11023 RVA: 0x000B30F8 File Offset: 0x000B12F8
			public long id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x17000C5E RID: 3166
			// (get) Token: 0x06002B10 RID: 11024 RVA: 0x000B3110 File Offset: 0x000B1310
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C5F RID: 3167
			// (get) Token: 0x06002B11 RID: 11025 RVA: 0x000B3120 File Offset: 0x000B1320
			// (set) Token: 0x06002B12 RID: 11026 RVA: 0x000B3128 File Offset: 0x000B1328
			public long hp
			{
				get
				{
					return this._hp;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._hp = value;
				}
			}

			// Token: 0x17000C60 RID: 3168
			// (get) Token: 0x06002B13 RID: 11027 RVA: 0x000B3140 File Offset: 0x000B1340
			public bool HasHp
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002B14 RID: 11028 RVA: 0x000B3150 File Offset: 0x000B1350
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 != 1)
						{
							this.deserialize.read_unknow_data();
						}
						else
						{
							this.hp = this.deserialize.read_integer();
						}
					}
					else
					{
						this.id = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002B15 RID: 11029 RVA: 0x000B31C8 File Offset: 0x000B13C8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.hp, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E39 RID: 7737
			private static int max_field_count = 2;

			// Token: 0x04001E3A RID: 7738
			private long _id;

			// Token: 0x04001E3B RID: 7739
			private long _hp;
		}
	}
}
