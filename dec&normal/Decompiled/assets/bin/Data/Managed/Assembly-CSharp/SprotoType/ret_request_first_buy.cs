using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200056D RID: 1389
	public class ret_request_first_buy
	{
		// Token: 0x0200056E RID: 1390
		public class request : SprotoTypeBase
		{
			// Token: 0x0600285B RID: 10331 RVA: 0x000ADB28 File Offset: 0x000ABD28
			public request() : base(ret_request_first_buy.request.max_field_count)
			{
			}

			// Token: 0x0600285C RID: 10332 RVA: 0x000ADB38 File Offset: 0x000ABD38
			public request(byte[] buffer) : base(ret_request_first_buy.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B6D RID: 2925
			// (get) Token: 0x0600285E RID: 10334 RVA: 0x000ADB54 File Offset: 0x000ABD54
			// (set) Token: 0x0600285F RID: 10335 RVA: 0x000ADB5C File Offset: 0x000ABD5C
			public string ID
			{
				get
				{
					return this._ID;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._ID = value;
				}
			}

			// Token: 0x17000B6E RID: 2926
			// (get) Token: 0x06002860 RID: 10336 RVA: 0x000ADB74 File Offset: 0x000ABD74
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B6F RID: 2927
			// (get) Token: 0x06002861 RID: 10337 RVA: 0x000ADB84 File Offset: 0x000ABD84
			// (set) Token: 0x06002862 RID: 10338 RVA: 0x000ADB8C File Offset: 0x000ABD8C
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._state = value;
				}
			}

			// Token: 0x17000B70 RID: 2928
			// (get) Token: 0x06002863 RID: 10339 RVA: 0x000ADBA4 File Offset: 0x000ABDA4
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002864 RID: 10340 RVA: 0x000ADBB4 File Offset: 0x000ABDB4
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
							this.state = this.deserialize.read_integer();
						}
					}
					else
					{
						this.ID = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002865 RID: 10341 RVA: 0x000ADC2C File Offset: 0x000ABE2C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.state, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D7E RID: 7550
			private static int max_field_count = 2;

			// Token: 0x04001D7F RID: 7551
			private string _ID;

			// Token: 0x04001D80 RID: 7552
			private long _state;
		}
	}
}
