using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000595 RID: 1429
	public class ret_sign_week
	{
		// Token: 0x02000596 RID: 1430
		public class request : SprotoTypeBase
		{
			// Token: 0x0600295D RID: 10589 RVA: 0x000AFB04 File Offset: 0x000ADD04
			public request() : base(ret_sign_week.request.max_field_count)
			{
			}

			// Token: 0x0600295E RID: 10590 RVA: 0x000AFB14 File Offset: 0x000ADD14
			public request(byte[] buffer) : base(ret_sign_week.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BC3 RID: 3011
			// (get) Token: 0x06002960 RID: 10592 RVA: 0x000AFB30 File Offset: 0x000ADD30
			// (set) Token: 0x06002961 RID: 10593 RVA: 0x000AFB38 File Offset: 0x000ADD38
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

			// Token: 0x17000BC4 RID: 3012
			// (get) Token: 0x06002962 RID: 10594 RVA: 0x000AFB50 File Offset: 0x000ADD50
			public bool HasCur_sign
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000BC5 RID: 3013
			// (get) Token: 0x06002963 RID: 10595 RVA: 0x000AFB60 File Offset: 0x000ADD60
			// (set) Token: 0x06002964 RID: 10596 RVA: 0x000AFB68 File Offset: 0x000ADD68
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

			// Token: 0x17000BC6 RID: 3014
			// (get) Token: 0x06002965 RID: 10597 RVA: 0x000AFB80 File Offset: 0x000ADD80
			public bool HasCur_sign_state
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002966 RID: 10598 RVA: 0x000AFB90 File Offset: 0x000ADD90
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
							this.cur_sign_state = this.deserialize.read_boolean();
						}
					}
					else
					{
						this.cur_sign = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002967 RID: 10599 RVA: 0x000AFC08 File Offset: 0x000ADE08
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
				return this.serialize.close();
			}

			// Token: 0x04001DC6 RID: 7622
			private static int max_field_count = 2;

			// Token: 0x04001DC7 RID: 7623
			private long _cur_sign;

			// Token: 0x04001DC8 RID: 7624
			private bool _cur_sign_state;
		}
	}
}
