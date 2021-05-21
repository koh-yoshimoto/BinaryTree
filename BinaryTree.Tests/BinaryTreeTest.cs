using NUnit.Framework;

namespace BinaryTree.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var tree = new BinaryTree();
            tree.Add(5);
            tree.Add(3);
            tree.Add(8);
            tree.Add(4);
            tree.Add(6);
            tree.Add(2);
            tree.Add(9);
            System.Collections.IEnumerator itr = tree.GetEnumerator();
            itr.MoveNext();
            Assert.AreEqual(2, itr.Current);
            itr.MoveNext();
            itr.MoveNext();
            itr.MoveNext();
            itr.MoveNext();
            itr.MoveNext();
            itr.MoveNext();
            itr.Reset();

            tree.Remove(9);
            tree.Remove(5);
            tree.Remove(3);
            tree.Remove(2);
            tree.Remove(6);
            tree.Remove(4);
            tree.Remove(8);
        }
    }
}