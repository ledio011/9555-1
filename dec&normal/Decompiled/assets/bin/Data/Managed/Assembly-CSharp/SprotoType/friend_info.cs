using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003B3 RID: 947
	public class friend_info : SprotoTypeBase
	{
		// Token: 0x06001C8A RID: 7306 RVA: 0x00096688 File Offset: 0x00094888
		public friend_info() : base(friend_info.max_field_count)
		{
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00096698 File Offset: 0x00094898
		public friend_info(byte[] buffer) : base(friend_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x000966B8 File Offset: 0x000948B8
		// (set) Token: 0x06001C8E RID: 7310 RVA: 0x000966C0 File Offset: 0x000948C0
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

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x000966D8 File Offset: 0x000948D8
		public bool HasCharacterId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x000966E8 File Offset: 0x000948E8
		// (set) Token: 0x06001C91 RID: 7313 RVA: 0x000966F0 File Offset: 0x000948F0
		public long friendId
		{
			get
			{
				return this._friendId;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._friendId = value;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x00096708 File Offset: 0x00094908
		public bool HasFriendId
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x00096718 File Offset: 0x00094918
		// (set) Token: 0x06001C94 RID: 7316 RVA: 0x00096720 File Offset: 0x00094920
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

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x00096738 File Offset: 0x00094938
		public bool HasName
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x00096748 File Offset: 0x00094948
		// (set) Token: 0x06001C97 RID: 7319 RVA: 0x00096750 File Offset: 0x00094950
		public long level
		{
			get
			{
				return this._level;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._level = value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x00096768 File Offset: 0x00094968
		public bool HasLevel
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x00096778 File Offset: 0x00094978
		// (set) Token: 0x06001C9A RID: 7322 RVA: 0x00096780 File Offset: 0x00094980
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

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x00096798 File Offset: 0x00094998
		public bool HasProfession
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x000967A8 File Offset: 0x000949A8
		// (set) Token: 0x06001C9D RID: 7325 RVA: 0x000967B0 File Offset: 0x000949B0
		public long combValue
		{
			get
			{
				return this._combValue;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._combValue = value;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x000967C8 File Offset: 0x000949C8
		public bool HasCombValue
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x000967D8 File Offset: 0x000949D8
		// (set) Token: 0x06001CA0 RID: 7328 RVA: 0x000967E0 File Offset: 0x000949E0
		public long state
		{
			get
			{
				return this._state;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._state = value;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x000967F8 File Offset: 0x000949F8
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x00096808 File Offset: 0x00094A08
		// (set) Token: 0x06001CA3 RID: 7331 RVA: 0x00096810 File Offset: 0x00094A10
		public long timeInfo
		{
			get
			{
				return this._timeInfo;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._timeInfo = value;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x00096828 File Offset: 0x00094A28
		public bool HasTimeInfo
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x00096838 File Offset: 0x00094A38
		// (set) Token: 0x06001CA6 RID: 7334 RVA: 0x00096840 File Offset: 0x00094A40
		public long friendType
		{
			get
			{
				return this._friendType;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._friendType = value;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x00096858 File Offset: 0x00094A58
		public bool HasFriendType
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x00096868 File Offset: 0x00094A68
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x00096870 File Offset: 0x00094A70
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._guildId = value;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x00096888 File Offset: 0x00094A88
		public bool HasGuildId
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x00096898 File Offset: 0x00094A98
		// (set) Token: 0x06001CAC RID: 7340 RVA: 0x000968A0 File Offset: 0x00094AA0
		public string guildName
		{
			get
			{
				return this._guildName;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._guildName = value;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x000968B8 File Offset: 0x00094AB8
		public bool HasGuildName
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x000968C8 File Offset: 0x00094AC8
		// (set) Token: 0x06001CAF RID: 7343 RVA: 0x000968D0 File Offset: 0x00094AD0
		public long friendScore
		{
			get
			{
				return this._friendScore;
			}
			set
			{
				this.has_field.set_field(11, true);
				this._friendScore = value;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x000968E8 File Offset: 0x00094AE8
		public bool HasFriendScore
		{
			get
			{
				return this.has_field.has_field(11);
			}
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x000968F8 File Offset: 0x00094AF8
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
					this.friendId = this.deserialize.read_integer();
					break;
				case 2:
					this.name = this.deserialize.read_string();
					break;
				case 3:
					this.level = this.deserialize.read_integer();
					break;
				case 4:
					this.profession = this.deserialize.read_integer();
					break;
				case 5:
					this.combValue = this.deserialize.read_integer();
					break;
				case 6:
					this.state = this.deserialize.read_integer();
					break;
				case 7:
					this.timeInfo = this.deserialize.read_integer();
					break;
				case 8:
					this.friendType = this.deserialize.read_integer();
					break;
				case 9:
					this.guildId = this.deserialize.read_integer();
					break;
				case 10:
					this.guildName = this.deserialize.read_string();
					break;
				case 11:
					this.friendScore = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00096A74 File Offset: 0x00094C74
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.characterId, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.friendId, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_string(this.name, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.level, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.profession, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.combValue, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.state, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.timeInfo, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.friendType, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_integer(this.guildId, 9);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_string(this.guildName, 10);
			}
			if (this.has_field.has_field(11))
			{
				this.serialize.write_integer(this.friendScore, 11);
			}
			return this.serialize.close();
		}

		// Token: 0x04001A5D RID: 6749
		private static int max_field_count = 12;

		// Token: 0x04001A5E RID: 6750
		private long _characterId;

		// Token: 0x04001A5F RID: 6751
		private long _friendId;

		// Token: 0x04001A60 RID: 6752
		private string _name;

		// Token: 0x04001A61 RID: 6753
		private long _level;

		// Token: 0x04001A62 RID: 6754
		private long _profession;

		// Token: 0x04001A63 RID: 6755
		private long _combValue;

		// Token: 0x04001A64 RID: 6756
		private long _state;

		// Token: 0x04001A65 RID: 6757
		private long _timeInfo;

		// Token: 0x04001A66 RID: 6758
		private long _friendType;

		// Token: 0x04001A67 RID: 6759
		private long _guildId;

		// Token: 0x04001A68 RID: 6760
		private string _guildName;

		// Token: 0x04001A69 RID: 6761
		private long _friendScore;
	}
}
