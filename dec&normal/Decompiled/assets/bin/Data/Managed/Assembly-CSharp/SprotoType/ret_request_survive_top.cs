using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200057D RID: 1405
	public class ret_request_survive_top
	{
		// Token: 0x0200057E RID: 1406
		public class request : SprotoTypeBase
		{
			// Token: 0x060028C4 RID: 10436 RVA: 0x000AE828 File Offset: 0x000ACA28
			public request() : base(ret_request_survive_top.request.max_field_count)
			{
			}

			// Token: 0x060028C5 RID: 10437 RVA: 0x000AE838 File Offset: 0x000ACA38
			public request(byte[] buffer) : base(ret_request_survive_top.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B8F RID: 2959
			// (get) Token: 0x060028C7 RID: 10439 RVA: 0x000AE854 File Offset: 0x000ACA54
			// (set) Token: 0x060028C8 RID: 10440 RVA: 0x000AE85C File Offset: 0x000ACA5C
			public List<score_info> score_infos
			{
				get
				{
					return this._score_infos;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._score_infos = value;
				}
			}

			// Token: 0x17000B90 RID: 2960
			// (get) Token: 0x060028C9 RID: 10441 RVA: 0x000AE874 File Offset: 0x000ACA74
			public bool HasScore_infos
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B91 RID: 2961
			// (get) Token: 0x060028CA RID: 10442 RVA: 0x000AE884 File Offset: 0x000ACA84
			// (set) Token: 0x060028CB RID: 10443 RVA: 0x000AE88C File Offset: 0x000ACA8C
			public long my_rank
			{
				get
				{
					return this._my_rank;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._my_rank = value;
				}
			}

			// Token: 0x17000B92 RID: 2962
			// (get) Token: 0x060028CC RID: 10444 RVA: 0x000AE8A4 File Offset: 0x000ACAA4
			public bool HasMy_rank
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000B93 RID: 2963
			// (get) Token: 0x060028CD RID: 10445 RVA: 0x000AE8B4 File Offset: 0x000ACAB4
			// (set) Token: 0x060028CE RID: 10446 RVA: 0x000AE8BC File Offset: 0x000ACABC
			public long my_score
			{
				get
				{
					return this._my_score;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._my_score = value;
				}
			}

			// Token: 0x17000B94 RID: 2964
			// (get) Token: 0x060028CF RID: 10447 RVA: 0x000AE8D4 File Offset: 0x000ACAD4
			public bool HasMy_score
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000B95 RID: 2965
			// (get) Token: 0x060028D0 RID: 10448 RVA: 0x000AE8E4 File Offset: 0x000ACAE4
			// (set) Token: 0x060028D1 RID: 10449 RVA: 0x000AE8EC File Offset: 0x000ACAEC
			public long end_time
			{
				get
				{
					return this._end_time;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._end_time = value;
				}
			}

			// Token: 0x17000B96 RID: 2966
			// (get) Token: 0x060028D2 RID: 10450 RVA: 0x000AE904 File Offset: 0x000ACB04
			public bool HasEnd_time
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x060028D3 RID: 10451 RVA: 0x000AE914 File Offset: 0x000ACB14
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.score_infos = this.deserialize.read_obj_list<score_info>();
						break;
					case 1:
						this.my_rank = this.deserialize.read_integer();
						break;
					case 2:
						this.my_score = this.deserialize.read_integer();
						break;
					case 3:
						this.end_time = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060028D4 RID: 10452 RVA: 0x000AE9C0 File Offset: 0x000ACBC0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<score_info>(this.score_infos, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.my_rank, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.my_score, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.end_time, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D9D RID: 7581
			private static int max_field_count = 4;

			// Token: 0x04001D9E RID: 7582
			private List<score_info> _score_infos;

			// Token: 0x04001D9F RID: 7583
			private long _my_rank;

			// Token: 0x04001DA0 RID: 7584
			private long _my_score;

			// Token: 0x04001DA1 RID: 7585
			private long _end_time;
		}
	}
}
