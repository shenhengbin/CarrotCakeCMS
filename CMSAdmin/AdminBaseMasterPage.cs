﻿using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Carrotware.CMS.Core;
using Carrotware.CMS.UI.Base;

/*
* CarrotCake CMS
* http://www.carrotware.com/
*
* Copyright 2011, Samantha Copeland
* Dual licensed under the MIT or GPL Version 3 licenses.
*
* Date: October 2011
*/

namespace Carrotware.CMS.UI.Admin {

	public class AdminBaseMasterPage : BaseMasterPage {
		protected SiteData siteHelper = new SiteData();

		public enum SectionID {
			SiteDashboard,
			SiteInfo,
			SiteTemplate,
			ContentAdd,
			ContentSnippet,
			PageComment,
			StatusChange,
			ContentHistory,
			ContentTemplate,
			ContentSkinEdit,
			ContentSiteMap,
			SiteExport,
			DataImport,
			SiteImport,
			TextWidget,
			UserAdmin,
			GroupAdmin,
			SiteIndex,
			UserFn,
			Modules,
			BlogIndex,
			BlogContentAdd,
			BlogCategory,
			BlogTag,
			BlogTemplate,
			BlogComment
		}

		protected enum ControlLocation {
			PublicFooter,
			PopupFooter,
			MainFooter,
		}

		protected void LoadFooterCtrl(PlaceHolder plcHolder, ControlLocation CtrlKey) {
			string sControlPath = String.Empty;
			CarrotCakeConfig config = CarrotCakeConfig.GetConfig();

			switch (CtrlKey) {
				case ControlLocation.PublicFooter:
					sControlPath = config.AdminFooterControls.ControlPathPublic;
					break;

				case ControlLocation.PopupFooter:
					sControlPath = config.AdminFooterControls.ControlPathPopup;
					break;

				case ControlLocation.MainFooter:
					sControlPath = config.AdminFooterControls.ControlPathMain;
					break;
			}

			if (!String.IsNullOrEmpty(sControlPath)) {
				if (File.Exists(Server.MapPath(sControlPath))) {
					Control ctrl = new Control();
					ctrl = Page.LoadControl(sControlPath);
					plcHolder.Controls.Add(ctrl);
				}
			}
		}
	}
}