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
			idColumn.Title = "ID";
			CellRendererText idColumnCell = new CellRendererText();
			idColumn.PackStart(idColumnCell, true);
			idColumn.AddAttribute(idColumnCell, "text", 0);
			
			
			TreeViewColumn productColumn = new TreeViewColumn();
			productColumn.Title = "Product";
			CellRendererText productColumnCell = new CellRendererText();
			productColumn.PackStart(productColumnCell, true);
			productColumn.AddAttribute(productColumnCell, "text", 1);
			
			
			TreeViewColumn severityColumn = new TreeViewColumn();
			severityColumn.Title = "Severity";
			CellRendererText severityColumnCell = new CellRendererText();
			severityColumn.PackStart(severityColumnCell, true);
			severityColumn.AddAttribute(severityColumnCell, "text", 2);
			
			
			TreeViewColumn statusColumn = new TreeViewColumn();
			statusColumn.Title = "Status";
			CellRendererText statusColumnCell = new CellRendererText();
			statusColumn.PackStart(statusColumnCell, true);
			statusColumn.AddAttribute(statusColumnCell, "text", 3);
			
			
			TreeViewColumn summaryColumn = new TreeViewColumn();
			summaryColumn.Title = "Summary";
			CellRendererText summaryColumnCell = new CellRendererText();
			summaryColumn.PackStart(summaryColumnCell, true);
			summaryColumn.AddAttribute(summaryColumnCell, "text", 0); // TODO: Change this?
			
			
			
			//treeview1.AppendColumn(idColumn);
			//treeview1.AppendColumn(productColumn);
			//treeview1.AppendColumn(severityColumn);
			//treeview1.AppendColumn(statusColumn);
			treeview1.AppendColumn(summaryColumn);
			
			
			treeview1.RowActivated += BugTreeRowActivated;
			
			bugStore = new TreeStore(typeof(string));
			treeview1.Model = bugStore;
			SplatterCore.LoadState();
			
			SyncTreeviewWithBugs();
			
		}

		void BugTreeRowActivated (object o, RowActivatedArgs args)
		{
			Console.WriteLine ("Row activated + " + args.Path.Indices.Length);
			
			// Check the length of the indices array in the path. We are ideally looking for something which is
			// in the first level of nesting.
			if(args.Path.Indices.Length != 1)
			{
				int queryIndex = args.Path.Indices[0];
				int bugIndex = args.Path.Indices[1];
				
				// Retrieve the bug
				BugReport target = SplatterCore.Instance.Queries[queryIndex].Bugs[bugIndex];
				Console.WriteLine ("Fetching comments for " + target.id);
				
				// Clear the comment VBox
				foreach(Widget w in commentVBox.Children)
				{
					commentVBox.Remove(w);
					w.Dispose();
				}
				
				foreach(var com in target.Comments)
				{
					Console.WriteLine (com.ToString());
					CommentSingletonWidget commentWidget = new CommentSingletonWidget();
					commentWidget.SetComment(com);
					commentWidget.ShowAll();
					
					
					commentVBox.PackStart(commentWidget, true, true, 0);
				}
				
				// Set the bug details
				int row_id = 0;
				
				bugPropertyTable.Resize(9, 2);
				
				foreach (Widget child in bugPropertyTable.Children) {
					bugPropertyTable.Remove(child);
				}
				
				StringValueToTable(bugPropertyTable, "Bug ID", target.id.ToString(), ref row_id);
				StringValueToTable(bugPropertyTable, "Component", target.component, ref row_id);
				StringValueToTable(bugPropertyTable, "Product", target.product, ref row_id);
				StringValueToTable(bugPropertyTable, "Assigned To", target.assigned_to, ref row_id);
				StringValueToTable(bugPropertyTable, "Resolution", target.resolution, ref row_id);
				StringValueToTable(bugPropertyTable, "Status", target.status, ref row_id);
				StringValueToTable(bugPropertyTable, "Severity", target.severity, ref row_id);
				StringValueToTable(bugPropertyTable, "Priority", target.priority, ref row_id);
				StringValueToTable(bugPropertyTable, "Created", target.creation_time.ToString(), ref row_id);
				
			}
		}
		
		protected void StringValueToTable (Table container, string name, string valueText, ref int row)
		{
			Label targetLabel = new Label();
			Entry target = new Entry();
			
			target.Text = valueText;
			target.Sensitive = false;
			target.Show();
			
			targetLabel.Text = name;
			targetLabel.Xalign = 0;
			targetLabel.Xpad = 13;
			targetLabel.Show();
			
			container.Attach(targetLabel, (uint)0, (uint)1, (uint)row, (uint)(row + 1), AttachOptions.Fill, AttachOptions.Fill, 0, 0);
			container.Attach(target, (uint)1, (uint)2, (uint)row, (uint)(row + 1), AttachOptions.Expand|AttachOptions.Fill, AttachOptions.Expand|AttachOptions.Fill, 0, 0);
			
			row += 1;
			Console.WriteLine ("Finished writing to row {0}", row);
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}
		protected virtual void AddNewQueryClicked (object sender, System.EventArgs e)
		{
			AddQueryDialog addQuery = new AddQueryDialog ();
			addQuery.ShowAll();
			addQuery.parentWindow = this;
		}

		protected virtual void RefreshQueriesBuggonClicked (object sender, System.EventArgs e)
		{
			int TotalQueries = SplatterCore.Instance.Queries.Count;
			if(TotalQueries > 0)
			{
				int index = 0;
				foreach (Query query in SplatterCore.Instance.Queries) {
					statusLabel.Text = "Refreshing query " + query.Generator.Title;
					progressbar1.Fraction = (double)(index + 1) / (double)(TotalQueries + 1);
					//query.Execute(); // TODO: Change back to this
					query.FetchComments();
					
				}
				progressbar1.Fraction = 1;
				statusLabel.Text = "Refresh complete!";
				
				// Save and sync
				SplatterCore.Instance.SaveState();
				//SyncTreeviewWithBugs();
			}
		}

		/// <summary>
		/// Updates the entire treeview with the latest bugs
		/// </summary>
		public void SyncTreeviewWithBugs ()
		{
			Console.WriteLine ("Syncing treeview with bugs " + SplatterCore.Instance.Queries.Count);
			// Clear all the items in the store
			bugStore.Clear();
			foreach(Query q in SplatterCore.Instance.Queries)
			{
				TreeIter queryIter = bugStore.AppendValues(q.Generator.Title);
				foreach(BugReport bug in q.Bugs)
				{
					//bugStore.AppendValues(queryIter, bug.id.ToString(), bug.product, bug.severity, bug.status, bug.summary);
					bugStore.AppendValues(queryIter, bug.summary);
				}
			}
		}
		
		protected virtual void DeleteQuery (object sender, System.EventArgs e)
		{
			TreePath[] selectedQueries = treeview1.Selection.GetSelectedRows();
			
			foreach(TreePath selected in selectedQueries)
			{
				if(selected.Indices.Length == 1)// Root node
				{
					int index = selected.Indices[0];
					
					Console.WriteLine ("Deleting query with index {0}", index);
					SplatterCore.Instance.Queries.RemoveAt(index);
					
					// Save state and sync state
					SplatterCore.Instance.SaveState();
					SyncTreeviewWithBugs();
				}
				
			}
		}
	}
}
