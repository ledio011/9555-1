using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000484 RID: 1156
	public class req_join_team
	{
		// Token: 0x02000485 RID: 1157
		public class request : SprotoTypeBase
		{
			// Token: 0x06002362 RID: 9058 RVA: 0x000A44F4 File Offset: 0x000A26F4
			public request() : base(req_join_team.request.max_field_count)
			{
			}

			// Token: 0x06002363 RID: 9059 RVA: 0x000A4504 File Offset: 0x000A2704
			public request(byte[] buffer) : base(req_join_team.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009FF RID: 2559
			// (get) Token: 0x06002365 RID: 9061 RVA: 0x000A4520 File Offset: 0x000A2720
			// (set) Token: 0x06002366 RID: 9062 RVA: 0x000A4528 File Offset: 0x000A2728
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

			// Token: 0x17000A00 RID: 2560
			// (get) Token: 0x06002367 RID: 9063 RVA: 0x000A4540 File Offset: 0x000A2740
			public bool HasTeamid
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A01 RID: 2561
			// (get) Token: 0x06002368 RID: 9064 RVA: 0x000A4550 File Offset: 0x000A2750
			// (set) Token: 0x06002369 RID: 9065 RVA: 0x000A4558 File Offset: 0x000A2758
			public bool isapply
			{
				get
				{
					return this._isapply;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._isapply = value;
				}
			}

			// Token: 0x17000A02 RID: 2562
			// (get) Token: 0x0600236A RID: 9066 RVA: 0x000A4570 File Offset: 0x000A2770
			public bool HasIsapply
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600236B RID: 9067 RVA: 0x000A4580 File Offset: 0x000A2780
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
							this.isapply = this.deserialize.read_boolean();
						}
					}
					else
					{
						this.teamid = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600236C RID: 9068 RVA: 0x000A45F8 File Offset: 0x000A27F8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.teamid, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_boolean(this.isapply, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C3B RID: 7227
			private static int max_field_count = 2;

			// Token: 0x04001C3C RID: 7228
			private long _teamid;

			// Token: 0x04001C3D RID: 7229
			private bool _isapply;
		}
	}
}
