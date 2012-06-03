using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using Encoding = System.Text.Encoding;

namespace Yusen.GExplorer {
	public class Parser {
		private readonly Regex regexDetail = new Regex(@"javascript:gotoDetail\( 'cnt([0-9]+)' \);", RegexOptions.Compiled);
		private readonly Regex regexList = new Regex(@"javascript:gotoList\( 'pac([0-9]+)' \);", RegexOptions.Compiled);
		private readonly Regex regexSpecial = new Regex(@"<a href=""([^""]+)"" .* alt=""���W�y�[�W""", RegexOptions.Compiled);

		public Parser() {
		}
		
		public VideoItem[] ExtractVideoItemsFromCategoryHtml(Category category) {
			List<VideoItem> items = new List<VideoItem>();
			try {
				TextReader tr = new StreamReader(new WebClient().OpenRead(category.Uri), Encoding.GetEncoding("Shift_JIS"));
				string line;
				Queue<string> subgenres = new Queue<string>();
				Queue<string> packs = new Queue<string>();
				Queue<string> episodes = new Queue<string>();
				Queue<bool> news = new Queue<bool>();
				Queue<string> limits = new Queue<string>();
				Queue<string> leads = new Queue<string>();
				while(null != (line = tr.ReadLine())) {
					switch(line) {
						case "<!--�T�u�W�������� ��-->":
							subgenres.Enqueue(ReadNextLineTextBeforeTag(tr));
							break;
						case "<!--�p�b�N�� ��-->":
							packs.Enqueue(ReadNextLineTextBeforeTag(tr));
							break;
						case "<!--��-�b ��-->":
							episodes.Enqueue(ReadNextLineTextBeforeTag(tr));
							break;
						case "<!--NEW�A�C�R�� ��-->":
							news.Enqueue(ReadNextLineHtml(tr).StartsWith("<img"));
							break;
						case "<!--�z�M�I���� ��-->":
							limits.Enqueue(ReadNextLineTextBeforeTag(tr));
							break;
						case "<!--���[�h ��-->":
							leads.Enqueue(ReadNextLineTextBeforeTag(tr));
							break;
						default: //������
							Match match;
							//����
							match = this.regexDetail.Match(line);
							if(match.Success) {
								VideoItem item = new VideoItem(
									0 == news.Count ? false : news.Dequeue(),
									int.Parse(match.Groups[1].Value),
									0 == limits.Count ? "" : limits.Dequeue(),
									0 == subgenres.Count ? "" : subgenres.Dequeue(),
									0 == packs.Count ? "" : packs.Dequeue(),
									0 == episodes.Count ? "" : episodes.Dequeue(),
									0 == leads.Count ? "" : leads.Dequeue());
								//����͂Ђǂ�
								if(0 < items.Count) {
									if("" == item.Subgenre) item.Subgenre = items[items.Count - 1].Subgenre;
									if("" == item.Pack) item.Pack = items[items.Count - 1].Pack;
								}
								items.Add(item);
								break;
							}
							//�S�V���[�Y������
							match = this.regexList.Match(line);
							if(match.Success) {
								items.Add(new VideoItem(
									int.Parse(match.Groups[1].Value),
									0 == subgenres.Count ? "" : subgenres.Peek(),
									0 == packs.Count ? "" : packs.Peek()));
								break;
							}
							//���W�y�[�W
							match = this.regexSpecial.Match(line);
							if(match.Success) {
								items.Add(new VideoItem(
									0 == news.Count ? false : news.Dequeue(),
									new Uri(category.Uri, match.Groups[1].Value),
									0 == limits.Count ? "" : limits.Dequeue(),
									0 == subgenres.Count ? "" : subgenres.Dequeue(),
									0 == packs.Count ? "" : packs.Dequeue(),
									0 == leads.Count ? "" : leads.Dequeue()));
								if(0 != episodes.Count) episodes.Dequeue();
								break;
							}
							break;
					}
				}
			} catch(WebException e) {
				Utility.DisplayException(e);
			}
			return items.ToArray();
		}

		private string ReadNextLineTextBeforeTag(TextReader reader) {
			string line = ReadNextLineHtml(reader);
			int indexOfLt = line.IndexOf('<');
			if(-1 != indexOfLt) {
				line = line.Substring(0, indexOfLt);
			}
			line = line.Replace("&nbsp;", " ");
			return line.Trim();
		}
		private string ReadNextLineHtml(TextReader reader) {
			string line = reader.ReadLine();
			if(null == line) return "";
			return line.Trim();
		}
	}
}