using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000323 RID: 803
	[ComVisible(true)]
	public class CryptoConfig
	{
		// Token: 0x06001873 RID: 6259 RVA: 0x00058AD8 File Offset: 0x00056CD8
		private static void Initialize()
		{
			Hashtable hashtable = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
			hashtable.Add("SHA", "System.Security.Cryptography.SHA1CryptoServiceProvider");
			hashtable.Add("SHA1", "System.Security.Cryptography.SHA1CryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.SHA1", "System.Security.Cryptography.SHA1CryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.HashAlgorithm", "System.Security.Cryptography.SHA1CryptoServiceProvider");
			hashtable.Add("MD5", "System.Security.Cryptography.MD5CryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.MD5", "System.Security.Cryptography.MD5CryptoServiceProvider");
			hashtable.Add("SHA256", "System.Security.Cryptography.SHA256Managed");
			hashtable.Add("SHA-256", "System.Security.Cryptography.SHA256Managed");
			hashtable.Add("System.Security.Cryptography.SHA256", "System.Security.Cryptography.SHA256Managed");
			hashtable.Add("SHA384", "System.Security.Cryptography.SHA384Managed");
			hashtable.Add("SHA-384", "System.Security.Cryptography.SHA384Managed");
			hashtable.Add("System.Security.Cryptography.SHA384", "System.Security.Cryptography.SHA384Managed");
			hashtable.Add("SHA512", "System.Security.Cryptography.SHA512Managed");
			hashtable.Add("SHA-512", "System.Security.Cryptography.SHA512Managed");
			hashtable.Add("System.Security.Cryptography.SHA512", "System.Security.Cryptography.SHA512Managed");
			hashtable.Add("RSA", "System.Security.Cryptography.RSACryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.RSA", "System.Security.Cryptography.RSACryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.AsymmetricAlgorithm", "System.Security.Cryptography.RSACryptoServiceProvider");
			hashtable.Add("DSA", "System.Security.Cryptography.DSACryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.DSA", "System.Security.Cryptography.DSACryptoServiceProvider");
			hashtable.Add("DES", "System.Security.Cryptography.DESCryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.DES", "System.Security.Cryptography.DESCryptoServiceProvider");
			hashtable.Add("3DES", "System.Security.Cryptography.TripleDESCryptoServiceProvider");
			hashtable.Add("TripleDES", "System.Security.Cryptography.TripleDESCryptoServiceProvider");
			hashtable.Add("Triple DES", "System.Security.Cryptography.TripleDESCryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.TripleDES", "System.Security.Cryptography.TripleDESCryptoServiceProvider");
			hashtable.Add("RC2", "System.Security.Cryptography.RC2CryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.RC2", "System.Security.Cryptography.RC2CryptoServiceProvider");
			hashtable.Add("Rijndael", "System.Security.Cryptography.RijndaelManaged");
			hashtable.Add("System.Security.Cryptography.Rijndael", "System.Security.Cryptography.RijndaelManaged");
			hashtable.Add("System.Security.Cryptography.SymmetricAlgorithm", "System.Security.Cryptography.RijndaelManaged");
			hashtable.Add("RandomNumberGenerator", "System.Security.Cryptography.RNGCryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.RandomNumberGenerator", "System.Security.Cryptography.RNGCryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.KeyedHashAlgorithm", "System.Security.Cryptography.HMACSHA1");
			hashtable.Add("HMACSHA1", "System.Security.Cryptography.HMACSHA1");
			hashtable.Add("System.Security.Cryptography.HMACSHA1", "System.Security.Cryptography.HMACSHA1");
			hashtable.Add("MACTripleDES", "System.Security.Cryptography.MACTripleDES");
			hashtable.Add("System.Security.Cryptography.MACTripleDES", "System.Security.Cryptography.MACTripleDES");
			hashtable.Add("RIPEMD160", "System.Security.Cryptography.RIPEMD160Managed");
			hashtable.Add("RIPEMD-160", "System.Security.Cryptography.RIPEMD160Managed");
			hashtable.Add("System.Security.Cryptography.RIPEMD160", "System.Security.Cryptography.RIPEMD160Managed");
			hashtable.Add("System.Security.Cryptography.HMAC", "System.Security.Cryptography.HMACSHA1");
			hashtable.Add("HMACMD5", "System.Security.Cryptography.HMACMD5");
			hashtable.Add("System.Security.Cryptography.HMACMD5", "System.Security.Cryptography.HMACMD5");
			hashtable.Add("HMACRIPEMD160", "System.Security.Cryptography.HMACRIPEMD160");
			hashtable.Add("System.Security.Cryptography.HMACRIPEMD160", "System.Security.Cryptography.HMACRIPEMD160");
			hashtable.Add("HMACSHA256", "System.Security.Cryptography.HMACSHA256");
			hashtable.Add("System.Security.Cryptography.HMACSHA256", "System.Security.Cryptography.HMACSHA256");
			hashtable.Add("HMACSHA384", "System.Security.Cryptography.HMACSHA384");
			hashtable.Add("System.Security.Cryptography.HMACSHA384", "System.Security.Cryptography.HMACSHA384");
			hashtable.Add("HMACSHA512", "System.Security.Cryptography.HMACSHA512");
			hashtable.Add("System.Security.Cryptography.HMACSHA512", "System.Security.Cryptography.HMACSHA512");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig#dsa-sha1", "System.Security.Cryptography.DSASignatureDescription");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig#rsa-sha1", "System.Security.Cryptography.RSAPKCS1SHA1SignatureDescription");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig#sha1", "System.Security.Cryptography.SHA1CryptoServiceProvider");
			hashtable.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315", "System.Security.Cryptography.Xml.XmlDsigC14NTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments", "System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig#base64", "System.Security.Cryptography.Xml.XmlDsigBase64Transform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/TR/1999/REC-xpath-19991116", "System.Security.Cryptography.Xml.XmlDsigXPathTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/TR/1999/REC-xslt-19991116", "System.Security.Cryptography.Xml.XmlDsigXsltTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig#enveloped-signature", "System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2001/10/xml-exc-c14n#", "System.Security.Cryptography.Xml.XmlDsigExcC14NTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2001/10/xml-exc-c14n#WithComments", "System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2002/07/decrypt#XML", "System.Security.Cryptography.Xml.XmlDecryptionTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2001/04/xmlenc#sha256", "System.Security.Cryptography.SHA256Managed");
			hashtable.Add("http://www.w3.org/2001/04/xmlenc#sha512", "System.Security.Cryptography.SHA512Managed");
			hashtable.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha256", "System.Security.Cryptography.HMACSHA256");
			hashtable.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha384", "System.Security.Cryptography.HMACSHA384");
			hashtable.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha512", "System.Security.Cryptography.HMACSHA512");
			hashtable.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160", "System.Security.Cryptography.HMACRIPEMD160");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig# X509Data", "System.Security.Cryptography.Xml.KeyInfoX509Data, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig# KeyName", "System.Security.Cryptography.Xml.KeyInfoName, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig# KeyValue/DSAKeyValue", "System.Security.Cryptography.Xml.DSAKeyValue, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig# KeyValue/RSAKeyValue", "System.Security.Cryptography.Xml.RSAKeyValue, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig# RetrievalMethod", "System.Security.Cryptography.Xml.KeyInfoRetrievalMethod, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("2.5.29.14", "System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			hashtable.Add("2.5.29.15", "System.Security.Cryptography.X509Certificates.X509KeyUsageExtension, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			hashtable.Add("2.5.29.19", "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			hashtable.Add("2.5.29.37", "System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			hashtable.Add("X509Chain", "System.Security.Cryptography.X509Certificates.X509Chain, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			Hashtable hashtable2 = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
			hashtable2.Add("System.Security.Cryptography.SHA1CryptoServiceProvider", "1.3.14.3.2.26");
			hashtable2.Add("System.Security.Cryptography.SHA1Managed", "1.3.14.3.2.26");
			hashtable2.Add("SHA1", "1.3.14.3.2.26");
			hashtable2.Add("System.Security.Cryptography.SHA1", "1.3.14.3.2.26");
			hashtable2.Add("System.Security.Cryptography.MD5CryptoServiceProvider", "1.2.840.113549.2.5");
			hashtable2.Add("MD5", "1.2.840.113549.2.5");
			hashtable2.Add("System.Security.Cryptography.MD5", "1.2.840.113549.2.5");
			hashtable2.Add("System.Security.Cryptography.SHA256Managed", "2.16.840.1.101.3.4.2.1");
			hashtable2.Add("SHA256", "2.16.840.1.101.3.4.2.1");
			hashtable2.Add("System.Security.Cryptography.SHA256", "2.16.840.1.101.3.4.2.1");
			hashtable2.Add("System.Security.Cryptography.SHA384Managed", "2.16.840.1.101.3.4.2.2");
			hashtable2.Add("SHA384", "2.16.840.1.101.3.4.2.2");
			hashtable2.Add("System.Security.Cryptography.SHA384", "2.16.840.1.101.3.4.2.2");
			hashtable2.Add("System.Security.Cryptography.SHA512Managed", "2.16.840.1.101.3.4.2.3");
			hashtable2.Add("SHA512", "2.16.840.1.101.3.4.2.3");
			hashtable2.Add("System.Security.Cryptography.SHA512", "2.16.840.1.101.3.4.2.3");
			hashtable2.Add("TripleDESKeyWrap", "1.2.840.113549.1.9.16.3.6");
			hashtable2.Add("DES", "1.3.14.3.2.7");
			hashtable2.Add("TripleDES", "1.2.840.113549.3.7");
			hashtable2.Add("RC2", "1.2.840.113549.3.2");
			CryptoConfig.algorithms = hashtable;
			CryptoConfig.oid = hashtable2;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00059154 File Offset: 0x00057354
		public static object CreateFromName(string name)
		{
			return CryptoConfig.CreateFromName(name, null);
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x00059160 File Offset: 0x00057360
		public static object CreateFromName(string name, params object[] args)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CryptoConfig.lockObject;
			lock (obj)
			{
				if (CryptoConfig.algorithms == null)
				{
					CryptoConfig.Initialize();
				}
			}
			object result;
			try
			{
				string text = (string)CryptoConfig.algorithms[name];
				if (text == null)
				{
					text = name;
				}
				Type type = Type.GetType(text);
				result = Activator.CreateInstance(type, args);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000CA7 RID: 3239
		private const string defaultNamespace = "System.Security.Cryptography.";

		// Token: 0x04000CA8 RID: 3240
		private const string defaultSHA1 = "System.Security.Cryptography.SHA1CryptoServiceProvider";

		// Token: 0x04000CA9 RID: 3241
		private const string defaultMD5 = "System.Security.Cryptography.MD5CryptoServiceProvider";

		// Token: 0x04000CAA RID: 3242
		private const string defaultSHA256 = "System.Security.Cryptography.SHA256Managed";

		// Token: 0x04000CAB RID: 3243
		private const string defaultSHA384 = "System.Security.Cryptography.SHA384Managed";

		// Token: 0x04000CAC RID: 3244
		private const string defaultSHA512 = "System.Security.Cryptography.SHA512Managed";

		// Token: 0x04000CAD RID: 3245
		private const string defaultRSA = "System.Security.Cryptography.RSACryptoServiceProvider";

		// Token: 0x04000CAE RID: 3246
		private const string defaultDSA = "System.Security.Cryptography.DSACryptoServiceProvider";

		// Token: 0x04000CAF RID: 3247
		private const string defaultDES = "System.Security.Cryptography.DESCryptoServiceProvider";

		// Token: 0x04000CB0 RID: 3248
		private const string default3DES = "System.Security.Cryptography.TripleDESCryptoServiceProvider";

		// Token: 0x04000CB1 RID: 3249
		private const string defaultRC2 = "System.Security.Cryptography.RC2CryptoServiceProvider";

		// Token: 0x04000CB2 RID: 3250
		private const string defaultAES = "System.Security.Cryptography.RijndaelManaged";

		// Token: 0x04000CB3 RID: 3251
		private const string defaultRNG = "System.Security.Cryptography.RNGCryptoServiceProvider";

		// Token: 0x04000CB4 RID: 3252
		private const string defaultHMAC = "System.Security.Cryptography.HMACSHA1";

		// Token: 0x04000CB5 RID: 3253
		private const string defaultMAC3DES = "System.Security.Cryptography.MACTripleDES";

		// Token: 0x04000CB6 RID: 3254
		private const string defaultDSASigDesc = "System.Security.Cryptography.DSASignatureDescription";

		// Token: 0x04000CB7 RID: 3255
		private const string defaultRSASigDesc = "System.Security.Cryptography.RSAPKCS1SHA1SignatureDescription";

		// Token: 0x04000CB8 RID: 3256
		private const string defaultRIPEMD160 = "System.Security.Cryptography.RIPEMD160Managed";

		// Token: 0x04000CB9 RID: 3257
		private const string defaultHMACMD5 = "System.Security.Cryptography.HMACMD5";

		// Token: 0x04000CBA RID: 3258
		private const string defaultHMACRIPEMD160 = "System.Security.Cryptography.HMACRIPEMD160";

		// Token: 0x04000CBB RID: 3259
		private const string defaultHMACSHA256 = "System.Security.Cryptography.HMACSHA256";

		// Token: 0x04000CBC RID: 3260
		private const string defaultHMACSHA384 = "System.Security.Cryptography.HMACSHA384";

		// Token: 0x04000CBD RID: 3261
		private const string defaultHMACSHA512 = "System.Security.Cryptography.HMACSHA512";

		// Token: 0x04000CBE RID: 3262
		private const string defaultC14N = "System.Security.Cryptography.Xml.XmlDsigC14NTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CBF RID: 3263
		private const string defaultC14NWithComments = "System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CC0 RID: 3264
		private const string defaultBase64 = "System.Security.Cryptography.Xml.XmlDsigBase64Transform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CC1 RID: 3265
		private const string defaultXPath = "System.Security.Cryptography.Xml.XmlDsigXPathTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CC2 RID: 3266
		private const string defaultXslt = "System.Security.Cryptography.Xml.XmlDsigXsltTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CC3 RID: 3267
		private const string defaultEnveloped = "System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CC4 RID: 3268
		private const string defaultXmlDecryption = "System.Security.Cryptography.Xml.XmlDecryptionTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CC5 RID: 3269
		private const string defaultExcC14N = "System.Security.Cryptography.Xml.XmlDsigExcC14NTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CC6 RID: 3270
		private const string defaultExcC14NWithComments = "System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CC7 RID: 3271
		private const string defaultX509Data = "System.Security.Cryptography.Xml.KeyInfoX509Data, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CC8 RID: 3272
		private const string defaultKeyName = "System.Security.Cryptography.Xml.KeyInfoName, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CC9 RID: 3273
		private const string defaultKeyValueDSA = "System.Security.Cryptography.Xml.DSAKeyValue, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CCA RID: 3274
		private const string defaultKeyValueRSA = "System.Security.Cryptography.Xml.RSAKeyValue, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CCB RID: 3275
		private const string defaultRetrievalMethod = "System.Security.Cryptography.Xml.KeyInfoRetrievalMethod, System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000CCC RID: 3276
		private const string managedSHA1 = "System.Security.Cryptography.SHA1Managed";

		// Token: 0x04000CCD RID: 3277
		private const string oidSHA1 = "1.3.14.3.2.26";

		// Token: 0x04000CCE RID: 3278
		private const string oidMD5 = "1.2.840.113549.2.5";

		// Token: 0x04000CCF RID: 3279
		private const string oidSHA256 = "2.16.840.1.101.3.4.2.1";

		// Token: 0x04000CD0 RID: 3280
		private const string oidSHA384 = "2.16.840.1.101.3.4.2.2";

		// Token: 0x04000CD1 RID: 3281
		private const string oidSHA512 = "2.16.840.1.101.3.4.2.3";

		// Token: 0x04000CD2 RID: 3282
		private const string oidDSA = "1.2.840.10040.4.1";

		// Token: 0x04000CD3 RID: 3283
		private const string oidDES = "1.3.14.3.2.7";

		// Token: 0x04000CD4 RID: 3284
		private const string oid3DES = "1.2.840.113549.3.7";

		// Token: 0x04000CD5 RID: 3285
		private const string oidRC2 = "1.2.840.113549.3.2";

		// Token: 0x04000CD6 RID: 3286
		private const string oid3DESKeyWrap = "1.2.840.113549.1.9.16.3.6";

		// Token: 0x04000CD7 RID: 3287
		private const string nameSHA1a = "SHA";

		// Token: 0x04000CD8 RID: 3288
		private const string nameSHA1b = "SHA1";

		// Token: 0x04000CD9 RID: 3289
		private const string nameSHA1c = "System.Security.Cryptography.SHA1";

		// Token: 0x04000CDA RID: 3290
		private const string nameSHA1d = "System.Security.Cryptography.HashAlgorithm";

		// Token: 0x04000CDB RID: 3291
		private const string nameMD5a = "MD5";

		// Token: 0x04000CDC RID: 3292
		private const string nameMD5b = "System.Security.Cryptography.MD5";

		// Token: 0x04000CDD RID: 3293
		private const string nameSHA256a = "SHA256";

		// Token: 0x04000CDE RID: 3294
		private const string nameSHA256b = "SHA-256";

		// Token: 0x04000CDF RID: 3295
		private const string nameSHA256c = "System.Security.Cryptography.SHA256";

		// Token: 0x04000CE0 RID: 3296
		private const string nameSHA384a = "SHA384";

		// Token: 0x04000CE1 RID: 3297
		private const string nameSHA384b = "SHA-384";

		// Token: 0x04000CE2 RID: 3298
		private const string nameSHA384c = "System.Security.Cryptography.SHA384";

		// Token: 0x04000CE3 RID: 3299
		private const string nameSHA512a = "SHA512";

		// Token: 0x04000CE4 RID: 3300
		private const string nameSHA512b = "SHA-512";

		// Token: 0x04000CE5 RID: 3301
		private const string nameSHA512c = "System.Security.Cryptography.SHA512";

		// Token: 0x04000CE6 RID: 3302
		private const string nameRSAa = "RSA";

		// Token: 0x04000CE7 RID: 3303
		private const string nameRSAb = "System.Security.Cryptography.RSA";

		// Token: 0x04000CE8 RID: 3304
		private const string nameRSAc = "System.Security.Cryptography.AsymmetricAlgorithm";

		// Token: 0x04000CE9 RID: 3305
		private const string nameDSAa = "DSA";

		// Token: 0x04000CEA RID: 3306
		private const string nameDSAb = "System.Security.Cryptography.DSA";

		// Token: 0x04000CEB RID: 3307
		private const string nameDESa = "DES";

		// Token: 0x04000CEC RID: 3308
		private const string nameDESb = "System.Security.Cryptography.DES";

		// Token: 0x04000CED RID: 3309
		private const string name3DESa = "3DES";

		// Token: 0x04000CEE RID: 3310
		private const string name3DESb = "TripleDES";

		// Token: 0x04000CEF RID: 3311
		private const string name3DESc = "Triple DES";

		// Token: 0x04000CF0 RID: 3312
		private const string name3DESd = "System.Security.Cryptography.TripleDES";

		// Token: 0x04000CF1 RID: 3313
		private const string nameRC2a = "RC2";

		// Token: 0x04000CF2 RID: 3314
		private const string nameRC2b = "System.Security.Cryptography.RC2";

		// Token: 0x04000CF3 RID: 3315
		private const string nameAESa = "Rijndael";

		// Token: 0x04000CF4 RID: 3316
		private const string nameAESb = "System.Security.Cryptography.Rijndael";

		// Token: 0x04000CF5 RID: 3317
		private const string nameAESc = "System.Security.Cryptography.SymmetricAlgorithm";

		// Token: 0x04000CF6 RID: 3318
		private const string nameRNGa = "RandomNumberGenerator";

		// Token: 0x04000CF7 RID: 3319
		private const string nameRNGb = "System.Security.Cryptography.RandomNumberGenerator";

		// Token: 0x04000CF8 RID: 3320
		private const string nameKeyHasha = "System.Security.Cryptography.KeyedHashAlgorithm";

		// Token: 0x04000CF9 RID: 3321
		private const string nameHMACSHA1a = "HMACSHA1";

		// Token: 0x04000CFA RID: 3322
		private const string nameHMACSHA1b = "System.Security.Cryptography.HMACSHA1";

		// Token: 0x04000CFB RID: 3323
		private const string nameMAC3DESa = "MACTripleDES";

		// Token: 0x04000CFC RID: 3324
		private const string nameMAC3DESb = "System.Security.Cryptography.MACTripleDES";

		// Token: 0x04000CFD RID: 3325
		private const string name3DESKeyWrap = "TripleDESKeyWrap";

		// Token: 0x04000CFE RID: 3326
		private const string nameRIPEMD160a = "RIPEMD160";

		// Token: 0x04000CFF RID: 3327
		private const string nameRIPEMD160b = "RIPEMD-160";

		// Token: 0x04000D00 RID: 3328
		private const string nameRIPEMD160c = "System.Security.Cryptography.RIPEMD160";

		// Token: 0x04000D01 RID: 3329
		private const string nameHMACa = "HMAC";

		// Token: 0x04000D02 RID: 3330
		private const string nameHMACb = "System.Security.Cryptography.HMAC";

		// Token: 0x04000D03 RID: 3331
		private const string nameHMACMD5a = "HMACMD5";

		// Token: 0x04000D04 RID: 3332
		private const string nameHMACMD5b = "System.Security.Cryptography.HMACMD5";

		// Token: 0x04000D05 RID: 3333
		private const string nameHMACRIPEMD160a = "HMACRIPEMD160";

		// Token: 0x04000D06 RID: 3334
		private const string nameHMACRIPEMD160b = "System.Security.Cryptography.HMACRIPEMD160";

		// Token: 0x04000D07 RID: 3335
		private const string nameHMACSHA256a = "HMACSHA256";

		// Token: 0x04000D08 RID: 3336
		private const string nameHMACSHA256b = "System.Security.Cryptography.HMACSHA256";

		// Token: 0x04000D09 RID: 3337
		private const string nameHMACSHA384a = "HMACSHA384";

		// Token: 0x04000D0A RID: 3338
		private const string nameHMACSHA384b = "System.Security.Cryptography.HMACSHA384";

		// Token: 0x04000D0B RID: 3339
		private const string nameHMACSHA512a = "HMACSHA512";

		// Token: 0x04000D0C RID: 3340
		private const string nameHMACSHA512b = "System.Security.Cryptography.HMACSHA512";

		// Token: 0x04000D0D RID: 3341
		private const string urlXmlDsig = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x04000D0E RID: 3342
		private const string urlDSASHA1 = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";

		// Token: 0x04000D0F RID: 3343
		private const string urlRSASHA1 = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		// Token: 0x04000D10 RID: 3344
		private const string urlSHA1 = "http://www.w3.org/2000/09/xmldsig#sha1";

		// Token: 0x04000D11 RID: 3345
		private const string urlC14N = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		// Token: 0x04000D12 RID: 3346
		private const string urlC14NWithComments = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		// Token: 0x04000D13 RID: 3347
		private const string urlBase64 = "http://www.w3.org/2000/09/xmldsig#base64";

		// Token: 0x04000D14 RID: 3348
		private const string urlXPath = "http://www.w3.org/TR/1999/REC-xpath-19991116";

		// Token: 0x04000D15 RID: 3349
		private const string urlXslt = "http://www.w3.org/TR/1999/REC-xslt-19991116";

		// Token: 0x04000D16 RID: 3350
		private const string urlEnveloped = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";

		// Token: 0x04000D17 RID: 3351
		private const string urlXmlDecryption = "http://www.w3.org/2002/07/decrypt#XML";

		// Token: 0x04000D18 RID: 3352
		private const string urlExcC14NWithComments = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		// Token: 0x04000D19 RID: 3353
		private const string urlExcC14N = "http://www.w3.org/2001/10/xml-exc-c14n#";

		// Token: 0x04000D1A RID: 3354
		private const string urlSHA256 = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x04000D1B RID: 3355
		private const string urlSHA512 = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x04000D1C RID: 3356
		private const string urlHMACSHA256 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

		// Token: 0x04000D1D RID: 3357
		private const string urlHMACSHA384 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";

		// Token: 0x04000D1E RID: 3358
		private const string urlHMACSHA512 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";

		// Token: 0x04000D1F RID: 3359
		private const string urlHMACRIPEMD160 = "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160";

		// Token: 0x04000D20 RID: 3360
		private const string urlX509Data = "http://www.w3.org/2000/09/xmldsig# X509Data";

		// Token: 0x04000D21 RID: 3361
		private const string urlKeyName = "http://www.w3.org/2000/09/xmldsig# KeyName";

		// Token: 0x04000D22 RID: 3362
		private const string urlKeyValueDSA = "http://www.w3.org/2000/09/xmldsig# KeyValue/DSAKeyValue";

		// Token: 0x04000D23 RID: 3363
		private const string urlKeyValueRSA = "http://www.w3.org/2000/09/xmldsig# KeyValue/RSAKeyValue";

		// Token: 0x04000D24 RID: 3364
		private const string urlRetrievalMethod = "http://www.w3.org/2000/09/xmldsig# RetrievalMethod";

		// Token: 0x04000D25 RID: 3365
		private const string oidX509SubjectKeyIdentifier = "2.5.29.14";

		// Token: 0x04000D26 RID: 3366
		private const string oidX509KeyUsage = "2.5.29.15";

		// Token: 0x04000D27 RID: 3367
		private const string oidX509BasicConstraints = "2.5.29.19";

		// Token: 0x04000D28 RID: 3368
		private const string oidX509EnhancedKeyUsage = "2.5.29.37";

		// Token: 0x04000D29 RID: 3369
		private const string nameX509SubjectKeyIdentifier = "System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000D2A RID: 3370
		private const string nameX509KeyUsage = "System.Security.Cryptography.X509Certificates.X509KeyUsageExtension, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000D2B RID: 3371
		private const string nameX509BasicConstraints = "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000D2C RID: 3372
		private const string nameX509EnhancedKeyUsage = "System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000D2D RID: 3373
		private const string nameX509Chain = "X509Chain";

		// Token: 0x04000D2E RID: 3374
		private const string defaultX509Chain = "System.Security.Cryptography.X509Certificates.X509Chain, System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000D2F RID: 3375
		private static object lockObject = new object();

		// Token: 0x04000D30 RID: 3376
		private static Hashtable algorithms;

		// Token: 0x04000D31 RID: 3377
		private static Hashtable oid;
	}
}
