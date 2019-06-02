﻿using Jacksonsoft;
using OSIRT.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSIRT.UI.AuditLog
{
    public class OsirtGridView : DataGridView
    {

        public event EventHandler RowEntered;
        public string table;

        public OsirtGridView()
        {
            InitialiseDataGridView();
        }

        public OsirtGridView(string table)
        {
            this.table = table;
            InitialiseDataGridView();
        }


        private void InitialiseDataGridView()
        {
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            Dock = DockStyle.Fill;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            ColumnAdded += AuditGridView_ColumnAdded;
            RowEnter += AuditGridView_RowEnter;
            CellClick += OsirtGridView_CellClick;

            DataBindingComplete += OsirtGridView_DataBindingComplete;
            ColumnHeaderMouseClick += OsirtGridView_ColumnHeaderMouseClick;
        }



        private void OsirtGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = Columns[e.ColumnIndex];

            if (newColumn.HeaderText != "print") return;

            WaitWindow.Show(UncheckAll, "Working... Please Wait");
        }

        private void UncheckAll(object sender, WaitWindowEventArgs e)
        {
            foreach (DataGridViewRow row in Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell is DataGridViewCheckBoxCell)
                    {

                        var checkbox = cell as DataGridViewCheckBoxCell;
                        string id = row.Cells["id"].Value.ToString();
                        bool isChecked = checkbox.Value != null && (bool)checkbox.Value;
                        row.Cells["print"].Value = !isChecked;
                        string tableName = ((System.Data.DataTable)DataSource).TableName;
                        //the value being returned is True or False (note uppercase) but we're getting it from the db using "where = 'true'"
                        //note lower case. hence toLower.
                        string query = $"UPDATE {tableName} SET print = '{(!isChecked)}' WHERE id='{id}'".ToLower();

                        DatabaseHandler db = new DatabaseHandler();
                        db.ExecuteNonQuery(query); //TODO: Place an Update method in the db handler  
                    }
                }
            }
       
        }

        private void OsirtGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }

        protected virtual void OsirtGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Don't want this to execute when the column header/row is clicked (OOB)
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
            {
                DataGridViewCheckBoxCell column = (DataGridViewCheckBoxCell)Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (column.Value != null)
                {
                    bool isChecked = (bool)column.Value;
                    string id = Rows[e.RowIndex].Cells["id"].Value.ToString();
                    string query = $"UPDATE {table} SET print = '{(!isChecked)}' WHERE id='{id}'";
                    DatabaseHandler db = new DatabaseHandler();
                    db.ExecuteNonQuery(query); //TODO: Place an Update method in the db handler   
                }
            }
        }

        private void AuditGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            Dictionary<string, string> rowCells = new Dictionary<string, string>();
            int cellCount = Rows[e.RowIndex].Cells.Count;
            for (int i = 0; i < cellCount; i++)
            {
                rowCells.Add(Columns[i].Name, Rows[e.RowIndex].Cells[i].Value.ToString());
            }

            ExtendedRowEnterEventArgs eventArgs = new ExtendedRowEnterEventArgs(e, rowCells);
            RowEntered?.Invoke(this, eventArgs);
        }


        //Makes the print column selectable, and keeps other columns readonly
        private void AuditGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            DataGridViewColumn column = e.Column;
            column.ReadOnly = true;
            if (column.Name == "print")
            {
                column.ReadOnly = false;
            }

        }



    }




}
