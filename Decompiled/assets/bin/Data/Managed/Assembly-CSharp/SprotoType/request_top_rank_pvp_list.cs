using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004D9 RID: 1241
	public class request_top_rank_pvp_list
	{
		// Token: 0x020004DA RID: 1242
		public class request : SprotoTypeBase
		{
			// Token: 0x0600249C RID: 9372 RVA: 0x000A6514 File Offset: 0x000A4714
			public request() : base(request_top_rank_pvp_list.request.max_field_count)
			{
			}

			// Token: 0x0600249D RID: 9373 RVA: 0x000A6524 File Offset: 0x000A4724
			public request(byte[] buffer) : base(request_top_rank_pvp_list.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A25 RID: 2597
			// (get) Token: 0x0600249F RID: 9375 RVA: 0x000A6540 File Offset: 0x000A4740
			// (set) Token: 0x060024A0 RID: 9376 RVA: 0x000A6548 File Offset: 0x000A4748
			public long curPage
			{
				get
				{
					return this._curPage;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._curPage = value;
				}
			}

			// Token: 0x17000A26 RID: 2598
			// (get) Token: 0x060024A1 RID: 9377 RVA: 0x000A6560 File Offset: 0x000A4760
			public bool HasCurPage
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060024A2 RID: 9378 RVA: 0x000A6570 File Offset: 0x000A4770
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
						this.curPage = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060024A3 RID: 9379 RVA: 0x000A65CC File Offset: 0x000A47CC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.curPage, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C79 RID: 7289
			private static int max_field_count = 1;

			// Token: 0x04001C7A RID: 7290
			private long _curPage;
		}
	}
}
