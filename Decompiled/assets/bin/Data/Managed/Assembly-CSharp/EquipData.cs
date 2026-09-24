using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200016D RID: 365
public class EquipData
{
	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x06000F29 RID: 3881 RVA: 0x00062400 File Offset: 0x00060600
	public PROFESSION_TYPE profession
	{
		get
		{
			return (PROFESSION_TYPE)this.Job;
		}
	}

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x06000F2A RID: 3882 RVA: 0x00062408 File Offset: 0x00060608
	public ATTRIBUTE_TYPE BaseStatusType
	{
		get
		{
			return (ATTRIBUTE_TYPE)this.Basestatus;
		}
	}

	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00062410 File Offset: 0x00060610
	public ATTRIBUTE_TYPE ExtraStatus1Type
	{
		get
		{
			return (ATTRIBUTE_TYPE)this.Status1;
		}
	}

	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00062418 File Offset: 0x00060618
	public ATTRIBUTE_TYPE ExtraStatus2Type
	{
		get
		{
			return (ATTRIBUTE_TYPE)this.Status2;
		}
	}

	// Token: 0x170002ED RID: 749
	// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00062420 File Offset: 0x00060620
	public ATTRIBUTE_TYPE ExStatusType
	{
		get
		{
			return (ATTRIBUTE_TYPE)this.ExStatus;
		}
	}

