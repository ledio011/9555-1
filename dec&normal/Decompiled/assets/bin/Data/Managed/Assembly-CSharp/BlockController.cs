using System;
using UnityEngine;

// Token: 0x02000873 RID: 2163
public class BlockController : MonoBehaviour
{
	// Token: 0x0600395F RID: 14687 RVA: 0x000F52E0 File Offset: 0x000F34E0
	public void Init()
	{
		this.array1 = base.GetComponentsInChildren<Animation>();
		this.array2 = base.GetComponentsInChildren<ParticleSystem>();
		this.Play(false);
	}

	// Token: 0x06003960 RID: 14688 RVA: 0x000F5304 File Offset: 0x000F3504
	private void Play(bool isEnable)
	{
		if (isEnable)
		{
			for (int i = 0; i < this.array1.Length; i++)
			{
				this.array1[i].Play();
			}
			for (int j = 0; j < this.array2.Length; j++)
			{
				this.array2[j].Play();
			}
		}
		else
		{
			for (int k = 0; k < this.array1.Length; k++)
			{
				this.array1[k].Stop();
			}
			for (int l = 0; l < this.array2.Length; l++)
			{
				this.array2[l].Clear();
				this.array2[l].Stop();
			}
		}
	}

	// Token: 0x06003961 RID: 14689 RVA: 0x000F53C4 File Offset: 0x000F35C4
	public void OpenBlock(float delayTime = 0f)
	{
		if (delayTime > 0f)
		{
			base.Invoke("InternalOpenBlock", delayTime);
		}
		else
		{
			this.InternalOpenBlock();
		}
	}

	// Token: 0x06003962 RID: 14690 RVA: 0x000F53F4 File Offset: 0x000F35F4
	private void InternalOpenBlock()
	{
		this.Play(true);
		base.Invoke("DestroyMyself", this.time);
	}

	// Token: 0x06003963 RID: 14691 RVA: 0x000F5410 File Offset: 0x000F3610
	private void DestroyMyself()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04002589 RID: 9609
	private Animation[] array1;

	// Token: 0x0400258A RID: 9610
	private ParticleSystem[] array2;

	// Token: 0x0400258B RID: 9611
	public float time = 2f;

	// Token: 0x0400258C RID: 9612
	private float startTime;
}
