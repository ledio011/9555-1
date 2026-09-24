using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000138 RID: 312
[Serializable]
public class CharacterAttributeData
{
	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x000551A0 File Offset: 0x000533A0
	// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x000551A8 File Offset: 0x000533A8
	public string Name
	{
		get
		{
			return this.mName;
		}
		set
		{
			this.mName = value;
		}
	}

	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x000551B4 File Offset: 0x000533B4
	// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x000551BC File Offset: 0x000533BC
	public string GuildName
	{
		get
		{
			return this.mGuildName;
		}
		set
		{
			this.mGuildName = value;
		}
	}

	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x000551C8 File Offset: 0x000533C8
	// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x000551D0 File Offset: 0x000533D0
	public long GuildId
	{
		get
		{
			return this.mGuildId;
		}
		set
		{
			this.mGuildId = value;
		}
	}

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x000551DC File Offset: 0x000533DC
	// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x000551E4 File Offset: 0x000533E4
	public long HP
	{
		get
		{
			return this.mHP;
		}
		set
		{
			this.mHP = value;
			if (this.mHP >= this.MaxHP)
			{
				this.mHP = this.MaxHP;
			}
			if (this.mHP <= 0L)
			{
				this.mHP = 0L;
			}
		}
	}

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x00055220 File Offset: 0x00053420
	// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x00055228 File Offset: 0x00053428
	public int Level
	{
		get
		{
			return this.mLevel;
		}
		set
		{
			this.mLevel = value;
		}
	}

	// Token: 0x170001EB RID: 491
	// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00055234 File Offset: 0x00053434
	// (set) Token: 0x06000BBB RID: 3003 RVA: 0x0005523C File Offset: 0x0005343C
	public long MaxHP
	{
		get
		{
			return this.mMaxHP;
		}
		set
		{
			this.mMaxHP = value;
		}
	}

	// Token: 0x170001EC RID: 492
	// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00055248 File Offset: 0x00053448
	// (set) Token: 0x06000BBD RID: 3005 RVA: 0x00055250 File Offset: 0x00053450
	public int ATK
	{
		get
		{
			return this.mATK;
		}
		set
		{
			this.mATK = value;
		}
	}

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0005525C File Offset: 0x0005345C
	// (set) Token: 0x06000BBF RID: 3007 RVA: 0x0005526C File Offset: 0x0005346C
	public float CurATK
	{
		get
		{
			return this.mCurATK;
		}
		set
		{
			this.mCurATK.value = value;
		}
	}

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0005527C File Offset: 0x0005347C
	// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x00055284 File Offset: 0x00053484
	public int HIT
	{
		get
		{
			return this.mHIT;
		}
		set
		{
			this.mHIT = value;
		}
	}

	// Token: 0x170001EF RID: 495
	// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00055290 File Offset: 0x00053490
	// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x000552A0 File Offset: 0x000534A0
	public float CurHIT
	{
		get
		{
			return this.mCurHIT;
		}
		set
		{
			this.mCurHIT.value = value;
		}
	}

	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x000552B0 File Offset: 0x000534B0
	// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x000552B8 File Offset: 0x000534B8
	public int CRI
	{
		get
		{
			return this.mCRI;
		}
		set
		{
			this.mCRI = value;
		}
	}

	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x000552C4 File Offset: 0x000534C4
	// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x000552D4 File Offset: 0x000534D4
	public float CurCRI
	{
		get
		{
			return this.mCurCRI;
		}
		set
		{
			this.mCurCRI.value = value;
		}
	}

	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x000552E4 File Offset: 0x000534E4
	// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x000552EC File Offset: 0x000534EC
	public float RES
	{
		get
		{
			return this.mRES;
		}
		set
		{
			this.mRES = value;
		}
	}

	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x06000BCA RID: 3018 RVA: 0x000552F8 File Offset: 0x000534F8
	// (set) Token: 0x06000BCB RID: 3019 RVA: 0x00055308 File Offset: 0x00053508
	public float CurRES
	{
		get
		{
			return this.mCurRES;
		}
		set
		{
			this.mCurRES.value = value;
		}
	}

	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00055318 File Offset: 0x00053518
	// (set) Token: 0x06000BCD RID: 3021 RVA: 0x00055320 File Offset: 0x00053520
	public int DEF
	{
		get
		{
			return this.mDEF;
		}
		set
		{
			this.mDEF = value;
		}
	}

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0005532C File Offset: 0x0005352C
	// (set) Token: 0x06000BCF RID: 3023 RVA: 0x0005533C File Offset: 0x0005353C
	public float CurDEF
	{
		get
		{
			return this.mCurDEF;
		}
		set
		{
			this.mCurDEF.value = value;
		}
	}

	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0005534C File Offset: 0x0005354C
	// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x00055354 File Offset: 0x00053554
	public int DGE
	{
		get
		{
			return this.mDGE;
		}
		set
		{
			this.mDGE = value;
		}
	}

	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00055360 File Offset: 0x00053560
	// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x00055370 File Offset: 0x00053570
	public float CurDGE
	{
		get
		{
			return this.mCurDGE;
		}
		set
		{
			this.mCurDGE.value = value;
		}
	}

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00055380 File Offset: 0x00053580
	// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x00055388 File Offset: 0x00053588
	public float EXD
	{
		get
		{
			return this.mEXD;
		}
		set
		{
			this.mEXD = value;
		}
	}

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00055394 File Offset: 0x00053594
	// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x000553A4 File Offset: 0x000535A4
	public float CurEXD
	{
		get
		{
			return this.mCurEXD;
		}
		set
		{
			this.mCurEXD.value = value;
		}
	}

	// Token: 0x170001FA RID: 506
	// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x000553B4 File Offset: 0x000535B4
	// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x000553BC File Offset: 0x000535BC
	public float EXR
	{
		get
		{
			return this.mEXR;
		}
		set
		{
			this.mEXR = value;
		}
	}

