using System.ComponentModel;

namespace FTAnalyzer.Utilities
{
    public class MultiSelectTreeview : TreeView
    {

        #region Selected Node(s) Properties

        readonly List<TreeNode> m_SelectedNodes;
        [DefaultValue(null)]
        public List<TreeNode> SelectedNodes
        {
            get => m_SelectedNodes;
            set
            {
                ClearSelectedNodes();
                if (value is not null)
                {
                    foreach (TreeNode node in value)
                    {
                        ToggleNode(node, true);
                    }
                }
            }
        }

        // Note we use the new keyword to Hide the native treeview's SelectedNode property.
        private TreeNode m_SelectedNode;
        [DefaultValue(null)]
        public new TreeNode SelectedNode
        {
            get => m_SelectedNode;
            set
            {
                ClearSelectedNodes();
                if (value is not null)
                {
                    SelectNode(value);
                }
            }
        }

        #endregion

        public MultiSelectTreeview()
        {
            m_SelectedNodes = [];
            base.SelectedNode = null;
        }

        #region Overridden Events

        protected override void OnGotFocus(EventArgs e)
        {
            // Make sure at least one node has a selection
            // this way we can tab to the ctrl and use the 
            // keyboard to select nodes
            try
            {
                if (m_SelectedNode is null && TopNode is not null)
                {
                    ToggleNode(TopNode, true);
                    OnAfterSelect(new TreeViewEventArgs(m_SelectedNode));
                }
                // Handle if HideSelection property is in use.
                // Always redraw as highlighted when we gain focus
                HighlightSelection();

                base.OnGotFocus(e);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            try
            {
                // Handle if HideSelection property is in use.
                if (HideSelection)
                {
                    DimSelection();
                }

                base.OnLostFocus(e);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            // If the user clicks on a node that was not
            // previously selected, select it now.

            try
            {
                base.SelectedNode = null;

                TreeNode? node = GetNodeAt(e.Location);
                if (node is null) return;
                int leftBound = node.Bounds.X; // - 20; // Allow user to click on image
                int rightBound = node.Bounds.Right + 10; // Give a little extra room
                if (e.Location.X > leftBound && e.Location.X < rightBound)
                {
                    if (ModifierKeys == Keys.None && (m_SelectedNodes.Contains(node)))
                    {
                        // Potential Drag Operation
                        // Let Mouse Up do select
                    }
                    else
                    {
                        SelectNode(node);
                    }
                }
                base.OnMouseDown(e);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            // If the clicked on a node that WAS previously
            // selected then, reselect it now. This will clear
            // any other selected nodes. e.g. A B C D are selected
            // the user clicks on B, now A C & D are no longer selected.
            try
            {
                // Check to see if a node was clicked on 
                TreeNode? node = GetNodeAt(e.Location);
                if (node is not null && ModifierKeys == Keys.None && m_SelectedNodes.Contains(node) && m_SelectedNodes.Count > 1)
                {
                    int leftBound = node.Bounds.X; // -20; // Allow user to click on image
                    int rightBound = node.Bounds.Right + 10; // Give a little extra room
                    if (e.Location.X > leftBound && e.Location.X < rightBound)
                    {

                        SelectNode(node);
                    }
                }

                base.OnMouseUp(e);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        //protected override void OnItemDrag(ItemDragEventArgs e)
        //{
        //    // If the user drags a node and the node being dragged is NOT
        //    // selected, then clear the active selection, select the
        //    // node being dragged and drag it. Otherwise if the node being
        //    // dragged is selected, drag the entire selection.
        //    try
        //    {
        //        TreeNode node = e.Item as TreeNode;

        //        if (node is not null)
        //        {
        //            if (!m_SelectedNodes.Contains(node))
        //            {
        //                SelectSingleNode(node);
        //                ToggleNode(node, true);
        //            }
        //        }

        //        base.OnItemDrag(e);
        //    }
        //    catch (Exception ex)
        //    {
        //        HandleException(ex);
        //    }
        //}

        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            // Never allow base.SelectedNode to be set!
            try
            {
                base.SelectedNode = null;
                e.Cancel = true;

                base.OnBeforeSelect(e);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            // Never allow base.SelectedNode to be set!
            try
            {
                base.OnAfterSelect(e);
                base.SelectedNode = null;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Handle all possible key strokes for the control.
            // including navigation, selection, etc.

            base.OnKeyDown(e);

            if (e.KeyCode == Keys.ShiftKey) return;
            if (e.KeyCode == Keys.ControlKey) return;

            //BeginUpdate();
            bool bShift = (ModifierKeys == Keys.Shift);

            try
            {
                // Nothing is selected in the tree, this isn't a good state
                // select the top node
                if (m_SelectedNode is null && TopNode is not null)
                {
                    ToggleNode(TopNode, true);
                    OnAfterSelect(new TreeViewEventArgs(m_SelectedNode));
                }

                // Nothing is still selected in the tree, this isn't a good state, leave.
                if (m_SelectedNode is null) return;

                if (e.KeyCode == Keys.Left)
                {
                    if (m_SelectedNode.IsExpanded && m_SelectedNode.Nodes.Count > 0)
                    {
                        // Collapse an expanded node that has children
                        m_SelectedNode.Collapse();
                    }
                    else if (m_SelectedNode.Parent is not null)
                    {
                        // Node is already collapsed, try to select its parent.
                        SelectSingleNode(m_SelectedNode.Parent);
                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (!m_SelectedNode.IsExpanded)
                    {
                        // Expand a collpased node's children
                        m_SelectedNode.Expand();
                    }
                    else
                    {
                        // Node was already expanded, select the first child
                        SelectSingleNode(m_SelectedNode.FirstNode);
                    }
                }
                else if (e.KeyCode == Keys.Up)
                {
                    // Select the previous node
                    if (m_SelectedNode.PrevVisibleNode is not null)
                    {
                        SelectNode(m_SelectedNode.PrevVisibleNode);
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    // Select the next node
                    if (m_SelectedNode.NextVisibleNode is not null)
                    {
                        SelectNode(m_SelectedNode.NextVisibleNode);
                    }
                }
                else if (e.KeyCode == Keys.Home)
                {
                    if (bShift)
                    {
                        if (m_SelectedNode.Parent is null)
                        {
                            // Select all of the root nodes up to this point 
                            if (Nodes.Count > 0)
                            {
                                SelectNode(Nodes[0]);
                            }
                        }
                        else
                        {
                            // Select all of the nodes up to this point under this nodes parent
                            if (m_SelectedNode.Parent.FirstNode is not null)
                                SelectNode(m_SelectedNode.Parent.FirstNode);
                        }
                    }
                    else
                    {
                        // Select this first node in the tree
                        if (Nodes.Count > 0)
                        {
                            SelectSingleNode(Nodes[0]);
                        }
                    }
                }
                else if (e.KeyCode == Keys.End)
                {
                    if (bShift)
                    {
                        if (m_SelectedNode.Parent is null)
                        {
                            // Select the last ROOT node in the tree
                            if (Nodes.Count > 0)
                                SelectNode(Nodes[^1]);
                        }
                        else
                        {
                            // Select the last node in this branch
                            if (m_SelectedNode.Parent.LastNode is not null)
                                SelectNode(m_SelectedNode.Parent.LastNode);
                        }
                    }
                    else
                    {
                        if (Nodes.Count > 0)
                        {
                            // Select the last node visible node in the tree.
                            // Don't expand branches incase the tree is virtual
                            TreeNode? ndLast = Nodes[0].LastNode;
                            while (ndLast.IsExpanded && (ndLast.LastNode is not null))
                            {
                                ndLast = ndLast.LastNode;
                            }
                            SelectSingleNode(ndLast);
                        }
                    }
                }
                else if (e.KeyCode == Keys.PageUp)
                {
                    // Select the highest node in the display
                    int nCount = VisibleCount;
                    TreeNode ndCurrent = m_SelectedNode;
                    while ((nCount) > 0 && (ndCurrent.PrevVisibleNode is not null))
                    {
                        ndCurrent = ndCurrent.PrevVisibleNode;
                        nCount--;
                    }
                    SelectSingleNode(ndCurrent);
                }
                else if (e.KeyCode == Keys.PageDown)
                {
                    // Select the lowest node in the display
                    int nCount = VisibleCount;
                    TreeNode ndCurrent = m_SelectedNode;
                    while ((nCount) > 0 && (ndCurrent.NextVisibleNode is not null))
                    {
                        ndCurrent = ndCurrent.NextVisibleNode;
                        nCount--;
                    }
                    SelectSingleNode(ndCurrent);
                }
                else if (e.Control && (e.KeyCode == Keys.A))
                {
                    // Select All (CTRL-A), selects all top level nodes
                    ClearSelectedNodes();
                    CollapseAll();
                    TreeNode? ndCurrent = TopNode;
                    while (ndCurrent is not null)
                    {
                        ToggleNode(ndCurrent, true);
                        ndCurrent = ndCurrent.NextNode;
                    }
                }
                else
                {
                    // Assume this is a search character a-z, A-Z, 0-9, etc.
                    // Select the first node after the current node that 
                    // starts with this character
                    string sSearch = ((char)e.KeyValue).ToString();

                    TreeNode ndCurrent = m_SelectedNode;
                    while ((ndCurrent.NextVisibleNode is not null))
                    {
                        ndCurrent = ndCurrent.NextVisibleNode;
                        if (ndCurrent.Text.StartsWith(sSearch))
                        {
                            SelectSingleNode(ndCurrent);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                EndUpdate();
            }
        }

        #endregion

        #region Helper Methods

        void HighlightSelection()
        {
            foreach (TreeNode node in SelectedNodes)
            {
                node.BackColor = SystemColors.Highlight;
                node.ForeColor = SystemColors.HighlightText;
            }
        }

        void DimSelection()
        {
            foreach (TreeNode node in SelectedNodes)
            {
                node.BackColor = SystemColors.Control;
                node.ForeColor = this.ForeColor;
            }
        }

        void SelectNode(TreeNode node)
        {
            try
            {
                BeginUpdate();

                if (m_SelectedNode is null || ModifierKeys == Keys.Control)
                {
                    // Ctrl+Click selects an unselected node, or unselects a selected node.
                    bool bIsSelected = m_SelectedNodes.Contains(node);
                    ToggleNode(node, !bIsSelected);
                    OnAfterSelect(new TreeViewEventArgs(m_SelectedNode));
                }
                else if (ModifierKeys == Keys.Shift)
                {
                    // Shift+Click selects nodes between the selected node and here.
                    TreeNode? ndStart = m_SelectedNode;
                    TreeNode? ndEnd = node;

                    if (ndStart.Parent == ndEnd.Parent)
                    {
                        // Selected node and clicked node have same parent, easy case.
                        if (ndStart.Index < ndEnd.Index)
                        {
                            // If the selected node is beneath the clicked node walk down
                            // selecting each Visible node until we reach the end.
                            while (ndStart != ndEnd)
                            {
                                ndStart = ndStart.NextVisibleNode;
                                if (ndStart is null) break;
                                ToggleNode(ndStart, true);
                            }
                        }
                        else if (ndStart.Index == ndEnd.Index)
                        {
                            // Clicked same node, do nothing
                        }
                        else
                        {
                            // If the selected node is above the clicked node walk up
                            // selecting each Visible node until we reach the end.
                            while (ndStart != ndEnd)
                            {
                                ndStart = ndStart.PrevVisibleNode;
                                if (ndStart is null) break;
                                ToggleNode(ndStart, true);
                            }
                        }
                    }
                    else
                    {
                        // Selected node and clicked node have same parent, hard case.
                        // We need to find a common parent to determine if we need
                        // to walk down selecting, or walk up selecting.

                        TreeNode? ndStartP = ndStart;
                        TreeNode? ndEndP = ndEnd;
                        int startDepth = Math.Min(ndStartP.Level, ndEndP.Level);

                        // Bring lower node up to common depth
                        while (ndStartP.Level > startDepth)
                        {
                            ndStartP = ndStartP.Parent;
                        }

                        // Bring lower node up to common depth
                        while (ndEndP.Level > startDepth)
                        {
                            ndEndP = ndEndP.Parent;
                        }

                        // Walk up the tree until we find the common parent
                        while (ndStartP.Parent != ndEndP.Parent)
                        {
                            ndStartP = ndStartP.Parent;
                            ndEndP = ndEndP.Parent;
                        }

                        // Select the node
                        if (ndStartP.Index < ndEndP.Index)
                        {
                            // If the selected node is beneath the clicked node walk down
                            // selecting each Visible node until we reach the end.
                            while (ndStart != ndEnd)
                            {
                                ndStart = ndStart.NextVisibleNode;
                                if (ndStart is null) break;
                                ToggleNode(ndStart, true);
                            }
                        }
                        else if (ndStartP.Index == ndEndP.Index)
                        {
                            if (ndStart.Level < ndEnd.Level)
                            {
                                while (ndStart != ndEnd)
                                {
                                    ndStart = ndStart.NextVisibleNode;
                                    if (ndStart is null) break;
                                    ToggleNode(ndStart, true);
                                }
                            }
                            else
                            {
                                while (ndStart != ndEnd)
                                {
                                    ndStart = ndStart.PrevVisibleNode;
                                    if (ndStart is null) break;
                                    ToggleNode(ndStart, true);
                                }
                            }
                        }
                        else
                        {
                            // If the selected node is above the clicked node walk up
                            // selecting each Visible node until we reach the end.
                            while (ndStart != ndEnd)
                            {
                                ndStart = ndStart.PrevVisibleNode;
                                if (ndStart is null) break;
                                ToggleNode(ndStart, true);
                            }
                        }
                    }
                    OnAfterSelect(new TreeViewEventArgs(m_SelectedNode));
                }
                else
                {
                    // Just clicked a node, select it
                    SelectSingleNode(node);
                }
            }
            finally
            {
                EndUpdate();
            }
        }

        void ClearSelectedNodes()
        {
            try
            {
                foreach (TreeNode node in m_SelectedNodes)
                {
                    node.BackColor = BackColor;
                    node.ForeColor = ForeColor;
                }
            }
            finally
            {
                m_SelectedNodes.Clear();
                m_SelectedNode = null;
            }
        }

        void SelectSingleNode(TreeNode? node)
        {
            if (node is null) return;
            ClearSelectedNodes();
            ToggleNode(node, true);
            node.EnsureVisible();

            OnAfterSelect(new TreeViewEventArgs(node));
        }

        void ToggleNode(TreeNode node, bool bSelectNode)
        {
            if (bSelectNode)
            {
                m_SelectedNode = node;
                if (!m_SelectedNodes.Contains(node))
                {
                    m_SelectedNodes.Add(node);
                }
                node.BackColor = SystemColors.Highlight;
                node.ForeColor = SystemColors.HighlightText;
            }
            else
            {
                m_SelectedNodes.Remove(node);
                node.BackColor = BackColor;
                node.ForeColor = ForeColor;
            }
        }

        static void HandleException(Exception ex)
        {
            // Perform some error handling here.
            // We don't want to bubble errors to the CLR. 
            UIHelpers.ShowMessage(ex.Message);
        }

        #endregion
    }
}
