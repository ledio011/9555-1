using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020002EC RID: 748
	[ComVisible(true)]
	public sealed class BinaryFormatter : IRemotingFormatter, IFormatter
	{
		// Token: 0x06001747 RID: 5959 RVA: 0x00051F54 File Offset: 0x00050154
		public BinaryFormatter()
		{
			this.surrogate_selector = BinaryFormatter.DefaultSurrogateSelector;
			this.context = new StreamingContext(StreamingContextStates.All);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00051F88 File Offset: 0x00050188
		public BinaryFormatter(ISurrogateSelector selector, StreamingContext context)
		{
			this.surrogate_selector = selector;
			this.context = context;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x00051FAC File Offset: 0x000501AC
		public static ISurrogateSelector DefaultSurrogateSelector { get; }

		// Token: 0x17000457 RID: 1111
		// (set) Token: 0x0600174A RID: 5962 RVA: 0x00051FB4 File Offset: 0x000501B4
		public FormatterAssemblyStyle AssemblyFormat
		{
			set
			{
				this.assembly_format = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00051FC0 File Offset: 0x000501C0
		public SerializationBinder Binder
		{
			get
			{
				return this.binder;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x00051FC8 File Offset: 0x000501C8
		public StreamingContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x00051FD0 File Offset: 0x000501D0
		// (set) Token: 0x0600174E RID: 5966 RVA: 0x00051FD8 File Offset: 0x000501D8
		public ISurrogateSelector SurrogateSelector
		{
			get
			{
				return this.surrogate_selector;
			}
			set
			{
				this.surrogate_selector = value;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x00051FE4 File Offset: 0x000501E4
		public TypeFilterLevel FilterLevel
		{
			get
			{
				return this.filter_level;
			}
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00051FEC File Offset: 0x000501EC
		public object Deserialize(Stream serializationStream)
		{
			return this.NoCheckDeserialize(serializationStream, null);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x00051FF8 File Offset: 0x000501F8
		public object Deserialize(Stream serializationStream, HeaderHandler handler)
		{
			return this.NoCheckDeserialize(serializationStream, handler);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00052004 File Offset: 0x00050204
		private object NoCheckDeserialize(Stream serializationStream, HeaderHandler handler)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream");
			}
			if (serializationStream.CanSeek && serializationStream.Length == 0L)
			{
				throw new SerializationException("serializationStream supports seeking, but its length is 0");
			}
			BinaryReader binaryReader = new BinaryReader(serializationStream);
			bool flag;
			this.ReadBinaryHeader(binaryReader, out flag);
			BinaryElement binaryElement = (BinaryElement)binaryReader.Read();
			if (binaryElement == BinaryElement.MethodCall)
			{
				return MessageFormatter.ReadMethodCall(binaryElement, binaryReader, flag, handler, this);
			}
			if (binaryElement == BinaryElement.MethodResponse)
			{
				return MessageFormatter.ReadMethodResponse(binaryElement, binaryReader, flag, handler, null, this);
			}
			ObjectReader objectReader = new ObjectReader(this);
			object result;
			Header[] headers;
			objectReader.ReadObjectGraph(binaryElement, binaryReader, flag, out result, out headers);
			if (handler != null)
			{
				handler(headers);
			}
			return result;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x000520A8 File Offset: 0x000502A8
		public object DeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			return this.NoCheckDeserializeMethodResponse(serializationStream, handler, methodCallMessage);
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x000520B4 File Offset: 0x000502B4
		private object NoCheckDeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream");
			}
			if (serializationStream.CanSeek && serializationStream.Length == 0L)
			{
				throw new SerializationException("serializationStream supports seeking, but its length is 0");
			}
			BinaryReader reader = new BinaryReader(serializationStream);
			bool hasHeaders;
			this.ReadBinaryHeader(reader, out hasHeaders);
			return MessageFormatter.ReadMethodResponse(reader, hasHeaders, handler, methodCallMessage, this);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00052110 File Offset: 0x00050310
		public void Serialize(Stream serializationStream, object graph)
		{
			this.Serialize(serializationStream, graph, null);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0005211C File Offset: 0x0005031C
		public void Serialize(Stream serializationStream, object graph, Header[] headers)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream");
			}
			BinaryWriter binaryWriter = new BinaryWriter(serializationStream);
			this.WriteBinaryHeader(binaryWriter, headers != null);
			if (graph is IMethodCallMessage)
			{
				MessageFormatter.WriteMethodCall(binaryWriter, graph, headers, this.surrogate_selector, this.context, this.assembly_format, this.type_format);
			}
			else if (graph is IMethodReturnMessage)
			{
				MessageFormatter.WriteMethodResponse(binaryWriter, graph, headers, this.surrogate_selector, this.context, this.assembly_format, this.type_format);
			}
			else
			{
				ObjectWriter objectWriter = new ObjectWriter(this.surrogate_selector, this.context, this.assembly_format, this.type_format);
				objectWriter.WriteObjectGraph(binaryWriter, graph, headers);
			}
			binaryWriter.Flush();
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x000521DC File Offset: 0x000503DC
		private void WriteBinaryHeader(BinaryWriter writer, bool hasHeaders)
		{
			writer.Write(0);
			writer.Write(1);
			if (hasHeaders)
			{
				writer.Write(2);
			}
			else
			{
				writer.Write(-1);
			}
			writer.Write(1);
			writer.Write(0);
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00052214 File Offset: 0x00050414
		private void ReadBinaryHeader(BinaryReader reader, out bool hasHeaders)
		{
			reader.ReadByte();
			reader.ReadInt32();
			int num = reader.ReadInt32();
			hasHeaders = (num == 2);
			reader.ReadInt32();
			reader.ReadInt32();
		}

		// Token: 0x04000C06 RID: 3078
		private FormatterAssemblyStyle assembly_format;

		// Token: 0x04000C07 RID: 3079
		private SerializationBinder binder;

		// Token: 0x04000C08 RID: 3080
		private StreamingContext context;

		// Token: 0x04000C09 RID: 3081
		private ISurrogateSelector surrogate_selector;

		// Token: 0x04000C0A RID: 3082
		private FormatterTypeStyle type_format = FormatterTypeStyle.TypesAlways;

		// Token: 0x04000C0B RID: 3083
		private TypeFilterLevel filter_level = TypeFilterLevel.Full;
	}
}
