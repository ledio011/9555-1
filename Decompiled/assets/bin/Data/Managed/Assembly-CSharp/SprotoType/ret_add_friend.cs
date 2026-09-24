using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004F5 RID: 1269
	public class ret_add_friend
	{
		// Token: 0x020004F6 RID: 1270
		public class request : SprotoTypeBase
		{
			// Token: 0x06002520 RID: 9504 RVA: 0x000A740C File Offset: 0x000A560C
			public request() : base(ret_add_friend.request.max_field_count)
			{
			}

			// Token: 0x06002521 RID: 9505 RVA: 0x000A741C File Offset: 0x000A561C
			public request(byte[] buffer) : base(ret_add_friend.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A45 RID: 2629
			// (get) Token: 0x06002523 RID: 9507 RVA: 0x000A7438 File Offset: 0x000A5638
			// (set) Token: 0x06002524 RID: 9508 RVA: 0x000A7440 File Offset: 0x000A5640
			public friend_info friend
			{
				get
				{
					return this._friend;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._friend = value;
				}
			}

			// Token: 0x17000A46 RID: 2630
			// (get) Token: 0x06002525 RID: 9509 RVA: 0x000A7458 File Offset: 0x000A5658
			public bool HasFriend
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002526 RID: 9510 RVA: 0x000A7468 File Offset: 0x000A5668
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
						this.friend = this.deserialize.read_obj<friend_info>();
					}
				}
			}

			// Token: 0x06002527 RID: 9511 RVA: 0x000A74C4 File Offset: 0x000A56C4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.friend, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C97 RID: 7319
			private static int max_field_count = 1;

			// Token: 0x04001C98 RID: 7320
			private friend_info _friend;
		}
	}
}
