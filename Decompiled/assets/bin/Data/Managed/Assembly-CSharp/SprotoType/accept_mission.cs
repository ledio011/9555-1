using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002E4 RID: 740
	public class accept_mission
	{
		// Token: 0x020002E5 RID: 741
		public class request : SprotoTypeBase
		{
			// Token: 0x060014AE RID: 5294 RVA: 0x00086158 File Offset: 0x00084358
			public request() : base(accept_mission.request.max_field_count)
			{
			}

			// Token: 0x060014AF RID: 5295 RVA: 0x00086168 File Offset: 0x00084368
			public request(byte[] buffer) : base(accept_mission.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170003DD RID: 989
			// (get) Token: 0x060014B1 RID: 5297 RVA: 0x00086184 File Offset: 0x00084384
			// (set) Token: 0x060014B2 RID: 5298 RVA: 0x0008618C File Offset: 0x0008438C
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

			// Token: 0x170003DE RID: 990
			// (get) Token: 0x060014B3 RID: 5299 RVA: 0x000861A4 File Offset: 0x000843A4
			public bool HasMissionId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170003DF RID: 991
			// (get) Token: 0x060014B4 RID: 5300 RVA: 0x000861B4 File Offset: 0x000843B4
			// (set) Token: 0x060014B5 RID: 5301 RVA: 0x000861BC File Offset: 0x000843BC
			public string onlineId
			{
				get
				{
					return this._onlineId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._onlineId = value;
				}
			}

			// Token: 0x170003E0 RID: 992
			// (get) Token: 0x060014B6 RID: 5302 RVA: 0x000861D4 File Offset: 0x000843D4
			public bool HasOnlineId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060014B7 RID: 5303 RVA: 0x000861E4 File Offset: 0x000843E4
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
							this.onlineId = this.deserialize.read_string();
						}
					}
					else
					{
						this.missionId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x060014B8 RID: 5304 RVA: 0x0008625C File Offset: 0x0008445C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.missionId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.onlineId, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001825 RID: 6181
			private static int max_field_count = 2;

			// Token: 0x04001826 RID: 6182
			private string _missionId;

			// Token: 0x04001827 RID: 6183
			private string _onlineId;
		}
	}
}
