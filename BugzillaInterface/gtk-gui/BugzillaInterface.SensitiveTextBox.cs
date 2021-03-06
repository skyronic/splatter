
// This file has been generated by the GUI designer. Do not modify.
namespace BugzillaInterface
{
	public partial class SensitiveTextBox
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.CheckButton enabledButton;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Label textLabel;

		private global::Gtk.Entry textEntry;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget BugzillaInterface.SensitiveTextBox
			global::Stetic.BinContainer.Attach (this);
			this.Name = "BugzillaInterface.SensitiveTextBox";
			// Container child BugzillaInterface.SensitiveTextBox.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.enabledButton = new global::Gtk.CheckButton ();
			this.enabledButton.CanFocus = true;
			this.enabledButton.Name = "enabledButton";
			this.enabledButton.Label = global::Mono.Unix.Catalog.GetString ("Enabled");
			this.enabledButton.DrawIndicator = true;
			this.enabledButton.UseUnderline = true;
			this.vbox2.Add (this.enabledButton);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.enabledButton]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.textLabel = new global::Gtk.Label ();
			this.textLabel.WidthRequest = 100;
			this.textLabel.Name = "textLabel";
			this.textLabel.Xpad = 13;
			this.textLabel.Xalign = 0f;
			this.textLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("label1");
			this.hbox1.Add (this.textLabel);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.textLabel]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.textEntry = new global::Gtk.Entry ();
			this.textEntry.Sensitive = false;
			this.textEntry.CanFocus = true;
			this.textEntry.Name = "textEntry";
			this.textEntry.IsEditable = true;
			this.textEntry.InvisibleChar = '●';
			this.hbox1.Add (this.textEntry);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.textEntry]));
			w3.Position = 1;
			this.vbox2.Add (this.hbox1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.Add (this.vbox2);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.enabledButton.Toggled += new global::System.EventHandler (this.ToggleSensitivity);
		}
	}
}
