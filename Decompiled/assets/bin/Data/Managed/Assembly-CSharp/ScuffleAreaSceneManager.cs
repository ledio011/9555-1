using System;

// Token: 0x02000886 RID: 2182
public class ScuffleAreaSceneManager : SceneManager
{
	// Token: 0x06003AD8 RID: 15064 RVA: 0x000FF990 File Offset: 0x000FDB90
	public override void Init(string id)
	{
		base.Init(id);
		this.scuffleData = DataManager.GetScuffleDataById(base.CurrentMapInofData.Param1);
		int floorId = (!this.scuffleData.MapID.Equals(base.CurrentMapInofData.ID)) ? 1 : 0;
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ScuffleAreaInfoRoot, delegate
		{
			SingletonUnity<ScuffleAreaInfoRoot>.Instance.Reset(floorId, this.scuffleData);
		}, null);
	}

	// Token: 0x06003AD9 RID: 15065 RVA: 0x000FFA10 File Offset: 0x000FDC10
	public virtual void MoveToNextFloor()
	{
	}

	// Token: 0x04002683 RID: 9859
	protected ScuffleData scuffleData;
}
