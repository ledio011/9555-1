using System;
using UnityEngine;

// Token: 0x02000102 RID: 258
public class TutorialPlayerEnterCheck : MonoBehaviour
{
	// Token: 0x0600094B RID: 2379 RVA: 0x00044838 File Offset: 0x00042A38
	private void Start()
	{
		this.tutorialSceneManager = (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager as NewTutorialSceneManager);
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x00044850 File Offset: 0x00042A50
	private void OnCollisionEnter(Collision other)
	{
		if (this.tutorialSceneManager != null && this.tutorialSceneManager.IsNeedCheckMainPlayerActiveRange())
		{
			ObjMainPlayer component = other.gameObject.GetComponent<ObjMainPlayer>();
			if (component != null && Time.time - this.mLastEnterTime > this.ShowInterVal)
			{
				StoryDialogRootLogic.ShowStory("108", null);
				component.StopMove();
				this.mLastEnterTime = Time.time;
			}
		}
	}

	// Token: 0x04000852 RID: 2130
	private float mLastEnterTime;

	// Token: 0x04000853 RID: 2131
	private float ShowInterVal = 10f;

	// Token: 0x04000854 RID: 2132
	private NewTutorialSceneManager tutorialSceneManager;
}
