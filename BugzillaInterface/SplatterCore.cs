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
				source.FetchLegalValues();
				Sources.Add(source);
				//Query q1 = new Query();
				//Queries.Add(q1);
				//q1.SourceID = 0;
				//q1.TestStuff();
				SaveState();
			}
		}
		
		public void TestLoggedInStuff ()
		{
			Query q1 = Queries[0];
			//q1.TestStuff();
			q1.TestLoggedInStuff();
		}
		
		const string configFileName = "config.xml";
		const string allowFileName = "allow.xml";
		
		public void SaveState()
		{
		
			/* The configuration regarding the various sources is stored in config.xml and the list of hosts who are
			 * allowed despite invalid certificates is stored in allowed.xml. This was done to allow one to easily 'reset'
			 * the security allowances by deleting a single file. */
		
			string configFolderRoot = Path.Combine (System.Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData), "splatter");
			
			if (!System.IO.Directory.Exists (configFolderRoot)) {
				System.IO.Directory.CreateDirectory (configFolderRoot);
			}
			
			string configFilePath = Path.Combine (configFolderRoot, configFileName);
			string allowedFilePath = Path.Combine (configFolderRoot, allowFileName);
			
			Console.WriteLine ("Saving configuration to " + configFilePath);
			Console.WriteLine ("Saving list of allowed thumbprints to " + configFilePath);
			
			/*XmlAttributes attrs = new XmlAttributes();
			
			TODO ??? Why is this here?
			
			XmlElementAttribute attr1 = new XmlElementAttribute("ReportedByQuery",typeof(ReportedByQuery));
			attrs.XmlElements.Add(attr1);
			
			XmlAttributeOverrides attrOverRides = new XmlAttributeOverrides();
			attrOverRides.Add(typeof(Query), "Generator", attrs);*/
			
			if(File.Exists (configFilePath)) {
				File.Move(configFilePath, configFilePath + ".bak");
				// Pray this works
			}		
			
			if(File.Exists (allowedFilePath)) {
				File.Move(allowedFilePath, allowedFilePath + ".bak");
			}		
			
			XmlSerializer configurationSerializer = new XmlSerializer(typeof(SplatterCore));
			FileStream configFile = new FileStream(configFilePath, FileMode.Create, FileAccess.Write);
			TextWriter configFileWriter = new StreamWriter(configFile);
			
			XmlSerializer thumbprintSerializer = new XmlSerializer(typeof(List<string>));
			FileStream allowedFile = new FileStream(allowedFilePath, FileMode.Create, FileAccess.Write);
			TextWriter allowedFileWriter = new StreamWriter(allowedFile);
			
			configurationSerializer.Serialize(configFileWriter, this);
			
			thumbprintSerializer.Serialize (allowedFileWriter, SecurityCertificateHandler.Instance.AllowedThumbPrints);
			
			configFileWriter.Close();
			configFile.Close();
			
			allowedFileWriter.Close();
			allowedFile.Close();
			
			/* @Andy Why delete the backup file at all ? */

		}
		

		public static void LoadState()
		{
		
			string configFolder = Path.Combine (System.Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData), "splatter");
			string filePath = Path.Combine (configFolder, configFileName);
			string allowedCertificatesPath = Path.Combine (configFolder, allowFileName);
			
			Console.WriteLine ("Loading configuration from " + filePath);
			Console.WriteLine ("Loading list of allowed untrusted connections from " + filePath);
			SecurityCertificateHandler.Initialize ();
			
			try {
				if(File.Exists(filePath)) {
				
					XmlSerializer ser = new XmlSerializer(typeof(SplatterCore));
					TextReader reader = new StreamReader(filePath);
					
					SplatterCore newCore = (SplatterCore)ser.Deserialize(reader);
						
					// TODO: is it safe to do this?
					
					SplatterCore.Instance = newCore;
					reader.Close();
						
				}
				
				if(File.Exists(allowedCertificatesPath)) {
				
					XmlSerializer thumbPrintsSerializer = new XmlSerializer (typeof (System.Collections.Generic.List <string>));
					TextReader reader = new StreamReader(allowedCertificatesPath);
					SecurityCertificateHandler.Instance.AllowedThumbPrints = (List <string>) thumbPrintsSerializer.Deserialize (reader);
					
					reader.Close();
						
				}
				
			} catch (XmlException exception) {
				/* Don't let bad xml do wreak havoc */
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
