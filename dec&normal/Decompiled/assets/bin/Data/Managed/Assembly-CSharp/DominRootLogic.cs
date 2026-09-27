using System;
using System.Collections;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A27 RID: 2599
public class DominRootLogic : SingletonUnity<DominRootLogic>
{
	// Token: 0x06004B1C RID: 19228 RVA: 0x0018DE44 File Offset: 0x0018C044
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004B1D RID: 19229 RVA: 0x0018DE50 File Offset: 0x0018C050
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x06004B1E RID: 19230 RVA: 0x0018DE80 File Offset: 0x0018C080
	public void EnableReset(string targetId)
	{
		this.mTargetId = targetId;
		for (int i = 0; i < this.DominLineList.Count; i++)
		{
			NGUITools.SetActive(this.DominLineList[i].gameObject, false);
		}
	}

	// Token: 0x06004B1F RID: 19231 RVA: 0x0018DEC8 File Offset: 0x0018C0C8
	public void Reset(Dictionary<string, domin_info> infoDic, Dictionary<long, character_look> lookDic)
	{
		WaitResponseUIRootLogic.CloseBox();
		this.curDominInfoDic = infoDic;
		this.curCharacterDic = lookDic;
		List<string> list = new List<string>(this.curDominInfoDic.Keys);
		list.Sort((string x, string y) => x.CompareTo(y));
		int num = list.Count - this.DominLineList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.DominLineList[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0:D2}", this.DominLineList.Count + 1);
				gameObject.transform.parent = this.DominLineList[0].transform.parent;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
				this.DominLineList.Add(gameObject.GetComponent<DominLineLogic>());
			}
		}
		for (int j = 0; j < this.DominLineList.Count; j++)
		{
			if (j < list.Count)
			{
				NGUITools.SetActive(this.DominLineList[j].gameObject, true);
				domin_info domin_info = this.curDominInfoDic[list[j]];
				if (domin_info.state == 0L)
				{
					if (this.curCharacterDic.ContainsKey(domin_info.serverId))
					{
						this.DominLineList[j].Reset(domin_info, this.curCharacterDic[domin_info.serverId], new DelegateDefine.OneStringParamDelegate(this.ClickTargetLine));
					}
					else
					{
						this.DominLineList[j].Reset(domin_info, null, new DelegateDefine.OneStringParamDelegate(this.ClickTargetLine));
					}
				}
				else
				{
					this.DominLineList[j].Reset(domin_info, null, new DelegateDefine.OneStringParamDelegate(this.ClickTargetLine));
				}
			}
			else
			{
				NGUITools.SetActive(this.DominLineList[j].gameObject, false);
			}
		}
		this.TableRoot.Reposition();
		this.ScrollView.ResetPosition();
		base.StartCoroutine(this.DelayFlash());
		if (SingletonUnity<NewMapUIRootLogic>.Exists)
		{
			SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjNormal();
		}
		if (!string.IsNullOrEmpty(this.mTargetId))
		{
			this.ClickTargetLine(this.mTargetId);
			this.mTargetId = string.Empty;
		}
	}

	// Token: 0x06004B20 RID: 19232 RVA: 0x0018E160 File Offset: 0x0018C360
	private IEnumerator DelayFlash()
	{
		yield return null;
		this.TableRoot.Reposition();
		this.ScrollView.ResetPosition();
		if (TutorialManager.CurStep == TUTORIAL_STEP.CAPTURE_WAIT_DATA)
		{
			this.CheckTutorialEvent();
		}
		yield break;
	}

	// Token: 0x06004B21 RID: 19233 RVA: 0x0018E17C File Offset: 0x0018C37C
	public void ClickTargetLine(string dominId)
	{
		if (this.curDominInfoDic.ContainsKey(dominId))
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DominPageRoot);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.DominInfoRoot, delegate
			{
				domin_info domin_info = this.curDominInfoDic[dominId];
				if (this.curCharacterDic.ContainsKey(domin_info.serverId))
				{
					SingletonUnity<DominInfoRootLogic>.Instance.Reset(domin_info, this.curCharacterDic[domin_info.serverId]);
				}
				else
				{
					SingletonUnity<DominInfoRootLogic>.Instance.Reset(domin_info, null);
				}
				if (TutorialManager.CurStep == TUTORIAL_STEP.CAPTURE_CLICK_ITEM)
				{
					this.CheckTutorialEvent();
				}
			}, null);
		}
	}

	// Token: 0x06004B22 RID: 19234 RVA: 0x0018E1E4 File Offset: 0x0018C3E4
	private void OnDisable()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.CAPTURE_START)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x040038D8 RID: 14552
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x040038D9 RID: 14553
	public List<DominLineLogic> DominLineList = new List<DominLineLogic>();

	// Token: 0x040038DA RID: 14554
	private Dictionary<string, domin_info> curDominInfoDic = new Dictionary<string, domin_info>();

	// Token: 0x040038DB RID: 14555
	private Dictionary<long, character_look> curCharacterDic = new Dictionary<long, character_look>();

	// Token: 0x040038DC RID: 14556
	private string mTargetId = string.Empty;

	// Token: 0x040038DD RID: 14557
	public UITable TableRoot;

	// Token: 0x040038DE RID: 14558
	public UIScrollView ScrollView;

	// Token: 0x040038DF RID: 14559
	private string mTargetMissionId = string.Empty;

	// Token: 0x040038E0 RID: 14560
	private NewMissionLineLogic mCurMissionLine;
}
