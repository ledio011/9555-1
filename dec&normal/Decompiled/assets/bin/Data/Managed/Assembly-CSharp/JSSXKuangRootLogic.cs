using System;
using UnityEngine;

// Token: 0x0200094B RID: 2379
public class JSSXKuangRootLogic : SingletonUnity<JSSXKuangRootLogic>
{
	// Token: 0x0600428E RID: 17038 RVA: 0x0014554C File Offset: 0x0014374C
	public void Reset(CharacterAttributeData data)
	{
		this.curAttributeData = data;
		if (this.JSSXKuangUIRoot == null)
		{
			SingletonUnity<UIManager>.Instance.LoadUIItem(UIInfo.PlayerInfoRootItem, new UIManager.OnLoadUIDelegate(this.OnLoadPlayerInfoRootItem), null);
		}
		else
		{
			this.JSSXKuangUIRoot.Show(this.curAttributeData);
		}
	}

	// Token: 0x0600428F RID: 17039 RVA: 0x001455A4 File Offset: 0x001437A4
	public void OnLoadPlayerInfoRootItem(GameObject newObj, object param)
	{
		GameObject gameObject = Object.Instantiate(newObj) as GameObject;
		gameObject.transform.parent = this.ScaleRoot;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		this.JSSXKuangUIRoot = gameObject.GetComponent<JSSXKuangUILogic>();
		this.JSSXKuangUIRoot.Show(this.curAttributeData);
	}

	// Token: 0x04002EA0 RID: 11936
	public JSSXKuangUILogic JSSXKuangUIRoot;

	// Token: 0x04002EA1 RID: 11937
	public Transform ScaleRoot;

	// Token: 0x04002EA2 RID: 11938
	private CharacterAttributeData curAttributeData;
}
