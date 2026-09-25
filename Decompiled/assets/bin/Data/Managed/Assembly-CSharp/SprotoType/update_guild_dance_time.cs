using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200063C RID: 1596
	public class update_guild_dance_time
	{
		// Token: 0x0200063D RID: 1597
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E4C RID: 11852 RVA: 0x000B99F8 File Offset: 0x000B7BF8
			public request() : base(update_guild_dance_time.request.max_field_count)
			{
			}

			// Token: 0x06002E4D RID: 11853 RVA: 0x000B9A08 File Offset: 0x000B7C08
			public request(byte[] buffer) : base(update_guild_dance_time.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D9D RID: 3485
			// (get) Token: 0x06002E4F RID: 11855 RVA: 0x000B9A24 File Offset: 0x000B7C24
			// (set) Token: 0x06002E50 RID: 11856 RVA: 0x000B9A2C File Offset: 0x000B7C2C
			public long index
			{
				get
				{
					return this._index;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._index = value;
				}
			}

			// Token: 0x17000D9E RID: 3486
			// (get) Token: 0x06002E51 RID: 11857 RVA: 0x000B9A44 File Offset: 0x000B7C44
			public bool HasIndex
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002E52 RID: 11858 RVA: 0x000B9A54 File Offset: 0x000B7C54
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
						this.index = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002E53 RID: 11859 RVA: 0x000B9AB0 File Offset: 0x000B7CB0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.index, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F20 RID: 7968
			private static int max_field_count = 1;

			// Token: 0x04001F21 RID: 7969
			private long _index;
		}
	}
}
