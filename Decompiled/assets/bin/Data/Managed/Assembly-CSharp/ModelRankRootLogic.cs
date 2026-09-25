using System;
using System.Collections.Generic;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x02000977 RID: 2423
public class ModelRankRootLogic : MonoBehaviour
{
	// Token: 0x06004465 RID: 17509 RVA: 0x0015491C File Offset: 0x00152B1C
	private void Start()
	{
		if (!this.mInitFlag)
		{
			for (int i = 0; i < this.RankItemList.Count; i++)
			{
				this.RankItemList[i].Init(new RankItemLogic.onClickBtn(this.OnClickRankNumBtn), i);
			}
			this.mInitFlag = true;
		}
	}

	// Token: 0x06004466 RID: 17510 RVA: 0x00154978 File Offset: 0x00152B78
	public void Reset(List<sort_item> ranklist, RANK_TYPE curtype)
	{
		this.mCurRankList = ranklist;
		if (curtype == RANK_TYPE.CAR)
		{
			this.ResetFakeCarObjRoot();
		}
		else
		{
			this.ResetFakeObjRoot();
		}
		if (ranklist.Count % 10 != 0)
		{
			this.MaxPageNum = ranklist.Count / 10 + 1;
		}
		else
		{
			this.MaxPageNum = ranklist.Count / 10;
			if (this.MaxPageNum == 0)
			{
				this.MaxPageNum = 1;
			}
		}
		this.CurRankType = curtype;
		this.curPagenum = 1;
		this.SelfInfoShow();
		this.mCurRankNum = -1;
		this.modelProfession = PROFESSION_TYPE.INVALID;
		this.PageInfoLabel.text = this.curPagenum + "/" + this.MaxPageNum;
		this.ResetItemList((this.curPagenum - 1) * 10);
		this.OnClickRankNum(0, 0);
		this.RankScrollView.ResetPosition();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Rank", string.Format("rank_{0}", (int)curtype), "opentimes");
	}

	// Token: 0x06004467 RID: 17511 RVA: 0x00154A80 File Offset: 0x00152C80
	public void ResetItemList(int startRanknum)
	{
		for (int i = 0; i < this.RankItemList.Count; i++)
		{
			if (startRanknum + i < this.mCurRankList.Count)
			{
				this.RankItemList[i].Reset(startRanknum + i, this.mCurRankList[startRanknum + i], this.CurRankType);
				UnityVersionUtil.SetActiveRecursive(this.RankItemList[i].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.RankItemList[i].gameObject, false);
			}
		}
	}

	// Token: 0x06004468 RID: 17512 RVA: 0x00154B18 File Offset: 0x00152D18
	public void OnClickLeftpageBtn()
	{
		if (this.curPagenum > 1)
		{
			this.curPagenum--;
			this.PageInfoLabel.text = this.curPagenum + "/" + this.MaxPageNum;
			this.ResetItemList((this.curPagenum - 1) * 10);
			this.RankScrollView.ResetPosition();
			this.RankItemList[0].OnClickBtn();
		}
	}

	// Token: 0x06004469 RID: 17513 RVA: 0x00154B98 File Offset: 0x00152D98
	public void OnClickRightpageBtn()
	{
		if (this.curPagenum < this.MaxPageNum)
		{
			this.curPagenum++;
			this.PageInfoLabel.text = this.curPagenum + "/" + this.MaxPageNum;
			this.ResetItemList((this.curPagenum - 1) * 10);
			this.RankScrollView.ResetPosition();
			this.RankItemList[0].OnClickBtn();
		}
	}

	// Token: 0x0600446A RID: 17514 RVA: 0x00154C1C File Offset: 0x00152E1C
	public void OnClickRankNumBtn(int clickNum, int objIndex)
	{
		this.OnClickRankNum(clickNum, objIndex);
	}

