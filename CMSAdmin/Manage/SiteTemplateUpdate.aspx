﻿<%@ Page Title="Site-Wide Template Update" Language="C#" MasterPageFile="~/Manage/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="SiteTemplateUpdate.aspx.cs"
	Inherits="Carrotware.CMS.UI.Admin.Manage.SiteTemplateUpdate" %>

<%@ MasterType VirtualPath="MasterPages/Main.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="H1ContentPlaceHolder" runat="server">
	Site-Wide Template Update
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
	<fieldset style="width: 500px;">
		<legend>
			<label>
				Content
			</label>
		</legend>
		<p>
			<b>Home Page</b>&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Literal ID="litHomepage" runat="server" />
			<br />
			<asp:DropDownList DataTextField="Caption" DataValueField="TemplatePath" ID="ddlHome" runat="server">
			</asp:DropDownList>
			<br />
		</p>
		<p>
			<b>All Content Pages (only)</b>
			<br />
			<asp:DropDownList DataTextField="Caption" DataValueField="TemplatePath" ID="ddlPages" runat="server">
			</asp:DropDownList>
			<br />
		</p>
		<p>
			<b>All Top Level Pages (only)</b>
			<br />
			<asp:DropDownList DataTextField="Caption" DataValueField="TemplatePath" ID="ddlTop" runat="server">
			</asp:DropDownList>
			<br />
		</p>
		<p>
			<b>All Sub Level Pages (only)</b>
			<br />
			<asp:DropDownList DataTextField="Caption" DataValueField="TemplatePath" ID="ddlSub" runat="server">
			</asp:DropDownList>
			<br />
		</p>
	</fieldset>
	<fieldset style="width: 500px;">
		<legend>
			<label>
				Blog
			</label>
		</legend>
		<p>
			<b>Blog Index Page</b>&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Literal ID="litBlogIndex" runat="server" />
			<br />
			<asp:DropDownList DataTextField="Caption" DataValueField="TemplatePath" ID="ddlBlog" runat="server">
			</asp:DropDownList>
			<br />
		</p>
		<p>
			<b>All Blog Posts (only)</b>
			<br />
			<asp:DropDownList DataTextField="Caption" DataValueField="TemplatePath" ID="ddlPosts" runat="server">
			</asp:DropDownList>
			<br />
		</p>
	</fieldset>
	<fieldset style="width: 500px;">
		<legend>
			<label>
				Everything (including home and blog index)
			</label>
		</legend>
		<p>
			<b>All Content (Content Pages and Blog Posts)</b>
			<br />
			<asp:DropDownList DataTextField="Caption" DataValueField="TemplatePath" ID="ddlAll" runat="server">
			</asp:DropDownList>
			<br />
		</p>
	</fieldset>
	<p>
		<br />
	</p>
	<p>
		<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
	</p>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NoAjaxContentPlaceHolder" runat="server">
</asp:Content>
