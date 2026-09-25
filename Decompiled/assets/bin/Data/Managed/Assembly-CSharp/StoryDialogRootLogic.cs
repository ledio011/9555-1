using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009B0 RID: 2480
public class StoryDialogRootLogic : SingletonUnity<StoryDialogRootLogic>
{
	// Token: 0x0600466C RID: 18028 RVA: 0x001648CC File Offset: 0x00162ACC
	private void ClearCurStoryData()
	{
		this.mCurStoryId = string.Empty;
		this.mCurStoryStep = 0;
		this.mCurStoryMissionId = string.Empty;
		this.mCurStoryDataList = null;
	}

	// Token: 0x0600466D RID: 18029 RVA: 0x00164900 File Offset: 0x00162B00
	public static void ShowStory(string storyId, NpcData targetNpcData)
	{
		if (SingletonUnity<UIManager>.Instance.CloseAllPOPUI())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.StoryDialogUIRoot, delegate
			{
				SingletonUnity<StoryDialogRootLogic>.Instance.StartStory(storyId, targetNpcData);
			}, null);
		}
	}

	// Token: 0x0600466E RID: 18030 RVA: 0x0016494C File Offset: 0x00162B4C
	private static void OnStoryDialogShow(bool isSuccess, object param)
	{
		Debug.Log("OnStoryDialogShow");
		if (isSuccess)
		{
		}
	}

	// Token: 0x0600466F RID: 18031 RVA: 0x00164960 File Offset: 0x00162B60
	public bool StartStory(string storyId, NpcData npcData)
	{
		if (string.IsNullOrEmpty(storyId))
		{
			return false;
		}
		this.ClearCurStoryData();
		this.mCurStoryDataList = DataManager.GetStoryDataListById(storyId);
		if (this.mCurStoryDataList == null)
		{
			return false;
		}
		this.mCurStoryMissionId = this.mCurStoryDataList[0].MissionId;
		this.mCurStoryId = storyId;
		if (npcData != null)
		{
			this.NpcFakeObjRoot.EnableFakeObjRoot();
			if (!string.IsNullOrEmpty(this.mCurNpcId) && !this.mCurNpcId.Equals(npcData.ID))
			{
				this.NpcFakeObj.DestroyNpcFakeObj();
			}
			this.NpcFakeObj.InitFakeNpcObj(npcData.Model, this.NpcFakeObjRoot.MeshRoot, null);
			this.mCurNpcId = npcData.ID;
			this.NpcPic.mainTexture = this.NpcFakeObjRoot.ModelPic;
		}
		else
		{
			this.NpcFakeObjRoot.DisableFakeObjRoot();
			this.NpcFakeObj.DestroyNpcFakeObj();
			NGUITools.SetActive(this.NpcPic.gameObject, false);
			this.mCurNpcId = string.Empty;
		}
		this.PlayerFakeObjRoot.EnableFakeObjRoot();
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (this.PlayerFakeObj.FakeObj == null)
		{
			if (playerData.IsShowFashion)
			{
				if (playerData.CheckWeaponIsSame())
				{
					this.PlayerFakeObj.InitFakeObject(playerData.FashionWeaponId, playerData.FashionHeadId, playerData.FashionBodyId, playerData.FashionLegId, this.PlayerFakeObjRoot.MeshRoot, null, "FakeObj2");
				}
				else
				{
					this.PlayerFakeObj.InitFakeObject(playerData.PartWeaponId, playerData.FashionHeadId, playerData.FashionBodyId, playerData.FashionLegId, this.PlayerFakeObjRoot.MeshRoot, null, "FakeObj2");
				}
			}
			else
			{
				this.PlayerFakeObj.InitFakeObject(playerData.PartWeaponId, playerData.PartHeadId, playerData.PartBodyId, playerData.PartLegId, this.PlayerFakeObjRoot.MeshRoot, null, "FakeObj2");
			}
		}
		else if (playerData.IsShowFashion)
		{
			if (playerData.CheckWeaponIsSame())
			{
				this.PlayerFakeObj.CheckFakeObject(playerData.FashionWeaponId, playerData.FashionHeadId, playerData.FashionBodyId, playerData.FashionLegId, null);
			}
			else
			{
				this.PlayerFakeObj.CheckFakeObject(playerData.PartWeaponId, playerData.FashionHeadId, playerData.FashionBodyId, playerData.FashionLegId, null);
			}
		}
		else
		{
			this.PlayerFakeObj.CheckFakeObject(playerData.PartWeaponId, playerData.PartHeadId, playerData.PartBodyId, playerData.PartLegId, null);
		}
		this.PlayerPic.mainTexture = this.PlayerFakeObjRoot.ModelPic;
		this.PlayStory();
		return true;
	}

	// Token: 0x06004670 RID: 18032 RVA: 0x00164C08 File Offset: 0x00162E08
	private void PlayStory()
	{
		if (!string.IsNullOrEmpty(this.mCurStoryId) && this.mCurStoryStep >= 0)
		{
			if (this.mCurStoryStep < this.mCurStoryDataList.Count)
			{
				this.SetSotryPage(this.mCurStoryDataList[this.mCurStoryStep].RolePicName, this.mCurStoryDataList[this.mCurStoryStep].SpeakerName, this.mCurStoryDataList[this.mCurStoryStep].TextInfo);
			}
			else
			{
				bool flag = false;
				if (!string.IsNullOrEmpty(this.mCurStoryMissionId))
				{
					MissionData missionDataByID = DataManager.GetMissionDataByID(this.mCurStoryMissionId);
					if (missionDataByID != null && missionDataByID.MissionLogicType == MISSION_LOGICTYPE.STORY)
					{
						update_misison_complete.request request = new update_misison_complete.request();
						request.missionId = this.mCurStoryMissionId;
						NetLogic.GetInstance().Send<Protocol.update_misison_complete>(request, null);
						if (missionDataByID.Submit.Equals(this.mCurNpcId))
						{
							flag = true;
						}
					}
				}
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.StoryDialogUIRoot);
				if (!flag)
				{
					SingletonUnity<UIManager>.Instance.CheckReShowUI(UIInfo.StoryDialogUIRoot);
				}
				if (UIUpdateEvent.OnStoryShowOver != null)
				{
					UIUpdateEvent.OnStoryShowOver(this.mCurStoryId);
				}
				this.ClearCurStoryData();
				Singleton<DialogManager>.Instance.OnCloseDialog();
			}
		}
	}

	// Token: 0x06004671 RID: 18033 RVA: 0x00164D48 File Offset: 0x00162F48
	private void SetSotryPage(string rolePicName, string speakerName, string textInfo)
	{
		if (speakerName.Equals("&"))
		{
			this.RoleNameLabel.text = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.Name;
			NGUITools.SetActive(this.PlayerPic.gameObject, true);
			NGUITools.SetActive(this.NpcPic.gameObject, false);
		}
		else
		{
			this.RoleNameLabel.text = speakerName;
			NGUITools.SetActive(this.PlayerPic.gameObject, false);
			NGUITools.SetActive(this.NpcPic.gameObject, true);
		}
		this.TextLabel.text = StrDictionary.GetDictionaryString(textInfo, new object[0]);
	}

	// Token: 0x06004672 RID: 18034 RVA: 0x00164DF0 File Offset: 0x00162FF0
	private void MoveNext()
	{
		this.mCurStoryStep++;
		this.PlayStory();
	}

	// Token: 0x06004673 RID: 18035 RVA: 0x00164E08 File Offset: 0x00163008
	public void OnClickContinueBtn()
	{
		this.MoveNext();
	}

	// Token: 0x06004674 RID: 18036 RVA: 0x00164E10 File Offset: 0x00163010
	private void OnEnable()
	{
		Singleton<ObjManager>.Instance.MainPlayer.IsTalking = true;
	}

	// Token: 0x06004675 RID: 18037 RVA: 0x00164E24 File Offset: 0x00163024
	private void OnDisable()
	{
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.IsTalking = false;
		}
	}

	// Token: 0x06004676 RID: 18038 RVA: 0x00164E58 File Offset: 0x00163058
	protected override void OnDestroy()
	{
		this.NpcFakeObj.DestroyNpcFakeObj();
		this.PlayerFakeObj.DestroyFakeObj();
		base.OnDestroy();
	}

	// Token: 0x04003373 RID: 13171
	public UITexture NpcPic;

	// Token: 0x04003374 RID: 13172
	public UITexture PlayerPic;

	// Token: 0x04003375 RID: 13173
	public UILabel RoleNameLabel;

	// Token: 0x04003376 RID: 13174
	public UILabel TextLabel;

	// Token: 0x04003377 RID: 13175
	public TeamFakeObjPicRootLogic NpcFakeObjRoot;

	// Token: 0x04003378 RID: 13176
	public FakeObjLogic NpcFakeObj;

	// Token: 0x04003379 RID: 13177
	public TeamFakeObjPicRootLogic PlayerFakeObjRoot;

	// Token: 0x0400337A RID: 13178
	public FakeObjLogic PlayerFakeObj;

	// Token: 0x0400337B RID: 13179
	private List<StoryData> mCurStoryDataList;

	// Token: 0x0400337C RID: 13180
	private string mCurStoryId = string.Empty;

	// Token: 0x0400337D RID: 13181
	private int mCurStoryStep;

	// Token: 0x0400337E RID: 13182
	private string mCurStoryMissionId = string.Empty;

	// Token: 0x0400337F RID: 13183
	private string mCurNpcId;
}
