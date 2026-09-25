using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000427 RID: 1063
	public class notice_add_friend
	{
		// Token: 0x02000428 RID: 1064
		public class request : SprotoTypeBase
		{
			// Token: 0x060020EE RID: 8430 RVA: 0x0009F7E8 File Offset: 0x0009D9E8
			public request() : base(notice_add_friend.request.max_field_count)
			{
			}

			// Token: 0x060020EF RID: 8431 RVA: 0x0009F7F8 File Offset: 0x0009D9F8
			public request(byte[] buffer) : base(notice_add_friend.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700091D RID: 2333
			// (get) Token: 0x060020F1 RID: 8433 RVA: 0x0009F814 File Offset: 0x0009DA14
			// (set) Token: 0x060020F2 RID: 8434 RVA: 0x0009F81C File Offset: 0x0009DA1C
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

			// Token: 0x1700091E RID: 2334
			// (get) Token: 0x060020F3 RID: 8435 RVA: 0x0009F834 File Offset: 0x0009DA34
			public bool HasFriend
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060020F4 RID: 8436 RVA: 0x0009F844 File Offset: 0x0009DA44
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

			// Token: 0x060020F5 RID: 8437 RVA: 0x0009F8A0 File Offset: 0x0009DAA0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.friend, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B99 RID: 7065
			private static int max_field_count = 1;

			// Token: 0x04001B9A RID: 7066
			private friend_info _friend;
		}
	}
}
