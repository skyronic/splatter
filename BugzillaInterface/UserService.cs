// 
// UserService.cs
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
using System.Threading;



namespace BugzillaInterface
{
	// Create the class that interfaces with XML-RPC
	interface IUserAPI : IXmlRpcProxy
	{
		[XmlRpcMethod("User.login")]
		XmlRpcStruct Login(LoginParams parameters);
		
		[XmlRpcMethod("Bug.legal_values")]
		LegalValuesResponse LegalValues(XmlRpcStruct parameters);
	}
	
	public struct LegalValuesResponse
	{
		public string[] values;
	}
	
	public struct LoginParams
	{
		public string login;
		public string password;
		public bool remember;
	}
	
	
	[XmlRoot("user")]
	public class UserService
	{
		[XmlAttribute("url")]
		private string Url{get;set;}
		
		[XmlIgnore()]
		private IUserAPI userProxy;
		
		[XmlAttribute("loggedin")]
		public bool LoggedIn
		{get;set;}
		
		[XmlIgnore()]
		public CookieCollection LoginCookies{get;set;}
		
		[XmlIgnore()]
		public Dictionary<string,string[]> CookieDict{get;set;}
		
		public List<string[]> CookieList{get;set;}
		
		[XmlIgnore()]
		public Repository Source {
			get;
			set;
		}
		
		[XmlAttribute("proxy")]
		string Proxy;
		
		
		public UserService (string url)
		{
			Url = url;
			userProxy = XmlRpcProxyGen.Create<IUserAPI>();
			userProxy.Url = Url;
			userProxy.Proxy = new WebProxy("http://144.16.192.245:8080");
			LoggedIn = false;
		}
		
		// should be called only by the serializer
		public UserService()
		{
		}
		
		public List<string> GetLegalValues(string fieldName)
		{
			XmlRpcStruct parameters = new XmlRpcStruct();
			parameters.Add("field", fieldName);
			//userProxy.Expect100Continue = true;
			//userProxy.EnableCompression = false;
			
			Console.WriteLine ("Retrieving legal values for " + fieldName);
			LegalValuesResponse response = userProxy.LegalValues(parameters);
			
			List<string> result = new List<string>();
			foreach(string item in response.values)
			{
				result.Add(item);
				Console.Write(item + ",");
			}
			Console.WriteLine("done");
			Thread.Sleep(1000);
			return result;
		}
		
		public UserService (Repository source)
		{
			Source = source;
			userProxy = XmlRpcProxyGen.Create<IUserAPI>();
			userProxy.Url = source.Url;
			Console.WriteLine ("The url is " + userProxy.Url);
			if(source.Proxy != "")
			{
				Console.WriteLine ("The proxy is :" + source.Proxy);
				userProxy.Proxy = new WebProxy(source.Proxy);
			}
			LoggedIn = false;
		}
		
		public bool TryLogin(string login, string password)
		{
			Console.WriteLine ("Logging in");
			LoginParams loginParams = new LoginParams();
			loginParams.login = login;
			loginParams.password = password;
			loginParams.remember = false;
			try
			{
				XmlRpcStruct loginResult = userProxy.Login(loginParams);
			}
			catch(XmlRpcFaultException ex)
			{
				if(ex.FaultCode == 300)
				{
					Console.WriteLine ("Username invalid");
					return false;
				}				
			}
			catch(Exception ex)
			{
				Console.WriteLine ("Unknown error" + ex.Message);
				return false;
			}
			
			// If the code reaches here, the login is valid
			Console.WriteLine ("Login succeeded");
			
			// yum :)
			LoginCookies = userProxy.ResponseCookies;
			
			CookieDict = new Dictionary<string, string[]>();
			CookieList = new List<string[]>();
			
			foreach(Cookie c in LoginCookies)
			{
				//CookieDict[c.Name] = new string[] {c.Value, c.Path, c.Domain};
				CookieList.Add(new string[] {c.Name, c.Value, c.Path, c.Domain});
			}
			
			
			LoggedIn = true;
			
			return true;
		}
		
		public bool TryLogin()
		{
			return TryLogin(Source.UserName, Source.Password);
		}
		
	}
}

