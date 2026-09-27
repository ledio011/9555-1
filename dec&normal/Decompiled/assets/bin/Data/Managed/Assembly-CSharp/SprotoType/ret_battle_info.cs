using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004FB RID: 1275
	public class ret_battle_info
	{
		// Token: 0x020004FC RID: 1276
		public class request : SprotoTypeBase
		{
			// Token: 0x0600254A RID: 9546 RVA: 0x000A794C File Offset: 0x000A5B4C
			public request() : base(ret_battle_info.request.max_field_count)
			{
			}

			// Token: 0x0600254B RID: 9547 RVA: 0x000A795C File Offset: 0x000A5B5C
			public request(byte[] buffer) : base(ret_battle_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A55 RID: 2645
			// (get) Token: 0x0600254D RID: 9549 RVA: 0x000A7978 File Offset: 0x000A5B78
			// (set) Token: 0x0600254E RID: 9550 RVA: 0x000A7980 File Offset: 0x000A5B80
			public battle_info battle_info
			{
				get
				{
					return this._battle_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._battle_info = value;
				}
			}

			// Token: 0x17000A56 RID: 2646
			// (get) Token: 0x0600254F RID: 9551 RVA: 0x000A7998 File Offset: 0x000A5B98
			public bool HasBattle_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A57 RID: 2647
			// (get) Token: 0x06002550 RID: 9552 RVA: 0x000A79A8 File Offset: 0x000A5BA8
			// (set) Token: 0x06002551 RID: 9553 RVA: 0x000A79B0 File Offset: 0x000A5BB0
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._type = value;
				}
			}

			// Token: 0x17000A58 RID: 2648
			// (get) Token: 0x06002552 RID: 9554 RVA: 0x000A79C8 File Offset: 0x000A5BC8
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002553 RID: 9555 RVA: 0x000A79D8 File Offset: 0x000A5BD8
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
							this.type = this.deserialize.read_integer();
						}
					}
					else
					{
						this.battle_info = this.deserialize.read_obj<battle_info>();
					}
				}
			}

			// Token: 0x06002554 RID: 9556 RVA: 0x000A7A50 File Offset: 0x000A5C50
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.battle_info, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.type, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CA2 RID: 7330
			private static int max_field_count = 2;

			// Token: 0x04001CA3 RID: 7331
			private battle_info _battle_info;

			// Token: 0x04001CA4 RID: 7332
			private long _type;
		}
	}
}
