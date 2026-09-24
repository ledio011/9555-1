using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000354 RID: 852
	public class complete_mission
	{
		// Token: 0x02000355 RID: 853
		public class request : SprotoTypeBase
		{
			// Token: 0x06001948 RID: 6472 RVA: 0x0008FBA4 File Offset: 0x0008DDA4
			public request() : base(complete_mission.request.max_field_count)
			{
			}

			// Token: 0x06001949 RID: 6473 RVA: 0x0008FBB4 File Offset: 0x0008DDB4
			public request(byte[] buffer) : base(complete_mission.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x0600194B RID: 6475 RVA: 0x0008FBD0 File Offset: 0x0008DDD0
			// (set) Token: 0x0600194C RID: 6476 RVA: 0x0008FBD8 File Offset: 0x0008DDD8
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

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x0600194D RID: 6477 RVA: 0x0008FBF0 File Offset: 0x0008DDF0
			public bool HasMissionId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x0600194E RID: 6478 RVA: 0x0008FC00 File Offset: 0x0008DE00
			// (set) Token: 0x0600194F RID: 6479 RVA: 0x0008FC08 File Offset: 0x0008DE08
			public long parm
			{
				get
				{
					return this._parm;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._parm = value;
				}
			}

			// Token: 0x170005EA RID: 1514
			// (get) Token: 0x06001950 RID: 6480 RVA: 0x0008FC20 File Offset: 0x0008DE20
			public bool HasParm
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001951 RID: 6481 RVA: 0x0008FC30 File Offset: 0x0008DE30
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
							this.parm = this.deserialize.read_integer();
						}
					}
					else
					{
						this.missionId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06001952 RID: 6482 RVA: 0x0008FCA8 File Offset: 0x0008DEA8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.missionId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.parm, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001979 RID: 6521
			private static int max_field_count = 2;

			// Token: 0x0400197A RID: 6522
			private string _missionId;

			// Token: 0x0400197B RID: 6523
			private long _parm;
		}
	}
}
