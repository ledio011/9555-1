using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005F5 RID: 1525
	public class stop_random_select_team
	{
		// Token: 0x020005F6 RID: 1526
		public class request : SprotoTypeBase
		{
			// Token: 0x06002C24 RID: 11300 RVA: 0x000B5454 File Offset: 0x000B3654
			public request() : base(stop_random_select_team.request.max_field_count)
			{
			}

			// Token: 0x06002C25 RID: 11301 RVA: 0x000B5464 File Offset: 0x000B3664
			public request(byte[] buffer) : base(stop_random_select_team.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000CCB RID: 3275
			// (get) Token: 0x06002C27 RID: 11303 RVA: 0x000B5480 File Offset: 0x000B3680
			// (set) Token: 0x06002C28 RID: 11304 RVA: 0x000B5488 File Offset: 0x000B3688
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x17000CCC RID: 3276
			// (get) Token: 0x06002C29 RID: 11305 RVA: 0x000B54A0 File Offset: 0x000B36A0
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000CCD RID: 3277
			// (get) Token: 0x06002C2A RID: 11306 RVA: 0x000B54B0 File Offset: 0x000B36B0
			// (set) Token: 0x06002C2B RID: 11307 RVA: 0x000B54B8 File Offset: 0x000B36B8
			public long type1
			{
				get
				{
					return this._type1;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._type1 = value;
				}
			}

			// Token: 0x17000CCE RID: 3278
			// (get) Token: 0x06002C2C RID: 11308 RVA: 0x000B54D0 File Offset: 0x000B36D0
			public bool HasType1
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002C2D RID: 11309 RVA: 0x000B54E0 File Offset: 0x000B36E0
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
							this.type1 = this.deserialize.read_integer();
						}
					}
					else
					{
						this.id = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002C2E RID: 11310 RVA: 0x000B5558 File Offset: 0x000B3758
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.type1, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E86 RID: 7814
			private static int max_field_count = 2;

			// Token: 0x04001E87 RID: 7815
			private string _id;

			// Token: 0x04001E88 RID: 7816
			private long _type1;
		}
	}
}
