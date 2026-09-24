using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000211 RID: 529
	[CLSCompliant(false)]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("17156360-2F1A-384A-BC52-FDE93C215C5B")]
	[TypeLibImportClass(typeof(Assembly))]
	public interface _Assembly
	{
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06001299 RID: 4761
		// (remove) Token: 0x0600129A RID: 4762
		event ModuleResolveEventHandler ModuleResolve;

		// Token: 0x0600129B RID: 4763
		string ToString();

		// Token: 0x0600129C RID: 4764
		Type GetType();

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600129D RID: 4765
		string CodeBase { get; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600129E RID: 4766
		string EscapedCodeBase { get; }

		// Token: 0x0600129F RID: 4767
		AssemblyName GetName();

		// Token: 0x060012A0 RID: 4768
		AssemblyName GetName(bool copiedName);

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060012A1 RID: 4769
		string FullName { get; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060012A2 RID: 4770
		MethodInfo EntryPoint { get; }

		// Token: 0x060012A3 RID: 4771
		Type GetType(string name);

		// Token: 0x060012A4 RID: 4772
		Type GetType(string name, bool throwOnError);

		// Token: 0x060012A5 RID: 4773
		Type[] GetExportedTypes();

		// Token: 0x060012A6 RID: 4774
		Type[] GetTypes();

		// Token: 0x060012A7 RID: 4775
		Stream GetManifestResourceStream(Type type, string name);

		// Token: 0x060012A8 RID: 4776
		Stream GetManifestResourceStream(string name);

		// Token: 0x060012A9 RID: 4777
		FileStream GetFile(string name);

		// Token: 0x060012AA RID: 4778
		FileStream[] GetFiles();

		// Token: 0x060012AB RID: 4779
		FileStream[] GetFiles(bool getResourceModules);

		// Token: 0x060012AC RID: 4780
		string[] GetManifestResourceNames();

		// Token: 0x060012AD RID: 4781
		ManifestResourceInfo GetManifestResourceInfo(string resourceName);

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060012AE RID: 4782
		string Location { get; }

		// Token: 0x060012AF RID: 4783
		void GetObjectData(SerializationInfo info, StreamingContext context);

		// Token: 0x060012B0 RID: 4784
		Type GetType(string name, bool throwOnError, bool ignoreCase);

		// Token: 0x060012B1 RID: 4785
		Assembly GetSatelliteAssembly(CultureInfo culture);

		// Token: 0x060012B2 RID: 4786
		Assembly GetSatelliteAssembly(CultureInfo culture, Version version);

		// Token: 0x060012B3 RID: 4787
		Module LoadModule(string moduleName, byte[] rawModule);

		// Token: 0x060012B4 RID: 4788
		Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore);

		// Token: 0x060012B5 RID: 4789
		object CreateInstance(string typeName);

		// Token: 0x060012B6 RID: 4790
		object CreateInstance(string typeName, bool ignoreCase);

		// Token: 0x060012B7 RID: 4791
		object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);

		// Token: 0x060012B8 RID: 4792
		Module[] GetLoadedModules();

		// Token: 0x060012B9 RID: 4793
		Module[] GetLoadedModules(bool getResourceModules);

		// Token: 0x060012BA RID: 4794
		Module[] GetModules();

		// Token: 0x060012BB RID: 4795
		Module[] GetModules(bool getResourceModules);

		// Token: 0x060012BC RID: 4796
		Module GetModule(string name);

		// Token: 0x060012BD RID: 4797
		AssemblyName[] GetReferencedAssemblies();
	}
}
