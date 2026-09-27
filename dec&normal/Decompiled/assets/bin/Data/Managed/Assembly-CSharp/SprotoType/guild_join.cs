using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003E1 RID: 993
	public class guild_join
	{
		// Token: 0x020003E2 RID: 994
		public class request : SprotoTypeBase
		{
			// Token: 0x06001EB7 RID: 7863 RVA: 0x0009B008 File Offset: 0x00099208
			public request() : base(guild_join.request.max_field_count)
			{
			}

			// Token: 0x06001EB8 RID: 7864 RVA: 0x0009B018 File Offset: 0x00099218
			public request(byte[] buffer) : base(guild_join.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x06001EBA RID: 7866 RVA: 0x0009B034 File Offset: 0x00099234
			// (set) Token: 0x06001EBB RID: 7867 RVA: 0x0009B03C File Offset: 0x0009923C
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

			// Token: 0x17000844 RID: 2116
			// (get) Token: 0x06001EBC RID: 7868 RVA: 0x0009B054 File Offset: 0x00099254
			public bool HasGuildId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001EBD RID: 7869 RVA: 0x0009B064 File Offset: 0x00099264
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

			// Token: 0x06001EBE RID: 7870 RVA: 0x0009B0C0 File Offset: 0x000992C0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.guildId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B00 RID: 6912
			private static int max_field_count = 1;

			// Token: 0x04001B01 RID: 6913
			private long _guildId;
		}
	}
}
