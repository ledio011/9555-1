using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000924 RID: 2340
public class CarRewardPageRootLogic : SingletonUnity<CarRewardPageRootLogic>
{
	// Token: 0x06004103 RID: 16643 RVA: 0x0013439C File Offset: 0x0013259C
	public void ResetCarRewardPageRoot(car_copy_result.request request)
	{
		this.CurTimeLabel.text = TimeTools.GetCentiSecondStr((int)request.parm);
		NGUITools.SetActive(this.NewRecordObj, request.new_record == 1L);
		this.Id = request.id;
		if (request.rankPos1 == -1L || request.rankPos2 == -1L)
		{
			NGUITools.SetActive(this.CarNotRankRoot, true);
			NGUITools.SetActive(this.CarRankRoot, false);
		}
		else
		{
			NGUITools.SetActive(this.CarNotRankRoot, false);
			NGUITools.SetActive(this.CarRankRoot, true);
			NGUITools.SetActive(this.RankUpObj, request.rankPos2 < request.rankPos1);
			this.PreRankLabel.text = request.rankPos1.ToString();
			this.CurRankLabel.text = request.rankPos2.ToString();
		}
		this.CarShowRewardItem.ShowRewards(request.items);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("DailyCopy", string.Format("copy_{0}", this.Id), "success");
	}

	// Token: 0x06004104 RID: 16644 RVA: 0x001344B0 File Offset: 0x001326B0
	public void OnClickContinueBtn()
	{
		NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
	}

	// Token: 0x06004105 RID: 16645 RVA: 0x001344C0 File Offset: 0x001326C0
	public void OnClickRepeatBtn()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		copyscene_info copyinfoByID = playerData.CopyInfoData.GetCopyinfoByID(this.Id);
		int num = 0;
		if (copyinfoByID != null)
		{
			num = (int)copyinfoByID.CurNum;
		}
		if (num <= 0)
		{
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{102057}", new object[0]), true, false);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
		enter_copy_scene.request request = new enter_copy_scene.request();
		request.mapInfoId = this.Id;
		NetLogic.GetInstance().Send<Protocol.enter_copy_scene>(request, null);
		playerData.CopyInfoData.DecTimesByID(this.Id);
	}

	// Token: 0x04002CB1 RID: 11441
	public UILabel CurTimeLabel;

	// Token: 0x04002CB2 RID: 11442
	public UILabel PreRankLabel;

	// Token: 0x04002CB3 RID: 11443
	public UILabel CurRankLabel;

	// Token: 0x04002CB4 RID: 11444
	public ShowRewardItems CarShowRewardItem;

	// Token: 0x04002CB5 RID: 11445
	public GameObject CarRankRoot;

	// Token: 0x04002CB6 RID: 11446
	public GameObject CarNotRankRoot;

	// Token: 0x04002CB7 RID: 11447
	public GameObject NewRecordObj;

	// Token: 0x04002CB8 RID: 11448
	public GameObject RankUpObj;

	// Token: 0x04002CB9 RID: 11449
	private string Id;
}
