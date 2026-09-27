using System;
using UnityEngine;

// Token: 0x02000A6A RID: 2666
public class FPS : MonoBehaviour
{
	// Token: 0x06004DB5 RID: 19893 RVA: 0x001A8FE8 File Offset: 0x001A71E8
	private void Start()
	{
		if (!base.guiText)
		{
			base.enabled = false;
			return;
		}
		this.timeleft = this.updateInterval;
	}

	// Token: 0x06004DB6 RID: 19894 RVA: 0x001A901C File Offset: 0x001A721C
	private void showNetBack(string str)
	{
		str = str + "Recive=" + NetLogic.nReceiveCount.ToString();
		str = str + "Send=" + NetLogic.nSendCount.ToString();
		base.guiText.text = str;
	}

	// Token: 0x06004DB7 RID: 19895 RVA: 0x001A9064 File Offset: 0x001A7264
	private void Update()
	{
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		if ((double)this.timeleft <= 0.0)
		{
			float num = this.accum / (float)this.frames;
			this.format = string.Format("{0:F2}", num);
			base.guiText.text = this.format;
			if (num < 20f)
			{
				base.guiText.material.color = Color.red;
			}
			else if (num < 30f)
			{
				base.guiText.material.color = Color.yellow;
			}
			else
			{
				base.guiText.material.color = Color.green;
			}
			this.timeleft = this.updateInterval;
			this.accum = 0f;
			this.frames = 0;
		}
		this.showNetBack(this.format);
	}

	// Token: 0x04003C6C RID: 15468
	public float updateInterval = 0.5f;

	// Token: 0x04003C6D RID: 15469
	private float accum;

	// Token: 0x04003C6E RID: 15470
	private int frames;

	// Token: 0x04003C6F RID: 15471
	private float timeleft;

	// Token: 0x04003C70 RID: 15472
	private string format;
}
