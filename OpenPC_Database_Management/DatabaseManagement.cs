//Team Medjed
//Johnathan Burns, Ethan Spangler, Michael Xie
//Volhacks 2017

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;

namespace OpenPC_Database_Management
{
    public partial class DatabaseManagement : Form
    {
        Dictionary<int, string> hierarchy = new Dictionary<int, string>();
        private ContextMenu treeRightClick = new ContextMenu();
        private readonly WebClient client = new WebClient();
        const string baseAddress = @"http://ec2-54-86-220-26.compute-1.amazonaws.com/?query=";

        public DatabaseManagement()
        {
            InitializeComponent();
            //Implement add and delete buttons when right click on treeview nodes
            treeRightClick.MenuItems.Add("Add");
            treeRightClick.MenuItems.Add("Edit");
            treeRightClick.MenuItems.Add("Delete");
            foreach (MenuItem item in treeRightClick.MenuItems)
            {
                switch (item.Text)
                {
                    case "Add":
                        item.Click += new EventHandler(RightClickAdd_Click);
                        break;
                    case "Delete":
                        item.Click += new EventHandler(RightClickDelete_Click);
                        break;
                    case "Edit":
                        item.Click += new EventHandler(RightClickEdit_Click);
                        break;
                    default:
                        break;
                }
            }
            hierarchy.Add(0, "School");
            hierarchy.Add(1, "Building");
            hierarchy.Add(2, "Room");
            hierarchy.Add(3, "Computer");
        }
                
        private void SQLTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void GenerateTreeFromDB()
        {
            School school = ParseSchoolData();
            SQLTreeView.Nodes.Add(new TreeNode() { Text = school.Name, Tag = school });

            for (int i = 0; i < school.Buildings.Count; i++)
            {
                SQLTreeView.Nodes[0].Nodes.Add(new TreeNode() { Text = school.Buildings[i].Name, Tag = school.Buildings[i] });
                if (school.Buildings[i].Rooms!= null && school.Buildings[i].Rooms.Count > 0)
                {
                    for (int j = 0; j < school.Buildings[i].Rooms.Count; j++)
                    {
                        SQLTreeView.Nodes[0].Nodes[i].Nodes.Add(new TreeNode() { Text = school.Buildings[i].Rooms[j].Name, Tag = school.Buildings[i].Rooms[j] });
                        if (school.Buildings[i].Rooms[j].Computers != null && school.Buildings[i].Rooms[j].Computers.Count > 0)
                        {
                            for (int k = 0; k < school.Buildings[i].Rooms[j].Computers.Count; k++)
                            {
                                SQLTreeView.Nodes[0].Nodes[i].Nodes[j].Nodes.Add(new TreeNode { Text = school.Buildings[i].Rooms[j].Computers[k].Name, Tag = school.Buildings[i].Rooms[j].Computers[k] });
                            }
                        }
                    }
                }
            }
        }

        private void DatabaseManagement_Load(object sender, EventArgs e)
        {
            GenerateTreeFromDB();
        }

