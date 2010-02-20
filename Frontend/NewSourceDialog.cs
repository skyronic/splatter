// 
// NewSourceDialog.cs
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
using BugzillaInterface;
namespace Frontend
{
	public partial class NewSourceDialog : Gtk.Dialog
	{
		
		public Repository target;
		public AddQueryDialog parentDialog{get;set;}
		
		protected virtual void VerifyButtonClicked (object sender, System.EventArgs e)
		{
			target = new Repository();
			target.Name = nameEntry.Text;
			target.Url = urlEntry.Text;
			target.UserName = usernameEntry.Text;
			target.Password = passwordEntry.Text;
			target.Proxy = proxyEntry.Text;
			
			// disable the verify button
			verifyButton.Sensitive = false;
			statusLabel.Text = "Status: <b>Verifying</b>";
			
			if(target.LoginAndVerify())
			{
				// login success
				statusLabel.Text = "Status: <b>Verified!</b>";
				
				// enable the button
				buttonOk.Sensitive = true;			
			}
			else
			{
				statusLabel.Text = "Status: <b>Verification failed</b>";
				
				// give user chance to modify
				verifyButton.Sensitive = true;
			}
		}
		
		
		public NewSourceDialog ()
		{
			this.Build ();
		}
		
		protected virtual void CancelButtonClicked (object sender, System.EventArgs e)
		{
			this.HideAll();
			this.Dispose();
		}
		
		protected virtual void OKButtonClicked (object sender, System.EventArgs e)
		{
			// target should be valid if control reaches here			
			SplatterCore.Instance.Sources.Add(target);
			
			parentDialog.UpdateAvailableSources();
		}
		
		
		
	}
}

