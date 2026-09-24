using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008D6 RID: 2262
public class BigPackRootLogic : SingletonUnity<BigPackRootLogic>
{
	// Token: 0x06003CF7 RID: 15607 RVA: 0x0010DFD4 File Offset: 0x0010C1D4
	public void EnableReset()
	{
		this.ambientLight = RenderSettings.ambientLight;
		this.TargetShowPack = string.Empty;
		this.textureFinish = true;
		this.requestFinish = false;
		this.mWaitTime = 10f;
		UnityVersionUtil.SetActiveRecursive(this.MainPage.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.TempPage.gameObject, false);
		UIEventListener uieventListener = this.uidragEvent;
		uieventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener.onDrag, new UIEventListener.VectorDelegate(this.OnDragMoveBtn));
		UnityVersionUtil.SetActiveRecursive(this.PointGrid.gameObject, false);
		NGUITools.SetActive(this.LeftSp.gameObject, false);
		NGUITools.SetActive(this.RightSp.gameObject, false);
	}

	// Token: 0x06003CF8 RID: 15608 RVA: 0x0010E08C File Offset: 0x0010C28C
	private void OnDisable()
	{
		this.ResetNormalLight();
	}

	// Token: 0x06003CF9 RID: 15609 RVA: 0x0010E094 File Offset: 0x0010C294
	public void ResetNormalLight()
	{
		RenderSettings.ambientLight = this.ambientLight;
	}

	// Token: 0x06003CFA RID: 15610 RVA: 0x0010E0A4 File Offset: 0x0010C2A4
	private void CheckShowUI()
	{
		if (this.textureFinish && this.requestFinish)
		{
			this.CurShowIndex = 0;
			this.starttime = Time.time;
			this.ResetPoint();
			UnityVersionUtil.SetActiveRecursive(this.MainPage.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.TempPage.gameObject, false);
			this.MainPage.RefershInfo(this.SpePackList[this.CurShowIndex], this.ambientLight);
			this.SelectPoint();
		}
	}

	// Token: 0x06003CFB RID: 15611 RVA: 0x0010E12C File Offset: 0x0010C32C
	private void Update()
	{
		if ((!this.requestFinish || !this.textureFinish) && this.mWaitTime > 0f)
		{
			this.mWaitTime -= Time.deltaTime;
			if (this.mWaitTime <= 0f)
			{
				this.OnClickCloseBtn();
			}
		}
		if (Input.touchCount > 0)
		{
			this.starttime = Time.time;
		}
		if (this.isMoveFlag)
		{
			this.temptime += Time.deltaTime;
			if (this.temptime >= this.MoveTime)
			{
				this.temptime -= this.MoveTime;
				this.isMoveFlag = false;
			}
		}
		else if (this.requestFinish && this.textureFinish && this.SpePackList.Count > 1 && Time.time - this.starttime >= this.DelAutoDragtime)
		{
			this.AutoMove();
		}
	}

	// Token: 0x06003CFC RID: 15612 RVA: 0x0010E230 File Offset: 0x0010C430
	public void AutoMove()
	{
		if (SingletonUnity<ItemInfoRootLogicNew>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ItemInfoRootLogicNew>.Instance.gameObject))
		{
			this.starttime = Time.time;
			return;
		}
		if (this.moveDirRight)
		{
			if (this.CurShowIndex < this.SpePackList.Count - 1)
			{
				this.OnClickRightBtn();
			}
			else
			{
				this.moveDirRight = false;
				this.AutoMove();
			}
		}
		else if (this.CurShowIndex > 0)
		{
			this.OnClickLeftBtn();
		}
		else
		{
			this.moveDirRight = true;
			this.AutoMove();
		}
	}

	// Token: 0x06003CFD RID: 15613 RVA: 0x0010E2CC File Offset: 0x0010C4CC
	public void Reset(ret_request_big_pack.request request)
	{
		this.SpePackList.Clear();
		if (request.HasSpecial_big_packs)
		{
			this.SpePackList = new List<special_big_pack>(request.special_big_packs.Values);
			for (int i = this.SpePackList.Count - 1; i >= 0; i--)
			{
				BigPackageData bigPackageDataById = DataManager.GetBigPackageDataById(this.SpePackList[i].ID);
				if (bigPackageDataById != null)
				{
					if (bigPackageDataById.SellType != 0 || this.SpePackList[i].state != 0L)
					{
						this.SpePackList.RemoveAt(i);
					}
				}
				else
				{
					this.SpePackList.RemoveAt(i);
				}
			}
			this.SpePackList.Sort(delegate(special_big_pack x, special_big_pack y)
			{
				BigPackageData bigPackageDataById2 = DataManager.GetBigPackageDataById(x.ID);
				BigPackageData bigPackageDataById3 = DataManager.GetBigPackageDataById(y.ID);
				if (bigPackageDataById2.sortID == bigPackageDataById3.sortID)
				{
					return int.Parse(x.ID) - int.Parse(y.ID);
				}
				return bigPackageDataById2.sortID - bigPackageDataById3.sortID;
			});
		}
		else
		{
			special_big_pack special_big_pack = new special_big_pack();
			special_big_pack.ID = request.ID;
			special_big_pack.state = request.state;
			if (request.HasEnd_time)
			{
				special_big_pack.end_time = request.end_time;
			}
			this.SpePackList.Add(special_big_pack);
		}
		if (this.SpePackList.Count == 0)
		{
			this.OnClickCloseBtn();
			return;
		}
		special_big_pack special_big_pack2 = null;
		if (!string.IsNullOrEmpty(this.TargetShowPack))
		{
			for (int j = 0; j < this.SpePackList.Count; j++)
			{
				if (this.SpePackList[j].ID.Equals(this.TargetShowPack))
				{
					special_big_pack2 = this.SpePackList[j];
					break;
				}
			}
		}
		if (special_big_pack2 != null)
		{
			this.SpePackList.Clear();
			this.SpePackList.Add(special_big_pack2);
		}
		this.requestFinish = true;
		this.CheckShowUI();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "BigSales", "open");
	}

	// Token: 0x06003CFE RID: 15614 RVA: 0x0010E4B0 File Offset: 0x0010C6B0
	public void OnDragMoveBtn(GameObject btn, Vector2 delta)
	{
		if (this.isMoveFlag)
		{
			return;
		}
		if (delta.x > 20f)
		{
			this.OnClickLeftBtn();
		}
		else if (delta.x < -20f)
		{
			this.OnClickRightBtn();
		}
	}

	// Token: 0x06003CFF RID: 15615 RVA: 0x0010E4FC File Offset: 0x0010C6FC
	public void OnClickLeftBtn()
	{
		if (this.CurShowIndex > 0)
		{
			this.isMoveFlag = true;
			this.moveDirRight = false;
			this.starttime = Time.time;
			this.CurShowIndex--;
			this.MainAnima.from = this.MainAnima.transform.localPosition;
			this.MainAnima.to = this.rightPos;
			this.MainAnima.duration = this.MoveTime;
			this.MainAnima.ResetToBeginning();
			this.TempAnima.from = this.LeftPos;
			this.TempAnima.to = Vector3.zero;
			this.TempAnima.duration = this.MoveTime;
			this.TempAnima.ResetToBeginning();
			this.MainAnima.PlayForward();
			this.TempAnima.PlayForward();
			this.MainPage.UnLoadFakeObj();
			UnityVersionUtil.SetActiveRecursive(this.TempPage.gameObject, true);
			this.TempPage.RefershInfo(this.SpePackList[this.CurShowIndex], this.ambientLight);
			this.SwapPage();
			this.SelectPoint();
		}
	}

	// Token: 0x06003D00 RID: 15616 RVA: 0x0010E624 File Offset: 0x0010C824
	public void OnClickRightBtn()
	{
		if (this.CurShowIndex < this.SpePackList.Count - 1)
		{
			this.isMoveFlag = true;
			this.moveDirRight = true;
			this.starttime = Time.time;
			this.CurShowIndex++;
			this.MainAnima.from = this.MainAnima.transform.localPosition;
			this.MainAnima.to = this.LeftPos;
			this.MainAnima.duration = this.MoveTime;
			this.MainAnima.ResetToBeginning();
			this.TempAnima.from = this.rightPos;
			this.TempAnima.to = Vector3.zero;
			this.TempAnima.duration = this.MoveTime;
			this.TempAnima.ResetToBeginning();
			this.MainAnima.PlayForward();
			this.TempAnima.PlayForward();
			this.MainPage.UnLoadFakeObj();
			UnityVersionUtil.SetActiveRecursive(this.TempPage.gameObject, true);
			this.TempPage.RefershInfo(this.SpePackList[this.CurShowIndex], this.ambientLight);
			this.SwapPage();
			this.SelectPoint();
		}
	}

	// Token: 0x06003D01 RID: 15617 RVA: 0x0010E758 File Offset: 0x0010C958
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BigPackRoot);
		if (SingletonUnity<AutoPopUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AutoPopUIRoot>.Instance.gameObject))
		{
			SingletonUnity<AutoPopUIRoot>.Instance.NextPop(false);
		}
	}

	// Token: 0x06003D02 RID: 15618 RVA: 0x0010E7A0 File Offset: 0x0010C9A0
	public void UpdateInfo(string id)
	{
		bool flag = true;
		for (int i = 0; i < this.SpePackList.Count; i++)
		{
			if (this.SpePackList[i].ID.Equals(id))
			{
				this.SpePackList[i].state = 1L;
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "BigSales", string.Format("require_{0}", id));
			}
			if (this.SpePackList[i].state == 0L)
			{
				flag = false;
			}
		}
		if (flag && SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.HideBigSaleBtn();
		}
		this.MainPage.UpdateInfo(id);
		this.TempPage.UpdateInfo(id);
	}

	// Token: 0x06003D03 RID: 15619 RVA: 0x0010E878 File Offset: 0x0010CA78
	public void SelectPoint()
	{
		for (int i = 0; i < this.PointList.Count; i++)
		{
			if (i == this.CurShowIndex)
			{
				this.PointList[i].color = new Color(0f, 1f, 1f);
			}
			else
			{
				this.PointList[i].color = new Color(0.27058825f, 0.27058825f, 0.27058825f);
			}
		}
		if (this.CurShowIndex == 0)
		{
			NGUITools.SetActive(this.LeftSp.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.LeftSp.gameObject, true);
		}
		if (this.CurShowIndex == this.SpePackList.Count - 1)
		{
			NGUITools.SetActive(this.RightSp.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.RightSp.gameObject, true);
		}
	}

	// Token: 0x06003D04 RID: 15620 RVA: 0x0010E970 File Offset: 0x0010CB70
	public void ResetPoint()
	{
		int num = this.SpePackList.Count - this.PointList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.PointList[0].gameObject) as GameObject;
				UISprite component = gameObject.GetComponent<UISprite>();
				gameObject.name = string.Format("point{0:D2}", this.PointList.Count);
				gameObject.transform.parent = this.PointGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				this.PointList.Add(component);
			}
		}
		for (int j = 0; j < this.PointList.Count; j++)
		{
			if (j < this.SpePackList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.PointList[j].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.PointList[j].gameObject, false);
			}
		}
		this.PointGrid.Reposition();
		if (this.SpePackList.Count <= 1)
		{
			UnityVersionUtil.SetActiveRecursive(this.PointGrid.gameObject, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.PointGrid.gameObject, true);
		}
	}

	// Token: 0x06003D05 RID: 15621 RVA: 0x0010EAD0 File Offset: 0x0010CCD0
	public void SwapPage()
	{
		TweenPosition mainAnima = this.MainAnima;
		this.MainAnima = this.TempAnima;
		this.TempAnima = mainAnima;
		BigPackPageLogic mainPage = this.MainPage;
		this.MainPage = this.TempPage;
		this.TempPage = mainPage;
	}

	// Token: 0x04002859 RID: 10329
	public TweenPosition MainAnima;

	// Token: 0x0400285A RID: 10330
	public TweenPosition TempAnima;

	// Token: 0x0400285B RID: 10331
	public BigPackPageLogic MainPage;

	// Token: 0x0400285C RID: 10332
	public BigPackPageLogic TempPage;

	// Token: 0x0400285D RID: 10333
	public UISprite LeftSp;

	// Token: 0x0400285E RID: 10334
	public UISprite RightSp;

	// Token: 0x0400285F RID: 10335
	public UIGrid PointGrid;

	// Token: 0x04002860 RID: 10336
	public List<UISprite> PointList;

	// Token: 0x04002861 RID: 10337
	public UIEventListener uidragEvent;

	// Token: 0x04002862 RID: 10338
	private List<special_big_pack> SpePackList = new List<special_big_pack>();

	// Token: 0x04002863 RID: 10339
	private int CurShowIndex;

	// Token: 0x04002864 RID: 10340
	private Vector3 LeftPos = new Vector3(-1000f, 0f, 0f);

	// Token: 0x04002865 RID: 10341
	private Vector3 rightPos = new Vector3(1000f, 0f, 0f);

	// Token: 0x04002866 RID: 10342
	private float MoveTime = 0.5f;

	// Token: 0x04002867 RID: 10343
	private float temptime;

	// Token: 0x04002868 RID: 10344
	private bool isMoveFlag;

	// Token: 0x04002869 RID: 10345
	private bool textureFinish;

	// Token: 0x0400286A RID: 10346
	private bool requestFinish;

	// Token: 0x0400286B RID: 10347
	private float mWaitTime = 10f;

	// Token: 0x0400286C RID: 10348
	private List<string> textureList = new List<string>();

	// Token: 0x0400286D RID: 10349
	private float starttime;

	// Token: 0x0400286E RID: 10350
	private float DelAutoDragtime = 5f;

	// Token: 0x0400286F RID: 10351
	public string TargetShowPack = string.Empty;

	// Token: 0x04002870 RID: 10352
	private Color ambientLight;

	// Token: 0x04002871 RID: 10353
	private bool moveDirRight = true;
}
