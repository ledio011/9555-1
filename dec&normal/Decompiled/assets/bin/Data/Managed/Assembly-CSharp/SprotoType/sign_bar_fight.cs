using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005D9 RID: 1497
	public class sign_bar_fight
	{
		// Token: 0x020005DA RID: 1498
		public class request : SprotoTypeBase
		{
			// Token: 0x06002B29 RID: 11049 RVA: 0x000B344C File Offset: 0x000B164C
			public request() : base(sign_bar_fight.request.max_field_count)
			{
			}

			// Token: 0x06002B2A RID: 11050 RVA: 0x000B345C File Offset: 0x000B165C
			public request(byte[] buffer) : base(sign_bar_fight.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C65 RID: 3173
			// (get) Token: 0x06002B2C RID: 11052 RVA: 0x000B3478 File Offset: 0x000B1678
			// (set) Token: 0x06002B2D RID: 11053 RVA: 0x000B3480 File Offset: 0x000B1680
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

			// Token: 0x17000C66 RID: 3174
			// (get) Token: 0x06002B2E RID: 11054 RVA: 0x000B3498 File Offset: 0x000B1698
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002B2F RID: 11055 RVA: 0x000B34A8 File Offset: 0x000B16A8
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

			// Token: 0x06002B30 RID: 11056 RVA: 0x000B3504 File Offset: 0x000B1704
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E40 RID: 7744
			private static int max_field_count = 1;

			// Token: 0x04001E41 RID: 7745
			private string _ID;
		}
	}
}
