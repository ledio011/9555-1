using System;
using SprotoType;
using UnityEngine;

// Token: 0x0200088C RID: 2188
public class SurvivalBattle2SceneManager : SurvivalBattleSceneManager
{
	// Token: 0x06003B06 RID: 15110 RVA: 0x00100D08 File Offset: 0x000FEF08
	public override void MoveToNextFloor()
	{
		Debug.Log("MoveToNextFloor");
		Debug.Log("CurrentMapInofData.Param1 !!!!!!!!!!!!!!" + base.CurrentMapInofData.Param1);
		MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{101592}", new object[]
		{
			this.surviveBattleData.FirstMaxScore
		}), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
		{
			enter_survive_batttle.request request = new enter_survive_batttle.request();
			request.id = base.CurrentMapInofData.Param1;
			request.floor = 0L;
			request.type = 1L;
			NetLogic.GetInstance().Send<Protocol.enter_survive_batttle>(request, null);
		}, null, null, null);
	}

	// Token: 0x06003B07 RID: 15111 RVA: 0x00100D80 File Offset: 0x000FEF80
	public override void OnLoadingOver()
	{
		base.OnLoadingOver();
		NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101587}", new object[0]), true, false);
	}
}