        private void SQLTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SQLTreeView.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right)
            {
                SQLTreeView.ContextMenu = treeRightClick;
            }
            else if(e.Button == MouseButtons.Left)
            {
                //Display Info about selected node
                InfoLabel.Text = GetNodeInfo(SQLTreeView.SelectedNode);
            }
        }

        private void RightClickAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked \"Add\" option");
            SQLTreeView.SelectedNode.Nodes.Add("T");
            //SQLTreeView.SelectedNode.Nodes.Add(new TreeNode() { Text = "test", Tag = (object)testbuilding});
            if (SQLTreeView.SelectedNode.Nodes.Count > 0 && !SQLTreeView.SelectedNode.IsExpanded)
                SQLTreeView.SelectedNode.Expand();
        }

        private void RightClickDelete_Click(object sender, EventArgs e)
        {
            DialogResult result =  MessageBox.Show($"You wish to delete \"{SQLTreeView.SelectedNode.Text}\" and all of its child nodes?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SQLTreeView.SelectedNode.Remove();
            }
        }

        private void RightClickEdit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked \"Edit\" option");
        }

        private string GetNodeInfo(TreeNode node)
        {
            string displayStr = String.Empty;
            switch (node.Level)
            {
                case 0:
                    displayStr += "Node type:\n" +
                                  "    School Node\n" +
                                  "Name:\n" +
                                 $"    {((School)node.Tag).Name}\n" +
                                  "Longitude:\n" +
                                 $"    {((School)node.Tag).Longitude}\n" +
                                  "Latitude:\n" +
                                 $"    {((School)node.Tag).Latitude}\n" +
                                  "Site:\n" +
                                 $"    {((School)node.Tag).Site}";
                    break;
                case 1:
                    displayStr += "Node type:\n" +
                                  "    Building Node\n" +
                                  "Name:\n" +
                                 $"    {((Building)node.Tag).Name}\n" +
                                  "ID:\n" +
                                 $"    {((Building)node.Tag).ID}\n" +
                                  "Longitude:\n" +
                                 $"    {((Building)node.Tag).Longitude.ToString()}\n" +
                                  "Latitude:\n" +
                                 $"    {((Building)node.Tag).Latitude.ToString()}";
                    break;
                case 2:
                    displayStr += "Node type:\n" +
                                  "    Room Node\n" +
                                  "Name:\n" +
                                 $"    {((Room)node.Tag).Name}\n" +
                                  "ID:\n" +
                                 $"    {((Room)node.Tag).ID}\n" +
                                  "Floor:\n" +
                                 $"    {((Room)node.Tag).Floor.ToString()}";
                    break;
                case 3:
                    displayStr += "Node type:\n" +
                                  "    Computer Node\n" +
                                  "Name:\n" +
                                 $"    {((Computer)node.Tag).Name}\n" +
                                  "ID:\n" +
                                 $"    {((Computer)node.Tag).ID}";
                    break;
                default:
                    break;
            }
            return displayStr;
        }
        
        private string GetWebContent(string address)
        {
            string downloadString = client.DownloadString(address);
            return downloadString;
        }

        private School ParseSchoolData()
        {
            string webContent = GetWebContent(baseAddress + @"Select%20*FROM%20school");
            string dataStart = "\"data\":";
            Match schoolMatch = Regex.Match(webContent.Substring(webContent.IndexOf(dataStart) + dataStart.Length), @"\[([^[\]]*)\]");
            string[] dataArr = schoolMatch.Value.Trim(']').Trim('[').Split(',');
            School school = new School()
            {
                Name = dataArr[0].Trim('\"'),
                Latitude = Convert.ToDouble(dataArr[1].Trim('\"')),
                Longitude = Convert.ToDouble(dataArr[2].Trim('\"')),
                Site = dataArr[3].Trim('\"'),
                Buildings = ParseBuildingData()
            };
            return school;
        }

        private List<Building> ParseBuildingData()
        {
            List<Building> buildings = new List<Building>();
            string webContent = GetWebContent(baseAddress + @"Select%20*FROM%20buildings");
            string dataStart = "\"data\":";
            MatchCollection buildingMatch = Regex.Matches(webContent.Substring(webContent.IndexOf(dataStart) + dataStart.Length), @"\[([^[\]]*)\]");
            foreach (Match match in buildingMatch)
            {
                string[] dataArr = match.Value.Trim(']').Trim('[').Split(',');
                buildings.Add(new Building()
                {
                    ID = Convert.ToInt32(dataArr[0]),
                    Name = dataArr[1].Trim('\"'),
                    Latitude = Convert.ToDouble(dataArr[2].Trim('\"')),
                    Longitude = Convert.ToDouble(dataArr[3].Trim('\"')),
                    Rooms = ParseRoomDataForABuilding(Convert.ToInt32(dataArr[0]))
                });
            }
            return buildings;
        }

        private List<Room> ParseRoomDataForABuilding(int ID)
        {
            List<Room> rooms = new List<Room>();
            string webContent = GetWebContent(baseAddress + @"Select%20*FROM%20rooms" + $"%20WHERE%20room_building_id={ID.ToString()}");
            string dataStart = "\"data\":";
            MatchCollection roomMatch = Regex.Matches(webContent.Substring(webContent.IndexOf(dataStart) + dataStart.Length), @"\[([^[\]]*)\]");
            if (roomMatch == null || roomMatch.Count <= 0)
                return null;
            foreach (Match match in roomMatch)
            {
                string[] dataArr = match.Value.Trim(']').Trim('[').Split(',');
                rooms.Add(new Room()
                {
                    ID = Convert.ToInt32(dataArr[0]),
                    Name = dataArr[2].Trim('\"'),
                    Floor = string.IsNullOrWhiteSpace(dataArr[3].Trim('\"'))? -1:Convert.ToInt32(dataArr[3].Trim('\"')),
                    Computers = ParseComputerDataForARoom(Convert.ToInt32(dataArr[0]))
                });
            }
            return rooms;
        }

        private List<Computer> ParseComputerDataForARoom(int ID)
        {
            List<Computer> computers = new List<Computer>();
            string webContent = GetWebContent(baseAddress + @"Select%20*FROM%20computers" + $"%20WHERE%20computer_room_id={ID.ToString()}");
            string dataStart = "\"data\":";
            MatchCollection compMatch = Regex.Matches(webContent.Substring(webContent.IndexOf(dataStart) + dataStart.Length), @"\[([^[\]]*)\]");
            if (compMatch == null || compMatch.Count <= 0)
                return null;
            foreach (Match match in compMatch)
            {
                string[] dataArr = match.Value.Trim(']').Trim('[').Split(',');
                computers.Add(new Computer()
                {
                    ID = dataArr[0].Trim('\"'),
                    Name = dataArr[3].Trim('\"')
                });
            }
            return computers;
        }
    }
}