	// Token: 0x170002EE RID: 750
	// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00062428 File Offset: 0x00060628
	public EQUIP_BACKPACK_TYPE EquipType
	{
		get
		{
			return (EQUIP_BACKPACK_TYPE)this.Position;
		}
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x00062430 File Offset: 0x00060630
	public int GetAppraisePrice(EQUIP_QUALITY quality)
	{
		if (this.mPrice == null && !string.IsNullOrEmpty(this.identify))
		{
			string[] array = this.identify.Split(new char[]
			{
				'#'
			});
			this.mPrice = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				this.mPrice[i] = int.Parse(array[i]);
			}
		}
		if (this.mPrice != null && quality < (EQUIP_QUALITY)this.mPrice.Length)
		{
			return this.mPrice[(int)quality];
		}
		return 0;
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x000624C8 File Offset: 0x000606C8
	public int GetAttrIDByQuality(int qualityValue)
	{
		switch (qualityValue)
		{
		case 0:
			return this.Basestatus;
		case 1:
			return this.Status1;
		case 2:
			return this.Status2;
		case 3:
			return this.ExStatus;
		default:
			return 0;
		}
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x00062510 File Offset: 0x00060710
	public int GetAttrValueByQuality(int targetQualityValue, int itemQuality)
	{
		ItemData itemDataByID = DataManager.GetItemDataByID(this.ID);
		if (itemDataByID.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
		{
			switch (targetQualityValue)
			{
			case 0:
				return Mathf.FloorToInt((float)this.BSValue);
			case 1:
				return Mathf.FloorToInt((float)this.ES1V);
			case 2:
				return Mathf.FloorToInt((float)this.ES2V);
			case 3:
				return Mathf.FloorToInt((float)this.ExV);
			default:
				return -1;
			}
		}
		else
		{
			float num = 1f;
			if (GameManager.IsSupportCurDataVersion167())
			{
				List<QualityData> qualityDataListByID = DataManager.GetQualityDataListByID(this.QualityID);
				if (qualityDataListByID != null && qualityDataListByID.Count > itemQuality)
				{
					num = qualityDataListByID[itemQuality].ModulusVal;
				}
			}
			else if (GameDefine.EquipQualityValAddName.ContainsKey(itemQuality))
			{
				ConfigData configDataByKey = DataManager.GetConfigDataByKey(GameDefine.EquipQualityValAddName[itemQuality]);
				if (configDataByKey != null)
				{
					num = configDataByKey.Valuef;
				}
			}
			switch (targetQualityValue)
			{
			case 0:
				return Mathf.FloorToInt((float)this.BSValue * num);
			case 1:
				return Mathf.FloorToInt((float)this.ES1V * num);
			case 2:
				return Mathf.FloorToInt((float)this.ES2V * num);
			case 3:
				return Mathf.FloorToInt((float)this.ExV * num);
			default:
				return -1;
			}
		}
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x00062658 File Offset: 0x00060858
	public int GetAttrValByQualityAndLevel(int targetQualityValue, int itemQuality, int itemLevel)
	{
		int attrValueByQuality = this.GetAttrValueByQuality(targetQualityValue, itemQuality);
		if (this.mCacheUpgradeDataList == null)
		{
			this.mCacheUpgradeDataList = DataManager.GetEquipmentUpgradeDataListByPartID(this.Position);
			if (this.mCacheUpgradeDataList == null)
			{
				Debug.LogError("EquipmentUpgradeData error no have postion");
			}
		}
		if (this.mCacheUpgradeDataList != null && this.mCacheUpgradeDataList.Count >= itemLevel && itemLevel > 0)
		{
			EquipmentUpgradeData equipmentUpgradeData = this.mCacheUpgradeDataList[itemLevel - 1];
			if (equipmentUpgradeData != null)
			{
				int num = this.Job;
				if (num == -1)
				{
					num = this.WeaponType;
				}
				float equipKByQuality = equipmentUpgradeData.GetEquipKByQuality(targetQualityValue, num);
				return Mathf.FloorToInt((float)attrValueByQuality + equipKByQuality * (float)itemLevel);
			}
		}
		return attrValueByQuality;
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x00062704 File Offset: 0x00060904
	public int GetAttrEnhanceVal(int targetQualityValue, int itemQuality, int itemLevel)
	{
		if (this.mCacheUpgradeDataList == null)
		{
			this.mCacheUpgradeDataList = DataManager.GetEquipmentUpgradeDataListByPartID(this.Position);
			if (this.mCacheUpgradeDataList == null)
			{
				Debug.LogError("EquipmentUpgradeData error no have postion");
			}
		}
		if (this.mCacheUpgradeDataList != null && this.mCacheUpgradeDataList.Count >= itemLevel && itemLevel > 0)
		{
			EquipmentUpgradeData equipmentUpgradeData = this.mCacheUpgradeDataList[itemLevel - 1];
			if (equipmentUpgradeData != null)
			{
				int num = this.Job;
				if (num == -1)
				{
					num = this.WeaponType;
				}
				float equipKByQuality = equipmentUpgradeData.GetEquipKByQuality(targetQualityValue, num);
				return Mathf.FloorToInt(equipKByQuality * (float)itemLevel);
			}
		}
		return 0;
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x000627A4 File Offset: 0x000609A4
	public int GetUpgradeLevelByExp(int curLevel, int exp, out int use)
	{
		if (this.mCacheUpgradeDataList == null)
		{
			this.mCacheUpgradeDataList = DataManager.GetEquipmentUpgradeDataListByPartID(this.Position);
			if (this.mCacheUpgradeDataList == null)
			{
				Debug.LogError("EquipmentUpgradeData error no have postion");
			}
		}
		use = 0;
		if (this.mCacheUpgradeDataList == null)
		{
			return curLevel;
		}
		int num = exp;
		int num2 = curLevel;
		while (num > 0 && this.mCacheUpgradeDataList.Count > num2)
		{
			EquipmentUpgradeData equipmentUpgradeData = this.mCacheUpgradeDataList[num2];
			if (equipmentUpgradeData != null)
			{
				int num3 = Mathf.FloorToInt(equipmentUpgradeData.LevelB + equipmentUpgradeData.LevelK * (float)num2 * (float)num2 * 3f);
				if (num < num3)
				{
					break;
				}
				num -= num3;
				use += num3;
				num2++;
			}
		}
		return num2;
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x00062868 File Offset: 0x00060A68
	public int GetUpgradeExpValByLevel(int itemLevel)
	{
		if (this.mCacheUpgradeDataList == null)
		{
			this.mCacheUpgradeDataList = DataManager.GetEquipmentUpgradeDataListByPartID(this.Position);
			if (this.mCacheUpgradeDataList == null)
			{
				Debug.LogError("EquipmentUpgradeData error no have postion");
			}
		}
		if (this.mCacheUpgradeDataList != null && this.mCacheUpgradeDataList.Count > itemLevel)
		{
			EquipmentUpgradeData equipmentUpgradeData = this.mCacheUpgradeDataList[itemLevel];
			if (equipmentUpgradeData != null)
			{
				return Mathf.FloorToInt(equipmentUpgradeData.LevelB + equipmentUpgradeData.LevelK * (float)itemLevel * (float)itemLevel * 3f);
			}
		}
		return 0;
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x000628F8 File Offset: 0x00060AF8
	public int GetUpgradeMoneyByQualityAndLevel(int itemQuality, int itemLevel)
	{
		if (this.mCacheUpgradeDataList == null)
		{
			this.mCacheUpgradeDataList = DataManager.GetEquipmentUpgradeDataListByPartID(this.Position);
			if (this.mCacheUpgradeDataList == null)
			{
				Debug.LogError("EquipmentUpgradeData error no have postion");
			}
		}
		if (this.mCacheUpgradeDataList != null && this.mCacheUpgradeDataList.Count > itemLevel)
		{
			EquipmentUpgradeData equipmentUpgradeData = this.mCacheUpgradeDataList[itemLevel];
			if (equipmentUpgradeData != null)
			{
				float moneyKByQuality = equipmentUpgradeData.GetMoneyKByQuality(itemQuality);
				float moneyBByQuality = equipmentUpgradeData.GetMoneyBByQuality(itemQuality);
				return Mathf.FloorToInt(moneyBByQuality + moneyKByQuality * (float)itemLevel);
			}
		}
		return 0;
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x00062984 File Offset: 0x00060B84
	public int GetBaseAttCount()
	{
		int num = 0;
		if (this.Basestatus != 0)
		{
			num++;
		}
		if (this.Status1 != 0)
		{
			num++;
		}
		if (this.Status2 != 0)
		{
			num++;
		}
		if (this.ExStatus != 0)
		{
			num++;
		}
		return num;
	}

	// Token: 0x04000E7C RID: 3708
	public string ID = string.Empty;

	// Token: 0x04000E7D RID: 3709
	public string NAME = string.Empty;

	// Token: 0x04000E7E RID: 3710
	public int Lv = 1;

	// Token: 0x04000E7F RID: 3711
	public int Job;

	// Token: 0x04000E80 RID: 3712
	public int Position;

	// Token: 0x04000E81 RID: 3713
	public int Basestatus;

	// Token: 0x04000E82 RID: 3714
	public int BSValue;

	// Token: 0x04000E83 RID: 3715
	public int Status1;

	// Token: 0x04000E84 RID: 3716
	public int ES1V;

	// Token: 0x04000E85 RID: 3717
	public int Status2;

	// Token: 0x04000E86 RID: 3718
	public int ES2V;

	// Token: 0x04000E87 RID: 3719
	public int ExStatus;

	// Token: 0x04000E88 RID: 3720
	public int ExV;

	// Token: 0x04000E89 RID: 3721
	public string ModelId = string.Empty;

	// Token: 0x04000E8A RID: 3722
	public int UpgradeMaxLevel;

	// Token: 0x04000E8B RID: 3723
	public string DropId1;

	// Token: 0x04000E8C RID: 3724
	public string DropId2;

	// Token: 0x04000E8D RID: 3725
	public string DropId3;

	// Token: 0x04000E8E RID: 3726
	public string DropId4;

	// Token: 0x04000E8F RID: 3727
	public string DropId5;

	// Token: 0x04000E90 RID: 3728
	public string DropId6;

	// Token: 0x04000E91 RID: 3729
	public string identify = string.Empty;

	// Token: 0x04000E92 RID: 3730
	public int LevelScale = 10000;

	// Token: 0x04000E93 RID: 3731
	public string BaseSkills;

	// Token: 0x04000E94 RID: 3732
	public int WeaponType;

	// Token: 0x04000E95 RID: 3733
	public string QualityID = string.Empty;

	// Token: 0x04000E96 RID: 3734
	public int Class;

	// Token: 0x04000E97 RID: 3735
	private int[] mPrice;

	// Token: 0x04000E98 RID: 3736
	private List<EquipmentUpgradeData> mCacheUpgradeDataList;
}
