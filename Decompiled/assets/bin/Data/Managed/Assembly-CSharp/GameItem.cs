using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000105 RID: 261
public class GameItem
{
	// Token: 0x06000951 RID: 2385 RVA: 0x000448DC File Offset: 0x00042ADC
	public GameItem()
	{
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x00044938 File Offset: 0x00042B38
	public GameItem(string id, EQUIP_QUALITY quality, int count)
	{
		this.mItemId = id;
		this.mQuality = quality;
		this.mStackNum = count;
		this.mItemData = DataManager.GetItemDataByID(id);
		if (this.mItemData.Type != GameDefine.ITEM_TYPE.EQUIP)
		{
			this.mQuality = this.mItemData.QualityType;
		}
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x000449D4 File Offset: 0x00042BD4
	public GameItem(long indexId, ITEM_CONTAINER_TYPE type, string itemId, bool bindFlag, int stackNum, EQUIP_QUALITY quality)
	{
		this.mIndexId = indexId;
		this.mType = type;
		this.ItemId = itemId;
		this.mBindFlag = bindFlag;
		this.mStackNum = stackNum;
		this.mQuality = quality;
	}

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x06000954 RID: 2388 RVA: 0x00044A5C File Offset: 0x00042C5C
	// (set) Token: 0x06000955 RID: 2389 RVA: 0x00044A64 File Offset: 0x00042C64
	public long IndexId
	{
		get
		{
			return this.mIndexId;
		}
		set
		{
			this.mIndexId = value;
		}
	}

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x06000956 RID: 2390 RVA: 0x00044A70 File Offset: 0x00042C70
	// (set) Token: 0x06000957 RID: 2391 RVA: 0x00044A78 File Offset: 0x00042C78
	public ITEM_CONTAINER_TYPE ContainerType
	{
		get
		{
			return this.mType;
		}
		set
		{
			this.mType = value;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x06000958 RID: 2392 RVA: 0x00044A84 File Offset: 0x00042C84
	// (set) Token: 0x06000959 RID: 2393 RVA: 0x00044A8C File Offset: 0x00042C8C
	public EQUIP_BACKPACK_TYPE EquipType
	{
		get
		{
			return this.mEquipType;
		}
		set
		{
			this.mEquipType = value;
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x0600095A RID: 2394 RVA: 0x00044A98 File Offset: 0x00042C98
	// (set) Token: 0x0600095B RID: 2395 RVA: 0x00044AA0 File Offset: 0x00042CA0
	public string ItemId
	{
		get
		{
			return this.mItemId;
		}
		set
		{
			if (!this.mItemId.Equals(value))
			{
				this.mItemData = DataManager.GetItemDataByID(value);
				if (this.mItemData != null)
				{
					this.mItemId = value;
					if (this.mItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
					{
						this.mEquipType = (EQUIP_BACKPACK_TYPE)this.mItemData.SubType;
					}
					else
					{
						this.mEquipType = EQUIP_BACKPACK_TYPE.COUNT;
					}
				}
				else
				{
					this.mEquipType = EQUIP_BACKPACK_TYPE.COUNT;
				}
			}
		}
	}

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x0600095C RID: 2396 RVA: 0x00044B18 File Offset: 0x00042D18
	// (set) Token: 0x0600095D RID: 2397 RVA: 0x00044B20 File Offset: 0x00042D20
	public ItemData ItemData
	{
		get
		{
			return this.mItemData;
		}
		set
		{
			this.mItemData = value;
		}
	}

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x0600095E RID: 2398 RVA: 0x00044B2C File Offset: 0x00042D2C
	// (set) Token: 0x0600095F RID: 2399 RVA: 0x00044B34 File Offset: 0x00042D34
	public bool BindFlag
	{
		get
		{
			return this.mBindFlag;
		}
		set
		{
			this.mBindFlag = value;
		}
	}

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x06000960 RID: 2400 RVA: 0x00044B40 File Offset: 0x00042D40
	// (set) Token: 0x06000961 RID: 2401 RVA: 0x00044B48 File Offset: 0x00042D48
	public int StackNum
	{
		get
		{
			return this.mStackNum;
		}
		set
		{
			this.mStackNum = value;
		}
	}

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x06000962 RID: 2402 RVA: 0x00044B54 File Offset: 0x00042D54
	// (set) Token: 0x06000963 RID: 2403 RVA: 0x00044B5C File Offset: 0x00042D5C
	public EQUIP_QUALITY Quality
	{
		get
		{
			return this.mQuality;
		}
		set
		{
			this.mQuality = value;
		}
	}

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x06000964 RID: 2404 RVA: 0x00044B68 File Offset: 0x00042D68
	// (set) Token: 0x06000965 RID: 2405 RVA: 0x00044B70 File Offset: 0x00042D70
	public int Appraise
	{
		get
		{
			return this.mAppraise;
		}
		set
		{
			this.mAppraise = value;
		}
	}

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x06000966 RID: 2406 RVA: 0x00044B7C File Offset: 0x00042D7C
	public bool IsAppraise
	{
		get
		{
			return this.mAppraise == 1;
		}
	}

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x06000967 RID: 2407 RVA: 0x00044B88 File Offset: 0x00042D88
	// (set) Token: 0x06000968 RID: 2408 RVA: 0x00044B90 File Offset: 0x00042D90
	public Dictionary<long, random_attri> Random_AttriDic
	{
		get
		{
			return this.mRandom_AttriDic;
		}
		set
		{
			this.mRandom_AttriDic = value;
		}
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x06000969 RID: 2409 RVA: 0x00044B9C File Offset: 0x00042D9C
	public bool IsHaveRandomAtt
	{
		get
		{
			return this.mRandom_AttriDic != null && this.mRandom_AttriDic.Count > 0;
		}
	}

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x0600096A RID: 2410 RVA: 0x00044BC0 File Offset: 0x00042DC0
	// (set) Token: 0x0600096B RID: 2411 RVA: 0x00044BC8 File Offset: 0x00042DC8
	public Dictionary<long, inlay> InlayDic
	{
		get
		{
			return this.mInlayDic;
		}
		set
		{
			this.mInlayDic = value;
		}
	}

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x0600096C RID: 2412 RVA: 0x00044BD4 File Offset: 0x00042DD4
	public bool IsHaveInlay
	{
		get
		{
			return this.mInlayDic != null && this.mInlayDic.Count > 0;
		}
	}

	// Token: 0x17000191 RID: 401
	// (get) Token: 0x0600096D RID: 2413 RVA: 0x00044BF8 File Offset: 0x00042DF8
	public List<QualityData> QualityDataList
	{
		get
		{
			if (!string.IsNullOrEmpty(this.ItemId))
			{
				EquipData equipDataById = DataManager.GetEquipDataById(this.ItemId);
				this.mQualityDataList = DataManager.GetQualityDataListByID(equipDataById.QualityID);
			}
			return this.mQualityDataList;
		}
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x00044C38 File Offset: 0x00042E38
	public void SetAttInfo(int appraiseinfo, Dictionary<long, random_attri> randomatt, Dictionary<long, inlay> inlaydic)
	{
		this.Appraise = appraiseinfo;
		this.Random_AttriDic = randomatt;
		this.InlayDic = inlaydic;
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x00044C50 File Offset: 0x00042E50
	public void SetAddItem()
	{
		this.mItemId = GameDefine.EmptyAddItemID;
		this.mQuality = EQUIP_QUALITY.KUANG_WHITE;
		this.mStackNum = 1;
		this.mItemData = DataManager.GetItemDataByID(this.mItemId);
		this.Parm[5] = 1;
	}

	// Token: 0x17000192 RID: 402
	// (get) Token: 0x06000970 RID: 2416 RVA: 0x00044C88 File Offset: 0x00042E88
	// (set) Token: 0x06000971 RID: 2417 RVA: 0x00044C90 File Offset: 0x00042E90
	public int ItemLevel
	{
		get
		{
			return this.mItemLevel;
		}
		set
		{
			this.mItemLevel = value;
		}
	}

	// Token: 0x17000193 RID: 403
	// (get) Token: 0x06000972 RID: 2418 RVA: 0x00044C9C File Offset: 0x00042E9C
	public int[] Parm
	{
		get
		{
			return this.parm;
		}
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x00044CA4 File Offset: 0x00042EA4
	public void SetParm(List<long> parms)
	{
		for (int i = 0; i < parms.Count; i++)
		{
			this.parm[i] = (int)parms[i];
		}
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x00044CD8 File Offset: 0x00042ED8
	public void Reset()
	{
		this.mItemId = string.Empty;
		this.mStackNum = -1;
		this.mIndexId = -1L;
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x00044CF4 File Offset: 0x00042EF4
	public bool IsEmpty()
	{
		return string.IsNullOrEmpty(this.mItemId) || this.mItemData == null;
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x00044D14 File Offset: 0x00042F14
	public GameItem ShallowCopy()
	{
		return (GameItem)base.MemberwiseClone();
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x00044D24 File Offset: 0x00042F24
	public bool IsFull()
	{
		if (!string.IsNullOrEmpty(this.mItemId))
		{
			ItemData itemDataByID = DataManager.GetItemDataByID(this.mItemId);
			if (itemDataByID != null && itemDataByID.Stack > 1 && itemDataByID.Stack > this.mStackNum)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x00044D74 File Offset: 0x00042F74
	public int GetItemLeftSpace()
	{
		if (!string.IsNullOrEmpty(this.mItemId))
		{
			ItemData itemDataByID = DataManager.GetItemDataByID(this.mItemId);
			if (itemDataByID != null)
			{
				return itemDataByID.Stack - this.mStackNum;
			}
		}
		return -1;
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x00044DB4 File Offset: 0x00042FB4
	public void SetItem(GameItem item)
	{
		this.mIndexId = item.IndexId;
		this.mItemId = item.ItemId;
		this.BindFlag = item.BindFlag;
		this.mStackNum = item.StackNum;
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x00044DF4 File Offset: 0x00042FF4
	public void UpdateItem(gameitem netItem)
	{
		this.ItemId = netItem.itemId;
		if (netItem.HasParm)
		{
			this.SetParm(netItem.parm);
		}
		if (netItem.HasBindflag)
		{
			this.BindFlag = netItem.bindflag;
		}
		else
		{
			this.BindFlag = false;
		}
		if (netItem.HasStack)
		{
			this.StackNum = (int)netItem.stack;
		}
		else
		{
			this.StackNum = 1;
		}
		if (netItem.HasIndexId)
		{
			this.IndexId = netItem.indexId;
		}
		else
		{
			this.IndexId = -1L;
		}
		if (netItem.HasQuality)
		{
			this.Quality = (EQUIP_QUALITY)netItem.quality;
		}
		else
		{
			this.Quality = EQUIP_QUALITY.INVALID;
		}
		if (netItem.HasLevel)
		{
			this.ItemLevel = (int)netItem.level;
		}
		else
		{
			this.ItemLevel = 0;
		}
		if (netItem.HasAppraise)
		{
			this.Appraise = (int)netItem.appraise;
		}
		else
		{
			this.Appraise = 0;
		}
		if (netItem.HasRandom_attri)
		{
			this.Random_AttriDic = netItem.random_attri;
		}
		else
		{
			this.Random_AttriDic = null;
		}
		if (netItem.HasInlay)
		{
			this.InlayDic = netItem.inlay;
		}
		else
		{
			this.InlayDic = null;
		}
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x00044F44 File Offset: 0x00043144
	public int GetItemScore()
	{
		if (this.mItemData != null)
		{
			return this.mItemData.GetScore(this.mQuality);
		}
		return 0;
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x00044F64 File Offset: 0x00043164
	public int GetItemCombatVal()
	{
		if (this.mItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			EquipData equipDataById = DataManager.GetEquipDataById(this.mItemData.ID);
			float num = 0f;
			for (int i = 0; i < equipDataById.GetBaseAttCount(); i++)
			{
				int attrIDByQuality = equipDataById.GetAttrIDByQuality(i);
				if (attrIDByQuality != 0)
				{
					num += (float)equipDataById.GetAttrValByQualityAndLevel(i, (int)this.GetItemQuality(), this.mItemLevel) * GameDefine.GET_ATTRIBUTE_COMBAT_VAL(attrIDByQuality);
				}
			}
			if (this.IsHaveRandomAtt)
			{
				if (this.mItemData.SubType != 0)
				{
					foreach (random_attri random_attri in this.Random_AttriDic.Values)
					{
						int attid = (int)random_attri.id;
						num += (float)((int)random_attri.value) * GameDefine.GET_ATTRIBUTE_COMBAT_VAL(attid);
					}
				}
			}
			return Mathf.FloorToInt(num);
		}
		if (this.mItemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
		{
			EquipData equipDataById2 = DataManager.GetEquipDataById(this.mItemData.ID);
			float num2 = 0f;
			for (int j = 0; j < equipDataById2.GetBaseAttCount(); j++)
			{
				int attrIDByQuality2 = equipDataById2.GetAttrIDByQuality(j);
				if (attrIDByQuality2 != 0)
				{
					num2 += (float)equipDataById2.GetAttrValByQualityAndLevel(j, (int)this.GetItemQuality(), this.mItemLevel) * GameDefine.GET_ATTRIBUTE_COMBAT_VAL(attrIDByQuality2);
				}
			}
			return Mathf.FloorToInt(num2);
		}
		if (this.mItemData.Type == GameDefine.ITEM_TYPE.BADGE)
		{
			BadgeData badgeDataById = DataManager.GetBadgeDataById(this.mItemData.ID);
			float num3 = 0f;
			num3 += (float)badgeDataById.Value1 * GameDefine.GET_ATTRIBUTE_COMBAT_VAL(badgeDataById.Status1);
			if (badgeDataById.Status2 != -1)
			{
				num3 += (float)badgeDataById.Value2 * GameDefine.GET_ATTRIBUTE_COMBAT_VAL(badgeDataById.Status2);
			}
			return Mathf.FloorToInt(num3);
		}
		return 0;
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x00045174 File Offset: 0x00043374
	public EQUIP_QUALITY GetItemQuality()
	{
		if (this.ItemData.Type != GameDefine.ITEM_TYPE.EQUIP)
		{
			return this.mQuality;
		}
		if (this.IsAppraise)
		{
			return (EQUIP_QUALITY)this.GetQualityByScore();
		}
		return this.mQuality;
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x000451B4 File Offset: 0x000433B4
	public int GetQualityByScore()
	{
		int randomAttScore = this.GetRandomAttScore();
		if (GameManager.IsSupportCurDataVersion167())
		{
			if (this.QualityDataList != null && this.QualityDataList.Count > 0)
			{
				int i = 0;
				while (i < this.QualityDataList.Count)
				{
					if (randomAttScore < this.QualityDataList[i].EquipQualityScore)
					{
						if (i == 0)
						{
							return i;
						}
						return i - 1;
					}
					else
					{
						if (i == this.QualityDataList.Count - 1)
						{
							return i;
						}
						i++;
					}
				}
			}
		}
		else
		{
			List<ConfigData> configQualityScoreList = DataManager.GetConfigQualityScoreList();
			if (configQualityScoreList != null && configQualityScoreList.Count > 0)
			{
				int j = 0;
				while (j < configQualityScoreList.Count)
				{
					if (randomAttScore < configQualityScoreList[j].Valuei)
					{
						if (j == 0)
						{
							return j;
						}
						return j - 1;
					}
					else
					{
						if (j == configQualityScoreList.Count - 1)
						{
							return j;
						}
						j++;
					}
				}
			}
		}
		return 0;
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x000452A8 File Offset: 0x000434A8
	public int GetStarByScore()
	{
		if (this.ItemData.Type != GameDefine.ITEM_TYPE.EQUIP)
		{
			int randomAttScore = this.GetRandomAttScore();
			if (GameManager.IsSupportCurDataVersion167())
			{
				int itemQuality = (int)this.GetItemQuality();
				if (this.QualityDataList != null && this.QualityDataList.Count > itemQuality)
				{
					QualityData qualityData = this.QualityDataList[itemQuality];
					List<int> equipStarList = qualityData.EquipStarList;
					if (equipStarList != null && equipStarList.Count > 0)
					{
						for (int i = 0; i < equipStarList.Count; i++)
						{
							if (randomAttScore <= equipStarList[i])
							{
								return i;
							}
							if (i == equipStarList.Count - 1)
							{
								return i;
							}
						}
					}
				}
			}
			else
			{
				List<ConfigData> configStarScoreList = DataManager.GetConfigStarScoreList();
				if (configStarScoreList.Count > 0)
				{
					for (int j = 0; j < configStarScoreList.Count; j++)
					{
						if (randomAttScore < configStarScoreList[j].Valuei)
						{
							return j;
						}
						if (j == configStarScoreList.Count - 1)
						{
							return j + 1;
						}
					}
				}
			}
			return 0;
		}
		EquipData equipDataById = DataManager.GetEquipDataById(this.ItemData.ID);
		if (equipDataById.Class >= 1)
		{
			return equipDataById.Class - 1;
		}
		return 0;
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x000453F0 File Offset: 0x000435F0
	public int GetRandomAttScore()
	{
		int num = 0;
		if (this.IsHaveRandomAtt)
		{
			if (GameManager.IsSupportCurDataVersion167())
			{
				foreach (random_attri random_attri in this.Random_AttriDic.Values)
				{
					if (!random_attri.HasQualityId)
					{
						EquipData equipDataById = DataManager.GetEquipDataById(this.mItemData.ID);
						random_attri.qualityId = equipDataById.QualityID;
					}
					num += this.GetAttScoreByQuality((int)random_attri.quality, random_attri.qualityId);
				}
			}
			else
			{
				foreach (random_attri random_attri2 in this.Random_AttriDic.Values)
				{
					num += this.GetAttScoreByQuality((int)random_attri2.quality);
				}
			}
		}
		return num;
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00045518 File Offset: 0x00043718
	public void ShowPrintInfo()
	{
		if (this.IsHaveRandomAtt)
		{
			foreach (random_attri random_attri in this.Random_AttriDic.Values)
			{
				if (!random_attri.HasQualityId)
				{
					EquipData equipDataById = DataManager.GetEquipDataById(this.mItemData.ID);
					random_attri.qualityId = equipDataById.QualityID;
				}
			}
		}
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x000455B0 File Offset: 0x000437B0
	private int GetAttScoreByQuality(int quaval)
	{
		if (GameDefine.StarAttIntegral.ContainsKey(quaval))
		{
			ConfigData configDataByKey = DataManager.GetConfigDataByKey(GameDefine.StarAttIntegral[quaval]);
			if (configDataByKey != null)
			{
				return configDataByKey.Valuei;
			}
		}
		return 0;
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x000455EC File Offset: 0x000437EC
	private int GetAttScoreByQuality(int quaval, string qualityid)
	{
		List<QualityData> qualityDataListByID = DataManager.GetQualityDataListByID(qualityid);
		if (qualityDataListByID != null && qualityDataListByID.Count > quaval)
		{
			return qualityDataListByID[quaval].AttInitialScore;
		}
		return 0;
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x00045620 File Offset: 0x00043820
	public int GetEquipAttQuality(int quaval, string qualityid)
	{
		if (GameManager.IsSupportCurDataVersion167())
		{
			int attScoreByQuality = this.GetAttScoreByQuality(quaval, qualityid);
			if (this.QualityDataList != null && this.QualityDataList.Count > 0)
			{
				int i = 0;
				while (i < this.QualityDataList.Count)
				{
					if (attScoreByQuality < this.QualityDataList[i].AttQualityScore)
					{
						if (i == 0)
						{
							return i;
						}
						return i - 1;
					}
					else
					{
						if (i == this.QualityDataList.Count - 1)
						{
							return i;
						}
						i++;
					}
				}
			}
			return 0;
		}
		return quaval;
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x000456B8 File Offset: 0x000438B8
	public int GetInhertPrice(int quality, bool isweapon)
	{
		if (this.QualityDataList == null || this.QualityDataList.Count <= quality)
		{
			return 0;
		}
		if (isweapon)
		{
			return this.QualityDataList[quality].WeaponInherit;
		}
		return this.QualityDataList[quality].EquipInherit;
	}

	// Token: 0x04000855 RID: 2133
	private long mIndexId = -1L;

	// Token: 0x04000856 RID: 2134
	private ITEM_CONTAINER_TYPE mType = ITEM_CONTAINER_TYPE.INVALID;

	// Token: 0x04000857 RID: 2135
	private EQUIP_BACKPACK_TYPE mEquipType = EQUIP_BACKPACK_TYPE.COUNT;

	// Token: 0x04000858 RID: 2136
	private string mItemId = string.Empty;

	// Token: 0x04000859 RID: 2137
	private ItemData mItemData;

	// Token: 0x0400085A RID: 2138
	private bool mBindFlag;

	// Token: 0x0400085B RID: 2139
	private int mStackNum = -1;

	// Token: 0x0400085C RID: 2140
	private EQUIP_QUALITY mQuality = EQUIP_QUALITY.INVALID;

	// Token: 0x0400085D RID: 2141
	private int mAppraise;

	// Token: 0x0400085E RID: 2142
	private Dictionary<long, random_attri> mRandom_AttriDic;

	// Token: 0x0400085F RID: 2143
	private Dictionary<long, inlay> mInlayDic;

	// Token: 0x04000860 RID: 2144
	private List<QualityData> mQualityDataList = new List<QualityData>();

	// Token: 0x04000861 RID: 2145
	private int mItemLevel;

	// Token: 0x04000862 RID: 2146
	private int[] parm = new int[8];
}
