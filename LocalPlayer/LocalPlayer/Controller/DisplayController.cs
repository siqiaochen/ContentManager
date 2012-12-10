using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LocalPlayer.PlayElements;
using System.Xml;
using System.IO;

namespace LocalPlayer.Controller
{
    class DisplayController
    {
        List<Schedule> schedules;
        int nextSchedule;
        public string CurrentPlayItem { get; set; }
        public DisplayController(List<Schedule> sches)
        {
            nextSchedule = 0;
            schedules = sches;
            CurrentPlayItem = "";
        }
        public DisplayController()
        {
            nextSchedule=0;
            schedules = new List<Schedule>();
            CurrentPlayItem = "";
        }
        public void ReceiveSchedule()
        { 
        
        }
        public void LoadSchedule()
        { 
            
        }
        public PlayItem GetNextFileFromSchedule()
        {
            if (schedules != null && schedules.Count>0)
            {
                if (nextSchedule >= schedules.Count)
                    nextSchedule = 0;
                Schedule sche = schedules[nextSchedule];
                PlayItem item = sche.CurrentItem;
                sche.CurrentItemIndex++;
                //  if schedule ends and replay item 0, play next schedule               
                if (sche.CurrentItemIndex == 0)
                    nextSchedule++;
                return item;
            }
            else
            {
                nextSchedule++;
                return null; 
            }
        }
        public bool WriteToXML(string XMLPath)
        {
            // Write To XML
            // Delete XML file if already existed.
            if (File.Exists(XMLPath))
                File.Delete(XMLPath);
            XmlDocument xmlDoc = new XmlDocument();        // Write down the XML declaration
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);

            // Create the root element
            XmlElement rootNode = xmlDoc.CreateElement("ScheduleList");
            // 
            xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
            XmlNode nodes = xmlDoc.AppendChild(rootNode);
            // add some child nodes
            for (int i = 0; i < schedules.Count; i++)
            {
                XmlElement xmlschedule = xmlDoc.CreateElement("Schedule");
                xmlschedule.SetAttribute("Name", schedules[i].Name);
                xmlschedule.SetAttribute("StartTime", schedules[i].StartTime.ToString());
                xmlschedule.SetAttribute("EndTime", schedules[i].StartTime.ToString());
                xmlschedule.SetAttribute("Continuous", schedules[i].Continuous.ToString());
                xmlschedule.SetAttribute("PlayOnePerTime", schedules[i].PlayOnePerTime.ToString());
                xmlschedule.SetAttribute("Mon", schedules[i].Mon.ToString());
                xmlschedule.SetAttribute("Tue", schedules[i].Tue.ToString());
                xmlschedule.SetAttribute("Wed", schedules[i].Wed.ToString());
                xmlschedule.SetAttribute("Thr", schedules[i].Thr.ToString());
                xmlschedule.SetAttribute("Fri", schedules[i].Fri.ToString());
                xmlschedule.SetAttribute("Sat", schedules[i].Sat.ToString());
                xmlschedule.SetAttribute("Sun", schedules[i].Sun.ToString());
                foreach (PlayItem item in schedules[i].PlayItems)
                {
                    XmlElement xmlitem = xmlDoc.CreateElement("Item");
                    xmlitem.SetAttribute("Title", item.Title);
                    xmlitem.SetAttribute("Path", item.Path);
                    xmlitem.SetAttribute("Duration", item.Duration.ToString());
                    xmlschedule.AppendChild(xmlitem);
                }
                rootNode.AppendChild(xmlschedule);
            }
            xmlDoc.Save(XMLPath);
            return true;
        }
        // Read From XML
        public bool ReadFromXML(string XMLPath)
        {
            // see if XML is existed
            schedules.Clear();
            if (File.Exists(XMLPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(XMLPath);

                // load Contents of root Element, may not need
                //int myRootValue = 0;
                //int.TryParse(xmlDoc.DocumentElement.Attributes["ScheduleList"].Value, out myRootValue);
                // get all child nodes and parse them one by one
                XmlNodeList nodes = xmlDoc.DocumentElement.ChildNodes;
                foreach (XmlNode xmlnode in nodes)
                {
                    // now need to deal with all elements and maybe other types of Node
                    if (xmlnode.Name == "Schedule" && xmlnode.NodeType == XmlNodeType.Element)
                    {
                        Schedule schedule = new Schedule();
                        schedule.Name = xmlnode.Attributes["Name"].Value;
                        DateTime time;
                        if (DateTime.TryParse(xmlnode.Attributes["StartTime"].Value, out time))
                            schedule.StartTime = time;
                        if (DateTime.TryParse(xmlnode.Attributes["EndTime"].Value, out time))
                            schedule.EndTime = time;
                        bool res = false;
                        if (bool.TryParse(xmlnode.Attributes["Continuous"].Value, out res))
                            schedule.Continuous = res;
                        if (bool.TryParse(xmlnode.Attributes["PlayOnePerTime"].Value, out res))
                            schedule.PlayOnePerTime = res;
                        if (bool.TryParse(xmlnode.Attributes["Mon"].Value, out res))
                            schedule.Mon = res;
                        if (bool.TryParse(xmlnode.Attributes["Tue"].Value, out res))
                            schedule.Tue = res;
                        if (bool.TryParse(xmlnode.Attributes["Wed"].Value, out res))
                            schedule.Wed = res;
                        if (bool.TryParse(xmlnode.Attributes["Thr"].Value, out res))
                            schedule.Thr = res;
                        if (bool.TryParse(xmlnode.Attributes["Fri"].Value, out res))
                            schedule.Fri = res;
                        if (bool.TryParse(xmlnode.Attributes["Sat"].Value, out res))
                            schedule.Sat = res;
                        if (bool.TryParse(xmlnode.Attributes["Sun"].Value, out res))
                            schedule.Sun = res;
                        foreach (XmlNode xmlitem in xmlnode.ChildNodes)
                        {
                            // now need to deal with all elements and maybe other types of Node
                            if (xmlitem.Name == "Item" && xmlnode.NodeType == XmlNodeType.Element)
                            {
                                PlayItem item = new PlayItem("","", 0);
                                int duration = 0;
                                if (int.TryParse(xmlitem.Attributes["Duration"].Value, out duration))
                                    item.Duration = duration;
                                item.Path = xmlitem.Attributes["Path"].Value;
                                item.Title = xmlitem.Attributes["Title"].Value;
                                schedule.PlayItems.Add(item);
                            }
                        }
                        schedules.Add(schedule);
                    }
                }
            }
            return true;
            // That is all
        }
        /*
        public void DoWork()
        {
            while (true)
            {
                Thread.Sleep(100);
                PlayItem item = readSchedule();
                if (item != null)
                {
                    displayForm.Play(item);
                }
            }
        }
         */ 
    }
}
