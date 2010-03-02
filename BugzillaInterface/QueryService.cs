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
using System.Reflection;

namespace BugzillaInterface
{

	[Serializable()]
	[XmlInclude(typeof(ReportedByQuery))]
	public class BaseQuery
	{
		public SearchParams queryParameters;

		public int SourceId { get; set; }

		[XmlIgnore()]
		public Repository Source { get; set; }

		[XmlIgnore()]
		public IBugAPI bugProxy { get; set; }

		public string Title { get; set; }

		public List<BugReport> GetQueryResults ()
		{
			SetSource (SplatterCore.Instance.Sources[SourceId]);
			Console.WriteLine ("Making query");
			List<BugReport> results = new List<BugReport> ();
			
			//Tracer trace = new Tracer();
			//trace.Attach(bugProxy);
			
			GetBugsResponse result = bugProxy.SearchBugs (queryParameters);
			Console.WriteLine ("Query finished with {0} results", result.bugs.Length);
			foreach (BugReport bug in result.bugs) {
				results.Add (bug);
			}
			
			return results;
		}
		public BaseQuery ()
		{
			queryParameters = new SearchParams ();
		}

		public BaseQuery (Repository source)
		{
		}

		public void SetSource (Repository source)
		{
			if (bugProxy == null) {
				Console.WriteLine ("Configuring base query");
				Source = source;
				bugProxy = XmlRpcProxyGen.Create<IBugAPI> ();
				bugProxy = (IBugAPI)Source.ConfigureXmlRpcProxy (bugProxy);
			}
		}
	}

	[Serializable()]
	public class ReportedByQuery : BaseQuery
	{
		public string ReportedByName { get; set; }

		public void Configure ()
		{
			ReportedByName = "balloonbending@yahoo.com";
			queryParameters.assigned_to = ReportedByName;
			queryParameters.status = new String[] { "RESOLVED", "ASSIGNED" };
		}

		public ReportedByQuery (Repository source) : base(source)
		{
			
		}

		public ReportedByQuery ()
		{
			
		}
	}

	public static class QueryService
	{
		public static Type[] QueryTypes = new Type[] { typeof(BaseQuery), typeof(ReportedByQuery) };
	}

	public class Query
	{
		[XmlArray("bugs")]
		[XmlArrayItem("bug", typeof(BugReport))]
		public List<BugReport> Bugs { get; set; }

		[XmlArray("bugids")]
		[XmlArrayItem("bugid", typeof(int))]
		public List<int> BugIds { get; set; }

		public BaseQuery Generator { get; set; }

		[XmlIgnore()]
		public Repository Source { get; set; }

		public int SourceID { get; set; }

		public Query ()
		{
			Bugs = new List<BugReport> ();
			BugIds = new List<int> ();
			Generator = new BaseQuery ();
		}

		public Query (Repository source)
		{
			Source = source;
		}

		public void TestStuff ()
		{
			Source = SplatterCore.Instance.Sources[SourceID];
			BaseQuery query1 = new BaseQuery ();
			Generator = query1;
			query1.SourceId = SourceID;
			//query1.Configure();
			List<BugReport> results = query1.GetQueryResults ();
			foreach (BugReport bug in results) {
				Console.WriteLine (bug.ToString ());
				Bugs.Add (bug);
				BugIds.Add (bug.id);
			}
		}

		/// <summary>
		/// Runs the Query and returns the number of results
		/// </summary>
		/// <returns>
		/// A <see cref="System.Int32"/>. -1 if failure, 0 - n if success
		/// </returns>
		public int Execute ()
		{
			Source = SplatterCore.Instance.Sources[SourceID];
			try {
				List<BugReport> results = Generator.GetQueryResults ();
				if(Bugs == null || BugIds == null)
				{
					BugIds = new List<int>();
					Bugs = new List<BugReport>();
				}
				foreach (var bug in results) {
					
					if(BugIds.Contains(bug.id))
					{
						Console.WriteLine ("Found old bug: " + bug.ToString());
						BugReport toMerge = Bugs[BugIds.IndexOf(bug.id)];
						
						List<Comment> oldComments, newComments;
						if(toMerge.last_change_time != bug.last_change_time)
						{
							// Case 1: The bug has been modified
							Console.WriteLine ("Bug has been modified");
							
							// back up the old comments
							oldComments = new List<Comment>();
							oldComments.AddRange(toMerge.Comments);
							
							// assign to the bug
							BugReport newBugReport = bug; // Try to avoid any late binding issues
							Bugs[BugIds.IndexOf(bug.id)] = newBugReport;
							
							// Set the old comments back
							int targetBugId = BugIds.IndexOf(bug.id);
							
							Bugs[targetBugId].setComments( oldComments );
						}
						else
						{
							newComments = bug.Comments;
							Console.WriteLine ("Bug has not been modified");
						}
						
						toMerge = Bugs[BugIds.IndexOf(bug.id)];
						//toMerge.MergeComments(newComments);
					}
					else
					{
						Console.WriteLine ("Found new bug");
						Bugs.Add(bug);
						BugIds.Add(bug.id);
					}
				}
				return Bugs.Count;
			} finally {
			}
			return -1;
		}

