// 
// MainWindow.cs
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
using Gtk;
using BugzillaInterface;

namespace Frontend
{
	public partial class MainWindow : Gtk.Window
	{
		
		TreeStore bugStore;
		public MainWindow () : base(Gtk.WindowType.Toplevel)
		{
			Build ();
			TreeViewColumn idColumn = new TreeViewColumn();
			CellRendererText idColumnCell = new CellRendererText();
			idColumn.PackStart(idColumnCell, true);
			idColumn.AddAttribute(idColumnCell, "text", 0);
			
			
			TreeViewColumn productColumn = new TreeViewColumn();
			CellRendererText productColumnCell = new CellRendererText();
			productColumn.PackStart(productColumnCell, true);
			productColumn.AddAttribute(productColumnCell, "text", 1);
			
			
			TreeViewColumn severityColumn = new TreeViewColumn();
			CellRendererText severityColumnCell = new CellRendererText();
			severityColumn.PackStart(severityColumnCell, true);
			severityColumn.AddAttribute(severityColumnCell, "text", 2);
			
			
			TreeViewColumn statusColumn = new TreeViewColumn();
			CellRendererText statusColumnCell = new CellRendererText();
			statusColumn.PackStart(statusColumnCell, true);
			statusColumn.AddAttribute(statusColumnCell, "text", 3);
			
			
			TreeViewColumn summaryColumn = new TreeViewColumn();
			CellRendererText summaryColumnCell = new CellRendererText();
			summaryColumn.PackStart(summaryColumnCell, true);
			summaryColumn.AddAttribute(summaryColumnCell, "text", 4);
			
			
			
			treeview1.AppendColumn(idColumn);
			treeview1.AppendColumn(productColumn);
			treeview1.AppendColumn(severityColumn);
			treeview1.AppendColumn(statusColumn);
			treeview1.AppendColumn(summaryColumn);
			
			bugStore = new TreeStore(typeof(string),typeof(string),typeof(string),typeof(string),typeof(string));
			treeview1.Model = bugStore;
			SplatterCore.LoadState();
			
			SyncTreeviewWithBugs();
			
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}
		protected virtual void AddNewQueryClicked (object sender, System.EventArgs e)
		{
			AddQueryDialog addQuery = new AddQueryDialog ();
		}

		protected virtual void RefreshQueriesBuggonClicked (object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// Updates the entire treeview with the latest bugs
		/// </summary>
		protected void SyncTreeviewWithBugs ()
		{
			Console.WriteLine ("Syncing treeview with bugs " + SplatterCore.Instance.Queries.Count);
			foreach(Query q in SplatterCore.Instance.Queries)
			{
				Console.WriteLine ("Adding new Query ");
				TreeIter queryIter = bugStore.AppendValues(q.Generator.Title);
				foreach(BugReport bug in q.Bugs)
				{
					bugStore.AppendValues(queryIter, bug.id.ToString(), bug.product, bug.severity, bug.status, bug.summary);
				}
			}
		}
		
		
		
	}
}
