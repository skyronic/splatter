// 
// AddQueryDialog.cs
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
using System.Collections.Generic;
namespace Frontend
{
	public partial class AddQueryDialog : Gtk.Dialog
	{
		protected virtual void AddSourceButtonClicked (object sender, System.EventArgs e)
		{
			NewSourceDialog addSource = new NewSourceDialog();
			addSource.parentDialog = this;
			addSource.ShowAll();
		}
		
		List<Widget> filterWidgets;
		List<string> filterNames;
		
		ListStore filterStore;
		
		/// <summary>
		/// Constructor
		/// </summary>
		public AddQueryDialog ()
		{
			this.Build ();
			UpdateAvailableSources();
			
			filterWidgets = new List<Widget>();
			filterNames = new List<string>();
			
			// 
			//
			// Add new widgets and names here
			//
			//
			
			filterWidgets.Add (new PeopleFilterWidget());
			filterNames.Add ("People");
			
			filterWidgets.Add(new ProductFilterWidget());
			filterNames.Add("Product");
			
			filterWidgets.Add(new StatusFilterWidget());
			filterNames.Add("Status");
			
			filterWidgets.Add(new ImportanceWidget());
			filterNames.Add("Importance");
			
			// set up the treeview columns
			TreeViewColumn filterColumn = new TreeViewColumn();
			CellRendererText filterRenderer = new CellRendererText();
			filterColumn.Title = "Filter";
			filterColumn.PackStart(filterRenderer, true);
			filterColumn.AddAttribute(filterRenderer, "text", 0);
			queryTreeView.AppendColumn(filterColumn);
			
			// set up the store and the treeview
			filterStore = new ListStore(typeof(string));
			queryTreeView.Model = filterStore;
			
			// Add the names to the treeview
			foreach(string name in filterNames)
			{
				Console.WriteLine ("Adding filter {0} to the list", name);
				filterStore.AppendValues(name);
			}
			
			// Add all the widgets into the hbox and hide them
			/*foreach(Widget filter in filterWidgets)
			{
				filter.HideAll();
				filter.Visible = false;
				filterContainer.PackStart(filter, false, false, 0);
			}
			filterContainer.Visible = false;
			filterContainer.HideAll();*/
		}
		
		
		protected virtual void CancelButtonClicked (object sender, System.EventArgs e)
		{
			this.HideAll();
			this.Dispose();
		}
		
		public void UpdateAvailableSources()
		{

			sourceSelector.Clear();
			CellRendererText cell = new CellRendererText();
			sourceSelector.PackStart(cell, false);
			sourceSelector.AddAttribute(cell, "text", 0);
			ListStore store = new ListStore(typeof (string));
			sourceSelector.Model = store;
			
			store.AppendValues("Select source");
			foreach(Repository r in SplatterCore.Instance.Sources)
			{
				store.AppendValues ( r.Name );
			}
			
			sourceSelector.Active = 0;
		}
		protected virtual void FilterSelected (object o, Gtk.SelectCursorRowArgs args)
		{
		}
		
		protected virtual void FilterActivated (object o, Gtk.RowActivatedArgs args)
		{
			int index = args.Path.Indices[0];
			Console.WriteLine ("Activating filter number{0}", index);
			filterContainer.Show();
			filterContainer.Visible = true;
			foreach(Widget child in filterContainer.Children)
			{
				//child.HideAll();
				//child.Visible = false;
				//filterContainer.SetChildPacking(child, false, false, 0, PackType.Start);
				filterContainer.Remove(child);
			}
			
			Widget target = filterWidgets[index];
			target.Visible = true;
			target.ShowAll();
			// Expand only the widget corresponding to the active index
			//filterContainer.SetChildPacking(target, true, true, 0, PackType.Start);
			
			filterContainer.PackStart(target, true, true, 0);
		}
		
		
		protected virtual void ComboBoxChanged (object sender, System.EventArgs e)
		{
			if(sourceSelector.Active != 0)
			{
				testQueryButton.Sensitive = true;
				foreach(Widget filterWidget in filterWidgets)
				{
					IFilterWidget item = (IFilterWidget)filterWidget;
					item.SetNewSourceID(sourceSelector.Active - 1);
				}
			}
			else
			{
				testQueryButton.Sensitive = false;
			}
		}
		
		// The candidate query that will be added to the master list
		Query Candidate;
		
		protected virtual void TestQueryButtonClicked (object sender, System.EventArgs e)
		{
			// we're assuming that the source is a valid one.
			buttonOk.Sensitive = false;
			Candidate = null; // Erase any previous successful query candidate
			Query target = new Query();
			
			// Set the global source ID of the target's source
			target.SourceID = sourceSelector.Active - 1;
			
			// Start setting the parameters that will be set via the filters
			SearchParams queryParams = new SearchParams();
			
			// iterate over all the filters and get the filters
			foreach(Widget filterWidget in filterWidgets)
			{
				IFilterWidget filter = (IFilterWidget)filterWidget;
				if(filter != null)
				{
					// modify the query parameters
					filter.SetFilterParams(ref queryParams);
				}				
			}
			
			target.Generator.queryParameters = queryParams;
			target.Generator.Title = bugTitleEntry.Text;
			
			// now try to run the query
			testQueryButton.Sensitive = false;
			int output = target.Execute();
			if(output == -1)
			{
				// the connection failed or something
				testQueryOutputLabel.Text = "The request to the server failed. Please try later";
				testQueryButton.Sensitive = true;
			}
			else
			{
				testQueryOutputLabel.Text = String.Format("The query returned {0} results", output);
				
				// Allow user to press OK button
				buttonOk.Sensitive = true;
				
				// Set the new candidate
				Candidate = target;
				
				testQueryButton.Sensitive = true;
				
			}
		}
		
		public MainWindow parentWindow{get;set;}
		protected virtual void OKButtonClicked (object sender, System.EventArgs e)
		{
			if(Candidate!=null)
			{
				// add the candidate query to the list of queries
				SplatterCore.Instance.Queries.Add(Candidate);
				Candidate.Generator.Title = bugTitleEntry.Text;
				
				// Force a save to disk
				SplatterCore.Instance.SaveState();
				
				// Tell GUI To sync				
				parentWindow.SyncTreeviewWithBugs();
				
				this.Visible = false;
				this.Dispose();
			}
		}
		
		
	}
}

