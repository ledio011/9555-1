using System;
using UnityEngine;

// Token: 0x020009B5 RID: 2485
public class SurveyItemBtnRootLogic : SingletonUnity<SurveyItemBtnRootLogic>
{
	// Token: 0x060046BA RID: 18106 RVA: 0x00166EE4 File Offset: 0x001650E4
	public void Reset(SurveyItemObj curObj)
	{
		this.mCurSurveyItemObj = curObj;
	}

	// Token: 0x060046BB RID: 18107 RVA: 0x00166EF0 File Offset: 0x001650F0
	public void OnClickSurveyBtn()
	{
		if (SingletonUnity<SurveyProgressLineLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SurveyProgressLineLogic>.Instance.gameObject))
		{
			return;
		}
		if (this.mCurSurveyItemObj != null)
		{
			ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			if (mainPlayer.IsLocalDrivingCar)
			{
				if (SingletonUnity<CitySimController>.Exists)
				{
					SingletonUnity<CitySimController>.Instance.RobCar(delegate
					{
						mainPlayer.MoveTo(this.mCurSurveyItemObj.transform.position, 1f, delegate(ObjCharacter A_1)
						{
							mainPlayer.FaceToPub(this.mCurSurveyItemObj.transform.position);
							Singleton<SurveyItemManager>.Instance.StartSurveyItem(this.mCurSurveyItemObj);
						});
					});
				}
			}
			else
			{
				mainPlayer.MoveTo(this.mCurSurveyItemObj.transform.position, 1f, delegate(ObjCharacter A_1)
				{
					mainPlayer.FaceToPub(this.mCurSurveyItemObj.transform.position);
					Singleton<SurveyItemManager>.Instance.StartSurveyItem(this.mCurSurveyItemObj);
				});
			}
		}
	}

	// Token: 0x040033C7 RID: 13255
	public GameObject BtnRoot;

	// Token: 0x040033C8 RID: 13256
	private SurveyItemObj mCurSurveyItemObj;
}
