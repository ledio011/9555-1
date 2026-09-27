using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200058F RID: 1423
	public class ret_search_online_character_by_name
	{
		// Token: 0x02000590 RID: 1424
		public class request : SprotoTypeBase
		{
			// Token: 0x06002930 RID: 10544 RVA: 0x000AF558 File Offset: 0x000AD758
			public request() : base(ret_search_online_character_by_name.request.max_field_count)
			{
			}

			// Token: 0x06002931 RID: 10545 RVA: 0x000AF568 File Offset: 0x000AD768
			public request(byte[] buffer) : base(ret_search_online_character_by_name.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BB1 RID: 2993
			// (get) Token: 0x06002933 RID: 10547 RVA: 0x000AF584 File Offset: 0x000AD784
			// (set) Token: 0x06002934 RID: 10548 RVA: 0x000AF58C File Offset: 0x000AD78C
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

			// Token: 0x17000BB2 RID: 2994
			// (get) Token: 0x06002935 RID: 10549 RVA: 0x000AF5A4 File Offset: 0x000AD7A4
			public bool HasFriend_list
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002936 RID: 10550 RVA: 0x000AF5B4 File Offset: 0x000AD7B4
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

			// Token: 0x06002937 RID: 10551 RVA: 0x000AF610 File Offset: 0x000AD810
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<friend_info>(this.friend_list, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DBA RID: 7610
			private static int max_field_count = 1;

			// Token: 0x04001DBB RID: 7611
			private List<friend_info> _friend_list;
		}
	}
}
