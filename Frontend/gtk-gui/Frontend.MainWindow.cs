
// This file has been generated by the GUI designer. Do not modify.
namespace Frontend
{
	public partial class MainWindow
	{
		private global::Gtk.UIManager UIManager;

		private global::Gtk.Action addAction;

		private global::Gtk.Action refreshAction;

		private global::Gtk.Action deleteAction;

		private global::Gtk.VBox vbox1;

		private global::Gtk.Toolbar toolbar3;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TreeView treeview1;

		private global::Gtk.Statusbar statusbar1;

		private global::Gtk.ProgressBar progressbar1;

		private global::Gtk.Label statusLabel;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Frontend.MainWindow
			this.UIManager = new global::Gtk.UIManager ();
			global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
			this.addAction = new global::Gtk.Action ("addAction", global::Mono.Unix.Catalog.GetString ("Add Query"), global::Mono.Unix.Catalog.GetString ("Add Queries"), "gtk-add");
			this.addAction.HideIfEmpty = false;
			this.addAction.IsImportant = true;
			this.addAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Add Query");
			w1.Add (this.addAction, "<Control><Alt><Meta>n");
			this.refreshAction = new global::Gtk.Action ("refreshAction", global::Mono.Unix.Catalog.GetString ("Refresh Queries"), global::Mono.Unix.Catalog.GetString ("Refresh Queries"), "gtk-refresh");
			this.refreshAction.HideIfEmpty = false;
			this.refreshAction.IsImportant = true;
			this.refreshAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Refresh Queries");
			w1.Add (this.refreshAction, null);
			this.deleteAction = new global::Gtk.Action ("deleteAction", global::Mono.Unix.Catalog.GetString ("Delete Query"), global::Mono.Unix.Catalog.GetString ("Delete Query"), "gtk-delete");
			this.deleteAction.HideIfEmpty = false;
			this.deleteAction.IsImportant = true;
			this.deleteAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Delete Query");
			w1.Add (this.deleteAction, null);
			this.UIManager.InsertActionGroup (w1, 0);
			this.AddAccelGroup (this.UIManager.AccelGroup);
			this.Name = "Frontend.MainWindow";
			this.Title = global::Mono.Unix.Catalog.GetString ("Splatter - Query View");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child Frontend.MainWindow.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.UIManager.AddUiFromString ("<ui><toolbar name='toolbar3'><toolitem name='addAction' action='addAction'/><toolitem name='refreshAction' action='refreshAction'/><toolitem name='deleteAction' action='deleteAction'/></toolbar></ui>");
			this.toolbar3 = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/toolbar3")));
			this.toolbar3.Name = "toolbar3";
			this.toolbar3.ShowArrow = false;
			this.toolbar3.IconSize = ((global::Gtk.IconSize)(3));
			this.vbox1.Add (this.toolbar3);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.toolbar3]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeview1 = new global::Gtk.TreeView ();
			this.treeview1.CanFocus = true;
			this.treeview1.Name = "treeview1";
			this.GtkScrolledWindow.Add (this.treeview1);
			this.vbox1.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
			w4.Position = 1;
			// Container child vbox1.Gtk.Box+BoxChild
			this.statusbar1 = new global::Gtk.Statusbar ();
			this.statusbar1.Name = "statusbar1";
			this.statusbar1.Spacing = 6;
			// Container child statusbar1.Gtk.Box+BoxChild
			this.progressbar1 = new global::Gtk.ProgressBar ();
			this.progressbar1.Name = "progressbar1";
			this.statusbar1.Add (this.progressbar1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.statusbar1[this.progressbar1]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			// Container child statusbar1.Gtk.Box+BoxChild
			this.statusLabel = new global::Gtk.Label ();
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Xpad = 13;
			this.statusLabel.Xalign = 0f;
			this.statusLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("label5");
			this.statusbar1.Add (this.statusLabel);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.statusbar1[this.statusLabel]));
			w6.Position = 2;
			this.vbox1.Add (this.statusbar1);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.statusbar1]));
			w7.Position = 2;
			w7.Expand = false;
			w7.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 751;
			this.DefaultHeight = 428;
			this.Show ();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
			this.addAction.Activated += new global::System.EventHandler (this.AddNewQueryClicked);
			this.refreshAction.Activated += new global::System.EventHandler (this.RefreshQueriesBuggonClicked);
			this.deleteAction.Activated += new global::System.EventHandler (this.DeleteQuery);
		}
	}
}
