using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200051F RID: 1311
	public class ret_guild_approve_resverve
	{
		// Token: 0x02000520 RID: 1312
		public class request : SprotoTypeBase
		{
			// Token: 0x06002655 RID: 9813 RVA: 0x000A9AEC File Offset: 0x000A7CEC
			public request() : base(ret_guild_approve_resverve.request.max_field_count)
			{
			}

			// Token: 0x06002656 RID: 9814 RVA: 0x000A9AFC File Offset: 0x000A7CFC
			public request(byte[] buffer) : base(ret_guild_approve_resverve.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000ABB RID: 2747
			// (get) Token: 0x06002658 RID: 9816 RVA: 0x000A9B18 File Offset: 0x000A7D18
			// (set) Token: 0x06002659 RID: 9817 RVA: 0x000A9B20 File Offset: 0x000A7D20
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

			// Token: 0x17000ABC RID: 2748
			// (get) Token: 0x0600265A RID: 9818 RVA: 0x000A9B38 File Offset: 0x000A7D38
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000ABD RID: 2749
			// (get) Token: 0x0600265B RID: 9819 RVA: 0x000A9B48 File Offset: 0x000A7D48
			// (set) Token: 0x0600265C RID: 9820 RVA: 0x000A9B50 File Offset: 0x000A7D50
			public long isAgree
			{
				get
				{
					return this._isAgree;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._isAgree = value;
				}
			}

			// Token: 0x17000ABE RID: 2750
			// (get) Token: 0x0600265D RID: 9821 RVA: 0x000A9B68 File Offset: 0x000A7D68
			public bool HasIsAgree
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000ABF RID: 2751
			// (get) Token: 0x0600265E RID: 9822 RVA: 0x000A9B78 File Offset: 0x000A7D78
			// (set) Token: 0x0600265F RID: 9823 RVA: 0x000A9B80 File Offset: 0x000A7D80
			public bool state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._state = value;
				}
			}

			// Token: 0x17000AC0 RID: 2752
			// (get) Token: 0x06002660 RID: 9824 RVA: 0x000A9B98 File Offset: 0x000A7D98
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002661 RID: 9825 RVA: 0x000A9BA8 File Offset: 0x000A7DA8
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.characterId = this.deserialize.read_integer();
						break;
					case 1:
						this.isAgree = this.deserialize.read_integer();
						break;
					case 2:
						this.state = this.deserialize.read_boolean();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002662 RID: 9826 RVA: 0x000A9C3C File Offset: 0x000A7E3C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.isAgree, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_boolean(this.state, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CED RID: 7405
			private static int max_field_count = 3;

			// Token: 0x04001CEE RID: 7406
			private long _characterId;

			// Token: 0x04001CEF RID: 7407
			private long _isAgree;

			// Token: 0x04001CF0 RID: 7408
			private bool _state;
		}
	}
}
