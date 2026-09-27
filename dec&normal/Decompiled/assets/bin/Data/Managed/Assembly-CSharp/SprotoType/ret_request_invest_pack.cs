using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000573 RID: 1395
	public class ret_request_invest_pack
	{
		// Token: 0x02000574 RID: 1396
		public class request : SprotoTypeBase
		{
			// Token: 0x06002888 RID: 10376 RVA: 0x000AE0D4 File Offset: 0x000AC2D4
			public request() : base(ret_request_invest_pack.request.max_field_count)
			{
			}

			// Token: 0x06002889 RID: 10377 RVA: 0x000AE0E4 File Offset: 0x000AC2E4
			public request(byte[] buffer) : base(ret_request_invest_pack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B7D RID: 2941
			// (get) Token: 0x0600288B RID: 10379 RVA: 0x000AE100 File Offset: 0x000AC300
			// (set) Token: 0x0600288C RID: 10380 RVA: 0x000AE108 File Offset: 0x000AC308
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

			// Token: 0x17000B7E RID: 2942
			// (get) Token: 0x0600288D RID: 10381 RVA: 0x000AE120 File Offset: 0x000AC320
			public bool HasInvest_pack
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600288E RID: 10382 RVA: 0x000AE130 File Offset: 0x000AC330
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

			// Token: 0x0600288F RID: 10383 RVA: 0x000AE1A8 File Offset: 0x000AC3A8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, invest_pack>(this.invest_pack, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D8C RID: 7564
			private static int max_field_count = 1;

			// Token: 0x04001D8D RID: 7565
			private Dictionary<string, invest_pack> _invest_pack;
		}
	}
}
