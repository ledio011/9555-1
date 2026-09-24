using System;
using UnityEngine;

// Token: 0x02000A8B RID: 2699
public class TestComponent : MonoBehaviour
{
	// Token: 0x06004E7F RID: 20095 RVA: 0x001ADC14 File Offset: 0x001ABE14
	private void Update()
	{
		this.m_StatusColor = Color.Lerp(this.m_StatusColor, new Color(1f, 1f, 1f, 0f), Time.deltaTime * 2.75f);
	}

	// Token: 0x06004E80 RID: 20096 RVA: 0x001ADC4C File Offset: 0x001ABE4C
	private void OnGUI()
	{
		GUI.color = this.m_StatusColor;
		GUI.Label(new Rect((float)(Screen.width - 190), (float)(Screen.height - (Screen.height - 95)), 400f, 50f), this.m_StatusString);
	}

	// Token: 0x06004E81 RID: 20097 RVA: 0x001ADC9C File Offset: 0x001ABE9C
	public void Test(string s)
	{
		this.m_StatusColor = Color.yellow;
		this.m_StatusString = s;
	}

	// Token: 0x04003D00 RID: 15616
	private Color m_StatusColor = Color.white;

	// Token: 0x04003D01 RID: 15617
	private string m_StatusString = string.Empty;
}
