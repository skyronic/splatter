using System;
using BugzillaInterface;
using CookComputing.XmlRpc;
using System.Net;
using System.Collections;
using System.Collections.Generic;


namespace Splatter
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
//			IProductAPI proxy = XmlRpcProxyGen.Create<IProductAPI>();
//			proxy.Proxy = new WebProxy("http://144.16.192.245:8080");
//			
//			proxy.Url = "https://landfill.bugzilla.org/bugzilla-3.4-branch/xmlrpc.cgi";
//			XmlRpcStruct acc_products = proxy.GetAccessibleProducts();
//			int[] productIds = (int[])acc_products["ids"];
//			Console.WriteLine(productIds.ToString());
//			
//			//object o1 = proxy.GetProducts(acc_products);
//			//Console.WriteLine (o1);
//			
//			ProductList products = proxy.GetProducts(acc_products);
//			foreach(Product prod in products.products)
//			{
//				Console.WriteLine(prod.ToString());
//			}
			
//			UserService userService = new UserService("https://landfill.bugzilla.org/bugzilla-3.4-branch/xmlrpc.cgi");
//			
//			userService.TryLogin("anirudh@anirudhsanjeev.org", "opeth");
//			
//			BugService bugService = new BugService("https://landfill.bugzilla.org/bugzilla-3.4-branch/xmlrpc.cgi");
//			bugService.LoginAsUser(userService);
//			bugService.DoSomething();
			
			//SplatterCore.Instance.TestStuff();
			SplatterCore.LoadState();
			SplatterCore.Instance.TestLoggedInStuff();
		}
	}
}

