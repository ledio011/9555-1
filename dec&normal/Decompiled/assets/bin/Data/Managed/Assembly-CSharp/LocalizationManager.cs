using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200013D RID: 317
public class LocalizationManager
{
	// Token: 0x06000D4C RID: 3404 RVA: 0x0005BC6C File Offset: 0x00059E6C
	public static bool ResourceLoadDictionary()
	{
		TextAsset textAsset = Resources.Load("LocalizationBase", typeof(TextAsset)) as TextAsset;
		SystemLanguage systemLanguage = Application.systemLanguage;
		string language = PlayerPrefs.GetString("Language", "English");
		language = "English";
		if (textAsset != null && LocalizationManager.LoadCSV(textAsset))
		{
			LocalizationManager.SelectLanguage(language);
			return true;
		}
		return false;
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0005BCD0 File Offset: 0x00059ED0
	public static bool BundleLoadDictionary()
	{
		TextAsset textAsset = BundleManager.LoadTable("Localization") as TextAsset;
		SystemLanguage systemLanguage = Application.systemLanguage;
		string text = PlayerPrefs.GetString("Language", "English");
		text = "English";
		Debug.Log(text + "lange");
		if (textAsset != null && LocalizationManager.LoadCSV(textAsset))
		{
			LocalizationManager.SelectLanguage(text);
			return true;
		}
		return false;
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0005BD3C File Offset: 0x00059F3C
	private static bool LoadAndSelect(string value)
	{
		if (!string.IsNullOrEmpty(value))
		{
			if (LocalizationManager.mDictionary.Count == 0 && !LocalizationManager.BundleLoadDictionary())
			{
				return false;
			}
			if (LocalizationManager.SelectLanguage(value))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0005BD80 File Offset: 0x00059F80
	public static bool LoadCSV(TextAsset asset)
	{
		ByteReader byteReader = new ByteReader(asset);
		BetterList<string> betterList = byteReader.ReadCSV();
		if (betterList.size < 2)
		{
			return false;
		}
		betterList[0] = "KEY";
		LocalizationManager.mDictionary.Clear();
		while (betterList != null)
		{
			LocalizationManager.AddCSV(betterList);
			betterList = byteReader.ReadCSV();
		}
		return true;
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x0005BDD8 File Offset: 0x00059FD8
	private static bool SelectLanguage(string language)
	{
		LocalizationManager.mLanguageIndex = -1;
		if (LocalizationManager.mDictionary.Count == 0)
		{
			return false;
		}
		string[] array;
		if (LocalizationManager.mDictionary.TryGetValue("KEY", ref array))
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == language)
				{
					LocalizationManager.mLanguageIndex = i;
					LocalizationManager.mLanguage = language;
					PlayerPrefs.SetString("Language", LocalizationManager.mLanguage);
					UIRoot.Broadcast("OnLocalize");
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x0005BE5C File Offset: 0x0005A05C
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
		if (LocalizationManager.mDictionary.ContainsKey(values[0]))
		{
			Debug.LogWarning(values[0]);
		}
		LocalizationManager.mDictionary.Add(values[0], array);
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x0005BEDC File Offset: 0x0005A0DC
	public static string Get(string key)
	{
		string[] array;
		if (LocalizationManager.mLanguageIndex != -1 && LocalizationManager.mDictionary.TryGetValue(key, ref array) && LocalizationManager.mLanguageIndex < array.Length)
		{
			return array[LocalizationManager.mLanguageIndex];
		}
		return key;
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x0005BF1C File Offset: 0x0005A11C
	[Obsolete("Use Localization.Get instead")]
	public static string Localize(string key)
	{
		return LocalizationManager.Get(key);
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x0005BF24 File Offset: 0x0005A124
	public static bool Exists(string key)
	{
		return LocalizationManager.mLanguageIndex != -1 && LocalizationManager.mDictionary.ContainsKey(key);
	}

	// Token: 0x04000B57 RID: 2903
	private static Dictionary<string, string[]> mDictionary = new Dictionary<string, string[]>();

	// Token: 0x04000B58 RID: 2904
	private static int mLanguageIndex = -1;

	// Token: 0x04000B59 RID: 2905
	private static string mLanguage;
}
