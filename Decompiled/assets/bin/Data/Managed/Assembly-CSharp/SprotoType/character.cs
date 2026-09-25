using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200033C RID: 828
	public class character : SprotoTypeBase
	{
		// Token: 0x060017AA RID: 6058 RVA: 0x0008C390 File Offset: 0x0008A590
		public character() : base(character.max_field_count)
		{
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0008C3A0 File Offset: 0x0008A5A0
		public character(byte[] buffer) : base(character.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x0008C3C0 File Offset: 0x0008A5C0
		// (set) Token: 0x060017AE RID: 6062 RVA: 0x0008C3C8 File Offset: 0x0008A5C8
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

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x0008C3E0 File Offset: 0x0008A5E0
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x0008C3F0 File Offset: 0x0008A5F0
		// (set) Token: 0x060017B1 RID: 6065 RVA: 0x0008C3F8 File Offset: 0x0008A5F8
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

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x0008C410 File Offset: 0x0008A610
		public bool HasGeneral
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0008C420 File Offset: 0x0008A620
		// (set) Token: 0x060017B4 RID: 6068 RVA: 0x0008C428 File Offset: 0x0008A628
		public attribute_other attribute_other
		{
			get
			{
				return this._attribute_other;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._attribute_other = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0008C440 File Offset: 0x0008A640
		public bool HasAttribute_other
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0008C450 File Offset: 0x0008A650
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x0008C458 File Offset: 0x0008A658
		public property property
		{
			get
			{
				return this._property;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._property = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0008C470 File Offset: 0x0008A670
		public bool HasProperty
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x0008C480 File Offset: 0x0008A680
		// (set) Token: 0x060017BA RID: 6074 RVA: 0x0008C488 File Offset: 0x0008A688
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

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x0008C4A0 File Offset: 0x0008A6A0
		public bool HasVisual
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0008C4B0 File Offset: 0x0008A6B0
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x0008C4B8 File Offset: 0x0008A6B8
		public movement movement
		{
			get
			{
				return this._movement;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._movement = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0008C4D0 File Offset: 0x0008A6D0
		public bool HasMovement
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x0008C4E0 File Offset: 0x0008A6E0
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x0008C4E8 File Offset: 0x0008A6E8
		public Dictionary<string, skill_info> skills
		{
			get
			{
				return this._skills;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._skills = value;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x0008C500 File Offset: 0x0008A700
		public bool HasSkills
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x0008C510 File Offset: 0x0008A710
		// (set) Token: 0x060017C3 RID: 6083 RVA: 0x0008C518 File Offset: 0x0008A718
		public Dictionary<long, gameitem> equip
		{
			get
			{
				return this._equip;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._equip = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x0008C530 File Offset: 0x0008A730
		public bool HasEquip
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x0008C540 File Offset: 0x0008A740
		// (set) Token: 0x060017C6 RID: 6086 RVA: 0x0008C548 File Offset: 0x0008A748
		public Dictionary<long, gameitem> badge_equip
		{
			get
			{
				return this._badge_equip;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._badge_equip = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x0008C560 File Offset: 0x0008A760
		public bool HasBadge_equip
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0008C570 File Offset: 0x0008A770
		// (set) Token: 0x060017C9 RID: 6089 RVA: 0x0008C578 File Offset: 0x0008A778
		public Dictionary<long, gameitem> fashion_equip
		{
			get
			{
				return this._fashion_equip;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._fashion_equip = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0008C590 File Offset: 0x0008A790
		public bool HasFashion_equip
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x0008C5A0 File Offset: 0x0008A7A0
		// (set) Token: 0x060017CC RID: 6092 RVA: 0x0008C5A8 File Offset: 0x0008A7A8
		public long potionIndex
		{
			get
			{
				return this._potionIndex;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._potionIndex = value;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x0008C5C0 File Offset: 0x0008A7C0
		public bool HasPotionIndex
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x0008C5D0 File Offset: 0x0008A7D0
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x0008C5D8 File Offset: 0x0008A7D8
		public runtime_agent runtime
		{
			get
			{
				return this._runtime;
			}
			set
			{
				this.has_field.set_field(11, true);
				this._runtime = value;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0008C5F0 File Offset: 0x0008A7F0
		public bool HasRuntime
		{
			get
			{
				return this.has_field.has_field(11);
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x0008C600 File Offset: 0x0008A800
		// (set) Token: 0x060017D2 RID: 6098 RVA: 0x0008C608 File Offset: 0x0008A808
		public Dictionary<long, enhance_info> equip_enhance
		{
			get
			{
				return this._equip_enhance;
			}
			set
			{
				this.has_field.set_field(12, true);
				this._equip_enhance = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x0008C620 File Offset: 0x0008A820
		public bool HasEquip_enhance
		{
			get
			{
				return this.has_field.has_field(12);
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0008C630 File Offset: 0x0008A830
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x0008C638 File Offset: 0x0008A838
		public long download
		{
			get
			{
				return this._download;
			}
			set
			{
				this.has_field.set_field(13, true);
				this._download = value;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x0008C650 File Offset: 0x0008A850
		public bool HasDownload
		{
			get
			{
				return this.has_field.has_field(13);
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x0008C660 File Offset: 0x0008A860
		// (set) Token: 0x060017D8 RID: 6104 RVA: 0x0008C668 File Offset: 0x0008A868
		public long skill_index
		{
			get
			{
				return this._skill_index;
			}
			set
			{
				this.has_field.set_field(14, true);
				this._skill_index = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x0008C680 File Offset: 0x0008A880
		public bool HasSkill_index
		{
			get
			{
				return this.has_field.has_field(14);
			}
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0008C690 File Offset: 0x0008A890
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.id = this.deserialize.read_integer();
					continue;
				case 1:
					this.general = this.deserialize.read_obj<general>();
					continue;
				case 2:
					this.attribute_other = this.deserialize.read_obj<attribute_other>();
					continue;
				case 5:
					this.property = this.deserialize.read_obj<property>();
					continue;
				case 6:
					this.visual = this.deserialize.read_obj<characterVisual>();
					continue;
				case 7:
					this.movement = this.deserialize.read_obj<movement>();
					continue;
				case 8:
					this.skills = this.deserialize.read_map<string, skill_info>((skill_info v) => v.skillId);
					continue;
				case 9:
					this.equip = this.deserialize.read_map<long, gameitem>((gameitem v) => v.indexId);
					continue;
				case 10:
					this.badge_equip = this.deserialize.read_map<long, gameitem>((gameitem v) => v.indexId);
					continue;
				case 11:
					this.fashion_equip = this.deserialize.read_map<long, gameitem>((gameitem v) => v.indexId);
					continue;
				case 12:
					this.potionIndex = this.deserialize.read_integer();
					continue;
				case 13:
					this.runtime = this.deserialize.read_obj<runtime_agent>();
					continue;
				case 14:
					this.equip_enhance = this.deserialize.read_map<long, enhance_info>((enhance_info v) => v.subType);
					continue;
				case 15:
					this.download = this.deserialize.read_integer();
					continue;
				case 16:
					this.skill_index = this.deserialize.read_integer();
					continue;
				}
				this.deserialize.read_unknow_data();
			}
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0008C8F4 File Offset: 0x0008AAF4
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
				this.serialize.write_obj(this.attribute_other, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_obj(this.property, 5);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_obj(this.visual, 6);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_obj(this.movement, 7);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_obj<string, skill_info>(this.skills, 8);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_obj<long, gameitem>(this.equip, 9);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_obj<long, gameitem>(this.badge_equip, 10);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_obj<long, gameitem>(this.fashion_equip, 11);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_integer(this.potionIndex, 12);
			}
			if (this.has_field.has_field(11))
			{
				this.serialize.write_obj(this.runtime, 13);
			}
			if (this.has_field.has_field(12))
			{
				this.serialize.write_obj<long, enhance_info>(this.equip_enhance, 14);
			}
			if (this.has_field.has_field(13))
			{
				this.serialize.write_integer(this.download, 15);
			}
			if (this.has_field.has_field(14))
			{
				this.serialize.write_integer(this.skill_index, 16);
			}
			return this.serialize.close();
		}

		// Token: 0x040018F6 RID: 6390
		private static int max_field_count = 16;

		// Token: 0x040018F7 RID: 6391
		private long _id;

		// Token: 0x040018F8 RID: 6392
		private general _general;

		// Token: 0x040018F9 RID: 6393
		private attribute_other _attribute_other;

		// Token: 0x040018FA RID: 6394
		private property _property;

		// Token: 0x040018FB RID: 6395
		private characterVisual _visual;

		// Token: 0x040018FC RID: 6396
		private movement _movement;

		// Token: 0x040018FD RID: 6397
		private Dictionary<string, skill_info> _skills;

		// Token: 0x040018FE RID: 6398
		private Dictionary<long, gameitem> _equip;

		// Token: 0x040018FF RID: 6399
		private Dictionary<long, gameitem> _badge_equip;

		// Token: 0x04001900 RID: 6400
		private Dictionary<long, gameitem> _fashion_equip;

		// Token: 0x04001901 RID: 6401
		private long _potionIndex;

		// Token: 0x04001902 RID: 6402
		private runtime_agent _runtime;

		// Token: 0x04001903 RID: 6403
		private Dictionary<long, enhance_info> _equip_enhance;

		// Token: 0x04001904 RID: 6404
		private long _download;

		// Token: 0x04001905 RID: 6405
		private long _skill_index;
	}
}
