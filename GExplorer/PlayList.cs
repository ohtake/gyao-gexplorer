using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Yusen.GExplorer {
	class PlayList : ItemsManagerBase<ContentAdapter> {
		private static PlayList instance = new PlayList();
		public static PlayList Instance {
			get { return PlayList.instance; }
		}
		
		public event EventHandler PlayListChanged;
		public event EventHandler CurrentContentChanged;

		private bool updating = false;
		private bool updated = false;
		private ContentAdapter currentContent = null;

		public void MoveToTop(ContentAdapter cont) {
			int contIdx = this.items.IndexOf(cont);
			if (contIdx > 0) {
				this.items.Remove(cont);
				this.items.Insert(0, cont);
				this.OnChanged();
			}
		}
		public void MoveUp(ContentAdapter cont) {
			int contIdx = this.items.IndexOf(cont);
			if (contIdx > 0) {
				base.Swap(contIdx, contIdx-1);
			}
		}
		public void MoveDown(ContentAdapter cont) {
			int contIdx = this.items.IndexOf(cont);
			if (contIdx < this.items.Count -1) {
				base.Swap(contIdx, contIdx+1);
			}
		}
		public void MoveToBottom(ContentAdapter cont) {
			int contIdx = this.items.IndexOf(cont);
			if (contIdx < this.items.Count -1) {
				this.items.Remove(cont);
				this.items.Add(cont);
				this.OnChanged();
			}
		}

		public ContentAdapter NextContentOf(ContentAdapter cont) {
			int contIdx = this.items.IndexOf(cont);
			if (contIdx < 0) {
				if (this.items.Count > 0) {
					return this.items[0];
				} else {
					return null;
				}
			}
			if (contIdx+1 == this.items.Count) {
				return null;
			}
			return this.items[contIdx+1];
		}
		public ContentAdapter PrevContentOf(ContentAdapter cont) {
			int contIdx = this.items.IndexOf(cont);
			if (contIdx < 0) {
				if (this.items.Count > 0) {
					return this.items[this.items.Count -1];
				} else {
					return null;
				}
			}
			if (0 == contIdx) {
				return null;
			}
			return this.items[contIdx-1];
		}

		public void BeginUpdate() {
			lock (this) {
				if (this.updating) throw new InvalidOperationException("updating�Ȃ̂�BeginUpdate�Ăяo��");
				this.updating = true;
				this.updated = false;
			}
		}
		public void EndUpdate() {
			lock (this) {
				if (!this.updating) throw new InvalidOperationException("��updating�Ȃ̂�EndUpdate�Ăяo��");
				this.updating = false;
				if (this.updated) this.OnChanged();
			}
		}
		public ContentAdapter CurrentContent {
			set {
				if (value != this.currentContent) {
					this.currentContent = value;
					this.OnCurrentContentChanged();
				}
			}
		}
		public bool IsCurrentContent(ContentAdapter cont) {
			return cont.Equals(this.currentContent);
		}
		private void OnCurrentContentChanged() {
			if (null != this.CurrentContentChanged) {
				this.CurrentContentChanged(this, EventArgs.Empty);
			}
		}
		protected override void OnChanged() {
			if (this.updating) {
				this.updated = true;
				return;
			}
			if (null != this.PlayListChanged) {
				this.PlayListChanged(this, EventArgs.Empty);
			}
		}
		public void ExportAsAsx(string filename) {
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "\t";
			settings.OmitXmlDeclaration = true; // XML�錾�������WMP���ǂݍ���ł���Ȃ�
			
			using (TextWriter textWriter = new StreamWriter(filename))
			using (XmlWriter writer = XmlWriter.Create(textWriter, settings)) {
				writer.WriteStartDocument();
				writer.WriteStartElement("ASX");
				writer.WriteAttributeString("VERSION", "3.0");
				foreach (ContentAdapter cont in this.items) {
					writer.WriteStartElement("ENTRY");
					
					//���{�ꂪ����Ƃ��߂��ۂ�
					//writer.WriteElementString("TITLE", cont.DisplayName);
					
					writer.WriteStartElement("REF");
					writer.WriteAttributeString("href", cont.MediaFileUri.AbsoluteUri);
					writer.WriteEndElement();

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
				writer.WriteEndDocument();
			}
		}
		
		protected override string FilenameForSerialization {
			get { return @"PlayList.xml"; }
		}
	}
}
