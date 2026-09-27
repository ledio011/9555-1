using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003DA RID: 986
	public class guild_info : SprotoTypeBase
	{
		// Token: 0x06001E4B RID: 7755 RVA: 0x0009A184 File Offset: 0x00098384
		public guild_info() : base(guild_info.max_field_count)
		{
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0009A194 File Offset: 0x00098394
		public guild_info(byte[] buffer) : base(guild_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001E4E RID: 7758 RVA: 0x0009A1B4 File Offset: 0x000983B4
		// (set) Token: 0x06001E4F RID: 7759 RVA: 0x0009A1BC File Offset: 0x000983BC
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

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001E50 RID: 7760 RVA: 0x0009A1D4 File Offset: 0x000983D4
		public bool HasGuildId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x0009A1E4 File Offset: 0x000983E4
		// (set) Token: 0x06001E52 RID: 7762 RVA: 0x0009A1EC File Offset: 0x000983EC
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

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x0009A204 File Offset: 0x00098404
		public bool HasGuildName
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x0009A214 File Offset: 0x00098414
		// (set) Token: 0x06001E55 RID: 7765 RVA: 0x0009A21C File Offset: 0x0009841C
		public string guildChiefName
		{
			get
			{
				return this._guildChiefName;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._guildChiefName = value;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x0009A234 File Offset: 0x00098434
		public bool HasGuildChiefName
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x0009A244 File Offset: 0x00098444
		// (set) Token: 0x06001E58 RID: 7768 RVA: 0x0009A24C File Offset: 0x0009844C
		public long guildChiefId
		{
			get
			{
				return this._guildChiefId;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._guildChiefId = value;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x0009A264 File Offset: 0x00098464
		public bool HasGuildChiefId
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0009A274 File Offset: 0x00098474
		// (set) Token: 0x06001E5B RID: 7771 RVA: 0x0009A27C File Offset: 0x0009847C
		public long guildExp
		{
			get
			{
				return this._guildExp;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._guildExp = value;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x0009A294 File Offset: 0x00098494
		public bool HasGuildExp
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x0009A2A4 File Offset: 0x000984A4
		// (set) Token: 0x06001E5E RID: 7774 RVA: 0x0009A2AC File Offset: 0x000984AC
		public long guildSkillPoint
		{
			get
			{
				return this._guildSkillPoint;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._guildSkillPoint = value;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x0009A2C4 File Offset: 0x000984C4
		public bool HasGuildSkillPoint
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x0009A2D4 File Offset: 0x000984D4
		// (set) Token: 0x06001E61 RID: 7777 RVA: 0x0009A2DC File Offset: 0x000984DC
		public long guildLevel
		{
			get
			{
				return this._guildLevel;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._guildLevel = value;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x0009A2F4 File Offset: 0x000984F4
		public bool HasGuildLevel
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x0009A304 File Offset: 0x00098504
		// (set) Token: 0x06001E64 RID: 7780 RVA: 0x0009A30C File Offset: 0x0009850C
		public long guildMemberNum
		{
			get
			{
				return this._guildMemberNum;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._guildMemberNum = value;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x0009A324 File Offset: 0x00098524
		public bool HasGuildMemberNum
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x0009A334 File Offset: 0x00098534
		// (set) Token: 0x06001E67 RID: 7783 RVA: 0x0009A33C File Offset: 0x0009853C
		public long guildCombo
		{
			get
			{
				return this._guildCombo;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._guildCombo = value;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x0009A354 File Offset: 0x00098554
		public bool HasGuildCombo
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x0009A364 File Offset: 0x00098564
		// (set) Token: 0x06001E6A RID: 7786 RVA: 0x0009A36C File Offset: 0x0009856C
		public long guildApplyNum
		{
			get
			{
				return this._guildApplyNum;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._guildApplyNum = value;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x0009A384 File Offset: 0x00098584
		public bool HasGuildApplyNum
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x0009A394 File Offset: 0x00098594
		// (set) Token: 0x06001E6D RID: 7789 RVA: 0x0009A39C File Offset: 0x0009859C
		public long guildApplyMaxNum
		{
			get
			{
				return this._guildApplyMaxNum;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._guildApplyMaxNum = value;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x0009A3B4 File Offset: 0x000985B4
		public bool HasGuildApplyMaxNum
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x0009A3C4 File Offset: 0x000985C4
		// (set) Token: 0x06001E70 RID: 7792 RVA: 0x0009A3CC File Offset: 0x000985CC
		public long guildMaxPlayer
		{
			get
			{
				return this._guildMaxPlayer;
			}
			set
			{
				this.has_field.set_field(11, true);
				this._guildMaxPlayer = value;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x0009A3E4 File Offset: 0x000985E4
		public bool HasGuildMaxPlayer
		{
			get
			{
				return this.has_field.has_field(11);
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x0009A3F4 File Offset: 0x000985F4
		// (set) Token: 0x06001E73 RID: 7795 RVA: 0x0009A3FC File Offset: 0x000985FC
		public string notice
		{
			get
			{
				return this._notice;
			}
			set
			{
				this.has_field.set_field(12, true);
				this._notice = value;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x0009A414 File Offset: 0x00098614
		public bool HasNotice
		{
			get
			{
				return this.has_field.has_field(12);
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x0009A424 File Offset: 0x00098624
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x0009A42C File Offset: 0x0009862C
		public bool isNeedAppro
		{
			get
			{
				return this._isNeedAppro;
			}
			set
			{
				this.has_field.set_field(13, true);
				this._isNeedAppro = value;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x0009A444 File Offset: 0x00098644
		public bool HasIsNeedAppro
		{
			get
			{
				return this.has_field.has_field(13);
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x0009A454 File Offset: 0x00098654
		// (set) Token: 0x06001E79 RID: 7801 RVA: 0x0009A45C File Offset: 0x0009865C
		public long createTime
		{
			get
			{
				return this._createTime;
			}
			set
			{
				this.has_field.set_field(14, true);
				this._createTime = value;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x0009A474 File Offset: 0x00098674
		public bool HasCreateTime
		{
			get
			{
				return this.has_field.has_field(14);
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x0009A484 File Offset: 0x00098684
		// (set) Token: 0x06001E7C RID: 7804 RVA: 0x0009A48C File Offset: 0x0009868C
		public bool guildBoss
		{
			get
			{
				return this._guildBoss;
			}
			set
			{
				this.has_field.set_field(15, true);
				this._guildBoss = value;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x0009A4A4 File Offset: 0x000986A4
		public bool HasGuildBoss
		{
			get
			{
				return this.has_field.has_field(15);
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x0009A4B4 File Offset: 0x000986B4
		// (set) Token: 0x06001E7F RID: 7807 RVA: 0x0009A4BC File Offset: 0x000986BC
		public bool guildBattle
		{
			get
			{
				return this._guildBattle;
			}
			set
			{
				this.has_field.set_field(16, true);
				this._guildBattle = value;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0009A4D4 File Offset: 0x000986D4
		public bool HasGuildBattle
		{
			get
			{
				return this.has_field.has_field(16);
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x0009A4E4 File Offset: 0x000986E4
		// (set) Token: 0x06001E82 RID: 7810 RVA: 0x0009A4EC File Offset: 0x000986EC
		public long viceNum
		{
			get
			{
				return this._viceNum;
			}
			set
			{
				this.has_field.set_field(17, true);
				this._viceNum = value;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x0009A504 File Offset: 0x00098704
		public bool HasViceNum
		{
			get
			{
				return this.has_field.has_field(17);
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001E84 RID: 7812 RVA: 0x0009A514 File Offset: 0x00098714
		// (set) Token: 0x06001E85 RID: 7813 RVA: 0x0009A51C File Offset: 0x0009871C
		public long elderNum
		{
			get
			{
				return this._elderNum;
			}
			set
			{
				this.has_field.set_field(18, true);
				this._elderNum = value;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x0009A534 File Offset: 0x00098734
		public bool HasElderNum
		{
			get
			{
				return this.has_field.has_field(18);
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0009A544 File Offset: 0x00098744
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x0009A54C File Offset: 0x0009874C
		public long playerJob
		{
			get
			{
				return this._playerJob;
			}
			set
			{
				this.has_field.set_field(19, true);
				this._playerJob = value;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0009A564 File Offset: 0x00098764
		public bool HasPlayerJob
		{
			get
			{
				return this.has_field.has_field(19);
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x0009A574 File Offset: 0x00098774
		// (set) Token: 0x06001E8B RID: 7819 RVA: 0x0009A57C File Offset: 0x0009877C
		public long icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this.has_field.set_field(20, true);
				this._icon = value;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0009A594 File Offset: 0x00098794
		public bool HasIcon
		{
			get
			{
				return this.has_field.has_field(20);
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x0009A5A4 File Offset: 0x000987A4
		// (set) Token: 0x06001E8E RID: 7822 RVA: 0x0009A5AC File Offset: 0x000987AC
		public long disactiveState
		{
			get
			{
				return this._disactiveState;
			}
			set
			{
				this.has_field.set_field(21, true);
				this._disactiveState = value;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x0009A5C4 File Offset: 0x000987C4
		public bool HasDisactiveState
		{
			get
			{
				return this.has_field.has_field(21);
			}
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0009A5D4 File Offset: 0x000987D4
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.guildId = this.deserialize.read_integer();
					continue;
				case 1:
					this.guildName = this.deserialize.read_string();
					continue;
				case 2:
					this.guildChiefName = this.deserialize.read_string();
					continue;
				case 3:
					this.guildChiefId = this.deserialize.read_integer();
					continue;
				case 4:
					this.guildExp = this.deserialize.read_integer();
					continue;
				case 5:
					this.guildSkillPoint = this.deserialize.read_integer();
					continue;
				case 6:
					this.guildLevel = this.deserialize.read_integer();
					continue;
				case 7:
					this.guildMemberNum = this.deserialize.read_integer();
					continue;
				case 8:
					this.guildCombo = this.deserialize.read_integer();
					continue;
				case 9:
					this.guildApplyNum = this.deserialize.read_integer();
					continue;
				case 10:
					this.guildApplyMaxNum = this.deserialize.read_integer();
					continue;
				case 11:
					this.guildMaxPlayer = this.deserialize.read_integer();
					continue;
				case 12:
					this.notice = this.deserialize.read_string();
					continue;
				case 13:
					this.isNeedAppro = this.deserialize.read_boolean();
					continue;
				case 14:
					this.createTime = this.deserialize.read_integer();
					continue;
				case 16:
					this.guildBoss = this.deserialize.read_boolean();
					continue;
				case 17:
					this.guildBattle = this.deserialize.read_boolean();
					continue;
				case 18:
					this.viceNum = this.deserialize.read_integer();
					continue;
				case 19:
					this.elderNum = this.deserialize.read_integer();
					continue;
				case 20:
					this.playerJob = this.deserialize.read_integer();
					continue;
				case 21:
					this.icon = this.deserialize.read_integer();
					continue;
				case 22:
					this.disactiveState = this.deserialize.read_integer();
					continue;
				}
				this.deserialize.read_unknow_data();
			}
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x0009A858 File Offset: 0x00098A58
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
				this.serialize.write_string(this.guildChiefName, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.guildChiefId, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.guildExp, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.guildSkillPoint, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.guildLevel, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.guildMemberNum, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.guildCombo, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_integer(this.guildApplyNum, 9);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_integer(this.guildApplyMaxNum, 10);
			}
			if (this.has_field.has_field(11))
			{
				this.serialize.write_integer(this.guildMaxPlayer, 11);
			}
			if (this.has_field.has_field(12))
			{
				this.serialize.write_string(this.notice, 12);
			}
			if (this.has_field.has_field(13))
			{
				this.serialize.write_boolean(this.isNeedAppro, 13);
			}
			if (this.has_field.has_field(14))
			{
				this.serialize.write_integer(this.createTime, 14);
			}
			if (this.has_field.has_field(15))
			{
				this.serialize.write_boolean(this.guildBoss, 16);
			}
			if (this.has_field.has_field(16))
			{
				this.serialize.write_boolean(this.guildBattle, 17);
			}
			if (this.has_field.has_field(17))
			{
				this.serialize.write_integer(this.viceNum, 18);
			}
			if (this.has_field.has_field(18))
			{
				this.serialize.write_integer(this.elderNum, 19);
			}
			if (this.has_field.has_field(19))
			{
				this.serialize.write_integer(this.playerJob, 20);
			}
			if (this.has_field.has_field(20))
			{
				this.serialize.write_integer(this.icon, 21);
			}
			if (this.has_field.has_field(21))
			{
				this.serialize.write_integer(this.disactiveState, 22);
			}
			return this.serialize.close();
		}

		// Token: 0x04001AE0 RID: 6880
		private static int max_field_count = 23;

		// Token: 0x04001AE1 RID: 6881
		private long _guildId;

		// Token: 0x04001AE2 RID: 6882
		private string _guildName;

		// Token: 0x04001AE3 RID: 6883
		private string _guildChiefName;

		// Token: 0x04001AE4 RID: 6884
		private long _guildChiefId;

		// Token: 0x04001AE5 RID: 6885
		private long _guildExp;

		// Token: 0x04001AE6 RID: 6886
		private long _guildSkillPoint;

		// Token: 0x04001AE7 RID: 6887
		private long _guildLevel;

		// Token: 0x04001AE8 RID: 6888
		private long _guildMemberNum;

		// Token: 0x04001AE9 RID: 6889
		private long _guildCombo;

		// Token: 0x04001AEA RID: 6890
		private long _guildApplyNum;

		// Token: 0x04001AEB RID: 6891
		private long _guildApplyMaxNum;

		// Token: 0x04001AEC RID: 6892
		private long _guildMaxPlayer;

		// Token: 0x04001AED RID: 6893
		private string _notice;

		// Token: 0x04001AEE RID: 6894
		private bool _isNeedAppro;

		// Token: 0x04001AEF RID: 6895
		private long _createTime;

		// Token: 0x04001AF0 RID: 6896
		private bool _guildBoss;

		// Token: 0x04001AF1 RID: 6897
		private bool _guildBattle;

		// Token: 0x04001AF2 RID: 6898
		private long _viceNum;

		// Token: 0x04001AF3 RID: 6899
		private long _elderNum;

		// Token: 0x04001AF4 RID: 6900
		private long _playerJob;

		// Token: 0x04001AF5 RID: 6901
		private long _icon;

		// Token: 0x04001AF6 RID: 6902
		private long _disactiveState;
	}
}
