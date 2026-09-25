using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200061B RID: 1563
	public class teammember : SprotoTypeBase
	{
		// Token: 0x06002D51 RID: 11601 RVA: 0x000B7AB4 File Offset: 0x000B5CB4
		public teammember() : base(teammember.max_field_count)
		{
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000B7AC4 File Offset: 0x000B5CC4
		public teammember(byte[] buffer) : base(teammember.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06002D54 RID: 11604 RVA: 0x000B7AE4 File Offset: 0x000B5CE4
		// (set) Token: 0x06002D55 RID: 11605 RVA: 0x000B7AEC File Offset: 0x000B5CEC
		public long id
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

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x000B7B04 File Offset: 0x000B5D04
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x000B7B14 File Offset: 0x000B5D14
		// (set) Token: 0x06002D58 RID: 11608 RVA: 0x000B7B1C File Offset: 0x000B5D1C
		public long teamid
		{
			get
			{
				return this._teamid;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._teamid = value;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x000B7B34 File Offset: 0x000B5D34
		public bool HasTeamid
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x000B7B44 File Offset: 0x000B5D44
		// (set) Token: 0x06002D5B RID: 11611 RVA: 0x000B7B4C File Offset: 0x000B5D4C
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

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000B7B64 File Offset: 0x000B5D64
		public bool HasName
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x000B7B74 File Offset: 0x000B5D74
		// (set) Token: 0x06002D5E RID: 11614 RVA: 0x000B7B7C File Offset: 0x000B5D7C
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

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x000B7B94 File Offset: 0x000B5D94
		public bool HasLevel
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000B7BA4 File Offset: 0x000B5DA4
		// (set) Token: 0x06002D61 RID: 11617 RVA: 0x000B7BAC File Offset: 0x000B5DAC
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

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x000B7BC4 File Offset: 0x000B5DC4
		public bool HasProfession
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06002D63 RID: 11619 RVA: 0x000B7BD4 File Offset: 0x000B5DD4
		// (set) Token: 0x06002D64 RID: 11620 RVA: 0x000B7BDC File Offset: 0x000B5DDC
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

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x000B7BF4 File Offset: 0x000B5DF4
		public bool HasCombValue
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x000B7C04 File Offset: 0x000B5E04
		// (set) Token: 0x06002D67 RID: 11623 RVA: 0x000B7C0C File Offset: 0x000B5E0C
		public long mapInfoId
		{
			get
			{
				return this._mapInfoId;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._mapInfoId = value;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06002D68 RID: 11624 RVA: 0x000B7C24 File Offset: 0x000B5E24
		public bool HasMapInfoId
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x000B7C34 File Offset: 0x000B5E34
		// (set) Token: 0x06002D6A RID: 11626 RVA: 0x000B7C3C File Offset: 0x000B5E3C
		public long lineIndex
		{
			get
			{
				return this._lineIndex;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._lineIndex = value;
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06002D6B RID: 11627 RVA: 0x000B7C54 File Offset: 0x000B5E54
		public bool HasLineIndex
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x000B7C64 File Offset: 0x000B5E64
		// (set) Token: 0x06002D6D RID: 11629 RVA: 0x000B7C6C File Offset: 0x000B5E6C
		public long memberType
		{
			get
			{
				return this._memberType;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._memberType = value;
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x000B7C84 File Offset: 0x000B5E84
		public bool HasMemberType
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x000B7C94 File Offset: 0x000B5E94
		// (set) Token: 0x06002D70 RID: 11632 RVA: 0x000B7C9C File Offset: 0x000B5E9C
		public long hp
		{
			get
			{
				return this._hp;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._hp = value;
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x000B7CB4 File Offset: 0x000B5EB4
		public bool HasHp
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x000B7CC4 File Offset: 0x000B5EC4
		// (set) Token: 0x06002D73 RID: 11635 RVA: 0x000B7CCC File Offset: 0x000B5ECC
		public long max_hp
		{
			get
			{
				return this._max_hp;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._max_hp = value;
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x000B7CE4 File Offset: 0x000B5EE4
		public bool HasMax_hp
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x000B7CF4 File Offset: 0x000B5EF4
		// (set) Token: 0x06002D76 RID: 11638 RVA: 0x000B7CFC File Offset: 0x000B5EFC
		public long vip
		{
			get
			{
				return this._vip;
			}
			set
			{
				this.has_field.set_field(11, true);
				this._vip = value;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x000B7D14 File Offset: 0x000B5F14
		public bool HasVip
		{
			get
			{
				return this.has_field.has_field(11);
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x000B7D24 File Offset: 0x000B5F24
		// (set) Token: 0x06002D79 RID: 11641 RVA: 0x000B7D2C File Offset: 0x000B5F2C
		public long time
		{
			get
			{
				return this._time;
			}
			set
			{
				this.has_field.set_field(12, true);
				this._time = value;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000B7D44 File Offset: 0x000B5F44
		public bool HasTime
		{
			get
			{
				return this.has_field.has_field(12);
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06002D7B RID: 11643 RVA: 0x000B7D54 File Offset: 0x000B5F54
		// (set) Token: 0x06002D7C RID: 11644 RVA: 0x000B7D5C File Offset: 0x000B5F5C
		public long apply_time
		{
			get
			{
				return this._apply_time;
			}
			set
			{
				this.has_field.set_field(13, true);
				this._apply_time = value;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06002D7D RID: 11645 RVA: 0x000B7D74 File Offset: 0x000B5F74
		public bool HasApply_time
		{
			get
			{
				return this.has_field.has_field(13);
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x000B7D84 File Offset: 0x000B5F84
		// (set) Token: 0x06002D7F RID: 11647 RVA: 0x000B7D8C File Offset: 0x000B5F8C
		public characterVisual visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				this.has_field.set_field(14, true);
				this._visual = value;
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x000B7DA4 File Offset: 0x000B5FA4
		public bool HasVisual
		{
			get
			{
				return this.has_field.has_field(14);
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x000B7DB4 File Offset: 0x000B5FB4
		// (set) Token: 0x06002D82 RID: 11650 RVA: 0x000B7DBC File Offset: 0x000B5FBC
		public long curNum
		{
			get
			{
				return this._curNum;
			}
			set
			{
				this.has_field.set_field(15, true);
				this._curNum = value;
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06002D83 RID: 11651 RVA: 0x000B7DD4 File Offset: 0x000B5FD4
		public bool HasCurNum
		{
			get
			{
				return this.has_field.has_field(15);
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x000B7DE4 File Offset: 0x000B5FE4
		// (set) Token: 0x06002D85 RID: 11653 RVA: 0x000B7DEC File Offset: 0x000B5FEC
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this.has_field.set_field(16, true);
				this._guildId = value;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x000B7E04 File Offset: 0x000B6004
		public bool HasGuildId
		{
			get
			{
				return this.has_field.has_field(16);
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x000B7E14 File Offset: 0x000B6014
		// (set) Token: 0x06002D88 RID: 11656 RVA: 0x000B7E1C File Offset: 0x000B601C
		public string guildName
		{
			get
			{
				return this._guildName;
			}
			set
			{
				this.has_field.set_field(17, true);
				this._guildName = value;
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x000B7E34 File Offset: 0x000B6034
		public bool HasGuildName
		{
			get
			{
				return this.has_field.has_field(17);
			}
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x000B7E44 File Offset: 0x000B6044
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.id = this.deserialize.read_integer();
					break;
				case 1:
					this.teamid = this.deserialize.read_integer();
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
					this.mapInfoId = this.deserialize.read_integer();
					break;
				case 7:
					this.lineIndex = this.deserialize.read_integer();
					break;
				case 8:
					this.memberType = this.deserialize.read_integer();
					break;
				case 9:
					this.hp = this.deserialize.read_integer();
					break;
				case 10:
					this.max_hp = this.deserialize.read_integer();
					break;
				case 11:
					this.vip = this.deserialize.read_integer();
					break;
				case 12:
					this.time = this.deserialize.read_integer();
					break;
				case 13:
					this.apply_time = this.deserialize.read_integer();
					break;
				case 14:
					this.visual = this.deserialize.read_obj<characterVisual>();
					break;
				case 15:
					this.curNum = this.deserialize.read_integer();
					break;
				case 16:
					this.guildId = this.deserialize.read_integer();
					break;
				case 17:
					this.guildName = this.deserialize.read_string();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000B805C File Offset: 0x000B625C
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.teamid, 1);
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
				this.serialize.write_integer(this.mapInfoId, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.lineIndex, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.memberType, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_integer(this.hp, 9);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_integer(this.max_hp, 10);
			}
			if (this.has_field.has_field(11))
			{
				this.serialize.write_integer(this.vip, 11);
			}
			if (this.has_field.has_field(12))
			{
				this.serialize.write_integer(this.time, 12);
			}
			if (this.has_field.has_field(13))
			{
				this.serialize.write_integer(this.apply_time, 13);
			}
			if (this.has_field.has_field(14))
			{
				this.serialize.write_obj(this.visual, 14);
			}
			if (this.has_field.has_field(15))
			{
				this.serialize.write_integer(this.curNum, 15);
			}
			if (this.has_field.has_field(16))
			{
				this.serialize.write_integer(this.guildId, 16);
			}
			if (this.has_field.has_field(17))
			{
				this.serialize.write_string(this.guildName, 17);
			}
			return this.serialize.close();
		}

		// Token: 0x04001EDE RID: 7902
		private static int max_field_count = 18;

		// Token: 0x04001EDF RID: 7903
		private long _id;

		// Token: 0x04001EE0 RID: 7904
		private long _teamid;

		// Token: 0x04001EE1 RID: 7905
		private string _name;

		// Token: 0x04001EE2 RID: 7906
		private long _level;

		// Token: 0x04001EE3 RID: 7907
		private long _profession;

		// Token: 0x04001EE4 RID: 7908
		private long _combValue;

		// Token: 0x04001EE5 RID: 7909
		private long _mapInfoId;

		// Token: 0x04001EE6 RID: 7910
		private long _lineIndex;

		// Token: 0x04001EE7 RID: 7911
		private long _memberType;

		// Token: 0x04001EE8 RID: 7912
		private long _hp;

		// Token: 0x04001EE9 RID: 7913
		private long _max_hp;

		// Token: 0x04001EEA RID: 7914
		private long _vip;

		// Token: 0x04001EEB RID: 7915
		private long _time;

		// Token: 0x04001EEC RID: 7916
		private long _apply_time;

		// Token: 0x04001EED RID: 7917
		private characterVisual _visual;

		// Token: 0x04001EEE RID: 7918
		private long _curNum;

		// Token: 0x04001EEF RID: 7919
		private long _guildId;

		// Token: 0x04001EF0 RID: 7920
		private string _guildName;
	}
}
