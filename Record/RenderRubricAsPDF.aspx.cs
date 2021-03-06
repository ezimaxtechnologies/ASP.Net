﻿using System;
using Standpoint.Core.Classes;
using System.Reflection;
using Thinkgate.Base.Classes;
using Thinkgate.Classes;
using WebSupergoo.ABCpdf9;
using Thinkgate.ExceptionHandling;

namespace Thinkgate.Record
{
    public partial class RenderRubricAsPDF : BasePage
    {
        private int _rubricID;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["xID"] == null)
            {
                SessionObject.RedirectMessage = "No entity ID provided in URL.";
                Response.Redirect("~/PortalSelection.aspx", true);
            }
            else
            {
                _rubricID = GetDecryptedEntityId(X_ID);
            }

            Doc doc = new Doc();

            doc = Assessment.RenderAssessmentRubricToPdf(_rubricID);

            byte[] theData = doc.GetData();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline; filename=MyPDF.PDF");
            Response.AddHeader("content-length", theData.Length.ToString());
            Response.BinaryWrite(theData);

            ThinkgateEventSource.Log.ApplicationEvent(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "->" + MethodBase.GetCurrentMethod().Name, "Finished rendering PDF for rubric " + _rubricID, string.Empty);
        }
    }
}