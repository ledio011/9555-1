using System;
using UnityEngine;

// Token: 0x020000FF RID: 255
public class ResourcesManager
{
	// Token: 0x06000843 RID: 2115 RVA: 0x0003B248 File Offset: 0x00039448
	public static Object LoadAndInstantiate(string path)
	{
		Object @object = ResourcesManager.Load(path);
		if (@object != null)
		{
			return Object.Instantiate(@object);
		}
		return null;
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x0003B274 File Offset: 0x00039474
	public static Object Load(string path)
	{
		return Resources.Load(path);
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x0003B28C File Offset: 0x0003948C
	public static Object LoadAnimation(string path)
	{
		return Resources.Load(path);
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x0003B2A4 File Offset: 0x000394A4
	public static void LoadAutoComboPrefab(UIPathData uiData, string prefabName, ResourcesManager.LoadAutoComboInfoDelegate del)
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager == null || sceneManager.NameBoadPool == null)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.LoadUIItem(uiData, new UIManager.OnLoadUIDelegate(ResourcesManager.LoadAutoInfo), del);
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x0003B2E8 File Offset: 0x000394E8
	public static void UnLoadAutoComboPrefab(GameObject autoInfoObj)
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager == null || sceneManager.NameBoadPool == null)
		{
			return;
		}
		Object.Destroy(autoInfoObj);
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x0003B318 File Offset: 0x00039518
	private static void LoadAutoInfo(GameObject Obj, object parm)
	{
		GameObject gameObject = null;
		if (Obj != null)
		{
			gameObject = (Object.Instantiate(Obj) as GameObject);
			gameObject.transform.parent = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.NameBoardRoot.transform;
		}
		ResourcesManager.LoadAutoComboInfoDelegate loadAutoComboInfoDelegate = parm as ResourcesManager.LoadAutoComboInfoDelegate;
		if (loadAutoComboInfoDelegate != null)
		{
			loadAutoComboInfoDelegate(gameObject);
		}
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x0003B374 File Offset: 0x00039574
	public static void LoadHeadInfoPrefab(UIPathData uiData, string prefabName, ResourcesManager.LoadHeadInfoDelegate del)
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager == null || sceneManager.NameBoadPool == null)
		{
			return;
		}
		sceneManager.NameBoadPool.LoadUIItem(uiData, prefabName, new GameObjectPool.LoadPoolObjDelegate(ResourcesManager.LoadHeadInfo), del);
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x0003B3B8 File Offset: 0x000395B8
	public static void UnLoadHeadInfoPrefab(GameObject headInfoObj)
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager == null || sceneManager.NameBoadPool == null)
		{
			return;
		}
		BillBoard component = headInfoObj.GetComponent<BillBoard>();
		if (component != null)
		{
			component.enabled = false;
			component.BindObj = null;
		}
		sceneManager.NameBoadPool.Remove(headInfoObj);
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x0003B410 File Offset: 0x00039610
	private static void LoadHeadInfo(GameObject newObj, object parm)
	{
		if (newObj != null)
		{
			newObj.transform.parent = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.NameBoardRoot.transform;
		}
		ResourcesManager.LoadHeadInfoDelegate loadHeadInfoDelegate = parm as ResourcesManager.LoadHeadInfoDelegate;
		if (loadHeadInfoDelegate != null)
		{
			loadHeadInfoDelegate(newObj);
		}
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0003B45C File Offset: 0x0003965C
	public static void LoadSimpleShadowPrefab(UIPathData uiData, string prefabName, ResourcesManager.LoadSimpleShadowDelegate del)
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager == null || sceneManager.SimpleShadowPool == null)
		{
			return;
		}
		sceneManager.SimpleShadowPool.LoadUIItem(uiData, prefabName, new GameObjectPool.LoadPoolObjDelegate(ResourcesManager.LoadSimpleShadow), del);
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x0003B4A0 File Offset: 0x000396A0
	public static void UnLoadSimpleShadowPrefab(GameObject simpleShadowObj)
	{
		if (simpleShadowObj == null)
		{
			return;
		}
		UnityVersionUtil.SetActiveRecursive(simpleShadowObj.gameObject, false);
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager == null || sceneManager.SimpleShadowPool == null)
		{
			return;
		}
		SimpleShadowFollow component = simpleShadowObj.GetComponent<SimpleShadowFollow>();
		component.BindObj = null;
		simpleShadowObj.transform.position = Vector3.up * -20f;
		sceneManager.SimpleShadowPool.Remove(simpleShadowObj);
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x0003B518 File Offset: 0x00039718
	private static void LoadSimpleShadow(GameObject newObj, object parm)
	{
		if (newObj != null)
		{
			newObj.transform.parent = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.NameBoardRoot.transform;
		}
		ResourcesManager.LoadSimpleShadowDelegate loadSimpleShadowDelegate = parm as ResourcesManager.LoadSimpleShadowDelegate;
		if (loadSimpleShadowDelegate != null)
		{
			loadSimpleShadowDelegate(newObj);
		}
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x0003B564 File Offset: 0x00039764
	public static void LoadDropItemPrefab(UIPathData uiData, string prefabName, ResourcesManager.LoadDropItemDelegate del, ObjInitDropItemData initData)
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager == null || sceneManager.DropItemPool == null)
		{
			return;
		}
		object[] parm = new object[]
		{
			del,
			initData
		};
		sceneManager.DropItemPool.LoadUIItem(uiData, prefabName, new GameObjectPool.LoadPoolObjDelegate(ResourcesManager.LoadDropItem), parm);
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0003B5B8 File Offset: 0x000397B8
	public static void UnLoadDropItemPrefab(GameObject dropItemObj)
	{
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager == null || sceneManager.DropItemPool == null)
		{
			return;
		}
		sceneManager.DropItemPool.Remove(dropItemObj);
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x0003B5F0 File Offset: 0x000397F0
	private static void LoadDropItem(GameObject newObj, object parm)
	{
		if (newObj != null)
		{
			newObj.transform.parent = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.DropItemRoot.transform;
			newObj.transform.localPosition = Vector3.zero;
			newObj.transform.localEulerAngles = Vector3.zero;
		}
		object[] array = parm as object[];
		ResourcesManager.LoadDropItemDelegate loadDropItemDelegate = array[0] as ResourcesManager.LoadDropItemDelegate;
		if (loadDropItemDelegate != null)
		{
			loadDropItemDelegate(newObj, array[1] as ObjInitDropItemData);
		}
	}

	// Token: 0x02000ABB RID: 2747
	// (Invoke) Token: 0x06004F75 RID: 20341
	public delegate void LoadAutoComboInfoDelegate(GameObject autoInfoObj);

	// Token: 0x02000ABC RID: 2748
	// (Invoke) Token: 0x06004F79 RID: 20345
	public delegate void LoadHeadInfoDelegate(GameObject headInfoObj);

	// Token: 0x02000ABD RID: 2749
	// (Invoke) Token: 0x06004F7D RID: 20349
	public delegate void LoadSimpleShadowDelegate(GameObject simpleShadowObj);

	// Token: 0x02000ABE RID: 2750
	// (Invoke) Token: 0x06004F81 RID: 20353
	public delegate void LoadDropItemDelegate(GameObject dropItemObj, ObjInitDropItemData initData);
}
