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
namespace BugzillaInterface
{
	// Create the class that interfaces with XML-RPC
	interface IUserAPI : IXmlRpcProxy
	{
		[XmlRpcMethod("User.login")]
		XmlRpcStruct Login(LoginParams parameters);
	}
	
	public struct LoginParams
	{
		public string login;
		public string password;
		public bool remember;
	}
	
	
	public class UserService
	{
		private string Url{get;set;}
		private IUserAPI userProxy;
		public bool LoggedIn
		{get;set;}
		public CookieCollection LoginCookies{get;set;}
		
		
		public UserService (string url)
		{
			Url = url;
			userProxy = XmlRpcProxyGen.Create<IUserAPI>();
			userProxy.Url = Url;
			userProxy.Proxy = new WebProxy("http://144.16.192.245:8080");
			LoggedIn = false;
		}
		
		public bool TryLogin(string login, string password)
		{
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
				Console.WriteLine ("Unknown error");
				return false;
			}
			
			// If the code reaches here, the login is valid
			Console.WriteLine ("Login succeeded");
			
			// yum :)
			LoginCookies = userProxy.ResponseCookies;
			
			LoggedIn = true;
			
			return true;
		}
		
	}
}

