using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003D8 RID: 984
	public class guild_donate
	{
		// Token: 0x020003D9 RID: 985
		public class request : SprotoTypeBase
		{
			// Token: 0x06001E43 RID: 7747 RVA: 0x0009A084 File Offset: 0x00098284
			public request() : base(guild_donate.request.max_field_count)
			{
			}

			// Token: 0x06001E44 RID: 7748 RVA: 0x0009A094 File Offset: 0x00098294
			public request(byte[] buffer) : base(guild_donate.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000809 RID: 2057
			// (get) Token: 0x06001E46 RID: 7750 RVA: 0x0009A0B0 File Offset: 0x000982B0
			// (set) Token: 0x06001E47 RID: 7751 RVA: 0x0009A0B8 File Offset: 0x000982B8
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

			// Token: 0x1700080A RID: 2058
			// (get) Token: 0x06001E48 RID: 7752 RVA: 0x0009A0D0 File Offset: 0x000982D0
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001E49 RID: 7753 RVA: 0x0009A0E0 File Offset: 0x000982E0
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

			// Token: 0x06001E4A RID: 7754 RVA: 0x0009A13C File Offset: 0x0009833C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001ADE RID: 6878
			private static int max_field_count = 1;

			// Token: 0x04001ADF RID: 6879
			private string _id;
		}
	}
}