		public void FetchComments ()
		{
			GetCommentsParams commentParameters = new GetCommentsParams ();
			commentParameters.ids = BugIds.ToArray ();
			
			for (int i = 0; i < Bugs.Count; i++) {
				Bugs[i].ResetComments ();
			}
			
			Console.WriteLine ("Fetching comments");
			
			// Set the source if it hasn't been set alreayd
			Generator.SetSource (SplatterCore.Instance.Sources[this.SourceID]);
			
			//Tracer trace = new Tracer();
			//trace.Attach(Generator.bugProxy);
			XmlRpcStruct comResponse = Generator.bugProxy.GetComments (commentParameters);
			Console.WriteLine ("Done!");
			
			// Okay, this is a ridiculous hack; I have no clue what it does. I wrote it last week;
			XmlRpcStruct bug_comments = (XmlRpcStruct)comResponse["bugs"];
			foreach (XmlRpcStruct bug1_comments in bug_comments.Values) {
				object[] comment_list = (object[])bug1_comments["comments"];
				
				List<Comment> targetCommentList = new List<Comment>();
				
				foreach (XmlRpcStruct comment in comment_list) {
					//Console.WriteLine(comment["text"].ToString());
					Comment obj1 = (Comment)AttemptStructDeserialization (comment, typeof(Comment));
					Comment target = (Comment)obj1;
					// find the bug that's in the bug list of queries
					
					if (BugIds.Contains (target.bug_id)) {
						
						// I have absolutely no clue what I'm doing here
						if(! Bugs[BugIds.IndexOf (target.bug_id)].Comments.Exists(delegate(Comment c1){
							return target.id == c1.id;
						}))
						{
							Bugs[BugIds.IndexOf (target.bug_id)].Comments.Add(target);
							BugReport temp = Bugs[BugIds.IndexOf (target.bug_id)];
							temp.NewCommentFlag = true;
							Console.WriteLine ("Found a new comment" + target.ToString());
						}
						else
						{
							// no new comment
						}
						
					}
				}
			}
			
		}
		
		


		/// <summary>
		/// Uses Reflection to deserialize objects from hashtables and structs. Specifically tested
		/// for use with the comment objects
		/// </summary>
		/// <param name="response">
		/// A <see cref="XmlRpcStruct"/>
		/// </param>
		/// <param name="output">
		/// A <see cref="Type"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Object"/>
		/// </returns>
		public static object AttemptStructDeserialization (XmlRpcStruct response, Type output)
		{
			MemberInfo[] outputMembers = output.GetMembers ();
			object result = Activator.CreateInstance (output);
			PropertyInfo[] properties = output.GetProperties ();
			foreach (PropertyInfo property in properties) {
				//Console.WriteLine (property.Name);
				if (response.ContainsKey (property.Name)) {
					//Console.WriteLine("Declaring type:{0}, output type:{1}", property.PropertyType, response[property.Name].GetType());
					if (Type.Equals (property.PropertyType, response[property.Name].GetType ())) {
						//Console.WriteLine ("Identical Type! Woot! Value is " + response[property.Name]);
						property.SetValue (result, response[property.Name], null);
					}
				}
			}
			return result;
		}


		public void TestLoggedInStuff ()
		{
			foreach (BugReport bug in Bugs) {
				Console.WriteLine (bug.ToString ());
			}
		}

		public void PostDeserialize ()
		{
			if (Generator != null && Source != null) {
				Console.WriteLine ("Doing post deser for query");
				Generator.SetSource (Source);
			}
		}
		
	}
}

