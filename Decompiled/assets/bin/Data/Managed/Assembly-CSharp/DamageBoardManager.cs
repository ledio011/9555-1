using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009DD RID: 2525
public class DamageBoardManager : MonoBehaviour
{
	// Token: 0x060047C4 RID: 18372 RVA: 0x0016F0A4 File Offset: 0x0016D2A4
	private void Awake()
	{
		DamageBoardManager.ClearDamgeDict();
	}

	// Token: 0x060047C5 RID: 18373 RVA: 0x0016F0AC File Offset: 0x0016D2AC
	public static void ClearDamgeDict()
	{
		DamageBoardManager.mEnableDict.Clear();
		DamageBoardManager.mDisableDict.Clear();
	}

	// Token: 0x060047C6 RID: 18374 RVA: 0x0016F0C4 File Offset: 0x0016D2C4
	public static void PreLoadDamageBoard()
	{
		DamageBoardManager.ClearDamgeDict();
	}

	// Token: 0x060047C7 RID: 18375 RVA: 0x0016F0CC File Offset: 0x0016D2CC
	private static void AddDict(Dictionary<int, List<DamageBoard>> dict, int type, DamageBoard board)
	{
		List<DamageBoard> list = null;
		if (dict.TryGetValue(type, ref list))
		{
			list.Add(board);
		}
		else
		{
			list = new List<DamageBoard>();
			list.Add(board);
			dict.Add(type, list);
		}
	}

	// Token: 0x060047C8 RID: 18376 RVA: 0x0016F10C File Offset: 0x0016D30C
	public void ShowDamgaeBoard(int type, string strValue, Vector3 pos)
	{
		List<DamageBoard> list = null;
		DamageBoard damageBoard = null;
		if (DamageBoardManager.mDisableDict.TryGetValue(type, ref list) && list.Count > 0)
		{
			damageBoard = list[0];
			damageBoard.Reuse();
			list.RemoveAt(0);
			DamageBoardManager.AddDict(DamageBoardManager.mEnableDict, type, damageBoard);
		}
		else if ((DamageBoardManager.mEnableDict.ContainsKey(type) && DamageBoardManager.mEnableDict[type].Count < 16) || !DamageBoardManager.mEnableDict.ContainsKey(type))
		{
			DamageBoardManager.DamageBoardLoadInfo param = new DamageBoardManager.DamageBoardLoadInfo(type, strValue, pos);
			if (type == 1)
			{
				SingletonUnity<UIManager>.Instance.LoadUIItem(UIInfo.DamgaeBoardUI, new UIManager.OnLoadUIDelegate(DamageBoardManager.LoadDamgeBoard), param);
			}
			else if (type == 3)
			{
				SingletonUnity<UIManager>.Instance.LoadUIItem(UIInfo.DamgaeBoardUI2, new UIManager.OnLoadUIDelegate(DamageBoardManager.LoadDamgeBoard), param);
			}
			else if (type == 4)
			{
				SingletonUnity<UIManager>.Instance.LoadUIItem(UIInfo.DamgaeBoardUI3, new UIManager.OnLoadUIDelegate(DamageBoardManager.LoadDamgeBoard), param);
			}
			else if (type == 5)
			{
				SingletonUnity<UIManager>.Instance.LoadUIItem(UIInfo.DamgaeBoardUI4, new UIManager.OnLoadUIDelegate(DamageBoardManager.LoadDamgeBoard), param);
			}
			else if (type == 7)
			{
				SingletonUnity<UIManager>.Instance.LoadUIItem(UIInfo.DamgaeBoardUI5, new UIManager.OnLoadUIDelegate(DamageBoardManager.LoadDamgeBoard), param);
			}
			else if (type == 8)
			{
				SingletonUnity<UIManager>.Instance.LoadUIItem(UIInfo.DamgaeBoardUI6, new UIManager.OnLoadUIDelegate(DamageBoardManager.LoadDamgeBoard), param);
			}
			else if (type == 10)
			{
				SingletonUnity<UIManager>.Instance.LoadUIItem(UIInfo.DamgaeBoardUI7, new UIManager.OnLoadUIDelegate(DamageBoardManager.LoadDamgeBoard), param);
			}
			else
			{
				SingletonUnity<UIManager>.Instance.LoadUIItem(UIInfo.DamgaeBoardUI, new UIManager.OnLoadUIDelegate(DamageBoardManager.LoadDamgeBoard), param);
			}
		}
		else
		{
			list = null;
			if (DamageBoardManager.mEnableDict.TryGetValue(type, ref list))
			{
				damageBoard = list[0];
				damageBoard.Reuse();
				list.RemoveAt(0);
				DamageBoardManager.AddDict(DamageBoardManager.mEnableDict, type, damageBoard);
			}
		}
		if (damageBoard != null)
		{
			damageBoard.ShowDamgeBoard(type, strValue, pos);
		}
	}

