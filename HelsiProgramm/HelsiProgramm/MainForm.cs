﻿using HelsiProgramm.UseControl;
using ServiceDLL.Concrete;
using ServiceDLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HelsiProgramm
{
    public partial class MainForm : Form
    {
        ClientApiService clientApi = new ClientApiService();
        DoctorApiService doctorApi = new DoctorApiService();
        CityApiService cityApi = new CityApiService();
        ScheduleApiService scheduleApi = new ScheduleApiService();
        ContactProfil contactProfil;
        public int IdDoctors;
        string EmailSearch;
        public MainForm(string email)
        {
            EmailSearch = email;
            InitializeComponent();
            SidePanel.Height = btnClinic.Height;
            SidePanel.Top = btnClinic.Top;

            var listclients = clientApi.GetClients();
            foreach (var p in listclients)
            {
                object[] row = { p.Id, p.Name, p.Surname, p.DateBirthday };
                dgwClients.Rows.Add(row);
            }

            var listcity = cityApi.GetCities();
            foreach (var p in listcity)
            {
                object[] row = { p.Id, p.Name };
                dvgCity.Rows.Add(row);
            }

            var listdoctor = doctorApi.GetDoctor();
            foreach (var p in listdoctor)
            {
                object[] row = { p.Id, p.Name, p.Surname, p.DateBirthday.ToShortDateString() };
                dvgDoctor.Rows.Add(row);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClinic_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnClinic.Height;
            SidePanel.Top = btnClinic.Top;
            dvgCity.BringToFront();
        }

        private void btnReform_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnReform.Height;
            SidePanel.Top = btnReform.Top;
            pAbout.BringToFront();
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
                    contactProfil = new ContactProfil(p.Name, p.Surname, p.DateBirthday.ToShortDateString());
                    this.Controls.Add(contactProfil);
                    contactProfil.Location = new Point(209, 28);
                    contactProfil.Size = new Size(926, 632);
                    contactProfil.BringToFront();
                    break;
                }
            }
            SidePanel.Height = btnContact.Height;
            SidePanel.Top = btnContact.Top;
            contactProfil.BringToFront();
        }


        private void btnSchedule_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnSchedule.Height;
            SidePanel.Top = btnSchedule.Top;
            dvgShedules.BringToFront();
        }
        private void dvgCity_SelectionChanged(object sender, EventArgs e)
        {
            dvgClinics.Rows.Clear();
            int id = Convert.ToInt32(dvgCity.Rows[dvgCity.CurrentRow.Index].Cells[0].Value);
            //=================================
            ClinicApiService clinicApi = new ClinicApiService();
            var listclinic = clinicApi.GetClinics();
            //==================================================
            CityApiService cityApi = new CityApiService();
            var listcity = cityApi.GetCities();

            foreach (var p in listclinic)
            {
                if (p.City == listcity[id - 1].Name)
                {
                    object[] row = { p.Id, p.City, p.Name, p.Street };
                    dvgClinics.Rows.Add(row);
                }
            }
            dvgClinics.BringToFront();
        }
        private void dvgDoctor_SelectionChanged(object sender, EventArgs e)
        {
            
        }
        private void btnConfirmShedule_Click(object sender, EventArgs e)
        {
            int id = dvgDoctor.CurrentRow.Index;
            var listclient = clientApi.GetClients();
            var listdoctor = doctorApi.GetDoctor();

            foreach (var p in listclient)
            {
                if (p.Email == EmailSearch)
                {
                    ScheduleAddModel scheduleAddModel = new ScheduleAddModel
                    {
                        ScheduleDateIn = dateShedulePicker.Value,
                        ClientId = p.Id,
                        DoctorId = IdDoctors
                    };
                    scheduleApi.CreateSchedule(scheduleAddModel);
                }
            }

            var listschedule = scheduleApi.GetSchedule();
            foreach (var p in listschedule)
            {
                foreach (var d in listdoctor)
                {
                    if (p.DoctorId == d.Id)
                    {
                        foreach (var l in listclient)
                        {
                            if (p.ClientId == l.Id && l.Email == EmailSearch)
                            {
                            object[] row = { p.Id, l.Name + " " + l.Surname, d.Name + " " + d.Surname, p.ScheduleDateIn.ToShortDateString() };
                            dvgShedules.Rows.Add(row);
                            }
                        }
                    }                    
                }
            }
        SidePanel.Height = btnSchedule.Height;
            SidePanel.Top = btnSchedule.Top;
            dvgShedules.BringToFront();
        }
    private void dateShedulePicker_onValueChanged(object sender, EventArgs e)
    {
        if (dateShedulePicker.Value < DateTime.Today)
        {
            dateShedulePicker.BackColor = Color.IndianRed;
        }
        else
        {
            dateShedulePicker.BackColor = Color.SeaGreen;
        }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        dvgDoctor.FirstDisplayedCell = null;
        dvgDoctor.ClearSelection();
    }
        private void dvgDoctor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = dvgDoctor.CurrentRow.Index;
            var listdoctor = doctorApi.GetDoctor();
            IdDoctors = Convert.ToInt32(dvgDoctor.CurrentRow.Cells[0].Value);
            txtNameDoctor.Text = dvgDoctor.CurrentRow.Cells[1].Value.ToString();
            txtSurnameDoctor.Text = dvgDoctor.CurrentRow.Cells[2].Value.ToString();
            txtBirthDoctor.Text = dvgDoctor.CurrentRow.Cells[3].Value.ToString();
            var listcl = clientApi.GetClients();
            foreach (var p in listcl)
            {
                if (p.Email == EmailSearch)
                {
                    txtNameClient.Text = p.Name;
                    txtSurNameClient.Text = p.Surname;
                    txtBirthClient.Text = p.DateBirthday.ToShortDateString();
                    break;
                }
            }
            pShedule.BringToFront();
        }
    }
}
