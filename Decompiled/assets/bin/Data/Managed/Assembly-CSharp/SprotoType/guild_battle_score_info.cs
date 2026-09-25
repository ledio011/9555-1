using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003D1 RID: 977
	public class guild_battle_score_info : SprotoTypeBase
	{
		// Token: 0x06001DE2 RID: 7650 RVA: 0x000993C8 File Offset: 0x000975C8
		public guild_battle_score_info() : base(guild_battle_score_info.max_field_count)
		{
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000993D8 File Offset: 0x000975D8
		public guild_battle_score_info(byte[] buffer) : base(guild_battle_score_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x000993F8 File Offset: 0x000975F8
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x00099400 File Offset: 0x00097600
		public List<guild_battle_item_info> item_info
		{
			get
			{
				return this._item_info;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._item_info = value;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x00099418 File Offset: 0x00097618
		public bool HasItem_info
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x00099428 File Offset: 0x00097628
		// (set) Token: 0x06001DE9 RID: 7657 RVA: 0x00099430 File Offset: 0x00097630
		public long score1
		{
			get
			{
				return this._score1;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._score1 = value;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x00099448 File Offset: 0x00097648
		public bool HasScore1
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x00099458 File Offset: 0x00097658
		// (set) Token: 0x06001DEC RID: 7660 RVA: 0x00099460 File Offset: 0x00097660
		public long score2
		{
			get
			{
				return this._score2;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._score2 = value;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x00099478 File Offset: 0x00097678
		public bool HasScore2
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x00099488 File Offset: 0x00097688
		// (set) Token: 0x06001DEF RID: 7663 RVA: 0x00099490 File Offset: 0x00097690
		public long selfKillNum
		{
			get
			{
				return this._selfKillNum;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._selfKillNum = value;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x000994A8 File Offset: 0x000976A8
		public bool HasSelfKillNum
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x000994B8 File Offset: 0x000976B8
		// (set) Token: 0x06001DF2 RID: 7666 RVA: 0x000994C0 File Offset: 0x000976C0
		public long selfScore
		{
			get
			{
				return this._selfScore;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._selfScore = value;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x000994D8 File Offset: 0x000976D8
		public bool HasSelfScore
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x000994E8 File Offset: 0x000976E8
		// (set) Token: 0x06001DF5 RID: 7669 RVA: 0x000994F0 File Offset: 0x000976F0
		public string guildName1
		{
			get
			{
				return this._guildName1;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._guildName1 = value;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x00099508 File Offset: 0x00097708
		public bool HasGuildName1
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x00099518 File Offset: 0x00097718
		// (set) Token: 0x06001DF8 RID: 7672 RVA: 0x00099520 File Offset: 0x00097720
		public string guildName2
		{
			get
			{
				return this._guildName2;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._guildName2 = value;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x00099538 File Offset: 0x00097738
		public bool HasGuildName2
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x00099548 File Offset: 0x00097748
		// (set) Token: 0x06001DFB RID: 7675 RVA: 0x00099550 File Offset: 0x00097750
		public long win
		{
			get
			{
				return this._win;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._win = value;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x00099568 File Offset: 0x00097768
		public bool HasWin
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x00099578 File Offset: 0x00097778
		// (set) Token: 0x06001DFE RID: 7678 RVA: 0x00099580 File Offset: 0x00097780
		public long guildIcon1
		{
			get
			{
				return this._guildIcon1;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._guildIcon1 = value;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x00099598 File Offset: 0x00097798
		public bool HasGuildIcon1
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x000995A8 File Offset: 0x000977A8
		// (set) Token: 0x06001E01 RID: 7681 RVA: 0x000995B0 File Offset: 0x000977B0
		public long guildIcon2
		{
			get
			{
				return this._guildIcon2;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._guildIcon2 = value;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x000995C8 File Offset: 0x000977C8
		public bool HasGuildIcon2
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000995D8 File Offset: 0x000977D8
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.item_info = this.deserialize.read_obj_list<guild_battle_item_info>();
					break;
				case 1:
					this.score1 = this.deserialize.read_integer();
					break;
				case 2:
					this.score2 = this.deserialize.read_integer();
					break;
				case 3:
					this.selfKillNum = this.deserialize.read_integer();
					break;
				case 4:
					this.selfScore = this.deserialize.read_integer();
					break;
				case 5:
					this.guildName1 = this.deserialize.read_string();
					break;
				case 6:
					this.guildName2 = this.deserialize.read_string();
					break;
				case 7:
					this.win = this.deserialize.read_integer();
					break;
				case 8:
					this.guildIcon1 = this.deserialize.read_integer();
					break;
				case 9:
					this.guildIcon2 = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x00099720 File Offset: 0x00097920
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_obj<guild_battle_item_info>(this.item_info, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.score1, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.score2, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.selfKillNum, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.selfScore, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_string(this.guildName1, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_string(this.guildName2, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.win, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.guildIcon1, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_integer(this.guildIcon2, 9);
			}
			return this.serialize.close();
		}

		// Token: 0x04001AC2 RID: 6850
		private static int max_field_count = 10;

		// Token: 0x04001AC3 RID: 6851
		private List<guild_battle_item_info> _item_info;

		// Token: 0x04001AC4 RID: 6852
		private long _score1;

		// Token: 0x04001AC5 RID: 6853
		private long _score2;

		// Token: 0x04001AC6 RID: 6854
		private long _selfKillNum;

		// Token: 0x04001AC7 RID: 6855
		private long _selfScore;

		// Token: 0x04001AC8 RID: 6856
		private string _guildName1;

		// Token: 0x04001AC9 RID: 6857
		private string _guildName2;

		// Token: 0x04001ACA RID: 6858
		private long _win;

		// Token: 0x04001ACB RID: 6859
		private long _guildIcon1;

		// Token: 0x04001ACC RID: 6860
		private long _guildIcon2;
	}
}
