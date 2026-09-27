using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000876 RID: 2166
public class CrashDoorController : MonoBehaviour
{
	// Token: 0x0600399E RID: 14750 RVA: 0x000F712C File Offset: 0x000F532C
	private void Start()
	{
		if (!this.mInitFlag)
		{
			this.mInitFlag = true;
			for (int i = 0; i < base.transform.childCount; i++)
			{
				GameObject gameObject = base.transform.GetChild(i).gameObject;
				if (gameObject.name.StartsWith("@"))
				{
					this.mSingleMeshObj = gameObject;
				}
				else
				{
					this.mCrashObjList.Add(gameObject);
				}
			}
		}
	}

	// Token: 0x0600399F RID: 14751 RVA: 0x000F71A8 File Offset: 0x000F53A8
	public void OpenBlock(float delayTime = 0f)
	{
		if (delayTime > 0f)
		{
			base.Invoke("Play", delayTime);
		}
		else
		{
			this.Play();
		}
	}

	// Token: 0x060039A0 RID: 14752 RVA: 0x000F71D8 File Offset: 0x000F53D8
	private void Play()
	{
		base.Invoke("DestroyMyself", this.mRecycleTime);
	}

	// Token: 0x060039A1 RID: 14753 RVA: 0x000F71EC File Offset: 0x000F53EC
	private void DestroyMyself()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040025C3 RID: 9667
	private float mRecycleTime = 5f;

	// Token: 0x040025C4 RID: 9668
	private GameObject mSingleMeshObj;

	// Token: 0x040025C5 RID: 9669
	private List<GameObject> mCrashObjList = new List<GameObject>();

	// Token: 0x040025C6 RID: 9670
	private bool mInitFlag;
}
