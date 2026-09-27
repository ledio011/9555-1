using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200063E RID: 1598
	public class update_guild_star
	{
		// Token: 0x0200063F RID: 1599
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E55 RID: 11861 RVA: 0x000B9B00 File Offset: 0x000B7D00
			public request() : base(update_guild_star.request.max_field_count)
			{
			}

			// Token: 0x06002E56 RID: 11862 RVA: 0x000B9B10 File Offset: 0x000B7D10
			public request(byte[] buffer) : base(update_guild_star.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D9F RID: 3487
			// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000B9B2C File Offset: 0x000B7D2C
			// (set) Token: 0x06002E59 RID: 11865 RVA: 0x000B9B34 File Offset: 0x000B7D34
			public string ID
			{
				get
				{
					return this._ID;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._ID = value;
				}
			}

			// Token: 0x17000DA0 RID: 3488
			// (get) Token: 0x06002E5A RID: 11866 RVA: 0x000B9B4C File Offset: 0x000B7D4C
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002E5B RID: 11867 RVA: 0x000B9B5C File Offset: 0x000B7D5C
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
						this.ID = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002E5C RID: 11868 RVA: 0x000B9BB8 File Offset: 0x000B7DB8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F22 RID: 7970
			private static int max_field_count = 1;

			// Token: 0x04001F23 RID: 7971
			private string _ID;
		}
	}
}
