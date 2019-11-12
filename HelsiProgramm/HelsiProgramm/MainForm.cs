﻿using HelsiProgramm.UseControl;
using ServiceDLL.Concrete;
using ServiceDLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HelsiProgramm
{
    public partial class MainForm : Form
    {
        string EmailSearch;
        ClientApiService clientApi = new ClientApiService();
        public MainForm(string email)
        {
            EmailSearch = email;
            InitializeComponent();
            SidePanel.Height = btnClinic.Height;
            SidePanel.Top = btnClinic.Top;

            //================================
            var listcl = clientApi.GetClients();
            foreach (var p in listcl)
            {
                object[] row = { p.Id, p.Name, p.Surname, p.DateBirthday.ToShortDateString() };
                dgwClients.Rows.Add(row);
            }

            //================================
            //ClinicApiService clinicApi = new ClinicApiService();
            //var listcl = clinicApi.GetClinics();
            //foreach (var p in listcl)
            //{
            //    object[] row = { p.Id, p.Name, p.Street };
            //    dvgClinics.Rows.Add(row);
            //}
            //========================================
            //DoctorApiService doctorApi = new DoctorApiService();
            //var listdc = doctorApi.GetDoctor();
            //foreach (var p in listdc)
            //{
            //    object[] row = { p.Id, p.Name, p.Surname, p.DateBirthday.ToShortDateString() };
            //    dvgDoctor.Rows.Add(row);
            //}
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClinic_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnClinic.Height;
            SidePanel.Top = btnClinic.Top;
            dvgClinics.BringToFront();
        }

        private void btnReform_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnReform.Height;
            SidePanel.Top = btnReform.Top;
        }

        private void BtnDoctor_Click(object sender, EventArgs e)
        {
            SidePanel.Height = BtnDoctor.Height;
            SidePanel.Top = BtnDoctor.Top;
            dvgDoctor.BringToFront();


        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            var listcl = clientApi.GetClients();
            foreach (var p in listcl)
            {
                if (p.Email == EmailSearch)
                {

                    ContactProfil contactProfil = new ContactProfil(p.Name, p.Surname, p.DateBirthday.ToShortDateString());
                    this.Controls.Add(contactProfil);
                    contactProfil.Location = new Point(237, 61);
                    contactProfil.BringToFront();
                    break;
                }
            }
            SidePanel.Height = btnContact.Height;
            SidePanel.Top = btnContact.Top;
            //contactProfil.BringToFront();
           
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnAbout.Height;
            SidePanel.Top = btnAbout.Top;
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnSchedule.Height;
            SidePanel.Top = btnSchedule.Top;
            dvgDoctor.BringToFront();
        }


    }
}
