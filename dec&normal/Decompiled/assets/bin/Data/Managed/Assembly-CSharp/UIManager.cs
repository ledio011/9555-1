using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A5F RID: 2655
public class UIManager : SingletonUnity<UIManager>
{
	// Token: 0x06004D41 RID: 19777 RVA: 0x001A5D2C File Offset: 0x001A3F2C
	protected override void Awake()
	{
		base.Awake();
		this.Init();
		GameObject res = ResourcesManager.LoadAndInstantiate("UIRoot/EmptyPanel") as GameObject;
		if (this.BaseUIRoot == null)
		{
			this.BaseUIRoot = this.CreateRootObj(res, "BaseUIRoot", 10);
		}
		if (this.PopUIRoot == null)
		{
			this.PopUIRoot = this.CreateRootObj(res, "PopUIRoot", 20);
		}
		if (this.MenuPopUIRoot == null)
		{
			this.MenuPopUIRoot = this.CreateRootObj(res, "MenuPopUIRoot", 30);
		}
		if (this.MenuTopUIRoot == null)
		{
			this.MenuTopUIRoot = this.CreateRootObj(res, "MenuTopUIRoot", 40);
		}
		if (this.MenuTop2UIRoot == null)
		{
			this.MenuTop2UIRoot = this.CreateRootObj(res, "MenuTop2UIRoot", 50);
		}
		if (this.MenuTop3UIRoot == null)
		{
			this.MenuTop3UIRoot = this.CreateRootObj(res, "MenuTop3UIRoot", 60);
		}
		if (this.MessageUIRoot == null)
		{
			this.MessageUIRoot = this.CreateRootObj(res, "MessageUIRoot", 70);
		}
		this.mHideObjList.Clear();
		this.CurShowUIList.Clear();
	}

	// Token: 0x06004D42 RID: 19778 RVA: 0x001A5E70 File Offset: 0x001A4070
	public static void Reset()
	{
		UIManager.NextShowType = UIManager.SHOW_TYPE.DEFAULT;
	}

