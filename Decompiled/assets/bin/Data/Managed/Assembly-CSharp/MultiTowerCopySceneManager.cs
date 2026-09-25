using System;

// Token: 0x0200087F RID: 2175
public class MultiTowerCopySceneManager : SceneManager
{
	// Token: 0x060039E6 RID: 14822 RVA: 0x000F87E4 File Offset: 0x000F69E4
	public override void Init(string id)
	{
		base.Init(id);
	}

	// Token: 0x060039E7 RID: 14823 RVA: 0x000F87F0 File Offset: 0x000F69F0
	public void ShowFloor()
	{
		TowerCurrentFloorInfoLogic.ShowFloor(this.CurrentFloor);
	}

	// Token: 0x040025FD RID: 9725
	private int CurrentFloor;
}
