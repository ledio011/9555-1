using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000061 RID: 97
	internal enum Category : ushort
	{
		// Token: 0x040008EC RID: 2284
		None,
		// Token: 0x040008ED RID: 2285
		Any,
		// Token: 0x040008EE RID: 2286
		AnySingleline,
		// Token: 0x040008EF RID: 2287
		Word,
		// Token: 0x040008F0 RID: 2288
		Digit,
		// Token: 0x040008F1 RID: 2289
		WhiteSpace,
		// Token: 0x040008F2 RID: 2290
		EcmaAny,
		// Token: 0x040008F3 RID: 2291
		EcmaAnySingleline,
		// Token: 0x040008F4 RID: 2292
		EcmaWord,
		// Token: 0x040008F5 RID: 2293
		EcmaDigit,
		// Token: 0x040008F6 RID: 2294
		EcmaWhiteSpace,
		// Token: 0x040008F7 RID: 2295
		UnicodeL,
		// Token: 0x040008F8 RID: 2296
		UnicodeM,
		// Token: 0x040008F9 RID: 2297
		UnicodeN,
		// Token: 0x040008FA RID: 2298
		UnicodeZ,
		// Token: 0x040008FB RID: 2299
		UnicodeP,
		// Token: 0x040008FC RID: 2300
		UnicodeS,
		// Token: 0x040008FD RID: 2301
		UnicodeC,
		// Token: 0x040008FE RID: 2302
		UnicodeLu,
		// Token: 0x040008FF RID: 2303
		UnicodeLl,
		// Token: 0x04000900 RID: 2304
		UnicodeLt,
		// Token: 0x04000901 RID: 2305
		UnicodeLm,
		// Token: 0x04000902 RID: 2306
		UnicodeLo,
		// Token: 0x04000903 RID: 2307
		UnicodeMn,
		// Token: 0x04000904 RID: 2308
		UnicodeMe,
		// Token: 0x04000905 RID: 2309
		UnicodeMc,
		// Token: 0x04000906 RID: 2310
		UnicodeNd,
		// Token: 0x04000907 RID: 2311
		UnicodeNl,
		// Token: 0x04000908 RID: 2312
		UnicodeNo,
		// Token: 0x04000909 RID: 2313
		UnicodeZs,
		// Token: 0x0400090A RID: 2314
		UnicodeZl,
		// Token: 0x0400090B RID: 2315
		UnicodeZp,
		// Token: 0x0400090C RID: 2316
		UnicodePd,
		// Token: 0x0400090D RID: 2317
		UnicodePs,
		// Token: 0x0400090E RID: 2318
		UnicodePi,
		// Token: 0x0400090F RID: 2319
		UnicodePe,
		// Token: 0x04000910 RID: 2320
		UnicodePf,
		// Token: 0x04000911 RID: 2321
		UnicodePc,
		// Token: 0x04000912 RID: 2322
		UnicodePo,
		// Token: 0x04000913 RID: 2323
		UnicodeSm,
		// Token: 0x04000914 RID: 2324
		UnicodeSc,
		// Token: 0x04000915 RID: 2325
		UnicodeSk,
		// Token: 0x04000916 RID: 2326
		UnicodeSo,
		// Token: 0x04000917 RID: 2327
		UnicodeCc,
		// Token: 0x04000918 RID: 2328
		UnicodeCf,
		// Token: 0x04000919 RID: 2329
		UnicodeCo,
		// Token: 0x0400091A RID: 2330
		UnicodeCs,
		// Token: 0x0400091B RID: 2331
		UnicodeCn,
		// Token: 0x0400091C RID: 2332
		UnicodeBasicLatin,
		// Token: 0x0400091D RID: 2333
		UnicodeLatin1Supplement,
		// Token: 0x0400091E RID: 2334
		UnicodeLatinExtendedA,
		// Token: 0x0400091F RID: 2335
		UnicodeLatinExtendedB,
		// Token: 0x04000920 RID: 2336
		UnicodeIPAExtensions,
		// Token: 0x04000921 RID: 2337
		UnicodeSpacingModifierLetters,
		// Token: 0x04000922 RID: 2338
		UnicodeCombiningDiacriticalMarks,
		// Token: 0x04000923 RID: 2339
		UnicodeGreek,
		// Token: 0x04000924 RID: 2340
		UnicodeCyrillic,
		// Token: 0x04000925 RID: 2341
		UnicodeArmenian,
		// Token: 0x04000926 RID: 2342
		UnicodeHebrew,
		// Token: 0x04000927 RID: 2343
		UnicodeArabic,
		// Token: 0x04000928 RID: 2344
		UnicodeSyriac,
		// Token: 0x04000929 RID: 2345
		UnicodeThaana,
		// Token: 0x0400092A RID: 2346
		UnicodeDevanagari,
		// Token: 0x0400092B RID: 2347
		UnicodeBengali,
		// Token: 0x0400092C RID: 2348
		UnicodeGurmukhi,
		// Token: 0x0400092D RID: 2349
		UnicodeGujarati,
		// Token: 0x0400092E RID: 2350
		UnicodeOriya,
		// Token: 0x0400092F RID: 2351
		UnicodeTamil,
		// Token: 0x04000930 RID: 2352
		UnicodeTelugu,
		// Token: 0x04000931 RID: 2353
		UnicodeKannada,
		// Token: 0x04000932 RID: 2354
		UnicodeMalayalam,
		// Token: 0x04000933 RID: 2355
		UnicodeSinhala,
		// Token: 0x04000934 RID: 2356
		UnicodeThai,
		// Token: 0x04000935 RID: 2357
		UnicodeLao,
		// Token: 0x04000936 RID: 2358
		UnicodeTibetan,
		// Token: 0x04000937 RID: 2359
		UnicodeMyanmar,
		// Token: 0x04000938 RID: 2360
		UnicodeGeorgian,
		// Token: 0x04000939 RID: 2361
		UnicodeHangulJamo,
		// Token: 0x0400093A RID: 2362
		UnicodeEthiopic,
		// Token: 0x0400093B RID: 2363
		UnicodeCherokee,
		// Token: 0x0400093C RID: 2364
		UnicodeUnifiedCanadianAboriginalSyllabics,
		// Token: 0x0400093D RID: 2365
		UnicodeOgham,
		// Token: 0x0400093E RID: 2366
		UnicodeRunic,
		// Token: 0x0400093F RID: 2367
		UnicodeKhmer,
		// Token: 0x04000940 RID: 2368
		UnicodeMongolian,
		// Token: 0x04000941 RID: 2369
		UnicodeLatinExtendedAdditional,
		// Token: 0x04000942 RID: 2370
		UnicodeGreekExtended,
		// Token: 0x04000943 RID: 2371
		UnicodeGeneralPunctuation,
		// Token: 0x04000944 RID: 2372
		UnicodeSuperscriptsandSubscripts,
		// Token: 0x04000945 RID: 2373
		UnicodeCurrencySymbols,
		// Token: 0x04000946 RID: 2374
		UnicodeCombiningMarksforSymbols,
		// Token: 0x04000947 RID: 2375
		UnicodeLetterlikeSymbols,
		// Token: 0x04000948 RID: 2376
		UnicodeNumberForms,
		// Token: 0x04000949 RID: 2377
		UnicodeArrows,
		// Token: 0x0400094A RID: 2378
		UnicodeMathematicalOperators,
		// Token: 0x0400094B RID: 2379
		UnicodeMiscellaneousTechnical,
		// Token: 0x0400094C RID: 2380
		UnicodeControlPictures,
		// Token: 0x0400094D RID: 2381
		UnicodeOpticalCharacterRecognition,
		// Token: 0x0400094E RID: 2382
		UnicodeEnclosedAlphanumerics,
		// Token: 0x0400094F RID: 2383
		UnicodeBoxDrawing,
		// Token: 0x04000950 RID: 2384
		UnicodeBlockElements,
		// Token: 0x04000951 RID: 2385
		UnicodeGeometricShapes,
		// Token: 0x04000952 RID: 2386
		UnicodeMiscellaneousSymbols,
		// Token: 0x04000953 RID: 2387
		UnicodeDingbats,
		// Token: 0x04000954 RID: 2388
		UnicodeBraillePatterns,
		// Token: 0x04000955 RID: 2389
		UnicodeCJKRadicalsSupplement,
		// Token: 0x04000956 RID: 2390
		UnicodeKangxiRadicals,
		// Token: 0x04000957 RID: 2391
		UnicodeIdeographicDescriptionCharacters,
		// Token: 0x04000958 RID: 2392
		UnicodeCJKSymbolsandPunctuation,
		// Token: 0x04000959 RID: 2393
		UnicodeHiragana,
		// Token: 0x0400095A RID: 2394
		UnicodeKatakana,
		// Token: 0x0400095B RID: 2395
		UnicodeBopomofo,
		// Token: 0x0400095C RID: 2396
		UnicodeHangulCompatibilityJamo,
		// Token: 0x0400095D RID: 2397
		UnicodeKanbun,
		// Token: 0x0400095E RID: 2398
		UnicodeBopomofoExtended,
		// Token: 0x0400095F RID: 2399
		UnicodeEnclosedCJKLettersandMonths,
		// Token: 0x04000960 RID: 2400
		UnicodeCJKCompatibility,
		// Token: 0x04000961 RID: 2401
		UnicodeCJKUnifiedIdeographsExtensionA,
		// Token: 0x04000962 RID: 2402
		UnicodeCJKUnifiedIdeographs,
		// Token: 0x04000963 RID: 2403
		UnicodeYiSyllables,
		// Token: 0x04000964 RID: 2404
		UnicodeYiRadicals,
		// Token: 0x04000965 RID: 2405
		UnicodeHangulSyllables,
		// Token: 0x04000966 RID: 2406
		UnicodeHighSurrogates,
		// Token: 0x04000967 RID: 2407
		UnicodeHighPrivateUseSurrogates,
		// Token: 0x04000968 RID: 2408
		UnicodeLowSurrogates,
		// Token: 0x04000969 RID: 2409
		UnicodePrivateUse,
		// Token: 0x0400096A RID: 2410
		UnicodeCJKCompatibilityIdeographs,
		// Token: 0x0400096B RID: 2411
		UnicodeAlphabeticPresentationForms,
		// Token: 0x0400096C RID: 2412
		UnicodeArabicPresentationFormsA,
		// Token: 0x0400096D RID: 2413
		UnicodeCombiningHalfMarks,
		// Token: 0x0400096E RID: 2414
		UnicodeCJKCompatibilityForms,
		// Token: 0x0400096F RID: 2415
		UnicodeSmallFormVariants,
		// Token: 0x04000970 RID: 2416
		UnicodeArabicPresentationFormsB,
		// Token: 0x04000971 RID: 2417
		UnicodeSpecials,
		// Token: 0x04000972 RID: 2418
		UnicodeHalfwidthandFullwidthForms,
		// Token: 0x04000973 RID: 2419
		UnicodeOldItalic,
		// Token: 0x04000974 RID: 2420
		UnicodeGothic,
		// Token: 0x04000975 RID: 2421
		UnicodeDeseret,
		// Token: 0x04000976 RID: 2422
		UnicodeByzantineMusicalSymbols,
		// Token: 0x04000977 RID: 2423
		UnicodeMusicalSymbols,
		// Token: 0x04000978 RID: 2424
		UnicodeMathematicalAlphanumericSymbols,
		// Token: 0x04000979 RID: 2425
		UnicodeCJKUnifiedIdeographsExtensionB,
		// Token: 0x0400097A RID: 2426
		UnicodeCJKCompatibilityIdeographsSupplement,
		// Token: 0x0400097B RID: 2427
		UnicodeTags,
		// Token: 0x0400097C RID: 2428
		LastValue
	}
}
