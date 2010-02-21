// 
// QueryService.cs
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
using System.Xml.Serialization;

namespace BugzillaInterface
{
	
	[Serializable()]
	[XmlInclude(typeof(ReportedByQuery))]
	public class BaseQuery
	{
		public SearchParams queryParameters;
		
		public int SourceId{get;set;}
		
		[XmlIgnore()]
		public Repository Source{get;set;}
		
		[XmlIgnore()]
		public IBugAPI bugProxy{get;set;}
		
		public string Title{get{
				return "Random Query Name";}set{}}
		
		public List<BugReport> GetQueryResults()
		{
			SetSource(SplatterCore.Instance.Sources[SourceId]);
			Console.WriteLine ("Making query");
			List<BugReport> results = new List<BugReport>();
			
			//Tracer trace = new Tracer();
			//trace.Attach(bugProxy);
			
			GetBugsResponse result = bugProxy.SearchBugs(queryParameters);
			Console.WriteLine ("Query finished with {0} results", result.bugs.Length);
			foreach(BugReport bug in result.bugs)
			{
				results.Add(bug);				
			}
			
			return results;
		}
		public BaseQuery()
		{
			Title = "Random query name";
			queryParameters = new SearchParams();
		}
		
		public BaseQuery(Repository source)
		{
		}
		
		public void SetSource (Repository source)
		{
			Console.WriteLine("Configuring base query");
			Source = source;
			bugProxy = XmlRpcProxyGen.Create<IBugAPI>();
			bugProxy = (IBugAPI)Source.ConfigureXmlRpcProxy(bugProxy);
		}
	}
	
	[Serializable()]
	public class ReportedByQuery : BaseQuery
	{
		public string ReportedByName{get;set;}
		
		public void Configure ()
		{
			ReportedByName = "balloonbending@yahoo.com";
			queryParameters.assigned_to = ReportedByName;
			queryParameters.status = new String[]{"RESOLVED", "ASSIGNED"};
		}		
		
		public ReportedByQuery(Repository source) : base(source)
		{
			
		}
		
		public ReportedByQuery()
		{
			
		}
	}
	
	public static class QueryService
	{
		public static Type[] QueryTypes = new Type[]{typeof(BaseQuery), typeof(ReportedByQuery)};
	}
	
	public class Query
	{
		[XmlArray("bugs")]
		[XmlArrayItem("bug", typeof(BugReport))]
		public List<BugReport> Bugs{get;set;}
		
		[XmlArray("bugids")]
		[XmlArrayItem("bugid", typeof(int))]
		public List<int> BugIds{get;set;}		
		
		public BaseQuery Generator{get;set;}
		
		[XmlIgnore()]
		public Repository Source{get;set;}
		
		public int SourceID{get;set;}
		
		public Query()
		{
			Bugs = new List<BugReport>();
			BugIds = new List<int>();
		}
		
		public Query(Repository source)
		{
			Source = source;
		}
		
		public void TestStuff()
		{
			Source = SplatterCore.Instance.Sources[SourceID];
			BaseQuery query1 = new BaseQuery();
			Generator = query1;
			query1.SourceId = SourceID;
			//query1.Configure();
			List<BugReport> results = query1.GetQueryResults();
			foreach(BugReport bug in results)
			{
				Console.WriteLine (bug.ToString());
				Bugs.Add(bug);
				BugIds.Add(bug.id);
			}
		}
		
		public void TestLoggedInStuff()
		{
			foreach (BugReport bug in Bugs) {
				Console.WriteLine (bug.ToString());
			}
		}
		
		public void PostDeserialize()
		{
			if(Generator != null && Source != null)
			{
				Console.WriteLine ("Doing post deser for query");
				Generator.SetSource(Source);
			}
		}
		
	}
}

