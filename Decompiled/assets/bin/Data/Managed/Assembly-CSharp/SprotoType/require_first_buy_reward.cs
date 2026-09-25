using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004E7 RID: 1255
	public class require_first_buy_reward
	{
		// Token: 0x020004E8 RID: 1256
		public class request : SprotoTypeBase
		{
			// Token: 0x060024DB RID: 9435 RVA: 0x000A6C1C File Offset: 0x000A4E1C
			public request() : base(require_first_buy_reward.request.max_field_count)
			{
			}

			// Token: 0x060024DC RID: 9436 RVA: 0x000A6C2C File Offset: 0x000A4E2C
			public request(byte[] buffer) : base(require_first_buy_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A33 RID: 2611
			// (get) Token: 0x060024DE RID: 9438 RVA: 0x000A6C48 File Offset: 0x000A4E48
			// (set) Token: 0x060024DF RID: 9439 RVA: 0x000A6C50 File Offset: 0x000A4E50
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

			// Token: 0x17000A34 RID: 2612
			// (get) Token: 0x060024E0 RID: 9440 RVA: 0x000A6C68 File Offset: 0x000A4E68
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060024E1 RID: 9441 RVA: 0x000A6C78 File Offset: 0x000A4E78
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

			// Token: 0x060024E2 RID: 9442 RVA: 0x000A6CD4 File Offset: 0x000A4ED4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C87 RID: 7303
			private static int max_field_count = 1;

			// Token: 0x04001C88 RID: 7304
			private string _ID;
		}
	}
}
