using System;
using UnityEngine;

// Token: 0x020009E9 RID: 2537
[ExecuteInEditMode]
[RequireComponent(typeof(UILabel))]
public class LabelState : MonoBehaviour
{
	// Token: 0x17000FBB RID: 4027
	// (get) Token: 0x0600483A RID: 18490 RVA: 0x001725D8 File Offset: 0x001707D8
	// (set) Token: 0x0600483B RID: 18491 RVA: 0x001725E0 File Offset: 0x001707E0
	public bool active
	{
		get
		{
			return this.mActive;
		}
		set
		{
			if (this.mLabel == null)
			{
				this.mLabel = base.GetComponent<UILabel>();
			}
			if (value != this.mActive)
			{
				if (value)
				{
					this.mLabel.color = this.activeColor;
					if (this.useGradientActive)
					{
						this.mLabel.gradientTop = this.gradientActiveTopColor;
						this.mLabel.gradientBottom = this.gradientActiveBottomColor;
						this.mLabel.applyGradient = true;
					}
					else
					{
						this.mLabel.applyGradient = false;
					}
				}
				else
				{
					this.mLabel.color = this.deactiveColor;
					if (this.useGradientDeactive)
					{
						this.mLabel.gradientTop = this.gradientDeactiveTopColor;
						this.mLabel.gradientBottom = this.gradientDeactiveBottomColor;
						this.mLabel.applyGradient = true;
					}
					else
					{
						this.mLabel.applyGradient = false;
					}
				}
				this.mActive = value;
			}
		}
	}

	// Token: 0x0600483C RID: 18492 RVA: 0x001726E0 File Offset: 0x001708E0
	private void Start()
	{
	}

	// Token: 0x0600483D RID: 18493 RVA: 0x001726E4 File Offset: 0x001708E4
	private void Update()
	{
	}

	// Token: 0x0400358D RID: 13709
	public Color activeColor;

	// Token: 0x0400358E RID: 13710
	public Color deactiveColor;

	// Token: 0x0400358F RID: 13711
	public bool useGradientActive;

	// Token: 0x04003590 RID: 13712
	public Color gradientActiveTopColor;

	// Token: 0x04003591 RID: 13713
	public Color gradientActiveBottomColor;

	// Token: 0x04003592 RID: 13714
	public bool useGradientDeactive;

	// Token: 0x04003593 RID: 13715
	public Color gradientDeactiveTopColor;

	// Token: 0x04003594 RID: 13716
	public Color gradientDeactiveBottomColor;

	// Token: 0x04003595 RID: 13717
	[SerializeField]
	private bool mActive;

	// Token: 0x04003596 RID: 13718
	private UILabel mLabel;
}
