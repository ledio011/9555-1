using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000346 RID: 838
	public class character_look : SprotoTypeBase
	{
		// Token: 0x06001870 RID: 6256 RVA: 0x0008DEA8 File Offset: 0x0008C0A8
		public character_look() : base(character_look.max_field_count)
		{
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0008DEB8 File Offset: 0x0008C0B8
		public character_look(byte[] buffer) : base(character_look.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x0008DED8 File Offset: 0x0008C0D8
		// (set) Token: 0x06001874 RID: 6260 RVA: 0x0008DEE0 File Offset: 0x0008C0E0
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

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x0008DEF8 File Offset: 0x0008C0F8
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0008DF08 File Offset: 0x0008C108
		// (set) Token: 0x06001877 RID: 6263 RVA: 0x0008DF10 File Offset: 0x0008C110
		public general general
		{
			get
			{
				return this._general;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._general = value;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0008DF28 File Offset: 0x0008C128
		public bool HasGeneral
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x0008DF38 File Offset: 0x0008C138
		// (set) Token: 0x0600187A RID: 6266 RVA: 0x0008DF40 File Offset: 0x0008C140
		public attribute attribute
		{
			get
			{
				return this._attribute;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._attribute = value;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x0008DF58 File Offset: 0x0008C158
		public bool HasAttribute
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x0008DF68 File Offset: 0x0008C168
		// (set) Token: 0x0600187D RID: 6269 RVA: 0x0008DF70 File Offset: 0x0008C170
		public attribute_other attribute_other
		{
			get
			{
				return this._attribute_other;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._attribute_other = value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0008DF88 File Offset: 0x0008C188
		public bool HasAttribute_other
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0008DF98 File Offset: 0x0008C198
		// (set) Token: 0x06001880 RID: 6272 RVA: 0x0008DFA0 File Offset: 0x0008C1A0
		public characterVisual visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._visual = value;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0008DFB8 File Offset: 0x0008C1B8
		public bool HasVisual
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0008DFC8 File Offset: 0x0008C1C8
		// (set) Token: 0x06001883 RID: 6275 RVA: 0x0008DFD0 File Offset: 0x0008C1D0
		public Dictionary<long, gameitem> equip
		{
			get
			{
				return this._equip;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._equip = value;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0008DFE8 File Offset: 0x0008C1E8
		public bool HasEquip
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0008DFF8 File Offset: 0x0008C1F8
		// (set) Token: 0x06001886 RID: 6278 RVA: 0x0008E000 File Offset: 0x0008C200
		public Dictionary<long, gameitem> fashion_equip
		{
			get
			{
				return this._fashion_equip;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._fashion_equip = value;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x0008E018 File Offset: 0x0008C218
		public bool HasFashion_equip
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x0008E028 File Offset: 0x0008C228
		// (set) Token: 0x06001889 RID: 6281 RVA: 0x0008E030 File Offset: 0x0008C230
		public Dictionary<long, gameitem> badge_equip
		{
			get
			{
				return this._badge_equip;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._badge_equip = value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x0008E048 File Offset: 0x0008C248
		public bool HasBadge_equip
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0008E058 File Offset: 0x0008C258
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x0008E060 File Offset: 0x0008C260
		public Dictionary<long, enhance_info> equip_enhance
		{
			get
			{
				return this._equip_enhance;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._equip_enhance = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0008E078 File Offset: 0x0008C278
		public bool HasEquip_enhance
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x0008E088 File Offset: 0x0008C288
		// (set) Token: 0x0600188F RID: 6287 RVA: 0x0008E090 File Offset: 0x0008C290
		public movement movement
		{
			get
			{
				return this._movement;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._movement = value;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x0008E0A8 File Offset: 0x0008C2A8
		public bool HasMovement
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0008E0B8 File Offset: 0x0008C2B8
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x0008E0C0 File Offset: 0x0008C2C0
		public Dictionary<string, skill_info> skills
		{
			get
			{
				return this._skills;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._skills = value;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0008E0D8 File Offset: 0x0008C2D8
		public bool HasSkills
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0008E0E8 File Offset: 0x0008C2E8
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
					this.general = this.deserialize.read_obj<general>();
					break;
				case 2:
					this.attribute = this.deserialize.read_obj<attribute>();
					break;
				case 3:
					this.attribute_other = this.deserialize.read_obj<attribute_other>();
					break;
				case 4:
					this.visual = this.deserialize.read_obj<characterVisual>();
					break;
				case 5:
					this.equip = this.deserialize.read_map<long, gameitem>((gameitem v) => v.indexId);
					break;
				case 6:
					this.fashion_equip = this.deserialize.read_map<long, gameitem>((gameitem v) => v.indexId);
					break;
				case 7:
					this.badge_equip = this.deserialize.read_map<long, gameitem>((gameitem v) => v.indexId);
					break;
				case 8:
					this.equip_enhance = this.deserialize.read_map<long, enhance_info>((enhance_info v) => v.subType);
					break;
				case 9:
					this.movement = this.deserialize.read_obj<movement>();
					break;
				case 10:
					this.skills = this.deserialize.read_map<string, skill_info>((skill_info v) => v.skillId);
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0008E2DC File Offset: 0x0008C4DC
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_obj(this.general, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_obj(this.attribute, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_obj(this.attribute_other, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_obj(this.visual, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_obj<long, gameitem>(this.equip, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_obj<long, gameitem>(this.fashion_equip, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_obj<long, gameitem>(this.badge_equip, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_obj<long, enhance_info>(this.equip_enhance, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_obj(this.movement, 9);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_obj<string, skill_info>(this.skills, 10);
			}
			return this.serialize.close();
		}

		// Token: 0x04001936 RID: 6454
		private static int max_field_count = 11;

		// Token: 0x04001937 RID: 6455
		private long _id;

		// Token: 0x04001938 RID: 6456
		private general _general;

		// Token: 0x04001939 RID: 6457
		private attribute _attribute;

		// Token: 0x0400193A RID: 6458
		private attribute_other _attribute_other;

		// Token: 0x0400193B RID: 6459
		private characterVisual _visual;

		// Token: 0x0400193C RID: 6460
		private Dictionary<long, gameitem> _equip;

		// Token: 0x0400193D RID: 6461
		private Dictionary<long, gameitem> _fashion_equip;

		// Token: 0x0400193E RID: 6462
		private Dictionary<long, gameitem> _badge_equip;

		// Token: 0x0400193F RID: 6463
		private Dictionary<long, enhance_info> _equip_enhance;

		// Token: 0x04001940 RID: 6464
		private movement _movement;

		// Token: 0x04001941 RID: 6465
		private Dictionary<string, skill_info> _skills;
	}
}
