using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000561 RID: 1377
	public class ret_request_30_day_info
	{
		// Token: 0x02000562 RID: 1378
		public class request : SprotoTypeBase
		{
			// Token: 0x060027F8 RID: 10232 RVA: 0x000ACE80 File Offset: 0x000AB080
			public request() : base(ret_request_30_day_info.request.max_field_count)
			{
			}

			// Token: 0x060027F9 RID: 10233 RVA: 0x000ACE90 File Offset: 0x000AB090
			public request(byte[] buffer) : base(ret_request_30_day_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B47 RID: 2887
			// (get) Token: 0x060027FB RID: 10235 RVA: 0x000ACEAC File Offset: 0x000AB0AC
			// (set) Token: 0x060027FC RID: 10236 RVA: 0x000ACEB4 File Offset: 0x000AB0B4
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

			// Token: 0x17000B48 RID: 2888
			// (get) Token: 0x060027FD RID: 10237 RVA: 0x000ACECC File Offset: 0x000AB0CC
			public bool HasCur_sign
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B49 RID: 2889
			// (get) Token: 0x060027FE RID: 10238 RVA: 0x000ACEDC File Offset: 0x000AB0DC
			// (set) Token: 0x060027FF RID: 10239 RVA: 0x000ACEE4 File Offset: 0x000AB0E4
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

			// Token: 0x17000B4A RID: 2890
			// (get) Token: 0x06002800 RID: 10240 RVA: 0x000ACEFC File Offset: 0x000AB0FC
			public bool HasReplenish
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000B4B RID: 2891
			// (get) Token: 0x06002801 RID: 10241 RVA: 0x000ACF0C File Offset: 0x000AB10C
			// (set) Token: 0x06002802 RID: 10242 RVA: 0x000ACF14 File Offset: 0x000AB114
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

			// Token: 0x17000B4C RID: 2892
			// (get) Token: 0x06002803 RID: 10243 RVA: 0x000ACF2C File Offset: 0x000AB12C
			public bool HasSys_sign
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000B4D RID: 2893
			// (get) Token: 0x06002804 RID: 10244 RVA: 0x000ACF3C File Offset: 0x000AB13C
			// (set) Token: 0x06002805 RID: 10245 RVA: 0x000ACF44 File Offset: 0x000AB144
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

			// Token: 0x17000B4E RID: 2894
			// (get) Token: 0x06002806 RID: 10246 RVA: 0x000ACF5C File Offset: 0x000AB15C
			public bool HasCur_sign_state
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000B4F RID: 2895
			// (get) Token: 0x06002807 RID: 10247 RVA: 0x000ACF6C File Offset: 0x000AB16C
			// (set) Token: 0x06002808 RID: 10248 RVA: 0x000ACF74 File Offset: 0x000AB174
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

			// Token: 0x17000B50 RID: 2896
			// (get) Token: 0x06002809 RID: 10249 RVA: 0x000ACF8C File Offset: 0x000AB18C
			public bool HasReplenish_sign_state
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000B51 RID: 2897
			// (get) Token: 0x0600280A RID: 10250 RVA: 0x000ACF9C File Offset: 0x000AB19C
			// (set) Token: 0x0600280B RID: 10251 RVA: 0x000ACFA4 File Offset: 0x000AB1A4
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

			// Token: 0x17000B52 RID: 2898
			// (get) Token: 0x0600280C RID: 10252 RVA: 0x000ACFBC File Offset: 0x000AB1BC
			public bool HasCount
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x17000B53 RID: 2899
			// (get) Token: 0x0600280D RID: 10253 RVA: 0x000ACFCC File Offset: 0x000AB1CC
			// (set) Token: 0x0600280E RID: 10254 RVA: 0x000ACFD4 File Offset: 0x000AB1D4
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

			// Token: 0x17000B54 RID: 2900
			// (get) Token: 0x0600280F RID: 10255 RVA: 0x000ACFEC File Offset: 0x000AB1EC
			public bool HasStr
			{
				get
				{
					return this.has_field.has_field(6);
				}
			}

			// Token: 0x06002810 RID: 10256 RVA: 0x000ACFFC File Offset: 0x000AB1FC
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

			// Token: 0x06002811 RID: 10257 RVA: 0x000AD0F8 File Offset: 0x000AB2F8
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

			// Token: 0x04001D5F RID: 7519
			private static int max_field_count = 7;

			// Token: 0x04001D60 RID: 7520
			private long _cur_sign;

			// Token: 0x04001D61 RID: 7521
			private long _replenish;

			// Token: 0x04001D62 RID: 7522
			private long _sys_sign;

			// Token: 0x04001D63 RID: 7523
			private bool _cur_sign_state;

			// Token: 0x04001D64 RID: 7524
			private bool _replenish_sign_state;

			// Token: 0x04001D65 RID: 7525
			private long _count;

			// Token: 0x04001D66 RID: 7526
			private string _str;
		}
	}
}
