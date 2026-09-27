using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003E9 RID: 1001
	public class guild_map_info : SprotoTypeBase
	{
		// Token: 0x06001EDA RID: 7898 RVA: 0x0009B410 File Offset: 0x00099610
		public guild_map_info() : base(guild_map_info.max_field_count)
		{
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x0009B420 File Offset: 0x00099620
		public guild_map_info(byte[] buffer) : base(guild_map_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x0009B43C File Offset: 0x0009963C
		// (set) Token: 0x06001EDE RID: 7902 RVA: 0x0009B444 File Offset: 0x00099644
		public string id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._id = value;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x0009B45C File Offset: 0x0009965C
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x0009B46C File Offset: 0x0009966C
		// (set) Token: 0x06001EE1 RID: 7905 RVA: 0x0009B474 File Offset: 0x00099674
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._guildId = value;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x0009B48C File Offset: 0x0009968C
		public bool HasGuildId
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x0009B49C File Offset: 0x0009969C
		// (set) Token: 0x06001EE4 RID: 7908 RVA: 0x0009B4A4 File Offset: 0x000996A4
		public string guildName
		{
			get
			{
				return this._guildName;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._guildName = value;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x0009B4BC File Offset: 0x000996BC
		public bool HasGuildName
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x0009B4CC File Offset: 0x000996CC
		// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x0009B4D4 File Offset: 0x000996D4
		public long guildIcon
		{
			get
			{
				return this._guildIcon;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._guildIcon = value;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x0009B4EC File Offset: 0x000996EC
		public bool HasGuildIcon
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x0009B4FC File Offset: 0x000996FC
		// (set) Token: 0x06001EEA RID: 7914 RVA: 0x0009B504 File Offset: 0x00099704
		public long requireState
		{
			get
			{
				return this._requireState;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._requireState = value;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x0009B51C File Offset: 0x0009971C
		public bool HasRequireState
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x0009B52C File Offset: 0x0009972C
		// (set) Token: 0x06001EED RID: 7917 RVA: 0x0009B534 File Offset: 0x00099734
		public long state
		{
			get
			{
				return this._state;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._state = value;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0009B54C File Offset: 0x0009974C
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x0009B55C File Offset: 0x0009975C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.id = this.deserialize.read_string();
					break;
				case 1:
					this.guildId = this.deserialize.read_integer();
					break;
				case 2:
					this.guildName = this.deserialize.read_string();
					break;
				case 3:
					this.guildIcon = this.deserialize.read_integer();
					break;
				case 4:
					this.requireState = this.deserialize.read_integer();
					break;
				case 5:
					this.state = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x0009B63C File Offset: 0x0009983C
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.guildId, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_string(this.guildName, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.guildIcon, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.requireState, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.state, 5);
			}
			return this.serialize.close();
		}

		// Token: 0x04001B08 RID: 6920
		private static int max_field_count = 6;

		// Token: 0x04001B09 RID: 6921
		private string _id;

		// Token: 0x04001B0A RID: 6922
		private long _guildId;

		// Token: 0x04001B0B RID: 6923
		private string _guildName;

		// Token: 0x04001B0C RID: 6924
		private long _guildIcon;

		// Token: 0x04001B0D RID: 6925
		private long _requireState;

		// Token: 0x04001B0E RID: 6926
		private long _state;
	}
}
