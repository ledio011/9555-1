using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000311 RID: 785
	public class attribute : SprotoTypeBase
	{
		// Token: 0x060015EC RID: 5612 RVA: 0x0008891C File Offset: 0x00086B1C
		public attribute() : base(attribute.max_field_count)
		{
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x0008892C File Offset: 0x00086B2C
		public attribute(byte[] buffer) : base(attribute.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0008894C File Offset: 0x00086B4C
		// (set) Token: 0x060015F0 RID: 5616 RVA: 0x00088954 File Offset: 0x00086B54
		public long max_hp
		{
			get
			{
				return this._max_hp;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._max_hp = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0008896C File Offset: 0x00086B6C
		public bool HasMax_hp
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0008897C File Offset: 0x00086B7C
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x00088984 File Offset: 0x00086B84
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

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0008899C File Offset: 0x00086B9C
		public bool HasExp
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x000889AC File Offset: 0x00086BAC
		// (set) Token: 0x060015F6 RID: 5622 RVA: 0x000889B4 File Offset: 0x00086BB4
		public long atk
		{
			get
			{
				return this._atk;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._atk = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x000889CC File Offset: 0x00086BCC
		public bool HasAtk
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x000889DC File Offset: 0x00086BDC
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x000889E4 File Offset: 0x00086BE4
		public long def
		{
			get
			{
				return this._def;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._def = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x000889FC File Offset: 0x00086BFC
		public bool HasDef
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x00088A0C File Offset: 0x00086C0C
		// (set) Token: 0x060015FC RID: 5628 RVA: 0x00088A14 File Offset: 0x00086C14
		public long hit
		{
			get
			{
				return this._hit;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._hit = value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x00088A2C File Offset: 0x00086C2C
		public bool HasHit
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00088A3C File Offset: 0x00086C3C
		// (set) Token: 0x060015FF RID: 5631 RVA: 0x00088A44 File Offset: 0x00086C44
		public long eva
		{
			get
			{
				return this._eva;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._eva = value;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x00088A5C File Offset: 0x00086C5C
		public bool HasEva
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x00088A6C File Offset: 0x00086C6C
		// (set) Token: 0x06001602 RID: 5634 RVA: 0x00088A74 File Offset: 0x00086C74
		public long cri
		{
			get
			{
				return this._cri;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._cri = value;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00088A8C File Offset: 0x00086C8C
		public bool HasCri
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00088A9C File Offset: 0x00086C9C
		// (set) Token: 0x06001605 RID: 5637 RVA: 0x00088AA4 File Offset: 0x00086CA4
		public long res
		{
			get
			{
				return this._res;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._res = value;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x00088ABC File Offset: 0x00086CBC
		public bool HasRes
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x00088ACC File Offset: 0x00086CCC
		// (set) Token: 0x06001608 RID: 5640 RVA: 0x00088AD4 File Offset: 0x00086CD4
		public long exd
		{
			get
			{
				return this._exd;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._exd = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x00088AEC File Offset: 0x00086CEC
		public bool HasExd
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x00088AFC File Offset: 0x00086CFC
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x00088B04 File Offset: 0x00086D04
		public long exr
		{
			get
			{
				return this._exr;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._exr = value;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x00088B1C File Offset: 0x00086D1C
		public bool HasExr
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00088B2C File Offset: 0x00086D2C
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x00088B34 File Offset: 0x00086D34
		public long crd
		{
			get
			{
				return this._crd;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._crd = value;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00088B4C File Offset: 0x00086D4C
		public bool HasCrd
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x00088B5C File Offset: 0x00086D5C
		// (set) Token: 0x06001611 RID: 5649 RVA: 0x00088B64 File Offset: 0x00086D64
		public long crr
		{
			get
			{
				return this._crr;
			}
			set
			{
				this.has_field.set_field(11, true);
				this._crr = value;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x00088B7C File Offset: 0x00086D7C
		public bool HasCrr
		{
			get
			{
				return this.has_field.has_field(11);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00088B8C File Offset: 0x00086D8C
		// (set) Token: 0x06001614 RID: 5652 RVA: 0x00088B94 File Offset: 0x00086D94
		public long defa
		{
			get
			{
				return this._defa;
			}
			set
			{
				this.has_field.set_field(12, true);
				this._defa = value;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x00088BAC File Offset: 0x00086DAC
		public bool HasDefa
		{
			get
			{
				return this.has_field.has_field(12);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x00088BBC File Offset: 0x00086DBC
		// (set) Token: 0x06001617 RID: 5655 RVA: 0x00088BC4 File Offset: 0x00086DC4
		public long mov
		{
			get
			{
				return this._mov;
			}
			set
			{
				this.has_field.set_field(13, true);
				this._mov = value;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x00088BDC File Offset: 0x00086DDC
		public bool HasMov
		{
			get
			{
				return this.has_field.has_field(13);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x00088BEC File Offset: 0x00086DEC
		// (set) Token: 0x0600161A RID: 5658 RVA: 0x00088BF4 File Offset: 0x00086DF4
		public long rec
		{
			get
			{
				return this._rec;
			}
			set
			{
				this.has_field.set_field(14, true);
				this._rec = value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x00088C0C File Offset: 0x00086E0C
		public bool HasRec
		{
			get
			{
				return this.has_field.has_field(14);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x00088C1C File Offset: 0x00086E1C
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x00088C24 File Offset: 0x00086E24
		public long anti_stun
		{
			get
			{
				return this._anti_stun;
			}
			set
			{
				this.has_field.set_field(15, true);
				this._anti_stun = value;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x00088C3C File Offset: 0x00086E3C
		public bool HasAnti_stun
		{
			get
			{
				return this.has_field.has_field(15);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x00088C4C File Offset: 0x00086E4C
		// (set) Token: 0x06001620 RID: 5664 RVA: 0x00088C54 File Offset: 0x00086E54
		public long anti_knock_down
		{
			get
			{
				return this._anti_knock_down;
			}
			set
			{
				this.has_field.set_field(16, true);
				this._anti_knock_down = value;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00088C6C File Offset: 0x00086E6C
		public bool HasAnti_knock_down
		{
			get
			{
				return this.has_field.has_field(16);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x00088C7C File Offset: 0x00086E7C
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x00088C84 File Offset: 0x00086E84
		public long dgea
		{
			get
			{
				return this._dgea;
			}
			set
			{
				this.has_field.set_field(17, true);
				this._dgea = value;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x00088C9C File Offset: 0x00086E9C
		public bool HasDgea
		{
			get
			{
				return this.has_field.has_field(17);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x00088CAC File Offset: 0x00086EAC
		// (set) Token: 0x06001626 RID: 5670 RVA: 0x00088CB4 File Offset: 0x00086EB4
		public long resa
		{
			get
			{
				return this._resa;
			}
			set
			{
				this.has_field.set_field(18, true);
				this._resa = value;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x00088CCC File Offset: 0x00086ECC
		public bool HasResa
		{
			get
			{
				return this.has_field.has_field(18);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x00088CDC File Offset: 0x00086EDC
		// (set) Token: 0x06001629 RID: 5673 RVA: 0x00088CE4 File Offset: 0x00086EE4
		public long hita
		{
			get
			{
				return this._hita;
			}
			set
			{
				this.has_field.set_field(19, true);
				this._hita = value;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x00088CFC File Offset: 0x00086EFC
		public bool HasHita
		{
			get
			{
				return this.has_field.has_field(19);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x00088D0C File Offset: 0x00086F0C
		// (set) Token: 0x0600162C RID: 5676 RVA: 0x00088D14 File Offset: 0x00086F14
		public long cria
		{
			get
			{
				return this._cria;
			}
			set
			{
				this.has_field.set_field(20, true);
				this._cria = value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x00088D2C File Offset: 0x00086F2C
		public bool HasCria
		{
			get
			{
				return this.has_field.has_field(20);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x00088D3C File Offset: 0x00086F3C
		// (set) Token: 0x0600162F RID: 5679 RVA: 0x00088D44 File Offset: 0x00086F44
		public long ate
		{
			get
			{
				return this._ate;
			}
			set
			{
				this.has_field.set_field(21, true);
				this._ate = value;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x00088D5C File Offset: 0x00086F5C
		public bool HasAte
		{
			get
			{
				return this.has_field.has_field(21);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x00088D6C File Offset: 0x00086F6C
		// (set) Token: 0x06001632 RID: 5682 RVA: 0x00088D74 File Offset: 0x00086F74
		public long satm
		{
			get
			{
				return this._satm;
			}
			set
			{
				this.has_field.set_field(22, true);
				this._satm = value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x00088D8C File Offset: 0x00086F8C
		public bool HasSatm
		{
			get
			{
				return this.has_field.has_field(22);
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x00088D9C File Offset: 0x00086F9C
		// (set) Token: 0x06001635 RID: 5685 RVA: 0x00088DA4 File Offset: 0x00086FA4
		public long satc
		{
			get
			{
				return this._satc;
			}
			set
			{
				this.has_field.set_field(23, true);
				this._satc = value;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x00088DBC File Offset: 0x00086FBC
		public bool HasSatc
		{
			get
			{
				return this.has_field.has_field(23);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x00088DCC File Offset: 0x00086FCC
		// (set) Token: 0x06001638 RID: 5688 RVA: 0x00088DD4 File Offset: 0x00086FD4
		public long satp
		{
			get
			{
				return this._satp;
			}
			set
			{
				this.has_field.set_field(24, true);
				this._satp = value;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00088DEC File Offset: 0x00086FEC
		public bool HasSatp
		{
			get
			{
				return this.has_field.has_field(24);
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00088DFC File Offset: 0x00086FFC
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.max_hp = this.deserialize.read_integer();
					break;
				case 1:
					this.exp = this.deserialize.read_integer();
					break;
				case 2:
					this.atk = this.deserialize.read_integer();
					break;
				case 3:
					this.def = this.deserialize.read_integer();
					break;
				case 4:
					this.hit = this.deserialize.read_integer();
					break;
				case 5:
					this.eva = this.deserialize.read_integer();
					break;
				case 6:
					this.cri = this.deserialize.read_integer();
					break;
				case 7:
					this.res = this.deserialize.read_integer();
					break;
				case 8:
					this.exd = this.deserialize.read_integer();
					break;
				case 9:
					this.exr = this.deserialize.read_integer();
					break;
				case 10:
					this.crd = this.deserialize.read_integer();
					break;
				case 11:
					this.crr = this.deserialize.read_integer();
					break;
				case 12:
					this.defa = this.deserialize.read_integer();
					break;
				case 13:
					this.mov = this.deserialize.read_integer();
					break;
				case 14:
					this.rec = this.deserialize.read_integer();
					break;
				case 15:
					this.anti_stun = this.deserialize.read_integer();
					break;
				case 16:
					this.anti_knock_down = this.deserialize.read_integer();
					break;
				case 17:
					this.dgea = this.deserialize.read_integer();
					break;
				case 18:
					this.resa = this.deserialize.read_integer();
					break;
				case 19:
					this.hita = this.deserialize.read_integer();
					break;
				case 20:
					this.cria = this.deserialize.read_integer();
					break;
				case 21:
					this.ate = this.deserialize.read_integer();
					break;
				case 22:
					this.satm = this.deserialize.read_integer();
					break;
				case 23:
					this.satc = this.deserialize.read_integer();
					break;
				case 24:
					this.satp = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000890CC File Offset: 0x000872CC
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.max_hp, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.exp, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.atk, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.def, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.hit, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.eva, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.cri, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.res, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.exd, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_integer(this.exr, 9);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_integer(this.crd, 10);
			}
			if (this.has_field.has_field(11))
			{
				this.serialize.write_integer(this.crr, 11);
			}
			if (this.has_field.has_field(12))
			{
				this.serialize.write_integer(this.defa, 12);
			}
			if (this.has_field.has_field(13))
			{
				this.serialize.write_integer(this.mov, 13);
			}
			if (this.has_field.has_field(14))
			{
				this.serialize.write_integer(this.rec, 14);
			}
			if (this.has_field.has_field(15))
			{
				this.serialize.write_integer(this.anti_stun, 15);
			}
			if (this.has_field.has_field(16))
			{
				this.serialize.write_integer(this.anti_knock_down, 16);
			}
			if (this.has_field.has_field(17))
			{
				this.serialize.write_integer(this.dgea, 17);
			}
			if (this.has_field.has_field(18))
			{
				this.serialize.write_integer(this.resa, 18);
			}
			if (this.has_field.has_field(19))
			{
				this.serialize.write_integer(this.hita, 19);
			}
			if (this.has_field.has_field(20))
			{
				this.serialize.write_integer(this.cria, 20);
			}
			if (this.has_field.has_field(21))
			{
				this.serialize.write_integer(this.ate, 21);
			}
			if (this.has_field.has_field(22))
			{
				this.serialize.write_integer(this.satm, 22);
			}
			if (this.has_field.has_field(23))
			{
				this.serialize.write_integer(this.satc, 23);
			}
			if (this.has_field.has_field(24))
			{
				this.serialize.write_integer(this.satp, 24);
			}
			return this.serialize.close();
		}

		// Token: 0x04001878 RID: 6264
		private static int max_field_count = 25;

		// Token: 0x04001879 RID: 6265
		private long _max_hp;

		// Token: 0x0400187A RID: 6266
		private long _exp;

		// Token: 0x0400187B RID: 6267
		private long _atk;

		// Token: 0x0400187C RID: 6268
		private long _def;

		// Token: 0x0400187D RID: 6269
		private long _hit;

		// Token: 0x0400187E RID: 6270
		private long _eva;

		// Token: 0x0400187F RID: 6271
		private long _cri;

		// Token: 0x04001880 RID: 6272
		private long _res;

		// Token: 0x04001881 RID: 6273
		private long _exd;

		// Token: 0x04001882 RID: 6274
		private long _exr;

		// Token: 0x04001883 RID: 6275
		private long _crd;

		// Token: 0x04001884 RID: 6276
		private long _crr;

		// Token: 0x04001885 RID: 6277
		private long _defa;

		// Token: 0x04001886 RID: 6278
		private long _mov;

		// Token: 0x04001887 RID: 6279
		private long _rec;

		// Token: 0x04001888 RID: 6280
		private long _anti_stun;

		// Token: 0x04001889 RID: 6281
		private long _anti_knock_down;

		// Token: 0x0400188A RID: 6282
		private long _dgea;

		// Token: 0x0400188B RID: 6283
		private long _resa;

		// Token: 0x0400188C RID: 6284
		private long _hita;

		// Token: 0x0400188D RID: 6285
		private long _cria;

		// Token: 0x0400188E RID: 6286
		private long _ate;

		// Token: 0x0400188F RID: 6287
		private long _satm;

		// Token: 0x04001890 RID: 6288
		private long _satc;

		// Token: 0x04001891 RID: 6289
		private long _satp;
	}
}
