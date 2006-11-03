using System;
using System.Collections.Generic;
using System.Text;
using Yusen.GExplorer.AppCore;
using System.ComponentModel;
using System.Xml.Serialization;
using Yusen.GExplorer.UserInterfaces;
using System.IO;

namespace Yusen.GExplorer {
	public sealed class RootOptions {
		public RootOptions() {
		}
		
		private AppBasicOptions appBasicOptions = new AppBasicOptions();
		[Browsable(false)]
		[SubOptions("基本設定", "アプリケーションの基本的な設定")]
		public AppBasicOptions AppBasicOptions {
			get { return this.appBasicOptions; }
			set { this.appBasicOptions = value; }
		}
		
		private CrawlOptions crawlOptions = new CrawlOptions();
		[Browsable(false)]
		[SubOptions("クローラ", "クローラの動作に関する設定")]
		public CrawlOptions CrawlOptions {
			get { return this.crawlOptions; }
			set { this.crawlOptions = value; }
		}

		private MainFormOptions mainFormOptions = new MainFormOptions();
		[Browsable(false)]
		[SubOptions("メインフォーム", "メインフォームに関する設定")]
		public MainFormOptions MainFormOptions {
			get { return this.mainFormOptions; }
			set { this.mainFormOptions = value; }
		}
		
		private PlayerFormOptions playerFormOptions = new PlayerFormOptions();
		[Browsable(false)]
		[SubOptions("プレーヤ", "プレーヤフォームに関する設定")]
		public PlayerFormOptions PlayerFormOptions {
			get { return this.playerFormOptions; }
			set { this.playerFormOptions = value; }
		}

		private BrowserFormOptions browserFormOptions = new BrowserFormOptions();
		[Browsable(false)]
		[SubOptions("ウェブブラウザ", "ブラウザフォームに関する設定")]
		public BrowserFormOptions BrowserFormOptions {
			get { return this.browserFormOptions; }
			set { this.browserFormOptions = value; }
		}
		
		private CacheViewerFormOptions cacheViewerFormOptions = new CacheViewerFormOptions();
		[Browsable(false)]
		[SubOptions("キャッシュビューア", "キャッシュビューアフォームに関する設定")]
		public CacheViewerFormOptions CacheViewerFormOptions {
			get { return this.cacheViewerFormOptions; }
			set { this.cacheViewerFormOptions = value; }
		}
		
		private ExternalCommandsEditorOptions externalCommandsEditorOptions = new ExternalCommandsEditorOptions();
		[Browsable(false)]
		[SubOptions("外部コマンドエディタ", "外部コマンドエディタに関する設定")]
		public ExternalCommandsEditorOptions ExternalCommandsEditorOptions {
			get { return this.externalCommandsEditorOptions; }
			set { this.externalCommandsEditorOptions = value; }
		}
		
		private OptionsFormOptions optionsFormOptions = new OptionsFormOptions();
		[Browsable(false)]
		[SubOptions("オプションフォーム", "オプションフォームに関する設定")]
		public OptionsFormOptions OptionsFormOptions {
			get { return this.optionsFormOptions; }
			set { this.optionsFormOptions = value; }
		}

		public void Serialize(string filename) {
			XmlSerializer xs = new XmlSerializer(typeof(RootOptions));
			using (StreamWriter sw = new StreamWriter(filename)) {
				xs.Serialize(sw, this);
			}
		}
		public static bool TryDeserialize(string filename, out RootOptions options) {
			FileInfo fi = new FileInfo(filename);
			if (fi.Exists) {
				XmlSerializer xs = new XmlSerializer(typeof(RootOptions));
				try {
					using (StreamReader sr = new StreamReader(filename)) {
						options = xs.Deserialize(sr) as RootOptions;
						if (null != options) {
							return true;
						}
					}
				} catch {
				}
			}
			options = null;
			return false;
		}
	}
}
