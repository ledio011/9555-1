using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005CE RID: 1486
	public class set_mission_state
	{
		// Token: 0x020005CF RID: 1487
		public class request : SprotoTypeBase
		{
			// Token: 0x06002AD6 RID: 10966 RVA: 0x000B29D0 File Offset: 0x000B0BD0
			public request() : base(set_mission_state.request.max_field_count)
			{
			}

			// Token: 0x06002AD7 RID: 10967 RVA: 0x000B29E0 File Offset: 0x000B0BE0
			public request(byte[] buffer) : base(set_mission_state.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C45 RID: 3141
			// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x000B29FC File Offset: 0x000B0BFC
			// (set) Token: 0x06002ADA RID: 10970 RVA: 0x000B2A04 File Offset: 0x000B0C04
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

			// Token: 0x17000C46 RID: 3142
			// (get) Token: 0x06002ADB RID: 10971 RVA: 0x000B2A1C File Offset: 0x000B0C1C
			public bool HasMissionId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C47 RID: 3143
			// (get) Token: 0x06002ADC RID: 10972 RVA: 0x000B2A2C File Offset: 0x000B0C2C
			// (set) Token: 0x06002ADD RID: 10973 RVA: 0x000B2A34 File Offset: 0x000B0C34
			public long missionstate
			{
				get
				{
					return this._missionstate;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._missionstate = value;
				}
			}

			// Token: 0x17000C48 RID: 3144
			// (get) Token: 0x06002ADE RID: 10974 RVA: 0x000B2A4C File Offset: 0x000B0C4C
			public bool HasMissionstate
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002ADF RID: 10975 RVA: 0x000B2A5C File Offset: 0x000B0C5C
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
							this.missionstate = this.deserialize.read_integer();
						}
					}
					else
					{
						this.missionId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002AE0 RID: 10976 RVA: 0x000B2AD4 File Offset: 0x000B0CD4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.missionId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.missionstate, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E2A RID: 7722
			private static int max_field_count = 2;

			// Token: 0x04001E2B RID: 7723
			private string _missionId;

			// Token: 0x04001E2C RID: 7724
			private long _missionstate;
		}
	}
}
