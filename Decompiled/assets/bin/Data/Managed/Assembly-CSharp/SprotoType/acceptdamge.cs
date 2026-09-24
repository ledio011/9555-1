using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002E6 RID: 742
	public class acceptdamge : SprotoTypeBase
	{
		// Token: 0x060014B9 RID: 5305 RVA: 0x000862C8 File Offset: 0x000844C8
		public acceptdamge() : base(acceptdamge.max_field_count)
		{
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x000862D8 File Offset: 0x000844D8
		public acceptdamge(byte[] buffer) : base(acceptdamge.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x000862F8 File Offset: 0x000844F8
		// (set) Token: 0x060014BD RID: 5309 RVA: 0x00086300 File Offset: 0x00084500
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

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x00086318 File Offset: 0x00084518
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00086328 File Offset: 0x00084528
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x00086330 File Offset: 0x00084530
		public long damage
		{
			get
			{
				return this._damage;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._damage = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x00086348 File Offset: 0x00084548
		public bool HasDamage
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00086358 File Offset: 0x00084558
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x00086360 File Offset: 0x00084560
		public string skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._skillId = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00086378 File Offset: 0x00084578
		public bool HasSkillId
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x00086388 File Offset: 0x00084588
		// (set) Token: 0x060014C6 RID: 5318 RVA: 0x00086390 File Offset: 0x00084590
		public string effinfoId
		{
			get
			{
				return this._effinfoId;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._effinfoId = value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x000863A8 File Offset: 0x000845A8
		public bool HasEffinfoId
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x000863B8 File Offset: 0x000845B8
		// (set) Token: 0x060014C9 RID: 5321 RVA: 0x000863C0 File Offset: 0x000845C0
		public bool cri
		{
			get
			{
				return this._cri;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._cri = value;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x000863D8 File Offset: 0x000845D8
		public bool HasCri
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x000863E8 File Offset: 0x000845E8
		// (set) Token: 0x060014CC RID: 5324 RVA: 0x000863F0 File Offset: 0x000845F0
		public long parm
		{
			get
			{
				return this._parm;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._parm = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x00086408 File Offset: 0x00084608
		public bool HasParm
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x00086418 File Offset: 0x00084618
		// (set) Token: 0x060014CF RID: 5327 RVA: 0x00086420 File Offset: 0x00084620
		public long parm2
		{
			get
			{
				return this._parm2;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._parm2 = value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00086438 File Offset: 0x00084638
		public bool HasParm2
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00086448 File Offset: 0x00084648
		// (set) Token: 0x060014D2 RID: 5330 RVA: 0x00086450 File Offset: 0x00084650
		public long parm3
		{
			get
			{
				return this._parm3;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._parm3 = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00086468 File Offset: 0x00084668
		public bool HasParm3
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00086478 File Offset: 0x00084678
		// (set) Token: 0x060014D5 RID: 5333 RVA: 0x00086480 File Offset: 0x00084680
		public long parm4
		{
			get
			{
				return this._parm4;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._parm4 = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00086498 File Offset: 0x00084698
		public bool HasParm4
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x000864A8 File Offset: 0x000846A8
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
					this.damage = this.deserialize.read_integer();
					break;
				case 2:
					this.skillId = this.deserialize.read_string();
					break;
				case 3:
					this.effinfoId = this.deserialize.read_string();
					break;
				case 4:
					this.cri = this.deserialize.read_boolean();
					break;
				case 5:
					this.parm = this.deserialize.read_integer();
					break;
				case 6:
					this.parm2 = this.deserialize.read_integer();
					break;
				case 7:
					this.parm3 = this.deserialize.read_integer();
					break;
				case 8:
					this.parm4 = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x000865D8 File Offset: 0x000847D8
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.damage, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_string(this.skillId, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_string(this.effinfoId, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_boolean(this.cri, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.parm, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.parm2, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.parm3, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.parm4, 8);
			}
			return this.serialize.close();
		}

		// Token: 0x04001828 RID: 6184
		private static int max_field_count = 9;

		// Token: 0x04001829 RID: 6185
		private long _id;

		// Token: 0x0400182A RID: 6186
		private long _damage;

		// Token: 0x0400182B RID: 6187
		private string _skillId;

		// Token: 0x0400182C RID: 6188
		private string _effinfoId;

		// Token: 0x0400182D RID: 6189
		private bool _cri;

		// Token: 0x0400182E RID: 6190
		private long _parm;

		// Token: 0x0400182F RID: 6191
		private long _parm2;

		// Token: 0x04001830 RID: 6192
		private long _parm3;

		// Token: 0x04001831 RID: 6193
		private long _parm4;
	}
}
