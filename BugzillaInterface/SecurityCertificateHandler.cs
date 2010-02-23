// 
// SecurityCertificates.cs
//  
// Author:
//       Sanjoy Das <sanjoy@playingwithpointers.com>
// 
// Copyright (c) 2010 sanjoy
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
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace BugzillaInterface
{

	/// <summary>
	/// Handles the cases when the user's computer does not have the 
	/// root certificates for the https bugzilla server.
	/// </summary>
	public class SecurityCertificateHandler
	{
	
		/// <summary>
		/// Represents the three responses which may be returned by the 
		/// <see cref="PromptMethod"/> implementation.
		/// </summary>
		public enum CertificateAllowanceState {
			DontAllow,
			AllowThisTime,
			AlwaysAllow
		};
		
		public delegate SecurityCertificateHandler.CertificateAllowanceState PromptUser (string thumbPrint);
		
		/// <summary>
		/// A <see cref="SecurityCertificates.PromptUser"/>, possibly provided
		/// by the UI which should prompt the user about signing onto some 
		/// possibly mistrusted https:// link.
		/// </summary>
		public static SecurityCertificateHandler.PromptUser PromptMethod = null;
		
		// TODO Provide more error to the UI handler to be able to display better
		// messages about which certificate to trust.
		
		// TODO Allow the user to remove already trusted sources without messing with
		// the config.xml we create.
		
		public List <string> AllowedThumbPrints {get; set;}
		
		public static SecurityCertificateHandler Instance {get; protected set;}
		
		protected bool RemoteCertificateValidationCallback(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
		
			if (AllowedThumbPrints == null)
				AllowedThumbPrints = new List<string> ();
		
			if (sslPolicyErrors == SslPolicyErrors.None) 
				return true;
				
			string thumbprint = certificate.GetCertHashString ();
			
			if (AllowedThumbPrints.Contains (thumbprint))
				return true;
			
			if (PromptMethod == null) {
				return false;
			}
			
			SecurityCertificateHandler.CertificateAllowanceState answer = PromptMethod (thumbprint);
			if (answer == SecurityCertificateHandler.CertificateAllowanceState.DontAllow) {
				return false;
			} else if (answer == SecurityCertificateHandler.CertificateAllowanceState.AllowThisTime) {
				return true;
			} else if (answer == SecurityCertificateHandler.CertificateAllowanceState.AlwaysAllow) {
				AllowedThumbPrints.Add(thumbprint);
				return true;
			} else {
				// Better to crash than connect to servers we don't trust.
				// Should not be executed if there is no disparity in the 
				// code.
				throw new System.NotImplementedException ();
			}
			
		}
		
		public static void Initialize () {
			Instance = new SecurityCertificateHandler ();
			ServicePointManager.ServerCertificateValidationCallback = Instance.RemoteCertificateValidationCallback;
		}

	}
}
