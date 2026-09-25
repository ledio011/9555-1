using System;
using SprotoType;
using UnityEngine;

// Token: 0x020000EB RID: 235
public class TeleportPoint : MonoBehaviour
{
	// Token: 0x0600075C RID: 1884 RVA: 0x00033180 File Offset: 0x00031380
	private void Start()
	{
		this.mTeleportTransform = base.transform;
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00033190 File Offset: 0x00031390
	private void GoNextScene()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange)
		{
			return;
		}
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CopySceneChange = true;
		enter_new_map.request request = new enter_new_map.request();
		enter_new_map.request request2 = request;
		int num = (int)this.nextSceneId;
		request2.mapInfoId = num.ToString();
		NetLogic.GetInstance().Send<Protocol.enter_new_map>(request, null);
		this.mfLastInvaildTime = Time.time;
		this.mbValid = false;
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x000331FC File Offset: 0x000313FC
	private void FixedUpdate()
	{
		if (!this.mbValid)
		{
			if (Time.time - this.mfLastInvaildTime < 3f)
			{
				return;
			}
			this.mbValid = true;
		}
		if (null == this.mMainPlayerTransform)
		{
			if (null != Singleton<ObjManager>.Instance.MainPlayer)
			{
				this.mMainPlayerTransform = Singleton<ObjManager>.Instance.MainPlayer.transform;
			}
			if (null == this.mMainPlayerTransform)
			{
				return;
			}
		}
		if (null != Singleton<ObjManager>.Instance.MainPlayer && Vector3.Distance(this.mMainPlayerTransform.position, this.mTeleportTransform.position) <= (float)this.ActiveRadius)
		{
			this.GoNextScene();
		}
	}

	// Token: 0x04000643 RID: 1603
	public int TeleportID = -1;

	// Token: 0x04000644 RID: 1604
	public int ActiveRadius = 3;

	// Token: 0x04000645 RID: 1605
	private bool mbValid = true;

	// Token: 0x04000646 RID: 1606
	private float mfLastInvaildTime;

	// Token: 0x04000647 RID: 1607
	private Transform mMainPlayerTransform;

	// Token: 0x04000648 RID: 1608
	private Transform mTeleportTransform;

	// Token: 0x04000649 RID: 1609
	public GameDefine.SCENE_DEFINE nextSceneId = GameDefine.SCENE_DEFINE.SCENE_MAIN_CITY;
}
