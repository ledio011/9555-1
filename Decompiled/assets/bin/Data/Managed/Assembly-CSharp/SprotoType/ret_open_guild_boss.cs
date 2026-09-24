using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000555 RID: 1365
	public class ret_open_guild_boss
	{
		// Token: 0x02000556 RID: 1366
		public class request : SprotoTypeBase
		{
			// Token: 0x060027B7 RID: 10167 RVA: 0x000AC6B8 File Offset: 0x000AA8B8
			public request() : base(ret_open_guild_boss.request.max_field_count)
			{
			}

			// Token: 0x060027B8 RID: 10168 RVA: 0x000AC6C8 File Offset: 0x000AA8C8
			public request(byte[] buffer) : base(ret_open_guild_boss.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B35 RID: 2869
			// (get) Token: 0x060027BA RID: 10170 RVA: 0x000AC6E4 File Offset: 0x000AA8E4
			// (set) Token: 0x060027BB RID: 10171 RVA: 0x000AC6EC File Offset: 0x000AA8EC
			public bool ok
			{
				get
				{
					return this._ok;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._ok = value;
				}
			}

			// Token: 0x17000B36 RID: 2870
			// (get) Token: 0x060027BC RID: 10172 RVA: 0x000AC704 File Offset: 0x000AA904
			public bool HasOk
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B37 RID: 2871
			// (get) Token: 0x060027BD RID: 10173 RVA: 0x000AC714 File Offset: 0x000AA914
			// (set) Token: 0x060027BE RID: 10174 RVA: 0x000AC71C File Offset: 0x000AA91C
			public guild_boss guild_boss
			{
				get
				{
					return this._guild_boss;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._guild_boss = value;
				}
			}

			// Token: 0x17000B38 RID: 2872
			// (get) Token: 0x060027BF RID: 10175 RVA: 0x000AC734 File Offset: 0x000AA934
			public bool HasGuild_boss
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060027C0 RID: 10176 RVA: 0x000AC744 File Offset: 0x000AA944
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
							this.guild_boss = this.deserialize.read_obj<guild_boss>();
						}
					}
					else
					{
						this.ok = this.deserialize.read_boolean();
					}
				}
			}

			// Token: 0x060027C1 RID: 10177 RVA: 0x000AC7BC File Offset: 0x000AA9BC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_boolean(this.ok, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj(this.guild_boss, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D4E RID: 7502
			private static int max_field_count = 2;

			// Token: 0x04001D4F RID: 7503
			private bool _ok;

			// Token: 0x04001D50 RID: 7504
			private guild_boss _guild_boss;
		}
	}
}
