using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003EF RID: 1007
	public class guild_skill : SprotoTypeBase
	{
		// Token: 0x06001F32 RID: 7986 RVA: 0x0009BFE0 File Offset: 0x0009A1E0
		public guild_skill() : base(guild_skill.max_field_count)
		{
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x0009BFF0 File Offset: 0x0009A1F0
		public guild_skill(byte[] buffer) : base(guild_skill.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x0009C00C File Offset: 0x0009A20C
		// (set) Token: 0x06001F36 RID: 7990 RVA: 0x0009C014 File Offset: 0x0009A214
		public long skillType
		{
			get
			{
				return this._skillType;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._skillType = value;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x0009C02C File Offset: 0x0009A22C
		public bool HasSkillType
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x0009C03C File Offset: 0x0009A23C
		// (set) Token: 0x06001F39 RID: 7993 RVA: 0x0009C044 File Offset: 0x0009A244
		public long level
		{
			get
			{
				return this._level;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._level = value;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x0009C05C File Offset: 0x0009A25C
		public bool HasLevel
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0009C06C File Offset: 0x0009A26C
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
						this.level = this.deserialize.read_integer();
					}
				}
				else
				{
					this.skillType = this.deserialize.read_integer();
				}
			}
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x0009C0E4 File Offset: 0x0009A2E4
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.skillType, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.level, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x04001B22 RID: 6946
		private static int max_field_count = 2;

		// Token: 0x04001B23 RID: 6947
		private long _skillType;

		// Token: 0x04001B24 RID: 6948
		private long _level;
	}
}
