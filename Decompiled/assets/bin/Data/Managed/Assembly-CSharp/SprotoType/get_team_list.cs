using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003C0 RID: 960
	public class get_team_list
	{
		// Token: 0x020003C1 RID: 961
		public class request : SprotoTypeBase
		{
			// Token: 0x06001D46 RID: 7494 RVA: 0x00097F90 File Offset: 0x00096190
			public request() : base(get_team_list.request.max_field_count)
			{
			}

			// Token: 0x06001D47 RID: 7495 RVA: 0x00097FA0 File Offset: 0x000961A0
			public request(byte[] buffer) : base(get_team_list.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000799 RID: 1945
			// (get) Token: 0x06001D49 RID: 7497 RVA: 0x00097FBC File Offset: 0x000961BC
			// (set) Token: 0x06001D4A RID: 7498 RVA: 0x00097FC4 File Offset: 0x000961C4
			public string goalId
			{
				get
				{
					return this._goalId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._goalId = value;
				}
			}

			// Token: 0x1700079A RID: 1946
			// (get) Token: 0x06001D4B RID: 7499 RVA: 0x00097FDC File Offset: 0x000961DC
			public bool HasGoalId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001D4C RID: 7500 RVA: 0x00097FEC File Offset: 0x000961EC
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
						this.goalId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06001D4D RID: 7501 RVA: 0x00098048 File Offset: 0x00096248
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.goalId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A96 RID: 6806
			private static int max_field_count = 1;

			// Token: 0x04001A97 RID: 6807
			private string _goalId;
		}
	}
}
