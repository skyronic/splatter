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
namespace BugzillaInterface
{
	public sealed class SplatterCore
	{
		public static readonly SplatterCore Instance = new SplatterCore ();

		public List<Repository> Sources { get; set; }
		public List<Query> Queries { get; set; }
		private SplatterCore ()
		{
			Sources = new List<Repository> ();
			Queries = new List<Query> ();
		}

		public void TestStuff ()
		{
			// Create a new repository and log in
			Repository source = new Repository ();
			source.Url = "https://landfill.bugzilla.org/bugzilla-3.4-branch/xmlrpc.cgi";
			source.UserName = "anirudh@anirudhsanjeev.org";
			source.Password = "opeth";
			// not my real password, don't worry
			source.Proxy = "http://144.16.192.247:8080";
			if (source.LoginAndVerify ()) {
				Query q1 = new Query (source);
				q1.TestStuff ();
			}
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

