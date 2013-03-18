﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;
using Carrotware.Web.UI.Controls;
/*
* CarrotCake CMS
* http://www.carrotware.com/
*
* Copyright 2011, Samantha Copeland
* Dual licensed under the MIT or GPL Version 2 licenses.
*
* Date: October 2011
*/

namespace Carrotware.CMS.UI.Controls {

	[Designer(typeof(CarrotGridPagedDesigner))]
	[ParseChildren(true, "TheGrid"), PersistChildren(true)]
	[ToolboxData("<{0}:CarrotGridPaged runat=server></{0}:CarrotGridPaged>")]

	public class CarrotGridPaged : DataBoundControl, INamingContainer {

		[Category("Appearance")]
		[DefaultValue(10)]
		public int PageSize {
			get {
				String s = (String)ViewState["PageSize"];
				return ((s == null) ? 10 : int.Parse(s));
			}
			set {
				ViewState["PageSize"] = value.ToString();
			}
		}

		[Category("Appearance")]
		[DefaultValue(1)]
		public int PageNumber {
			get {
				String s = (String)ViewState["PageNumber"];
				return ((s == null) ? 1 : int.Parse(s));
			}
			set {
				hdnPageNbr.Value = value.ToString();
				ViewState["PageNumber"] = value.ToString();
			}
		}

		[Category("Appearance")]
		[DefaultValue(1)]
		public int TotalRecords {
			get {
				String s = (String)ViewState["TotalRecords"];
				return ((s == null) ? 1 : int.Parse(s));
			}
			set {
				ViewState["TotalRecords"] = value.ToString();
			}
		}

		public bool IsPostBack {
			get {
				string sReq = "GET";
				try { sReq = HttpContext.Current.Request.ServerVariables["REQUEST_METHOD"].ToString().ToUpper(); } catch { }
				return sReq != "GET" ? true : false;
			}
		}

		private HiddenField hdnPageNbr = new HiddenField();
		private bool bHeadClicked = true;
		private string sBtnName = "lnkPagerBtn";

		public string SortingBy { get; set; }

