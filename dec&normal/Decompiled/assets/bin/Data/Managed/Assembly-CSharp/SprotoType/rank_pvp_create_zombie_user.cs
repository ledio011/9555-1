using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200044E RID: 1102
	public class rank_pvp_create_zombie_user
	{
		// Token: 0x0200044F RID: 1103
		public class request : SprotoTypeBase
		{
			// Token: 0x06002269 RID: 8809 RVA: 0x000A2920 File Offset: 0x000A0B20
			public request() : base(rank_pvp_create_zombie_user.request.max_field_count)
			{
			}

			// Token: 0x0600226A RID: 8810 RVA: 0x000A2930 File Offset: 0x000A0B30
			public request(byte[] buffer) : base(rank_pvp_create_zombie_user.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009C5 RID: 2501
			// (get) Token: 0x0600226C RID: 8812 RVA: 0x000A294C File Offset: 0x000A0B4C
			// (set) Token: 0x0600226D RID: 8813 RVA: 0x000A2954 File Offset: 0x000A0B54
			public character character
			{
				get
				{
					return this._character;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._character = value;
				}
			}

			// Token: 0x170009C6 RID: 2502
			// (get) Token: 0x0600226E RID: 8814 RVA: 0x000A296C File Offset: 0x000A0B6C
			public bool HasCharacter
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600226F RID: 8815 RVA: 0x000A297C File Offset: 0x000A0B7C
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
						this.character = this.deserialize.read_obj<character>();
					}
				}
			}

			// Token: 0x06002270 RID: 8816 RVA: 0x000A29D8 File Offset: 0x000A0BD8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.character, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C03 RID: 7171
			private static int max_field_count = 1;

			// Token: 0x04001C04 RID: 7172
			private character _character;
		}
	}
}
