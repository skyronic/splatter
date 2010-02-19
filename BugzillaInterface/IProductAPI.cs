// 
// IProductAPI.cs
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

namespace BugzillaInterface
{	
	public interface IProductAPI : IXmlRpcProxy
	{
		[XmlRpcMethod("Product.get_accessible_products")]
		XmlRpcStruct GetAccessibleProducts();
		
		[XmlRpcMethod("Product.get")]
		ProductList GetProducts(XmlRpcStruct ids);
	}
	
	public struct ProductList
	{
		public Product[] products;
	}
	
	public struct Product
	{
		public int id{get;set;}
		public string name{get;set;}
		public string description{get;set;}
		public object internals{get;set;}
		public override string ToString ()
		{
			return string.Format("[Product: id={0}, name={1}, description={2}, internals={3}]", id, name, description, internals);
		}

	}
}

