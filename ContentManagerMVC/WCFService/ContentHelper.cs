using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using ContentManagerMVC.Models;

namespace ContentManagerMVC.WCFService
{
    public class ContentHelper
    {
        static public string ScheduleToXMLString(Schedule schedule)
        {
            try
            {
                // Write To XML
                // Delete XML file if already existed.
                XmlDocument xmlDoc = new XmlDocument();        // Write down the XML declaration
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);

                // Create the root element
                XmlElement rootNode = xmlDoc.CreateElement("ScheduleList");
                // 
                xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
                XmlNode nodes = xmlDoc.AppendChild(rootNode);
                // add some child nodes

                XmlElement xmlschedule = xmlDoc.CreateElement("Schedule");
                xmlschedule.SetAttribute("Name", schedule.Title);
                xmlschedule.SetAttribute("StartTime", schedule.StartTime.ToString());
                xmlschedule.SetAttribute("EndTime", schedule.StartTime.ToString());
                xmlschedule.SetAttribute("Continuous", schedule.Continuous.ToString());
                xmlschedule.SetAttribute("PlayOnePerTime", schedule.PlayOnePerRound.ToString());
                xmlschedule.SetAttribute("Mon", schedule.Mon.ToString());
                xmlschedule.SetAttribute("Tue", schedule.Tue.ToString());
                xmlschedule.SetAttribute("Wed", schedule.Wed.ToString());
                xmlschedule.SetAttribute("Thr", schedule.Thr.ToString());
                xmlschedule.SetAttribute("Fri", schedule.Fri.ToString());
                xmlschedule.SetAttribute("Sat", schedule.Sat.ToString());
                xmlschedule.SetAttribute("Sun", schedule.Sun.ToString());
                foreach (ScheduledItem item in schedule.Contents)
                {
                    XmlElement xmlitem = xmlDoc.CreateElement("Item");
                    xmlitem.SetAttribute("Title", item.Content.Title);
                    xmlitem.SetAttribute("Path", item.Content.Path);
                    xmlitem.SetAttribute("Duration", item.Content.Duration.ToString());
                    xmlschedule.AppendChild(xmlitem);
                }
                rootNode.AppendChild(xmlschedule);
                using (var stringWriter = new StringWriter())
                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    xmlDoc.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    return stringWriter.GetStringBuilder().ToString();
                }
            }
            catch (Exception exp)
            {
                return string.Empty;
            }
             
        }
    }
}