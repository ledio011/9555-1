using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200002A RID: 42
[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
	// Token: 0x060000B3 RID: 179 RVA: 0x00005938 File Offset: 0x00003B38
	private IEnumerator Start()
	{
		WWW www = new WWW(this.url);
		yield return www;
		this.mTex = www.texture;
		if (this.mTex != null)
		{
			UITexture ut = base.GetComponent<UITexture>();
			ut.mainTexture = this.mTex;
			ut.MakePixelPerfect();
		}
		www.Dispose();
		yield break;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00005954 File Offset: 0x00003B54
	private void OnDestroy()
	{
		if (this.mTex != null)
		{
			Object.Destroy(this.mTex);
		}
	}

	// Token: 0x040000C7 RID: 199
	public string url = "http://www.yourwebsite.com/logo.png";

	// Token: 0x040000C8 RID: 200
	private Texture2D mTex;
}
