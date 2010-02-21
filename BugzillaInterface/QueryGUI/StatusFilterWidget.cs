// 
// StatusFilterWidget.cs
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
namespace BugzillaInterface
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class StatusFilterWidget : Gtk.Bin, IFilterWidget
	{
		#region IFilterWidget implementation
		public void SetFilterParams (ref SearchParams filter)
		{
			if(statusArray.Enabled)
			{
				filter.status = statusArray.GetSelected();				
			}
			
			if(resolutionArray.Enabled)
			{
				filter.resolution = resolutionArray.GetSelected();
			}
		}
		
		public void SetNewSourceID(int sourceID)
		{
			
			/*statusArray.Options = new String[]{
				"NEW","ASSIGNED","NEEDINFO","REOPENED","RESOLVED","VERIFIED","CLOSED"
			};
			statusArray.ColumnTitle = "Status";
			
			resolutionArray.Options = new String[]{
				"FIXED","INVALID","WONTFIX","NORESPONSE","UPSTREAM","FEATURE","DUPLICATE","WORKSFORME","MOVED"
			};
			resolutionArray.ColumnTitle = "Resolution";*/
			Repository source = SplatterCore.Instance.Sources[sourceID];
			
			statusArray.Options = source.statusList.ToArray();
			statusArray.ColumnTitle = "Status";
			
			resolutionArray.Options = source.resolutionList.ToArray();
			resolutionArray.ColumnTitle = "Resolution";
		}
		
		#endregion
		public StatusFilterWidget ()
		{
			this.Build ();
		}
	}
}

