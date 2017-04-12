using System.Data.SqlClient;
using System.Text;
using System;
using System.Xml;
using System.Collections.Generic;

namespace test1
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			string p = ParseRssFile();
			Console.WriteLine(p);
		}

		public static string ParseRssFile()
		{

			Console.WriteLine("testing");
			XmlDocument rssXmlDoc = new XmlDocument();

			// Load the RSS file from the RSS URL
			rssXmlDoc.Load("http://www.turfclub.com.sg/_layouts/RssGeneration.aspx?ModuleName=RD");

			// Parse the Items in the RSS file
			XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

			StringBuilder rssContent = new StringBuilder();



			// Iterate through the items in the RSS file
			List<itemResult> item = new List<itemResult>();
			foreach (XmlNode rssNode in rssNodes)
			{
				XmlNode rssSubNode = rssNode.SelectSingleNode("title");
				string title = rssSubNode != null ? rssSubNode.InnerText : "";

				rssSubNode = rssNode.SelectSingleNode("description");
				string description = rssSubNode != null ? rssSubNode.InnerText : "";

				rssSubNode = rssNode.SelectSingleNode("link");
				string link = rssSubNode != null ? rssSubNode.InnerText : "";

				rssSubNode = rssNode.SelectSingleNode("category");
				string category = rssSubNode != null ? rssSubNode.InnerText : "";

				rssSubNode = rssNode.SelectSingleNode("guid");
				string guid = rssSubNode != null ? rssSubNode.InnerText : "";

				rssSubNode = rssNode.SelectSingleNode("pubDate");
				string pubDate = rssSubNode != null ? rssSubNode.InnerText : "";

				itemResult newIt = new itemResult();
				newIt.title = title;
				newIt.decsription = description;
				newIt.link = link;
				newIt.category = category;
				newIt.guid = guid;
				newIt.gpubDate = pubDate;

				item.Add(newIt);

				Console.WriteLine(item.Count);



			}


			//foreach (itemResult k in item) {
				
			//	Insert(k.title, k.decsription, k.link, k.category, k.guid, k.gpubDate);
			//}


			string s = item[3].decsription;
			// Split string on spaces.
			// ... This will separate all the words.
			string[] words = s.Split(new string[] { "<br/>" }, StringSplitOptions.None);
			int k = words.Length;
			Console.WriteLine("----------");

			Console.WriteLine(k);

			Console.WriteLine("----------");

			Console.WriteLine(words[0]);
			Console.WriteLine(words[1]);
			Console.WriteLine(words[2]);
			Console.WriteLine(words[3]);
			Console.WriteLine("----------");

			// Return the string that contain the RSS items
			return "hello C#";
		}

		public class raceResult
		{
			public string title { get; set; }
			public string decsription { get; set; }
			public string link { get; set; }
			public string language { get; set; }
			public string date { get; set; }
			public string lastbuild { get; set; }


		}

		private class itemResult
		{
			public string title { get; set; }
			public string decsription { get; set; }
			public string link { get; set; }
			public string category { get; set; }
			public string guid { get; set; }
			public string gpubDate { get; set; }


		}

		static void Insert(string title, string description, string link,  string categorie, string guid, string pubdate)
		{
			try
			{
				string connectionString =
					@"Server=192.168..;Database= "";User ID="";Password="";
				
				using (SqlConnection conn = 
				       new SqlConnection(connectionString)) 
				{
					conn.Open();
					using (SqlCommand cmd = 
					       new SqlCommand("INSERT INTO Turf VALUES(" + "@Title, @Description, @Link, @Categorie, @Guid, @PubDate)", conn))
					{
						cmd.Parameters.AddWithValue("@Title", title);
						cmd.Parameters.AddWithValue("@Description", description);
						cmd.Parameters.AddWithValue("@LINK", link);
						cmd.Parameters.AddWithValue("@Categorie", categorie);
						cmd.Parameters.AddWithValue("@Guid", guid);
						cmd.Parameters.AddWithValue("@Pubdate", pubdate);

						int rows = cmd.ExecuteNonQuery();

						// rows number of record got inserted
					}
					
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex);
			}
			
		}

	
	}
}