	// Token: 0x0600446B RID: 17515 RVA: 0x00154C28 File Offset: 0x00152E28
	public void OnClickHitOtherPlayer(sort_item item)
	{
		if (this.currentLook != null && item.id != Singleton<ObjManager>.Instance.MainPlayer.ServerId)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			TargetBasicInfo selectTargetBasicInfo = playerData.SelectTargetBasicInfo;
			selectTargetBasicInfo.ResetInfo(item.id, (int)this.currentLook.attribute_other.level, (int)this.currentLook.attribute_other.combValue, this.currentLook.general.name, (PROFESSION_TYPE)this.currentLook.general.profession, 0, this.currentLook.attribute_other.guildId, this.currentLook.attribute_other.guildName, UICamera.currentTouch.pos);
			HitOtherPLayerLogic.ShowMenu(HitType.HitTop, selectTargetBasicInfo);
		}
	}

	// Token: 0x0600446C RID: 17516 RVA: 0x00154CF0 File Offset: 0x00152EF0
	public void OnClickRankNum(int rankNum, int itemIndex)
	{
		if (this.mCurRankNum == rankNum)
		{
			this.OnClickHitOtherPlayer(this.mCurRankList[rankNum]);
			return;
		}
		this.currentLook = null;
		if (rankNum >= this.mCurRankList.Count)
		{
			this.ModelProfessionFlag.enabled = false;
			this.ModelNameLabel.text = string.Empty;
			return;
		}
		this.mCurRankNum = rankNum;
		this.mCurRankObjIndex = itemIndex;
		this.selectitemPic.transform.parent = this.RankItemList[this.mCurRankObjIndex].transform;
		this.selectitemPic.transform.localPosition = Vector3.zero;
		if (!UnityVersionUtil.IsActive(this.selectitemPic.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(this.selectitemPic.gameObject, true);
		}
		checked
		{
			if (this.CurRankType == RANK_TYPE.CAR)
			{
				this.ModelProfessionFlag.enabled = false;
				string[] array = this.mCurRankList[rankNum].name.Split(new char[]
				{
					'#'
				});
				MountData mountDataById = DataManager.GetMountDataById(array[1]);
				this.ModelNameLabel.text = StrDictionary.GetDictionaryString(mountDataById.CarName, new object[0]);
				ColorData datacolor = null;
				if (array.Length > 2)
				{
					datacolor = DataManager.GetColorDataById(array[2]);
				}
				this.ResetCarModelVisual(mountDataById, datacolor);
			}
			else if (this.CurRankType == RANK_TYPE.LADDER || this.CurRankType == RANK_TYPE.SEX)
			{
				string[] array2 = this.mCurRankList[rankNum].name.Split(new char[]
				{
					'#'
				});
				this.ModelProfessionFlag.enabled = true;
				this.ModelProfessionFlag.spriteName = GameDefine.Profession_PicName[(int)((IntPtr)this.mCurRankList[rankNum].profession)];
				this.ModelNameLabel.text = array2[0];
				ask_character_info.request request = new ask_character_info.request();
				request.characterId = this.mCurRankList[rankNum].id;
				NetLogic.GetInstance().Send<Protocol.ask_character_info>(request, new RpcRspHandler(this.RetAskCharacterinfo));
			}
			else
			{
				this.ModelProfessionFlag.enabled = true;
				this.ModelProfessionFlag.spriteName = GameDefine.Profession_PicName[(int)((IntPtr)this.mCurRankList[rankNum].profession)];
				this.ModelNameLabel.text = this.mCurRankList[rankNum].name;
				ask_character_info.request request2 = new ask_character_info.request();
				request2.characterId = this.mCurRankList[rankNum].id;
				NetLogic.GetInstance().Send<Protocol.ask_character_info>(request2, new RpcRspHandler(this.RetAskCharacterinfo));
			}
		}
	}

	// Token: 0x0600446D RID: 17517 RVA: 0x00154F78 File Offset: 0x00153178
	private void RetAskCharacterinfo(SprotoTypeBase req)
	{
		if (this == null || !UnityVersionUtil.IsActive(base.gameObject))
		{
			return;
		}
		ask_character_info.response response = req as ask_character_info.response;
		if (response != null && response.HasCharacter)
		{
			this.currentLook = response.character;
			character_look character = response.character;
			this.ResetModelVisual(character);
		}
	}

	// Token: 0x0600446E RID: 17518 RVA: 0x00154FD4 File Offset: 0x001531D4
	private void ResetFakeObjRoot()
	{
		this.RotateModelBtnListener.onDrag = new UIEventListener.VectorDelegate(this.OnDragModelBtn);
		FakeObjRootLogic instance = SingletonUnity<FakeObjRootLogic>.Instance;
		if (instance == null)
		{
			ResourcesManager.LoadAndInstantiate("UIRoot/FakeObjRoot");
			instance = SingletonUnity<FakeObjRootLogic>.Instance;
		}
		SingletonUnity<FakeObjRootLogic>.Instance.SetPicValue(0.65f);
		SingletonUnity<FakeObjRootLogic>.Instance.EnableFakeObjRoot();
		this.ModelPic.mainTexture = instance.ModelPic;
		if (this.mCurRankList.Count > 0)
		{
			this.ModelPic.enabled = true;
		}
		else
		{
			this.ModelPic.enabled = false;
		}
		this.CarModelPic.enabled = false;
	}

	// Token: 0x0600446F RID: 17519 RVA: 0x00155080 File Offset: 0x00153280
	private void ResetFakeCarObjRoot()
	{
		this.RotateModelBtnListener.onDrag = new UIEventListener.VectorDelegate(this.OnDragCarModelPic);
		FakeCarObjRootLogic instance = SingletonUnity<FakeCarObjRootLogic>.Instance;
		if (instance == null)
		{
			ResourcesManager.LoadAndInstantiate("UIRoot/FakeCarObjRoot");
			instance = SingletonUnity<FakeCarObjRootLogic>.Instance;
		}
		this.CarMeshRoot = instance.MeshRoot;
		SingletonUnity<FakeCarObjRootLogic>.Instance.EnableFakeObjRoot();
		this.CarModelPic.mainTexture = instance.ModelPic;
		this.ModelPic.enabled = false;
		this.CarModelPic.enabled = true;
	}

	// Token: 0x06004470 RID: 17520 RVA: 0x00155108 File Offset: 0x00153308
	private void ResetCarModelVisual(MountData mountData, ColorData datacolor)
	{
		SingletonUnity<FakeCarObjRootLogic>.Instance.InitFakeCarObj(mountData, datacolor);
	}

	// Token: 0x06004471 RID: 17521 RVA: 0x00155118 File Offset: 0x00153318
	private void ResetModelVisual(character_look CurCharacterLook)
	{
		PROFESSION_TYPE profession_TYPE = (PROFESSION_TYPE)CurCharacterLook.general.profession;
		string modeName = ServerToClientTools.GetModeName(CurCharacterLook.visual.HeadId);
		if (profession_TYPE != this.modelProfession)
		{
			this.modelProfession = profession_TYPE;
			if (this.mCurFakeObj != null)
			{
				this.mCurFakeObj.DestroyFakeObj();
			}
		}
		if (this.mCurFakeObj == null || this.mCurFakeObj.FakeObj == null)
		{
			this.mCurFakeObj = new FakeObjLogic();
			this.mCurFakeObj.InitFakeObject(CurCharacterLook.visual, profession_TYPE.ToString(), modeName, SingletonUnity<FakeObjRootLogic>.Instance.MeshRoot, null, "FakeObj");
		}
		else
		{
			this.mCurFakeObj.CheckFakeObject(CurCharacterLook.visual, null);
			this.mCurFakeObj.PlayAnim("idle", modeName);
		}
	}

	// Token: 0x06004472 RID: 17522 RVA: 0x001551F0 File Offset: 0x001533F0
	private void OnDragModelBtn(GameObject btn, Vector2 delta)
	{
		if (this.mCurFakeObj != null && this.mCurFakeObj.FakeObj != null)
		{
			this.mCurFakeObj.FakeObj.transform.localEulerAngles -= delta.x * Vector3.up;
		}
	}

	// Token: 0x06004473 RID: 17523 RVA: 0x00155250 File Offset: 0x00153450
	public void OnDragCarModelPic(GameObject btn, Vector2 delta)
	{
		if (this.CarMeshRoot != null)
		{
			this.CarMeshRoot.transform.localEulerAngles -= new Vector3(0f, delta.x, 0f);
		}
	}

	// Token: 0x06004474 RID: 17524 RVA: 0x001552A0 File Offset: 0x001534A0
	public void SelfInfoShow()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		switch (this.CurRankType)
		{
		case RANK_TYPE.FIGHT:
			this.curValuename = StrDictionary.GetDictionaryString("#{101315}", new object[0]);
			this.VocationLabel.text = StrDictionary.GetDictionaryString("#{101311}", new object[0]);
			break;
		case RANK_TYPE.LEVEL:
			this.curValuename = StrDictionary.GetDictionaryString("#{101314}", new object[0]);
			this.VocationLabel.text = StrDictionary.GetDictionaryString("#{101311}", new object[0]);
			break;
		case RANK_TYPE.LADDER:
			this.curValuename = StrDictionary.GetDictionaryString("#{101315}", new object[0]);
			this.VocationLabel.text = StrDictionary.GetDictionaryString("#{101311}", new object[0]);
			break;
		case RANK_TYPE.TOWER:
			this.curValuename = StrDictionary.GetDictionaryString("#{101317}", new object[0]);
			this.VocationLabel.text = StrDictionary.GetDictionaryString("#{101311}", new object[0]);
			break;
		case RANK_TYPE.CAR:
			this.curValuename = StrDictionary.GetDictionaryString("#{101319}", new object[0]);
			this.VocationLabel.text = StrDictionary.GetDictionaryString("#{101568}", new object[0]);
			break;
		case RANK_TYPE.SEX:
			this.curValuename = StrDictionary.GetDictionaryString("#{100609}", new object[0]);
			this.VocationLabel.text = StrDictionary.GetDictionaryString("#{101311}", new object[0]);
			break;
		}
		this.rankInfoLabel.text = this.curValuename;
		this.SelfRankLabel.text = string.Format("{0}", StrDictionary.GetDictionaryString("#{101322}", new object[0]));
		this.SelfValLabel.enabled = false;
		this.SelfNameLabel.enabled = false;
		for (int i = 0; i < this.mCurRankList.Count; i++)
		{
			if (this.mCurRankList[i].id == PlayerData.MainPlayerServerId)
			{
				if (this.CurRankType == RANK_TYPE.LADDER)
				{
					string[] array = this.mCurRankList[i].name.Split(new char[]
					{
						'#'
					});
					if (array.Length > 1)
					{
						this.SelfValLabel.text = string.Format("{0}:{1}", this.curValuename, array[1]);
					}
				}
				else if (this.CurRankType == RANK_TYPE.CAR)
				{
					this.SelfValLabel.text = string.Format("{0}:{1}", this.curValuename, TimeTools.GetCentiSecondStr((int)((long)GameDefine.DayCentiSecond - (this.mCurRankList[i].score >> 32))));
				}
				else if (this.CurRankType == RANK_TYPE.TOWER)
				{
					this.SelfValLabel.text = string.Format("{0}:{1}", this.curValuename, (this.mCurRankList[i].score >> 32) + 1L);
				}
				else
				{
					this.SelfValLabel.text = string.Format("{0}:{1}", this.curValuename, this.mCurRankList[i].score >> 32);
				}
				this.SelfRankLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{101309}", new object[0]), i + 1);
				this.SelfNameLabel.text = playerData.MainPlayerAttrData.Name;
				this.SelfValLabel.enabled = true;
				this.SelfNameLabel.enabled = true;
				break;
			}
		}
	}

	// Token: 0x06004475 RID: 17525 RVA: 0x00155638 File Offset: 0x00153838
	public void UnLoadFakeObj()
	{
		if (this.mCurFakeObj != null)
		{
			this.mCurFakeObj.DestroyFakeObj();
			this.mCurFakeObj = null;
		}
		if (SingletonUnity<FakeObjRootLogic>.Instance != null)
		{
			SingletonUnity<FakeObjRootLogic>.Instance.DisableFakeObjRoot();
		}
		if (SingletonUnity<FakeCarObjRootLogic>.Instance != null)
		{
			SingletonUnity<FakeCarObjRootLogic>.Instance.DisableFakeObjRoot();
		}
	}

	// Token: 0x040030FB RID: 12539
	public RANK_TYPE CurRankType;

	// Token: 0x040030FC RID: 12540
	public UIEventListener RotateModelBtnListener;

	// Token: 0x040030FD RID: 12541
	private FakeObjLogic mCurFakeObj;

	// Token: 0x040030FE RID: 12542
	public UITexture ModelPic;

	// Token: 0x040030FF RID: 12543
	private PROFESSION_TYPE modelProfession = PROFESSION_TYPE.INVALID;

	// Token: 0x04003100 RID: 12544
	private List<sort_item> mCurRankList;

	// Token: 0x04003101 RID: 12545
	public UILabel SelfRankLabel;

	// Token: 0x04003102 RID: 12546
	public UILabel SelfNameLabel;

	// Token: 0x04003103 RID: 12547
	public UILabel SelfValLabel;

	// Token: 0x04003104 RID: 12548
	public UILabel rankInfoLabel;

	// Token: 0x04003105 RID: 12549
	public UILabel VocationLabel;

	// Token: 0x04003106 RID: 12550
	public UISprite VocationFlag;

	// Token: 0x04003107 RID: 12551
	public UISprite ModelProfessionFlag;

	// Token: 0x04003108 RID: 12552
	public UILabel ModelNameLabel;

	// Token: 0x04003109 RID: 12553
	private bool mInitFlag;

	// Token: 0x0400310A RID: 12554
	public List<RankItemLogic> RankItemList;

	// Token: 0x0400310B RID: 12555
	public UISprite selectitemPic;

	// Token: 0x0400310C RID: 12556
	private int mCurRankNum;

	// Token: 0x0400310D RID: 12557
	private int mCurRankObjIndex;

	// Token: 0x0400310E RID: 12558
	public UILabel PageInfoLabel;

	// Token: 0x0400310F RID: 12559
	private int curPagenum;

	// Token: 0x04003110 RID: 12560
	private int MaxPageNum = 10;

	// Token: 0x04003111 RID: 12561
	public UIScrollView RankScrollView;

	// Token: 0x04003112 RID: 12562
	private string curValuename;

	// Token: 0x04003113 RID: 12563
	public UITexture CarModelPic;

	// Token: 0x04003114 RID: 12564
	public Transform CarMeshRoot;

	// Token: 0x04003115 RID: 12565
	private character_look currentLook;
}
