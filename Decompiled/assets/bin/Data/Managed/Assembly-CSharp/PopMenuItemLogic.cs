using System;
using UnityEngine;

// Token: 0x02000A3C RID: 2620
public class PopMenuItemLogic : MonoBehaviour
{
	// Token: 0x06004C6F RID: 19567 RVA: 0x0019E9B4 File Offset: 0x0019CBB4
	private void Start()
	{
	}

	// Token: 0x06004C70 RID: 19568 RVA: 0x0019E9B8 File Offset: 0x0019CBB8
	private void OnClick()
	{
		PopMenuItemLogic.MenuItemOnClicked menuItemOnClicked = this.deleMenuItemOnClicked;
		if (menuItemOnClicked != null)
		{
			menuItemOnClicked();
			SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(4, 1f, null);
		}
	}

	// Token: 0x06004C71 RID: 19569 RVA: 0x0019E9EC File Offset: 0x0019CBEC
	public void InitMenuItem(string strLabel, PopMenuItemLogic.MenuItemOnClicked funcItemOnClicked)
	{
		this.m_MenuItemLabel.text = strLabel;
		this.deleMenuItemOnClicked = funcItemOnClicked;
	}

	// Token: 0x04003A22 RID: 14882
	public UILabel m_MenuItemLabel;

	// Token: 0x04003A23 RID: 14883
	private PopMenuItemLogic.MenuItemOnClicked deleMenuItemOnClicked;

	// Token: 0x02000B03 RID: 2819
	// (Invoke) Token: 0x06005095 RID: 20629
	public delegate void MenuItemOnClicked();
}
