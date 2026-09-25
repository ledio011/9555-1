using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200064F RID: 1615
	public class update_team
	{
		// Token: 0x02000650 RID: 1616
		public class request : SprotoTypeBase
		{
			// Token: 0x06002EC3 RID: 11971 RVA: 0x000BA8A0 File Offset: 0x000B8AA0
			public request() : base(update_team.request.max_field_count)
			{
			}

			// Token: 0x06002EC4 RID: 11972 RVA: 0x000BA8B0 File Offset: 0x000B8AB0
			public request(byte[] buffer) : base(update_team.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DC5 RID: 3525
			// (get) Token: 0x06002EC6 RID: 11974 RVA: 0x000BA8CC File Offset: 0x000B8ACC
			// (set) Token: 0x06002EC7 RID: 11975 RVA: 0x000BA8D4 File Offset: 0x000B8AD4
			public team team
			{
				get
				{
					return this._team;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._team = value;
				}
			}

			// Token: 0x17000DC6 RID: 3526
			// (get) Token: 0x06002EC8 RID: 11976 RVA: 0x000BA8EC File Offset: 0x000B8AEC
			public bool HasTeam
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002EC9 RID: 11977 RVA: 0x000BA8FC File Offset: 0x000B8AFC
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
						this.team = this.deserialize.read_obj<team>();
					}
				}
			}

			// Token: 0x06002ECA RID: 11978 RVA: 0x000BA958 File Offset: 0x000B8B58
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.team, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F3E RID: 7998
			private static int max_field_count = 1;

			// Token: 0x04001F3F RID: 7999
			private team _team;
		}
	}
}
