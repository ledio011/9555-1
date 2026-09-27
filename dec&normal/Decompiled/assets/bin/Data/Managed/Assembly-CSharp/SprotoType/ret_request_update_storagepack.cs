using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000585 RID: 1413
	public class ret_request_update_storagepack
	{
		// Token: 0x02000586 RID: 1414
		public class request : SprotoTypeBase
		{
			// Token: 0x060028FE RID: 10494 RVA: 0x000AEF78 File Offset: 0x000AD178
			public request() : base(ret_request_update_storagepack.request.max_field_count)
			{
			}

			// Token: 0x060028FF RID: 10495 RVA: 0x000AEF88 File Offset: 0x000AD188
			public request(byte[] buffer) : base(ret_request_update_storagepack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BA5 RID: 2981
			// (get) Token: 0x06002901 RID: 10497 RVA: 0x000AEFA4 File Offset: 0x000AD1A4
			// (set) Token: 0x06002902 RID: 10498 RVA: 0x000AEFAC File Offset: 0x000AD1AC
			public Dictionary<long, gameitem> gameitems
			{
				get
				{
					return this._gameitems;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._gameitems = value;
				}
			}

			// Token: 0x17000BA6 RID: 2982
			// (get) Token: 0x06002903 RID: 10499 RVA: 0x000AEFC4 File Offset: 0x000AD1C4
			public bool HasGameitems
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002904 RID: 10500 RVA: 0x000AEFD4 File Offset: 0x000AD1D4
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
						this.gameitems = this.deserialize.read_map<long, gameitem>((gameitem v) => v.indexId);
					}
				}
			}

			// Token: 0x06002905 RID: 10501 RVA: 0x000AF04C File Offset: 0x000AD24C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<long, gameitem>(this.gameitems, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DAD RID: 7597
			private static int max_field_count = 1;

			// Token: 0x04001DAE RID: 7598
			private Dictionary<long, gameitem> _gameitems;
		}
	}
}
