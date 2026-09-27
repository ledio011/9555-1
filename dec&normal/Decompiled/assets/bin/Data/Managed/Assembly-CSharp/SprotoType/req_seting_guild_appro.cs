using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000490 RID: 1168
	public class req_seting_guild_appro
	{
		// Token: 0x02000491 RID: 1169
		public class request : SprotoTypeBase
		{
			// Token: 0x06002392 RID: 9106 RVA: 0x000A4A14 File Offset: 0x000A2C14
			public request() : base(req_seting_guild_appro.request.max_field_count)
			{
			}

			// Token: 0x06002393 RID: 9107 RVA: 0x000A4A24 File Offset: 0x000A2C24
			public request(byte[] buffer) : base(req_seting_guild_appro.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A07 RID: 2567
			// (get) Token: 0x06002395 RID: 9109 RVA: 0x000A4A40 File Offset: 0x000A2C40
			// (set) Token: 0x06002396 RID: 9110 RVA: 0x000A4A48 File Offset: 0x000A2C48
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

			// Token: 0x17000A08 RID: 2568
			// (get) Token: 0x06002397 RID: 9111 RVA: 0x000A4A60 File Offset: 0x000A2C60
			public bool HasGuildId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A09 RID: 2569
			// (get) Token: 0x06002398 RID: 9112 RVA: 0x000A4A70 File Offset: 0x000A2C70
			// (set) Token: 0x06002399 RID: 9113 RVA: 0x000A4A78 File Offset: 0x000A2C78
			public bool isNeedAppro
			{
				get
				{
					return this._isNeedAppro;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._isNeedAppro = value;
				}
			}

			// Token: 0x17000A0A RID: 2570
			// (get) Token: 0x0600239A RID: 9114 RVA: 0x000A4A90 File Offset: 0x000A2C90
			public bool HasIsNeedAppro
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600239B RID: 9115 RVA: 0x000A4AA0 File Offset: 0x000A2CA0
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
							this.isNeedAppro = this.deserialize.read_boolean();
						}
					}
					else
					{
						this.guildId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600239C RID: 9116 RVA: 0x000A4B18 File Offset: 0x000A2D18
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.guildId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_boolean(this.isNeedAppro, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C45 RID: 7237
			private static int max_field_count = 2;

			// Token: 0x04001C46 RID: 7238
			private long _guildId;

			// Token: 0x04001C47 RID: 7239
			private bool _isNeedAppro;
		}
	}
}
