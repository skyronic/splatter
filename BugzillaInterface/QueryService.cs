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

namespace BugzillaInterface
{
	public abstract class BaseQuery
	{
		public SearchParams queryParameters;
		public Repository Source{get;set;}
		public IBugAPI bugProxy{get;set;}
		
		public List<BugReport> GetQueryResults()
		{
			Console.WriteLine ("Making query");
			List<BugReport> results = new List<BugReport>();
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
		}
		
		public abstract void Configure();
		
		public BaseQuery(Repository source)
		{
			Console.WriteLine("Configuring base query");
			queryParameters = new SearchParams();
			Source = source;
			bugProxy = XmlRpcProxyGen.Create<IBugAPI>();
			bugProxy = (IBugAPI)Source.ConfigureXmlRpcProxy(bugProxy);
		}
	}
	
	public class ReportedByQuery : BaseQuery
	{
		public string ReportedByName{get;set;}
		
		public override void Configure ()
		{
			ReportedByName = "balloonbending@yahoo.com";
			queryParameters.assigned_to = ReportedByName;
		}		
		
		public ReportedByQuery(Repository source) : base(source)
		{
			
		}
	}
	
	public class QueryService
	{
		public Type[] QueryTypes{get;set;}
		public QueryService ()
		{
		}
	}
	
	public class Query
	{
		public List<BugReport> Bugs{get;set;}
		public List<int> BugIds{get;set;}
		public BaseQuery Generator{get;set;}
		
		public Repository Source{get;set;}
		
		public Query()
		{
		}
		
		public Query(Repository source)
		{
			Source = source;
		}
		
		public void TestStuff()
		{
			BaseQuery query1 = new ReportedByQuery(Source);
			query1.Configure();
			List<BugReport> results = query1.GetQueryResults();
			foreach(BugReport bug in results)
			{
				Console.WriteLine (bug.ToString());
			}
		}
		
	}
}

