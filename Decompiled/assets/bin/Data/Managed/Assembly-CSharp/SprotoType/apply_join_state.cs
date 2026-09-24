using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002FA RID: 762
	public class apply_join_state
	{
		// Token: 0x020002FB RID: 763
		public class request : SprotoTypeBase
		{
			// Token: 0x06001554 RID: 5460 RVA: 0x00087648 File Offset: 0x00085848
			public request() : base(apply_join_state.request.max_field_count)
			{
			}

			// Token: 0x06001555 RID: 5461 RVA: 0x00087658 File Offset: 0x00085858
			public request(byte[] buffer) : base(apply_join_state.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700041D RID: 1053
			// (get) Token: 0x06001557 RID: 5463 RVA: 0x00087674 File Offset: 0x00085874
			// (set) Token: 0x06001558 RID: 5464 RVA: 0x0008767C File Offset: 0x0008587C
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

			// Token: 0x1700041E RID: 1054
			// (get) Token: 0x06001559 RID: 5465 RVA: 0x00087694 File Offset: 0x00085894
			public bool HasTeamid
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700041F RID: 1055
			// (get) Token: 0x0600155A RID: 5466 RVA: 0x000876A4 File Offset: 0x000858A4
			// (set) Token: 0x0600155B RID: 5467 RVA: 0x000876AC File Offset: 0x000858AC
			public long isAgree
			{
				get
				{
					return this._isAgree;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._isAgree = value;
				}
			}

			// Token: 0x17000420 RID: 1056
			// (get) Token: 0x0600155C RID: 5468 RVA: 0x000876C4 File Offset: 0x000858C4
			public bool HasIsAgree
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600155D RID: 5469 RVA: 0x000876D4 File Offset: 0x000858D4
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
							this.isAgree = this.deserialize.read_integer();
						}
					}
					else
					{
						this.teamid = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600155E RID: 5470 RVA: 0x0008774C File Offset: 0x0008594C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.teamid, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.isAgree, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001851 RID: 6225
			private static int max_field_count = 2;

			// Token: 0x04001852 RID: 6226
			private long _teamid;

			// Token: 0x04001853 RID: 6227
			private long _isAgree;
		}
	}
}
