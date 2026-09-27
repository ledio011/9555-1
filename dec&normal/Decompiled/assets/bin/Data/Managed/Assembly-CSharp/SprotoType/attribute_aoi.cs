using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000312 RID: 786
	public class attribute_aoi : SprotoTypeBase
	{
		// Token: 0x0600163C RID: 5692 RVA: 0x0008947C File Offset: 0x0008767C
		public attribute_aoi() : base(attribute_aoi.max_field_count)
		{
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x0008948C File Offset: 0x0008768C
		public attribute_aoi(byte[] buffer) : base(attribute_aoi.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x000894AC File Offset: 0x000876AC
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x000894B4 File Offset: 0x000876B4
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

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x000894CC File Offset: 0x000876CC
		public bool HasHp
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x000894DC File Offset: 0x000876DC
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x000894E4 File Offset: 0x000876E4
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

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x000894FC File Offset: 0x000876FC
		public bool HasExp
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0008950C File Offset: 0x0008770C
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x00089514 File Offset: 0x00087714
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

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x0008952C File Offset: 0x0008772C
		public bool HasLevel
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0008953C File Offset: 0x0008773C
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x00089544 File Offset: 0x00087744
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

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0008955C File Offset: 0x0008775C
		public bool HasCombValue
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0008956C File Offset: 0x0008776C
		// (set) Token: 0x0600164C RID: 5708 RVA: 0x00089574 File Offset: 0x00087774
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

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0008958C File Offset: 0x0008778C
		public bool HasTitle_level
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0008959C File Offset: 0x0008779C
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x000895A4 File Offset: 0x000877A4
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

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x000895BC File Offset: 0x000877BC
		public bool HasTitle_exp
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x000895CC File Offset: 0x000877CC
		// (set) Token: 0x06001652 RID: 5714 RVA: 0x000895D4 File Offset: 0x000877D4
		public long refineNeckLevel
		{
			get
			{
				return this._refineNeckLevel;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._refineNeckLevel = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x000895EC File Offset: 0x000877EC
		public bool HasRefineNeckLevel
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x000895FC File Offset: 0x000877FC
		// (set) Token: 0x06001655 RID: 5717 RVA: 0x00089604 File Offset: 0x00087804
		public long refineRing1Level
		{
			get
			{
				return this._refineRing1Level;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._refineRing1Level = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0008961C File Offset: 0x0008781C
		public bool HasRefineRing1Level
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0008962C File Offset: 0x0008782C
		// (set) Token: 0x06001658 RID: 5720 RVA: 0x00089634 File Offset: 0x00087834
		public long refineRing2Level
		{
			get
			{
				return this._refineRing2Level;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._refineRing2Level = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0008964C File Offset: 0x0008784C
		public bool HasRefineRing2Level
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0008965C File Offset: 0x0008785C
		// (set) Token: 0x0600165B RID: 5723 RVA: 0x00089664 File Offset: 0x00087864
		public long refineBeltLevel
		{
			get
			{
				return this._refineBeltLevel;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._refineBeltLevel = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x0008967C File Offset: 0x0008787C
		public bool HasRefineBeltLevel
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x0008968C File Offset: 0x0008788C
		// (set) Token: 0x0600165E RID: 5726 RVA: 0x00089694 File Offset: 0x00087894
		public long refineLevel
		{
			get
			{
				return this._refineLevel;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._refineLevel = value;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x000896AC File Offset: 0x000878AC
		public bool HasRefineLevel
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000896BC File Offset: 0x000878BC
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.hp = this.deserialize.read_integer();
					continue;
				case 1:
					this.exp = this.deserialize.read_integer();
					continue;
				case 2:
					this.level = this.deserialize.read_integer();
					continue;
				case 3:
					this.combValue = this.deserialize.read_integer();
					continue;
				case 4:
					this.title_level = this.deserialize.read_integer();
					continue;
				case 5:
					this.title_exp = this.deserialize.read_integer();
					continue;
				case 9:
					this.refineNeckLevel = this.deserialize.read_integer();
					continue;
				case 10:
					this.refineRing1Level = this.deserialize.read_integer();
					continue;
				case 11:
					this.refineRing2Level = this.deserialize.read_integer();
					continue;
				case 12:
					this.refineBeltLevel = this.deserialize.read_integer();
					continue;
				case 13:
					this.refineLevel = this.deserialize.read_integer();
					continue;
				}
				this.deserialize.read_unknow_data();
			}
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0008982C File Offset: 0x00087A2C
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
				this.serialize.write_integer(this.refineNeckLevel, 9);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.refineRing1Level, 10);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.refineRing2Level, 11);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_integer(this.refineBeltLevel, 12);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_integer(this.refineLevel, 13);
			}
			return this.serialize.close();
		}

		// Token: 0x04001892 RID: 6290
		private static int max_field_count = 12;

		// Token: 0x04001893 RID: 6291
		private long _hp;

		// Token: 0x04001894 RID: 6292
		private long _exp;

		// Token: 0x04001895 RID: 6293
		private long _level;

		// Token: 0x04001896 RID: 6294
		private long _combValue;

		// Token: 0x04001897 RID: 6295
		private long _title_level;

		// Token: 0x04001898 RID: 6296
		private long _title_exp;

		// Token: 0x04001899 RID: 6297
		private long _refineNeckLevel;

		// Token: 0x0400189A RID: 6298
		private long _refineRing1Level;

		// Token: 0x0400189B RID: 6299
		private long _refineRing2Level;

		// Token: 0x0400189C RID: 6300
		private long _refineBeltLevel;

		// Token: 0x0400189D RID: 6301
		private long _refineLevel;
	}
}
