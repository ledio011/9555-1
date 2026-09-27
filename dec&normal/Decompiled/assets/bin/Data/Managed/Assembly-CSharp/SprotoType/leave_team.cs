using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000403 RID: 1027
	public class leave_team
	{
		// Token: 0x02000404 RID: 1028
		public class request : SprotoTypeBase
		{
			// Token: 0x06001FC6 RID: 8134 RVA: 0x0009D23C File Offset: 0x0009B43C
			public request() : base(leave_team.request.max_field_count)
			{
			}

			// Token: 0x06001FC7 RID: 8135 RVA: 0x0009D24C File Offset: 0x0009B44C
			public request(byte[] buffer) : base(leave_team.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170008A9 RID: 2217
			// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x0009D268 File Offset: 0x0009B468
			// (set) Token: 0x06001FCA RID: 8138 RVA: 0x0009D270 File Offset: 0x0009B470
			public long teamid
			{
				get
				{
					return this._teamid;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._teamid = value;
				}
			}

			// Token: 0x170008AA RID: 2218
			// (get) Token: 0x06001FCB RID: 8139 RVA: 0x0009D288 File Offset: 0x0009B488
			public bool HasTeamid
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170008AB RID: 2219
			// (get) Token: 0x06001FCC RID: 8140 RVA: 0x0009D298 File Offset: 0x0009B498
			// (set) Token: 0x06001FCD RID: 8141 RVA: 0x0009D2A0 File Offset: 0x0009B4A0
			public long characterId
			{
				get
				{
					return this._characterId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._characterId = value;
				}
			}

			// Token: 0x170008AC RID: 2220
			// (get) Token: 0x06001FCE RID: 8142 RVA: 0x0009D2B8 File Offset: 0x0009B4B8
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001FCF RID: 8143 RVA: 0x0009D2C8 File Offset: 0x0009B4C8
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
							this.characterId = this.deserialize.read_integer();
						}
					}
					else
					{
						this.teamid = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001FD0 RID: 8144 RVA: 0x0009D340 File Offset: 0x0009B540
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.teamid, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.characterId, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B48 RID: 6984
			private static int max_field_count = 2;

			// Token: 0x04001B49 RID: 6985
			private long _teamid;

			// Token: 0x04001B4A RID: 6986
			private long _characterId;
		}
	}
}
