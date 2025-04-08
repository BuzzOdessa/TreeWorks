using System.Drawing;

namespace TreeWorks
{
    public class Node
    {
        public int data;
        public int level;
        public Node? left, right;
        public Node? Parent = null;


        public Node(int d)
        {
            this.data = d;
            
            left  = null;
            right = null;
        }
    }
    public class TreeClass
    {
        public int Count { get; private set; }
        public Node Root { get; set; }

        /*private Node InternalInsert(int v, Node parent = null, bool isRecursive = false, int newLevel=1 )
        {
            Node result = null;

            if (parent is null)
            {
                
                
                if (Count == 0 && !isRecursive)
                {
                    Root = result;
                    result.level = 1;
                    Count = 1;
                    result = new Node(v) { level = newLevel, Parent = parent };
                    return result;
                }
                else
                if (isRecursive)
                {
                    ++Count;
                    result = new Node(v) { level = newLevel, Parent = parent };
                    return result;
                }
                else
                    parent = Root;                        
            }
            
            if (v < parent.data)
            {
                result = InternalInsert(v, parent.left, isRecursive: true);
                if (parent.left is null)
                {
                    parent.left = result;
                    result.level = parent.level + 1;
                }  
                
            }
            else
            {
                result = InternalInsert(v, parent.right, isRecursive: true);
                if (parent.right is null)
                {
                    parent.right = result;
                    result.level = parent.level + 1;
                }
                    
            }
            return result;
        }*/

        /// <summary>
        /// Поиск узла по значению
        /// </summary>
        /// <param name="data">Искомое значение</param>
        /// <param name="startWithNode">Узел начала поиска</param>
        /// <returns>Найденный узел</returns>
        public Node FindNode(int data, Node startWithNode = null)
        {
            startWithNode = startWithNode ?? Root;

            if (data == startWithNode.data)
                return startWithNode;
            else
            if (data < startWithNode.data)
            {
                if (startWithNode.left is null)
                    return null;
                else
                    return FindNode(data, startWithNode.left);
            }
            else            
            {
                if (startWithNode.right is null)
                    return null;
                else
                    return FindNode(data, startWithNode.right);
            }
      }

        

        /// <summary>
        /// Добавление нового узла в бинарное дерево
        /// </summary>
        /// <param name="node">Новый узел</param>
        /// <param name="currentNode">Текущий узел</param>
        /// <returns>Узел</returns>
        public Node Add(Node node, Node currentNode = null)
        {
            if (Root is null)
            {
                node.Parent = null;
                return Root = node;
            }

            currentNode = currentNode ?? Root;
            node.Parent = currentNode;
            if (node.data < node.Parent.data)
            {
                if (currentNode.left is null)
                {
                    currentNode.left = node;
                    return currentNode;
                }
                else
                    return Add(node, currentNode.left);
            }
            else
            {
                if (currentNode.right  is null)
                {
                    currentNode.right = node;
                    return currentNode;
                }
                    
                else
                    return Add(node, currentNode.right);

            }            
        }

        public Node Add(int val)
        {
            return Add(new Node(val));
        }

        /// <summary>
        /// Удаление узла бинарного дерева
        /// </summary>
        /// <param name="node">Узел для удаления</param>
        public void Remove(Node node)
        {
            if (node == null)
            {
                return;
            }

            //если у узла нет подузлов, можно его удалить
            if (node.left is null && node.right is null)
            {
                if (node.Parent is not null)
                {
                    if (node.Parent.data > node.data)
                        node.Parent.left = null;
                    else
                        node.Parent.right = null;
                }
            }
            //если нет левого, то правый ставим на место удаляемого 
            else if (node.left is null)
            {
                if (node.Parent is not null)
                {
                    if (node.Parent.data > node.data)
                        node.Parent.left = node.right;
                    else
                        node.Parent.right = node.right;
                }
                node.right.Parent = node.Parent;
            }
            //если нет правого, то левый ставим на место удаляемого 
            else if (node.right is null)
            {
                if (node.Parent is not null)
                { 
                    if (node.Parent.data > node.data)
                        node.Parent.left = node.left;
                    else
                        node.Parent.right = node.left;
                }
                node.left.Parent = node.Parent;
            }
            //если оба дочерних присутствуют, 
            //то правый становится на место удаляемого,
            //а левый вставляется в правый
            else
            {
                if (node.Parent.data > node.data)
                {
                    node.Parent.left = node.right;
                    node.right.Parent = node.Parent;
                    Add(node.left, node.right);
                }
                else
                if (node.Parent.data < node.data)
                {
                    node.Parent.right = node.right;
                    node.right.Parent = node.Parent;
                    Add(node.left, node.right);
                }
                else
                {
                    var bufLeft = node.left;
                    var bufRightLeft = node.right.left;
                    var bufRightRight = node.right.right;
                    node.data = node.right.data;
                    node.right = bufRightRight;
                    node.left = bufRightLeft;
                    Add(bufLeft, node);
                }                
            }
        }

        /// <summary>
        /// Вывод бинарного дерева
        /// </summary>
        public void PrintTree()
        {
            PrintTree(Root);
        }

        /// <summary>
        /// Вывод бинарного дерева начиная с указанного узла
        /// </summary>
        /// <param name="startNode">Узел с которого начинается печать</param>
        /// <param name="indent">Отступ</param>
        /// <param name="side">Сторона</param>
        private void PrintTree(Node startNode, string indent = "", bool printLeft= false )
        {
            if (startNode is not null)
            {
                var nodeSide = startNode == Root ? "+" : printLeft ? "L" : "R";
                var parentData = startNode.Parent == null? "" : startNode.Parent.data.ToString()+":" ;
                Console.WriteLine($"{indent} [{nodeSide}]- {parentData}{startNode.data}");
                indent += new string(' ', 3);
                //рекурсивный вызов для левой и правой веток
                PrintTree(startNode.left, indent, true);
                PrintTree(startNode.right, indent, false);
            }
        }
        public void Traverse(Node node)
        {            
            if (node == null)         
                return;
            Console.WriteLine($"{node.level}:   {node.data}");
            Traverse(node.left);            
            Traverse(node.right);
        }
    }
}
