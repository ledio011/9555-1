using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnityEngine
{
	// Token: 0x0200013F RID: 319
	public sealed class WWWForm
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0001B1F4 File Offset: 0x000193F4
		public Hashtable headers
		{
			get
			{
				Hashtable hashtable = new Hashtable();
				if (this.containsFiles)
				{
					hashtable["Content-Type"] = "multipart/form-data; boundary=\"" + Encoding.UTF8.GetString(this.boundary) + "\"";
				}
				else
				{
					hashtable["Content-Type"] = "application/x-www-form-urlencoded";
				}
				return hashtable;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0001B254 File Offset: 0x00019454
		public byte[] data
		{
			get
			{
				if (this.containsFiles)
				{
					byte[] bytes = WWW.DefaultEncoding.GetBytes("--");
					byte[] bytes2 = WWW.DefaultEncoding.GetBytes("\r\n");
					byte[] bytes3 = WWW.DefaultEncoding.GetBytes("Content-Type: ");
					byte[] bytes4 = WWW.DefaultEncoding.GetBytes("Content-disposition: form-data; name=\"");
					byte[] bytes5 = WWW.DefaultEncoding.GetBytes("\"");
					byte[] bytes6 = WWW.DefaultEncoding.GetBytes("; filename=\"");
					using (MemoryStream memoryStream = new MemoryStream(1024))
					{
						for (int i = 0; i < this.formData.Count; i++)
						{
							memoryStream.Write(bytes2, 0, bytes2.Length);
							memoryStream.Write(bytes, 0, bytes.Length);
							memoryStream.Write(this.boundary, 0, this.boundary.Length);
							memoryStream.Write(bytes2, 0, bytes2.Length);
							memoryStream.Write(bytes3, 0, bytes3.Length);
							byte[] bytes7 = Encoding.UTF8.GetBytes(this.types[i]);
							memoryStream.Write(bytes7, 0, bytes7.Length);
							memoryStream.Write(bytes2, 0, bytes2.Length);
							memoryStream.Write(bytes4, 0, bytes4.Length);
							string headerName = Encoding.UTF8.HeaderName;
							string text = this.fieldNames[i];
							if (!WWWTranscoder.SevenBitClean(text, Encoding.UTF8) || text.IndexOf("=?") > -1)
							{
								text = string.Concat(new string[]
								{
									"=?",
									headerName,
									"?Q?",
									WWWTranscoder.QPEncode(text, Encoding.UTF8),
									"?="
								});
							}
							byte[] bytes8 = Encoding.UTF8.GetBytes(text);
							memoryStream.Write(bytes8, 0, bytes8.Length);
							memoryStream.Write(bytes5, 0, bytes5.Length);
							if (this.fileNames[i] != null)
							{
								string text2 = this.fileNames[i];
								if (!WWWTranscoder.SevenBitClean(text2, Encoding.UTF8) || text2.IndexOf("=?") > -1)
								{
									text2 = string.Concat(new string[]
									{
										"=?",
										headerName,
										"?Q?",
										WWWTranscoder.QPEncode(text2, Encoding.UTF8),
										"?="
									});
								}
								byte[] bytes9 = Encoding.UTF8.GetBytes(text2);
								memoryStream.Write(bytes6, 0, bytes6.Length);
								memoryStream.Write(bytes9, 0, bytes9.Length);
								memoryStream.Write(bytes5, 0, bytes5.Length);
							}
							memoryStream.Write(bytes2, 0, bytes2.Length);
							memoryStream.Write(bytes2, 0, bytes2.Length);
							byte[] array = this.formData[i];
							memoryStream.Write(array, 0, array.Length);
						}
						memoryStream.Write(bytes2, 0, bytes2.Length);
						memoryStream.Write(bytes, 0, bytes.Length);
						memoryStream.Write(this.boundary, 0, this.boundary.Length);
						memoryStream.Write(bytes, 0, bytes.Length);
						memoryStream.Write(bytes2, 0, bytes2.Length);
						return memoryStream.ToArray();
					}
				}
				byte[] bytes10 = WWW.DefaultEncoding.GetBytes("&");
				byte[] bytes11 = WWW.DefaultEncoding.GetBytes("=");
				byte[] result;
				using (MemoryStream memoryStream2 = new MemoryStream(1024))
				{
					for (int j = 0; j < this.formData.Count; j++)
					{
						byte[] array2 = WWWTranscoder.URLEncode(Encoding.UTF8.GetBytes(this.fieldNames[j]));
						byte[] toEncode = this.formData[j];
						byte[] array3 = WWWTranscoder.URLEncode(toEncode);
						if (j > 0)
						{
							memoryStream2.Write(bytes10, 0, bytes10.Length);
						}
						memoryStream2.Write(array2, 0, array2.Length);
						memoryStream2.Write(bytes11, 0, bytes11.Length);
						memoryStream2.Write(array3, 0, array3.Length);
					}
					result = memoryStream2.ToArray();
				}
				return result;
			}
		}

		// Token: 0x040004FF RID: 1279
		private List<byte[]> formData;

		// Token: 0x04000500 RID: 1280
		private List<string> fieldNames;

		// Token: 0x04000501 RID: 1281
		private List<string> fileNames;

		// Token: 0x04000502 RID: 1282
		private List<string> types;

		// Token: 0x04000503 RID: 1283
		private byte[] boundary;

		// Token: 0x04000504 RID: 1284
		private bool containsFiles;
	}
}
