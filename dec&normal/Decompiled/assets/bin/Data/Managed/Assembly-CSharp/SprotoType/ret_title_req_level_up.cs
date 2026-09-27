using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005A1 RID: 1441
	public class ret_title_req_level_up
	{
		// Token: 0x020005A2 RID: 1442
		public class request : SprotoTypeBase
		{
			// Token: 0x060029AF RID: 10671 RVA: 0x000B053C File Offset: 0x000AE73C
			public request() : base(ret_title_req_level_up.request.max_field_count)
			{
			}

			// Token: 0x060029B0 RID: 10672 RVA: 0x000B054C File Offset: 0x000AE74C
			public request(byte[] buffer) : base(ret_title_req_level_up.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BDF RID: 3039
			// (get) Token: 0x060029B2 RID: 10674 RVA: 0x000B0568 File Offset: 0x000AE768
			// (set) Token: 0x060029B3 RID: 10675 RVA: 0x000B0570 File Offset: 0x000AE770
			public long title_level
			{
				get
				{
					return this._title_level;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._title_level = value;
				}
			}

			// Token: 0x17000BE0 RID: 3040
			// (get) Token: 0x060029B4 RID: 10676 RVA: 0x000B0588 File Offset: 0x000AE788
			public bool HasTitle_level
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000BE1 RID: 3041
			// (get) Token: 0x060029B5 RID: 10677 RVA: 0x000B0598 File Offset: 0x000AE798
			// (set) Token: 0x060029B6 RID: 10678 RVA: 0x000B05A0 File Offset: 0x000AE7A0
			public long title_exp
			{
				get
				{
					return this._title_exp;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._title_exp = value;
				}
			}

			// Token: 0x17000BE2 RID: 3042
			// (get) Token: 0x060029B7 RID: 10679 RVA: 0x000B05B8 File Offset: 0x000AE7B8
			public bool HasTitle_exp
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060029B8 RID: 10680 RVA: 0x000B05C8 File Offset: 0x000AE7C8
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 != 1)
						{
							this.deserialize.read_unknow_data();
						}
						else
						{
							this.title_exp = this.deserialize.read_integer();
						}
					}
					else
					{
						this.title_level = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060029B9 RID: 10681 RVA: 0x000B0640 File Offset: 0x000AE840
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.title_level, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.title_exp, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DDE RID: 7646
			private static int max_field_count = 2;

			// Token: 0x04001DDF RID: 7647
			private long _title_level;

			// Token: 0x04001DE0 RID: 7648
			private long _title_exp;
		}
	}
}
