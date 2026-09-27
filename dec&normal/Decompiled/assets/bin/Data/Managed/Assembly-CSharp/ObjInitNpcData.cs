using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000820 RID: 2080
public class ObjInitNpcData : ObjInitData
{
	// Token: 0x06003371 RID: 13169 RVA: 0x000CA16C File Offset: 0x000C836C
	public void InitData(npc_attribute npc_attribute)
	{
		this.mServerID = npc_attribute.id;
		this.mDir = MathUtil.HeadingToVector3((float)npc_attribute.o / 100f);
		this.mPos = new Vector3((float)npc_attribute.x / 100f, 0f, (float)npc_attribute.z / 100f);
		this.mPos = new Vector3(this.mPos.x, SceneManager.GetHitHeight(this.mPos), this.mPos.z);
		NpcData npcDataByID = DataManager.GetNpcDataByID(npc_attribute.npcdataid);
		this.HP = npc_attribute.hp;
		this.MaxHP = npc_attribute.max_hp;
		this.npcInfoData = npcDataByID;
		this.ATK = (int)npc_attribute.atk;
		this.DEF = (int)npc_attribute.def;
		this.HIT = (int)npc_attribute.hit;
		this.EVA = (int)npc_attribute.eva;
		this.CRI = (int)npc_attribute.cri;
		this.EXD = (int)npc_attribute.exd;
		this.EXR = (int)npc_attribute.exr;
		this.RES = (int)npc_attribute.res;
		this.CRD = (int)npc_attribute.crd;
		this.CRR = (int)npc_attribute.crr;
		this.DEFA = (int)npc_attribute.defa;
		this.DGEA = (int)npc_attribute.dgea;
		this.RESA = (int)npc_attribute.resa;
		this.HITA = (int)npc_attribute.hita;
		this.CRIA = (int)npc_attribute.cria;
		this.X = (int)npc_attribute.x;
		this.Z = (int)npc_attribute.z;
		this.O = (int)npc_attribute.o;
		this.Level = (int)npc_attribute.level;
		this.AntiStun = (int)npc_attribute.anti_stun;
		this.AntiKnockDown = (int)npc_attribute.anti_knock_down;
		this.PlayerName = npc_attribute.player_name;
		this.GuildId = ((!npc_attribute.HasGuildId) ? -1L : npc_attribute.guildId);
		this.TeamId = ((!npc_attribute.HasTeamid) ? -1L : npc_attribute.teamid);
	}

	// Token: 0x040021DD RID: 8669
	public NpcData npcInfoData;

	// Token: 0x040021DE RID: 8670
	public long HP;

	// Token: 0x040021DF RID: 8671
	public long MaxHP;

	// Token: 0x040021E0 RID: 8672
	public int ATK;

	// Token: 0x040021E1 RID: 8673
	public int DEF;

	// Token: 0x040021E2 RID: 8674
	public int HIT;

	// Token: 0x040021E3 RID: 8675
	public int EVA;

	// Token: 0x040021E4 RID: 8676
	public int CRI;

	// Token: 0x040021E5 RID: 8677
	public int EXD;

	// Token: 0x040021E6 RID: 8678
	public int EXR;

	// Token: 0x040021E7 RID: 8679
	public int RES;

	// Token: 0x040021E8 RID: 8680
	public int CRD;

	// Token: 0x040021E9 RID: 8681
	public int CRR;

	// Token: 0x040021EA RID: 8682
	public int DEFA;

	// Token: 0x040021EB RID: 8683
	public int DGEA;

	// Token: 0x040021EC RID: 8684
	public int RESA;

	// Token: 0x040021ED RID: 8685
	public int HITA;

	// Token: 0x040021EE RID: 8686
	public int CRIA;

	// Token: 0x040021EF RID: 8687
	public int X;

	// Token: 0x040021F0 RID: 8688
	public int Z;

	// Token: 0x040021F1 RID: 8689
	public int O;

	// Token: 0x040021F2 RID: 8690
	public int Level;

	// Token: 0x040021F3 RID: 8691
	public int AntiStun;

	// Token: 0x040021F4 RID: 8692
	public int AntiKnockDown;

	// Token: 0x040021F5 RID: 8693
	public string PathID = string.Empty;

	// Token: 0x040021F6 RID: 8694
	public string PlayerName = string.Empty;

	// Token: 0x040021F7 RID: 8695
	public new long GuildId = -1L;

	// Token: 0x040021F8 RID: 8696
	public long TeamId = -1L;
}
