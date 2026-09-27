using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000579 RID: 1401
	public class ret_request_retrieve_info
	{
		// Token: 0x0200057A RID: 1402
		public class request : SprotoTypeBase
		{
			// Token: 0x060028AB RID: 10411 RVA: 0x000AE514 File Offset: 0x000AC714
			public request() : base(ret_request_retrieve_info.request.max_field_count)
			{
			}

			// Token: 0x060028AC RID: 10412 RVA: 0x000AE524 File Offset: 0x000AC724
			public request(byte[] buffer) : base(ret_request_retrieve_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B87 RID: 2951
			// (get) Token: 0x060028AE RID: 10414 RVA: 0x000AE540 File Offset: 0x000AC740
			// (set) Token: 0x060028AF RID: 10415 RVA: 0x000AE548 File Offset: 0x000AC748
			public Dictionary<string, retrieve_info> info
			{
				get
				{
					return this._info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._info = value;
				}
			}

			// Token: 0x17000B88 RID: 2952
			// (get) Token: 0x060028B0 RID: 10416 RVA: 0x000AE560 File Offset: 0x000AC760
			public bool HasInfo
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060028B1 RID: 10417 RVA: 0x000AE570 File Offset: 0x000AC770
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
						this.info = this.deserialize.read_map<string, retrieve_info>((retrieve_info v) => v.ID);
					}
				}
			}

			// Token: 0x060028B2 RID: 10418 RVA: 0x000AE5E8 File Offset: 0x000AC7E8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, retrieve_info>(this.info, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D96 RID: 7574
			private static int max_field_count = 1;

			// Token: 0x04001D97 RID: 7575
			private Dictionary<string, retrieve_info> _info;
		}
	}
}
