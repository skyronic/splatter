// 
// BugService.cs
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
using CookComputing.XmlRpc;
using System.Collections.Generic;
using System.Net;
using System.Collections;
using System.Reflection;

namespace BugzillaInterface
{
	
	public interface IBugAPI : IXmlRpcProxy
	{
		[XmlRpcMethod("Bug.get")]
		GetBugsResponse GetBugs(GetBugsParams parameters);
		
		[XmlRpcMethod("Bug.comments")]
		XmlRpcStruct GetComments(GetCommentsParams parameters);
		
		[XmlRpcMethod("Bug.search")]
		GetBugsResponse SearchBugs(SearchParams parameters);
		
	}
	
	public struct GetBugsParams
	{
		public int[] ids;
		public bool permissive;
	}
	
	public struct GetBugsResponse
	{
		public BugReport[] bugs;
	}
	
	public struct BugReport
	{
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string alias{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string assigned_to{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string component{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public DateTime creation_time{get;set;}

		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int dupe_of{get;set;}
			
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int id{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public bool is_open{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public DateTime last_changed_time{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string priority{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string product{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string resolution{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string severity{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string status{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string summary{get;set;}
		
		
		public override string ToString ()
		{
			return string.Format("[BugReport: alias={0}, assigned_to={1}, component={2}, creation_time={3}, dupe_of={4}, id={5}, is_open={6}, last_changed_time={7}, priority={8}, product={9}, resolution={10}, severity={11}, status={12}, severity={13}, summary={14}]", alias, assigned_to, component, creation_time, dupe_of, id, is_open, last_changed_time, priority, product, resolution, severity, status, severity, summary);
		}
	}
	
	public struct Comment
	{
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int id{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int bug_id{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string text{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string author{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public DateTime time{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int is_private{get;set;}
		
		public override string ToString ()
		{
			return string.Format("[Comment: id={0}, bug_id={1}, text={2}, author={3}, time={4}, is_private={5}]", id, bug_id, text, author, time, is_private);
		}
	}
	
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct SearchParams
	{
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string alias{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string assigned_to{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string component{get;set;}
		
		public DateTime? creation_time;
		
		public int? id;
		
		public int? limit;
		
		public int? offset;
		
		public DateTime? last_change_time;
		
		//[XmlRpcMissingMapping(MappingAction.Ignore)]
		//public int limit{get;set;}
		
		//[XmlRpcMissingMapping(MappingAction.Ignore)]
		//public int offset{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string op_sys{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string platform{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string priority{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string product{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string reporter{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string resolution{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string severity{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string status{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string summary{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string qa_contact{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string url{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string version{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string votes{get;set;}
		
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string whiteboard{get;set;}
	}
	
	public struct GetCommentsParams
	{
		public int[] ids;
		public DateTime new_since;
	}
	
	public struct GetCommentsResponse
	{
		//[XmlRpcMissingMapping(MappingAction.Ignore)]
		//public BugReport[] bugs;
		public XmlRpcStruct bugs;
		public XmlRpcStruct comments;
	}

	
	public class BugService
	{
		IBugAPI bugProxy;
		UserService User{get;set;}
		public BugService (string url)
		{
			bugProxy = XmlRpcProxyGen.Create<IBugAPI>();
			bugProxy.Url = url;
			bugProxy.Proxy = new WebProxy("http://144.16.192.245:8080");
		}
		
		public void LoginAsUser(UserService user)
		{
			if(user.LoggedIn)
			{
				// store the ref to the user so we can get the cookies later
				User = user;
				
				// TODO: should we be doing this later?
				foreach(Cookie c in user.LoginCookies)
				{
					bugProxy.CookieContainer.Add(c);
				}
			}
		}
		
		public static object AttemptStructDeserialization(XmlRpcStruct response, Type output)
		{
			MemberInfo[] outputMembers = output.GetMembers();
			object result = Activator.CreateInstance(output);
			PropertyInfo[] properties = output.GetProperties();
			foreach(PropertyInfo property in properties)
			{
				/*Console.WriteLine ("Membername: " + member.Name);
				if(response.ContainsKey(member.Name))
				{
					Console.WriteLine ("Found a member with the key");
					
				}*/
				Console.WriteLine (property.Name);
				if(response.ContainsKey (property.Name))
				{
					Console.WriteLine("Declaring type:{0}, output type:{1}", property.PropertyType, response[property.Name].GetType());
					if(Type.Equals(property.PropertyType, response[property.Name].GetType()))
					{
						Console.WriteLine ("Identical Type! Woot! Value is " + response[property.Name]);
						property.SetValue(result, response[property.Name], null);
					}
				}
			}
			return result;
		}
		
		// Todo: remove
		public void DoSomething()
		{
			GetBugsParams reqParams;
			reqParams.permissive = false;
			reqParams.ids = new Int32[]{7861, 112, 6187};
			GetCommentsParams comParams = new GetCommentsParams();
			comParams.ids = reqParams.ids;
			//AttemptStructDeserialization(null, typeof(Comment));
			
			GetBugsResponse response = bugProxy.GetBugs(reqParams);
			XmlRpcStruct comResponse = bugProxy.GetComments(comParams);
			
			foreach(BugReport bug in response.bugs)
			{
				Console.WriteLine (bug.ToString());
			}
			
			XmlRpcStruct bug_comments = (XmlRpcStruct)comResponse["bugs"];
			foreach(XmlRpcStruct bug1_comments in bug_comments.Values)
			{
				object[] comment_list = (object[])bug1_comments["comments"];
				foreach(XmlRpcStruct comment in comment_list)
				{
					//Console.WriteLine(comment["text"].ToString());
					object obj1 = AttemptStructDeserialization(comment, typeof(Comment));
					if(obj1 !=null)
					{
						Console.WriteLine (obj1.ToString());
					}
				}
			}
		}
			
	}
}

