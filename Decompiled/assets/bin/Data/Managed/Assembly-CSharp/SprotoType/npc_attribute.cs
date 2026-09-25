using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000437 RID: 1079
	public class npc_attribute : SprotoTypeBase
	{
		// Token: 0x0600215F RID: 8543 RVA: 0x000A0624 File Offset: 0x0009E824
		public npc_attribute() : base(npc_attribute.max_field_count)
		{
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000A0634 File Offset: 0x0009E834
		public npc_attribute(byte[] buffer) : base(npc_attribute.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x000A0654 File Offset: 0x0009E854
		// (set) Token: 0x06002163 RID: 8547 RVA: 0x000A065C File Offset: 0x0009E85C
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

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x000A0674 File Offset: 0x0009E874
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x000A0684 File Offset: 0x0009E884
		// (set) Token: 0x06002166 RID: 8550 RVA: 0x000A068C File Offset: 0x0009E88C
		public string npcdataid
		{
			get
			{
				return this._npcdataid;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._npcdataid = value;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x000A06A4 File Offset: 0x0009E8A4
		public bool HasNpcdataid
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x000A06B4 File Offset: 0x0009E8B4
		// (set) Token: 0x06002169 RID: 8553 RVA: 0x000A06BC File Offset: 0x0009E8BC
		public long hp
		{
			get
			{
				return this._hp;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._hp = value;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x000A06D4 File Offset: 0x0009E8D4
		public bool HasHp
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x0600216B RID: 8555 RVA: 0x000A06E4 File Offset: 0x0009E8E4
		// (set) Token: 0x0600216C RID: 8556 RVA: 0x000A06EC File Offset: 0x0009E8EC
		public long max_hp
		{
			get
			{
				return this._max_hp;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._max_hp = value;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x000A0704 File Offset: 0x0009E904
		public bool HasMax_hp
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x000A0714 File Offset: 0x0009E914
		// (set) Token: 0x0600216F RID: 8559 RVA: 0x000A071C File Offset: 0x0009E91C
		public long atk
		{
			get
			{
				return this._atk;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._atk = value;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x000A0734 File Offset: 0x0009E934
		public bool HasAtk
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002171 RID: 8561 RVA: 0x000A0744 File Offset: 0x0009E944
		// (set) Token: 0x06002172 RID: 8562 RVA: 0x000A074C File Offset: 0x0009E94C
		public long def
		{
			get
			{
				return this._def;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._def = value;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002173 RID: 8563 RVA: 0x000A0764 File Offset: 0x0009E964
		public bool HasDef
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002174 RID: 8564 RVA: 0x000A0774 File Offset: 0x0009E974
		// (set) Token: 0x06002175 RID: 8565 RVA: 0x000A077C File Offset: 0x0009E97C
		public long hit
		{
			get
			{
				return this._hit;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._hit = value;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x000A0794 File Offset: 0x0009E994
		public bool HasHit
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x000A07A4 File Offset: 0x0009E9A4
		// (set) Token: 0x06002178 RID: 8568 RVA: 0x000A07AC File Offset: 0x0009E9AC
		public long eva
		{
			get
			{
				return this._eva;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._eva = value;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x000A07C4 File Offset: 0x0009E9C4
		public bool HasEva
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x000A07D4 File Offset: 0x0009E9D4
		// (set) Token: 0x0600217B RID: 8571 RVA: 0x000A07DC File Offset: 0x0009E9DC
		public long cri
		{
			get
			{
				return this._cri;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._cri = value;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x000A07F4 File Offset: 0x0009E9F4
		public bool HasCri
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x000A0804 File Offset: 0x0009EA04
		// (set) Token: 0x0600217E RID: 8574 RVA: 0x000A080C File Offset: 0x0009EA0C
		public long exd
		{
			get
			{
				return this._exd;
			}
			set
			{
				this.has_field.set_field(9, true);
				this._exd = value;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x000A0824 File Offset: 0x0009EA24
		public bool HasExd
		{
			get
			{
				return this.has_field.has_field(9);
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x000A0834 File Offset: 0x0009EA34
		// (set) Token: 0x06002181 RID: 8577 RVA: 0x000A083C File Offset: 0x0009EA3C
		public long exr
		{
			get
			{
				return this._exr;
			}
			set
			{
				this.has_field.set_field(10, true);
				this._exr = value;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x000A0854 File Offset: 0x0009EA54
		public bool HasExr
		{
			get
			{
				return this.has_field.has_field(10);
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x000A0864 File Offset: 0x0009EA64
		// (set) Token: 0x06002184 RID: 8580 RVA: 0x000A086C File Offset: 0x0009EA6C
		public long res
		{
			get
			{
				return this._res;
			}
			set
			{
				this.has_field.set_field(11, true);
				this._res = value;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x000A0884 File Offset: 0x0009EA84
		public bool HasRes
		{
			get
			{
				return this.has_field.has_field(11);
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x000A0894 File Offset: 0x0009EA94
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x000A089C File Offset: 0x0009EA9C
		public long crd
		{
			get
			{
				return this._crd;
			}
			set
			{
				this.has_field.set_field(12, true);
				this._crd = value;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x000A08B4 File Offset: 0x0009EAB4
		public bool HasCrd
		{
			get
			{
				return this.has_field.has_field(12);
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x000A08C4 File Offset: 0x0009EAC4
		// (set) Token: 0x0600218A RID: 8586 RVA: 0x000A08CC File Offset: 0x0009EACC
		public long crr
		{
			get
			{
				return this._crr;
			}
			set
			{
				this.has_field.set_field(13, true);
				this._crr = value;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x0600218B RID: 8587 RVA: 0x000A08E4 File Offset: 0x0009EAE4
		public bool HasCrr
		{
			get
			{
				return this.has_field.has_field(13);
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x000A08F4 File Offset: 0x0009EAF4
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x000A08FC File Offset: 0x0009EAFC
		public long defa
		{
			get
			{
				return this._defa;
			}
			set
			{
				this.has_field.set_field(14, true);
				this._defa = value;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x000A0914 File Offset: 0x0009EB14
		public bool HasDefa
		{
			get
			{
				return this.has_field.has_field(14);
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x000A0924 File Offset: 0x0009EB24
		// (set) Token: 0x06002190 RID: 8592 RVA: 0x000A092C File Offset: 0x0009EB2C
		public long x
		{
			get
			{
				return this._x;
			}
			set
			{
				this.has_field.set_field(15, true);
				this._x = value;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x000A0944 File Offset: 0x0009EB44
		public bool HasX
		{
			get
			{
				return this.has_field.has_field(15);
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x000A0954 File Offset: 0x0009EB54
		// (set) Token: 0x06002193 RID: 8595 RVA: 0x000A095C File Offset: 0x0009EB5C
		public long z
		{
			get
			{
				return this._z;
			}
			set
			{
				this.has_field.set_field(16, true);
				this._z = value;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x000A0974 File Offset: 0x0009EB74
		public bool HasZ
		{
			get
			{
				return this.has_field.has_field(16);
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x000A0984 File Offset: 0x0009EB84
		// (set) Token: 0x06002196 RID: 8598 RVA: 0x000A098C File Offset: 0x0009EB8C
		public long o
		{
			get
			{
				return this._o;
			}
			set
			{
				this.has_field.set_field(17, true);
				this._o = value;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x000A09A4 File Offset: 0x0009EBA4
		public bool HasO
		{
			get
			{
				return this.has_field.has_field(17);
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x000A09B4 File Offset: 0x0009EBB4
		// (set) Token: 0x06002199 RID: 8601 RVA: 0x000A09BC File Offset: 0x0009EBBC
		public long level
		{
			get
			{
				return this._level;
			}
			set
			{
				this.has_field.set_field(18, true);
				this._level = value;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x000A09D4 File Offset: 0x0009EBD4
		public bool HasLevel
		{
			get
			{
				return this.has_field.has_field(18);
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x000A09E4 File Offset: 0x0009EBE4
		// (set) Token: 0x0600219C RID: 8604 RVA: 0x000A09EC File Offset: 0x0009EBEC
		public long anti_stun
		{
			get
			{
				return this._anti_stun;
			}
			set
			{
				this.has_field.set_field(19, true);
				this._anti_stun = value;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x000A0A04 File Offset: 0x0009EC04
		public bool HasAnti_stun
		{
			get
			{
				return this.has_field.has_field(19);
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x000A0A14 File Offset: 0x0009EC14
		// (set) Token: 0x0600219F RID: 8607 RVA: 0x000A0A1C File Offset: 0x0009EC1C
		public long anti_knock_down
		{
			get
			{
				return this._anti_knock_down;
			}
			set
			{
				this.has_field.set_field(20, true);
				this._anti_knock_down = value;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x060021A0 RID: 8608 RVA: 0x000A0A34 File Offset: 0x0009EC34
		public bool HasAnti_knock_down
		{
			get
			{
				return this.has_field.has_field(20);
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x000A0A44 File Offset: 0x0009EC44
		// (set) Token: 0x060021A2 RID: 8610 RVA: 0x000A0A4C File Offset: 0x0009EC4C
		public string player_name
		{
			get
			{
				return this._player_name;
			}
			set
			{
				this.has_field.set_field(21, true);
				this._player_name = value;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x000A0A64 File Offset: 0x0009EC64
		public bool HasPlayer_name
		{
			get
			{
				return this.has_field.has_field(21);
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x000A0A74 File Offset: 0x0009EC74
		// (set) Token: 0x060021A5 RID: 8613 RVA: 0x000A0A7C File Offset: 0x0009EC7C
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this.has_field.set_field(22, true);
				this._guildId = value;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x000A0A94 File Offset: 0x0009EC94
		public bool HasGuildId
		{
			get
			{
				return this.has_field.has_field(22);
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x000A0AA4 File Offset: 0x0009ECA4
		// (set) Token: 0x060021A8 RID: 8616 RVA: 0x000A0AAC File Offset: 0x0009ECAC
		public long teamid
		{
			get
			{
				return this._teamid;
			}
			set
			{
				this.has_field.set_field(23, true);
				this._teamid = value;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x000A0AC4 File Offset: 0x0009ECC4
		public bool HasTeamid
		{
			get
			{
				return this.has_field.has_field(23);
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x000A0AD4 File Offset: 0x0009ECD4
		// (set) Token: 0x060021AB RID: 8619 RVA: 0x000A0ADC File Offset: 0x0009ECDC
		public long dgea
		{
			get
			{
				return this._dgea;
			}
			set
			{
				this.has_field.set_field(24, true);
				this._dgea = value;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x000A0AF4 File Offset: 0x0009ECF4
		public bool HasDgea
		{
			get
			{
				return this.has_field.has_field(24);
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x000A0B04 File Offset: 0x0009ED04
		// (set) Token: 0x060021AE RID: 8622 RVA: 0x000A0B0C File Offset: 0x0009ED0C
		public long resa
		{
			get
			{
				return this._resa;
			}
			set
			{
				this.has_field.set_field(25, true);
				this._resa = value;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x000A0B24 File Offset: 0x0009ED24
		public bool HasResa
		{
			get
			{
				return this.has_field.has_field(25);
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x000A0B34 File Offset: 0x0009ED34
		// (set) Token: 0x060021B1 RID: 8625 RVA: 0x000A0B3C File Offset: 0x0009ED3C
		public long hita
		{
			get
			{
				return this._hita;
			}
			set
			{
				this.has_field.set_field(26, true);
				this._hita = value;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x000A0B54 File Offset: 0x0009ED54
		public bool HasHita
		{
			get
			{
				return this.has_field.has_field(26);
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x000A0B64 File Offset: 0x0009ED64
		// (set) Token: 0x060021B4 RID: 8628 RVA: 0x000A0B6C File Offset: 0x0009ED6C
		public long cria
		{
			get
			{
				return this._cria;
			}
			set
			{
				this.has_field.set_field(27, true);
				this._cria = value;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x000A0B84 File Offset: 0x0009ED84
		public bool HasCria
		{
			get
			{
				return this.has_field.has_field(27);
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000A0B94 File Offset: 0x0009ED94
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
					this.npcdataid = this.deserialize.read_string();
					break;
				case 2:
					this.hp = this.deserialize.read_integer();
					break;
				case 3:
					this.max_hp = this.deserialize.read_integer();
					break;
				case 4:
					this.atk = this.deserialize.read_integer();
					break;
				case 5:
					this.def = this.deserialize.read_integer();
					break;
				case 6:
					this.hit = this.deserialize.read_integer();
					break;
				case 7:
					this.eva = this.deserialize.read_integer();
					break;
				case 8:
					this.cri = this.deserialize.read_integer();
					break;
				case 9:
					this.exd = this.deserialize.read_integer();
					break;
				case 10:
					this.exr = this.deserialize.read_integer();
					break;
				case 11:
					this.res = this.deserialize.read_integer();
					break;
				case 12:
					this.crd = this.deserialize.read_integer();
					break;
				case 13:
					this.crr = this.deserialize.read_integer();
					break;
				case 14:
					this.defa = this.deserialize.read_integer();
					break;
				case 15:
					this.x = this.deserialize.read_integer();
					break;
				case 16:
					this.z = this.deserialize.read_integer();
					break;
				case 17:
					this.o = this.deserialize.read_integer();
					break;
				case 18:
					this.level = this.deserialize.read_integer();
					break;
				case 19:
					this.anti_stun = this.deserialize.read_integer();
					break;
				case 20:
					this.anti_knock_down = this.deserialize.read_integer();
					break;
				case 21:
					this.player_name = this.deserialize.read_string();
					break;
				case 22:
					this.guildId = this.deserialize.read_integer();
					break;
				case 23:
					this.teamid = this.deserialize.read_integer();
					break;
				case 24:
					this.dgea = this.deserialize.read_integer();
					break;
				case 25:
					this.resa = this.deserialize.read_integer();
					break;
				case 26:
					this.hita = this.deserialize.read_integer();
					break;
				case 27:
					this.cria = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000A0EB0 File Offset: 0x0009F0B0
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.npcdataid, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.hp, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.max_hp, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.atk, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.def, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_integer(this.hit, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.eva, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.cri, 8);
			}
			if (this.has_field.has_field(9))
			{
				this.serialize.write_integer(this.exd, 9);
			}
			if (this.has_field.has_field(10))
			{
				this.serialize.write_integer(this.exr, 10);
			}
			if (this.has_field.has_field(11))
			{
				this.serialize.write_integer(this.res, 11);
			}
			if (this.has_field.has_field(12))
			{
				this.serialize.write_integer(this.crd, 12);
			}
			if (this.has_field.has_field(13))
			{
				this.serialize.write_integer(this.crr, 13);
			}
			if (this.has_field.has_field(14))
			{
				this.serialize.write_integer(this.defa, 14);
			}
			if (this.has_field.has_field(15))
			{
				this.serialize.write_integer(this.x, 15);
			}
			if (this.has_field.has_field(16))
			{
				this.serialize.write_integer(this.z, 16);
			}
			if (this.has_field.has_field(17))
			{
				this.serialize.write_integer(this.o, 17);
			}
			if (this.has_field.has_field(18))
			{
				this.serialize.write_integer(this.level, 18);
			}
			if (this.has_field.has_field(19))
			{
				this.serialize.write_integer(this.anti_stun, 19);
			}
			if (this.has_field.has_field(20))
			{
				this.serialize.write_integer(this.anti_knock_down, 20);
			}
			if (this.has_field.has_field(21))
			{
				this.serialize.write_string(this.player_name, 21);
			}
			if (this.has_field.has_field(22))
			{
				this.serialize.write_integer(this.guildId, 22);
			}
			if (this.has_field.has_field(23))
			{
				this.serialize.write_integer(this.teamid, 23);
			}
			if (this.has_field.has_field(24))
			{
				this.serialize.write_integer(this.dgea, 24);
			}
			if (this.has_field.has_field(25))
			{
				this.serialize.write_integer(this.resa, 25);
			}
			if (this.has_field.has_field(26))
			{
				this.serialize.write_integer(this.hita, 26);
			}
			if (this.has_field.has_field(27))
			{
				this.serialize.write_integer(this.cria, 27);
			}
			return this.serialize.close();
		}

		// Token: 0x04001BB7 RID: 7095
		private static int max_field_count = 28;

		// Token: 0x04001BB8 RID: 7096
		private long _id;

		// Token: 0x04001BB9 RID: 7097
		private string _npcdataid;

		// Token: 0x04001BBA RID: 7098
		private long _hp;

		// Token: 0x04001BBB RID: 7099
		private long _max_hp;

		// Token: 0x04001BBC RID: 7100
		private long _atk;

		// Token: 0x04001BBD RID: 7101
		private long _def;

		// Token: 0x04001BBE RID: 7102
		private long _hit;

		// Token: 0x04001BBF RID: 7103
		private long _eva;

		// Token: 0x04001BC0 RID: 7104
		private long _cri;

		// Token: 0x04001BC1 RID: 7105
		private long _exd;

		// Token: 0x04001BC2 RID: 7106
		private long _exr;

		// Token: 0x04001BC3 RID: 7107
		private long _res;

		// Token: 0x04001BC4 RID: 7108
		private long _crd;

		// Token: 0x04001BC5 RID: 7109
		private long _crr;

		// Token: 0x04001BC6 RID: 7110
		private long _defa;

		// Token: 0x04001BC7 RID: 7111
		private long _x;

		// Token: 0x04001BC8 RID: 7112
		private long _z;

		// Token: 0x04001BC9 RID: 7113
		private long _o;

		// Token: 0x04001BCA RID: 7114
		private long _level;

		// Token: 0x04001BCB RID: 7115
		private long _anti_stun;

		// Token: 0x04001BCC RID: 7116
		private long _anti_knock_down;

		// Token: 0x04001BCD RID: 7117
		private string _player_name;

		// Token: 0x04001BCE RID: 7118
		private long _guildId;

		// Token: 0x04001BCF RID: 7119
		private long _teamid;

		// Token: 0x04001BD0 RID: 7120
		private long _dgea;

		// Token: 0x04001BD1 RID: 7121
		private long _resa;

		// Token: 0x04001BD2 RID: 7122
		private long _hita;

		// Token: 0x04001BD3 RID: 7123
		private long _cria;
	}
}
