// 
// StringArrayWidget.cs
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
using System.Collections.Generic;
namespace BugzillaInterface
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class StringArrayWidget : Gtk.Bin
	{
		TreeViewColumn optionColumn;
		
		public StringArrayWidget ()
		{
			this.Build ();
			optionColumn = new TreeViewColumn();
			CellRendererText optionRender = new CellRendererText();
			optionColumn.PackStart(optionRender, true);
			optionColumn.AddAttribute(optionRender, "text", 0);
			optionTreeView.AppendColumn(optionColumn);
			
			model = new ListStore(typeof(string));
			optionTreeView.Model = model;
			
			optionTreeView.Selection.Mode = SelectionMode.Multiple;
			
		}
		
		public ListStore model;
		
		public string[] options;
		
		public string[] Options{
			get{
				return options;
			}set
			{
				options = value;
				model.Clear();
				foreach(string s in options)
				{
					// Add the model values
					model.AppendValues(s);
				}				
			}
		}
		
		public string[] GetSelected()
		{
			
			TreePath[] selection = optionTreeView.Selection.GetSelectedRows();
			List<string> selected = new List<string>();
			foreach(TreePath path in selection)				
			{
				
				// Find the index of the path and add the string corresponding to that index
				selected.Add(options[path.Indices[0]]);
			}
			
			return selected.ToArray();
		}
		
		protected virtual void CheckboxToggled (object sender, System.EventArgs e)
		{
			optionTreeView.Sensitive = enabledCheckbox.Active;
		}
		
		
		public string ColumnTitle
		{
			get
			{
				return optionColumn.Title;
			}
			set
			{
				optionColumn.Title = value;				
			}
		}
		
		public bool Enabled
		{
			get{
				return enabledCheckbox.Active && GetSelected().Length > 0;
			}
			private set{
				
			}
		}

	}
}

