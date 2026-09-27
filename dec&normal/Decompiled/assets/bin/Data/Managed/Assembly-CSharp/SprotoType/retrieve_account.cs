using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005AF RID: 1455
	public class retrieve_account
	{
		// Token: 0x020005B0 RID: 1456
		public class request : SprotoTypeBase
		{
			// Token: 0x06002A01 RID: 10753 RVA: 0x000B0F30 File Offset: 0x000AF130
			public request() : base(retrieve_account.request.max_field_count)
			{
			}

			// Token: 0x06002A02 RID: 10754 RVA: 0x000B0F40 File Offset: 0x000AF140
			public request(byte[] buffer) : base(retrieve_account.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BF9 RID: 3065
			// (get) Token: 0x06002A04 RID: 10756 RVA: 0x000B0F5C File Offset: 0x000AF15C
			// (set) Token: 0x06002A05 RID: 10757 RVA: 0x000B0F64 File Offset: 0x000AF164
			public long id
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

			// Token: 0x17000BFA RID: 3066
			// (get) Token: 0x06002A06 RID: 10758 RVA: 0x000B0F7C File Offset: 0x000AF17C
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002A07 RID: 10759 RVA: 0x000B0F8C File Offset: 0x000AF18C
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
						this.id = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002A08 RID: 10760 RVA: 0x000B0FE8 File Offset: 0x000AF1E8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DF3 RID: 7667
			private static int max_field_count = 1;

			// Token: 0x04001DF4 RID: 7668
			private long _id;
		}
	}
}
