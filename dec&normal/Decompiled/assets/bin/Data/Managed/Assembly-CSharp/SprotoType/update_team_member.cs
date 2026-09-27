using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000651 RID: 1617
	public class update_team_member
	{
		// Token: 0x02000652 RID: 1618
		public class request : SprotoTypeBase
		{
			// Token: 0x06002ECC RID: 11980 RVA: 0x000BA9A8 File Offset: 0x000B8BA8
			public request() : base(update_team_member.request.max_field_count)
			{
			}

			// Token: 0x06002ECD RID: 11981 RVA: 0x000BA9B8 File Offset: 0x000B8BB8
			public request(byte[] buffer) : base(update_team_member.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DC7 RID: 3527
			// (get) Token: 0x06002ECF RID: 11983 RVA: 0x000BA9D4 File Offset: 0x000B8BD4
			// (set) Token: 0x06002ED0 RID: 11984 RVA: 0x000BA9DC File Offset: 0x000B8BDC
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

			// Token: 0x17000DC8 RID: 3528
			// (get) Token: 0x06002ED1 RID: 11985 RVA: 0x000BA9F4 File Offset: 0x000B8BF4
			public bool HasTeamid
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000DC9 RID: 3529
			// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x000BAA04 File Offset: 0x000B8C04
			// (set) Token: 0x06002ED3 RID: 11987 RVA: 0x000BAA0C File Offset: 0x000B8C0C
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

			// Token: 0x17000DCA RID: 3530
			// (get) Token: 0x06002ED4 RID: 11988 RVA: 0x000BAA24 File Offset: 0x000B8C24
			public bool HasMember
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002ED5 RID: 11989 RVA: 0x000BAA34 File Offset: 0x000B8C34
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
							this.member = this.deserialize.read_obj<teammember>();
						}
					}
					else
					{
						this.teamid = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002ED6 RID: 11990 RVA: 0x000BAAAC File Offset: 0x000B8CAC
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
				return this.serialize.close();
			}

			// Token: 0x04001F40 RID: 8000
			private static int max_field_count = 2;

			// Token: 0x04001F41 RID: 8001
			private long _teamid;

			// Token: 0x04001F42 RID: 8002
			private teammember _member;
		}
	}
}
