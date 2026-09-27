using System;

// Token: 0x020009D2 RID: 2514
public class TowerCurrentFloorInfoLogic : SingletonUnity<TowerCurrentFloorInfoLogic>
{
	// Token: 0x0600477A RID: 18298 RVA: 0x0016C63C File Offset: 0x0016A83C
	public void ShowCurrentFloor(int floor)
	{
		this.currentFloorLabel.text = string.Format("Floor {0}", floor);
	}

	// Token: 0x0600477B RID: 18299 RVA: 0x0016C65C File Offset: 0x0016A85C
	public static void ShowFloor(int floor)
	{
		if (SingletonUnity<TowerCurrentFloorInfoLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TowerCurrentFloorInfoLogic>.Instance.gameObject))
		{
			SingletonUnity<TowerCurrentFloorInfoLogic>.Instance.ShowCurrentFloor(floor);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TowerCurrentFloorInfo, delegate(bool bSuccess, object param)
			{
				if (bSuccess)
				{
					SingletonUnity<TowerCurrentFloorInfoLogic>.Instance.ShowCurrentFloor(floor);
				}
			}, null);
		}
	}

	// Token: 0x040034B9 RID: 13497
	public UILabel currentFloorLabel;
}
