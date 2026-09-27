using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004F3 RID: 1267
	public class ret_accept_mission
	{
		// Token: 0x020004F4 RID: 1268
		public class request : SprotoTypeBase
		{
			// Token: 0x0600250E RID: 9486 RVA: 0x000A71BC File Offset: 0x000A53BC
			public request() : base(ret_accept_mission.request.max_field_count)
			{
			}

			// Token: 0x0600250F RID: 9487 RVA: 0x000A71CC File Offset: 0x000A53CC
			public request(byte[] buffer) : base(ret_accept_mission.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A3D RID: 2621
			// (get) Token: 0x06002511 RID: 9489 RVA: 0x000A71E8 File Offset: 0x000A53E8
			// (set) Token: 0x06002512 RID: 9490 RVA: 0x000A71F0 File Offset: 0x000A53F0
			public string missionId
			{
				get
				{
					return this._missionId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._missionId = value;
				}
			}

			// Token: 0x17000A3E RID: 2622
			// (get) Token: 0x06002513 RID: 9491 RVA: 0x000A7208 File Offset: 0x000A5408
			public bool HasMissionId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A3F RID: 2623
			// (get) Token: 0x06002514 RID: 9492 RVA: 0x000A7218 File Offset: 0x000A5418
			// (set) Token: 0x06002515 RID: 9493 RVA: 0x000A7220 File Offset: 0x000A5420
			public long missionquality
			{
				get
				{
					return this._missionquality;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._missionquality = value;
				}
			}

			// Token: 0x17000A40 RID: 2624
			// (get) Token: 0x06002516 RID: 9494 RVA: 0x000A7238 File Offset: 0x000A5438
			public bool HasMissionquality
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000A41 RID: 2625
			// (get) Token: 0x06002517 RID: 9495 RVA: 0x000A7248 File Offset: 0x000A5448
			// (set) Token: 0x06002518 RID: 9496 RVA: 0x000A7250 File Offset: 0x000A5450
			public long ret
			{
				get
				{
					return this._ret;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._ret = value;
				}
			}

			// Token: 0x17000A42 RID: 2626
			// (get) Token: 0x06002519 RID: 9497 RVA: 0x000A7268 File Offset: 0x000A5468
			public bool HasRet
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000A43 RID: 2627
			// (get) Token: 0x0600251A RID: 9498 RVA: 0x000A7278 File Offset: 0x000A5478
			// (set) Token: 0x0600251B RID: 9499 RVA: 0x000A7280 File Offset: 0x000A5480
			public ownmission mission
			{
				get
				{
					return this._mission;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._mission = value;
				}
			}

			// Token: 0x17000A44 RID: 2628
			// (get) Token: 0x0600251C RID: 9500 RVA: 0x000A7298 File Offset: 0x000A5498
			public bool HasMission
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x0600251D RID: 9501 RVA: 0x000A72A8 File Offset: 0x000A54A8
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.missionId = this.deserialize.read_string();
						break;
					case 1:
						this.missionquality = this.deserialize.read_integer();
						break;
					case 2:
						this.ret = this.deserialize.read_integer();
						break;
					case 3:
						this.mission = this.deserialize.read_obj<ownmission>();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x0600251E RID: 9502 RVA: 0x000A7354 File Offset: 0x000A5554
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.missionId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.missionquality, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.ret, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_obj(this.mission, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C92 RID: 7314
			private static int max_field_count = 4;

			// Token: 0x04001C93 RID: 7315
			private string _missionId;

			// Token: 0x04001C94 RID: 7316
			private long _missionquality;

			// Token: 0x04001C95 RID: 7317
			private long _ret;

			// Token: 0x04001C96 RID: 7318
			private ownmission _mission;
		}
	}
}
