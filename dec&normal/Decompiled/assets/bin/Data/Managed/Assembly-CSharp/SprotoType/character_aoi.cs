using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200033E RID: 830
	public class character_aoi : SprotoTypeBase
	{
		// Token: 0x06001816 RID: 6166 RVA: 0x0008D2D8 File Offset: 0x0008B4D8
		public character_aoi() : base(character_aoi.max_field_count)
		{
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0008D2E8 File Offset: 0x0008B4E8
		public character_aoi(byte[] buffer) : base(character_aoi.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x0008D304 File Offset: 0x0008B504
		// (set) Token: 0x0600181A RID: 6170 RVA: 0x0008D30C File Offset: 0x0008B50C
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._id = value;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x0008D324 File Offset: 0x0008B524
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x0008D334 File Offset: 0x0008B534
		// (set) Token: 0x0600181D RID: 6173 RVA: 0x0008D33C File Offset: 0x0008B53C
		public characterVisual visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._visual = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x0008D354 File Offset: 0x0008B554
		public bool HasVisual
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x0008D364 File Offset: 0x0008B564
		// (set) Token: 0x06001820 RID: 6176 RVA: 0x0008D36C File Offset: 0x0008B56C
		public general general
		{
			get
			{
				return this._general;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._general = value;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x0008D384 File Offset: 0x0008B584
		public bool HasGeneral
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x0008D394 File Offset: 0x0008B594
		// (set) Token: 0x06001823 RID: 6179 RVA: 0x0008D39C File Offset: 0x0008B59C
		public attribute_other attribute_other
		{
			get
			{
				return this._attribute_other;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._attribute_other = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x0008D3B4 File Offset: 0x0008B5B4
		public bool HasAttribute_other
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0008D3C4 File Offset: 0x0008B5C4
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x0008D3CC File Offset: 0x0008B5CC
		public movement movement
		{
			get
			{
				return this._movement;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._movement = value;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x0008D3E4 File Offset: 0x0008B5E4
		public bool HasMovement
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x0008D3F4 File Offset: 0x0008B5F4
		// (set) Token: 0x06001829 RID: 6185 RVA: 0x0008D3FC File Offset: 0x0008B5FC
		public runtime_agent runtime
		{
			get
			{
				return this._runtime;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._runtime = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0008D414 File Offset: 0x0008B614
		public bool HasRuntime
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0008D424 File Offset: 0x0008B624
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.id = this.deserialize.read_integer();
					continue;
				case 1:
					this.visual = this.deserialize.read_obj<characterVisual>();
					continue;
				case 2:
					this.general = this.deserialize.read_obj<general>();
					continue;
				case 3:
					this.attribute_other = this.deserialize.read_obj<attribute_other>();
					continue;
				case 5:
					this.movement = this.deserialize.read_obj<movement>();
					continue;
				case 6:
					this.runtime = this.deserialize.read_obj<runtime_agent>();
					continue;
				}
				this.deserialize.read_unknow_data();
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0008D508 File Offset: 0x0008B708
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_obj(this.visual, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_obj(this.general, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_obj(this.attribute_other, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_obj(this.movement, 5);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_obj(this.runtime, 6);
			}
			return this.serialize.close();
		}

		// Token: 0x0400191C RID: 6428
		private static int max_field_count = 7;

		// Token: 0x0400191D RID: 6429
		private long _id;

		// Token: 0x0400191E RID: 6430
		private characterVisual _visual;

		// Token: 0x0400191F RID: 6431
		private general _general;

		// Token: 0x04001920 RID: 6432
		private attribute_other _attribute_other;

		// Token: 0x04001921 RID: 6433
		private movement _movement;

		// Token: 0x04001922 RID: 6434
		private runtime_agent _runtime;
	}
}
