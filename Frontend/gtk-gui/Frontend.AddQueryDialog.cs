
// This file has been generated by the GUI designer. Do not modify.
namespace Frontend
{
	public partial class AddQueryDialog
	{
		private global::Gtk.VBox vbox3;

		private global::Gtk.Expander expander1;

		private global::Gtk.Label label4;

		private global::Gtk.Label GtkLabel6;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Label label3;

		private global::Gtk.ComboBox sourceSelector;

		private global::Gtk.Button addSourceButton;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Frame frame1;

		private global::Gtk.Alignment GtkAlignment3;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TreeView queryTreeView;

		private global::Gtk.Label GtkLabel3;

		private global::Gtk.Frame frame2;

		private global::Gtk.Alignment GtkAlignment4;

		private global::Gtk.HBox filterContainer;

		private global::Gtk.Label GtkLabel4;

		private global::Gtk.HBox hbox3;

		private global::Gtk.Label label2;

		private global::Gtk.Button testQueryButton;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Frontend.AddQueryDialog
			this.Name = "Frontend.AddQueryDialog";
			this.Title = global::Mono.Unix.Catalog.GetString ("dialog1");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child Frontend.AddQueryDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.expander1 = new global::Gtk.Expander (null);
			this.expander1.CanFocus = true;
			this.expander1.Name = "expander1";
			// Container child expander1.Gtk.Container+ContainerChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.Xpad = 13;
			this.label4.Xalign = 0f;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("1. Select a source or add a new one.\n2. Set the required parameter for each filter of the query\n3. Test the query to see number of results.\n4. Click \"OK\" to add the Query.\n\nYou'll need to test the query again if you make any changes to the filters.");
			this.expander1.Add (this.label4);
			this.GtkLabel6 = new global::Gtk.Label ();
			this.GtkLabel6.Name = "GtkLabel6";
			this.GtkLabel6.LabelProp = global::Mono.Unix.Catalog.GetString ("Instructions");
			this.GtkLabel6.UseUnderline = true;
			this.expander1.LabelWidget = this.GtkLabel6;
			this.vbox3.Add (this.expander1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.expander1]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Source: ");
			this.hbox1.Add (this.label3);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.label3]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.sourceSelector = global::Gtk.ComboBox.NewText ();
			this.sourceSelector.Name = "sourceSelector";
			this.hbox1.Add (this.sourceSelector);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.sourceSelector]));
			w5.Position = 1;
			// Container child hbox1.Gtk.Box+BoxChild
			this.addSourceButton = new global::Gtk.Button ();
			this.addSourceButton.CanFocus = true;
			this.addSourceButton.Name = "addSourceButton";
			this.addSourceButton.UseUnderline = true;
			// Container child addSourceButton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w6 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w7 = new global::Gtk.HBox ();
			w7.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w8 = new global::Gtk.Image ();
			w8.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-add", global::Gtk.IconSize.Menu);
			w7.Add (w8);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w10 = new global::Gtk.Label ();
			w10.LabelProp = global::Mono.Unix.Catalog.GetString ("Add New Source");
			w10.UseUnderline = true;
			w7.Add (w10);
			w6.Add (w7);
			this.addSourceButton.Add (w6);
			this.hbox1.Add (this.addSourceButton);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.addSourceButton]));
			w14.Position = 2;
			w14.Expand = false;
			w14.Fill = false;
			this.vbox3.Add (this.hbox1);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox1]));
			w15.Position = 1;
			w15.Expand = false;
			w15.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.frame1 = new global::Gtk.Frame ();
			this.frame1.Name = "frame1";
			this.frame1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame1.Gtk.Container+ContainerChild
			this.GtkAlignment3 = new global::Gtk.Alignment (0f, 0f, 1f, 1f);
			this.GtkAlignment3.Name = "GtkAlignment3";
			this.GtkAlignment3.LeftPadding = ((uint)(12));
			// Container child GtkAlignment3.Gtk.Container+ContainerChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.queryTreeView = new global::Gtk.TreeView ();
			this.queryTreeView.WidthRequest = 130;
			this.queryTreeView.CanFocus = true;
			this.queryTreeView.Name = "queryTreeView";
			this.GtkScrolledWindow.Add (this.queryTreeView);
			this.GtkAlignment3.Add (this.GtkScrolledWindow);
			this.frame1.Add (this.GtkAlignment3);
			this.GtkLabel3 = new global::Gtk.Label ();
			this.GtkLabel3.Name = "GtkLabel3";
			this.GtkLabel3.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Query Type</b>");
			this.GtkLabel3.UseMarkup = true;
			this.frame1.LabelWidget = this.GtkLabel3;
			this.hbox2.Add (this.frame1);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.frame1]));
			w19.Position = 0;
			w19.Expand = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.frame2 = new global::Gtk.Frame ();
			this.frame2.Name = "frame2";
			this.frame2.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame2.Gtk.Container+ContainerChild
			this.GtkAlignment4 = new global::Gtk.Alignment (0f, 0f, 1f, 1f);
			this.GtkAlignment4.Name = "GtkAlignment4";
			this.GtkAlignment4.LeftPadding = ((uint)(12));
			// Container child GtkAlignment4.Gtk.Container+ContainerChild
			this.filterContainer = new global::Gtk.HBox ();
			this.filterContainer.Name = "filterContainer";
			this.filterContainer.Spacing = 6;
			this.GtkAlignment4.Add (this.filterContainer);
			this.frame2.Add (this.GtkAlignment4);
			this.GtkLabel4 = new global::Gtk.Label ();
			this.GtkLabel4.Name = "GtkLabel4";
			this.GtkLabel4.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Query Configuration</b>");
			this.GtkLabel4.UseMarkup = true;
			this.frame2.LabelWidget = this.GtkLabel4;
			this.hbox2.Add (this.frame2);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.frame2]));
			w22.Position = 1;
			this.vbox3.Add (this.hbox2);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox2]));
			w23.Position = 2;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Please test the query before adding.");
			this.hbox3.Add (this.label2);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.label2]));
			w24.Position = 0;
			// Container child hbox3.Gtk.Box+BoxChild
			this.testQueryButton = new global::Gtk.Button ();
			this.testQueryButton.WidthRequest = 150;
			this.testQueryButton.HeightRequest = 0;
			this.testQueryButton.Sensitive = false;
			this.testQueryButton.CanFocus = true;
			this.testQueryButton.Name = "testQueryButton";
			this.testQueryButton.UseUnderline = true;
			// Container child testQueryButton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w25 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w26 = new global::Gtk.HBox ();
			w26.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w27 = new global::Gtk.Image ();
			w27.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-execute", global::Gtk.IconSize.Dnd);
			w26.Add (w27);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w29 = new global::Gtk.Label ();
			w29.LabelProp = global::Mono.Unix.Catalog.GetString ("Test Query");
			w29.UseUnderline = true;
			w26.Add (w29);
			w25.Add (w26);
			this.testQueryButton.Add (w25);
			this.hbox3.Add (this.testQueryButton);
			global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.testQueryButton]));
			w33.Position = 1;
			w33.Expand = false;
			w33.Fill = false;
			this.vbox3.Add (this.hbox3);
			global::Gtk.Box.BoxChild w34 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox3]));
			w34.Position = 3;
			w34.Expand = false;
			w34.Fill = false;
			w1.Add (this.vbox3);
			global::Gtk.Box.BoxChild w35 = ((global::Gtk.Box.BoxChild)(w1[this.vbox3]));
			w35.Position = 0;
			// Internal child Frontend.AddQueryDialog.ActionArea
			global::Gtk.HButtonBox w36 = this.ActionArea;
			w36.Name = "dialog1_ActionArea";
			w36.Spacing = 10;
			w36.BorderWidth = ((uint)(5));
			w36.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w37 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w36[this.buttonCancel]));
			w37.Expand = false;
			w37.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w38 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w36[this.buttonOk]));
			w38.Position = 1;
			w38.Expand = false;
			w38.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 631;
			this.DefaultHeight = 467;
			this.Show ();
			this.sourceSelector.Changed += new global::System.EventHandler (this.ComboBoxChanged);
			this.addSourceButton.Clicked += new global::System.EventHandler (this.AddSourceButtonClicked);
			this.queryTreeView.SelectCursorRow += new global::Gtk.SelectCursorRowHandler (this.FilterSelected);
			this.queryTreeView.RowActivated += new global::Gtk.RowActivatedHandler (this.FilterActivated);
			this.buttonCancel.Clicked += new global::System.EventHandler (this.CancelButtonClicked);
		}
	}
}
