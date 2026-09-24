using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200043A RID: 1082
	public class open_guild_boss
	{
		// Token: 0x0200043B RID: 1083
		public class request : SprotoTypeBase
		{
			// Token: 0x060021C2 RID: 8642 RVA: 0x000A13E0 File Offset: 0x0009F5E0
			public request() : base(open_guild_boss.request.max_field_count)
			{
			}

			// Token: 0x060021C3 RID: 8643 RVA: 0x000A13F0 File Offset: 0x0009F5F0
			public request(byte[] buffer) : base(open_guild_boss.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000983 RID: 2435
			// (get) Token: 0x060021C5 RID: 8645 RVA: 0x000A140C File Offset: 0x0009F60C
			// (set) Token: 0x060021C6 RID: 8646 RVA: 0x000A1414 File Offset: 0x0009F614
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

			// Token: 0x17000984 RID: 2436
			// (get) Token: 0x060021C7 RID: 8647 RVA: 0x000A142C File Offset: 0x0009F62C
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060021C8 RID: 8648 RVA: 0x000A143C File Offset: 0x0009F63C
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
						this.id = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x060021C9 RID: 8649 RVA: 0x000A1498 File Offset: 0x0009F698
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001BD6 RID: 7126
			private static int max_field_count = 1;

			// Token: 0x04001BD7 RID: 7127
			private string _id;
		}
	}
}
