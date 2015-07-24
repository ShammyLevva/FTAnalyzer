using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FTAnalyzer.Utilities
{
    /// <summary>
    /// Summary description for TreeViewMS.
    /// </summary>
    public class TreeViewMS : TreeView
    {
        protected ArrayList selectedNodes;
        protected TreeNode m_lastNode, m_firstNode;

        public TreeViewMS()
        {
            selectedNodes = new ArrayList();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        public ArrayList SelectedNodes
        {
            get { return selectedNodes; }
            set
            {
                RemovePaintFromNodes();
                selectedNodes.Clear();
                selectedNodes = value;
                PaintSelectedNodes();
            }
        }

        // Triggers
        //
        // (overriden method, and base class called to ensure events are triggered)

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);
            if (e.Node.IsSelected && ModifierKeys == Keys.Control && selectedNodes.Count > 1)
            {
                OnBeforeSelect(new TreeViewCancelEventArgs(e.Node, false, TreeViewAction.ByMouse));
                OnAfterSelect(new TreeViewEventArgs(e.Node));
            }
        }

        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            base.OnBeforeSelect(e);

            bool bControl = (ModifierKeys == Keys.Control);
            bool bShift = (ModifierKeys == Keys.Shift);

            // selecting twice the node while pressing CTRL ?
            if (bControl && selectedNodes.Contains(e.Node))
            {
                // unselect it (let framework know we don't want selection this time)
                e.Cancel = true;

                // update nodes
                RemovePaintFromNodes();
                selectedNodes.Remove(e.Node);
                PaintSelectedNodes();
                return;
            }
            m_lastNode = e.Node;
            if (!bShift) m_firstNode = e.Node; // store begin of shift sequence
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            bool bControl = (ModifierKeys == Keys.Control);
            bool bShift = (ModifierKeys == Keys.Shift);

            if (bControl)
            {
                if (!selectedNodes.Contains(e.Node)) // new node ?
                    selectedNodes.Add(e.Node);
                else  // not new, remove it from the collection
                {
                    RemovePaintFromNodes();
                    selectedNodes.Remove(e.Node);
                }
                PaintSelectedNodes();
            }
            else
            {
                // SHIFT is pressed
                if (bShift)
                {
                    Queue myQueue = new Queue();
                    TreeNode uppernode = m_firstNode;
                    TreeNode bottomnode = e.Node;
                    // case 1 : begin and end nodes are parent
                    bool bParent = isParent(m_firstNode, e.Node); // is m_firstNode parent (direct or not) of e.Node
                    if (!bParent)
                    {
                        bParent = isParent(bottomnode, uppernode);
                        if (bParent) // swap nodes
                        {
                            TreeNode t = uppernode;
                            uppernode = bottomnode;
                            bottomnode = t;
                        }
                    }
                    if (bParent)
                    {
                        TreeNode n = bottomnode;
                        while (n != uppernode.Parent)
                        {
                            if (!selectedNodes.Contains(n)) // new node ?
                                myQueue.Enqueue(n);
                            n = n.Parent;
                        }
                    }
                    // case 2 : nor the begin nor the end node are descendant one another
                    else
                    {
                        if ((uppernode.Parent == null && bottomnode.Parent == null) || (uppernode.Parent != null && uppernode.Parent.Nodes.Contains(bottomnode))) // are they siblings ?
                        {
                            int nIndexUpper = uppernode.Index;
                            int nIndexBottom = bottomnode.Index;
                            if (nIndexBottom < nIndexUpper) // reversed?
                            {
                                TreeNode t = uppernode;
                                uppernode = bottomnode;
                                bottomnode = t;
                                nIndexUpper = uppernode.Index;
                                nIndexBottom = bottomnode.Index;
                            }
                            TreeNode n = uppernode;
                            while (nIndexUpper <= nIndexBottom)
                            {
                                if (!selectedNodes.Contains(n)) // new node ?
                                    myQueue.Enqueue(n);
                                n = n.NextNode;
                                nIndexUpper++;
                            } // end while
                        }
                        else
                        {
                            if (!selectedNodes.Contains(uppernode)) myQueue.Enqueue(uppernode);
                            if (!selectedNodes.Contains(bottomnode)) myQueue.Enqueue(bottomnode);
                        }
                    }

                    selectedNodes.AddRange(myQueue);
                    PaintSelectedNodes();
                    m_firstNode = e.Node; // let us chain several SHIFTs if we like it
                } // end if m_bShift
                else
                {
                    // in the case of a simple click, just add this item
                    if (selectedNodes != null && selectedNodes.Count > 0)
                    {
                        RemovePaintFromNodes();
                        selectedNodes.Clear();
                    }
                    selectedNodes.Add(e.Node);
                    PaintSelectedNodes();
                }
            }
            base.OnAfterSelect(e);
        }

        // Helpers
        //
        //

        protected bool isParent(TreeNode parentNode, TreeNode childNode)
        {
            if (parentNode == childNode)
                return true;

            TreeNode n = childNode;
            bool bFound = false;
            while (!bFound && n != null)
            {
                n = n.Parent;
                bFound = (n == parentNode);
            }
            return bFound;
        }

        public void Clear()
        {
            Nodes.Clear();
            selectedNodes.Clear();
            RemovePaintFromNodes();
        }

        protected void PaintSelectedNodes()
        {
            foreach (TreeNode n in selectedNodes)
            {
                n.BackColor = SystemColors.Highlight;
                n.ForeColor = SystemColors.HighlightText;
            }
        }

        protected void RemovePaintFromNodes()
        {
            if (selectedNodes.Count == 0) return;

            TreeNode n0 = (TreeNode)selectedNodes[0];
            Color back = n0.TreeView.BackColor;
            Color fore = n0.TreeView.ForeColor;

            foreach (TreeNode n in selectedNodes)
            {
                n.BackColor = back;
                n.ForeColor = fore;
            }
        }
    }
}
