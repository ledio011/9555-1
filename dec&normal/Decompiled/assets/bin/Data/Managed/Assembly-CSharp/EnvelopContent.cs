using System;
using UnityEngine;

// Token: 0x0200002B RID: 43
[AddComponentMenu("NGUI/Examples/Envelop Content")]
[RequireComponent(typeof(UIWidget))]
public class EnvelopContent : MonoBehaviour
{
	// Token: 0x060000B6 RID: 182 RVA: 0x0000597C File Offset: 0x00003B7C
	private void Start()
	{
		this.Execute();
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00005984 File Offset: 0x00003B84
	[ContextMenu("Execute")]
	public void Execute()
	{
		if (this.targetRoot == base.transform)
		{
			Debug.LogError("Target Root object cannot be the same object that has Envelop Content. Make it a sibling instead.", this);
		}
		else if (NGUITools.IsChild(this.targetRoot, base.transform))
		{
			Debug.LogError("Target Root object should not be a parent of Envelop Content. Make it a sibling instead.", this);
		}
		else
		{
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(base.transform.parent, this.targetRoot, false);
			float num = bounds.min.x + (float)this.padLeft;
			float num2 = bounds.min.y + (float)this.padBottom;
			float num3 = bounds.max.x + (float)this.padRight;
			float num4 = bounds.max.y + (float)this.padTop;
			UIWidget component = base.GetComponent<UIWidget>();
			component.SetRect(num, num2, num3 - num, num4 - num2);
		}
	}

	// Token: 0x040000C9 RID: 201
	public Transform targetRoot;

	// Token: 0x040000CA RID: 202
	public int padLeft;

	// Token: 0x040000CB RID: 203
	public int padRight;

	// Token: 0x040000CC RID: 204
	public int padBottom;

	// Token: 0x040000CD RID: 205
	public int padTop;
}
