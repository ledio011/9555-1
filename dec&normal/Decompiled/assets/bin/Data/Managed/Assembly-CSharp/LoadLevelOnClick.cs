using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
[AddComponentMenu("NGUI/Examples/Load Level On Click")]
public class LoadLevelOnClick : MonoBehaviour
{
	// Token: 0x060000C2 RID: 194 RVA: 0x00005D88 File Offset: 0x00003F88
	private void OnClick()
	{
		if (!string.IsNullOrEmpty(this.levelName))
		{
			Application.LoadLevel(this.levelName);
		}
	}

	// Token: 0x040000DC RID: 220
	public string levelName;
}
