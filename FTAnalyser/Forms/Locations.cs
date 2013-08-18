using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer.Forms
{
    public partial class Locations : Form
    {
        public Locations()
        {
            InitializeComponent();
        }

        public void BuildLocationTree(List<FactLocation> locations)
        {
            FamilyTree ft = FamilyTree.Instance;
            FactLocation currentLocation = new FactLocation();
            tvLocations.BeginUpdate();
            tvLocations.Nodes.Clear();
            foreach(FactLocation loc in locations)
            {
                if (loc.Country.Length > 0)
                {
                    TreeNode countryNode = AddChild(tvLocations.Nodes, loc.Country);
                    if (loc.Region.Length > 0)
                    {
                        TreeNode regionNode = AddChild(countryNode.Nodes, loc.Region);
                        if (loc.Parish.Length > 0)
                        {
                            TreeNode parishNode = AddChild(regionNode.Nodes, loc.Parish);
                            if (parishNode.Nodes.Count == 0)
                            {
                                List<string> surnames = ft.GetSurnamesAtLocation(loc);
                                foreach (string s in surnames)
                                {
                                    parishNode.Nodes.Add(new TreeNode(s));
                                }
                            }
                        }
                    }
                }
            }
            tvLocations.EndUpdate();
            Cursor.Current = Cursors.Default;
        }

        private TreeNode AddChild(TreeNodeCollection nodes, string location)
        {
            if (!nodes.ContainsKey(location))
            {
                TreeNode child = new TreeNode(location);
                child.Name = location;
                nodes.Add(child);
                return child;
            }
            else
            {
                return nodes[location];
            }
        }

    }
}
