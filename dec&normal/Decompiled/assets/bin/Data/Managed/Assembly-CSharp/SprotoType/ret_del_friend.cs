using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000515 RID: 1301
	public class ret_del_friend
	{
		// Token: 0x02000516 RID: 1302
		public class request : SprotoTypeBase
		{
			// Token: 0x06002616 RID: 9750 RVA: 0x000A932C File Offset: 0x000A752C
			public request() : base(ret_del_friend.request.max_field_count)
			{
			}

			// Token: 0x06002617 RID: 9751 RVA: 0x000A933C File Offset: 0x000A753C
			public request(byte[] buffer) : base(ret_del_friend.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AA7 RID: 2727
			// (get) Token: 0x06002619 RID: 9753 RVA: 0x000A9358 File Offset: 0x000A7558
			// (set) Token: 0x0600261A RID: 9754 RVA: 0x000A9360 File Offset: 0x000A7560
			public long characterId
			{
				get
				{
					return this._characterId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._characterId = value;
				}
			}

			// Token: 0x17000AA8 RID: 2728
			// (get) Token: 0x0600261B RID: 9755 RVA: 0x000A9378 File Offset: 0x000A7578
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600261C RID: 9756 RVA: 0x000A9388 File Offset: 0x000A7588
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
						this.characterId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600261D RID: 9757 RVA: 0x000A93E4 File Offset: 0x000A75E4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CDB RID: 7387
			private static int max_field_count = 1;

			// Token: 0x04001CDC RID: 7388
			private long _characterId;
		}
	}
}
