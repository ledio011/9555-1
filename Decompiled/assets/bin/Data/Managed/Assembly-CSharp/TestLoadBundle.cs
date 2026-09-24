using System;
using System.Collections;
using UnityEngine;

// Token: 0x020008A1 RID: 2209
public class TestLoadBundle : MonoBehaviour
{
	// Token: 0x06003BA5 RID: 15269 RVA: 0x00104798 File Offset: 0x00102998
	private void Start()
	{
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(this.TestLoad());
		}
	}

	// Token: 0x06003BA6 RID: 15270 RVA: 0x001047B8 File Offset: 0x001029B8
	private void Update()
	{
	}

	// Token: 0x06003BA7 RID: 15271 RVA: 0x001047BC File Offset: 0x001029BC
	public IEnumerator TestLoad()
	{
		WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/Bundle/Model/BOSS_Nan_001.bundle");
		yield return www;
		(Object.Instantiate(www.assetBundle.mainAsset) as GameObject).transform.position = Vector3.zero;
		www.assetBundle.Unload(true);
		www.assetBundle.ToString();
		yield return new WaitForSeconds(2f);
		www = new WWW("file:///" + Application.streamingAssetsPath + "/Bundle/Model/BOSS_Nan_001.bundle");
		yield return www;
		(Object.Instantiate(www.assetBundle.mainAsset) as GameObject).transform.position = Vector3.zero;
		www.assetBundle.Unload(false);
		yield break;
	}
}
