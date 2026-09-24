using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000407 RID: 1031
	public class local_character_attack
	{
		// Token: 0x02000408 RID: 1032
		public class request : SprotoTypeBase
		{
			// Token: 0x06001FE8 RID: 8168 RVA: 0x0009D694 File Offset: 0x0009B894
			public request() : base(local_character_attack.request.max_field_count)
			{
			}

			// Token: 0x06001FE9 RID: 8169 RVA: 0x0009D6A4 File Offset: 0x0009B8A4
			public request(byte[] buffer) : base(local_character_attack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170008B5 RID: 2229
			// (get) Token: 0x06001FEB RID: 8171 RVA: 0x0009D6C0 File Offset: 0x0009B8C0
			// (set) Token: 0x06001FEC RID: 8172 RVA: 0x0009D6C8 File Offset: 0x0009B8C8
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

			// Token: 0x170008B6 RID: 2230
			// (get) Token: 0x06001FED RID: 8173 RVA: 0x0009D6E0 File Offset: 0x0009B8E0
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170008B7 RID: 2231
			// (get) Token: 0x06001FEE RID: 8174 RVA: 0x0009D6F0 File Offset: 0x0009B8F0
			// (set) Token: 0x06001FEF RID: 8175 RVA: 0x0009D6F8 File Offset: 0x0009B8F8
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

			// Token: 0x170008B8 RID: 2232
			// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x0009D710 File Offset: 0x0009B910
			public bool HasDamage
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170008B9 RID: 2233
			// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x0009D720 File Offset: 0x0009B920
			// (set) Token: 0x06001FF2 RID: 8178 RVA: 0x0009D728 File Offset: 0x0009B928
			public string effinfoId
			{
				get
				{
					return this._effinfoId;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._effinfoId = value;
				}
			}

			// Token: 0x170008BA RID: 2234
			// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x0009D740 File Offset: 0x0009B940
			public bool HasEffinfoId
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06001FF4 RID: 8180 RVA: 0x0009D750 File Offset: 0x0009B950
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.characterId = this.deserialize.read_integer();
						break;
					case 1:
						this.damage = this.deserialize.read_integer();
						break;
					case 2:
						this.effinfoId = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001FF5 RID: 8181 RVA: 0x0009D7E4 File Offset: 0x0009B9E4
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
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.effinfoId, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B51 RID: 6993
			private static int max_field_count = 3;

			// Token: 0x04001B52 RID: 6994
			private long _characterId;

			// Token: 0x04001B53 RID: 6995
			private long _damage;

			// Token: 0x04001B54 RID: 6996
			private string _effinfoId;
		}
	}
}
