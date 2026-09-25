using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003FC RID: 1020
	public class invite_join_team
	{
		// Token: 0x020003FD RID: 1021
		public class request : SprotoTypeBase
		{
			// Token: 0x06001F97 RID: 8087 RVA: 0x0009CC88 File Offset: 0x0009AE88
			public request() : base(invite_join_team.request.max_field_count)
			{
			}

			// Token: 0x06001F98 RID: 8088 RVA: 0x0009CC98 File Offset: 0x0009AE98
			public request(byte[] buffer) : base(invite_join_team.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000899 RID: 2201
			// (get) Token: 0x06001F9A RID: 8090 RVA: 0x0009CCB4 File Offset: 0x0009AEB4
			// (set) Token: 0x06001F9B RID: 8091 RVA: 0x0009CCBC File Offset: 0x0009AEBC
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

			// Token: 0x1700089A RID: 2202
			// (get) Token: 0x06001F9C RID: 8092 RVA: 0x0009CCD4 File Offset: 0x0009AED4
			public bool HasTeamid
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700089B RID: 2203
			// (get) Token: 0x06001F9D RID: 8093 RVA: 0x0009CCE4 File Offset: 0x0009AEE4
			// (set) Token: 0x06001F9E RID: 8094 RVA: 0x0009CCEC File Offset: 0x0009AEEC
			public teammember member
			{
				get
				{
					return this._member;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._member = value;
				}
			}

			// Token: 0x1700089C RID: 2204
			// (get) Token: 0x06001F9F RID: 8095 RVA: 0x0009CD04 File Offset: 0x0009AF04
			public bool HasMember
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x1700089D RID: 2205
			// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x0009CD14 File Offset: 0x0009AF14
			// (set) Token: 0x06001FA1 RID: 8097 RVA: 0x0009CD1C File Offset: 0x0009AF1C
			public string goalId
			{
				get
				{
					return this._goalId;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._goalId = value;
				}
			}

			// Token: 0x1700089E RID: 2206
			// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x0009CD34 File Offset: 0x0009AF34
			public bool HasGoalId
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06001FA3 RID: 8099 RVA: 0x0009CD44 File Offset: 0x0009AF44
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.teamid = this.deserialize.read_integer();
						break;
					case 1:
						this.member = this.deserialize.read_obj<teammember>();
						break;
					case 2:
						this.goalId = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001FA4 RID: 8100 RVA: 0x0009CDD8 File Offset: 0x0009AFD8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.teamid, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj(this.member, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.goalId, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B3C RID: 6972
			private static int max_field_count = 3;

			// Token: 0x04001B3D RID: 6973
			private long _teamid;

			// Token: 0x04001B3E RID: 6974
			private teammember _member;

			// Token: 0x04001B3F RID: 6975
			private string _goalId;
		}
	}
}
