using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200089D RID: 2205
public class TestBundleUnload : MonoBehaviour
{
	// Token: 0x06003B95 RID: 15253 RVA: 0x001045C4 File Offset: 0x001027C4
	private IEnumerator Start()
	{
		string url = BundleManager.GetLocalUrl(BundleManager.BundleModelRootPath, "NPC_Nan_001.bundle");
		WWW www = new WWW(url);
		yield return www;
		this.bundle = www.assetBundle;
		yield break;
	}

	// Token: 0x06003B96 RID: 15254 RVA: 0x001045E0 File Offset: 0x001027E0
	private void Update()
	{
	}

	// Token: 0x06003B97 RID: 15255 RVA: 0x001045E4 File Offset: 0x001027E4
	private void OnDisable()
	{
		this.bundle.Unload(false);
	}

	// Token: 0x04002710 RID: 10000
	private AssetBundle bundle;
}
