using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000315 RID: 789
	public class attribute_other : SprotoTypeBase
	{
		// Token: 0x06001674 RID: 5748 RVA: 0x00089C28 File Offset: 0x00087E28
		public attribute_other() : base(attribute_other.max_field_count)
		{
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00089C38 File Offset: 0x00087E38
		public attribute_other(byte[] buffer) : base(attribute_other.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x00089C58 File Offset: 0x00087E58
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x00089C60 File Offset: 0x00087E60
		public long hp
		{
			get
			{
				return this._hp;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._hp = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x00089C78 File Offset: 0x00087E78
		public bool HasHp
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x00089C88 File Offset: 0x00087E88
		// (set) Token: 0x0600167B RID: 5755 RVA: 0x00089C90 File Offset: 0x00087E90
		public long exp
		{
			get
			{
				return this._exp;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._exp = value;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x00089CA8 File Offset: 0x00087EA8
		public bool HasExp
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x00089CB8 File Offset: 0x00087EB8
		// (set) Token: 0x0600167E RID: 5758 RVA: 0x00089CC0 File Offset: 0x00087EC0
		public long level
		{
			get
			{
				return this._level;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._level = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x00089CD8 File Offset: 0x00087ED8
		public bool HasLevel
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x00089CE8 File Offset: 0x00087EE8
		// (set) Token: 0x06001681 RID: 5761 RVA: 0x00089CF0 File Offset: 0x00087EF0
		public long combValue
		{
			get
			{
				return this._combValue;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._combValue = value;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x00089D08 File Offset: 0x00087F08
		public bool HasCombValue
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x00089D18 File Offset: 0x00087F18
		// (set) Token: 0x06001684 RID: 5764 RVA: 0x00089D20 File Offset: 0x00087F20
		public long title_level
		{
			get
			{
				return this._title_level;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._title_level = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x00089D38 File Offset: 0x00087F38
		public bool HasTitle_level
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00089D48 File Offset: 0x00087F48
		// (set) Token: 0x06001687 RID: 5767 RVA: 0x00089D50 File Offset: 0x00087F50
		public long title_exp
		{
			get
			{
				return this._title_exp;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._title_exp = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x00089D68 File Offset: 0x00087F68
		public bool HasTitle_exp
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x00089D78 File Offset: 0x00087F78
		// (set) Token: 0x0600168A RID: 5770 RVA: 0x00089D80 File Offset: 0x00087F80
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._guildId = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x00089D98 File Offset: 0x00087F98
		public bool HasGuildId
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x00089DA8 File Offset: 0x00087FA8
		// (set) Token: 0x0600168D RID: 5773 RVA: 0x00089DB0 File Offset: 0x00087FB0
		public long guildJob
		{
			get
			{
				return this._guildJob;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._guildJob = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x00089DC8 File Offset: 0x00087FC8
		public bool HasGuildJob
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x00089DD8 File Offset: 0x00087FD8
		// (set) Token: 0x06001690 RID: 5776 RVA: 0x00089DE0 File Offset: 0x00087FE0
		public string guildName
		{
			get
			{
				return this._guildName;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._guildName = value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x00089DF8 File Offset: 0x00087FF8
		public bool HasGuildName
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x00089E08 File Offset: 0x00088008
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x00089E10 File Offset: 0x00088010
		public long refineNeckLevel
		{
			get
			{
				return this._refineNeckLevel;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._refineNeckLevel = value;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x00089E28 File Offset: 0x00088028
		public bool HasRefineNeckLevel
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x00089E38 File Offset: 0x00088038
		// (set) Token: 0x06001696 RID: 5782 RVA: 0x00089E40 File Offset: 0x00088040
		public long refineRing1Level
		{
			get
			{
				return this._refineRing1Level;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._refineRing1Level = value;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x00089E58 File Offset: 0x00088058
		public bool HasRefineRing1Level
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00089E68 File Offset: 0x00088068
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x00089E70 File Offset: 0x00088070
		public long refineRing2Level
		{
			get
			{
				return this._refineRing2Level;
			}
			set
			{
				this.has_field.set_field(11, true);
				this._refineRing2Level = value;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x00089E88 File Offset: 0x00088088
		public bool HasRefineRing2Level
		{
			get
			{
				return this.has_field.has_field(11);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x00089E98 File Offset: 0x00088098
		// (set) Token: 0x0600169C RID: 5788 RVA: 0x00089EA0 File Offset: 0x000880A0
		public long refineBeltLevel
		{
			get
			{
				return this._refineBeltLevel;
			}
			set
			{
				this.has_field.set_field(12, true);
				this._refineBeltLevel = value;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x00089EB8 File Offset: 0x000880B8
		public bool HasRefineBeltLevel
		{
			get
			{
				return this.has_field.has_field(12);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00089EC8 File Offset: 0x000880C8
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x00089ED0 File Offset: 0x000880D0
		public long refineLevel
		{
			get
			{
				return this._refineLevel;
			}
			set
			{
				this.has_field.set_field(13, true);
				this._refineLevel = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00089EE8 File Offset: 0x000880E8
		public bool HasRefineLevel
		{
			get
			{
				return this.has_field.has_field(13);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00089EF8 File Offset: 0x000880F8
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x00089F00 File Offset: 0x00088100
		public long vip
		{
			get
			{
				return this._vip;
			}
			set
			{
				this.has_field.set_field(14, true);
				this._vip = value;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00089F18 File Offset: 0x00088118
		public bool HasVip
		{
			get
			{
				return this.has_field.has_field(14);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x00089F28 File Offset: 0x00088128
		// (set) Token: 0x060016A5 RID: 5797 RVA: 0x00089F30 File Offset: 0x00088130
		public long camp
		{
			get
			{
				return this._camp;
			}
			set
			{
				this.has_field.set_field(15, true);
				this._camp = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x00089F48 File Offset: 0x00088148
		public bool HasCamp
		{
			get
			{
				return this.has_field.has_field(15);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x00089F58 File Offset: 0x00088158
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x00089F60 File Offset: 0x00088160
		public long pkMode
		{
			get
			{
				return this._pkMode;
			}
			set
			{
				this.has_field.set_field(16, true);
				this._pkMode = value;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x00089F78 File Offset: 0x00088178
		public bool HasPkMode
		{
			get
			{
				return this.has_field.has_field(16);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x00089F88 File Offset: 0x00088188
		// (set) Token: 0x060016AB RID: 5803 RVA: 0x00089F90 File Offset: 0x00088190
		public long dance_state
		{
			get
			{
				return this._dance_state;
			}
			set
			{
				this.has_field.set_field(17, true);
				this._dance_state = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x00089FA8 File Offset: 0x000881A8
		public bool HasDance_state
		{
			get
			{
				return this.has_field.has_field(17);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00089FB8 File Offset: 0x000881B8
		// (set) Token: 0x060016AE RID: 5806 RVA: 0x00089FC0 File Offset: 0x000881C0
		public string dance_id
		{
			get
			{
				return this._dance_id;
			}
			set
			{
				this.has_field.set_field(18, true);
				this._dance_id = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x00089FD8 File Offset: 0x000881D8
		public bool HasDance_id
		{
			get
			{
				return this.has_field.has_field(18);
			}
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00089FE8 File Offset: 0x000881E8
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.hp = this.deserialize.read_integer();
					break;
				case 1:
					this.exp = this.deserialize.read_integer();
					break;
				case 2:
					this.level = this.deserialize.read_integer();
					break;
				case 3:
					this.combValue = this.deserialize.read_integer();
					break;
				case 4:
					this.title_level = this.deserialize.read_integer();
					break;
				case 5:
					this.title_exp = this.deserialize.read_integer();
					break;
				case 6:
					this.guildId = this.deserialize.read_integer();
					break;
				case 7:
					this.guildJob = this.deserialize.read_integer();
					break;
				case 8:
					this.guildName = this.deserialize.read_string();
					break;
				case 9:
					this.refineNeckLevel = this.deserialize.read_integer();
					break;
				case 10:
					this.refineRing1Level = this.deserialize.read_integer();
					break;
				case 11:
					this.refineRing2Level = this.deserialize.read_integer();
					break;
				case 12:
					this.refineBeltLevel = this.deserialize.read_integer();
					break;
				case 13:
					this.refineLevel = this.deserialize.read_integer();
					break;
				case 14:
					this.vip = this.deserialize.read_integer();
					break;
				case 15:
					this.camp = this.deserialize.read_integer();
					break;
				case 16:
					this.pkMode = this.deserialize.read_integer();
					break;
				case 17:
					this.dance_state = this.deserialize.read_integer();
					break;
				case 18:
					this.dance_id = this.deserialize.read_string();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0008A21C File Offset: 0x0008841C
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.hp, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.exp, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.level, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.combValue, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.title_level, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.title_exp, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.guildId, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.guildJob, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_string(this.guildName, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_integer(this.refineNeckLevel, 9);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_integer(this.refineRing1Level, 10);
			}
			if (this.has_field.has_field(11))
			{
				this.serialize.write_integer(this.refineRing2Level, 11);
			}
			if (this.has_field.has_field(12))
			{
				this.serialize.write_integer(this.refineBeltLevel, 12);
			}
			if (this.has_field.has_field(13))
			{
				this.serialize.write_integer(this.refineLevel, 13);
			}
			if (this.has_field.has_field(14))
			{
				this.serialize.write_integer(this.vip, 14);
			}
			if (this.has_field.has_field(15))
			{
				this.serialize.write_integer(this.camp, 15);
			}
			if (this.has_field.has_field(16))
			{
				this.serialize.write_integer(this.pkMode, 16);
			}
			if (this.has_field.has_field(17))
			{
				this.serialize.write_integer(this.dance_state, 17);
			}
			if (this.has_field.has_field(18))
			{
				this.serialize.write_string(this.dance_id, 18);
			}
			return this.serialize.close();
		}

		// Token: 0x040018A3 RID: 6307
		private static int max_field_count = 19;

		// Token: 0x040018A4 RID: 6308
		private long _hp;

		// Token: 0x040018A5 RID: 6309
		private long _exp;

		// Token: 0x040018A6 RID: 6310
		private long _level;

		// Token: 0x040018A7 RID: 6311
		private long _combValue;

		// Token: 0x040018A8 RID: 6312
		private long _title_level;

		// Token: 0x040018A9 RID: 6313
		private long _title_exp;

		// Token: 0x040018AA RID: 6314
		private long _guildId;

		// Token: 0x040018AB RID: 6315
		private long _guildJob;

		// Token: 0x040018AC RID: 6316
		private string _guildName;

		// Token: 0x040018AD RID: 6317
		private long _refineNeckLevel;

		// Token: 0x040018AE RID: 6318
		private long _refineRing1Level;

		// Token: 0x040018AF RID: 6319
		private long _refineRing2Level;

		// Token: 0x040018B0 RID: 6320
		private long _refineBeltLevel;

		// Token: 0x040018B1 RID: 6321
		private long _refineLevel;

		// Token: 0x040018B2 RID: 6322
		private long _vip;

		// Token: 0x040018B3 RID: 6323
		private long _camp;

		// Token: 0x040018B4 RID: 6324
		private long _pkMode;

		// Token: 0x040018B5 RID: 6325
		private long _dance_state;

		// Token: 0x040018B6 RID: 6326
		private string _dance_id;
	}
}
