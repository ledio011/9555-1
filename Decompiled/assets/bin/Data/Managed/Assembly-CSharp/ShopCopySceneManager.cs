using System;
using System.Collections.Generic;

// Token: 0x02000887 RID: 2183
public class ShopCopySceneManager : SceneManager
{
	// Token: 0x06003ADB RID: 15067 RVA: 0x000FFA1C File Offset: 0x000FDC1C
	public override void Init(string id)
	{
		base.Init(id);
		this.CreateNpc();
	}

	// Token: 0x06003ADC RID: 15068 RVA: 0x000FFA2C File Offset: 0x000FDC2C
	public override void OnLoadingOver()
	{
		base.OnLoadingOver();
	}

	// Token: 0x06003ADD RID: 15069 RVA: 0x000FFA34 File Offset: 0x000FDC34
	public void CreateNpc()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		List<MonsterData> monsterDataList = base.MonsterDataList;
		for (int i = 0; i < monsterDataList.Count; i++)
		{
			if (DataManager.GetNpcDataByID(monsterDataList[i].NpcID) != null)
			{
				this.GetNpc(monsterDataList[i]);
			}
		}
	}

	// Token: 0x06003ADE RID: 15070 RVA: 0x000FFA94 File Offset: 0x000FDC94
	public void GetNpc(MonsterData curData)
	{
		ObjInitNpcData objInitNpcData = new ObjInitNpcData();
		objInitNpcData.mServerID = UUID.GenUUID();
		objInitNpcData.mPos = curData.GetNpcPos();
		objInitNpcData.mDir = MathUtil.HeadingToVector3(curData.PositionO);
		objInitNpcData.npcInfoData = DataManager.GetNpcDataByID(curData.NpcID);
		objInitNpcData.mCharacterModelId = objInitNpcData.npcInfoData.Model;
		objInitNpcData.MaxHP = objInitNpcData.npcInfoData.Hp;
		objInitNpcData.HP = objInitNpcData.npcInfoData.Hp;
		objInitNpcData.ATK = objInitNpcData.npcInfoData.Atk;
		objInitNpcData.DEF = objInitNpcData.npcInfoData.Def;
		objInitNpcData.HIT = objInitNpcData.npcInfoData.HIT;
		objInitNpcData.EVA = objInitNpcData.npcInfoData.DGE;
		objInitNpcData.CRI = objInitNpcData.npcInfoData.CRI;
		objInitNpcData.EXD = objInitNpcData.npcInfoData.EXD;
		objInitNpcData.EXR = objInitNpcData.npcInfoData.EXR;
		objInitNpcData.RES = objInitNpcData.npcInfoData.RES;
		objInitNpcData.CRD = objInitNpcData.npcInfoData.CRD;
		objInitNpcData.CRR = objInitNpcData.npcInfoData.CRR;
		objInitNpcData.DEFA = objInitNpcData.npcInfoData.DEFA;
		objInitNpcData.DGEA = objInitNpcData.npcInfoData.DGEA;
		objInitNpcData.HITA = objInitNpcData.npcInfoData.HITA;
		objInitNpcData.RESA = objInitNpcData.npcInfoData.RESA;
		objInitNpcData.CRIA = objInitNpcData.npcInfoData.CRIA;
		objInitNpcData.Level = objInitNpcData.npcInfoData.Lv;
		objInitNpcData.AntiStun = objInitNpcData.npcInfoData.AntiStun;
		objInitNpcData.AntiKnockDown = objInitNpcData.npcInfoData.AntiKnockDown;
		objInitNpcData.PathID = curData.PathId;
		Singleton<ObjManager>.Instance.CreateNPC(objInitNpcData, null, null);
	}
}
