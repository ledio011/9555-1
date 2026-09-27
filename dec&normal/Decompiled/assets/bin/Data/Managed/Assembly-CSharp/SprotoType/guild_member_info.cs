using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003EA RID: 1002
	public class guild_member_info : SprotoTypeBase
	{
		// Token: 0x06001EF1 RID: 7921 RVA: 0x0009B734 File Offset: 0x00099934
		public guild_member_info() : base(guild_member_info.max_field_count)
		{
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0009B744 File Offset: 0x00099944
		public guild_member_info(byte[] buffer) : base(guild_member_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x0009B764 File Offset: 0x00099964
		// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x0009B76C File Offset: 0x0009996C
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

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x0009B784 File Offset: 0x00099984
		public bool HasGuildId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x0009B794 File Offset: 0x00099994
		// (set) Token: 0x06001EF8 RID: 7928 RVA: 0x0009B79C File Offset: 0x0009999C
		public long characterId
		{
			get
			{
				return this._characterId;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._characterId = value;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x0009B7B4 File Offset: 0x000999B4
		public bool HasCharacterId
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x0009B7C4 File Offset: 0x000999C4
		// (set) Token: 0x06001EFB RID: 7931 RVA: 0x0009B7CC File Offset: 0x000999CC
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._name = value;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x0009B7E4 File Offset: 0x000999E4
		public bool HasName
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x0009B7F4 File Offset: 0x000999F4
		// (set) Token: 0x06001EFE RID: 7934 RVA: 0x0009B7FC File Offset: 0x000999FC
		public long vip
		{
			get
			{
				return this._vip;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._vip = value;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x0009B814 File Offset: 0x00099A14
		public bool HasVip
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x0009B824 File Offset: 0x00099A24
		// (set) Token: 0x06001F01 RID: 7937 RVA: 0x0009B82C File Offset: 0x00099A2C
		public long profession
		{
			get
			{
				return this._profession;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._profession = value;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x0009B844 File Offset: 0x00099A44
		public bool HasProfession
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x0009B854 File Offset: 0x00099A54
		// (set) Token: 0x06001F04 RID: 7940 RVA: 0x0009B85C File Offset: 0x00099A5C
		public long level
		{
			get
			{
				return this._level;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._level = value;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x0009B874 File Offset: 0x00099A74
		public bool HasLevel
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x0009B884 File Offset: 0x00099A84
		// (set) Token: 0x06001F07 RID: 7943 RVA: 0x0009B88C File Offset: 0x00099A8C
		public long contribute
		{
			get
			{
				return this._contribute;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._contribute = value;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x0009B8A4 File Offset: 0x00099AA4
		public bool HasContribute
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x0009B8B4 File Offset: 0x00099AB4
		// (set) Token: 0x06001F0A RID: 7946 RVA: 0x0009B8BC File Offset: 0x00099ABC
		public long lastLogout
		{
			get
			{
				return this._lastLogout;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._lastLogout = value;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x0009B8D4 File Offset: 0x00099AD4
		public bool HasLastLogout
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x0009B8E4 File Offset: 0x00099AE4
		// (set) Token: 0x06001F0D RID: 7949 RVA: 0x0009B8EC File Offset: 0x00099AEC
		public long state
		{
			get
			{
				return this._state;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._state = value;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x0009B904 File Offset: 0x00099B04
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x0009B914 File Offset: 0x00099B14
		// (set) Token: 0x06001F10 RID: 7952 RVA: 0x0009B91C File Offset: 0x00099B1C
		public long job
		{
			get
			{
				return this._job;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._job = value;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x0009B934 File Offset: 0x00099B34
		public bool HasJob
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x0009B944 File Offset: 0x00099B44
		// (set) Token: 0x06001F13 RID: 7955 RVA: 0x0009B94C File Offset: 0x00099B4C
		public long combValue
		{
			get
			{
				return this._combValue;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._combValue = value;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x0009B964 File Offset: 0x00099B64
		public bool HasCombValue
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x0009B974 File Offset: 0x00099B74
		// (set) Token: 0x06001F16 RID: 7958 RVA: 0x0009B97C File Offset: 0x00099B7C
		public long all_contribute
		{
			get
			{
				return this._all_contribute;
			}
			set
			{
				this.has_field.set_field(11, true);
				this._all_contribute = value;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x0009B994 File Offset: 0x00099B94
		public bool HasAll_contribute
		{
			get
			{
				return this.has_field.has_field(11);
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x0009B9A4 File Offset: 0x00099BA4
		// (set) Token: 0x06001F19 RID: 7961 RVA: 0x0009B9AC File Offset: 0x00099BAC
		public long battle
		{
			get
			{
				return this._battle;
			}
			set
			{
				this.has_field.set_field(12, true);
				this._battle = value;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0009B9C4 File Offset: 0x00099BC4
		public bool HasBattle
		{
			get
			{
				return this.has_field.has_field(12);
			}
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0009B9D4 File Offset: 0x00099BD4
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
					this.characterId = this.deserialize.read_integer();
					break;
				case 2:
					this.name = this.deserialize.read_string();
					break;
				case 3:
					this.vip = this.deserialize.read_integer();
					break;
				case 4:
					this.profession = this.deserialize.read_integer();
					break;
				case 5:
					this.level = this.deserialize.read_integer();
					break;
				case 6:
					this.contribute = this.deserialize.read_integer();
					break;
				case 7:
					this.lastLogout = this.deserialize.read_integer();
					break;
				case 8:
					this.state = this.deserialize.read_integer();
					break;
				case 9:
					this.job = this.deserialize.read_integer();
					break;
				case 10:
					this.combValue = this.deserialize.read_integer();
					break;
				case 11:
					this.all_contribute = this.deserialize.read_integer();
					break;
				case 12:
					this.battle = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0009BB6C File Offset: 0x00099D6C
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.guildId, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.characterId, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_string(this.name, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.vip, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.profession, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.level, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.contribute, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.lastLogout, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.state, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_integer(this.job, 9);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_integer(this.combValue, 10);
			}
			if (this.has_field.has_field(11))
			{
				this.serialize.write_integer(this.all_contribute, 11);
			}
			if (this.has_field.has_field(12))
			{
				this.serialize.write_integer(this.battle, 12);
			}
			return this.serialize.close();
		}

		// Token: 0x04001B0F RID: 6927
		private static int max_field_count = 13;

		// Token: 0x04001B10 RID: 6928
		private long _guildId;

		// Token: 0x04001B11 RID: 6929
		private long _characterId;

		// Token: 0x04001B12 RID: 6930
		private string _name;

		// Token: 0x04001B13 RID: 6931
		private long _vip;

		// Token: 0x04001B14 RID: 6932
		private long _profession;

		// Token: 0x04001B15 RID: 6933
		private long _level;

		// Token: 0x04001B16 RID: 6934
		private long _contribute;

		// Token: 0x04001B17 RID: 6935
		private long _lastLogout;

		// Token: 0x04001B18 RID: 6936
		private long _state;

		// Token: 0x04001B19 RID: 6937
		private long _job;

		// Token: 0x04001B1A RID: 6938
		private long _combValue;

		// Token: 0x04001B1B RID: 6939
		private long _all_contribute;

		// Token: 0x04001B1C RID: 6940
		private long _battle;
	}
}
