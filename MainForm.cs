using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class MainForm : Form, IUsesUserSettings{
		private static MainForm instance = null;
		public static MainForm Instance {
			get{
				if(null == MainForm.instance || !MainForm.instance.CanFocus) {
					MainForm.instance = new MainForm();
				}
				return MainForm.instance;
			}
		}
		
		public event GenreListViewSelectedContentsChangedEventHandler SelectedContentsChanged;
		
		private MainForm() {
			InitializeComponent();
			this.LoadSettings();
			Utility.AppendHelpMenu(this.menuStrip1);
			this.Icon = Utility.GetGExplorerIcon();
			this.Text = Application.ProductName + " " + Application.ProductVersion;
			this.tsslCategoryStat.Text = "";
			
			//�W�������^�u
			this.tabGenre.TabPages.Clear();
			foreach(GGenre g in GGenre.AllGenres) {
				TabPage tab = new TabPage(g.GenreName);
				tab.Tag = g;
				this.tabGenre.TabPages.Add(tab);
			}
			this.tabGenre.SelectedIndex = -1;
			this.tabGenre.SelectedIndexChanged += delegate {
				GGenre genre = (GGenre)this.tabGenre.SelectedTab.Tag;
				if(!genre.HasLoaded) genre.FetchAll();
				this.glvMain.Genre = genre;
			};
			this.tabGenre.DoubleClick += delegate {
				GGenre genre = (GGenre)this.tabGenre.SelectedTab.Tag;
				genre.FetchAll();
				this.glvMain.Genre = genre;
			};
			
			//���j���[����
			this.tsmiGyaoTop.Click += delegate {
				Utility.BrowseWithIE(new Uri("http://www.gyao.jp/"));
			};
			this.tsmiQuit.Click += delegate {
				this.Close();
			};
			this.tsmiContentProperty.Click += delegate {
				ContentPropertyViewer.Instance.Show();
				ContentPropertyViewer.Instance.Focus();
			};
			this.tsmiEditCommands.Click += delegate{
				UserCommandsEditor.Instance.Show();
				UserCommandsEditor.Instance.Focus();
			};
			this.tsmiUserSettings.Click += delegate {
				UserSettingsToolbox.Instance.Show();
				UserSettingsToolbox.Instance.Focus();
			};
			//���j���[���� (���X�g�r���[)
			foreach(View v in Enum.GetValues(typeof(View))){
				ToolStripMenuItem mi = new ToolStripMenuItem(v.ToString());
				mi.Tag = v;
				mi.Click += delegate(object sender, EventArgs e) {
					this.ListView.View = (View)(sender as ToolStripMenuItem).Tag;
					this.RefleshLvViewDropDownItems();
					this.SaveSettings();
				};
				this.tsmiLvView.DropDownItems.Add(mi);
			}
			this.tsmiFullRowSelect.Click += delegate {
				this.ListView.FullRowSelect = this.tsmiFullRowSelect.Checked;
				this.SaveSettings();
			};
			this.tsmiMultiSelect.Click += delegate {
				this.ListView.MultiSelect = this.tsmiMultiSelect.Checked;
				this.SaveSettings();
			};
			//�X�e�[�^�X�o�[
			this.glvMain.GenreChanged += new GenreListViewGenreChangedEventHandler(
				delegate(GenreListView sender, GGenre genre, int cntCount) {
					this.tsslCategoryStat.Text =
						"[" + genre.GenreName + "]"
						+ " " + cntCount.ToString() + " �̃I�u�W�F�N�g"
						+ " (�ŏI�ǂݍ��ݎ��� " + genre.LastFetchTime.ToShortTimeString() + ")";
					this.tspbPackages.Value = this.tspbPackages.Minimum;
				});
			GGenre.LoadingPackages += new LoadingPackagesEventHandler(
				delegate(GGenre sender, int nume, int denom) {
					this.tspbPackages.Maximum = denom;
					this.tspbPackages.Value = nume;
					this.tsslCategoryStat.Text = "[" + sender.GenreName + "] �ǂݍ��ݒ�";
					Application.DoEvents(); //�X�e�[�^�X�o�[�̃��x����`�悳���邽�߂ɕK�v�H
				});
			//�R���e���c�̑I��
			this.glvMain.SelectedContentsChanged += delegate(GenreListView sender, IEnumerable<GContent> contents) {
				if(null != this.SelectedContentsChanged) {
					this.SelectedContentsChanged(sender, contents);
				}
			};
			//���[�U�ݒ�
			this.LocationChanged += delegate {
				this.SaveSettings();
			};
			this.SizeChanged += delegate {
				this.SaveSettings();
			};
			this.glvMain.ColumnWidthChanged += delegate {
				this.SaveSettings();
			};
			UserSettings.Instance.MainForm.ChangeCompleted +=
				new UserSettingsChangeCompletedEventHandler(this.LoadSettings);
			this.FormClosing += new FormClosingEventHandler(
				delegate(object sender, FormClosingEventArgs e) {
					if(FormWindowState.Minimized == this.WindowState) {
						//�ŏ��������܂܏I�������ƃE�B���h�E�ʒu���ςɂȂ�̂Ō��ɖ߂�
						this.WindowState = FormWindowState.Normal;
					}
					UserSettings.Instance.MainForm.ChangeCompleted -= this.LoadSettings;
				});
		}
		
		public GGenre Genre {
			get {
				if(null == this.tabGenre.SelectedTab) return null;
				return (GGenre)this.tabGenre.SelectedTab.Tag;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				foreach(TabPage tp in this.tabGenre.TabPages) {
					if(value == (GGenre)tp.Tag) {
						this.tabGenre.SelectedTab = tp;
						break;
					}
				}
			}
		}
		
		private void RefleshLvViewDropDownItems() {
			foreach(ToolStripMenuItem m in this.tsmiLvView.DropDownItems) {
				m.Checked = (this.ListView.View == (View)m.Tag);
			}
		}
		public void LoadSettings() {
			UserSettings.Instance.MainForm.ApplySettings(this);
			this.RefleshLvViewDropDownItems();
			this.tsmiMultiSelect.Checked = this.ListView.MultiSelect;
			this.tsmiFullRowSelect.Checked = this.ListView.FullRowSelect;
		}
		public void SaveSettings() {
			UserSettings.Instance.MainForm.StoreSettings(this);
			UserSettings.Instance.MainForm.OnChangeCompleted();
		}
		
		public ListView ListView {
			get {
				return this.glvMain.ListView;
			}
		}
	}
}

