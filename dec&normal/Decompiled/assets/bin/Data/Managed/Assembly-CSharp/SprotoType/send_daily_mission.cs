using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005C0 RID: 1472
	public class send_daily_mission
	{
		// Token: 0x020005C1 RID: 1473
		public class request : SprotoTypeBase
		{
			// Token: 0x06002A7C RID: 10876 RVA: 0x000B1EA8 File Offset: 0x000B00A8
			public request() : base(send_daily_mission.request.max_field_count)
			{
			}

			// Token: 0x06002A7D RID: 10877 RVA: 0x000B1EB8 File Offset: 0x000B00B8
			public request(byte[] buffer) : base(send_daily_mission.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C25 RID: 3109
			// (get) Token: 0x06002A7F RID: 10879 RVA: 0x000B1ED4 File Offset: 0x000B00D4
			// (set) Token: 0x06002A80 RID: 10880 RVA: 0x000B1EDC File Offset: 0x000B00DC
			public ownmission mission
			{
				get
				{
					return this._mission;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._mission = value;
				}
			}

			// Token: 0x17000C26 RID: 3110
			// (get) Token: 0x06002A81 RID: 10881 RVA: 0x000B1EF4 File Offset: 0x000B00F4
			public bool HasMission
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002A82 RID: 10882 RVA: 0x000B1F04 File Offset: 0x000B0104
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
						this.mission = this.deserialize.read_obj<ownmission>();
					}
				}
			}

			// Token: 0x06002A83 RID: 10883 RVA: 0x000B1F60 File Offset: 0x000B0160
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.mission, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E13 RID: 7699
			private static int max_field_count = 1;

			// Token: 0x04001E14 RID: 7700
			private ownmission _mission;
		}
	}
}
