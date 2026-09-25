using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000583 RID: 1411
	public class ret_request_update_friend_useinfo
	{
		// Token: 0x02000584 RID: 1412
		public class request : SprotoTypeBase
		{
			// Token: 0x060028F1 RID: 10481 RVA: 0x000AEDDC File Offset: 0x000ACFDC
			public request() : base(ret_request_update_friend_useinfo.request.max_field_count)
			{
			}

			// Token: 0x060028F2 RID: 10482 RVA: 0x000AEDEC File Offset: 0x000ACFEC
			public request(byte[] buffer) : base(ret_request_update_friend_useinfo.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BA1 RID: 2977
			// (get) Token: 0x060028F4 RID: 10484 RVA: 0x000AEE08 File Offset: 0x000AD008
			// (set) Token: 0x060028F5 RID: 10485 RVA: 0x000AEE10 File Offset: 0x000AD010
			public Dictionary<long, friend_info> friend_list
			{
				get
				{
					return this._friend_list;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._friend_list = value;
				}
			}

			// Token: 0x17000BA2 RID: 2978
			// (get) Token: 0x060028F6 RID: 10486 RVA: 0x000AEE28 File Offset: 0x000AD028
			public bool HasFriend_list
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000BA3 RID: 2979
			// (get) Token: 0x060028F7 RID: 10487 RVA: 0x000AEE38 File Offset: 0x000AD038
			// (set) Token: 0x060028F8 RID: 10488 RVA: 0x000AEE40 File Offset: 0x000AD040
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._type = value;
				}
			}

			// Token: 0x17000BA4 RID: 2980
			// (get) Token: 0x060028F9 RID: 10489 RVA: 0x000AEE58 File Offset: 0x000AD058
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060028FA RID: 10490 RVA: 0x000AEE68 File Offset: 0x000AD068
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
							this.type = this.deserialize.read_integer();
						}
					}
					else
					{
						this.friend_list = this.deserialize.read_map<long, friend_info>((friend_info v) => v.friendId);
					}
				}
			}

			// Token: 0x060028FB RID: 10491 RVA: 0x000AEEFC File Offset: 0x000AD0FC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<long, friend_info>(this.friend_list, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.type, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DA9 RID: 7593
			private static int max_field_count = 2;

			// Token: 0x04001DAA RID: 7594
			private Dictionary<long, friend_info> _friend_list;

			// Token: 0x04001DAB RID: 7595
			private long _type;
		}
	}
}