	// Token: 0x170001FB RID: 507
	// (get) Token: 0x06000BDA RID: 3034 RVA: 0x000553C8 File Offset: 0x000535C8
	// (set) Token: 0x06000BDB RID: 3035 RVA: 0x000553D8 File Offset: 0x000535D8
	public float CurEXR
	{
		get
		{
			return this.mCurEXR;
		}
		set
		{
			this.mCurEXR.value = value;
		}
	}

	// Token: 0x170001FC RID: 508
	// (get) Token: 0x06000BDC RID: 3036 RVA: 0x000553E8 File Offset: 0x000535E8
	// (set) Token: 0x06000BDD RID: 3037 RVA: 0x000553F8 File Offset: 0x000535F8
	public float CurCRD
	{
		get
		{
			return this.mCurCRD;
		}
		set
		{
			this.mCurCRD.value = value;
		}
	}

	// Token: 0x170001FD RID: 509
	// (get) Token: 0x06000BDE RID: 3038 RVA: 0x00055408 File Offset: 0x00053608
	// (set) Token: 0x06000BDF RID: 3039 RVA: 0x00055410 File Offset: 0x00053610
	public float CRD
	{
		get
		{
			return this.mCRD;
		}
		set
		{
			this.mCRD = value;
		}
	}

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0005541C File Offset: 0x0005361C
	// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x0005542C File Offset: 0x0005362C
	public float CurCRR
	{
		get
		{
			return this.mCurCRR;
		}
		set
		{
			this.mCurCRR.value = value;
		}
	}

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0005543C File Offset: 0x0005363C
	// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x00055444 File Offset: 0x00053644
	public float CRR
	{
		get
		{
			return this.mCRR;
		}
		set
		{
			this.mCRR = value;
		}
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00055450 File Offset: 0x00053650
	// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x00055458 File Offset: 0x00053658
	public long CurEXP
	{
		get
		{
			return this.mCurEXP;
		}
		set
		{
			this.mCurEXP = value;
		}
	}

	// Token: 0x17000201 RID: 513
	// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x00055464 File Offset: 0x00053664
	// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x0005546C File Offset: 0x0005366C
	public int DEFA
	{
		get
		{
			return this.mDEFA;
		}
		set
		{
			this.mDEFA = value;
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00055478 File Offset: 0x00053678
	// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x00055488 File Offset: 0x00053688
	public int CurDEFA
	{
		get
		{
			return this.mCurDEFA;
		}
		set
		{
			this.mCurDEFA.value = value;
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00055498 File Offset: 0x00053698
	// (set) Token: 0x06000BEB RID: 3051 RVA: 0x000554A0 File Offset: 0x000536A0
	public int DGEA
	{
		get
		{
			return this.mDGEA;
		}
		set
		{
			this.mDGEA = value;
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x06000BEC RID: 3052 RVA: 0x000554AC File Offset: 0x000536AC
	// (set) Token: 0x06000BED RID: 3053 RVA: 0x000554BC File Offset: 0x000536BC
	public int CurDGEA
	{
		get
		{
			return this.mCurDGEA;
		}
		set
		{
			this.mCurDGEA.value = value;
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06000BEE RID: 3054 RVA: 0x000554CC File Offset: 0x000536CC
	// (set) Token: 0x06000BEF RID: 3055 RVA: 0x000554D4 File Offset: 0x000536D4
	public int RESA
	{
		get
		{
			return this.mRESA;
		}
		set
		{
			this.mRESA = value;
		}
	}

	// Token: 0x17000206 RID: 518
	// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x000554E0 File Offset: 0x000536E0
	// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x000554F0 File Offset: 0x000536F0
	public int CurRESA
	{
		get
		{
			return this.mCurRESA;
		}
		set
		{
			this.mCurRESA.value = value;
		}
	}

	// Token: 0x17000207 RID: 519
	// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00055500 File Offset: 0x00053700
	// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x00055508 File Offset: 0x00053708
	public int HITA
	{
		get
		{
			return this.mHITA;
		}
		set
		{
			this.mHITA = value;
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00055514 File Offset: 0x00053714
	// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x00055524 File Offset: 0x00053724
	public int CurHITA
	{
		get
		{
			return this.mCurHITA;
		}
		set
		{
			this.mCurHITA.value = value;
		}
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00055534 File Offset: 0x00053734
	// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x0005553C File Offset: 0x0005373C
	public int CRIA
	{
		get
		{
			return this.mCRIA;
		}
		set
		{
			this.mCRIA = value;
		}
	}

	// Token: 0x1700020A RID: 522
	// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00055548 File Offset: 0x00053748
	// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x00055558 File Offset: 0x00053758
	public int CurCRIA
	{
		get
		{
			return this.mCurCRIA;
		}
		set
		{
			this.mCurCRIA.value = value;
		}
	}

	// Token: 0x1700020B RID: 523
	// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00055568 File Offset: 0x00053768
	// (set) Token: 0x06000BFB RID: 3067 RVA: 0x00055570 File Offset: 0x00053770
	public int ATE
	{
		get
		{
			return this.mATE;
		}
		set
		{
			this.mATE = value;
		}
	}

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0005557C File Offset: 0x0005377C
	// (set) Token: 0x06000BFD RID: 3069 RVA: 0x0005558C File Offset: 0x0005378C
	public int CurATE
	{
		get
		{
			return this.mCurATE;
		}
		set
		{
			this.mCurATE.value = value;
		}
	}

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0005559C File Offset: 0x0005379C
	// (set) Token: 0x06000BFF RID: 3071 RVA: 0x000555A4 File Offset: 0x000537A4
	public int SATM
	{
		get
		{
			return this.mSATM;
		}
		set
		{
			this.mSATM = value;
		}
	}

	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06000C00 RID: 3072 RVA: 0x000555B0 File Offset: 0x000537B0
	// (set) Token: 0x06000C01 RID: 3073 RVA: 0x000555C0 File Offset: 0x000537C0
	public int CurSATM
	{
		get
		{
			return this.mCurSATM;
		}
		set
		{
			this.mCurSATM.value = value;
		}
	}

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x06000C02 RID: 3074 RVA: 0x000555D0 File Offset: 0x000537D0
	// (set) Token: 0x06000C03 RID: 3075 RVA: 0x000555D8 File Offset: 0x000537D8
	public int SATC
	{
		get
		{
			return this.mSATC;
		}
		set
		{
			this.mSATC = value;
		}
	}

	// Token: 0x17000210 RID: 528
	// (get) Token: 0x06000C04 RID: 3076 RVA: 0x000555E4 File Offset: 0x000537E4
	// (set) Token: 0x06000C05 RID: 3077 RVA: 0x000555F4 File Offset: 0x000537F4
	public int CurSATC
	{
		get
		{
			return this.mCurSATC;
		}
		set
		{
			this.mCurSATC.value = value;
		}
	}

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00055604 File Offset: 0x00053804
	// (set) Token: 0x06000C07 RID: 3079 RVA: 0x0005560C File Offset: 0x0005380C
	public int SATP
	{
		get
		{
			return this.mSATP;
		}
		set
		{
			this.mSATP = value;
		}
	}

	// Token: 0x17000212 RID: 530
	// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00055618 File Offset: 0x00053818
	// (set) Token: 0x06000C09 RID: 3081 RVA: 0x00055628 File Offset: 0x00053828
	public int CurSATP
	{
		get
		{
			return this.mCurSATP;
		}
		set
		{
			this.mCurSATP.value = value;
		}
	}

	// Token: 0x17000213 RID: 531
	// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00055638 File Offset: 0x00053838
	// (set) Token: 0x06000C0B RID: 3083 RVA: 0x00055640 File Offset: 0x00053840
	public int CurTitleLevel
	{
		get
		{
			return this.mCurTitleLevel;
		}
		set
		{
			this.mCurTitleLevel = value;
		}
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0005564C File Offset: 0x0005384C
	// (set) Token: 0x06000C0D RID: 3085 RVA: 0x00055654 File Offset: 0x00053854
	public int CurTitleExp
	{
		get
		{
			return this.mCurTitleExp;
		}
		set
		{
			this.mCurTitleExp = value;
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00055660 File Offset: 0x00053860
	// (set) Token: 0x06000C0F RID: 3087 RVA: 0x00055668 File Offset: 0x00053868
	public int BLK
	{
		get
		{
			return this.mBLK;
		}
		set
		{
			this.mBLK = value;
		}
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00055674 File Offset: 0x00053874
	// (set) Token: 0x06000C11 RID: 3089 RVA: 0x0005567C File Offset: 0x0005387C
	public float CurBLK
	{
		get
		{
			return this.mCurBLK;
		}
		set
		{
			this.mCurBLK = value;
		}
	}

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00055688 File Offset: 0x00053888
	// (set) Token: 0x06000C13 RID: 3091 RVA: 0x00055698 File Offset: 0x00053898
	public float CurSpeed
	{
		get
		{
			return this.speedFloat1;
		}
		set
		{
			this.speedFloat1.value = value;
		}
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x06000C14 RID: 3092 RVA: 0x000556A8 File Offset: 0x000538A8
	// (set) Token: 0x06000C15 RID: 3093 RVA: 0x000556B8 File Offset: 0x000538B8
	public float Speed
	{
		get
		{
			return this.speedFloat;
		}
		set
		{
			this.speedFloat.value = value;
		}
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x06000C16 RID: 3094 RVA: 0x000556C8 File Offset: 0x000538C8
	// (set) Token: 0x06000C17 RID: 3095 RVA: 0x000556D8 File Offset: 0x000538D8
	public float WalkSpeed
	{
		get
		{
			return this.walkFloat;
		}
		set
		{
			this.walkFloat.value = value;
		}
	}

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x06000C18 RID: 3096 RVA: 0x000556E8 File Offset: 0x000538E8
	// (set) Token: 0x06000C19 RID: 3097 RVA: 0x000556F8 File Offset: 0x000538F8
	public int CurRec
	{
		get
		{
			return this.mCurRec;
		}
		set
		{
			this.mCurRec.value = value;
		}
	}

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00055708 File Offset: 0x00053908
	// (set) Token: 0x06000C1B RID: 3099 RVA: 0x00055710 File Offset: 0x00053910
	public int Rec
	{
		get
		{
			return this.mRec;
		}
		set
		{
			this.mRec = value;
		}
	}

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0005571C File Offset: 0x0005391C
	// (set) Token: 0x06000C1D RID: 3101 RVA: 0x0005572C File Offset: 0x0005392C
	public float CurAntiStun
	{
		get
		{
			return this.mCurAntiStun;
		}
		set
		{
			this.mCurAntiStun.value = value;
		}
	}

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0005573C File Offset: 0x0005393C
	// (set) Token: 0x06000C1F RID: 3103 RVA: 0x0005574C File Offset: 0x0005394C
	public float AntiStun
	{
		get
		{
			return this.mAntiStun;
		}
		set
		{
			this.mAntiStun.value = value;
		}
	}

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0005575C File Offset: 0x0005395C
	// (set) Token: 0x06000C21 RID: 3105 RVA: 0x0005576C File Offset: 0x0005396C
	public float CurAntiKnockDown
	{
		get
		{
			return this.mCurAntiKnockDown;
		}
		set
		{
			this.mCurAntiKnockDown.value = value;
		}
	}

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0005577C File Offset: 0x0005397C
	// (set) Token: 0x06000C23 RID: 3107 RVA: 0x00055784 File Offset: 0x00053984
	public float AntiKnockDown
	{
		get
		{
			return this.mAntiKnockDown;
		}
		set
		{
			this.mAntiKnockDown = value;
		}
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x00055790 File Offset: 0x00053990
	public bool IsChampionGuild()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsChampionGuild(this.mGuildId);
	}

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x06000C25 RID: 3109 RVA: 0x000557A8 File Offset: 0x000539A8
	// (set) Token: 0x06000C26 RID: 3110 RVA: 0x000557B0 File Offset: 0x000539B0
	public int ComboValue
	{
		get
		{
			return this.mComboValue;
		}
		set
		{
			this.mComboValue = value;
		}
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x000557BC File Offset: 0x000539BC
	public static int GetDamageByCar(ObjPlayerCar attackercar, ObjCharacter attackerObj, ObjCharacter defenderObj)
	{
		if (attackercar == null || defenderObj == null || attackerObj == null)
		{
			return 0;
		}
		float num = Mathf.Abs(attackercar.CurSpeed) * 100f;
		float num2 = attackercar.CurMountData.ATKValue;
		CharacterAttributeData attributeData = attackerObj.AttributeData;
		float num3 = Mathf.Min((attributeData.CurHIT + 1f) / ((float)attributeData.CurHITA + attributeData.CurHIT + 1f), 1f);
		float num4 = Mathf.Min((attributeData.CurCRI + 1f) / (attributeData.CurCRI + (float)attributeData.CurCRIA + 1f), 0.9f) * attributeData.CurCRD;
		float num5 = attributeData.CurATK * num3 * (attributeData.CurEXD + 1f) * (1f + num4) * num2 * (1f + num / 1000f);
		return (int)num5;
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x000558AC File Offset: 0x00053AAC
	public static int GetDamage(ObjCharacter attacker, ObjCharacter defender, string SkillId, EffInfoData effinfoData, out bool isHit, out bool isCri)
	{
		float num = (float)effinfoData.Damagex;
		float num2 = effinfoData.DamageMulti_100f;
		float num3 = 1f;
		isHit = false;
		isCri = false;
		if (attacker == null || defender == null)
		{
			return 0;
		}
		if (attacker.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER || attacker.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_PLAYER || attacker.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_RAGDOLL)
		{
			CharacterSkillData characterSkillDataByID = attacker.GetCharacterSkillDataByID(SkillId);
			if (characterSkillDataByID == null)
			{
				return 0;
			}
			if (characterSkillDataByID.Level > 0)
			{
				SkillData skillDataById = DataManager.GetSkillDataById(SkillId);
				num = (float)(effinfoData.Damagex + effinfoData.DamageAdd * characterSkillDataByID.Level);
				num2 = effinfoData.DamageMulti_100f + effinfoData.DamageMultiAdd_f * (float)characterSkillDataByID.Level;
			}
			if (defender.ObjType == GameDefine.OBJ_TYPE.OBJ_OTHER_PLAYER || defender.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_PLAYER || defender.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER || defender.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_RAGDOLL)
			{
				if (attacker.AttributeData.ComboValue > defender.AttributeData.ComboValue)
				{
					num3 = PlayerCommonData.PvpScale + PlayerCommonData.PvpScaleAdd;
				}
				else
				{
					num3 = PlayerCommonData.PvpScale;
				}
			}
		}
		return CharacterAttributeData.GetDamage(attacker, defender, effinfoData, num * num3, num2 * num3, out isHit, out isCri);
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x000559E8 File Offset: 0x00053BE8
	public static int GetDamage(ObjCharacter attackerObj, ObjCharacter defenderObj, EffInfoData effinfoData, float skillDamge, float skillScale, out bool isHit, out bool isCri)
	{
		CharacterAttributeData attributeData = attackerObj.AttributeData;
		CharacterAttributeData attributeData2 = defenderObj.AttributeData;
		isHit = false;
		isCri = false;
		float num = 1f;
		if (attributeData == null || attributeData2 == null)
		{
			return 0;
		}
		float skillTypeValue = GameDefine.GetSkillTypeValue(effinfoData, attackerObj.ObjType, SKILL_ADD_TYPE.SHIT);
		if (!CharacterAttributeData.IsDodeg(attributeData, attributeData2, attackerObj.ObjType, skillTypeValue))
		{
			return 0;
		}
		float skillTypeValue2 = GameDefine.GetSkillTypeValue(effinfoData, attackerObj.ObjType, SKILL_ADD_TYPE.SCRI);
		isCri = CharacterAttributeData.IsCri(attributeData, attributeData2, attackerObj.ObjType, skillTypeValue2);
		if (attackerObj.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER || attackerObj.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_PLAYER || attackerObj.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_RAGDOLL)
		{
			if (defenderObj.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC_CAR || defenderObj.ObjType == GameDefine.OBJ_TYPE.OBJ_PLAYER_CAR)
			{
				skillScale += GameDefine.GetSkillTypeValue(effinfoData, attackerObj.ObjType, SKILL_ADD_TYPE.SATC);
				skillDamge += (float)attributeData.CurSATC;
			}
			else if (defenderObj.ObjType == GameDefine.OBJ_TYPE.OBJ_NPC)
			{
				ObjNPC objNPC = defenderObj as ObjNPC;
				if (objNPC.NPCData.FunctionType == 10)
				{
					skillScale += GameDefine.GetSkillTypeValue(effinfoData, attackerObj.ObjType, SKILL_ADD_TYPE.SATC);
					skillDamge += (float)attributeData.CurSATC;
				}
				else
				{
					skillScale += GameDefine.GetSkillTypeValue(effinfoData, attackerObj.ObjType, SKILL_ADD_TYPE.SATM);
					skillDamge += (float)attributeData.CurSATM;
				}
			}
			else
			{
				skillScale += GameDefine.GetSkillTypeValue(effinfoData, attackerObj.ObjType, SKILL_ADD_TYPE.SATP);
				skillDamge += (float)attributeData.CurSATP;
			}
		}
		float num2 = attributeData.CurATK * skillScale + skillDamge;
		float num3 = Mathf.Min((attributeData2.CurDEF + 1f) / (attributeData2.CurDEF + (float)attributeData.CurDEFA), 0.5f);
		if (isCri)
		{
			num = Mathf.Max(1f, Mathf.Min(1f + (attributeData.CurCRD - attributeData2.CurCRR), 2f));
		}
		isHit = true;
		float num4 = (float)PlayerCommonData.GetRandom(attackerObj.ObjType) / 1000f + 0.95f;
		float skillTypeValue3 = GameDefine.GetSkillTypeValue(effinfoData, attackerObj.ObjType, SKILL_ADD_TYPE.SEXD);
		float num5 = num * num2 * num4 * (1f - num3) * (1f + (attributeData.CurEXD - attributeData2.CurEXR + skillTypeValue3));
		return Mathf.CeilToInt(num5);
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x00055C2C File Offset: 0x00053E2C
	public static int BuffUseState(ObjCharacter attacker, CharacterAttributeData defender, string SkillId, EffInfoData effinfoData, float value, BUFF_TYPE type)
	{
		if (type == BUFF_TYPE.CHANGE_ATTR || type == BUFF_TYPE.INVINCIBLE)
		{
			return GameDefine.BUF_USE_SUCCESS;
		}
		float buffProbFloat = effinfoData.BuffProbFloat;
		if ((attacker.ObjType == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER || attacker.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_PLAYER || attacker.ObjType == GameDefine.OBJ_TYPE.OBJ_ZOMBIE_RAGDOLL) && attacker.GetCharacterSkillDataByID(SkillId) == null)
		{
			return GameDefine.BUF_USE_FAIL;
		}
		float num = 0f;
		if (type == BUFF_TYPE.KNOCK_DOWN)
		{
			num = defender.CurAntiKnockDown;
		}
		else if (type == BUFF_TYPE.STUN)
		{
			num = defender.CurAntiStun;
		}
		if (buffProbFloat - num >= value / 100f)
		{
			return GameDefine.BUF_USE_SUCCESS;
		}
		return GameDefine.BUF_USE_FAIL;
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x00055CD4 File Offset: 0x00053ED4
	public static bool IsDodeg(CharacterAttributeData attacker, CharacterAttributeData defender, GameDefine.OBJ_TYPE type, float add = 0f)
	{
		if (attacker == null || defender == null)
		{
			return false;
		}
		float num = Mathf.Min((attacker.CurHIT + 1f) / ((float)attacker.CurHITA + attacker.CurHIT + 1f), 1f);
		float num2 = Mathf.Min((defender.CurDGE + 1f) / (defender.CurDGE + (float)defender.CurDGEA + 1f), 0.5f);
		float num3 = 1f + num - num2 + add;
		float num4 = (float)PlayerCommonData.GetRandom(type) / 100f;
		return num3 >= num4;
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00055D6C File Offset: 0x00053F6C
	public static bool IsCri(CharacterAttributeData attacker, CharacterAttributeData defender, GameDefine.OBJ_TYPE type, float add = 0f)
	{
		if (attacker == null || defender == null)
		{
			return false;
		}
		float num = Mathf.Min((attacker.CurCRI + 1f) / (attacker.CurCRI + (float)attacker.CurCRIA + 1f), 0.9f);
		float num2 = Mathf.Min((defender.CurRES + 1f) / (defender.CurRES + (float)defender.CurRESA + 1f), 0.8f);
		float num3 = num - num2 + add;
		float num4 = (float)PlayerCommonData.GetRandom(type) / 100f;
		return num3 >= num4;
	}

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x06000C2D RID: 3117 RVA: 0x00055E00 File Offset: 0x00054000
	// (set) Token: 0x06000C2E RID: 3118 RVA: 0x00055E08 File Offset: 0x00054008
	public int RefineNeckLevel
	{
		get
		{
			return this.mRefineNeckLevel;
		}
		set
		{
			this.mRefineNeckLevel = value;
		}
	}

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x06000C2F RID: 3119 RVA: 0x00055E14 File Offset: 0x00054014
	// (set) Token: 0x06000C30 RID: 3120 RVA: 0x00055E1C File Offset: 0x0005401C
	public int RefineRing1Level
	{
		get
		{
			return this.mRefineRing1Level;
		}
		set
		{
			this.mRefineRing1Level = value;
		}
	}

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x06000C31 RID: 3121 RVA: 0x00055E28 File Offset: 0x00054028
	// (set) Token: 0x06000C32 RID: 3122 RVA: 0x00055E30 File Offset: 0x00054030
	public int RefineRing2Level
	{
		get
		{
			return this.mRefineRing2Level;
		}
		set
		{
			this.mRefineRing2Level = value;
		}
	}

	// Token: 0x17000224 RID: 548
	// (get) Token: 0x06000C33 RID: 3123 RVA: 0x00055E3C File Offset: 0x0005403C
	// (set) Token: 0x06000C34 RID: 3124 RVA: 0x00055E44 File Offset: 0x00054044
	public int RefineBeltLevel
	{
		get
		{
			return this.mRefineBeltLevel;
		}
		set
		{
			this.mRefineBeltLevel = value;
		}
	}

	// Token: 0x17000225 RID: 549
	// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00055E50 File Offset: 0x00054050
	// (set) Token: 0x06000C36 RID: 3126 RVA: 0x00055E58 File Offset: 0x00054058
	public int RefineLevel
	{
		get
		{
			return this.mRefineLevel;
		}
		set
		{
			this.mRefineLevel = value;
		}
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x00055E64 File Offset: 0x00054064
	public int GetTargetRefinePartLevel(REFINE_PART targetPart)
	{
		switch (targetPart)
		{
		case REFINE_PART.NECK:
			return this.mRefineNeckLevel;
		case REFINE_PART.RING1:
			return this.mRefineRing1Level;
		case REFINE_PART.RING2:
			return this.mRefineRing2Level;
		case REFINE_PART.BELT:
			return this.mRefineBeltLevel;
		default:
			return -1;
		}
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x00055EB0 File Offset: 0x000540B0
	public void SetTargetRefinePartLevel(REFINE_PART targetPart, int level)
	{
		switch (targetPart)
		{
		case REFINE_PART.NECK:
			this.mRefineNeckLevel = level;
			return;
		case REFINE_PART.RING1:
			this.mRefineRing1Level = level;
			return;
		case REFINE_PART.RING2:
			this.mRefineRing2Level = level;
			return;
		case REFINE_PART.BELT:
			this.mRefineBeltLevel = level;
			return;
		default:
			return;
		}
	}

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00055EFC File Offset: 0x000540FC
	public int[] EquipEnhanceList
	{
		get
		{
			return this.mEquipEnhanceList;
		}
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x00055F04 File Offset: 0x00054104
	public int GetEquipEnhanceLevel(EQUIP_BACKPACK_TYPE targetType)
	{
		return this.GetEquipEnhanceLevel((int)targetType);
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x00055F10 File Offset: 0x00054110
	public int GetEquipEnhanceLevel(int targetType)
	{
		return this.EquipEnhanceList[targetType];
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x00055F1C File Offset: 0x0005411C
	public void SetEquipEnhanceLevel(EQUIP_BACKPACK_TYPE targetType, int level)
	{
		this.SetEquipEnhanceLevel((int)targetType, level);
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x00055F28 File Offset: 0x00054128
	public void SetEquipEnhanceLevel(int targetType, int level)
	{
		if (this.EquipEnhanceList[targetType] < level)
		{
			this.EquipEnhanceList[targetType] = level;
		}
	}

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x06000C3E RID: 3134 RVA: 0x00055F44 File Offset: 0x00054144
	// (set) Token: 0x06000C3F RID: 3135 RVA: 0x00055F54 File Offset: 0x00054154
	public virtual int PkMode
	{
		get
		{
			return this.mPkMode.value;
		}
		set
		{
			this.mPkMode.value = value;
		}
	}

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00055F64 File Offset: 0x00054164
	// (set) Token: 0x06000C41 RID: 3137 RVA: 0x00055F6C File Offset: 0x0005416C
	public virtual GameDefine.CAMP_TYPE Camp
	{
		get
		{
			return this.mCamp;
		}
		set
		{
			this.mCamp = value;
		}
	}

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00055F78 File Offset: 0x00054178
	// (set) Token: 0x06000C43 RID: 3139 RVA: 0x00055F80 File Offset: 0x00054180
	public int DanceState
	{
		get
		{
			return this.mDanceState;
		}
		set
		{
			this.mDanceState = value;
		}
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00055F8C File Offset: 0x0005418C
	// (set) Token: 0x06000C45 RID: 3141 RVA: 0x00055F94 File Offset: 0x00054194
	public string DanceId
	{
		get
		{
			return this.mDanceId;
		}
		set
		{
			this.mDanceId = value;
		}
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00055FA0 File Offset: 0x000541A0
	public void InitData(attribute Attribute, attribute AttributeAll)
	{
		this.CurATK = (float)((int)AttributeAll.atk);
		this.CurDEF = (float)((int)AttributeAll.def);
		this.CurHIT = (float)((int)AttributeAll.hit);
		this.CurDGE = (float)((int)AttributeAll.eva);
		this.CurCRI = (float)((int)AttributeAll.cri);
		this.CurEXD = (float)AttributeAll.exd / 10000f;
		this.CurEXR = (float)AttributeAll.exr / 10000f;
		this.CurRES = (float)((int)AttributeAll.res);
		this.CurCRD = (float)AttributeAll.crd / 10000f;
		this.CurCRR = (float)AttributeAll.crr / 10000f;
		this.CurDEFA = (int)AttributeAll.defa;
		this.CurDGEA = (int)AttributeAll.dgea;
		this.CurRESA = (int)AttributeAll.resa;
		this.CurHITA = (int)AttributeAll.hita;
		this.CurCRIA = (int)AttributeAll.cria;
		this.CurATE = (int)AttributeAll.ate;
		this.CurSATM = (int)AttributeAll.satm;
		this.CurSATC = (int)AttributeAll.satc;
		this.CurSATP = (int)AttributeAll.satp;
		this.CurAntiStun = (float)AttributeAll.anti_stun / 10000f;
		this.CurAntiKnockDown = (float)AttributeAll.anti_knock_down / 10000f;
		this.MaxHP = Attribute.max_hp;
		this.ATK = (int)Attribute.atk;
		this.DEF = (int)Attribute.def;
		this.HIT = (int)Attribute.hit;
		this.DGE = (int)Attribute.eva;
		this.CRI = (int)Attribute.cri;
		this.EXD = (float)((int)Attribute.exd) / 10000f;
		this.EXR = (float)((int)Attribute.exr) / 10000f;
		this.RES = (float)((int)Attribute.res);
		this.CRD = (float)Attribute.crd / 10000f;
		this.CRR = (float)Attribute.crr / 10000f;
		this.DEFA = (int)Attribute.defa;
		this.DGEA = (int)Attribute.dgea;
		this.RESA = (int)Attribute.resa;
		this.HITA = (int)Attribute.hita;
		this.CRIA = (int)Attribute.cria;
		this.ATE = (int)Attribute.ate;
		this.SATM = (int)Attribute.satm;
		this.SATC = (int)Attribute.satc;
		this.SATP = (int)Attribute.satp;
		this.AntiStun = (float)Attribute.anti_stun / 10000f;
		this.AntiKnockDown = (float)Attribute.anti_knock_down / 10000f;
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x0005622C File Offset: 0x0005442C
	public void InitData(character_aoi_attribute character, bool isMainPlayer)
	{
		if (character.HasAttribute)
		{
			this.ATK = (int)character.attribute.atk;
			this.DEF = (int)character.attribute.def;
			this.HIT = (int)character.attribute.hit;
			this.DGE = (int)character.attribute.eva;
			this.CRI = (int)character.attribute.cri;
			this.EXD = (float)character.attribute.exd / 10000f;
			this.EXR = (float)character.attribute.exr / 10000f;
			this.CRD = (float)character.attribute.crd / 10000f;
			this.CRR = (float)character.attribute.crr / 10000f;
			this.DEFA = (int)character.attribute.defa;
			this.ATE = (int)character.attribute.ate;
			this.SATM = (int)character.attribute.satm;
			this.SATC = (int)character.attribute.satc;
			this.SATP = (int)character.attribute.satp;
			this.DGEA = (int)character.attribute.dgea;
			this.RESA = (int)character.attribute.resa;
			this.HITA = (int)character.attribute.hita;
			this.CRIA = (int)character.attribute.cria;
			this.RES = (float)((int)character.attribute.res);
			this.Speed = (float)character.attribute.mov / 100f;
			this.Rec = (int)character.attribute.rec;
			this.AntiStun = (float)character.attribute.anti_stun / 10000f;
			this.AntiKnockDown = (float)character.attribute.anti_knock_down / 10000f;
		}
		if (character.HasAttribute_other)
		{
			this.CurEXP = character.attribute_other.exp;
			int level = this.Level;
			this.Level = (int)character.attribute_other.level;
			if (this.Level > level && isMainPlayer)
			{
				if (UIUpdateEvent.LevelUpEvent != null)
				{
					UIUpdateEvent.LevelUpEvent();
				}
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.UpdateEnhanceTips();
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.UpdateTips();
				if (SingletonUnity<FunctionBtnRootLogic>.Exists)
				{
					SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateSkillTips();
				}
				if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager != null)
				{
					SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.RefershMapActivity();
				}
			}
			this.CurTitleLevel = (int)character.attribute_other.title_level;
			this.CurTitleExp = (int)character.attribute_other.title_exp;
			this.ComboValue = (int)character.attribute_other.combValue;
			this.RefineNeckLevel = (int)character.attribute_other.refineNeckLevel;
			this.RefineRing1Level = (int)character.attribute_other.refineRing1Level;
			this.RefineRing2Level = (int)character.attribute_other.refineRing2Level;
			this.RefineBeltLevel = (int)character.attribute_other.refineBeltLevel;
			this.RefineLevel = (int)character.attribute_other.refineLevel;
			GameDefine.CAMP_TYPE camp_TYPE = (GameDefine.CAMP_TYPE)character.attribute_other.camp;
			if (camp_TYPE != this.Camp)
			{
				ObjCharacter objCharacter = Singleton<ObjManager>.Instance.FindObjInScene(character.id);
				if (objCharacter != null)
				{
					Singleton<ObjManager>.Instance.ChangeCamp(objCharacter, this.Camp, camp_TYPE);
				}
				this.Camp = camp_TYPE;
			}
			this.PkMode = (int)character.attribute_other.pkMode;
			this.DanceState = (int)character.attribute_other.dance_state;
			this.DanceId = character.attribute_other.dance_id;
			if (character.attribute_other.HasGuildId)
			{
				this.GuildName = character.attribute_other.guildName;
				this.GuildId = character.attribute_other.guildId;
			}
			else
			{
				this.GuildName = string.Empty;
				this.GuildId = -1L;
			}
		}
		if (character.HasAttribute_all)
		{
			this.CurATK = (float)((int)character.attribute_all.atk);
			this.CurDEF = (float)((int)character.attribute_all.def);
			this.CurHIT = (float)character.attribute_all.hit;
			this.CurDGE = (float)character.attribute_all.eva;
			this.CurCRI = (float)character.attribute_all.cri;
			this.CurEXD = (float)character.attribute_all.exd / 10000f;
			this.CurEXR = (float)character.attribute_all.exr / 10000f;
			this.CurCRD = (float)character.attribute_all.crd / 10000f;
			this.CurCRR = (float)character.attribute_all.crr / 10000f;
			this.CurDEFA = (int)character.attribute_all.defa;
			this.CurDGEA = (int)character.attribute_all.dgea;
			this.CurRESA = (int)character.attribute_all.resa;
			this.CurHITA = (int)character.attribute_all.hita;
			this.CurCRIA = (int)character.attribute_all.cria;
			this.CurATE = (int)character.attribute_all.ate;
			this.CurSATM = (int)character.attribute_all.satm;
			this.CurSATC = (int)character.attribute_all.satc;
			this.CurSATP = (int)character.attribute_all.satp;
			this.CurRES = (float)((int)character.attribute_all.res);
			this.CurSpeed = (float)character.attribute_all.mov / 100f;
			this.CurRec = (int)character.attribute_all.rec;
			this.CurAntiStun = (float)character.attribute_all.anti_stun / 10000f;
			this.CurAntiKnockDown = (float)character.attribute_all.anti_knock_down / 10000f;
		}
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x000567F4 File Offset: 0x000549F4
	public void ReName(string newname)
	{
		this.Name = newname;
	}

	// Token: 0x04000A66 RID: 2662
	private string mName = string.Empty;

	// Token: 0x04000A67 RID: 2663
	private string mGuildName = string.Empty;

	// Token: 0x04000A68 RID: 2664
	private long mGuildId = -1L;

	// Token: 0x04000A69 RID: 2665
	private long mHP = -1L;

	// Token: 0x04000A6A RID: 2666
	private int mLevel = 1;

	// Token: 0x04000A6B RID: 2667
	private long mMaxHP = long.MaxValue;

	// Token: 0x04000A6C RID: 2668
	private int mATK;

	// Token: 0x04000A6D RID: 2669
	private XorFloat mCurATK = new XorFloat();

	// Token: 0x04000A6E RID: 2670
	private int mHIT;

	// Token: 0x04000A6F RID: 2671
	private XorFloat mCurHIT = new XorFloat();

	// Token: 0x04000A70 RID: 2672
	private int mCRI;

	// Token: 0x04000A71 RID: 2673
	private XorFloat mCurCRI = new XorFloat();

	// Token: 0x04000A72 RID: 2674
	private float mRES;

	// Token: 0x04000A73 RID: 2675
	private XorFloat mCurRES = new XorFloat();

	// Token: 0x04000A74 RID: 2676
	private int mDEF;

	// Token: 0x04000A75 RID: 2677
	private XorFloat mCurDEF = new XorFloat();

	// Token: 0x04000A76 RID: 2678
	private int mDGE;

	// Token: 0x04000A77 RID: 2679
	private XorFloat mCurDGE = new XorFloat();

	// Token: 0x04000A78 RID: 2680
	private float mEXD;

	// Token: 0x04000A79 RID: 2681
	private XorFloat mCurEXD = new XorFloat();

	// Token: 0x04000A7A RID: 2682
	private float mEXR;

	// Token: 0x04000A7B RID: 2683
	private XorFloat mCurEXR = new XorFloat();

	// Token: 0x04000A7C RID: 2684
	private XorFloat mCurCRD = new XorFloat();

	// Token: 0x04000A7D RID: 2685
	private float mCRD;

	// Token: 0x04000A7E RID: 2686
	private XorFloat mCurCRR = new XorFloat();

	// Token: 0x04000A7F RID: 2687
	private float mCRR;

	// Token: 0x04000A80 RID: 2688
	public long mCurEXP;

	// Token: 0x04000A81 RID: 2689
	public int mDEFA;

	// Token: 0x04000A82 RID: 2690
	private XorInt mCurDEFA = new XorInt();

	// Token: 0x04000A83 RID: 2691
	public int mDGEA;

	// Token: 0x04000A84 RID: 2692
	private XorInt mCurDGEA = new XorInt();

	// Token: 0x04000A85 RID: 2693
	public int mRESA;

	// Token: 0x04000A86 RID: 2694
	private XorInt mCurRESA = new XorInt();

	// Token: 0x04000A87 RID: 2695
	public int mHITA;

	// Token: 0x04000A88 RID: 2696
	private XorInt mCurHITA = new XorInt();

	// Token: 0x04000A89 RID: 2697
	public int mCRIA;

	// Token: 0x04000A8A RID: 2698
	private XorInt mCurCRIA = new XorInt();

	// Token: 0x04000A8B RID: 2699
	public int mATE;

	// Token: 0x04000A8C RID: 2700
	private XorInt mCurATE = new XorInt();

	// Token: 0x04000A8D RID: 2701
	public int mSATM;

	// Token: 0x04000A8E RID: 2702
	private XorInt mCurSATM = new XorInt();

	// Token: 0x04000A8F RID: 2703
	public int mSATC;

	// Token: 0x04000A90 RID: 2704
	private XorInt mCurSATC = new XorInt();

	// Token: 0x04000A91 RID: 2705
	public int mSATP;

	// Token: 0x04000A92 RID: 2706
	private XorInt mCurSATP = new XorInt();

	// Token: 0x04000A93 RID: 2707
	private int mCurTitleLevel;

	// Token: 0x04000A94 RID: 2708
	private int mCurTitleExp;

	// Token: 0x04000A95 RID: 2709
	private int mBLK;

	// Token: 0x04000A96 RID: 2710
	private float mCurBLK;

	// Token: 0x04000A97 RID: 2711
	private XorFloat speedFloat1 = new XorFloat();

	// Token: 0x04000A98 RID: 2712
	private XorFloat speedFloat = new XorFloat();

	// Token: 0x04000A99 RID: 2713
	private XorFloat walkFloat = new XorFloat();

	// Token: 0x04000A9A RID: 2714
	private XorInt mCurRec = new XorInt();

	// Token: 0x04000A9B RID: 2715
	public int mRec;

	// Token: 0x04000A9C RID: 2716
	private XorFloat mCurAntiStun = new XorFloat();

	// Token: 0x04000A9D RID: 2717
	private XorFloat mAntiStun = new XorFloat();

	// Token: 0x04000A9E RID: 2718
	private XorFloat mCurAntiKnockDown = new XorFloat();

	// Token: 0x04000A9F RID: 2719
	public float mAntiKnockDown;

	// Token: 0x04000AA0 RID: 2720
	private int mComboValue;

	// Token: 0x04000AA1 RID: 2721
	private int mRefineNeckLevel;

	// Token: 0x04000AA2 RID: 2722
	private int mRefineRing1Level;

	// Token: 0x04000AA3 RID: 2723
	private int mRefineRing2Level;

	// Token: 0x04000AA4 RID: 2724
	private int mRefineBeltLevel;

	// Token: 0x04000AA5 RID: 2725
	private int mRefineLevel;

	// Token: 0x04000AA6 RID: 2726
	private int[] mEquipEnhanceList = new int[6];

	// Token: 0x04000AA7 RID: 2727
	private XorInt mPkMode = new XorInt();

	// Token: 0x04000AA8 RID: 2728
	private GameDefine.CAMP_TYPE mCamp;

	// Token: 0x04000AA9 RID: 2729
	private int mDanceState;

	// Token: 0x04000AAA RID: 2730
	private string mDanceId;
}
