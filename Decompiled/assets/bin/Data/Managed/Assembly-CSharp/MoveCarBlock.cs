using System;
using UnityEngine;

// Token: 0x02000115 RID: 277
public class MoveCarBlock : MonoBehaviour
{
	// Token: 0x06000A11 RID: 2577 RVA: 0x00049A74 File Offset: 0x00047C74
	private void OnTriggerEnter(Collider other)
	{
		GameObject gameObject = other.gameObject;
		if (gameObject.transform.parent != null && gameObject.transform.parent.parent != null)
		{
			Obj component = gameObject.transform.parent.parent.gameObject.GetComponent<Obj>();
			if (component != null && component.ObjType == GameDefine.OBJ_TYPE.OBJ_PLAYER_CAR)
			{
				this.StartBlocking();
			}
		}
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x00049AF4 File Offset: 0x00047CF4
	private void StartBlocking()
	{
		if (this.EnableFlag)
		{
			return;
		}
		this.EnableFlag = true;
		ObjCarInitData initData = null;
		if (this.PoliceFlag == 0)
		{
			initData = new ObjCarInitData(this.Path.PathPointList[0].transform.position, this.Path.PathPointList[0].transform.eulerAngles, -1L, "PoliceCar", true, null);
		}
		else if (this.PoliceFlag == 3)
		{
			initData = new ObjCarInitData(this.Path.PathPointList[0].transform.position, this.Path.PathPointList[0].transform.eulerAngles, -1L, MoveCarBlock.NormalCarName[Random.Range(0, MoveCarBlock.NormalCarName.Length)], false, null);
		}
		else if (this.PoliceFlag == 4)
		{
			initData = new ObjCarInitData(this.Path.PathPointList[0].transform.position, this.Path.PathPointList[0].transform.eulerAngles, -1L, "PoliceCar", false, null);
		}
		ObjSimpleAICar simpleAICar = Singleton<ObjManager>.Instance.GetSimpleAICar(initData);
		if (simpleAICar != null)
		{
			simpleAICar.Reset(initData);
			simpleAICar.SetPath(this.Path, 0);
			simpleAICar.EnableCar(null);
			simpleAICar.rigidbody.velocity = simpleAICar.transform.forward * 30f;
		}
	}

	// Token: 0x040008E9 RID: 2281
	public CarPath Path;

	// Token: 0x040008EA RID: 2282
	public int PoliceFlag;

	// Token: 0x040008EB RID: 2283
	public static string[] NormalCarName = new string[]
	{
		"Chevrolet"
	};

	// Token: 0x040008EC RID: 2284
	private bool EnableFlag;
}
