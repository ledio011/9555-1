using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005F9 RID: 1529
	public class syn_friend_info
	{
		// Token: 0x020005FA RID: 1530
		public class request : SprotoTypeBase
		{
			// Token: 0x06002C39 RID: 11321 RVA: 0x000B56D4 File Offset: 0x000B38D4
			public request() : base(syn_friend_info.request.max_field_count)
			{
			}

			// Token: 0x06002C3A RID: 11322 RVA: 0x000B56E4 File Offset: 0x000B38E4
			public request(byte[] buffer) : base(syn_friend_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000CD1 RID: 3281
			// (get) Token: 0x06002C3C RID: 11324 RVA: 0x000B5700 File Offset: 0x000B3900
			// (set) Token: 0x06002C3D RID: 11325 RVA: 0x000B5708 File Offset: 0x000B3908
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

			// Token: 0x17000CD2 RID: 3282
			// (get) Token: 0x06002C3E RID: 11326 RVA: 0x000B5720 File Offset: 0x000B3920
			public bool HasFriend
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002C3F RID: 11327 RVA: 0x000B5730 File Offset: 0x000B3930
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

			// Token: 0x06002C40 RID: 11328 RVA: 0x000B578C File Offset: 0x000B398C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.friend, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E8B RID: 7819
			private static int max_field_count = 1;

			// Token: 0x04001E8C RID: 7820
			private friend_info _friend;
		}
	}
}
