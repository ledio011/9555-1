using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003EB RID: 1003
	public class guild_req_info
	{
		// Token: 0x020003EC RID: 1004
		public class request : SprotoTypeBase
		{
			// Token: 0x06001F1E RID: 7966 RVA: 0x0009BD68 File Offset: 0x00099F68
			public request() : base(guild_req_info.request.max_field_count)
			{
			}

			// Token: 0x06001F1F RID: 7967 RVA: 0x0009BD78 File Offset: 0x00099F78
			public request(byte[] buffer) : base(guild_req_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000871 RID: 2161
			// (get) Token: 0x06001F21 RID: 7969 RVA: 0x0009BD94 File Offset: 0x00099F94
			// (set) Token: 0x06001F22 RID: 7970 RVA: 0x0009BD9C File Offset: 0x00099F9C
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

			// Token: 0x17000872 RID: 2162
			// (get) Token: 0x06001F23 RID: 7971 RVA: 0x0009BDB4 File Offset: 0x00099FB4
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001F24 RID: 7972 RVA: 0x0009BDC4 File Offset: 0x00099FC4
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

			// Token: 0x06001F25 RID: 7973 RVA: 0x0009BE20 File Offset: 0x0009A020
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B1D RID: 6941
			private static int max_field_count = 1;

			// Token: 0x04001B1E RID: 6942
			private long _characterId;
		}
	}
}
