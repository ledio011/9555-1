using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000984 RID: 2436
public class RewardPageRootLogic : SingletonUnity<RewardPageRootLogic>
{
	// Token: 0x060044E5 RID: 17637 RVA: 0x00158C34 File Offset: 0x00156E34
	public void BeforeResultReset()
	{
		UnityVersionUtil.SetActiveRecursive(this.WinPageRoot, false);
		UnityVersionUtil.SetActiveRecursive(this.RewardPageRoot, false);
		UnityVersionUtil.SetActiveRecursive(this.LoosePageRoot, false);
		UnityVersionUtil.SetActiveRecursive(this.CarRewardPageRoot, false);
	}

	// Token: 0x060044E6 RID: 17638 RVA: 0x00158C74 File Offset: 0x00156E74
	public void ResetPVPReward(tiantti_result.request request)
	{
		if (request.win)
		{
			UnityVersionUtil.SetActiveRecursive(this.WinPageRoot, true);
			UnityVersionUtil.SetActiveRecursive(this.RewardPageRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.LoosePageRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.CarRewardPageRoot, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.WinPageRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.RewardPageRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.LoosePageRoot, true);
			UnityVersionUtil.SetActiveRecursive(this.CarRewardPageRoot, false);
		}
	}

	// Token: 0x060044E7 RID: 17639 RVA: 0x00158CF4 File Offset: 0x00156EF4
	public void ResetCopySceneReward(copy_scene_result.request request)
	{
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		mainPlayer.CameraController.FinishCopyCameraEffect(1f, null);
		if (request.win)
		{
			UnityVersionUtil.SetActiveRecursive(this.WinPageRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.RewardPageRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.LoosePageRoot, false);
			this.ResetCopySceneRewardPage(request);
			UnityVersionUtil.SetActiveRecursive(this.CarRewardPageRoot, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.WinPageRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.RewardPageRoot, false);
			UnityVersionUtil.SetActiveRecursive(this.LoosePageRoot, true);
			UnityVersionUtil.SetActiveRecursive(this.CarRewardPageRoot, false);
			if ((int)request.subType == 9)
			{
				vp_Timer.In(2f, delegate()
				{
					this.OnClickTowerLeaveBtn();
				}, null);
			}
		}
	}

	// Token: 0x060044E8 RID: 17640 RVA: 0x00158DBC File Offset: 0x00156FBC
	private void ResetCopySceneRewardPage(copy_scene_result.request request)
	{
		UnityVersionUtil.SetActiveRecursive(this.RewardPageRoot, true);
		UnityVersionUtil.SetActiveRecursive(this.BottomRoot, false);
		if ((int)request.subType == 9)
		{
			UnityVersionUtil.SetActiveRecursive(this.TowerCtlRoot.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.NormalCtlRoot, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.TowerCtlRoot.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.NormalCtlRoot, true);
		}
		for (int i = 0; i < this.TopStars.Length; i++)
		{
			if ((long)i <= request.grade)
			{
				UnityVersionUtil.SetActiveRecursive(this.TopStars[i].gameObject, true);
				UnityVersionUtil.SetActiveRecursive(this.LineStars[i].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.TopStars[i].gameObject, false);
				UnityVersionUtil.SetActiveRecursive(this.LineStars[i].gameObject, false);
			}
		}
		if ((int)request.subType != 9)
		{
			CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(request.id);
			List<string> list = new List<string>();
			copySceneDataById.GetStarDescriptionList((int)request.gradeFlag, list);
			for (int j = 0; j < list.Count; j++)
			{
				this.LineLabelList[j].text = list[j];
			}
		}
		this.ShowRewardItem.ShowRewards(request.items);
	}

	// Token: 0x060044E9 RID: 17641 RVA: 0x00158F10 File Offset: 0x00157110
	public void ResetCarRewardPageRoot(car_copy_result.request request)
	{
		this.BeforeResultReset();
		UnityVersionUtil.SetActiveRecursive(this.CarRewardPageRoot, true);
		UnityVersionUtil.SetActiveRecursive(this.BottomRoot, false);
		this.CurTimeLabel.text = TimeTools.GetCentiSecondStr((int)request.parm);
		NGUITools.SetActive(this.NewRecordObj, request.new_record == 1L);
		Debug.Log("request.rankPos1 :: " + request.rankPos1);
		Debug.Log("request.rankPos2 :: " + request.rankPos2);
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
	}

	// Token: 0x060044EA RID: 17642 RVA: 0x00159048 File Offset: 0x00157248
	public void OnClickContinueBtn()
	{
		NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
	}

	// Token: 0x060044EB RID: 17643 RVA: 0x00159058 File Offset: 0x00157258
	public void OnClickRepeatBtn()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
		enter_copy_scene.request request = new enter_copy_scene.request();
		request.mapInfoId = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID;
		NetLogic.GetInstance().Send<Protocol.enter_copy_scene>(request, null);
	}

	// Token: 0x060044EC RID: 17644 RVA: 0x001590A4 File Offset: 0x001572A4
	public void OnClickTowerNextBtn()
	{
		TowerSceneManager towerSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as TowerSceneManager;
		towerSceneManager.GoNextLevel();
	}

	// Token: 0x060044ED RID: 17645 RVA: 0x001590C8 File Offset: 0x001572C8
	public void OnClickTowerLeaveBtn()
	{
		NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
	}

	// Token: 0x040031AB RID: 12715
	public GameObject WinPageRoot;

	// Token: 0x040031AC RID: 12716
	public GameObject LoosePageRoot;

	// Token: 0x040031AD RID: 12717
	public GameObject RewardPageRoot;

	// Token: 0x040031AE RID: 12718
	public GameObject CarRewardPageRoot;

	// Token: 0x040031AF RID: 12719
	public GameObject NormalCtlRoot;

	// Token: 0x040031B0 RID: 12720
	public GameObject TowerCtlRoot;

	// Token: 0x040031B1 RID: 12721
	public GameObject BottomRoot;

	// Token: 0x040031B2 RID: 12722
	public UISprite[] TopStars;

	// Token: 0x040031B3 RID: 12723
	public GameObject[] LineStars;

	// Token: 0x040031B4 RID: 12724
	public UILabel[] LineLabelList;

	// Token: 0x040031B5 RID: 12725
	public ShowRewardItems ShowRewardItem;

	// Token: 0x040031B6 RID: 12726
	public UILabel CurTimeLabel;

	// Token: 0x040031B7 RID: 12727
	public UILabel PreRankLabel;

	// Token: 0x040031B8 RID: 12728
	public UILabel CurRankLabel;

	// Token: 0x040031B9 RID: 12729
	public ShowRewardItems CarShowRewardItem;

	// Token: 0x040031BA RID: 12730
	public GameObject CarRankRoot;

	// Token: 0x040031BB RID: 12731
	public GameObject CarNotRankRoot;

	// Token: 0x040031BC RID: 12732
	public GameObject NewRecordObj;

	// Token: 0x040031BD RID: 12733
	public GameObject RankUpObj;
}
