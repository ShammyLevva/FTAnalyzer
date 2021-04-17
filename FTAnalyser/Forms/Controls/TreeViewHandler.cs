using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FTAnalyzer.Forms.Controls
{
    public class TreeViewHandler
    {
        static TreeViewHandler instance;
        TreeNode mainformTreeRootNode;
        TreeNode placesTreeRootNode;

        TreeViewHandler() => ResetData();

        public static TreeViewHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TreeViewHandler();
                }
                return instance;
            }
        }

        public void ResetData()
        {
            mainformTreeRootNode = null;
            placesTreeRootNode = null;
        }

        #region Location Tree Building
        public TreeNode[] GetAllLocationsTreeNodes(Font defaultFont, bool mainform, ToolStripProgressBar progressBar)
        {
            try
            {
                if (mainformTreeRootNode != null)
                    return BuildTreeNodeArray(mainform);
                progressBar.Value = 0;
                int locationCount = 0;
                progressBar.Maximum = FamilyTree.Instance.AllDisplayPlaces.Count;
                mainformTreeRootNode = new TreeNode();
                placesTreeRootNode = new TreeNode();
                Font regularFont = new Font(defaultFont, FontStyle.Regular);
                Font boldFont = new Font(defaultFont, FontStyle.Bold);
                foreach (FactLocation location in FamilyTree.Instance.AllDisplayPlaces)
                {
                    string[] parts = location.GetParts();
                    TreeNode currentM = mainformTreeRootNode;
                    TreeNode currentP = placesTreeRootNode;
                    foreach (string part in parts)
                    {
                        if (part.Length == 0 && !Properties.GeneralSettings.Default.AllowEmptyLocations) break;
                        TreeNode childM = currentM.Nodes.Find(part, false).FirstOrDefault();
                        TreeNode childP = currentP.Nodes.Find(part, false).FirstOrDefault();
                        if (childM == null)
                        {
                            TreeNode child = new TreeNode((part.Length == 0 ? "<blank>" : part))
                            {
                                Name = part,
                                Tag = location,
                                ToolTipText = "Geocoding Status : " + location.Geocoded
                            };
                            SetTreeNodeImage(location, child);
                            // Set everything other than known countries and known regions to regular
                            if ((currentM.Level == 0 && Countries.IsKnownCountry(part)) ||
                                (currentM.Level == 1 && Regions.IsKnownRegion(part)))
                                child.NodeFont = boldFont;
                            else
                                child.NodeFont = regularFont;
                            childM = child;
                            childP = (TreeNode)child.Clone();
                            currentM.Nodes.Add(childM);
                            currentP.Nodes.Add(childP);
                        }
                        currentM = childM;
                        currentP = childP;
                    }
                    if (++locationCount % 10 == 0)
                    {
                        progressBar.Value = locationCount;
                        Application.DoEvents();
                    }
                }
                if (Properties.GeneralSettings.Default.AllowEmptyLocations)
                { // trim empty end nodes
                    bool recheck = true;
                    while (recheck)
                    {
                        TreeNode[] emptyNodes = mainformTreeRootNode.Nodes.Find(string.Empty, true);
                        recheck = false;
                        foreach (TreeNode node in emptyNodes)
                        {
                            if (node.FirstNode == null)
                            {
                                node.Remove();
                                recheck = true;
                            }
                        }
                    }
                }
                foreach (TreeNode node in mainformTreeRootNode.Nodes)
                    node.Text += "         "; // force text to be longer to fix bold bug
                foreach (TreeNode node in placesTreeRootNode.Nodes)
                    node.Text += "         "; // force text to be longer to fix bold bug
                regularFont = null;
                boldFont = null; // hopefully fixes dispose bug
            }
            catch (Exception) { }
            return BuildTreeNodeArray(mainform);
        }

        static void SetTreeNodeImage(FactLocation location, TreeNode child)
        {
            if (child == null)
                return;
            switch (location.GeocodeStatus)
            {
                case FactLocation.Geocode.NOT_SEARCHED:
                    child.ImageIndex = 0;
                    child.ToolTipText += "\nUse 'Run Google Geocoder' option under Maps menu to search Google for location.";
                    break;
                case FactLocation.Geocode.MATCHED:
                    child.ImageIndex = 1;
                    break;
                case FactLocation.Geocode.PARTIAL_MATCH:
                    child.ImageIndex = 2;
                    break;
                case FactLocation.Geocode.GEDCOM_USER:
                    child.ImageIndex = 3;
                    break;
                case FactLocation.Geocode.NO_MATCH:
                    child.ImageIndex = 4;
                    break;
                case FactLocation.Geocode.INCORRECT:
                    child.ImageIndex = 5;
                    break;
                case FactLocation.Geocode.OUT_OF_BOUNDS:
                    child.ImageIndex = 6;
                    break;
                case FactLocation.Geocode.LEVEL_MISMATCH:
                    child.ImageIndex = 7;
                    break;
                case FactLocation.Geocode.OS_50KMATCH:
                    child.ImageIndex = 8;
                    break;
                case FactLocation.Geocode.OS_50KPARTIAL:
                    child.ImageIndex = 9;
                    break;
                case FactLocation.Geocode.OS_50KFUZZY:
                    child.ImageIndex = 10;
                    break;
            }
        }

        TreeNode[] BuildTreeNodeArray(bool mainForm)
        {
            TreeNodeCollection nodes;
            if (mainForm)
                nodes = mainformTreeRootNode.Nodes;
            else
                nodes = placesTreeRootNode.Nodes;
            TreeNode[] result = new TreeNode[nodes.Count];
            nodes.CopyTo(result, 0);
            return result;
        }

        public void RefreshTreeNodeIcon(FactLocation location)
        {
            if (location is null) return;
            string[] parts = location.GetParts();
            TreeNode currentM = mainformTreeRootNode;
            TreeNode currentP = placesTreeRootNode;
            foreach (string part in parts)
            {
                if (part.Length == 0 && !Properties.GeneralSettings.Default.AllowEmptyLocations) break;
                if (mainformTreeRootNode != null && currentM != null)
                {
                    TreeNode childM = currentM.Nodes.Find(part, false).FirstOrDefault();
                    currentM = childM;
                }
                if (placesTreeRootNode != null && currentP != null)
                {
                    TreeNode childP = currentP.Nodes.Find(part, false).FirstOrDefault();
                    currentP = childP;
                }
            }
            // we should now have nodes to update   
            if (mainformTreeRootNode != null && currentM != null)
                SetTreeNodeImage(location, currentM);
            if (placesTreeRootNode != null && currentP != null)
                SetTreeNodeImage(location, currentP);
        }
        #endregion
    }
}
