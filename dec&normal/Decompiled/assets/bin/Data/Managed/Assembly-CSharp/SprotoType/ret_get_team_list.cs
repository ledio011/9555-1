using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200051B RID: 1307
	public class ret_get_team_list
	{
		// Token: 0x0200051C RID: 1308
		public class request : SprotoTypeBase
		{
			// Token: 0x06002636 RID: 9782 RVA: 0x000A9700 File Offset: 0x000A7900
			public request() : base(ret_get_team_list.request.max_field_count)
			{
			}

			// Token: 0x06002637 RID: 9783 RVA: 0x000A9710 File Offset: 0x000A7910
			public request(byte[] buffer) : base(ret_get_team_list.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AAF RID: 2735
			// (get) Token: 0x06002639 RID: 9785 RVA: 0x000A972C File Offset: 0x000A792C
			// (set) Token: 0x0600263A RID: 9786 RVA: 0x000A9734 File Offset: 0x000A7934
			public Dictionary<long, team> teams
			{
				get
				{
					return this._teams;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._teams = value;
				}
			}

			// Token: 0x17000AB0 RID: 2736
			// (get) Token: 0x0600263B RID: 9787 RVA: 0x000A974C File Offset: 0x000A794C
			public bool HasTeams
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600263C RID: 9788 RVA: 0x000A975C File Offset: 0x000A795C
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
						this.teams = this.deserialize.read_map<long, team>((team v) => v.id);
					}
				}
			}

			// Token: 0x0600263D RID: 9789 RVA: 0x000A97D4 File Offset: 0x000A79D4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<long, team>(this.teams, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CE4 RID: 7396
			private static int max_field_count = 1;

			// Token: 0x04001CE5 RID: 7397
			private Dictionary<long, team> _teams;
		}
	}
}
