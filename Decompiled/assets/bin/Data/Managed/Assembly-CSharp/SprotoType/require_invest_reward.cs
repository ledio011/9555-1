using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004E9 RID: 1257
	public class require_invest_reward
	{
		// Token: 0x020004EA RID: 1258
		public class request : SprotoTypeBase
		{
			// Token: 0x060024E4 RID: 9444 RVA: 0x000A6D24 File Offset: 0x000A4F24
			public request() : base(require_invest_reward.request.max_field_count)
			{
			}

			// Token: 0x060024E5 RID: 9445 RVA: 0x000A6D34 File Offset: 0x000A4F34
			public request(byte[] buffer) : base(require_invest_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A35 RID: 2613
			// (get) Token: 0x060024E7 RID: 9447 RVA: 0x000A6D50 File Offset: 0x000A4F50
			// (set) Token: 0x060024E8 RID: 9448 RVA: 0x000A6D58 File Offset: 0x000A4F58
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

			// Token: 0x17000A36 RID: 2614
			// (get) Token: 0x060024E9 RID: 9449 RVA: 0x000A6D70 File Offset: 0x000A4F70
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060024EA RID: 9450 RVA: 0x000A6D80 File Offset: 0x000A4F80
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

			// Token: 0x060024EB RID: 9451 RVA: 0x000A6DDC File Offset: 0x000A4FDC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C89 RID: 7305
			private static int max_field_count = 1;

			// Token: 0x04001C8A RID: 7306
			private string _ID;
		}
	}
}
