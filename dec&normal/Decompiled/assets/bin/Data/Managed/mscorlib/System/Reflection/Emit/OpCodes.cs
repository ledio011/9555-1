using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001B4 RID: 436
	[ComVisible(true)]
	public class OpCodes
	{
		// Token: 0x04000726 RID: 1830
		public static readonly OpCode Nop = new OpCode(1179903, 84215041);

		// Token: 0x04000727 RID: 1831
		public static readonly OpCode Break = new OpCode(1180159, 17106177);

		// Token: 0x04000728 RID: 1832
		public static readonly OpCode Ldarg_0 = new OpCode(1245951, 84214017);

		// Token: 0x04000729 RID: 1833
		public static readonly OpCode Ldarg_1 = new OpCode(1246207, 84214017);

		// Token: 0x0400072A RID: 1834
		public static readonly OpCode Ldarg_2 = new OpCode(1246463, 84214017);

		// Token: 0x0400072B RID: 1835
		public static readonly OpCode Ldarg_3 = new OpCode(1246719, 84214017);

		// Token: 0x0400072C RID: 1836
		public static readonly OpCode Ldloc_0 = new OpCode(1246975, 84214017);

		// Token: 0x0400072D RID: 1837
		public static readonly OpCode Ldloc_1 = new OpCode(1247231, 84214017);

		// Token: 0x0400072E RID: 1838
		public static readonly OpCode Ldloc_2 = new OpCode(1247487, 84214017);

		// Token: 0x0400072F RID: 1839
		public static readonly OpCode Ldloc_3 = new OpCode(1247743, 84214017);

		// Token: 0x04000730 RID: 1840
		public static readonly OpCode Stloc_0 = new OpCode(17959679, 84214017);

		// Token: 0x04000731 RID: 1841
		public static readonly OpCode Stloc_1 = new OpCode(17959935, 84214017);

		// Token: 0x04000732 RID: 1842
		public static readonly OpCode Stloc_2 = new OpCode(17960191, 84214017);

		// Token: 0x04000733 RID: 1843
		public static readonly OpCode Stloc_3 = new OpCode(17960447, 84214017);

		// Token: 0x04000734 RID: 1844
		public static readonly OpCode Ldarg_S = new OpCode(1249023, 85065985);

		// Token: 0x04000735 RID: 1845
		public static readonly OpCode Ldarga_S = new OpCode(1380351, 85065985);

		// Token: 0x04000736 RID: 1846
		public static readonly OpCode Starg_S = new OpCode(17961215, 85065985);

		// Token: 0x04000737 RID: 1847
		public static readonly OpCode Ldloc_S = new OpCode(1249791, 85065985);

		// Token: 0x04000738 RID: 1848
		public static readonly OpCode Ldloca_S = new OpCode(1381119, 85065985);

		// Token: 0x04000739 RID: 1849
		public static readonly OpCode Stloc_S = new OpCode(17961983, 85065985);

		// Token: 0x0400073A RID: 1850
		public static readonly OpCode Ldnull = new OpCode(1643775, 84215041);

		// Token: 0x0400073B RID: 1851
		public static readonly OpCode Ldc_I4_M1 = new OpCode(1381887, 84214017);

		// Token: 0x0400073C RID: 1852
		public static readonly OpCode Ldc_I4_0 = new OpCode(1382143, 84214017);

		// Token: 0x0400073D RID: 1853
		public static readonly OpCode Ldc_I4_1 = new OpCode(1382399, 84214017);

		// Token: 0x0400073E RID: 1854
		public static readonly OpCode Ldc_I4_2 = new OpCode(1382655, 84214017);

		// Token: 0x0400073F RID: 1855
		public static readonly OpCode Ldc_I4_3 = new OpCode(1382911, 84214017);

		// Token: 0x04000740 RID: 1856
		public static readonly OpCode Ldc_I4_4 = new OpCode(1383167, 84214017);

		// Token: 0x04000741 RID: 1857
		public static readonly OpCode Ldc_I4_5 = new OpCode(1383423, 84214017);

		// Token: 0x04000742 RID: 1858
		public static readonly OpCode Ldc_I4_6 = new OpCode(1383679, 84214017);

		// Token: 0x04000743 RID: 1859
		public static readonly OpCode Ldc_I4_7 = new OpCode(1383935, 84214017);

		// Token: 0x04000744 RID: 1860
		public static readonly OpCode Ldc_I4_8 = new OpCode(1384191, 84214017);

		// Token: 0x04000745 RID: 1861
		public static readonly OpCode Ldc_I4_S = new OpCode(1384447, 84934913);

		// Token: 0x04000746 RID: 1862
		public static readonly OpCode Ldc_I4 = new OpCode(1384703, 84018433);

		// Token: 0x04000747 RID: 1863
		public static readonly OpCode Ldc_I8 = new OpCode(1450495, 84083969);

		// Token: 0x04000748 RID: 1864
		public static readonly OpCode Ldc_R4 = new OpCode(1516287, 85001473);

		// Token: 0x04000749 RID: 1865
		public static readonly OpCode Ldc_R8 = new OpCode(1582079, 84346113);

		// Token: 0x0400074A RID: 1866
		public static readonly OpCode Dup = new OpCode(18097663, 84215041);

		// Token: 0x0400074B RID: 1867
		public static readonly OpCode Pop = new OpCode(17966847, 84215041);

		// Token: 0x0400074C RID: 1868
		public static readonly OpCode Jmp = new OpCode(1189887, 33817857);

		// Token: 0x0400074D RID: 1869
		public static readonly OpCode Call = new OpCode(437987583, 33817857);

		// Token: 0x0400074E RID: 1870
		public static readonly OpCode Calli = new OpCode(437987839, 34145537);

		// Token: 0x0400074F RID: 1871
		public static readonly OpCode Ret = new OpCode(437398271, 117769473);

		// Token: 0x04000750 RID: 1872
		public static readonly OpCode Br_S = new OpCode(1190911, 983297);

		// Token: 0x04000751 RID: 1873
		public static readonly OpCode Brfalse_S = new OpCode(51522815, 51314945);

		// Token: 0x04000752 RID: 1874
		public static readonly OpCode Brtrue_S = new OpCode(51523071, 51314945);

		// Token: 0x04000753 RID: 1875
		public static readonly OpCode Beq_S = new OpCode(34746111, 51314945);

		// Token: 0x04000754 RID: 1876
		public static readonly OpCode Bge_S = new OpCode(34746367, 51314945);

		// Token: 0x04000755 RID: 1877
		public static readonly OpCode Bgt_S = new OpCode(34746623, 51314945);

		// Token: 0x04000756 RID: 1878
		public static readonly OpCode Ble_S = new OpCode(34746879, 51314945);

		// Token: 0x04000757 RID: 1879
		public static readonly OpCode Blt_S = new OpCode(34747135, 51314945);

		// Token: 0x04000758 RID: 1880
		public static readonly OpCode Bne_Un_S = new OpCode(34747391, 51314945);

		// Token: 0x04000759 RID: 1881
		public static readonly OpCode Bge_Un_S = new OpCode(34747647, 51314945);

		// Token: 0x0400075A RID: 1882
		public static readonly OpCode Bgt_Un_S = new OpCode(34747903, 51314945);

		// Token: 0x0400075B RID: 1883
		public static readonly OpCode Ble_Un_S = new OpCode(34748159, 51314945);

		// Token: 0x0400075C RID: 1884
		public static readonly OpCode Blt_Un_S = new OpCode(34748415, 51314945);

		// Token: 0x0400075D RID: 1885
		public static readonly OpCode Br = new OpCode(1194239, 1281);

		// Token: 0x0400075E RID: 1886
		public static readonly OpCode Brfalse = new OpCode(51526143, 50332929);

		// Token: 0x0400075F RID: 1887
		public static readonly OpCode Brtrue = new OpCode(51526399, 50332929);

		// Token: 0x04000760 RID: 1888
		public static readonly OpCode Beq = new OpCode(34749439, 50331905);

		// Token: 0x04000761 RID: 1889
		public static readonly OpCode Bge = new OpCode(34749695, 50331905);

		// Token: 0x04000762 RID: 1890
		public static readonly OpCode Bgt = new OpCode(34749951, 50331905);

		// Token: 0x04000763 RID: 1891
		public static readonly OpCode Ble = new OpCode(34750207, 50331905);

		// Token: 0x04000764 RID: 1892
		public static readonly OpCode Blt = new OpCode(34750463, 50331905);

		// Token: 0x04000765 RID: 1893
		public static readonly OpCode Bne_Un = new OpCode(34750719, 50331905);

		// Token: 0x04000766 RID: 1894
		public static readonly OpCode Bge_Un = new OpCode(34750975, 50331905);

		// Token: 0x04000767 RID: 1895
		public static readonly OpCode Bgt_Un = new OpCode(34751231, 50331905);

		// Token: 0x04000768 RID: 1896
		public static readonly OpCode Ble_Un = new OpCode(34751487, 50331905);

		// Token: 0x04000769 RID: 1897
		public static readonly OpCode Blt_Un = new OpCode(34751743, 50331905);

		// Token: 0x0400076A RID: 1898
		public static readonly OpCode Switch = new OpCode(51529215, 51053825);

		// Token: 0x0400076B RID: 1899
		public static readonly OpCode Ldind_I1 = new OpCode(51726079, 84215041);

		// Token: 0x0400076C RID: 1900
		public static readonly OpCode Ldind_U1 = new OpCode(51726335, 84215041);

		// Token: 0x0400076D RID: 1901
		public static readonly OpCode Ldind_I2 = new OpCode(51726591, 84215041);

		// Token: 0x0400076E RID: 1902
		public static readonly OpCode Ldind_U2 = new OpCode(51726847, 84215041);

		// Token: 0x0400076F RID: 1903
		public static readonly OpCode Ldind_I4 = new OpCode(51727103, 84215041);

		// Token: 0x04000770 RID: 1904
		public static readonly OpCode Ldind_U4 = new OpCode(51727359, 84215041);

		// Token: 0x04000771 RID: 1905
		public static readonly OpCode Ldind_I8 = new OpCode(51793151, 84215041);

		// Token: 0x04000772 RID: 1906
		public static readonly OpCode Ldind_I = new OpCode(51727871, 84215041);

		// Token: 0x04000773 RID: 1907
		public static readonly OpCode Ldind_R4 = new OpCode(51859199, 84215041);

		// Token: 0x04000774 RID: 1908
		public static readonly OpCode Ldind_R8 = new OpCode(51924991, 84215041);

		// Token: 0x04000775 RID: 1909
		public static readonly OpCode Ldind_Ref = new OpCode(51990783, 84215041);

		// Token: 0x04000776 RID: 1910
		public static readonly OpCode Stind_Ref = new OpCode(85086719, 84215041);

		// Token: 0x04000777 RID: 1911
		public static readonly OpCode Stind_I1 = new OpCode(85086975, 84215041);

		// Token: 0x04000778 RID: 1912
		public static readonly OpCode Stind_I2 = new OpCode(85087231, 84215041);

		// Token: 0x04000779 RID: 1913
		public static readonly OpCode Stind_I4 = new OpCode(85087487, 84215041);

		// Token: 0x0400077A RID: 1914
		public static readonly OpCode Stind_I8 = new OpCode(101864959, 84215041);

		// Token: 0x0400077B RID: 1915
		public static readonly OpCode Stind_R4 = new OpCode(135419647, 84215041);

		// Token: 0x0400077C RID: 1916
		public static readonly OpCode Stind_R8 = new OpCode(152197119, 84215041);

		// Token: 0x0400077D RID: 1917
		public static readonly OpCode Add = new OpCode(34822399, 84215041);

		// Token: 0x0400077E RID: 1918
		public static readonly OpCode Sub = new OpCode(34822655, 84215041);

		// Token: 0x0400077F RID: 1919
		public static readonly OpCode Mul = new OpCode(34822911, 84215041);

		// Token: 0x04000780 RID: 1920
		public static readonly OpCode Div = new OpCode(34823167, 84215041);

		// Token: 0x04000781 RID: 1921
		public static readonly OpCode Div_Un = new OpCode(34823423, 84215041);

		// Token: 0x04000782 RID: 1922
		public static readonly OpCode Rem = new OpCode(34823679, 84215041);

		// Token: 0x04000783 RID: 1923
		public static readonly OpCode Rem_Un = new OpCode(34823935, 84215041);

		// Token: 0x04000784 RID: 1924
		public static readonly OpCode And = new OpCode(34824191, 84215041);

		// Token: 0x04000785 RID: 1925
		public static readonly OpCode Or = new OpCode(34824447, 84215041);

		// Token: 0x04000786 RID: 1926
		public static readonly OpCode Xor = new OpCode(34824703, 84215041);

		// Token: 0x04000787 RID: 1927
		public static readonly OpCode Shl = new OpCode(34824959, 84215041);

		// Token: 0x04000788 RID: 1928
		public static readonly OpCode Shr = new OpCode(34825215, 84215041);

		// Token: 0x04000789 RID: 1929
		public static readonly OpCode Shr_Un = new OpCode(34825471, 84215041);

		// Token: 0x0400078A RID: 1930
		public static readonly OpCode Neg = new OpCode(18048511, 84215041);

		// Token: 0x0400078B RID: 1931
		public static readonly OpCode Not = new OpCode(18048767, 84215041);

		// Token: 0x0400078C RID: 1932
		public static readonly OpCode Conv_I1 = new OpCode(18180095, 84215041);

		// Token: 0x0400078D RID: 1933
		public static readonly OpCode Conv_I2 = new OpCode(18180351, 84215041);

		// Token: 0x0400078E RID: 1934
		public static readonly OpCode Conv_I4 = new OpCode(18180607, 84215041);

		// Token: 0x0400078F RID: 1935
		public static readonly OpCode Conv_I8 = new OpCode(18246399, 84215041);

		// Token: 0x04000790 RID: 1936
		public static readonly OpCode Conv_R4 = new OpCode(18312191, 84215041);

		// Token: 0x04000791 RID: 1937
		public static readonly OpCode Conv_R8 = new OpCode(18377983, 84215041);

		// Token: 0x04000792 RID: 1938
		public static readonly OpCode Conv_U4 = new OpCode(18181631, 84215041);

		// Token: 0x04000793 RID: 1939
		public static readonly OpCode Conv_U8 = new OpCode(18247423, 84215041);

		// Token: 0x04000794 RID: 1940
		public static readonly OpCode Callvirt = new OpCode(438005759, 33817345);

		// Token: 0x04000795 RID: 1941
		public static readonly OpCode Cpobj = new OpCode(85094655, 84738817);

		// Token: 0x04000796 RID: 1942
		public static readonly OpCode Ldobj = new OpCode(51606015, 84738817);

		// Token: 0x04000797 RID: 1943
		public static readonly OpCode Ldstr = new OpCode(1667839, 84542209);

		// Token: 0x04000798 RID: 1944
		public static readonly OpCode Newobj = new OpCode(437875711, 33817345);

		// Token: 0x04000799 RID: 1945
		[ComVisible(true)]
		public static readonly OpCode Castclass = new OpCode(169440511, 84738817);

		// Token: 0x0400079A RID: 1946
		public static readonly OpCode Isinst = new OpCode(169178623, 84738817);

		// Token: 0x0400079B RID: 1947
		public static readonly OpCode Conv_R_Un = new OpCode(18380543, 84215041);

		// Token: 0x0400079C RID: 1948
		public static readonly OpCode Unbox = new OpCode(169179647, 84739329);

		// Token: 0x0400079D RID: 1949
		public static readonly OpCode Throw = new OpCode(168983295, 134546177);

		// Token: 0x0400079E RID: 1950
		public static readonly OpCode Ldfld = new OpCode(169049087, 83952385);

		// Token: 0x0400079F RID: 1951
		public static readonly OpCode Ldflda = new OpCode(169180415, 83952385);

		// Token: 0x040007A0 RID: 1952
		public static readonly OpCode Stfld = new OpCode(185761279, 83952385);

		// Token: 0x040007A1 RID: 1953
		public static readonly OpCode Ldsfld = new OpCode(1277695, 83952385);

		// Token: 0x040007A2 RID: 1954
		public static readonly OpCode Ldsflda = new OpCode(1409023, 83952385);

		// Token: 0x040007A3 RID: 1955
		public static readonly OpCode Stsfld = new OpCode(17989887, 83952385);

		// Token: 0x040007A4 RID: 1956
		public static readonly OpCode Stobj = new OpCode(68321791, 84739329);

		// Token: 0x040007A5 RID: 1957
		public static readonly OpCode Conv_Ovf_I1_Un = new OpCode(18187007, 84215041);

		// Token: 0x040007A6 RID: 1958
		public static readonly OpCode Conv_Ovf_I2_Un = new OpCode(18187263, 84215041);

		// Token: 0x040007A7 RID: 1959
		public static readonly OpCode Conv_Ovf_I4_Un = new OpCode(18187519, 84215041);

		// Token: 0x040007A8 RID: 1960
		public static readonly OpCode Conv_Ovf_I8_Un = new OpCode(18253311, 84215041);

		// Token: 0x040007A9 RID: 1961
		public static readonly OpCode Conv_Ovf_U1_Un = new OpCode(18188031, 84215041);

		// Token: 0x040007AA RID: 1962
		public static readonly OpCode Conv_Ovf_U2_Un = new OpCode(18188287, 84215041);

		// Token: 0x040007AB RID: 1963
		public static readonly OpCode Conv_Ovf_U4_Un = new OpCode(18188543, 84215041);

		// Token: 0x040007AC RID: 1964
		public static readonly OpCode Conv_Ovf_U8_Un = new OpCode(18254335, 84215041);

		// Token: 0x040007AD RID: 1965
		public static readonly OpCode Conv_Ovf_I_Un = new OpCode(18189055, 84215041);

		// Token: 0x040007AE RID: 1966
		public static readonly OpCode Conv_Ovf_U_Un = new OpCode(18189311, 84215041);

		// Token: 0x040007AF RID: 1967
		public static readonly OpCode Box = new OpCode(18451711, 84739329);

		// Token: 0x040007B0 RID: 1968
		public static readonly OpCode Newarr = new OpCode(52006399, 84738817);

		// Token: 0x040007B1 RID: 1969
		public static readonly OpCode Ldlen = new OpCode(169185023, 84214529);

		// Token: 0x040007B2 RID: 1970
		public static readonly OpCode Ldelema = new OpCode(202739711, 84738817);

		// Token: 0x040007B3 RID: 1971
		public static readonly OpCode Ldelem_I1 = new OpCode(202739967, 84214529);

		// Token: 0x040007B4 RID: 1972
		public static readonly OpCode Ldelem_U1 = new OpCode(202740223, 84214529);

		// Token: 0x040007B5 RID: 1973
		public static readonly OpCode Ldelem_I2 = new OpCode(202740479, 84214529);

		// Token: 0x040007B6 RID: 1974
		public static readonly OpCode Ldelem_U2 = new OpCode(202740735, 84214529);

		// Token: 0x040007B7 RID: 1975
		public static readonly OpCode Ldelem_I4 = new OpCode(202740991, 84214529);

		// Token: 0x040007B8 RID: 1976
		public static readonly OpCode Ldelem_U4 = new OpCode(202741247, 84214529);

		// Token: 0x040007B9 RID: 1977
		public static readonly OpCode Ldelem_I8 = new OpCode(202807039, 84214529);

		// Token: 0x040007BA RID: 1978
		public static readonly OpCode Ldelem_I = new OpCode(202741759, 84214529);

		// Token: 0x040007BB RID: 1979
		public static readonly OpCode Ldelem_R4 = new OpCode(202873087, 84214529);

		// Token: 0x040007BC RID: 1980
		public static readonly OpCode Ldelem_R8 = new OpCode(202938879, 84214529);

		// Token: 0x040007BD RID: 1981
		public static readonly OpCode Ldelem_Ref = new OpCode(203004671, 84214529);

		// Token: 0x040007BE RID: 1982
		public static readonly OpCode Stelem_I = new OpCode(219323391, 84214529);

		// Token: 0x040007BF RID: 1983
		public static readonly OpCode Stelem_I1 = new OpCode(219323647, 84214529);

		// Token: 0x040007C0 RID: 1984
		public static readonly OpCode Stelem_I2 = new OpCode(219323903, 84214529);

		// Token: 0x040007C1 RID: 1985
		public static readonly OpCode Stelem_I4 = new OpCode(219324159, 84214529);

		// Token: 0x040007C2 RID: 1986
		public static readonly OpCode Stelem_I8 = new OpCode(236101631, 84214529);

		// Token: 0x040007C3 RID: 1987
		public static readonly OpCode Stelem_R4 = new OpCode(252879103, 84214529);

		// Token: 0x040007C4 RID: 1988
		public static readonly OpCode Stelem_R8 = new OpCode(269656575, 84214529);

		// Token: 0x040007C5 RID: 1989
		public static readonly OpCode Stelem_Ref = new OpCode(286434047, 84214529);

		// Token: 0x040007C6 RID: 1990
		public static readonly OpCode Ldelem = new OpCode(202613759, 84738817);

		// Token: 0x040007C7 RID: 1991
		public static readonly OpCode Stelem = new OpCode(470983935, 84738817);

		// Token: 0x040007C8 RID: 1992
		public static readonly OpCode Unbox_Any = new OpCode(169059839, 84738817);

		// Token: 0x040007C9 RID: 1993
		public static readonly OpCode Conv_Ovf_I1 = new OpCode(18199551, 84215041);

		// Token: 0x040007CA RID: 1994
		public static readonly OpCode Conv_Ovf_U1 = new OpCode(18199807, 84215041);

		// Token: 0x040007CB RID: 1995
		public static readonly OpCode Conv_Ovf_I2 = new OpCode(18200063, 84215041);

		// Token: 0x040007CC RID: 1996
		public static readonly OpCode Conv_Ovf_U2 = new OpCode(18200319, 84215041);

		// Token: 0x040007CD RID: 1997
		public static readonly OpCode Conv_Ovf_I4 = new OpCode(18200575, 84215041);

		// Token: 0x040007CE RID: 1998
		public static readonly OpCode Conv_Ovf_U4 = new OpCode(18200831, 84215041);

		// Token: 0x040007CF RID: 1999
		public static readonly OpCode Conv_Ovf_I8 = new OpCode(18266623, 84215041);

		// Token: 0x040007D0 RID: 2000
		public static readonly OpCode Conv_Ovf_U8 = new OpCode(18266879, 84215041);

		// Token: 0x040007D1 RID: 2001
		public static readonly OpCode Refanyval = new OpCode(18203391, 84739329);

		// Token: 0x040007D2 RID: 2002
		public static readonly OpCode Ckfinite = new OpCode(18400255, 84215041);

		// Token: 0x040007D3 RID: 2003
		public static readonly OpCode Mkrefany = new OpCode(51627775, 84739329);

		// Token: 0x040007D4 RID: 2004
		public static readonly OpCode Ldtoken = new OpCode(1429759, 84673793);

		// Token: 0x040007D5 RID: 2005
		public static readonly OpCode Conv_U2 = new OpCode(18207231, 84215041);

		// Token: 0x040007D6 RID: 2006
		public static readonly OpCode Conv_U1 = new OpCode(18207487, 84215041);

		// Token: 0x040007D7 RID: 2007
		public static readonly OpCode Conv_I = new OpCode(18207743, 84215041);

		// Token: 0x040007D8 RID: 2008
		public static readonly OpCode Conv_Ovf_I = new OpCode(18207999, 84215041);

		// Token: 0x040007D9 RID: 2009
		public static readonly OpCode Conv_Ovf_U = new OpCode(18208255, 84215041);

		// Token: 0x040007DA RID: 2010
		public static readonly OpCode Add_Ovf = new OpCode(34854655, 84215041);

		// Token: 0x040007DB RID: 2011
		public static readonly OpCode Add_Ovf_Un = new OpCode(34854911, 84215041);

		// Token: 0x040007DC RID: 2012
		public static readonly OpCode Mul_Ovf = new OpCode(34855167, 84215041);

		// Token: 0x040007DD RID: 2013
		public static readonly OpCode Mul_Ovf_Un = new OpCode(34855423, 84215041);

		// Token: 0x040007DE RID: 2014
		public static readonly OpCode Sub_Ovf = new OpCode(34855679, 84215041);

		// Token: 0x040007DF RID: 2015
		public static readonly OpCode Sub_Ovf_Un = new OpCode(34855935, 84215041);

		// Token: 0x040007E0 RID: 2016
		public static readonly OpCode Endfinally = new OpCode(1236223, 117769473);

		// Token: 0x040007E1 RID: 2017
		public static readonly OpCode Leave = new OpCode(1236479, 1281);

		// Token: 0x040007E2 RID: 2018
		public static readonly OpCode Leave_S = new OpCode(1236735, 984321);

		// Token: 0x040007E3 RID: 2019
		public static readonly OpCode Stind_I = new OpCode(85123071, 84215041);

		// Token: 0x040007E4 RID: 2020
		public static readonly OpCode Conv_U = new OpCode(18211071, 84215041);

		// Token: 0x040007E5 RID: 2021
		public static readonly OpCode Prefix7 = new OpCode(1243391, 67437057);

		// Token: 0x040007E6 RID: 2022
		public static readonly OpCode Prefix6 = new OpCode(1243647, 67437057);

		// Token: 0x040007E7 RID: 2023
		public static readonly OpCode Prefix5 = new OpCode(1243903, 67437057);

		// Token: 0x040007E8 RID: 2024
		public static readonly OpCode Prefix4 = new OpCode(1244159, 67437057);

		// Token: 0x040007E9 RID: 2025
		public static readonly OpCode Prefix3 = new OpCode(1244415, 67437057);

		// Token: 0x040007EA RID: 2026
		public static readonly OpCode Prefix2 = new OpCode(1244671, 67437057);

		// Token: 0x040007EB RID: 2027
		public static readonly OpCode Prefix1 = new OpCode(1244927, 67437057);

		// Token: 0x040007EC RID: 2028
		public static readonly OpCode Prefixref = new OpCode(1245183, 67437057);

		// Token: 0x040007ED RID: 2029
		public static readonly OpCode Arglist = new OpCode(1376510, 84215042);

		// Token: 0x040007EE RID: 2030
		public static readonly OpCode Ceq = new OpCode(34931198, 84215042);

		// Token: 0x040007EF RID: 2031
		public static readonly OpCode Cgt = new OpCode(34931454, 84215042);

		// Token: 0x040007F0 RID: 2032
		public static readonly OpCode Cgt_Un = new OpCode(34931710, 84215042);

		// Token: 0x040007F1 RID: 2033
		public static readonly OpCode Clt = new OpCode(34931966, 84215042);

		// Token: 0x040007F2 RID: 2034
		public static readonly OpCode Clt_Un = new OpCode(34932222, 84215042);

		// Token: 0x040007F3 RID: 2035
		public static readonly OpCode Ldftn = new OpCode(1378046, 84149506);

		// Token: 0x040007F4 RID: 2036
		public static readonly OpCode Ldvirtftn = new OpCode(169150462, 84149506);

		// Token: 0x040007F5 RID: 2037
		public static readonly OpCode Ldarg = new OpCode(1247742, 84804866);

		// Token: 0x040007F6 RID: 2038
		public static readonly OpCode Ldarga = new OpCode(1379070, 84804866);

		// Token: 0x040007F7 RID: 2039
		public static readonly OpCode Starg = new OpCode(17959934, 84804866);

		// Token: 0x040007F8 RID: 2040
		public static readonly OpCode Ldloc = new OpCode(1248510, 84804866);

		// Token: 0x040007F9 RID: 2041
		public static readonly OpCode Ldloca = new OpCode(1379838, 84804866);

		// Token: 0x040007FA RID: 2042
		public static readonly OpCode Stloc = new OpCode(17960702, 84804866);

		// Token: 0x040007FB RID: 2043
		public static readonly OpCode Localloc = new OpCode(51711998, 84215042);

		// Token: 0x040007FC RID: 2044
		public static readonly OpCode Endfilter = new OpCode(51515902, 117769474);

		// Token: 0x040007FD RID: 2045
		public static readonly OpCode Unaligned = new OpCode(1184510, 68158466);

		// Token: 0x040007FE RID: 2046
		public static readonly OpCode Volatile = new OpCode(1184766, 67437570);

		// Token: 0x040007FF RID: 2047
		public static readonly OpCode Tailcall = new OpCode(1185022, 67437570);

		// Token: 0x04000800 RID: 2048
		public static readonly OpCode Initobj = new OpCode(51516926, 84738818);

		// Token: 0x04000801 RID: 2049
		public static readonly OpCode Constrained = new OpCode(1185534, 67961858);

		// Token: 0x04000802 RID: 2050
		public static readonly OpCode Cpblk = new OpCode(118626302, 84215042);

		// Token: 0x04000803 RID: 2051
		public static readonly OpCode Initblk = new OpCode(118626558, 84215042);

		// Token: 0x04000804 RID: 2052
		public static readonly OpCode Rethrow = new OpCode(1186558, 134546178);

		// Token: 0x04000805 RID: 2053
		public static readonly OpCode Sizeof = new OpCode(1383678, 84739330);

		// Token: 0x04000806 RID: 2054
		public static readonly OpCode Refanytype = new OpCode(18161150, 84215042);

		// Token: 0x04000807 RID: 2055
		public static readonly OpCode Readonly = new OpCode(1187582, 67437570);
	}
}
