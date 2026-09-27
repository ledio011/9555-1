using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000569 RID: 1385
	public class ret_request_daily_buy
	{
		// Token: 0x0200056A RID: 1386
		public class request : SprotoTypeBase
		{
			// Token: 0x06002841 RID: 10305 RVA: 0x000AD7F0 File Offset: 0x000AB9F0
			public request() : base(ret_request_daily_buy.request.max_field_count)
			{
			}

			// Token: 0x06002842 RID: 10306 RVA: 0x000AD800 File Offset: 0x000ABA00
			public request(byte[] buffer) : base(ret_request_daily_buy.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B65 RID: 2917
			// (get) Token: 0x06002844 RID: 10308 RVA: 0x000AD81C File Offset: 0x000ABA1C
			// (set) Token: 0x06002845 RID: 10309 RVA: 0x000AD824 File Offset: 0x000ABA24
			public Dictionary<string, daily_buy> daily_buys
			{
				get
				{
					return this._daily_buys;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._daily_buys = value;
				}
			}

			// Token: 0x17000B66 RID: 2918
			// (get) Token: 0x06002846 RID: 10310 RVA: 0x000AD83C File Offset: 0x000ABA3C
			public bool HasDaily_buys
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002847 RID: 10311 RVA: 0x000AD84C File Offset: 0x000ABA4C
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
						this.daily_buys = this.deserialize.read_map<string, daily_buy>((daily_buy v) => v.ID);
					}
				}
			}

			// Token: 0x06002848 RID: 10312 RVA: 0x000AD8C4 File Offset: 0x000ABAC4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, daily_buy>(this.daily_buys, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D76 RID: 7542
			private static int max_field_count = 1;

			// Token: 0x04001D77 RID: 7543
			private Dictionary<string, daily_buy> _daily_buys;
		}
	}
}
