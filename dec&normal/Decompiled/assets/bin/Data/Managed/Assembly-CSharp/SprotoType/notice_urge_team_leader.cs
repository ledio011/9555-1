using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000431 RID: 1073
	public class notice_urge_team_leader
	{
		// Token: 0x02000432 RID: 1074
		public class request : SprotoTypeBase
		{
			// Token: 0x06002136 RID: 8502 RVA: 0x000A00EC File Offset: 0x0009E2EC
			public request() : base(notice_urge_team_leader.request.max_field_count)
			{
			}

			// Token: 0x06002137 RID: 8503 RVA: 0x000A00FC File Offset: 0x0009E2FC
			public request(byte[] buffer) : base(notice_urge_team_leader.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000939 RID: 2361
			// (get) Token: 0x06002139 RID: 8505 RVA: 0x000A0118 File Offset: 0x0009E318
			// (set) Token: 0x0600213A RID: 8506 RVA: 0x000A0120 File Offset: 0x0009E320
			public string name
			{
				get
				{
					return this._name;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._name = value;
				}
			}

			// Token: 0x1700093A RID: 2362
			// (get) Token: 0x0600213B RID: 8507 RVA: 0x000A0138 File Offset: 0x0009E338
			public bool HasName
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700093B RID: 2363
			// (get) Token: 0x0600213C RID: 8508 RVA: 0x000A0148 File Offset: 0x0009E348
			// (set) Token: 0x0600213D RID: 8509 RVA: 0x000A0150 File Offset: 0x0009E350
			public long id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._id = value;
				}
			}

			// Token: 0x1700093C RID: 2364
			// (get) Token: 0x0600213E RID: 8510 RVA: 0x000A0168 File Offset: 0x0009E368
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600213F RID: 8511 RVA: 0x000A0178 File Offset: 0x0009E378
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
							this.id = this.deserialize.read_integer();
						}
					}
					else
					{
						this.name = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002140 RID: 8512 RVA: 0x000A01F0 File Offset: 0x0009E3F0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.name, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.id, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001BAC RID: 7084
			private static int max_field_count = 2;

			// Token: 0x04001BAD RID: 7085
			private string _name;

			// Token: 0x04001BAE RID: 7086
			private long _id;
		}
	}
}
