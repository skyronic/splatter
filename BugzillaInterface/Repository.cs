// 
// Repository.cs
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
using System.Net;
using System.Xml.Serialization;

using System.Collections.Generic;
namespace BugzillaInterface
{
	public class Repository
	{
		public string Name {
			get;
			set;
		}
		
		public string Url {
			get;
			set;
		}
		
		public string UserName {
			get;
			set;
		}
		
		public string Password {
			get;
			set;
		}
		
		public string Proxy {
			get;
			set;
		}
		
		public bool Verified  {
			get;
			private set;
		}
		
		public UserService User  {
			get;
			private set;
		}
		
		public List<string> componentList{get;set;}
		public List<string> platformList{get;set;}
		public List<string> productList{get;set;}
		public List<string> resolutionList{get;set;}
		public List<string> severityList{get;set;}
		public List<string> statusList{get;set;}
		public List<string> milestoneList{get;set;}
		public List<string> versionList{get;set;}
		public List<string> priorityList{get;set;}
		//public List<string> versionList{get;set;}
		//public List<string> versionList{get;set;}
		
		public Repository ()
		{
			// This should be done in case it's deserialized
			if(Name != "")
			{
				//User = new UserService(this);
				Console.WriteLine ("Deserializing. Yeah!");
			}
		}
		
		public bool LoginAndVerify()
		{
			User = new UserService(this);
			if(!Verified && !User.LoggedIn)
			{
				if(User.TryLogin())
				{
					Verified = true;
					return true;
				}
			}
			return false;
		}
		
		public void FetchLegalValues()
		{
			//platformList = User.GetLegalValues("platform");
			resolutionList = User.GetLegalValues("resolution");
			severityList = User.GetLegalValues("severity");
			statusList = User.GetLegalValues("status");
			priorityList = User.GetLegalValues("priority");
			
		}
		
		public IXmlRpcProxy ConfigureXmlRpcProxy(IXmlRpcProxy proxy)
		{
			proxy.Url = this.Url;
			proxy.Proxy = new WebProxy(this.Proxy);
			if(Verified && User.LoggedIn)
			{
				// Store the login cookies
				/*foreach(Cookie c in User.LoginCookies)
				{
					proxy.CookieContainer.Add(c);
				}*/
				
				/*foreach(KeyValuePair<string, string[]> item in User.CookieDict)
				{
					proxy.CookieContainer.Add(new Cookie(item.Key, item.Value[0], item.Value[1], item.Value[2]));
				}*/
				foreach(string[] item in User.CookieList)
				{
					proxy.CookieContainer.Add(new Cookie(item[0], item[1], item[2], item[3]));
				}
			}
			
			return proxy;
		}	
	}
}

