using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200056B RID: 1387
	public class ret_request_dance_info
	{
		// Token: 0x0200056C RID: 1388
		public class request : SprotoTypeBase
		{
			// Token: 0x0600284B RID: 10315 RVA: 0x000AD91C File Offset: 0x000ABB1C
			public request() : base(ret_request_dance_info.request.max_field_count)
			{
			}

			// Token: 0x0600284C RID: 10316 RVA: 0x000AD92C File Offset: 0x000ABB2C
			public request(byte[] buffer) : base(ret_request_dance_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B67 RID: 2919
			// (get) Token: 0x0600284E RID: 10318 RVA: 0x000AD948 File Offset: 0x000ABB48
			// (set) Token: 0x0600284F RID: 10319 RVA: 0x000AD950 File Offset: 0x000ABB50
			public string curUse
			{
				get
				{
					return this._curUse;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._curUse = value;
				}
			}

			// Token: 0x17000B68 RID: 2920
			// (get) Token: 0x06002850 RID: 10320 RVA: 0x000AD968 File Offset: 0x000ABB68
			public bool HasCurUse
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B69 RID: 2921
			// (get) Token: 0x06002851 RID: 10321 RVA: 0x000AD978 File Offset: 0x000ABB78
			// (set) Token: 0x06002852 RID: 10322 RVA: 0x000AD980 File Offset: 0x000ABB80
			public Dictionary<string, dance_info> dance_info
			{
				get
				{
					return this._dance_info;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._dance_info = value;
				}
			}

			// Token: 0x17000B6A RID: 2922
			// (get) Token: 0x06002853 RID: 10323 RVA: 0x000AD998 File Offset: 0x000ABB98
			public bool HasDance_info
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000B6B RID: 2923
			// (get) Token: 0x06002854 RID: 10324 RVA: 0x000AD9A8 File Offset: 0x000ABBA8
			// (set) Token: 0x06002855 RID: 10325 RVA: 0x000AD9B0 File Offset: 0x000ABBB0
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._type = value;
				}
			}

			// Token: 0x17000B6C RID: 2924
			// (get) Token: 0x06002856 RID: 10326 RVA: 0x000AD9C8 File Offset: 0x000ABBC8
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002857 RID: 10327 RVA: 0x000AD9D8 File Offset: 0x000ABBD8
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.curUse = this.deserialize.read_string();
						break;
					case 1:
						this.dance_info = this.deserialize.read_map<string, dance_info>((dance_info v) => v.ID);
						break;
					case 2:
						this.type = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002858 RID: 10328 RVA: 0x000ADA88 File Offset: 0x000ABC88
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.curUse, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<string, dance_info>(this.dance_info, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.type, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D79 RID: 7545
			private static int max_field_count = 3;

			// Token: 0x04001D7A RID: 7546
			private string _curUse;

			// Token: 0x04001D7B RID: 7547
			private Dictionary<string, dance_info> _dance_info;

			// Token: 0x04001D7C RID: 7548
			private long _type;
		}
	}
}
