using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200055B RID: 1371
	public class ret_random_online_character_list
	{
		// Token: 0x0200055C RID: 1372
		public class request : SprotoTypeBase
		{
			// Token: 0x060027D9 RID: 10201 RVA: 0x000ACAD4 File Offset: 0x000AACD4
			public request() : base(ret_random_online_character_list.request.max_field_count)
			{
			}

			// Token: 0x060027DA RID: 10202 RVA: 0x000ACAE4 File Offset: 0x000AACE4
			public request(byte[] buffer) : base(ret_random_online_character_list.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B3F RID: 2879
			// (get) Token: 0x060027DC RID: 10204 RVA: 0x000ACB00 File Offset: 0x000AAD00
			// (set) Token: 0x060027DD RID: 10205 RVA: 0x000ACB08 File Offset: 0x000AAD08
			public List<friend_info> friend_list
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

			// Token: 0x17000B40 RID: 2880
			// (get) Token: 0x060027DE RID: 10206 RVA: 0x000ACB20 File Offset: 0x000AAD20
			public bool HasFriend_list
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060027DF RID: 10207 RVA: 0x000ACB30 File Offset: 0x000AAD30
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.friend_list = this.deserialize.read_obj_list<friend_info>();
					}
				}
			}

			// Token: 0x060027E0 RID: 10208 RVA: 0x000ACB8C File Offset: 0x000AAD8C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<friend_info>(this.friend_list, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D57 RID: 7511
			private static int max_field_count = 1;

			// Token: 0x04001D58 RID: 7512
			private List<friend_info> _friend_list;
		}
	}
}
