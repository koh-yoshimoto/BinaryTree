using System;

namespace BinaryTree
{
    public class BinaryTree
    {
        public BinaryTreeNode Root { get; private set; }

        public int Count { get; private set; }

        public BinaryTree()
        {
            Root = null;
            Count = 0;
        }

        public void Add(int data)
        {
            var node = new BinaryTreeNode(data);
            BinaryTreeNode current = Root;

            if (Root == null)
            {
                Root = node;
                Count = 1;
                return;
            }

            while (true)
            {
                if (current.Data > node.Data && current.Left != null)
                {
                    current = current.Left;
                }
                else if (current.Data <= node.Data && current.Right != null)
                {
                    current = current.Right;
                }
                else if (current.Data > node.Data)
                {
                    current.Left = node;
                    node.Parent = current;
                    break;
                }
                else if (current.Data <= node.Data)
                {
                    current.Right = node;
                    node.Parent = current;
                    break;
                }
            }
            Count++;
        }

        public BinaryTreeNode Find(int data)
        {
            if (Root == null) return null;
            BinaryTreeNode current = Root;
            while (current != null)
            {

                if (current.Data > data)
                {
                    current = current.Left;
                }
                else if (current.Data < data)
                {
                    current = current.Right;
                }
                else if (current.Data == data)
                {
                    return current;
                }
            }
            return null;
        }

        public BinaryTreeNode GetLeftMost(BinaryTreeNode root)
        {
            if (root == null) return null;
            BinaryTreeNode current = root;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }

        public BinaryTreeNode GetRightMost(BinaryTreeNode root)
        {
            if (root == null) return null;
            BinaryTreeNode current = root;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current;
        }

        public bool Remove(int data)
        {
            if (Find(data) == null) return false;
            InternalRemove(Root, data);
            return true;
        }

        internal BinaryTreeNode InternalRemove(BinaryTreeNode root, int data)
        {
            if (root == null)
            {
                return null;
            }

            if (root.Data > data)
            {
                root.Left = InternalRemove(root.Left, data);
            }
            else if (root.Data < data)
            {
                root.Right = InternalRemove(root.Right, data);
            }
            else
            {
                if (root.Left == null && root.Right == null)
                {
                    root = null;
                    return root;
                }

                if (root.Left == null)
                {
                    root = root.Right;
                }
                else if (root.Right == null)
                {
                    root = root.Left;
                }
                else
                {
                    BinaryTreeNode maxNode = GetRightMost(root.Left);
                    root.Data = maxNode.Data;
                    root.Left = InternalRemove(root.Left, root.Data);
                }
            }
            return root;
        }


        public override string ToString()
        {
            System.Text.StringBuilder sb = new();
            BuildTree(Root, sb);
            sb.Append("[end]");
            return sb.ToString();
        }

        public void BuildTree(BinaryTreeNode node, System.Text.StringBuilder sb)
        {
            if (node != null)
            {
                BuildTree(node.Left, sb);
                sb.Append($"[{node.Data}]->");
                BuildTree(node.Right, sb);
            }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
    }

    public class Enumerator : System.Collections.IEnumerator
    {
        private BinaryTree tree;
        private BinaryTreeNode node;
        public Object Current { get; private set; }

        public Enumerator(BinaryTree tree)
        {
            this.tree = tree;
            node = this.tree.GetLeftMost(this.tree.Root);
            Current = null;
        }

        public bool MoveNext()
        {
            if (node == null)
            {
                return false;
            }

            Current = node.Data;

            BinaryTreeNode current = null;
            if (node.Right != null)
            {
                current = node.Right;
                while (current.Left != null)
                {
                    current = current.Left;
                }
            }
            else if (node.Parent.Left == node && node.Right == null)
            {
                current = node.Parent;
            }
            else if (node.Parent.Right == node && node.Right == null)
            {
                current = node;
                while (current.Parent != null && current == current.Parent.Right)
                {
                    current = current.Parent;
                    // if (current.Parent == null) break;
                }
                current = current.Parent;
            }

            node = current;
            return true;
        }

        public void Reset()
        {
            Current = null;
            node = tree.GetLeftMost(tree.Root);
        }
    }
    public class BinaryTreeNode
    {
        public int Data { get; set; }
        public BinaryTreeNode Parent { get; set; }
        public BinaryTreeNode Right { get; set; }
        public BinaryTreeNode Left { get; set; }

        public BinaryTreeNode(int data)
        {
            this.Data = data;
        }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            var other = obj as BinaryTreeNode;

            if (this.GetHashCode() != other.GetHashCode())
            {
                return false;
            }

            if (!this.Data.Equals(other.Data))
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"{this.Data}";
        }
    }

}