using System;
using Sproto;
using SprotoType;

// Token: 0x02000242 RID: 578
public class next_wave_handler
{
	// Token: 0x06001309 RID: 4873 RVA: 0x0007C274 File Offset: 0x0007A474
	public static SprotoTypeBase next_wave_request(SprotoTypeBase req)
	{
		next_wave.request request = req as next_wave.request;
		if (request != null)
		{
			SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			MapInfoData currentMapInofData = sceneManager.CurrentMapInofData;
			if (currentMapInofData.MapType == MAPTYPE.SINGLE_KILL_MONSTER_COPY)
			{
				KillBossLocalSceneManager killBossLocalSceneManager = sceneManager as KillBossLocalSceneManager;
				if (killBossLocalSceneManager != null)
				{
					int num = (int)request.waveid;
					killBossLocalSceneManager.OpenBlock(num - 1);
				}
			}
			else if (currentMapInofData.MapType == MAPTYPE.EQUIP_COPY)
			{
				EquipCopySceneManager equipCopySceneManager = sceneManager as EquipCopySceneManager;
				if (equipCopySceneManager != null)
				{
					int num2 = (int)request.waveid;
					equipCopySceneManager.OpenBlock(num2 - 1);
				}
			}
			else if (currentMapInofData.MapType == MAPTYPE.MUTIPLE_KILL_MONSTER_COPY)
			{
				MultiKillBossSceneManager multiKillBossSceneManager = sceneManager as MultiKillBossSceneManager;
				if (multiKillBossSceneManager != null)
				{
					int num3 = (int)request.waveid;
					multiKillBossSceneManager.OpenBlock(num3 - 1);
				}
			}
			else if (currentMapInofData.MapType == MAPTYPE.EXP_DAILY_COPY || currentMapInofData.MapType == MAPTYPE.SINGLE_EXP_DAILY_COPY)
			{
				EXPSceneManager expsceneManager = sceneManager as EXPSceneManager;
				if (expsceneManager != null)
				{
					expsceneManager.OpenBlock();
				}
			}
			else if (currentMapInofData.MapType == MAPTYPE.SINGLE_RUN_POINT_COPY)
			{
				SingleRunPointSceneManager singleRunPointSceneManager = sceneManager as SingleRunPointSceneManager;
				if (singleRunPointSceneManager != null)
				{
					singleRunPointSceneManager.OpenBlock();
				}
			}
		}
		return null;
	}
}
