using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000087 RID: 135
public static class Localization
{
	// Token: 0x1700005B RID: 91
	// (get) Token: 0x060002E2 RID: 738 RVA: 0x00013FE8 File Offset: 0x000121E8
	// (set) Token: 0x060002E3 RID: 739 RVA: 0x00014010 File Offset: 0x00012210
	public static Dictionary<string, string[]> dictionary
	{
		get
		{
			if (!Localization.localizationHasBeenSet)
			{
				Localization.language = PlayerPrefs.GetString("Language", "English");
			}
			return Localization.mDictionary;
		}
		set
		{
			Localization.localizationHasBeenSet = (value != null);
			Localization.mDictionary = value;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x060002E4 RID: 740 RVA: 0x00014024 File Offset: 0x00012224
	public static string[] knownLanguages
	{
		get
		{
			if (!Localization.localizationHasBeenSet)
			{
				Localization.LoadDictionary(PlayerPrefs.GetString("Language", "English"));
			}
			return Localization.mLanguages;
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x060002E5 RID: 741 RVA: 0x00014058 File Offset: 0x00012258
	// (set) Token: 0x060002E6 RID: 742 RVA: 0x000140AC File Offset: 0x000122AC
	public static string language
	{
		get
		{
			if (string.IsNullOrEmpty(Localization.mLanguage))
			{
				string[] knownLanguages = Localization.knownLanguages;
				Localization.mLanguage = PlayerPrefs.GetString("Language", (knownLanguages == null) ? "English" : knownLanguages[0]);
				Localization.LoadAndSelect(Localization.mLanguage);
			}
			return Localization.mLanguage;
		}
		set
		{
			if (Localization.mLanguage != value)
			{
				Localization.mLanguage = value;
				Localization.LoadAndSelect(value);
			}
		}
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x000140CC File Offset: 0x000122CC
	private static bool LoadDictionary(string value)
	{
		TextAsset textAsset = (!Localization.localizationHasBeenSet) ? (Resources.Load("Localization", typeof(TextAsset)) as TextAsset) : null;
		Localization.localizationHasBeenSet = true;
		if (textAsset != null && Localization.LoadCSV(textAsset))
		{
			return true;
		}
		if (string.IsNullOrEmpty(value))
		{
			return false;
		}
		textAsset = (Resources.Load(value, typeof(TextAsset)) as TextAsset);
		if (textAsset != null)
		{
			Localization.Load(textAsset);
			return true;
		}
		return false;
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x0001415C File Offset: 0x0001235C
	private static bool LoadAndSelect(string value)
	{
		if (!string.IsNullOrEmpty(value))
		{
			if (Localization.mDictionary.Count == 0 && !Localization.LoadDictionary(value))
			{
				return false;
			}
			if (Localization.SelectLanguage(value))
			{
				return true;
			}
		}
		if (Localization.mOldDictionary.Count > 0)
		{
			return true;
		}
		Localization.mOldDictionary.Clear();
		Localization.mDictionary.Clear();
		if (string.IsNullOrEmpty(value))
		{
			PlayerPrefs.DeleteKey("Language");
		}
		return false;
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x000141DC File Offset: 0x000123DC
	public static void Load(TextAsset asset)
	{
		ByteReader byteReader = new ByteReader(asset);
		Localization.Set(asset.name, byteReader.ReadDictionary());
	}

	// Token: 0x060002EA RID: 746 RVA: 0x00014204 File Offset: 0x00012404
	public static bool LoadCSV(TextAsset asset)
	{
		ByteReader byteReader = new ByteReader(asset);
		BetterList<string> betterList = byteReader.ReadCSV();
		if (betterList.size < 2)
		{
			return false;
		}
		betterList[0] = "KEY";
		if (!string.Equals(betterList[0], "KEY"))
		{
			Debug.LogError("Invalid localization CSV file. The first value is expected to be 'KEY', followed by language columns.\nInstead found '" + betterList[0] + "'", asset);
			return false;
		}
		Localization.mLanguages = new string[betterList.size - 1];
		for (int i = 0; i < Localization.mLanguages.Length; i++)
		{
			Localization.mLanguages[i] = betterList[i + 1];
		}
		Localization.mDictionary.Clear();
		while (betterList != null)
		{
			Localization.AddCSV(betterList);
			betterList = byteReader.ReadCSV();
		}
		return true;
	}

	// Token: 0x060002EB RID: 747 RVA: 0x000142CC File Offset: 0x000124CC
	private static bool SelectLanguage(string language)
	{
		Localization.mLanguageIndex = -1;
		if (Localization.mDictionary.Count == 0)
		{
			return false;
		}
		string[] array;
		if (Localization.mDictionary.TryGetValue("KEY", ref array))
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == language)
				{
					Localization.mOldDictionary.Clear();
					Localization.mLanguageIndex = i;
					Localization.mLanguage = language;
					PlayerPrefs.SetString("Language", Localization.mLanguage);
					UIRoot.Broadcast("OnLocalize");
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060002EC RID: 748 RVA: 0x0001435C File Offset: 0x0001255C
	private static void AddCSV(BetterList<string> values)
	{
		if (values.size < 2)
		{
			return;
		}
		string[] array = new string[values.size - 1];
		for (int i = 1; i < values.size; i++)
		{
			array[i - 1] = values[i];
		}
		Localization.mDictionary.Add(values[0], array);
	}

	// Token: 0x060002ED RID: 749 RVA: 0x000143BC File Offset: 0x000125BC
	public static void Set(string languageName, Dictionary<string, string> dictionary)
	{
		Localization.mLanguage = languageName;
		PlayerPrefs.SetString("Language", Localization.mLanguage);
		Localization.mOldDictionary = dictionary;
		Localization.localizationHasBeenSet = false;
		Localization.mLanguageIndex = -1;
		Localization.mLanguages = new string[]
		{
			languageName
		};
		UIRoot.Broadcast("OnLocalize");
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0001440C File Offset: 0x0001260C
	public static string Get(string key)
	{
		if (!Localization.localizationHasBeenSet)
		{
			Localization.language = PlayerPrefs.GetString("Language", "English");
		}
		string text = key + " Mobile";
		string[] array;
		string result;
		if (Localization.mLanguageIndex != -1 && Localization.mDictionary.TryGetValue(text, ref array))
		{
			if (Localization.mLanguageIndex < array.Length)
			{
				return array[Localization.mLanguageIndex];
			}
		}
		else if (Localization.mOldDictionary.TryGetValue(text, ref result))
		{
			return result;
		}
		if (Localization.mLanguageIndex != -1 && Localization.mDictionary.TryGetValue(key, ref array))
		{
			if (Localization.mLanguageIndex < array.Length)
			{
				return array[Localization.mLanguageIndex];
			}
		}
		else if (Localization.mOldDictionary.TryGetValue(key, ref result))
		{
			return result;
		}
		return key;
	}

	// Token: 0x060002EF RID: 751 RVA: 0x000144DC File Offset: 0x000126DC
	[Obsolete("Use Localization.Get instead")]
	public static string Localize(string key)
	{
		return Localization.Get(key);
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x000144E4 File Offset: 0x000126E4
	public static bool Exists(string key)
	{
		if (Localization.mLanguageIndex != -1)
		{
			return Localization.mDictionary.ContainsKey(key);
		}
		return Localization.mOldDictionary.ContainsKey(key);
	}

	// Token: 0x04000319 RID: 793
	public static bool localizationHasBeenSet = false;

	// Token: 0x0400031A RID: 794
	private static string[] mLanguages = null;

	// Token: 0x0400031B RID: 795
	private static Dictionary<string, string> mOldDictionary = new Dictionary<string, string>();

	// Token: 0x0400031C RID: 796
	private static Dictionary<string, string[]> mDictionary = new Dictionary<string, string[]>();

	// Token: 0x0400031D RID: 797
	private static int mLanguageIndex = -1;

	// Token: 0x0400031E RID: 798
	private static string mLanguage;
}
