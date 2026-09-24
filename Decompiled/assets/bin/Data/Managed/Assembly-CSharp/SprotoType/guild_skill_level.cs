using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003F0 RID: 1008
	public class guild_skill_level
	{
		// Token: 0x020003F1 RID: 1009
		public class request : SprotoTypeBase
		{
			// Token: 0x06001F3E RID: 7998 RVA: 0x0009C158 File Offset: 0x0009A358
			public request() : base(guild_skill_level.request.max_field_count)
			{
			}

			// Token: 0x06001F3F RID: 7999 RVA: 0x0009C168 File Offset: 0x0009A368
			public request(byte[] buffer) : base(guild_skill_level.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700087B RID: 2171
			// (get) Token: 0x06001F41 RID: 8001 RVA: 0x0009C184 File Offset: 0x0009A384
			// (set) Token: 0x06001F42 RID: 8002 RVA: 0x0009C18C File Offset: 0x0009A38C
			public long guildSkillType
			{
				get
				{
					return this._guildSkillType;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guildSkillType = value;
				}
			}

			// Token: 0x1700087C RID: 2172
			// (get) Token: 0x06001F43 RID: 8003 RVA: 0x0009C1A4 File Offset: 0x0009A3A4
			public bool HasGuildSkillType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001F44 RID: 8004 RVA: 0x0009C1B4 File Offset: 0x0009A3B4
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
						this.guildSkillType = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001F45 RID: 8005 RVA: 0x0009C210 File Offset: 0x0009A410
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.guildSkillType, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B25 RID: 6949
			private static int max_field_count = 1;

			// Token: 0x04001B26 RID: 6950
			private long _guildSkillType;
		}
	}
}
