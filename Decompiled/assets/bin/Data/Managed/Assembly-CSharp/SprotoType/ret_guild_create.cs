using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200052B RID: 1323
	public class ret_guild_create
	{
		// Token: 0x0200052C RID: 1324
		public class request : SprotoTypeBase
		{
			// Token: 0x06002691 RID: 9873 RVA: 0x000AA1FC File Offset: 0x000A83FC
			public request() : base(ret_guild_create.request.max_field_count)
			{
			}

			// Token: 0x06002692 RID: 9874 RVA: 0x000AA20C File Offset: 0x000A840C
			public request(byte[] buffer) : base(ret_guild_create.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000ACB RID: 2763
			// (get) Token: 0x06002694 RID: 9876 RVA: 0x000AA228 File Offset: 0x000A8428
			// (set) Token: 0x06002695 RID: 9877 RVA: 0x000AA230 File Offset: 0x000A8430
			public guild_info guild_info
			{
				get
				{
					return this._guild_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guild_info = value;
				}
			}

			// Token: 0x17000ACC RID: 2764
			// (get) Token: 0x06002696 RID: 9878 RVA: 0x000AA248 File Offset: 0x000A8448
			public bool HasGuild_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000ACD RID: 2765
			// (get) Token: 0x06002697 RID: 9879 RVA: 0x000AA258 File Offset: 0x000A8458
			// (set) Token: 0x06002698 RID: 9880 RVA: 0x000AA260 File Offset: 0x000A8460
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

			// Token: 0x17000ACE RID: 2766
			// (get) Token: 0x06002699 RID: 9881 RVA: 0x000AA278 File Offset: 0x000A8478
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000ACF RID: 2767
			// (get) Token: 0x0600269A RID: 9882 RVA: 0x000AA288 File Offset: 0x000A8488
			// (set) Token: 0x0600269B RID: 9883 RVA: 0x000AA290 File Offset: 0x000A8490
			public Dictionary<string, donate_record> donate_records
			{
				get
				{
					return this._donate_records;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._donate_records = value;
				}
			}

			// Token: 0x17000AD0 RID: 2768
			// (get) Token: 0x0600269C RID: 9884 RVA: 0x000AA2A8 File Offset: 0x000A84A8
			public bool HasDonate_records
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x0600269D RID: 9885 RVA: 0x000AA2B8 File Offset: 0x000A84B8
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.guild_info = this.deserialize.read_obj<guild_info>();
						break;
					case 1:
						this.state = this.deserialize.read_integer();
						break;
					case 2:
						this.donate_records = this.deserialize.read_map<string, donate_record>((donate_record v) => v.id);
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x0600269E RID: 9886 RVA: 0x000AA368 File Offset: 0x000A8568
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.guild_info, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.state, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_obj<string, donate_record>(this.donate_records, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CFB RID: 7419
			private static int max_field_count = 3;

			// Token: 0x04001CFC RID: 7420
			private guild_info _guild_info;

			// Token: 0x04001CFD RID: 7421
			private long _state;

			// Token: 0x04001CFE RID: 7422
			private Dictionary<string, donate_record> _donate_records;
		}
	}
}
