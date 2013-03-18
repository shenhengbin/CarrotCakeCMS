﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using Carrotware.CMS.Core;
using Carrotware.CMS.UI.Base;
using Carrotware.CMS.UI.Controls;
/*
* CarrotCake CMS
* http://www.carrotware.com/
*
* Copyright 2011, Samantha Copeland
* Dual licensed under the MIT or GPL Version 2 licenses.
*
* Date: October 2011
*/

namespace Carrotware.CMS.UI.Admin {
	public class AdminBasePage : BasePage {

		protected override void OnInit(EventArgs e) {
			if (Page.User.Identity.IsAuthenticated) {

				bool bHasAccess = siteHelper.VerifyUserHasSiteAccess(SiteData.CurrentSiteID, SecurityData.CurrentUserGuid);

				if (!bHasAccess) {
					FormsAuthentication.SignOut();
					Response.Redirect(SiteFilename.LogonURL);
				}
			}

			Response.Cache.SetCacheability(System.Web.HttpCacheability.Private);
			DateTime dtExpire = System.DateTime.Now.AddMinutes(-5);
			Response.Cache.SetExpires(dtExpire);

			base.OnInit(e);

		}

		protected Guid GetGuidPageIDFromQuery() {
			return GeneralUtilities.GetGuidPageIDFromQuery();
		}

		protected Guid GetGuidIDFromQuery() {
			return GeneralUtilities.GetGuidIDFromQuery();
		}

		protected Guid GetGuidParameterFromQuery(string ParmName) {
			return GeneralUtilities.GetGuidParameterFromQuery(ParmName);
		}

		protected void RedirectIfNoSite() {
			if (SiteData.CurrentSite == null) {
				Response.Redirect(SiteFilename.DashboardURL);
			}
		}

		public DateTime CalcNearestFiveMinTime(DateTime dateIn) {

			dateIn = dateIn.AddMinutes(-2);
			int iMin = 5 * (dateIn.Minute / 5);

			DateTime dateOut = dateIn.AddMinutes(0 - dateIn.Minute).AddMinutes(iMin);

			return dateOut;
		}

		public void PreselectCheckboxRepeater(Repeater repeater, List<IContentMetaInfo> lst) {
			foreach (RepeaterItem r in repeater.Items) {
				CheckBox chk = (CheckBox)r.FindControl("chk");
				Guid id = new Guid(chk.Attributes["value"].ToString());
				if (lst.Where(x => x.ContentMetaInfoID == id).Count() > 0) {
					chk.Checked = true;
				}
			}
		}

		public List<Guid> CollectCheckboxRepeater(Repeater repeater) {
			List<Guid> lst = new List<Guid>();
			foreach (RepeaterItem r in repeater.Items) {
				CheckBox chk = (CheckBox)r.FindControl("chk");
				Guid id = new Guid(chk.Attributes["value"].ToString());
				if (chk.Checked) {
					lst.Add(id);
				}
			}
			return lst;
		}

	}
}
