using System;
using UnityEngine;

// Token: 0x02000A79 RID: 2681
public class UIButtonExtent : MonoBehaviour
{
	// Token: 0x06004E04 RID: 19972 RVA: 0x001AA768 File Offset: 0x001A8968
	private void Init()
	{
		if (this.widget == null)
		{
			this.widget = base.GetComponent<UIWidget>();
		}
		if (this.widget != null)
		{
			if (this.boxCollider == null)
			{
				this.boxCollider = base.GetComponent<BoxCollider>();
			}
			if (this.boxCollider != null)
			{
				this.boxCollider.size = new Vector3((float)this.widget.width * this.scale, (float)this.widget.height * this.scale, 0f);
			}
		}
	}

	// Token: 0x06004E05 RID: 19973 RVA: 0x001AA80C File Offset: 0x001A8A0C
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06004E06 RID: 19974 RVA: 0x001AA814 File Offset: 0x001A8A14
	private void Update()
	{
		if (!this.run)
		{
			this.Init();
			base.enabled = false;
			this.run = true;
		}
	}

	// Token: 0x04003C92 RID: 15506
	public float scale = 1f;

	// Token: 0x04003C93 RID: 15507
	private UIWidget widget;

	// Token: 0x04003C94 RID: 15508
	private BoxCollider boxCollider;

	// Token: 0x04003C95 RID: 15509
	private bool run;
}
