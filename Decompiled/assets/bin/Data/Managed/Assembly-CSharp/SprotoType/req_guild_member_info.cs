using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000478 RID: 1144
	public class req_guild_member_info
	{
		// Token: 0x02000479 RID: 1145
		public class request : SprotoTypeBase
		{
			// Token: 0x06002320 RID: 8992 RVA: 0x000A3D30 File Offset: 0x000A1F30
			public request() : base(req_guild_member_info.request.max_field_count)
			{
			}

			// Token: 0x06002321 RID: 8993 RVA: 0x000A3D40 File Offset: 0x000A1F40
			public request(byte[] buffer) : base(req_guild_member_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009EB RID: 2539
			// (get) Token: 0x06002323 RID: 8995 RVA: 0x000A3D5C File Offset: 0x000A1F5C
			// (set) Token: 0x06002324 RID: 8996 RVA: 0x000A3D64 File Offset: 0x000A1F64
			public long guildId
			{
				get
				{
					return this._guildId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guildId = value;
				}
			}

			// Token: 0x170009EC RID: 2540
			// (get) Token: 0x06002325 RID: 8997 RVA: 0x000A3D7C File Offset: 0x000A1F7C
			public bool HasGuildId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002326 RID: 8998 RVA: 0x000A3D8C File Offset: 0x000A1F8C
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
						this.guildId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002327 RID: 8999 RVA: 0x000A3DE8 File Offset: 0x000A1FE8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.guildId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C2B RID: 7211
			private static int max_field_count = 1;

			// Token: 0x04001C2C RID: 7212
			private long _guildId;
		}
	}
}