	// Token: 0x06004D43 RID: 19779 RVA: 0x001A5E78 File Offset: 0x001A4078
	private void Start()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager != null)
		{
			this.ShowUI(UIInfo.LoadingUIRoot, null, null);
		}
	}

	// Token: 0x06004D44 RID: 19780 RVA: 0x001A5E98 File Offset: 0x001A4098
	private UIPanel CreateRootObj(GameObject res, string objName, int depth)
	{
		GameObject gameObject = Object.Instantiate(res) as GameObject;
		gameObject.gameObject.name = objName;
		gameObject.transform.parent = base.gameObject.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		UIPanel component = gameObject.GetComponent<UIPanel>();
		if (component != null)
		{
			component.depth = depth;
		}
		return component;
	}

	// Token: 0x06004D45 RID: 19781 RVA: 0x001A5F20 File Offset: 0x001A4120
	private void Init()
	{
		this.mDicBaseUI.Clear();
		this.mDicPopUI.Clear();
		this.mDicCacheUI.Clear();
		this.CurShowUIList.Clear();
	}

	// Token: 0x06004D46 RID: 19782 RVA: 0x001A5F5C File Offset: 0x001A415C
	public void ShowUI(UIPathData pathData, UIManager.OnOpenUIDelegate delOpenUI = null, object param = null)
	{
		Dictionary<string, GameObject> dictionary = null;
		switch (pathData.uiType)
		{
		case UIPathData.UIType.TYPE_BASE:
			dictionary = this.mDicBaseUI;
			break;
		case UIPathData.UIType.TYPE_POP:
			dictionary = this.mDicPopUI;
			break;
		case UIPathData.UIType.TYPE_MENU_POP:
			dictionary = this.mDicMenuPopUI;
			break;
		case UIPathData.UIType.TYPE_MENU_TOP:
			dictionary = this.mDicMenuTopUI;
			break;
		case UIPathData.UIType.TYPE_MEMU_TOP_2:
			dictionary = this.mDicMenuTop2UI;
			break;
		case UIPathData.UIType.TYPE_MEMU_TOP_3:
			dictionary = this.mDicMenuTop3UI;
			break;
		case UIPathData.UIType.TYPE_MESSAGE:
			dictionary = this.mDicMessageUI;
			break;
		}
		if (dictionary == null)
		{
			return;
		}
		if (pathData.hideBase)
		{
			if (!this.mHideObjList.Contains(pathData))
			{
				this.mHideObjList.Add(pathData);
			}
			this.HideBaseUI();
		}
		if (this.mDicCacheUI.ContainsKey(pathData.name))
		{
			if (!dictionary.ContainsKey(pathData.name))
			{
				dictionary.Add(pathData.name, this.mDicCacheUI[pathData.name]);
			}
			this.mDicCacheUI.Remove(pathData.name);
		}
		if (dictionary.ContainsKey(pathData.name))
		{
			this.DoShowUI(pathData, dictionary[pathData.name], delOpenUI, param);
			return;
		}
		this.LoadUI(pathData, delOpenUI, param);
	}

	// Token: 0x06004D47 RID: 19783 RVA: 0x001A60A8 File Offset: 0x001A42A8
	public void LoadUI(UIPathData pathData, UIManager.OnOpenUIDelegate delOpenUI = null, object param = null)
	{
		string text = "UI/" + pathData.path;
		text = text.Insert(text.LastIndexOf("/"), "/New");
		GameObject gameObject = ResourcesManager.Load(text) as GameObject;
		if (gameObject == null)
		{
			gameObject = (ResourcesManager.Load("UI/" + pathData.path) as GameObject);
		}
		if (gameObject != null)
		{
			this.DoShowUI(pathData, gameObject, delOpenUI, param);
			return;
		}
	}

	// Token: 0x06004D48 RID: 19784 RVA: 0x001A6128 File Offset: 0x001A4328
	public void LoadUIItem(UIPathData pathData, UIManager.OnLoadUIDelegate delOpenUI = null, object param = null)
	{
		GameObject gameObject = ResourcesManager.Load("UI/" + pathData.path) as GameObject;
		if (gameObject != null)
		{
			if (delOpenUI != null)
			{
				delOpenUI(gameObject, param);
			}
			return;
		}
	}

	// Token: 0x06004D49 RID: 19785 RVA: 0x001A616C File Offset: 0x001A436C
	private void DoShowUI(UIPathData pathData, GameObject curWindow, UIManager.OnOpenUIDelegate delOpenUI, object param = null)
	{
		if (curWindow == null)
		{
			return;
		}
		Dictionary<string, GameObject> dictionary = null;
		Transform transform;
		switch (pathData.uiType)
		{
		case UIPathData.UIType.TYPE_BASE:
			dictionary = this.mDicBaseUI;
			transform = this.BaseUIRoot.transform;
			break;
		case UIPathData.UIType.TYPE_POP:
			dictionary = this.mDicPopUI;
			transform = this.PopUIRoot.transform;
			break;
		case UIPathData.UIType.TYPE_MENU_POP:
			dictionary = this.mDicMenuPopUI;
			transform = this.MenuPopUIRoot.transform;
			break;
		case UIPathData.UIType.TYPE_MENU_TOP:
			dictionary = this.mDicMenuTopUI;
			transform = this.MenuTopUIRoot.transform;
			break;
		case UIPathData.UIType.TYPE_MEMU_TOP_2:
			dictionary = this.mDicMenuTop2UI;
			transform = this.MenuTop2UIRoot.transform;
			break;
		case UIPathData.UIType.TYPE_MEMU_TOP_3:
			dictionary = this.mDicMenuTop3UI;
			transform = this.MenuTop3UIRoot.transform;
			break;
		case UIPathData.UIType.TYPE_MESSAGE:
			dictionary = this.mDicMessageUI;
			transform = this.MessageUIRoot.transform;
			break;
		default:
			transform = UIRoot.list[0].transform;
			break;
		}
		if (dictionary != null && dictionary.ContainsKey(pathData.name))
		{
			if (!UnityVersionUtil.IsActive(dictionary[pathData.name]))
			{
				dictionary[pathData.name].transform.parent = transform;
				dictionary[pathData.name].transform.localScale = Vector3.one;
				dictionary[pathData.name].transform.localPosition = Vector3.zero;
				NGUITools.SetActive(dictionary[pathData.name], true);
			}
		}
		else if (dictionary != null)
		{
			GameObject gameObject = Object.Instantiate(curWindow) as GameObject;
			gameObject.transform.parent = transform;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localPosition = Vector3.zero;
			if (gameObject != null)
			{
				dictionary.Add(pathData.name, gameObject);
			}
		}
		if (delOpenUI != null)
		{
			delOpenUI(curWindow != null, param);
		}
		if (pathData.backFlag)
		{
			this.AddShowUIList(pathData);
		}
	}

	// Token: 0x06004D4A RID: 19786 RVA: 0x001A6384 File Offset: 0x001A4584
	private void AddShowUIList(UIPathData newpathdata)
	{
		if (this.CurShowUIList.Contains(newpathdata))
		{
			this.CurShowUIList.Remove(newpathdata);
		}
		this.CurShowUIList.Insert(0, newpathdata);
	}

	// Token: 0x06004D4B RID: 19787 RVA: 0x001A63B4 File Offset: 0x001A45B4
	private IEnumerator DelayCheckUnlockFunction(UIPathData pathData)
	{
		yield return null;
		if (!this.IsHideBaseUI && !SingletonUnity<LoadingUIRoot>.Exists)
		{
			if (pathData == null)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckShowUnlockFunction();
			}
			else if (TutorialManager.IsNeedCheckTutorial(pathData))
			{
				SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.CheckShowUnlockFunction();
			}
		}
		yield break;
	}

	// Token: 0x06004D4C RID: 19788 RVA: 0x001A63E0 File Offset: 0x001A45E0
	public void CloseUI(UIPathData pathData)
	{
		if (pathData.hideBase)
		{
			if (this.mHideObjList.Contains(pathData))
			{
				this.mHideObjList.Remove(pathData);
			}
			if (this.mHideObjList.Count == 0)
			{
				this.ReShowBaseUI();
			}
		}
		else if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(this.DelayCheckUnlockFunction(pathData));
		}
		switch (pathData.uiType)
		{
		case UIPathData.UIType.TYPE_BASE:
			this.CloseBaseUI(pathData);
			break;
		case UIPathData.UIType.TYPE_POP:
			this.ClosePopUI(pathData);
			break;
		case UIPathData.UIType.TYPE_MENU_POP:
			this.CloseMenuPopUI(pathData);
			break;
		case UIPathData.UIType.TYPE_MENU_TOP:
			this.CloseMenuTopUI(pathData);
			break;
		case UIPathData.UIType.TYPE_MEMU_TOP_2:
			this.CloseMenuTop2UI(pathData);
			break;
		case UIPathData.UIType.TYPE_MEMU_TOP_3:
			this.CloseMenuTop3UI(pathData);
			break;
		case UIPathData.UIType.TYPE_MESSAGE:
			this.CloseMessageUI(pathData);
			break;
		}
		if (pathData.backFlag)
		{
			this.RemoveShowList(pathData);
		}
	}

	// Token: 0x06004D4D RID: 19789 RVA: 0x001A64E4 File Offset: 0x001A46E4
	private void RemoveShowList(UIPathData pathData)
	{
		if (this.CurShowUIList.Contains(pathData))
		{
			this.CurShowUIList.Remove(pathData);
		}
	}

	// Token: 0x06004D4E RID: 19790 RVA: 0x001A6504 File Offset: 0x001A4704
	private void DestroyUI(UIPathData pathData, GameObject obj)
	{
		Object.Destroy(obj);
	}

	// Token: 0x06004D4F RID: 19791 RVA: 0x001A650C File Offset: 0x001A470C
	private void CloseBaseUI(UIPathData pathData)
	{
		if (this.mDicBaseUI.ContainsKey(pathData.name))
		{
			NGUITools.SetActive(this.mDicBaseUI[pathData.name], false);
		}
	}

	// Token: 0x06004D50 RID: 19792 RVA: 0x001A653C File Offset: 0x001A473C
	private void CloseMessageUI(UIPathData pathData)
	{
		this.TryDestroyUI(this.mDicMessageUI, pathData);
	}

	// Token: 0x06004D51 RID: 19793 RVA: 0x001A654C File Offset: 0x001A474C
	private void CloseMenuPopUI(UIPathData pathData)
	{
		this.TryDestroyUI(this.mDicMenuPopUI, pathData);
	}

	// Token: 0x06004D52 RID: 19794 RVA: 0x001A655C File Offset: 0x001A475C
	private void ClosePopUI(UIPathData pathData)
	{
		this.TryDestroyUI(this.mDicPopUI, pathData);
	}

	// Token: 0x06004D53 RID: 19795 RVA: 0x001A656C File Offset: 0x001A476C
	private void CloseMenuTopUI(UIPathData pathData)
	{
		this.TryDestroyUI(this.mDicMenuTopUI, pathData);
	}

	// Token: 0x06004D54 RID: 19796 RVA: 0x001A657C File Offset: 0x001A477C
	private void CloseMenuTop2UI(UIPathData pathData)
	{
		this.TryDestroyUI(this.mDicMenuTop2UI, pathData);
	}

	// Token: 0x06004D55 RID: 19797 RVA: 0x001A658C File Offset: 0x001A478C
	private void CloseMenuTop3UI(UIPathData pathData)
	{
		this.TryDestroyUI(this.mDicMenuTop3UI, pathData);
	}

	// Token: 0x06004D56 RID: 19798 RVA: 0x001A659C File Offset: 0x001A479C
	private void TryDestroyUI(Dictionary<string, GameObject> dict, UIPathData pathData)
	{
		if (dict == null)
		{
			return;
		}
		string name = pathData.name;
		if (!dict.ContainsKey(name))
		{
			return;
		}
		if (!pathData.isDestoryOnUnload)
		{
			NGUITools.SetActive(dict[name], false);
			this.mDicCacheUI.Add(name, dict[name]);
		}
		else
		{
			this.DestroyUI(pathData, dict[name]);
		}
		dict.Remove(pathData.name);
	}

	// Token: 0x06004D57 RID: 19799 RVA: 0x001A6610 File Offset: 0x001A4810
	public bool CheckUIExit(UIPathData pathData)
	{
		switch (pathData.uiType)
		{
		case UIPathData.UIType.TYPE_BASE:
			if (this.mDicBaseUI.ContainsKey(pathData.name))
			{
				return true;
			}
			break;
		case UIPathData.UIType.TYPE_POP:
			if (this.mDicPopUI.ContainsKey(pathData.name))
			{
				return true;
			}
			break;
		case UIPathData.UIType.TYPE_MENU_POP:
			if (this.mDicMenuPopUI.ContainsKey(pathData.name))
			{
				return true;
			}
			break;
		case UIPathData.UIType.TYPE_MENU_TOP:
			if (this.mDicMenuTopUI.ContainsKey(pathData.name))
			{
				return true;
			}
			break;
		case UIPathData.UIType.TYPE_MEMU_TOP_2:
			if (this.mDicMenuTop2UI.ContainsKey(pathData.name))
			{
				return true;
			}
			break;
		case UIPathData.UIType.TYPE_MEMU_TOP_3:
			if (this.mDicMenuTop3UI.ContainsKey(pathData.name))
			{
				return true;
			}
			break;
		case UIPathData.UIType.TYPE_MESSAGE:
			if (this.mDicMessageUI.ContainsKey(pathData.name))
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x06004D58 RID: 19800 RVA: 0x001A6718 File Offset: 0x001A4918
	public void CloseOtherPlayerUI()
	{
		if (SingletonUnity<OtherPlayerInfoUILogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<OtherPlayerInfoUILogic>.Instance.gameObject))
		{
			this.CloseUI(UIInfo.OtherPlayerInfoUILogicRoot);
		}
		if (SingletonUnity<HitOtherPLayerLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<HitOtherPLayerLogic>.Instance.gameObject))
		{
			this.CloseUI(UIInfo.HitOtherPlayerRoot);
		}
	}

	// Token: 0x06004D59 RID: 19801 RVA: 0x001A6778 File Offset: 0x001A4978
	public bool CloseAllPOPUI()
	{
		if (UIManager.IsUnlockTutorialEnable())
		{
			Debug.Log("Tutorial Is On, Can't Close All Pop UI!!!!!!!!!!!!!!!!!");
			return false;
		}
		List<string> list = new List<string>(this.mDicPopUI.Keys);
		this.AddReShowUI(list);
		for (int i = 0; i < list.Count; i++)
		{
			UIPathData pathData = UIPathData.UINameDic[list[i]];
			this.CloseUI(pathData);
		}
		if (SingletonUnity<SocialUIRootLogic>.Exists)
		{
			SingletonUnity<SocialUIRootLogic>.Instance.CloseSocialUI();
		}
		list = new List<string>(this.mDicMenuPopUI.Keys);
		this.AddReShowUI(list);
		for (int j = 0; j < list.Count; j++)
		{
			UIPathData pathData2 = UIPathData.UINameDic[list[j]];
			this.CloseUI(pathData2);
		}
		list = new List<string>(this.mDicMenuTopUI.Keys);
		this.AddReShowUI(list);
		for (int k = 0; k < list.Count; k++)
		{
			UIPathData pathData3 = UIPathData.UINameDic[list[k]];
			this.CloseUI(pathData3);
		}
		list = new List<string>(this.mDicMenuTop2UI.Keys);
		this.AddReShowUI(list);
		for (int l = 0; l < list.Count; l++)
		{
			UIPathData pathData4 = UIPathData.UINameDic[list[l]];
			this.CloseUI(pathData4);
		}
		this.CloseUI(UIInfo.ItemInfoRoot);
		this.CloseUI(UIInfo.ItemInfoRootNew);
		this.CloseUI(UIInfo.NumRoot);
		this.CloseUI(UIInfo.OpenBoxRoot);
		this.CloseUI(UIInfo.ChatRoot);
		return true;
	}

	// Token: 0x06004D5A RID: 19802 RVA: 0x001A6918 File Offset: 0x001A4B18
	public void UICheckChangeScene()
	{
		UIManager.CanReShowUIList.Clear();
		List<string> uinamelist = new List<string>(this.mDicPopUI.Keys);
		this.AddReShowUI(uinamelist);
		uinamelist = new List<string>(this.mDicMenuPopUI.Keys);
		this.AddReShowUI(uinamelist);
		uinamelist = new List<string>(this.mDicMenuTopUI.Keys);
		this.AddReShowUI(uinamelist);
		uinamelist = new List<string>(this.mDicMenuTop2UI.Keys);
		this.AddReShowUI(uinamelist);
	}

	// Token: 0x06004D5B RID: 19803 RVA: 0x001A6990 File Offset: 0x001A4B90
	public void AddReShowUI(List<string> uinamelist)
	{
		for (int i = 0; i < uinamelist.Count; i++)
		{
			UIPathData uipathData = UIPathData.UINameDic[uinamelist[i]];
			if (uipathData.ReShowFlag && !UIManager.CanReShowUIList.Contains(uinamelist[i]))
			{
				UIManager.CanReShowUIList.Add(uinamelist[i]);
			}
		}
	}

	// Token: 0x06004D5C RID: 19804 RVA: 0x001A69F8 File Offset: 0x001A4BF8
	public void HideBaseUI()
	{
		this.IsHideBaseUI = true;
		this.BaseUIRoot.transform.localPosition = new Vector3(0f, 2000f, 0f);
		if (SingletonUnity<JoyStickLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<JoyStickLogic>.Instance.gameObject))
		{
			SingletonUnity<JoyStickLogic>.Instance.MoveOutScreen();
		}
		if (SingletonUnity<ChatUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ChatUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ChatRoot);
		}
	}

	// Token: 0x06004D5D RID: 19805 RVA: 0x001A6A88 File Offset: 0x001A4C88
	public void ReShowBaseUI()
	{
		this.IsHideBaseUI = false;
		this.BaseUIRoot.transform.localPosition = Vector3.zero;
		if (!SingletonUnity<TutorialUIRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject))
		{
			if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
			{
				base.StartCoroutine(this.DelayCheckUnlockFunction(null));
			}
		}
		if (UIUpdateEvent.OnReshowBase != null)
		{
			UIUpdateEvent.OnReshowBase();
		}
	}

	// Token: 0x06004D5E RID: 19806 RVA: 0x001A6B08 File Offset: 0x001A4D08
	public void ShowBaseUI()
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager.IsTutorialScene())
		{
			return;
		}
		if (sceneManager.IsCarScene())
		{
			return;
		}
		if (sceneManager.IsPvPScene())
		{
			return;
		}
		if (sceneManager.CurrentMapInofData.MapType == MAPTYPE.ANIMA_EDITOR)
		{
			return;
		}
		this.ShowDefaultUI();
	}

	// Token: 0x06004D5F RID: 19807 RVA: 0x001A6B60 File Offset: 0x001A4D60
	public void ShowTutorialDefaultUI()
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		this.ShowUI(UIInfo.LevelUpUIRoot, delegate(bool bSuccess, object param)
		{
			this.CloseUI(UIInfo.LevelUpUIRoot);
		}, null);
		this.ShowUI(UIInfo.YiDongKongZhiUI, null, null);
		this.ShowUI(UIInfo.JueseJiNengQuUI, delegate
		{
			SingletonUnity<JueseJiNengQuLogic>.Instance.Reset(true);
		}, null);
		this.ShowUI(UIInfo.ExpLineRoot, null, null);
		this.ShowUI(UIInfo.SelectTargetUI, null, null);
		this.ShowUI(UIInfo.NotifyRootUI, null, null);
		this.ShowUI(UIInfo.TouXiangKuangUI, delegate(bool isSuccess, object param)
		{
			if (isSuccess)
			{
				SingletonUnity<TouXiangKuangLogic>.Instance.Init();
			}
		}, null);
	}

	// Token: 0x06004D60 RID: 19808 RVA: 0x001A6C18 File Offset: 0x001A4E18
	public void ShowCarDefaultUI()
	{
		this.ShowUI(UIInfo.ExpLineRoot, null, null);
		this.ShowUI(UIInfo.NotifyRootUI, null, null);
		this.ShowUI(UIInfo.CarControllerRoot, delegate
		{
			SingletonUnity<CarControllerRootLogic>.Instance.Reset();
		}, null);
		MapInfoData mapInfo = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData;
		if (!SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene())
		{
			this.ShowUI(UIInfo.CopyFunctionBtnRoot, delegate
			{
				SingletonUnity<CopyFunctionRootLogic>.Instance.Reset(mapInfo.MapType, mapInfo.Name);
			}, null);
		}
		this.ShowUI(UIInfo.MiniMapRoot, delegate
		{
			SingletonUnity<MiniMap>.Instance.Reset(mapInfo.fMapLength, mapInfo.fMapHeight, (mapInfo.fMapLength + mapInfo.fMapHeight) / 6f, mapInfo.MiniMapName, mapInfo.Name);
		}, null);
		this.ShowUI(UIInfo.BroadCastRoot, null, null);
	}

	// Token: 0x06004D61 RID: 19809 RVA: 0x001A6CD4 File Offset: 0x001A4ED4
	public void ShowLowPhoneUI()
	{
		this.ShowUI(UIInfo.YiDongKongZhiUI, null, null);
		this.ShowUI(UIInfo.JueseJiNengQuUI, null, null);
	}

	// Token: 0x06004D62 RID: 19810 RVA: 0x001A6CF0 File Offset: 0x001A4EF0
	public static void SetSpecialType(UIManager.SHOW_TYPE type)
	{
		UIManager.NextShowType = type;
	}

	// Token: 0x06004D63 RID: 19811 RVA: 0x001A6CF8 File Offset: 0x001A4EF8
	public bool ShowSpecialUI()
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if ((sceneManager.IsBigWorld() || sceneManager.IsTutorialScene()) && UIManager.NextShowType == UIManager.SHOW_TYPE.RANK_PVP)
		{
			UIManager.NextShowType = UIManager.SHOW_TYPE.DEFAULT;
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.ResetToPVP();
			}, null);
			return true;
		}
		return false;
	}

	// Token: 0x06004D64 RID: 19812 RVA: 0x001A6D68 File Offset: 0x001A4F68
	public bool ShowSpecialRebirthUI()
	{
		if (UIManager.NextShowType == UIManager.SHOW_TYPE.DEFAULT)
		{
			return false;
		}
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager.IsBigWorld() || sceneManager.IsTutorialScene())
		{
			if (UIManager.NextShowType == UIManager.SHOW_TYPE.BIGSALE)
			{
				UIManager.NextShowType = UIManager.SHOW_TYPE.DEFAULT;
				this.ShowUI(UIInfo.BigPackRoot, delegate
				{
					SingletonUnity<BigPackRootLogic>.Instance.EnableReset();
					WaitResponseUIRootLogic.OpenWaitBox(260, 10f, 0f, null);
					NetLogic.GetInstance().Send<Protocol.request_big_pack>(null, null);
				}, null);
				return true;
			}
			if (UIManager.NextShowType == UIManager.SHOW_TYPE.SHOP)
			{
				UIManager.NextShowType = UIManager.SHOW_TYPE.DEFAULT;
				this.ShowUI(UIInfo.ShopRoot, delegate
				{
					SingletonUnity<ShopUIRootLogic>.Instance.OnClickEquipBtn(GameDefine.SHOP_TAB_TYPE.TICKET, GameDefine.UIBACKTYPE.NOTHINTG, null);
				}, null);
				return true;
			}
			if (UIManager.NextShowType == UIManager.SHOW_TYPE.SLOT)
			{
				UIManager.NextShowType = UIManager.SHOW_TYPE.DEFAULT;
				this.ShowUI(UIInfo.SlotUIRoot, delegate
				{
					SingletonUnity<SlotUIRootLogic>.Instance.EnableReset();
					WaitResponseUIRootLogic.OpenWaitBox(242, 10f, 0f, null);
					NetLogic.GetInstance().Send<Protocol.request_slot_info>(null, null);
				}, null);
				return true;
			}
			if (UIManager.NextShowType == UIManager.SHOW_TYPE.WELFARE)
			{
				UIManager.NextShowType = UIManager.SHOW_TYPE.DEFAULT;
				this.ShowUI(UIInfo.CommercialUIRoot, delegate
				{
					SingletonUnity<CommercialUIRootLogic>.Instance.Reset();
				}, null);
				return true;
			}
			if (UIManager.NextShowType == UIManager.SHOW_TYPE.MYSTERY)
			{
				UIManager.NextShowType = UIManager.SHOW_TYPE.DEFAULT;
				this.ShowUI(UIInfo.MysteryShopRoot, delegate
				{
					SingletonUnity<MysteryShopRootLogic>.Instance.EnableReset();
					WaitResponseUIRootLogic.OpenWaitBox(274, 10f, 0f, null);
					NetLogic.GetInstance().Send<Protocol.request_special_big_pack>(null, null);
				}, null);
				return true;
			}
			if (UIManager.NextShowType == UIManager.SHOW_TYPE.SKILL)
			{
				UIManager.NextShowType = UIManager.SHOW_TYPE.DEFAULT;
				this.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
				{
					SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowSkillInfo(EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
				}, null);
				return true;
			}
			if (UIManager.NextShowType == UIManager.SHOW_TYPE.ENHANCE_EQUIP)
			{
				UIManager.NextShowType = UIManager.SHOW_TYPE.DEFAULT;
				this.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
				{
					SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipEnhance(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
				}, null);
				return true;
			}
			if (UIManager.NextShowType == UIManager.SHOW_TYPE.ENHANCE_STAR)
			{
				UIManager.NextShowType = UIManager.SHOW_TYPE.DEFAULT;
				this.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
				{
					SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipRefine();
				}, null);
				return true;
			}
			if (UIManager.NextShowType == UIManager.SHOW_TYPE.INHERT)
			{
				UIManager.NextShowType = UIManager.SHOW_TYPE.DEFAULT;
				this.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
				{
					SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickEquipBackPackBtn();
				}, null);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004D65 RID: 19813 RVA: 0x001A6FC4 File Offset: 0x001A51C4
	public void ShowDefaultUI()
	{
		SceneManager curSceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		this.ShowUI(UIInfo.YiDongKongZhiUI, null, null);
		this.ShowUI(UIInfo.JueseJiNengQuUI, null, null);
		if (!GameSettingData.IsLowPhone)
		{
			this.ShowUI(UIInfo.ExpLineRoot, null, null);
		}
		this.ShowUI(UIInfo.SelectTargetUI, null, null);
		if (curSceneManager.IsLowPhoneManager())
		{
			MapInfoData currentMapInofData = curSceneManager.CurrentMapInofData;
			this.ShowUI(UIInfo.TouXiangKuangUI, delegate(bool isSuccess, object param)
			{
				if (isSuccess)
				{
					SingletonUnity<TouXiangKuangLogic>.Instance.Init();
				}
			}, null);
			return;
		}
		this.ShowUI(UIInfo.NotifyRootUI, null, null);
		this.ShowUI(UIInfo.SocialUIRootLogic, null, null);
		this.ShowUI(UIInfo.LevelUpUIRoot, delegate(bool bSuccess, object param)
		{
			this.CloseUI(UIInfo.LevelUpUIRoot);
		}, null);
		if (curSceneManager.IsBigWorld() || curSceneManager.IsTutorialScene())
		{
			this.ShowUI(UIInfo.MissionTeamTipRootUI, delegate
			{
				SingletonUnity<MissionTeamTipLogic>.Instance.EnableReset();
			}, null);
			this.ShowUI(UIInfo.FunctionBtnRootUI, delegate
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.Reset();
			}, null);
			MapInfoData mapInfo = curSceneManager.CurrentMapInofData;
			this.ShowUI(UIInfo.MiniMapRoot, delegate
			{
				SingletonUnity<MiniMap>.Instance.Reset(mapInfo.fMapLength, mapInfo.fMapHeight, (mapInfo.fMapLength + mapInfo.fMapHeight) / 6f, mapInfo.MiniMapName, mapInfo.Name);
			}, null);
			this.ShowUI(UIInfo.CountTimeRoot, null, null);
		}
		else
		{
			if (curSceneManager.IsCopyShowMap())
			{
				MapInfoData mapInfo = curSceneManager.CurrentMapInofData;
				this.ShowUI(UIInfo.MiniMapRoot, delegate
				{
					SingletonUnity<MiniMap>.Instance.Reset(mapInfo.fMapLength, mapInfo.fMapHeight, (mapInfo.fMapLength + mapInfo.fMapHeight) / 6f, mapInfo.MiniMapName, mapInfo.Name);
				}, null);
				this.ShowUI(UIInfo.CopyFunctionBtnRoot, delegate
				{
					SingletonUnity<CopyFunctionRootLogic>.Instance.Reset(curSceneManager.CurrentMapInofData.MapType, curSceneManager.CurrentMapInofData.Name);
				}, null);
			}
			else
			{
				this.ShowUI(UIInfo.CopyFunctionBtnRoot, delegate
				{
					SingletonUnity<CopyFunctionRootLogic>.Instance.Reset(curSceneManager.CurrentMapInofData.MapType, curSceneManager.CurrentMapInofData.Name);
				}, null);
			}
			this.ShowUI(UIInfo.CountTimeRoot, null, null);
			if (!curSceneManager.IsRankPvPScene())
			{
				this.ShowUI(UIInfo.CopyDrugUseUIRoot, delegate
				{
					SingletonUnity<CopyDrugUseUIRoot>.Instance.Reset();
				}, null);
			}
			this.ShowUI(UIInfo.ChatBaseRoot, delegate
			{
				SingletonUnity<ChatBaseRootLogic>.Instance.UpdateMessage();
			}, null);
		}
		if (curSceneManager.IsCanUsePotion() && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
		{
			this.ShowUI(UIInfo.PotionObjRoot, delegate(bool bSuccess, object param)
			{
				SingletonUnity<PotionLogic>.Instance.Reset();
			}, null);
		}
		this.ShowUI(UIInfo.TouXiangKuangUI, delegate(bool isSuccess, object param)
		{
			if (isSuccess)
			{
				SingletonUnity<TouXiangKuangLogic>.Instance.Init();
			}
		}, null);
		this.ShowUI(UIInfo.BroadCastRoot, null, null);
	}

	// Token: 0x06004D66 RID: 19814 RVA: 0x001A72C4 File Offset: 0x001A54C4
	private void CheckTipUI()
	{
	}

	// Token: 0x06004D67 RID: 19815 RVA: 0x001A72C8 File Offset: 0x001A54C8
	public static bool IsUnlockTutorialEnable()
	{
		return (SingletonUnity<UnlockFunctionRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<UnlockFunctionRootLogic>.Instance.gameObject)) || (SingletonUnity<TutorialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject)) || (SingletonUnity<TeamPreparationRootLogic>.Exists && UnityVersionUtil.IsactiveInHierarchy(SingletonUnity<TeamPreparationRootLogic>.Instance.gameObject));
	}

	// Token: 0x06004D68 RID: 19816 RVA: 0x001A7334 File Offset: 0x001A5534
	public static bool IsPopMessageCanShow()
	{
		return !SingletonUnity<SexGameUIRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<SexGameUIRootLogic>.Instance.gameObject);
	}

	// Token: 0x06004D69 RID: 19817 RVA: 0x001A7358 File Offset: 0x001A5558
	public void CheckFunctionTop(bool isshowbossline)
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager != null && SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsBigWorld() && SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
		{
			if (isshowbossline)
			{
				if (SingletonUnity<FunctionBtnRootLogic>.Instance.IsOpenLeftBtn)
				{
					SingletonUnity<FunctionBtnRootLogic>.Instance.OnClickLeftArrowBtn(false);
				}
			}
			else if (!SingletonUnity<FunctionBtnRootLogic>.Instance.IsOpenLeftBtn)
			{
				SingletonUnity<FunctionBtnRootLogic>.Instance.OnClickLeftArrowBtn(false);
			}
		}
	}

	// Token: 0x06004D6A RID: 19818 RVA: 0x001A73E8 File Offset: 0x001A55E8
	public bool BackUIFun()
	{
		if (UIManager.IsUnlockTutorialEnable())
		{
			return true;
		}
		if (this.CurShowUIList.Count <= 0)
		{
			return false;
		}
		UIPathData uipathData = this.CurShowUIList[0];
		if (uipathData.needSelfClose)
		{
			return true;
		}
		this.CurShowUIList.RemoveAt(0);
		if (uipathData.name.Equals("ExitGameRoot"))
		{
			return false;
		}
		if (uipathData.name.Equals("ChooseRoleRoot"))
		{
			if (SingletonUnity<ChooseRoleRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ChooseRoleRootLogic>.Instance.gameObject))
			{
				SingletonUnity<ChooseRoleRootLogic>.Instance.OnClickBackBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("MenuBaseRoot"))
		{
			if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("OptionUIRootLogic"))
		{
			if (SingletonUnity<OptionUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<OptionUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<OptionUIRootLogic>.Instance.OnClickClose();
				return true;
			}
		}
		else if (uipathData.name.Equals("OtherPlayerInfoUILogicRoot"))
		{
			if (SingletonUnity<OtherPlayerInfoUILogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<OtherPlayerInfoUILogic>.Instance.gameObject))
			{
				SingletonUnity<OtherPlayerInfoUILogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("CreateTeamRoot"))
		{
			if (SingletonUnity<CreateTeamRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CreateTeamRootLogic>.Instance.gameObject))
			{
				SingletonUnity<CreateTeamRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("SearchTeamRoot"))
		{
			if (SingletonUnity<SearchTeamRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SearchTeamRootLogic>.Instance.gameObject))
			{
				SingletonUnity<SearchTeamRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("TeamApplyListRoot"))
		{
			if (SingletonUnity<TeamApplyListLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TeamApplyListLogic>.Instance.gameObject))
			{
				SingletonUnity<TeamApplyListLogic>.Instance.OnClickClose();
				return true;
			}
		}
		else if (uipathData.name.Equals("TeamInviteRoot"))
		{
			if (SingletonUnity<TeamInviteRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TeamInviteRootLogic>.Instance.gameObject))
			{
				SingletonUnity<TeamInviteRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("ItemInfoRoot"))
		{
			if (SingletonUnity<ItemInfoRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ItemInfoRootLogic>.Instance.gameObject))
			{
				SingletonUnity<ItemInfoRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("ItemInfoRootNew"))
		{
			if (SingletonUnity<ItemInfoRootLogicNew>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ItemInfoRootLogicNew>.Instance.gameObject))
			{
				SingletonUnity<ItemInfoRootLogicNew>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("MissionPageRoot"))
		{
			if (SingletonUnity<MissionPageRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MissionPageRootLogic>.Instance.gameObject))
			{
				SingletonUnity<MissionPageRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("CopyFunctionBtnRoot"))
		{
			if (SingletonUnity<CopyFunctionRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CopyFunctionRootLogic>.Instance.gameObject))
			{
				SingletonUnity<CopyFunctionRootLogic>.Instance.OnClickLeaveCopyBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("HitOtherPlayerUIRoot"))
		{
			if (SingletonUnity<HitOtherPLayerLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<HitOtherPLayerLogic>.Instance.gameObject))
			{
				SingletonUnity<HitOtherPLayerLogic>.Instance.Close();
				return true;
			}
		}
		else if (uipathData.name.Equals("ExcInfoRoot"))
		{
			if (SingletonUnity<ExcInfoRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ExcInfoRootLogic>.Instance.gameObject))
			{
				SingletonUnity<ExcInfoRootLogic>.Instance.Close();
				return true;
			}
		}
		else if (uipathData.name.Equals("ReportRoot"))
		{
			if (SingletonUnity<ReportRootUILogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ReportRootUILogic>.Instance.gameObject))
			{
				SingletonUnity<ReportRootUILogic>.Instance.OnClickClose();
				return true;
			}
		}
		else if (uipathData.name.Equals("ChooseServerRoot"))
		{
			if (SingletonUnity<ChooseServerRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ChooseServerRootLogic>.Instance.gameObject))
			{
				SingletonUnity<ChooseServerRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("LoginNoticeRoot"))
		{
			if (SingletonUnity<LoginNoticeRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<LoginNoticeRootLogic>.Instance.gameObject))
			{
				SingletonUnity<LoginNoticeRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("MapUIRoot"))
		{
			if (SingletonUnity<MapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MapUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<MapUIRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("PVPLogUIRoot"))
		{
			if (SingletonUnity<PVPLogUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PVPLogUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<PVPLogUIRootLogic>.Instance.OnlClickClose();
				return true;
			}
		}
		else if (uipathData.name.Equals("GuildLogUIRootLogic"))
		{
			if (SingletonUnity<GuildLogUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildLogUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildLogUIRootLogic>.Instance.OnlClickClose();
				return true;
			}
		}
		else if (uipathData.name.Equals("GuildApplyListLogicRoot"))
		{
			if (SingletonUnity<GuildApplyListLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildApplyListLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildApplyListLogic>.Instance.OnClickClose();
				return true;
			}
		}
		else if (uipathData.name.Equals("PopShopRoot"))
		{
			if (SingletonUnity<PopShopRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PopShopRootLogic>.Instance.gameObject))
			{
				SingletonUnity<PopShopRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("PopDiamondBuyRoot"))
		{
			if (SingletonUnity<PopDiamondBuyRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PopDiamondBuyRootLogic>.Instance.gameObject))
			{
				SingletonUnity<PopDiamondBuyRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("PopTopShopRoot"))
		{
			if (SingletonUnity<PopTopShopRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PopTopShopRootLogic>.Instance.gameObject))
			{
				SingletonUnity<PopTopShopRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("PopTopDiamondBuyRoot"))
		{
			if (SingletonUnity<PopTopDiamondBuyRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PopTopDiamondBuyRootLogic>.Instance.gameObject))
			{
				SingletonUnity<PopTopDiamondBuyRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("NumRoot"))
		{
			if (SingletonUnity<NumRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NumRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NumRootLogic>.Instance.OnClickClose();
				return true;
			}
		}
		else if (uipathData.name.Equals("FriendAddUILogic"))
		{
			if (SingletonUnity<FriendAddUILogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FriendAddUILogic>.Instance.gameObject))
			{
				SingletonUnity<FriendAddUILogic>.Instance.OnClickClose();
				return true;
			}
		}
		else if (uipathData.name.Equals("MapLineInfoLogic"))
		{
			if (SingletonUnity<MapLineInfoLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MapLineInfoLogic>.Instance.gameObject))
			{
				SingletonUnity<MapLineInfoLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("SlotLogRoot"))
		{
			if (SingletonUnity<SlotLogRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SlotLogRootLogic>.Instance.gameObject))
			{
				SingletonUnity<SlotLogRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("SlotRuleRoot"))
		{
			if (SingletonUnity<SlotRuleRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SlotRuleRootLogic>.Instance.gameObject))
			{
				SingletonUnity<SlotRuleRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("FirstBuyRoot"))
		{
			if (SingletonUnity<FirstBuyRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FirstBuyRootLogic>.Instance.gameObject))
			{
				SingletonUnity<FirstBuyRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("BigPackRoot"))
		{
			if (SingletonUnity<BigPackRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<BigPackRootLogic>.Instance.gameObject))
			{
				SingletonUnity<BigPackRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("RateRoot"))
		{
			if (SingletonUnity<RateRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<RateRootLogic>.Instance.gameObject))
			{
				SingletonUnity<RateRootLogic>.Instance.CloseRate();
				return true;
			}
		}
		else if (uipathData.name.Equals("TimerActivityTipsRoot"))
		{
			if (SingletonUnity<TimerActivityTipsRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TimerActivityTipsRootLogic>.Instance.gameObject))
			{
				SingletonUnity<TimerActivityTipsRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("RankPvpShowRewardRoot"))
		{
			if (SingletonUnity<RankPVPRewardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<RankPVPRewardRootLogic>.Instance.gameObject))
			{
				SingletonUnity<RankPVPRewardRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("GuildBattleRankRoot"))
		{
			if (SingletonUnity<GuildBattleRankRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildBattleRankRoot>.Instance.gameObject))
			{
				SingletonUnity<GuildBattleRankRoot>.Instance.OnClickCloseBtn();
			}
		}
		else if (uipathData.name.Equals("LevelRewardRoot"))
		{
			if (SingletonUnity<LevelRewardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<LevelRewardRootLogic>.Instance.gameObject))
			{
				SingletonUnity<LevelRewardRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("GuildBattleResultRoot"))
		{
			if (SingletonUnity<GuildBattleResultRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<GuildBattleResultRootLogic>.Instance.gameObject))
			{
				SingletonUnity<GuildBattleResultRootLogic>.Instance.OnClickExitBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("WorldMapRoot"))
		{
			if (SingletonUnity<WorldMapRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<WorldMapRoot>.Instance.gameObject))
			{
				SingletonUnity<WorldMapRoot>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("EquipInhertRoot"))
		{
			if (SingletonUnity<EquipInhertRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<EquipInhertRootLogic>.Instance.gameObject))
			{
				SingletonUnity<EquipInhertRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("NewMessageUIRoot"))
		{
			if (SingletonUnity<NewMessageUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMessageUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewMessageUIRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("TeamBroadCastRoot"))
		{
			if (SingletonUnity<TeamBroadCastRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TeamBroadCastRootLogic>.Instance.gameObject))
			{
				SingletonUnity<TeamBroadCastRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("ChangeTimeRoot"))
		{
			if (SingletonUnity<ChangeTimeRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ChangeTimeRootLogic>.Instance.gameObject))
			{
				SingletonUnity<ChangeTimeRootLogic>.Instance.OnClickCancelBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("EnemyRevengeRoot"))
		{
			if (SingletonUnity<EnemyRevengeRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<EnemyRevengeRootLogic>.Instance.gameObject))
			{
				SingletonUnity<EnemyRevengeRootLogic>.Instance.OnClickCloseBtn();
				return true;
			}
		}
		else if (uipathData.name.Equals("CityDamageRoot") && SingletonUnity<CityDamageRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CityDamageRootLogic>.Instance.gameObject))
		{
			SingletonUnity<CityDamageRootLogic>.Instance.OnClickCloseBtn();
			return true;
		}
		return true;
	}

	// Token: 0x06004D6B RID: 19819 RVA: 0x001A8040 File Offset: 0x001A6240
	public void ClearReshowUI()
	{
		if (UIManager.CanReShowUIList == null)
		{
			UIManager.CanReShowUIList.Clear();
		}
	}

	// Token: 0x06004D6C RID: 19820 RVA: 0x001A8058 File Offset: 0x001A6258
	public bool CheckReShowUI(UIPathData pathData)
	{
		if (UIManager.IsUnlockTutorialEnable())
		{
			return false;
		}
		if (UIManager.CanReShowUIList == null || UIManager.CanReShowUIList.Count == 0)
		{
			return false;
		}
		if (pathData.name.Equals("StoryDialogUIRoot") || pathData.name.Equals("DialogMissionUIRoot") || pathData.name.Equals("DialogUIRoot") || pathData.name.Equals("OptionDialogUIRoot") || pathData.name.Equals("LoadingUIRoot"))
		{
			SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			string text = UIManager.CanReShowUIList[UIManager.CanReShowUIList.Count - 1];
			if (sceneManager.IsBigWorld())
			{
				if (text.Equals("NewMissionUIRootLogic"))
				{
					this.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
					{
						SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickMissionBtn();
					}, null);
				}
				else if (text.Equals("NewDailyCopyUIRootLogic"))
				{
					this.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
					{
						SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn();
					}, null);
				}
				else if (text.Equals("NewDailyActivityUIRoot"))
				{
					this.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
					{
						SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn();
					}, null);
				}
				else if (text.Equals("RankPVPRoot"))
				{
					this.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
					{
						SingletonUnity<NewActivityUIRootLogic>.Instance.ResetToPVP();
					}, null);
				}
				else if (text.Equals("SlotUIRoot"))
				{
					this.ShowUI(UIInfo.SlotUIRoot, delegate
					{
						SingletonUnity<SlotUIRootLogic>.Instance.EnableReset();
						WaitResponseUIRootLogic.OpenWaitBox(242, 10f, 0f, null);
						NetLogic.GetInstance().Send<Protocol.request_slot_info>(null, null);
					}, null);
				}
				else if (text.Equals("DailyActiveRewardRoot"))
				{
					this.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
					{
						SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyActiveBtn();
					}, null);
				}
				else if (text.Equals("EnhanceUIRootLogic"))
				{
					this.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
					{
						SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipEnhance(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
					}, null);
				}
				else if (text.Equals("RefineUIRootLogic"))
				{
					this.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
					{
						SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipRefine();
					}, null);
				}
				else if (text.Equals("BadgeMergeRoot"))
				{
					this.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
					{
						SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowBadgeMerge(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
					}, null);
				}
				else if (text.Equals("SkillInfoRoot"))
				{
					this.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate(bool bSuccess, object param)
					{
						SingletonUnity<EquipStrengthenUIRootLogic>.Instance.OnClickSkillBtn();
					}, null);
				}
				else if (text.Equals("ShopTabRootLogic"))
				{
					this.ShowUI(UIInfo.ShopRoot, delegate
					{
						SingletonUnity<ShopUIRootLogic>.Instance.OnClickToolsBtn();
					}, null);
				}
				else if (text.Equals("BuyDiamondRoot"))
				{
					this.ShowUI(UIInfo.ShopRoot, delegate
					{
						SingletonUnity<ShopUIRootLogic>.Instance.OnClickBuyDiamondBtn();
					}, null);
				}
				else if (text.Equals("PlayerCarRoot"))
				{
					this.ShowUI(UIInfo.PlayerCarRoot, delegate
					{
						SingletonUnity<PlayerCarRootLogic>.Instance.EnableReset();
						WaitResponseUIRootLogic.OpenWaitBox(235, 10f, 0f, null);
						NetLogic.GetInstance().Send<Protocol.request_mount_info>(null, null);
					}, null);
				}
			}
		}
		UIManager.CanReShowUIList.Clear();
		return true;
	}

	// Token: 0x04003C03 RID: 15363
	public static UIManager.SHOW_TYPE NextShowType = UIManager.SHOW_TYPE.DEFAULT;

	// Token: 0x04003C04 RID: 15364
	private Dictionary<string, GameObject> mDicBaseUI = new Dictionary<string, GameObject>();

	// Token: 0x04003C05 RID: 15365
	private Dictionary<string, GameObject> mDicPopUI = new Dictionary<string, GameObject>();

	// Token: 0x04003C06 RID: 15366
	private Dictionary<string, GameObject> mDicCacheUI = new Dictionary<string, GameObject>();

	// Token: 0x04003C07 RID: 15367
	private Dictionary<string, GameObject> mDicMenuPopUI = new Dictionary<string, GameObject>();

	// Token: 0x04003C08 RID: 15368
	private Dictionary<string, GameObject> mDicMessageUI = new Dictionary<string, GameObject>();

	// Token: 0x04003C09 RID: 15369
	private Dictionary<string, GameObject> mDicMenuTopUI = new Dictionary<string, GameObject>();

	// Token: 0x04003C0A RID: 15370
	private Dictionary<string, GameObject> mDicMenuTop2UI = new Dictionary<string, GameObject>();

	// Token: 0x04003C0B RID: 15371
	private Dictionary<string, GameObject> mDicMenuTop3UI = new Dictionary<string, GameObject>();

	// Token: 0x04003C0C RID: 15372
	private UIPanel BaseUIRoot;

	// Token: 0x04003C0D RID: 15373
	private UIPanel PopUIRoot;

	// Token: 0x04003C0E RID: 15374
	private UIPanel TipUIRoot;

	// Token: 0x04003C0F RID: 15375
	private UIPanel MenuPopUIRoot;

	// Token: 0x04003C10 RID: 15376
	private UIPanel MenuTop2UIRoot;

	// Token: 0x04003C11 RID: 15377
	private UIPanel MenuTopUIRoot;

	// Token: 0x04003C12 RID: 15378
	private UIPanel MenuTop3UIRoot;

	// Token: 0x04003C13 RID: 15379
	private UIPanel MessageUIRoot;

	// Token: 0x04003C14 RID: 15380
	private UIPanel DeathUIRoot;

	// Token: 0x04003C15 RID: 15381
	public List<UIPathData> mHideObjList = new List<UIPathData>();

	// Token: 0x04003C16 RID: 15382
	public List<UIPathData> CurShowUIList = new List<UIPathData>();

	// Token: 0x04003C17 RID: 15383
	public static List<string> CanReShowUIList = new List<string>();

	// Token: 0x04003C18 RID: 15384
	public bool IsHideBaseUI;

	// Token: 0x04003C19 RID: 15385
	private List<UIPanel> mBaseUIPanelList = new List<UIPanel>();

	// Token: 0x02000A60 RID: 2656
	public enum SHOW_TYPE
	{
		// Token: 0x04003C3C RID: 15420
		DEFAULT,
		// Token: 0x04003C3D RID: 15421
		RANK_PVP,
		// Token: 0x04003C3E RID: 15422
		BIGSALE,
		// Token: 0x04003C3F RID: 15423
		SHOP,
		// Token: 0x04003C40 RID: 15424
		SLOT,
		// Token: 0x04003C41 RID: 15425
		WELFARE,
		// Token: 0x04003C42 RID: 15426
		MYSTERY,
		// Token: 0x04003C43 RID: 15427
		SKILL,
		// Token: 0x04003C44 RID: 15428
		ENHANCE_EQUIP,
		// Token: 0x04003C45 RID: 15429
		ENHANCE_STAR,
		// Token: 0x04003C46 RID: 15430
		INHERT
	}

	// Token: 0x02000B04 RID: 2820
	// (Invoke) Token: 0x06005099 RID: 20633
	public delegate void OnOpenUIDelegate(bool bSuccess, object param);

	// Token: 0x02000B05 RID: 2821
	// (Invoke) Token: 0x0600509D RID: 20637
	public delegate void OnLoadUIDelegate(GameObject resObject, object param);
}
