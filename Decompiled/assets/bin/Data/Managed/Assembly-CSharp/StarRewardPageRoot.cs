using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009AF RID: 2479
public class StarRewardPageRoot : SingletonUnity<StarRewardPageRoot>
{
	// Token: 0x06004665 RID: 18021 RVA: 0x00164658 File Offset: 0x00162858
	public void ResetCopySceneReward(copy_scene_result.request request)
	{
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		mainPlayer.AnimationLogic.PlayAnimation(GameDefine.GetRoleIdelSelectName((int)mainPlayer.Profession, mainPlayer.GetWeaponType()), null, -1f);
		mainPlayer.CameraController.FinishCopyCameraEffect(1f, delegate
		{
			Singleton<ObjManager>.Instance.MainPlayer.AnimationLogic.PlayAnimation(GameDefine.ShowSelectAnimaName, null, -1f);
		});
		if (request.win)
		{
			this.ResetCopySceneRewardPage(request);
		}
	}

	// Token: 0x06004666 RID: 18022 RVA: 0x001646D4 File Offset: 0x001628D4
	private void ResetCopySceneRewardPage(copy_scene_result.request request)
	{
		for (int i = 0; i < this.TopStars.Length; i++)
		{
			if ((long)i < request.grade)
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
		this.LineStarGrid.Reposition();
		CopySceneData copySceneDataById = DataManager.GetCopySceneDataById(request.id);
		List<string> list = new List<string>();
		copySceneDataById.GetStarDescriptionList((int)request.gradeFlag, list);
		for (int j = 0; j < list.Count; j++)
		{
			this.LineLabelList[j].text = StrDictionary.GetDictionaryString(list[j], new object[0]);
		}
		this.ShowRewardItem.ShowRewards(request.items);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("DailyCopy", string.Format("copy_{0}", request.id), string.Format("star_{0}", request.grade));
	}

	// Token: 0x06004667 RID: 18023 RVA: 0x00164800 File Offset: 0x00162A00
	private void OnEnable()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = true;
		}
		SingletonUnity<UIManager>.Instance.CloseOtherPlayerUI();
	}

	// Token: 0x06004668 RID: 18024 RVA: 0x0016483C File Offset: 0x00162A3C
	private void OnDisable()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = false;
		}
	}

	// Token: 0x06004669 RID: 18025 RVA: 0x00164870 File Offset: 0x00162A70
	public void OnClickLeave()
	{
		NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
	}

	// Token: 0x0400336D RID: 13165
	public UISprite[] TopStars;

	// Token: 0x0400336E RID: 13166
	public GameObject[] LineStars;

	// Token: 0x0400336F RID: 13167
	public UILabel[] LineLabelList;

	// Token: 0x04003370 RID: 13168
	public ShowRewardItems ShowRewardItem;

	// Token: 0x04003371 RID: 13169
	public UIGrid LineStarGrid;
}
