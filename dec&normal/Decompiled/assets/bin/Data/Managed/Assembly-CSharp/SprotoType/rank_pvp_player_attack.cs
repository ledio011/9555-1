using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000454 RID: 1108
	public class rank_pvp_player_attack
	{
		// Token: 0x02000455 RID: 1109
		public class request : SprotoTypeBase
		{
			// Token: 0x06002281 RID: 8833 RVA: 0x000A2BB8 File Offset: 0x000A0DB8
			public request() : base(rank_pvp_player_attack.request.max_field_count)
			{
			}

			// Token: 0x06002282 RID: 8834 RVA: 0x000A2BC8 File Offset: 0x000A0DC8
			public request(byte[] buffer) : base(rank_pvp_player_attack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009C9 RID: 2505
			// (get) Token: 0x06002284 RID: 8836 RVA: 0x000A2BE4 File Offset: 0x000A0DE4
			// (set) Token: 0x06002285 RID: 8837 RVA: 0x000A2BEC File Offset: 0x000A0DEC
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

			// Token: 0x170009CA RID: 2506
			// (get) Token: 0x06002286 RID: 8838 RVA: 0x000A2C04 File Offset: 0x000A0E04
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170009CB RID: 2507
			// (get) Token: 0x06002287 RID: 8839 RVA: 0x000A2C14 File Offset: 0x000A0E14
			// (set) Token: 0x06002288 RID: 8840 RVA: 0x000A2C1C File Offset: 0x000A0E1C
			public long damage
			{
				get
				{
					return this._damage;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._damage = value;
				}
			}

			// Token: 0x170009CC RID: 2508
			// (get) Token: 0x06002289 RID: 8841 RVA: 0x000A2C34 File Offset: 0x000A0E34
			public bool HasDamage
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600228A RID: 8842 RVA: 0x000A2C44 File Offset: 0x000A0E44
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
							this.damage = this.deserialize.read_integer();
						}
					}
					else
					{
						this.characterId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600228B RID: 8843 RVA: 0x000A2CBC File Offset: 0x000A0EBC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.damage, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C08 RID: 7176
			private static int max_field_count = 2;

			// Token: 0x04001C09 RID: 7177
			private long _characterId;

			// Token: 0x04001C0A RID: 7178
			private long _damage;
		}
	}
}
