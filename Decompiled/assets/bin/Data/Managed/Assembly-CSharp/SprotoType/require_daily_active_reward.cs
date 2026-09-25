using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004E3 RID: 1251
	public class require_daily_active_reward
	{
		// Token: 0x020004E4 RID: 1252
		public class request : SprotoTypeBase
		{
			// Token: 0x060024C3 RID: 9411 RVA: 0x000A692C File Offset: 0x000A4B2C
			public request() : base(require_daily_active_reward.request.max_field_count)
			{
			}

			// Token: 0x060024C4 RID: 9412 RVA: 0x000A693C File Offset: 0x000A4B3C
			public request(byte[] buffer) : base(require_daily_active_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A2B RID: 2603
			// (get) Token: 0x060024C6 RID: 9414 RVA: 0x000A6958 File Offset: 0x000A4B58
			// (set) Token: 0x060024C7 RID: 9415 RVA: 0x000A6960 File Offset: 0x000A4B60
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

			// Token: 0x17000A2C RID: 2604
			// (get) Token: 0x060024C8 RID: 9416 RVA: 0x000A6978 File Offset: 0x000A4B78
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060024C9 RID: 9417 RVA: 0x000A6988 File Offset: 0x000A4B88
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

			// Token: 0x060024CA RID: 9418 RVA: 0x000A69E4 File Offset: 0x000A4BE4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C81 RID: 7297
			private static int max_field_count = 1;

			// Token: 0x04001C82 RID: 7298
			private string _ID;
		}
	}
}
