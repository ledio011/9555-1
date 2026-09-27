using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200060D RID: 1549
	public class sync_mission
	{
		// Token: 0x0200060E RID: 1550
		public class request : SprotoTypeBase
		{
			// Token: 0x06002CDC RID: 11484 RVA: 0x000B6BA0 File Offset: 0x000B4DA0
			public request() : base(sync_mission.request.max_field_count)
			{
			}

			// Token: 0x06002CDD RID: 11485 RVA: 0x000B6BB0 File Offset: 0x000B4DB0
			public request(byte[] buffer) : base(sync_mission.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D11 RID: 3345
			// (get) Token: 0x06002CDF RID: 11487 RVA: 0x000B6BCC File Offset: 0x000B4DCC
			// (set) Token: 0x06002CE0 RID: 11488 RVA: 0x000B6BD4 File Offset: 0x000B4DD4
			public Dictionary<string, ownmission> missions
			{
				get
				{
					return this._missions;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._missions = value;
				}
			}

			// Token: 0x17000D12 RID: 3346
			// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x000B6BEC File Offset: 0x000B4DEC
			public bool HasMissions
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000D13 RID: 3347
			// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x000B6BFC File Offset: 0x000B4DFC
			// (set) Token: 0x06002CE3 RID: 11491 RVA: 0x000B6C04 File Offset: 0x000B4E04
			public string last_missionId
			{
				get
				{
					return this._last_missionId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._last_missionId = value;
				}
			}

			// Token: 0x17000D14 RID: 3348
			// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x000B6C1C File Offset: 0x000B4E1C
			public bool HasLast_missionId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000D15 RID: 3349
			// (get) Token: 0x06002CE5 RID: 11493 RVA: 0x000B6C2C File Offset: 0x000B4E2C
			// (set) Token: 0x06002CE6 RID: 11494 RVA: 0x000B6C34 File Offset: 0x000B4E34
			public List<long> sidedone_mission
			{
				get
				{
					return this._sidedone_mission;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._sidedone_mission = value;
				}
			}

			// Token: 0x17000D16 RID: 3350
			// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x000B6C4C File Offset: 0x000B4E4C
			public bool HasSidedone_mission
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002CE8 RID: 11496 RVA: 0x000B6C5C File Offset: 0x000B4E5C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.missions = this.deserialize.read_map<string, ownmission>((ownmission v) => v.missionId);
						break;
					case 1:
						this.last_missionId = this.deserialize.read_string();
						break;
					case 2:
						this.sidedone_mission = this.deserialize.read_integer_list();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002CE9 RID: 11497 RVA: 0x000B6D0C File Offset: 0x000B4F0C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, ownmission>(this.missions, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.last_missionId, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.sidedone_mission, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001EBC RID: 7868
			private static int max_field_count = 3;

			// Token: 0x04001EBD RID: 7869
			private Dictionary<string, ownmission> _missions;

			// Token: 0x04001EBE RID: 7870
			private string _last_missionId;

			// Token: 0x04001EBF RID: 7871
			private List<long> _sidedone_mission;
		}
	}
}
