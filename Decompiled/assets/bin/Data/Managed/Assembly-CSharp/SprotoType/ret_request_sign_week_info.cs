using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200057B RID: 1403
	public class ret_request_sign_week_info
	{
		// Token: 0x0200057C RID: 1404
		public class request : SprotoTypeBase
		{
			// Token: 0x060028B5 RID: 10421 RVA: 0x000AE640 File Offset: 0x000AC840
			public request() : base(ret_request_sign_week_info.request.max_field_count)
			{
			}

			// Token: 0x060028B6 RID: 10422 RVA: 0x000AE650 File Offset: 0x000AC850
			public request(byte[] buffer) : base(ret_request_sign_week_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B89 RID: 2953
			// (get) Token: 0x060028B8 RID: 10424 RVA: 0x000AE66C File Offset: 0x000AC86C
			// (set) Token: 0x060028B9 RID: 10425 RVA: 0x000AE674 File Offset: 0x000AC874
			public long cur_sign
			{
				get
				{
					return this._cur_sign;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._cur_sign = value;
				}
			}

			// Token: 0x17000B8A RID: 2954
			// (get) Token: 0x060028BA RID: 10426 RVA: 0x000AE68C File Offset: 0x000AC88C
			public bool HasCur_sign
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B8B RID: 2955
			// (get) Token: 0x060028BB RID: 10427 RVA: 0x000AE69C File Offset: 0x000AC89C
			// (set) Token: 0x060028BC RID: 10428 RVA: 0x000AE6A4 File Offset: 0x000AC8A4
			public bool cur_sign_state
			{
				get
				{
					return this._cur_sign_state;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._cur_sign_state = value;
				}
			}

			// Token: 0x17000B8C RID: 2956
			// (get) Token: 0x060028BD RID: 10429 RVA: 0x000AE6BC File Offset: 0x000AC8BC
			public bool HasCur_sign_state
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000B8D RID: 2957
			// (get) Token: 0x060028BE RID: 10430 RVA: 0x000AE6CC File Offset: 0x000AC8CC
			// (set) Token: 0x060028BF RID: 10431 RVA: 0x000AE6D4 File Offset: 0x000AC8D4
			public bool complete
			{
				get
				{
					return this._complete;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._complete = value;
				}
			}

			// Token: 0x17000B8E RID: 2958
			// (get) Token: 0x060028C0 RID: 10432 RVA: 0x000AE6EC File Offset: 0x000AC8EC
			public bool HasComplete
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x060028C1 RID: 10433 RVA: 0x000AE6FC File Offset: 0x000AC8FC
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.cur_sign = this.deserialize.read_integer();
						break;
					case 1:
						this.cur_sign_state = this.deserialize.read_boolean();
						break;
					case 2:
						this.complete = this.deserialize.read_boolean();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060028C2 RID: 10434 RVA: 0x000AE790 File Offset: 0x000AC990
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.cur_sign, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_boolean(this.cur_sign_state, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_boolean(this.complete, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D99 RID: 7577
			private static int max_field_count = 3;

			// Token: 0x04001D9A RID: 7578
			private long _cur_sign;

			// Token: 0x04001D9B RID: 7579
			private bool _cur_sign_state;

			// Token: 0x04001D9C RID: 7580
			private bool _complete;
		}
	}
}
