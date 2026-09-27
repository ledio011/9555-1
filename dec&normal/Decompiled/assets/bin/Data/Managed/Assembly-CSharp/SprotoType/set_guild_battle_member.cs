using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005CA RID: 1482
	public class set_guild_battle_member
	{
		// Token: 0x020005CB RID: 1483
		public class request : SprotoTypeBase
		{
			// Token: 0x06002ABE RID: 10942 RVA: 0x000B26E0 File Offset: 0x000B08E0
			public request() : base(set_guild_battle_member.request.max_field_count)
			{
			}

			// Token: 0x06002ABF RID: 10943 RVA: 0x000B26F0 File Offset: 0x000B08F0
			public request(byte[] buffer) : base(set_guild_battle_member.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C3D RID: 3133
			// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x000B270C File Offset: 0x000B090C
			// (set) Token: 0x06002AC2 RID: 10946 RVA: 0x000B2714 File Offset: 0x000B0914
			public List<long> list
			{
				get
				{
					return this._list;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._list = value;
				}
			}

			// Token: 0x17000C3E RID: 3134
			// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x000B272C File Offset: 0x000B092C
			public bool HasList
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002AC4 RID: 10948 RVA: 0x000B273C File Offset: 0x000B093C
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
						this.list = this.deserialize.read_integer_list();
					}
				}
			}

			// Token: 0x06002AC5 RID: 10949 RVA: 0x000B2798 File Offset: 0x000B0998
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.list, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E24 RID: 7716
			private static int max_field_count = 1;

			// Token: 0x04001E25 RID: 7717
			private List<long> _list;
		}
	}
}