		[
		Category("Behavior"),
		Description("The CarrotGridView "),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Browsable(true),
		TemplateContainer(typeof(CarrotGridView)),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public CarrotGridView TheGrid { get; set; }

		[
		Category("Behavior"),
		Description("The Repeater "),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Browsable(true),
		TemplateContainer(typeof(Repeater)),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public Repeater ThePager { get; set; }

		protected override void OnInit(EventArgs e) {
			if (this.DataSource == null) {
				this.DataSource = new List<object>();
			}

			hdnPageNbr.ID = "hdnPageNbr";
			this.Controls.Add(hdnPageNbr);

			this.TheGrid.ID = "gridData";
			this.Controls.Add(TheGrid);

			if (this.ThePager == null) {
				this.ThePager = new Repeater();
			}

			this.ThePager.ID = "repeaterPager";
			this.Controls.Add(ThePager);

			base.OnInit(e);

		}

		private Repeater GetCtrl() {
			Repeater r = new Repeater();

			try {
				ControlUtilities cu = new ControlUtilities(this);
				Control userControl = cu.CreateControlFromResource("Carrotware.CMS.UI.Controls.ucFancyPager.ascx");
				r = (Repeater)cu.FindControl("rpPager", userControl);
			} catch { }

			return r;
		}

		public void BuildSorting() {
			HttpContext context = HttpContext.Current;

			if (context != null) {
				HttpRequest request = context.Request;

				this.SortingBy = this.TheGrid.DefaultSort;

				if (!IsPostBack) {
					bHeadClicked = false;
					hdnPageNbr.Value = "1";
					SetSort();
				} else {
					if (request.Form["__EVENTARGUMENT"] != null) {
						string arg = request.Form["__EVENTARGUMENT"].ToString();
						string tgt = request.Form["__EVENTTARGET"].ToString();

						if (tgt.Contains("$lnkHead") && tgt.Contains("$" + this.TheGrid.ID + "$")) {
							bHeadClicked = true;
						}

						if (tgt.Contains("$" + sBtnName) && tgt.Contains("$" + this.ThePager.ID + "$")) {
							string[] parms = tgt.Split('$');
							int pg = int.Parse(parms[parms.Length - 1].Replace(sBtnName, ""));
							PageNumber = pg;
							hdnPageNbr.Value = PageNumber.ToString();
							bHeadClicked = false;
						}
					}
				}
			}

			if (PageNumber <= 1 && !string.IsNullOrEmpty(hdnPageNbr.Value)) {
				PageNumber = int.Parse(hdnPageNbr.Value);
			}

			if (IsPostBack) {
				SetSort();
			}
		}

		private void SetSort() {

			string sSort = this.TheGrid.CurrentSort;
			if (bHeadClicked) {
				sSort = this.TheGrid.PredictNewSort;
			}

			this.SortingBy = sSort;
		}



		public override object DataSource {
			get { return this.TheGrid.DataSource; }
			set { this.TheGrid.DataSource = value; }
		}

		public override void DataBind() {
			//base.DataBind();
			this.TheGrid.DataBind();

			int iTotalPages = 0;

			int iPageNbr = PageNumber - 1;

			iTotalPages = this.TotalRecords / this.PageSize;

			if ((this.TotalRecords % this.PageSize) > 0) {
				iTotalPages++;
			}

			this.ThePager.Visible = true;

			if (this.ThePager.ItemTemplate == null) {
				Repeater rp = GetCtrl();
				this.ThePager.HeaderTemplate = rp.HeaderTemplate;
				this.ThePager.ItemTemplate = rp.ItemTemplate;
				this.ThePager.FooterTemplate = rp.FooterTemplate;
			}

			if (iTotalPages > 1) {
				List<int> pagelist = new List<int>();
				pagelist = Enumerable.Range(1, iTotalPages).ToList();

				this.ThePager.DataSource = pagelist;
				this.ThePager.DataBind();
			}

			if (iTotalPages <= 1) {
				this.ThePager.Visible = false;
			}

			WalkCtrlsForAssignment(this.ThePager);

		}

		private void WalkCtrlsForAssignment(Control X) {
			foreach (Control c in X.Controls) {
				if (c is IActivatePageNavItem) {
					IActivatePageNavItem btn = (IActivatePageNavItem)c;
					if (btn.PageNumber == PageNumber) {
						btn.IsSelected = true;
					}
					WalkCtrlsForAssignment(c);
				} else {
					WalkCtrlsForAssignment(c);
				}
			}
		}

	}

	//======================================

	public class CarrotGridPagedDesigner : DataBoundControlDesigner {

		public override string GetDesignTimeHtml() {
			CarrotGridPaged myctrl = (CarrotGridPaged)base.ViewControl;
			string sType = myctrl.GetType().ToString().Replace(myctrl.GetType().Namespace + ".", "CMS, ");
			string sID = myctrl.ID;
			string sTextOut = "<span>[" + sType + " - " + sID + "]</span>\r\n";

			StringBuilder sb = new StringBuilder();
			sb.Append(sTextOut);

			try {
				IEnumerable designTimeDataSource = GetDesignTimeDataSource();

				myctrl.DataSource = designTimeDataSource;
				myctrl.DataBind();

				sb.Append(base.GetDesignTimeHtml());
			} finally {
				myctrl.DataSource = null;
			}

			sb.Append(RenderCtrl(myctrl));

			return sb.ToString();
		}

		private string RenderCtrl(Control ctrl) {
			StringWriter sw = new StringWriter();
			using (HtmlTextWriter tw = new HtmlTextWriter(sw)) {

				ctrl.RenderControl(tw);

				return sw.ToString();
			}
		}

	}

}
