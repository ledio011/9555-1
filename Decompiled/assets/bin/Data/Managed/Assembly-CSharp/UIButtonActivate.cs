using System;
using UnityEngine;

// Token: 0x02000040 RID: 64
[AddComponentMenu("NGUI/Interaction/Button Activate")]
public class UIButtonActivate : MonoBehaviour
{
	// Token: 0x060000F9 RID: 249 RVA: 0x00006F3C File Offset: 0x0000513C
	private void OnClick()
	{
		if (this.target != null)
		{
			NGUITools.SetActive(this.target, this.state);
		}
	}

	// Token: 0x04000111 RID: 273
	public GameObject target;

	// Token: 0x04000112 RID: 274
	public bool state = true;
}
