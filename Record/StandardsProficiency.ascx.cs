﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Thinkgate.Classes;
using Telerik.Web.UI;
using System.Linq;
using Microsoft.Reporting.WebForms;
using System.IO;
using Standpoint.Core.Classes;

namespace Thinkgate.Controls.Reports
{
    public partial class StandardsProficiency : TileControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindCurricula();
        }

        private void BindCurricula()
        {
            var studentinformation = (Thinkgate.Base.Classes.Student)Tile.TileParms.GetParm("student");
            SelectedStudentID.Value = Encryption.EncryptString(Convert.ToString(studentinformation.ID));

            DataTable CurriculaList = Thinkgate.Base.Classes.Reports.GetCurriculaByStudent(studentinformation.ID);

            CurriculaList.Columns.Add("EncryptedGrade", typeof(string));
            CurriculaList.Columns.Add("EncryptedSubject", typeof(string));
            CurriculaList.Columns.Add("EncryptedCourse", typeof(string));

            foreach (DataRow Curricula in CurriculaList.Rows)
            {
                Curricula["EncryptedGrade"] = Encryption.EncryptString(Convert.ToString(Curricula["Grade"]));
                Curricula["EncryptedSubject"] = Encryption.EncryptString(Convert.ToString(Curricula["Subject"]));
                Curricula["EncryptedCourse"] = Encryption.EncryptString(Convert.ToString(Curricula["Course"]));
            }
           
            if (CurriculaList.Rows.Count > 0)
            {
                rptCurricula.DataSource = CurriculaList;
                rptCurricula.DataBind();
                rptCurricula.Visible = true;
            }
            else
            {
                lblEmptyMessage.Text = "No Record Available";
                rptCurricula.Visible = false;
            }
        }
    }
}