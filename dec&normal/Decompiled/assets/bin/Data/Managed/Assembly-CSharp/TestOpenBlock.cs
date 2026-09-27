using System;
using UnityEngine;

// Token: 0x020008A7 RID: 2215
public class TestOpenBlock : MonoBehaviour
{
	// Token: 0x06003BBB RID: 15291 RVA: 0x001048F0 File Offset: 0x00102AF0
	public void OpenBlock(object ins)
	{
		int num = (int)ins;
		if (this.blockList[num] != null)
		{
			for (int i = 0; i < this.blockList[num].transform.childCount; i++)
			{
				this.blockList[num].transform.GetChild(i).GetChild(0).animation.Play();
			}
		}
		if (this.ParticleList[num] != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ParticleList[num].gameObject, true);
		}
	}

	// Token: 0x06003BBC RID: 15292 RVA: 0x00104984 File Offset: 0x00102B84
	private void Start()
	{
		for (int i = 0; i < this.ParticleList.Length; i++)
		{
			if (this.ParticleList[i] != null)
			{
				UnityVersionUtil.SetActiveRecursive(this.ParticleList[i], false);
			}
		}
		SingletonUnity<MyEvent>.Instance.Register("OpenBlock", this, "OpenBlock");
	}

	// Token: 0x06003BBD RID: 15293 RVA: 0x001049E4 File Offset: 0x00102BE4
	private void Update()
	{
	}

	// Token: 0x04002718 RID: 10008
	public GameObject[] blockList;

	// Token: 0x04002719 RID: 10009
	public GameObject[] ParticleList;
}
