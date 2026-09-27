using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000593 RID: 1427
	public class ret_sign_30_day
	{
		// Token: 0x02000594 RID: 1428
		public class request : SprotoTypeBase
		{
			// Token: 0x06002942 RID: 10562 RVA: 0x000AF768 File Offset: 0x000AD968
			public request() : base(ret_sign_30_day.request.max_field_count)
			{
			}

			// Token: 0x06002943 RID: 10563 RVA: 0x000AF778 File Offset: 0x000AD978
			public request(byte[] buffer) : base(ret_sign_30_day.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BB5 RID: 2997
			// (get) Token: 0x06002945 RID: 10565 RVA: 0x000AF794 File Offset: 0x000AD994
			// (set) Token: 0x06002946 RID: 10566 RVA: 0x000AF79C File Offset: 0x000AD99C
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

			// Token: 0x17000BB6 RID: 2998
			// (get) Token: 0x06002947 RID: 10567 RVA: 0x000AF7B4 File Offset: 0x000AD9B4
			public bool HasCur_sign
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000BB7 RID: 2999
			// (get) Token: 0x06002948 RID: 10568 RVA: 0x000AF7C4 File Offset: 0x000AD9C4
			// (set) Token: 0x06002949 RID: 10569 RVA: 0x000AF7CC File Offset: 0x000AD9CC
			public long replenish
			{
				get
				{
					return this._replenish;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._replenish = value;
				}
			}

			// Token: 0x17000BB8 RID: 3000
			// (get) Token: 0x0600294A RID: 10570 RVA: 0x000AF7E4 File Offset: 0x000AD9E4
			public bool HasReplenish
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000BB9 RID: 3001
			// (get) Token: 0x0600294B RID: 10571 RVA: 0x000AF7F4 File Offset: 0x000AD9F4
			// (set) Token: 0x0600294C RID: 10572 RVA: 0x000AF7FC File Offset: 0x000AD9FC
			public long sys_sign
			{
				get
				{
					return this._sys_sign;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._sys_sign = value;
				}
			}

			// Token: 0x17000BBA RID: 3002
			// (get) Token: 0x0600294D RID: 10573 RVA: 0x000AF814 File Offset: 0x000ADA14
			public bool HasSys_sign
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000BBB RID: 3003
			// (get) Token: 0x0600294E RID: 10574 RVA: 0x000AF824 File Offset: 0x000ADA24
			// (set) Token: 0x0600294F RID: 10575 RVA: 0x000AF82C File Offset: 0x000ADA2C
			public bool cur_sign_state
			{
				get
				{
					return this._cur_sign_state;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._cur_sign_state = value;
				}
			}

			// Token: 0x17000BBC RID: 3004
			// (get) Token: 0x06002950 RID: 10576 RVA: 0x000AF844 File Offset: 0x000ADA44
			public bool HasCur_sign_state
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000BBD RID: 3005
			// (get) Token: 0x06002951 RID: 10577 RVA: 0x000AF854 File Offset: 0x000ADA54
			// (set) Token: 0x06002952 RID: 10578 RVA: 0x000AF85C File Offset: 0x000ADA5C
			public bool replenish_sign_state
			{
				get
				{
					return this._replenish_sign_state;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._replenish_sign_state = value;
				}
			}

			// Token: 0x17000BBE RID: 3006
			// (get) Token: 0x06002953 RID: 10579 RVA: 0x000AF874 File Offset: 0x000ADA74
			public bool HasReplenish_sign_state
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000BBF RID: 3007
			// (get) Token: 0x06002954 RID: 10580 RVA: 0x000AF884 File Offset: 0x000ADA84
			// (set) Token: 0x06002955 RID: 10581 RVA: 0x000AF88C File Offset: 0x000ADA8C
			public long count
			{
				get
				{
					return this._count;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._count = value;
				}
			}

			// Token: 0x17000BC0 RID: 3008
			// (get) Token: 0x06002956 RID: 10582 RVA: 0x000AF8A4 File Offset: 0x000ADAA4
			public bool HasCount
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x17000BC1 RID: 3009
			// (get) Token: 0x06002957 RID: 10583 RVA: 0x000AF8B4 File Offset: 0x000ADAB4
			// (set) Token: 0x06002958 RID: 10584 RVA: 0x000AF8BC File Offset: 0x000ADABC
			public string str
			{
				get
				{
					return this._str;
				}
				set
				{
					this.has_field.set_field(6, true);
					this._str = value;
				}
			}

			// Token: 0x17000BC2 RID: 3010
			// (get) Token: 0x06002959 RID: 10585 RVA: 0x000AF8D4 File Offset: 0x000ADAD4
			public bool HasStr
			{
				get
				{
					return this.has_field.has_field(6);
				}
			}

			// Token: 0x0600295A RID: 10586 RVA: 0x000AF8E4 File Offset: 0x000ADAE4
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
						this.replenish = this.deserialize.read_integer();
						break;
					case 2:
						this.sys_sign = this.deserialize.read_integer();
						break;
					case 3:
						this.cur_sign_state = this.deserialize.read_boolean();
						break;
					case 4:
						this.replenish_sign_state = this.deserialize.read_boolean();
						break;
					case 5:
						this.count = this.deserialize.read_integer();
						break;
					case 6:
						this.str = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x0600295B RID: 10587 RVA: 0x000AF9E0 File Offset: 0x000ADBE0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.cur_sign, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.replenish, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.sys_sign, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_boolean(this.cur_sign_state, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_boolean(this.replenish_sign_state, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.count, 5);
				}
				if (this.has_field.has_field(6))
				{
					this.serialize.write_string(this.str, 6);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DBE RID: 7614
			private static int max_field_count = 7;

			// Token: 0x04001DBF RID: 7615
			private long _cur_sign;

			// Token: 0x04001DC0 RID: 7616
			private long _replenish;

			// Token: 0x04001DC1 RID: 7617
			private long _sys_sign;

			// Token: 0x04001DC2 RID: 7618
			private bool _cur_sign_state;

			// Token: 0x04001DC3 RID: 7619
			private bool _replenish_sign_state;

			// Token: 0x04001DC4 RID: 7620
			private long _count;

			// Token: 0x04001DC5 RID: 7621
			private string _str;
		}
	}
}
