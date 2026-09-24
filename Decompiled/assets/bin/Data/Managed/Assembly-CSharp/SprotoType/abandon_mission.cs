using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002E0 RID: 736
	public class abandon_mission
	{
		// Token: 0x020002E1 RID: 737
		public class request : SprotoTypeBase
		{
			// Token: 0x06001499 RID: 5273 RVA: 0x00085ED8 File Offset: 0x000840D8
			public request() : base(abandon_mission.request.max_field_count)
			{
			}

			// Token: 0x0600149A RID: 5274 RVA: 0x00085EE8 File Offset: 0x000840E8
			public request(byte[] buffer) : base(abandon_mission.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170003D7 RID: 983
			// (get) Token: 0x0600149C RID: 5276 RVA: 0x00085F04 File Offset: 0x00084104
			// (set) Token: 0x0600149D RID: 5277 RVA: 0x00085F0C File Offset: 0x0008410C
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

			// Token: 0x170003D8 RID: 984
			// (get) Token: 0x0600149E RID: 5278 RVA: 0x00085F24 File Offset: 0x00084124
			public bool HasMissionId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170003D9 RID: 985
			// (get) Token: 0x0600149F RID: 5279 RVA: 0x00085F34 File Offset: 0x00084134
			// (set) Token: 0x060014A0 RID: 5280 RVA: 0x00085F3C File Offset: 0x0008413C
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

			// Token: 0x170003DA RID: 986
			// (get) Token: 0x060014A1 RID: 5281 RVA: 0x00085F54 File Offset: 0x00084154
			public bool HasParm
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060014A2 RID: 5282 RVA: 0x00085F64 File Offset: 0x00084164
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

			// Token: 0x060014A3 RID: 5283 RVA: 0x00085FDC File Offset: 0x000841DC
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

			// Token: 0x04001820 RID: 6176
			private static int max_field_count = 2;

			// Token: 0x04001821 RID: 6177
			private string _missionId;

			// Token: 0x04001822 RID: 6178
			private long _parm;
		}
	}
}
