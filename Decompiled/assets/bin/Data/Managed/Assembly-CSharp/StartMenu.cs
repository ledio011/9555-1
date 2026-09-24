using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class StartMenu : MonoBehaviour
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000064 RID: 100 RVA: 0x00003D14 File Offset: 0x00001F14
	// (set) Token: 0x06000065 RID: 101 RVA: 0x00003D1C File Offset: 0x00001F1C
	public Transform CurrentMenuRoot
	{
		get
		{
			return this.currentMenuRoot;
		}
		set
		{
			this.currentMenuRoot = value;
		}
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00003D28 File Offset: 0x00001F28
	private void Start()
	{
		this.CurrentMenuRoot = this.itemsTree;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00003D38 File Offset: 0x00001F38
	private void OnGUI()
	{
		SampleUI.ApplyVirtualScreen();
		GUILayout.BeginArea(this.screenRect);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Space(this.sideBorder);
		if (this.CurrentMenuRoot)
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			GUILayout.Space(15f);
			GUILayout.Label(this.CurrentMenuRoot.name, this.titleStyle, new GUILayoutOption[0]);
			for (int i = 0; i < this.CurrentMenuRoot.childCount; i++)
			{
				Transform child = this.CurrentMenuRoot.GetChild(i);
				if (GUILayout.Button(child.name, new GUILayoutOption[]
				{
					GUILayout.Height(this.buttonHeight)
				}))
				{
					MenuNode component = child.GetComponent<MenuNode>();
					if (component && component.sceneName != null && component.sceneName.Length > 0)
					{
						Application.LoadLevel(component.sceneName);
					}
					else if (child.childCount > 0)
					{
						this.CurrentMenuRoot = child;
					}
				}
				GUILayout.Space(5f);
			}
			GUILayout.FlexibleSpace();
			if (this.CurrentMenuRoot != this.itemsTree && this.CurrentMenuRoot.parent)
			{
				if (GUILayout.Button("<< BACK <<", new GUILayoutOption[]
				{
					GUILayout.Height(this.buttonHeight)
				}))
				{
					this.CurrentMenuRoot = this.CurrentMenuRoot.parent;
				}
				GUILayout.Space(15f);
			}
			GUILayout.EndVertical();
		}
		GUILayout.Space(this.sideBorder);
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	// Token: 0x0400005A RID: 90
	public GUIStyle titleStyle;

	// Token: 0x0400005B RID: 91
	public GUIStyle buttonStyle;

	// Token: 0x0400005C RID: 92
	public float buttonHeight = 80f;

	// Token: 0x0400005D RID: 93
	public Transform itemsTree;

	// Token: 0x0400005E RID: 94
	private Transform currentMenuRoot;

	// Token: 0x0400005F RID: 95
	private Rect screenRect = new Rect(0f, 0f, SampleUI.VirtualScreenWidth, SampleUI.VirtualScreenHeight);

	// Token: 0x04000060 RID: 96
	public float menuWidth = 450f;

	// Token: 0x04000061 RID: 97
	public float sideBorder = 30f;
}
