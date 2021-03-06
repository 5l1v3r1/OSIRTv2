﻿using OSIRT.Enums;
using OSIRT.Helpers;
using OSIRT.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIRT.UI.AuditLog
{
    class DatatableToHtml
    {

        //http://stackoverflow.com/questions/19682996/datatable-to-html-table
        public static string ConvertToHtml(DataTable table, string exportPath, string reportContainerName)
        {

            //TODO: Have this similar logic when we create the case. DRY.
            //also fire every iteration, obviously don't want that.
            List<string> directories = Constants.Directories.GetCaseDirectories();
            foreach (string directory in directories)
            {
                Directory.CreateDirectory(Path.Combine($"{exportPath}", reportContainerName, Constants.Artefacts, directory));
            }

            string columnName;
            string html = "<table>";
            html += "<tr>";
            for (int i = 0; i < table.Columns.Count; i++)
            {
                columnName = table.Columns[i].ColumnName;
                if (columnName == "note" && !UserSettings.Load().PrintAuditNotes)
                    continue;

                html += "<th>" + columnName.ToTitleCase() + "</th>";
            }
            html += "</tr>";


            int count = 0;
            foreach (DataRow row in table.Rows)
            {
                html += "</tr>";
                foreach (DataColumn column in table.Columns)
                {
                    string cellValue = row[column] != null ? row[column].ToString() : "";
                    columnName = column.ColumnName;

                    if (columnName == "note" && !UserSettings.Load().PrintAuditNotes) continue;

                    if(columnName == "file" && table.TableName != "osirt_actions" && cellValue != "")
                    {
                        count++;
                        string rowAction = row["action"].ToString();
                        if (Path.HasExtension(cellValue) && rowAction != "Case Notes")
                        {
                            Actions action = (Actions)Enum.Parse(typeof(Actions), rowAction);
                            string location = Constants.Directories.GetSpecifiedCaseDirectory(action);
                            string sourceFile = Path.Combine(Constants.ContainerLocation, location, cellValue);
                            string relativePath = Path.Combine(Constants.Artefacts, location, cellValue);
                            string destination = Path.Combine($"{exportPath}", reportContainerName, relativePath);
                            try
                            {
                                File.Copy(sourceFile, destination, true);
                            }
                            finally
                            {
                                if (cellValue.HasImageExtension() && UserSettings.Load().PrintImagesInReport)
                                {
                                    html += $@"<td><a href='{relativePath}' onmouseover=showImageOnMouseOver('{relativePath.Replace(@"\", @"\\")}','{count}'); onmouseout=resetImage('{count}');> <img src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAABGdBTUEAALGPC/xhBQAAAA1JREFUGFdj+O/LwAAABOgBTRkGJGkAAAAASUVORK5CYII=' id='place-holder-{count}' style='zindex: 100; position: absolute;'/>{cellValue}</a></td>";
                                }
                                else if (cellValue.HasVideoExtension() && UserSettings.Load().ShowVideosInReport)
                                {
                                    html += $@"<td><video width='300' height='200' controls> <source src='{relativePath}' type='video/mp4'><a href='{relativePath}'>{cellValue}</a></video>";
                                }
                                else
                                {
                                    html += $@"<td><a href='{relativePath}'>{cellValue}</a></td>";
                                }
                           }
                        }
                        else
                        {
                            html += "<td>" + cellValue + "</td>";
                        }
                    }
                    else
                    {
                        html += "<td>" + cellValue + "</td>";
                    }
                }
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }


    }
}
