// 
// SplatterCore.cs
//  
// Author:
//       Anirudh Sanjeev <anirudh@anirudhsanjeev.org>
// 
// Copyright (c) 2010 Anirudh Sanjeev
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// The Software shall be used for Good, not Evil.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using CookComputing.XmlRpc;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Xml;
using System.Text;

namespace BugzillaInterface
{
	[XmlRoot("SplatterCore")]
	public sealed class SplatterCore
	{
		public static SplatterCore Instance = new SplatterCore ();
		
		
		[XmlArray("sources")]
		[XmlArrayItem("source", typeof(Repository))]
		public List<Repository> Sources { get; set; }
		
		[XmlArray("queries")]
		[XmlArrayItem("query", typeof(Query))]
		public List<Query> Queries { get; set; }
		
		public int Loaded{get;private set;}
		public SplatterCore ()
		{
			if(Loaded != 50)
			{
				Sources = new List<Repository> ();
				Queries = new List<Query> ();
				Loaded = 50;
			}
			else
			{
				Console.WriteLine ("Deserialization");
			}
		}

		public void TestStuff ()
		{
			// Create a new repository and log in
			Repository source = new Repository ();
			source.Name = "Bugzilla Landfill";
			source.Url = "https://landfill.bugzilla.org/bugzilla-3.4-branch/xmlrpc.cgi";
			source.UserName = "anirudh@anirudhsanjeev.org";
			source.Password = "opeth";
			// not my real password, don't worry
			source.Proxy = "http://10.3.100.211:8080";
			if (source.LoginAndVerify ()) {
				Sources.Add(source);
				Query q1 = new Query();
				Queries.Add(q1);
				q1.SourceID = 0;
				q1.TestStuff();
				SaveState();
			}
		}
		public void TestLoggedInStuff ()
		{
			Query q1 = Queries[0];
			//q1.TestStuff();
			q1.TestLoggedInStuff();
		}
		
		public void SaveState()
		{
			string filePath = "huha.xml"; // In true kgp style
			if(File.Exists (filePath))
			{
				File.Move(filePath, filePath + ".bak");
				// pray this works
			}
			
			XmlSerializer ser = new XmlSerializer(typeof(SplatterCore));
			FileStream outfile = new FileStream(filePath, FileMode.Create, FileAccess.Write);
			TextWriter textWriter = new StreamWriter(outfile);
			ser.Serialize(textWriter, this);
			textWriter.Close();
			outfile.Close();
			
			if(File.Exists(filePath + ".bak"))
			{
				File.Delete(filePath + ".bak");
			}
		}
		
		public static void LoadState()
		{
			string filePath = "huha.xml";
			if(File.Exists(filePath))
			{
				XmlSerializer ser = new XmlSerializer(typeof(SplatterCore));
				TextReader reader = new StreamReader(filePath);
				
				SplatterCore newCore = (SplatterCore)ser.Deserialize(reader);
				
				// TODO: is it safe to do this?
				SplatterCore.Instance = newCore;
				reader.Close();
			}
		}
		
		public void PostDeserialize()
		{
			
		}
	}

	public class Tracer : XmlRpcLogger
	{
		protected override void OnRequest (object sender, XmlRpcRequestEventArgs e)
		{
			DumpStream (e.RequestStream);
		}

		protected override void OnResponse (object sender, XmlRpcResponseEventArgs e)
		{
			DumpStream (e.ResponseStream);
		}

		private void DumpStream (Stream stm)
		{
			stm.Position = 0;
			TextReader trdr = new StreamReader (stm);
			String s = trdr.ReadLine ();
			while (s != null) {
				Console.WriteLine (s);
				s = trdr.ReadLine ();
			}
		}
	}
	
}

