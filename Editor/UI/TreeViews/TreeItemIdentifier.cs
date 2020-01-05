﻿using System;
using UnityEditor.IMGUI.Controls;

namespace Unity.ProjectAuditor.Editor
{
    public struct TreeItemIdentifier
    {
        public string nameWithIndex { get; private set; }
        public string name { get; private set; }
        // SteveM TODO - Pretty sure this can go. Assemblies don't have indeces. I think the most we'll need is a flag
        // to say whether this is the "All" TreeItemIdentifier (i.e. (nameWithIndex == "All"))
        public int index { get; private set; }

        public static int kAll = -1;
        public static int kSingle = 0;

        public TreeItemIdentifier(string name, int index)
        {
            this.name = name;
            this.index = index;
            if (index == kAll)
                nameWithIndex = string.Format("All:{1}", index, name);
            else
                nameWithIndex = string.Format("{0}:{1}", index, name);
        }

        public TreeItemIdentifier(TreeItemIdentifier treeItemIdentifier)
        {
            name = treeItemIdentifier.name;
            index = treeItemIdentifier.index;
            nameWithIndex = treeItemIdentifier.nameWithIndex;
        }

        public TreeItemIdentifier(string nameWithIndex)
        {
            // SteveM TODO - Pretty sure this can go. Assembly names don't have a foo:N (or N:foo?) naming convention like threads do.
            // So index should probably always be treated as 0 (sorry, "kSingle")
            this.nameWithIndex = nameWithIndex;

            string[] tokens = nameWithIndex.Split(':');
            if (tokens.Length >= 2)
            {
                name = tokens[1];
                string indexString = tokens[0];
                if (indexString == "All")
                {
                    index = kAll;
                }
                else
                {
                    int intValue;
                    if (Int32.TryParse(tokens[0], out intValue))
                        index = intValue;
                    else
                        index = kSingle;
                }
            }
            else
            {
                index = kSingle;
                name = nameWithIndex;
            }
        }

        void UpdateAssemblyNameWithIndex()
        {
            if (index == kAll)
                nameWithIndex = string.Format("All:{1}", index, name);
            else
                nameWithIndex = string.Format("{0}:{1}", index, name);
        }

        public void SetName(string newName)
        {
            name = newName;
            UpdateAssemblyNameWithIndex();
        }

        public void SetIndex(int newIndex)
        {
            index = newIndex;
            UpdateAssemblyNameWithIndex();
        }

        public void SetAll()
        {
            SetIndex(kAll);
        }
    }
    
    class SelectionWindowTreeViewItem : TreeViewItem
    {
        public readonly TreeItemIdentifier TreeItemIdentifier;

        public SelectionWindowTreeViewItem(int id, int depth, string displayName, TreeItemIdentifier treeItemIdentifier) : base(id, depth, displayName)
        {
            this.TreeItemIdentifier = treeItemIdentifier;
        }
    }
}