	// Token: 0x060047C9 RID: 18377 RVA: 0x0016F328 File Offset: 0x0016D528
	private static void LoadDamgeBoard(GameObject obj, object param)
	{
		GameObject gameObject = Object.Instantiate(obj) as GameObject;
		gameObject.transform.parent = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.DamageBoadRoot.transform;
		DamageBoard component = gameObject.GetComponent<DamageBoard>();
		if (component != null)
		{
			DamageBoardManager.DamageBoardLoadInfo damageBoardLoadInfo = param as DamageBoardManager.DamageBoardLoadInfo;
			DamageBoardManager.AddDict(DamageBoardManager.mEnableDict, damageBoardLoadInfo.mType, component);
			component.ShowDamgeBoard(damageBoardLoadInfo.mType, damageBoardLoadInfo.valueStr, damageBoardLoadInfo.mPos);
		}
	}

	// Token: 0x060047CA RID: 18378 RVA: 0x0016F3A4 File Offset: 0x0016D5A4
	private void UpdateDamageBoard()
	{
		float time = Time.time;
		for (int i = 1; i < 10; i++)
		{
			List<DamageBoard> list = null;
			if (DamageBoardManager.mEnableDict.TryGetValue(i, ref list))
			{
				for (int j = list.Count - 1; j > -1; j--)
				{
					DamageBoard damageBoard = list[j];
					if (time - damageBoard.ShowTime > 0.5f)
					{
						damageBoard.Reuse();
						DamageBoardManager.AddDict(DamageBoardManager.mDisableDict, i, damageBoard);
						list.RemoveAt(j);
					}
				}
			}
		}
	}

	// Token: 0x060047CB RID: 18379 RVA: 0x0016F430 File Offset: 0x0016D630
	private void FixedUpdate()
	{
		this.UpdateDamageBoard();
	}

	// Token: 0x0400351D RID: 13597
	private const int mMaxDamgeBoardType = 10;

	// Token: 0x0400351E RID: 13598
	private const int mMaxDamgeBoard = 16;

	// Token: 0x0400351F RID: 13599
	private static Dictionary<int, List<DamageBoard>> mEnableDict = new Dictionary<int, List<DamageBoard>>();

	// Token: 0x04003520 RID: 13600
	private static Dictionary<int, List<DamageBoard>> mDisableDict = new Dictionary<int, List<DamageBoard>>();

	// Token: 0x020009DE RID: 2526
	private class DamageBoardLoadInfo
	{
		// Token: 0x060047CC RID: 18380 RVA: 0x0016F438 File Offset: 0x0016D638
		public DamageBoardLoadInfo(int type, string str, Vector3 pos)
		{
			this.mType = type;
			this.valueStr = str;
			this.mPos = pos;
		}

		// Token: 0x04003521 RID: 13601
		public int mType;

		// Token: 0x04003522 RID: 13602
		public string valueStr;

		// Token: 0x04003523 RID: 13603
		public Vector3 mPos;
	}
}
