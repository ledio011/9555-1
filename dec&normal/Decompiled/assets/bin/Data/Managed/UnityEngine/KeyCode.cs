using System;

namespace UnityEngine
{
	// Token: 0x020000A0 RID: 160
	public enum KeyCode
	{
		// Token: 0x040001DD RID: 477
		None,
		// Token: 0x040001DE RID: 478
		Backspace = 8,
		// Token: 0x040001DF RID: 479
		Delete = 127,
		// Token: 0x040001E0 RID: 480
		Tab = 9,
		// Token: 0x040001E1 RID: 481
		Clear = 12,
		// Token: 0x040001E2 RID: 482
		Return,
		// Token: 0x040001E3 RID: 483
		Pause = 19,
		// Token: 0x040001E4 RID: 484
		Escape = 27,
		// Token: 0x040001E5 RID: 485
		Space = 32,
		// Token: 0x040001E6 RID: 486
		Keypad0 = 256,
		// Token: 0x040001E7 RID: 487
		Keypad1,
		// Token: 0x040001E8 RID: 488
		Keypad2,
		// Token: 0x040001E9 RID: 489
		Keypad3,
		// Token: 0x040001EA RID: 490
		Keypad4,
		// Token: 0x040001EB RID: 491
		Keypad5,
		// Token: 0x040001EC RID: 492
		Keypad6,
		// Token: 0x040001ED RID: 493
		Keypad7,
		// Token: 0x040001EE RID: 494
		Keypad8,
		// Token: 0x040001EF RID: 495
		Keypad9,
		// Token: 0x040001F0 RID: 496
		KeypadPeriod,
		// Token: 0x040001F1 RID: 497
		KeypadDivide,
		// Token: 0x040001F2 RID: 498
		KeypadMultiply,
		// Token: 0x040001F3 RID: 499
		KeypadMinus,
		// Token: 0x040001F4 RID: 500
		KeypadPlus,
		// Token: 0x040001F5 RID: 501
		KeypadEnter,
		// Token: 0x040001F6 RID: 502
		KeypadEquals,
		// Token: 0x040001F7 RID: 503
		UpArrow,
		// Token: 0x040001F8 RID: 504
		DownArrow,
		// Token: 0x040001F9 RID: 505
		RightArrow,
		// Token: 0x040001FA RID: 506
		LeftArrow,
		// Token: 0x040001FB RID: 507
		Insert,
		// Token: 0x040001FC RID: 508
		Home,
		// Token: 0x040001FD RID: 509
		End,
		// Token: 0x040001FE RID: 510
		PageUp,
		// Token: 0x040001FF RID: 511
		PageDown,
		// Token: 0x04000200 RID: 512
		F1,
		// Token: 0x04000201 RID: 513
		F2,
		// Token: 0x04000202 RID: 514
		F3,
		// Token: 0x04000203 RID: 515
		F4,
		// Token: 0x04000204 RID: 516
		F5,
		// Token: 0x04000205 RID: 517
		F6,
		// Token: 0x04000206 RID: 518
		F7,
		// Token: 0x04000207 RID: 519
		F8,
		// Token: 0x04000208 RID: 520
		F9,
		// Token: 0x04000209 RID: 521
		F10,
		// Token: 0x0400020A RID: 522
		F11,
		// Token: 0x0400020B RID: 523
		F12,
		// Token: 0x0400020C RID: 524
		F13,
		// Token: 0x0400020D RID: 525
		F14,
		// Token: 0x0400020E RID: 526
		F15,
		// Token: 0x0400020F RID: 527
		Alpha0 = 48,
		// Token: 0x04000210 RID: 528
		Alpha1,
		// Token: 0x04000211 RID: 529
		Alpha2,
		// Token: 0x04000212 RID: 530
		Alpha3,
		// Token: 0x04000213 RID: 531
		Alpha4,
		// Token: 0x04000214 RID: 532
		Alpha5,
		// Token: 0x04000215 RID: 533
		Alpha6,
		// Token: 0x04000216 RID: 534
		Alpha7,
		// Token: 0x04000217 RID: 535
		Alpha8,
		// Token: 0x04000218 RID: 536
		Alpha9,
		// Token: 0x04000219 RID: 537
		Exclaim = 33,
		// Token: 0x0400021A RID: 538
		DoubleQuote,
		// Token: 0x0400021B RID: 539
		Hash,
		// Token: 0x0400021C RID: 540
		Dollar,
		// Token: 0x0400021D RID: 541
		Ampersand = 38,
		// Token: 0x0400021E RID: 542
		Quote,
		// Token: 0x0400021F RID: 543
		LeftParen,
		// Token: 0x04000220 RID: 544
		RightParen,
		// Token: 0x04000221 RID: 545
		Asterisk,
		// Token: 0x04000222 RID: 546
		Plus,
		// Token: 0x04000223 RID: 547
		Comma,
		// Token: 0x04000224 RID: 548
		Minus,
		// Token: 0x04000225 RID: 549
		Period,
		// Token: 0x04000226 RID: 550
		Slash,
		// Token: 0x04000227 RID: 551
		Colon = 58,
		// Token: 0x04000228 RID: 552
		Semicolon,
		// Token: 0x04000229 RID: 553
		Less,
		// Token: 0x0400022A RID: 554
		Equals,
		// Token: 0x0400022B RID: 555
		Greater,
		// Token: 0x0400022C RID: 556
		Question,
		// Token: 0x0400022D RID: 557
		At,
		// Token: 0x0400022E RID: 558
		LeftBracket = 91,
		// Token: 0x0400022F RID: 559
		Backslash,
		// Token: 0x04000230 RID: 560
		RightBracket,
		// Token: 0x04000231 RID: 561
		Caret,
		// Token: 0x04000232 RID: 562
		Underscore,
		// Token: 0x04000233 RID: 563
		BackQuote,
		// Token: 0x04000234 RID: 564
		A,
		// Token: 0x04000235 RID: 565
		B,
		// Token: 0x04000236 RID: 566
		C,
		// Token: 0x04000237 RID: 567
		D,
		// Token: 0x04000238 RID: 568
		E,
		// Token: 0x04000239 RID: 569
		F,
		// Token: 0x0400023A RID: 570
		G,
		// Token: 0x0400023B RID: 571
		H,
		// Token: 0x0400023C RID: 572
		I,
		// Token: 0x0400023D RID: 573
		J,
		// Token: 0x0400023E RID: 574
		K,
		// Token: 0x0400023F RID: 575
		L,
		// Token: 0x04000240 RID: 576
		M,
		// Token: 0x04000241 RID: 577
		N,
		// Token: 0x04000242 RID: 578
		O,
		// Token: 0x04000243 RID: 579
		P,
		// Token: 0x04000244 RID: 580
		Q,
		// Token: 0x04000245 RID: 581
		R,
		// Token: 0x04000246 RID: 582
		S,
		// Token: 0x04000247 RID: 583
		T,
		// Token: 0x04000248 RID: 584
		U,
		// Token: 0x04000249 RID: 585
		V,
		// Token: 0x0400024A RID: 586
		W,
		// Token: 0x0400024B RID: 587
		X,
		// Token: 0x0400024C RID: 588
		Y,
		// Token: 0x0400024D RID: 589
		Z,
		// Token: 0x0400024E RID: 590
		Numlock = 300,
		// Token: 0x0400024F RID: 591
		CapsLock,
		// Token: 0x04000250 RID: 592
		ScrollLock,
		// Token: 0x04000251 RID: 593
		RightShift,
		// Token: 0x04000252 RID: 594
		LeftShift,
		// Token: 0x04000253 RID: 595
		RightControl,
		// Token: 0x04000254 RID: 596
		LeftControl,
		// Token: 0x04000255 RID: 597
		RightAlt,
		// Token: 0x04000256 RID: 598
		LeftAlt,
		// Token: 0x04000257 RID: 599
		LeftCommand = 310,
		// Token: 0x04000258 RID: 600
		LeftApple = 310,
		// Token: 0x04000259 RID: 601
		LeftWindows,
		// Token: 0x0400025A RID: 602
		RightCommand = 309,
		// Token: 0x0400025B RID: 603
		RightApple = 309,
		// Token: 0x0400025C RID: 604
		RightWindows = 312,
		// Token: 0x0400025D RID: 605
		AltGr,
		// Token: 0x0400025E RID: 606
		Help = 315,
		// Token: 0x0400025F RID: 607
		Print,
		// Token: 0x04000260 RID: 608
		SysReq,
		// Token: 0x04000261 RID: 609
		Break,
		// Token: 0x04000262 RID: 610
		Menu,
		// Token: 0x04000263 RID: 611
		Mouse0 = 323,
		// Token: 0x04000264 RID: 612
		Mouse1,
		// Token: 0x04000265 RID: 613
		Mouse2,
		// Token: 0x04000266 RID: 614
		Mouse3,
		// Token: 0x04000267 RID: 615
		Mouse4,
		// Token: 0x04000268 RID: 616
		Mouse5,
		// Token: 0x04000269 RID: 617
		Mouse6,
		// Token: 0x0400026A RID: 618
		JoystickButton0,
		// Token: 0x0400026B RID: 619
		JoystickButton1,
		// Token: 0x0400026C RID: 620
		JoystickButton2,
		// Token: 0x0400026D RID: 621
		JoystickButton3,
		// Token: 0x0400026E RID: 622
		JoystickButton4,
		// Token: 0x0400026F RID: 623
		JoystickButton5,
		// Token: 0x04000270 RID: 624
		JoystickButton6,
		// Token: 0x04000271 RID: 625
		JoystickButton7,
		// Token: 0x04000272 RID: 626
		JoystickButton8,
		// Token: 0x04000273 RID: 627
		JoystickButton9,
		// Token: 0x04000274 RID: 628
		JoystickButton10,
		// Token: 0x04000275 RID: 629
		JoystickButton11,
		// Token: 0x04000276 RID: 630
		JoystickButton12,
		// Token: 0x04000277 RID: 631
		JoystickButton13,
		// Token: 0x04000278 RID: 632
		JoystickButton14,
		// Token: 0x04000279 RID: 633
		JoystickButton15,
		// Token: 0x0400027A RID: 634
		JoystickButton16,
		// Token: 0x0400027B RID: 635
		JoystickButton17,
		// Token: 0x0400027C RID: 636
		JoystickButton18,
		// Token: 0x0400027D RID: 637
		JoystickButton19,
		// Token: 0x0400027E RID: 638
		Joystick1Button0,
		// Token: 0x0400027F RID: 639
		Joystick1Button1,
		// Token: 0x04000280 RID: 640
		Joystick1Button2,
		// Token: 0x04000281 RID: 641
		Joystick1Button3,
		// Token: 0x04000282 RID: 642
		Joystick1Button4,
		// Token: 0x04000283 RID: 643
		Joystick1Button5,
		// Token: 0x04000284 RID: 644
		Joystick1Button6,
		// Token: 0x04000285 RID: 645
		Joystick1Button7,
		// Token: 0x04000286 RID: 646
		Joystick1Button8,
		// Token: 0x04000287 RID: 647
		Joystick1Button9,
		// Token: 0x04000288 RID: 648
		Joystick1Button10,
		// Token: 0x04000289 RID: 649
		Joystick1Button11,
		// Token: 0x0400028A RID: 650
		Joystick1Button12,
		// Token: 0x0400028B RID: 651
		Joystick1Button13,
		// Token: 0x0400028C RID: 652
		Joystick1Button14,
		// Token: 0x0400028D RID: 653
		Joystick1Button15,
		// Token: 0x0400028E RID: 654
		Joystick1Button16,
		// Token: 0x0400028F RID: 655
		Joystick1Button17,
		// Token: 0x04000290 RID: 656
		Joystick1Button18,
		// Token: 0x04000291 RID: 657
		Joystick1Button19,
		// Token: 0x04000292 RID: 658
		Joystick2Button0,
		// Token: 0x04000293 RID: 659
		Joystick2Button1,
		// Token: 0x04000294 RID: 660
		Joystick2Button2,
		// Token: 0x04000295 RID: 661
		Joystick2Button3,
		// Token: 0x04000296 RID: 662
		Joystick2Button4,
		// Token: 0x04000297 RID: 663
		Joystick2Button5,
		// Token: 0x04000298 RID: 664
		Joystick2Button6,
		// Token: 0x04000299 RID: 665
		Joystick2Button7,
		// Token: 0x0400029A RID: 666
		Joystick2Button8,
		// Token: 0x0400029B RID: 667
		Joystick2Button9,
		// Token: 0x0400029C RID: 668
		Joystick2Button10,
		// Token: 0x0400029D RID: 669
		Joystick2Button11,
		// Token: 0x0400029E RID: 670
		Joystick2Button12,
		// Token: 0x0400029F RID: 671
		Joystick2Button13,
		// Token: 0x040002A0 RID: 672
		Joystick2Button14,
		// Token: 0x040002A1 RID: 673
		Joystick2Button15,
		// Token: 0x040002A2 RID: 674
		Joystick2Button16,
		// Token: 0x040002A3 RID: 675
		Joystick2Button17,
		// Token: 0x040002A4 RID: 676
		Joystick2Button18,
		// Token: 0x040002A5 RID: 677
		Joystick2Button19,
		// Token: 0x040002A6 RID: 678
		Joystick3Button0,
		// Token: 0x040002A7 RID: 679
		Joystick3Button1,
		// Token: 0x040002A8 RID: 680
		Joystick3Button2,
		// Token: 0x040002A9 RID: 681
		Joystick3Button3,
		// Token: 0x040002AA RID: 682
		Joystick3Button4,
		// Token: 0x040002AB RID: 683
		Joystick3Button5,
		// Token: 0x040002AC RID: 684
		Joystick3Button6,
		// Token: 0x040002AD RID: 685
		Joystick3Button7,
		// Token: 0x040002AE RID: 686
		Joystick3Button8,
		// Token: 0x040002AF RID: 687
		Joystick3Button9,
		// Token: 0x040002B0 RID: 688
		Joystick3Button10,
		// Token: 0x040002B1 RID: 689
		Joystick3Button11,
		// Token: 0x040002B2 RID: 690
		Joystick3Button12,
		// Token: 0x040002B3 RID: 691
		Joystick3Button13,
		// Token: 0x040002B4 RID: 692
		Joystick3Button14,
		// Token: 0x040002B5 RID: 693
		Joystick3Button15,
		// Token: 0x040002B6 RID: 694
		Joystick3Button16,
		// Token: 0x040002B7 RID: 695
		Joystick3Button17,
		// Token: 0x040002B8 RID: 696
		Joystick3Button18,
		// Token: 0x040002B9 RID: 697
		Joystick3Button19,
		// Token: 0x040002BA RID: 698
		Joystick4Button0,
		// Token: 0x040002BB RID: 699
		Joystick4Button1,
		// Token: 0x040002BC RID: 700
		Joystick4Button2,
		// Token: 0x040002BD RID: 701
		Joystick4Button3,
		// Token: 0x040002BE RID: 702
		Joystick4Button4,
		// Token: 0x040002BF RID: 703
		Joystick4Button5,
		// Token: 0x040002C0 RID: 704
		Joystick4Button6,
		// Token: 0x040002C1 RID: 705
		Joystick4Button7,
		// Token: 0x040002C2 RID: 706
		Joystick4Button8,
		// Token: 0x040002C3 RID: 707
		Joystick4Button9,
		// Token: 0x040002C4 RID: 708
		Joystick4Button10,
		// Token: 0x040002C5 RID: 709
		Joystick4Button11,
		// Token: 0x040002C6 RID: 710
		Joystick4Button12,
		// Token: 0x040002C7 RID: 711
		Joystick4Button13,
		// Token: 0x040002C8 RID: 712
		Joystick4Button14,
		// Token: 0x040002C9 RID: 713
		Joystick4Button15,
		// Token: 0x040002CA RID: 714
		Joystick4Button16,
		// Token: 0x040002CB RID: 715
		Joystick4Button17,
		// Token: 0x040002CC RID: 716
		Joystick4Button18,
		// Token: 0x040002CD RID: 717
		Joystick4Button19
	}
}
