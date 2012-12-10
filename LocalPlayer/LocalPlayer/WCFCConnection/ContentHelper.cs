using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalPlayer.PlayElements;
using System.Xml;

namespace LocalPlayer.WCFCConnection
{
    class ContentHelper
    {
        static public Schedule XMLStringToSchedule(string str)
        {

            // see if XML is existed
            Schedule schedule = new Schedule();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(str);

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
                            PlayItem item = new PlayItem("", "", 0);
                            int duration = 0;
                            if (int.TryParse(xmlitem.Attributes["Duration"].Value, out duration))
                                item.Duration = duration;
                            item.Path = xmlitem.Attributes["Path"].Value;
                            item.Title = xmlitem.Attributes["Title"].Value;
                            schedule.PlayItems.Add(item);
                        }
                    }
                    return schedule;
                }
            }

            return null;
            // That is all

        }
    }
}
