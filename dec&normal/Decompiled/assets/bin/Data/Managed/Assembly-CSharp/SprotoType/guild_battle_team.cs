using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003D4 RID: 980
	public class guild_battle_team : SprotoTypeBase
	{
		// Token: 0x06001E0B RID: 7691 RVA: 0x0009992C File Offset: 0x00097B2C
		public guild_battle_team() : base(guild_battle_team.max_field_count)
		{
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x0009993C File Offset: 0x00097B3C
		public guild_battle_team(byte[] buffer) : base(guild_battle_team.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x00099958 File Offset: 0x00097B58
		// (set) Token: 0x06001E0F RID: 7695 RVA: 0x00099960 File Offset: 0x00097B60
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

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x00099978 File Offset: 0x00097B78
		public bool HasGuildId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x00099988 File Offset: 0x00097B88
		// (set) Token: 0x06001E12 RID: 7698 RVA: 0x00099990 File Offset: 0x00097B90
		public string guildName
		{
			get
			{
				return this._guildName;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._guildName = value;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x000999A8 File Offset: 0x00097BA8
		public bool HasGuildName
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x000999B8 File Offset: 0x00097BB8
		// (set) Token: 0x06001E15 RID: 7701 RVA: 0x000999C0 File Offset: 0x00097BC0
		public long index
		{
			get
			{
				return this._index;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._index = value;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x000999D8 File Offset: 0x00097BD8
		public bool HasIndex
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x000999E8 File Offset: 0x00097BE8
		// (set) Token: 0x06001E18 RID: 7704 RVA: 0x000999F0 File Offset: 0x00097BF0
		public long state
		{
			get
			{
				return this._state;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._state = value;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x00099A08 File Offset: 0x00097C08
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x00099A18 File Offset: 0x00097C18
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.guildId = this.deserialize.read_integer();
					break;
				case 1:
					this.guildName = this.deserialize.read_string();
					break;
				case 2:
					this.index = this.deserialize.read_integer();
					break;
				case 3:
					this.state = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x00099AC4 File Offset: 0x00097CC4
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.guildId, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.guildName, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.index, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.state, 3);
			}
			return this.serialize.close();
		}

		// Token: 0x04001ACE RID: 6862
		private static int max_field_count = 4;

		// Token: 0x04001ACF RID: 6863
		private long _guildId;

		// Token: 0x04001AD0 RID: 6864
		private string _guildName;

		// Token: 0x04001AD1 RID: 6865
		private long _index;

		// Token: 0x04001AD2 RID: 6866
		private long _state;
	}
}
