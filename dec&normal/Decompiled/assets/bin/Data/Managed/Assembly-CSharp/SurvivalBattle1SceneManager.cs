using System;
using SprotoType;

// Token: 0x0200088B RID: 2187
public class SurvivalBattle1SceneManager : SurvivalBattleSceneManager
{
	// Token: 0x06003B02 RID: 15106 RVA: 0x00100BDC File Offset: 0x000FEDDC
	public override void MoveToNextFloor()
	{
		if (this.mPlayerScores >= (long)this.surviveBattleData.SecondMinScore)
		{
			enter_survive_batttle.request request = new enter_survive_batttle.request();
			request.id = base.CurrentMapInofData.Param1;
			request.floor = 1L;
			NetLogic.GetInstance().Send<Protocol.enter_survive_batttle>(request, null);
		}
		else
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101590}", new object[]
			{
				this.surviveBattleData.SecondMinScore
			}), true, false);
		}
	}

	// Token: 0x06003B03 RID: 15107 RVA: 0x00100C5C File Offset: 0x000FEE5C
	public override void UpdatePlayerScore(long playerScores)
	{
		this.mPlayerScores = playerScores;
		if (SingletonUnity<SurviveBattleFloorInfoRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SurviveBattleFloorInfoRootLogic>.Instance.gameObject))
		{
			SingletonUnity<SurviveBattleFloorInfoRootLogic>.Instance.UpdateBtnEnable(this.mPlayerScores >= (long)this.surviveBattleData.SecondMinScore);
		}
		if (!this.mHasShowMessageBox && this.mPlayerScores >= (long)this.surviveBattleData.FirstMaxScore)
		{
			this.mHasShowMessageBox = true;
			MessageBoxLogic.OpenOKCancelBox("#{103001}", "#{100127}", delegate
			{
				this.MoveToNextFloor();
			}, null, null, null);
		}
	}
}
