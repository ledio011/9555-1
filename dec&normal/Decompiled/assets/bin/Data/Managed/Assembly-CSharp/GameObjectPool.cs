using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A6C RID: 2668
public class GameObjectPool
{
	// Token: 0x06004DC3 RID: 19907 RVA: 0x001A9798 File Offset: 0x001A7998
	public GameObjectPool(string poolName, int maxSize = 64)
	{
		this.mPoolName = poolName;
		this.mMaxSize = maxSize;
		this.mEnablePool = new Dictionary<string, List<GameObject>>();
		this.mDisablePool = new Dictionary<string, List<GameObject>>();
	}

	// Token: 0x06004DC4 RID: 19908 RVA: 0x001A97D8 File Offset: 0x001A79D8
	public void LoadUIItem(UIPathData uiData, string name, GameObjectPool.LoadPoolObjDelegate del, object parm1)
	{
		GameObject gameObject = this.ReUseItem(name);
		if (gameObject == null)
		{
			if (SingletonUnity<UIManager>.Instance == null)
			{
				Debug.Log("SingletonUnity<UIManager>.Instance == null");
			}
			else
			{
				SingletonUnity<UIManager>.Instance.LoadUIItem(uiData, new UIManager.OnLoadUIDelegate(this.LoadItem), new GameObjectPool.LoadParm(name, uiData, del, parm1));
			}
		}
		else
		{
			NGUITools.SetActive(gameObject, true);
			if (del != null)
			{
				del(gameObject, parm1);
			}
		}
	}

	// Token: 0x06004DC5 RID: 19909 RVA: 0x001A9854 File Offset: 0x001A7A54
	private void LoadItem(GameObject resObj, object param)
	{
		GameObjectPool.LoadParm loadParm = param as GameObjectPool.LoadParm;
		if (param == null)
		{
			return;
		}
		GameObject gameObject = Object.Instantiate(resObj) as GameObject;
		if (gameObject != null)
		{
			gameObject.name = loadParm.mObjName;
			if (!this.InsertItem(this.mDisablePool, gameObject))
			{
				Object.Destroy(gameObject);
			}
			if (loadParm.mDelFun != null)
			{
				loadParm.mDelFun(gameObject, loadParm.mParm1);
			}
		}
	}

	// Token: 0x17000FD2 RID: 4050
	// (get) Token: 0x06004DC6 RID: 19910 RVA: 0x001A98C8 File Offset: 0x001A7AC8
	public string PoolName
	{
		get
		{
			return this.mPoolName;
		}
	}

	// Token: 0x06004DC7 RID: 19911 RVA: 0x001A98D0 File Offset: 0x001A7AD0
	public void Remove(GameObject item)
	{
		if (item == null)
		{
			return;
		}
		UnityVersionUtil.SetActiveRecursive(item, false);
		if (this.RemoveItem(this.mDisablePool, item) && !this.InsertItem(this.mEnablePool, item))
		{
			Object.Destroy(item);
		}
	}

	// Token: 0x06004DC8 RID: 19912 RVA: 0x001A991C File Offset: 0x001A7B1C
	public GameObject ReUseItem(string name)
	{
		if (this.mEnablePool.ContainsKey(name))
		{
			List<GameObject> list = this.mEnablePool[name];
			if (list == null || list.Count <= 0)
			{
				return null;
			}
			GameObject gameObject = list[0];
			if (gameObject == null)
			{
				list.RemoveAt(0);
				return null;
			}
			list.Remove(gameObject);
			if (this.InsertItem(this.mDisablePool, gameObject))
			{
				return gameObject;
			}
			Object.Destroy(gameObject);
		}
		return null;
	}

	// Token: 0x06004DC9 RID: 19913 RVA: 0x001A99A0 File Offset: 0x001A7BA0
	private bool RemoveItem(Dictionary<string, List<GameObject>> pool, GameObject item)
	{
		if (item == null || pool == null)
		{
			return false;
		}
		if (pool.ContainsKey(item.name))
		{
			List<GameObject> list = pool[item.name];
			if (list != null && list.Contains(item))
			{
				list.Remove(item);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004DCA RID: 19914 RVA: 0x001A99FC File Offset: 0x001A7BFC
	private bool InsertItem(Dictionary<string, List<GameObject>> pool, GameObject item)
	{
		if (item == null || pool == null)
		{
			return false;
		}
		List<GameObject> list = null;
		if (!pool.TryGetValue(item.name, ref list))
		{
			list = new List<GameObject>();
			pool.Add(item.name, list);
		}
		if (list == null)
		{
			return false;
		}
		if (list.Count >= this.mMaxSize)
		{
			Debug.LogWarning("Max Pool");
			return false;
		}
		list.Add(item);
		return true;
	}

	// Token: 0x06004DCB RID: 19915 RVA: 0x001A9A74 File Offset: 0x001A7C74
	public void ClearAllPool()
	{
		if (this.mDisablePool != null)
		{
			foreach (KeyValuePair<string, List<GameObject>> keyValuePair in this.mDisablePool)
			{
				List<GameObject> value = keyValuePair.Value;
				if (value != null)
				{
					for (int i = 0; i < value.Count; i++)
					{
						value[i] = null;
					}
				}
				value.Clear();
			}
		}
		if (this.mEnablePool != null)
		{
			foreach (KeyValuePair<string, List<GameObject>> keyValuePair2 in this.mEnablePool)
			{
				List<GameObject> value2 = keyValuePair2.Value;
				if (value2 != null)
				{
					for (int j = 0; j < value2.Count; j++)
					{
						value2[j] = null;
					}
				}
				value2.Clear();
			}
		}
	}

	// Token: 0x04003C71 RID: 15473
	private string mPoolName;

	// Token: 0x04003C72 RID: 15474
	private Dictionary<string, List<GameObject>> mEnablePool;

	// Token: 0x04003C73 RID: 15475
	private Dictionary<string, List<GameObject>> mDisablePool;

	// Token: 0x04003C74 RID: 15476
	private int mMaxSize = 64;

	// Token: 0x02000A6D RID: 2669
	private class LoadParm
	{
		// Token: 0x06004DCC RID: 19916 RVA: 0x001A9BAC File Offset: 0x001A7DAC
		public LoadParm(string name, UIPathData data, GameObjectPool.LoadPoolObjDelegate del, object parm1)
		{
			this.mObjName = name;
			this.mUIData = data;
			this.mDelFun = del;
			this.mParm1 = parm1;
		}

		// Token: 0x04003C75 RID: 15477
		public GameObjectPool.LoadPoolObjDelegate mDelFun;

		// Token: 0x04003C76 RID: 15478
		public object mParm1;

		// Token: 0x04003C77 RID: 15479
		public UIPathData mUIData;

		// Token: 0x04003C78 RID: 15480
		public string mObjName;
	}

	// Token: 0x02000B09 RID: 2825
	// (Invoke) Token: 0x060050AD RID: 20653
	public delegate void LoadPoolObjDelegate(GameObject newObj, object parm1);
}
