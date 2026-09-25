using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200033D RID: 829
	public class characterVisual : SprotoTypeBase
	{
		// Token: 0x060017E1 RID: 6113 RVA: 0x0008CB5C File Offset: 0x0008AD5C
		public characterVisual() : base(characterVisual.max_field_count)
		{
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0008CB6C File Offset: 0x0008AD6C
		public characterVisual(byte[] buffer) : base(characterVisual.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x0008CB8C File Offset: 0x0008AD8C
		// (set) Token: 0x060017E5 RID: 6117 RVA: 0x0008CB94 File Offset: 0x0008AD94
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._name = value;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x0008CBAC File Offset: 0x0008ADAC
		public bool HasName
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0008CBBC File Offset: 0x0008ADBC
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x0008CBC4 File Offset: 0x0008ADC4
		public string ModeId
		{
			get
			{
				return this._ModeId;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._ModeId = value;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0008CBDC File Offset: 0x0008ADDC
		public bool HasModeId
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x0008CBEC File Offset: 0x0008ADEC
		// (set) Token: 0x060017EB RID: 6123 RVA: 0x0008CBF4 File Offset: 0x0008ADF4
		public string HeadId
		{
			get
			{
				return this._HeadId;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._HeadId = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x0008CC0C File Offset: 0x0008AE0C
		public bool HasHeadId
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0008CC1C File Offset: 0x0008AE1C
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x0008CC24 File Offset: 0x0008AE24
		public string BodyId
		{
			get
			{
				return this._BodyId;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._BodyId = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0008CC3C File Offset: 0x0008AE3C
		public bool HasBodyId
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0008CC4C File Offset: 0x0008AE4C
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x0008CC54 File Offset: 0x0008AE54
		public string LegId
		{
			get
			{
				return this._LegId;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._LegId = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0008CC6C File Offset: 0x0008AE6C
		public bool HasLegId
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0008CC7C File Offset: 0x0008AE7C
		// (set) Token: 0x060017F4 RID: 6132 RVA: 0x0008CC84 File Offset: 0x0008AE84
		public string WeaponId
		{
			get
			{
				return this._WeaponId;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._WeaponId = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0008CC9C File Offset: 0x0008AE9C
		public bool HasWeaponId
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x0008CCAC File Offset: 0x0008AEAC
		// (set) Token: 0x060017F7 RID: 6135 RVA: 0x0008CCB4 File Offset: 0x0008AEB4
		public string Fashion_HeadId
		{
			get
			{
				return this._Fashion_HeadId;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._Fashion_HeadId = value;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0008CCCC File Offset: 0x0008AECC
		public bool HasFashion_HeadId
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0008CCDC File Offset: 0x0008AEDC
		// (set) Token: 0x060017FA RID: 6138 RVA: 0x0008CCE4 File Offset: 0x0008AEE4
		public string Fashion_BodyId
		{
			get
			{
				return this._Fashion_BodyId;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._Fashion_BodyId = value;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0008CCFC File Offset: 0x0008AEFC
		public bool HasFashion_BodyId
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0008CD0C File Offset: 0x0008AF0C
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x0008CD14 File Offset: 0x0008AF14
		public string Fashion_LegId
		{
			get
			{
				return this._Fashion_LegId;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._Fashion_LegId = value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x0008CD2C File Offset: 0x0008AF2C
		public bool HasFashion_LegId
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x0008CD3C File Offset: 0x0008AF3C
		// (set) Token: 0x06001800 RID: 6144 RVA: 0x0008CD44 File Offset: 0x0008AF44
		public string Fashion_WeaponId
		{
			get
			{
				return this._Fashion_WeaponId;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._Fashion_WeaponId = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x0008CD5C File Offset: 0x0008AF5C
		public bool HasFashion_WeaponId
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x0008CD6C File Offset: 0x0008AF6C
		// (set) Token: 0x06001803 RID: 6147 RVA: 0x0008CD74 File Offset: 0x0008AF74
		public long showType
		{
			get
			{
				return this._showType;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._showType = value;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x0008CD8C File Offset: 0x0008AF8C
		public bool HasShowType
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0008CD9C File Offset: 0x0008AF9C
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x0008CDA4 File Offset: 0x0008AFA4
		public string MountId
		{
			get
			{
				return this._MountId;
			}
			set
			{
				this.has_field.set_field(11, true);
				this._MountId = value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0008CDBC File Offset: 0x0008AFBC
		public bool HasMountId
		{
			get
			{
				return this.has_field.has_field(11);
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x0008CDCC File Offset: 0x0008AFCC
		// (set) Token: 0x06001809 RID: 6153 RVA: 0x0008CDD4 File Offset: 0x0008AFD4
		public long mount_state
		{
			get
			{
				return this._mount_state;
			}
			set
			{
				this.has_field.set_field(12, true);
				this._mount_state = value;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x0008CDEC File Offset: 0x0008AFEC
		public bool HasMount_state
		{
			get
			{
				return this.has_field.has_field(12);
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x0008CDFC File Offset: 0x0008AFFC
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x0008CE04 File Offset: 0x0008B004
		public string mount_color
		{
			get
			{
				return this._mount_color;
			}
			set
			{
				this.has_field.set_field(13, true);
				this._mount_color = value;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0008CE1C File Offset: 0x0008B01C
		public bool HasMount_color
		{
			get
			{
				return this.has_field.has_field(13);
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x0008CE2C File Offset: 0x0008B02C
		// (set) Token: 0x0600180F RID: 6159 RVA: 0x0008CE34 File Offset: 0x0008B034
		public string WeaponItemId
		{
			get
			{
				return this._WeaponItemId;
			}
			set
			{
				this.has_field.set_field(14, true);
				this._WeaponItemId = value;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x0008CE4C File Offset: 0x0008B04C
		public bool HasWeaponItemId
		{
			get
			{
				return this.has_field.has_field(14);
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0008CE5C File Offset: 0x0008B05C
		// (set) Token: 0x06001812 RID: 6162 RVA: 0x0008CE64 File Offset: 0x0008B064
		public string FashionItemId
		{
			get
			{
				return this._FashionItemId;
			}
			set
			{
				this.has_field.set_field(15, true);
				this._FashionItemId = value;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x0008CE7C File Offset: 0x0008B07C
		public bool HasFashionItemId
		{
			get
			{
				return this.has_field.has_field(15);
			}
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0008CE8C File Offset: 0x0008B08C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.name = this.deserialize.read_string();
					continue;
				case 1:
					this.ModeId = this.deserialize.read_string();
					continue;
				case 2:
					this.HeadId = this.deserialize.read_string();
					continue;
				case 3:
					this.BodyId = this.deserialize.read_string();
					continue;
				case 4:
					this.LegId = this.deserialize.read_string();
					continue;
				case 5:
					this.WeaponId = this.deserialize.read_string();
					continue;
				case 6:
					this.Fashion_HeadId = this.deserialize.read_string();
					continue;
				case 7:
					this.Fashion_BodyId = this.deserialize.read_string();
					continue;
				case 8:
					this.Fashion_LegId = this.deserialize.read_string();
					continue;
				case 9:
					this.Fashion_WeaponId = this.deserialize.read_string();
					continue;
				case 10:
					this.showType = this.deserialize.read_integer();
					continue;
				case 12:
					this.MountId = this.deserialize.read_string();
					continue;
				case 13:
					this.mount_state = this.deserialize.read_integer();
					continue;
				case 14:
					this.mount_color = this.deserialize.read_string();
					continue;
				case 15:
					this.WeaponItemId = this.deserialize.read_string();
					continue;
				case 16:
					this.FashionItemId = this.deserialize.read_string();
					continue;
				}
				this.deserialize.read_unknow_data();
			}
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0008D074 File Offset: 0x0008B274
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.name, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.ModeId, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_string(this.HeadId, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_string(this.BodyId, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_string(this.LegId, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_string(this.WeaponId, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_string(this.Fashion_HeadId, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_string(this.Fashion_BodyId, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_string(this.Fashion_LegId, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_string(this.Fashion_WeaponId, 9);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_integer(this.showType, 10);
			}
			if (this.has_field.has_field(11))
			{
				this.serialize.write_string(this.MountId, 12);
			}
			if (this.has_field.has_field(12))
			{
				this.serialize.write_integer(this.mount_state, 13);
			}
			if (this.has_field.has_field(13))
			{
				this.serialize.write_string(this.mount_color, 14);
			}
			if (this.has_field.has_field(14))
			{
				this.serialize.write_string(this.WeaponItemId, 15);
			}
			if (this.has_field.has_field(15))
			{
				this.serialize.write_string(this.FashionItemId, 16);
			}
			return this.serialize.close();
		}

		// Token: 0x0400190B RID: 6411
		private static int max_field_count = 17;

		// Token: 0x0400190C RID: 6412
		private string _name;

		// Token: 0x0400190D RID: 6413
		private string _ModeId;

		// Token: 0x0400190E RID: 6414
		private string _HeadId;

		// Token: 0x0400190F RID: 6415
		private string _BodyId;

		// Token: 0x04001910 RID: 6416
		private string _LegId;

		// Token: 0x04001911 RID: 6417
		private string _WeaponId;

		// Token: 0x04001912 RID: 6418
		private string _Fashion_HeadId;

		// Token: 0x04001913 RID: 6419
		private string _Fashion_BodyId;

		// Token: 0x04001914 RID: 6420
		private string _Fashion_LegId;

		// Token: 0x04001915 RID: 6421
		private string _Fashion_WeaponId;

		// Token: 0x04001916 RID: 6422
		private long _showType;

		// Token: 0x04001917 RID: 6423
		private string _MountId;

		// Token: 0x04001918 RID: 6424
		private long _mount_state;

		// Token: 0x04001919 RID: 6425
		private string _mount_color;

		// Token: 0x0400191A RID: 6426
		private string _WeaponItemId;

		// Token: 0x0400191B RID: 6427
		private string _FashionItemId;
	}
}
