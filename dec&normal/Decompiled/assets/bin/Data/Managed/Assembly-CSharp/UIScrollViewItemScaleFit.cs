using System;
using UnityEngine;

// Token: 0x020008C8 RID: 2248
public class UIScrollViewItemScaleFit : MonoBehaviour
{
	// Token: 0x06003C92 RID: 15506 RVA: 0x00109FE8 File Offset: 0x001081E8
	private void Start()
	{
		if (this.mTransform == null)
		{
			this.mTransform = base.transform;
		}
	}

	// Token: 0x06003C93 RID: 15507 RVA: 0x0010A008 File Offset: 0x00108208
	private void Update()
	{
		this.mDisOfCenterObj = Mathf.Abs(this.CenterObj.InverseTransformPoint(this.mTransform.position).x);
		this.mTransform.localScale = Vector3.one * (1f - this.mDisOfCenterObj / 200f * 0.5f);
	}

	// Token: 0x040027D1 RID: 10193
	public Transform CenterObj;

	// Token: 0x040027D2 RID: 10194
	private float mDisOfCenterObj;

	// Token: 0x040027D3 RID: 10195
	private Transform mTransform;
}
