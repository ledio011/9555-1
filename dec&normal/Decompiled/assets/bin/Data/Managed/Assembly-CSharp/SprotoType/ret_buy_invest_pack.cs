using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000501 RID: 1281
	public class ret_buy_invest_pack
	{
		// Token: 0x02000502 RID: 1282
		public class request : SprotoTypeBase
		{
			// Token: 0x06002574 RID: 9588 RVA: 0x000A7E8C File Offset: 0x000A608C
			public request() : base(ret_buy_invest_pack.request.max_field_count)
			{
			}

			// Token: 0x06002575 RID: 9589 RVA: 0x000A7E9C File Offset: 0x000A609C
			public request(byte[] buffer) : base(ret_buy_invest_pack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A65 RID: 2661
			// (get) Token: 0x06002577 RID: 9591 RVA: 0x000A7EB8 File Offset: 0x000A60B8
			// (set) Token: 0x06002578 RID: 9592 RVA: 0x000A7EC0 File Offset: 0x000A60C0
			public Dictionary<string, invest_pack> invest_pack
			{
				get
				{
					return this._invest_pack;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._invest_pack = value;
				}
			}

			// Token: 0x17000A66 RID: 2662
			// (get) Token: 0x06002579 RID: 9593 RVA: 0x000A7ED8 File Offset: 0x000A60D8
			public bool HasInvest_pack
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600257A RID: 9594 RVA: 0x000A7EE8 File Offset: 0x000A60E8
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
						this.invest_pack = this.deserialize.read_map<string, invest_pack>((invest_pack v) => v.ID);
					}
				}
			}

			// Token: 0x0600257B RID: 9595 RVA: 0x000A7F60 File Offset: 0x000A6160
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, invest_pack>(this.invest_pack, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CAD RID: 7341
			private static int max_field_count = 1;

			// Token: 0x04001CAE RID: 7342
			private Dictionary<string, invest_pack> _invest_pack;
		}
	}
}
