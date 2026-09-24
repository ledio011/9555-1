using System;
using UnityEngine;

// Token: 0x02000A4C RID: 2636
public class SpriteState : MonoBehaviour
{
	// Token: 0x17000FD0 RID: 4048
	// (get) Token: 0x06004CCE RID: 19662 RVA: 0x001A0FAC File Offset: 0x0019F1AC
	// (set) Token: 0x06004CCF RID: 19663 RVA: 0x001A0FB4 File Offset: 0x0019F1B4
	public bool active
	{
		get
		{
			return this.mActive;
		}
		set
		{
			if (this.mSprite == null)
			{
				this.mSprite = base.GetComponent<UISprite>();
			}
			if (value)
			{
				this.mSprite.color = this.activeColor;
			}
			else
			{
				this.mSprite.color = this.deactiveColor;
			}
			this.mActive = value;
		}
	}

	// Token: 0x04003A67 RID: 14951
	public Color activeColor;

	// Token: 0x04003A68 RID: 14952
	public Color deactiveColor;

	// Token: 0x04003A69 RID: 14953
	[SerializeField]
	private bool mActive;

	// Token: 0x04003A6A RID: 14954
	private UISprite mSprite;
}
