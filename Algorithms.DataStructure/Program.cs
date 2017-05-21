using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Algorithms.DataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            SetWithSum2();
            Console.Read();
        }

        #region stack

        static void forStack()
        {
            //string[] line = Console.ReadLine().Split(' ');
            char[] line = Console.ReadLine().ToCharArray();
            Stack<Tuple<string, int>> tempStack = new Stack<Tuple<string, int>>();
            var indexFail = 0;
            var v = string.Empty;
            string[] arrBrackets = new string[] { "(", ")", "[", "]", "{", "}" };

            for (int i = 0; i < line.Length; i++)
            {
                var sChar = line[i].ToString();
                if (Array.IndexOf(arrBrackets, sChar) > -1)
                {
                    if (tempStack.Count > 0)
                    {
                        if (sChar == GetEnumDescription(Brackets.CircleLeft)
                            || sChar == GetEnumDescription(Brackets.SquareLeft)
                            || sChar == GetEnumDescription(Brackets.FigureLeft))
                        {
                            tempStack.Push(new Tuple<string, int>(sChar, i));
                            indexFail = i + 1;
                        }
                        else if (sChar == GetEnumDescription(Brackets.CircleRight))
                        {
                            if (tempStack.Peek().Item1 == GetEnumDescription(Brackets.CircleLeft))
                            {
                                tempStack.Pop();
                            }
                            else
                            {
                                indexFail = i + 1;
                                Console.Write(indexFail);
                                return;
                            }
                        }
                        else if (sChar == GetEnumDescription(Brackets.SquareRight))
                        {
                            if (tempStack.Peek().Item1 == GetEnumDescription(Brackets.SquareLeft))
                            {
                                tempStack.Pop();
                            }
                            else
                            {
                                indexFail = i + 1;
                                Console.Write(indexFail);
                                return;
                            }
                        }
                        else if (sChar == GetEnumDescription(Brackets.FigureRight))
                        {
                            if (tempStack.Peek().Item1 == GetEnumDescription(Brackets.FigureLeft))
                            {
                                tempStack.Pop();
                            }
                            else
                            {
                                indexFail = i + 1;
                                Console.Write(indexFail);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (sChar == GetEnumDescription(Brackets.CircleLeft)
                            || sChar == GetEnumDescription(Brackets.SquareLeft)
                            || sChar == GetEnumDescription(Brackets.FigureLeft))
                        {
                            tempStack.Push(new Tuple<string, int>(sChar, i));
                            indexFail = i + 1;
                        }
                        else
                        {
                            indexFail = i + 1;
                            Console.Write(indexFail);
                            return;
                        }

                    }
                }
            }
            if (tempStack.Count == 0)
            {
                Console.Write("Success");
            }
            else
            {
                var lastItemStack = tempStack.Peek();
                Console.Write(lastItemStack.Item2 + 1);
            }
        }
        enum Brackets
        {
            [Description("(")]
            CircleLeft,
            [Description(")")]
            CircleRight,
            [Description("[")]
            SquareLeft,
            [Description("]")]
            SquareRight,
            [Description("{")]
            FigureLeft,
            [Description("}")]
            FigureRight
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        #endregion

        #region height tree
        static void heightTree()
        {
            int n = int.Parse(Console.ReadLine());
            string[] line = Console.ReadLine().Split(' ');
            var maxN = Math.Pow(10, 5);
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            if (n > 1 && n <= maxN && n == line.Length)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    var s = int.Parse(line[i]);
                    if (!dict.ContainsKey(s))
                    {
                        dict.Add(s, new List<int>());
                    }
                    var list = dict[s];
                    list.Add(i);
                    dict[s] = list;

                }
                Console.WriteLine(recDict(dict[-1], dict));
            }
        }

        static int recDict(List<int> list, Dictionary<int, List<int>> dict)
        {
            var depth = 1;
            foreach (var item in list)
            {
                if (dict.ContainsKey(item))
                {
                    var tempList = dict[item];
                    var d = recDict(tempList, dict);
                    if (d + 1 > depth)
                    {
                        depth = d + 1;
                    }
                }
            }
            return depth;
        }
        //static void heightTree()
        //{
        //    int n = int.Parse(Console.ReadLine());
        //    string[] line = Console.ReadLine().Split(' ');
        //    var tempLine = line.Select((x, i) => new Tuple<string, int>(x, i));
        //    List<Tuple<string, int>> lineList = new List<Tuple<string, int>>(tempLine);
        //    var maxN = Math.Pow(10, 5);
        //    if (n > 1 && n <= maxN && n == line.Length)
        //    {
        //        Node node = new Node();
        //        var index = Array.IndexOf(line, (-1).ToString());
        //        node.Index = index;
        //        node.Value = -1;
        //        lineList.Remove(new Tuple<string, int>("-1", index));
        //        var children = lineList.Where(x => x.Item1 == node.Index.ToString()).Select(x => x.Item2);
        //        node.Children.AddRange(children);
        //        //node.Children.AddRange(line.Select((x,i) => new { V = x, I = i} ).Where(x => x.V == node.Index.ToString()).Select(x => x.I));
        //        lineList = lineList.Where(x => !children.Contains(x.Item2) || x.Item1 == "-1").ToList();
        //        Console.WriteLine(recNode(node, lineList) + 1);
        //    }
        //}
        //static int recNode(Node node, List<Tuple<string, int>> lineList)
        //{
        //    var depth = 1;
        //    foreach(var item in node.Children)
        //    {
        //        //var s = int.Parse(line[item].Item1);
        //        //var t = line.FirstOrDefault(x => x.Item2 == item);
        //        var tempNode = new Node { Index = item, Value = 0 };
        //        var children = lineList.Where(x => x.Item1 == tempNode.Index.ToString()).Select(x => x.Item2);
        //        //tempNode.Children.AddRange(line.Select((x, i) => new { V = x, I = i }).Where(x => x.V == tempNode.Index.ToString()).Select(x => x.I));
        //        tempNode.Children.AddRange(children);
        //        lineList = lineList.Where(x => !children.Contains(x.Item2)).ToList();
        //        if(tempNode.Children.Any())
        //        {
        //            var d = recNode(tempNode, lineList);
        //            if (d + 1 > depth)
        //            {
        //                depth = d + 1;
        //            }
        //        }
        //    }
        //    return depth;
        //}
        //static int recNode (Node node, string[] line)
        //{
        //    var depth = 1;
        //    for (int i = 0; i < line.Length; i++)
        //    {
        //        var s = int.Parse(line[i]);
        //        if (node.Index == s)
        //        {
        //            var tempNode = new Node { Index = i, Value = s };
        //            node.Children.Add(i);
        //            var d = recNode(tempNode,line);
        //            if (d + 1 > depth)
        //            {
        //                depth = d + 1;
        //            }
        //        }
        //    }
        //    return depth;
        //}
        //static int GetDepthTree(Node node)
        //{
        //    var depth = 1;
        //    if(node.Children.Any())
        //    {
        //        foreach (var item in node.Children)
        //        {
        //            var d = GetDepthTree(item);
        //            if (d + 1 > depth)
        //            {
        //                depth = d + 1;
        //            }

        //        }
        //    }
        //    return depth;
        //}
        class Node
        {
            public Node()
            {
                Children = new List<int>();
            }
            public int Value { get; set; }
            public int Index { get; set; }
            public List<int> Children { get; set; }
        }
        #endregion

        #region simulation network packages

        //static void simulationNetworkPackages()
        //{
        //    string[] line = Console.ReadLine().Split(' ');
        //    int bufferSize = int.Parse(line[0]);
        //    int packageCount = int.Parse(line[1]);
        //    List<Tuple<int, int>> packageList = new List<Tuple<int, int>>();
        //    for (int i = 0; i < packageCount; i++)
        //    {
        //        string[] package = Console.ReadLine().Split(' ');
        //        var arrival = int.Parse(package[0]);
        //        var duration = int.Parse(package[1]);
        //        packageList.Add(new Tuple<int, int>(arrival, duration));
        //    }
        //    Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
        //    if (packageList.Any())
        //    {
        //        Console.WriteLine(packageList[0].Item1);
        //        if(packageList[0].Item2 > 0)
        //        {
        //            queue.Enqueue(packageList[0]);
        //        }
        //        for (int i = 1; i < packageList.Count; i++)
        //        {
        //            var previusPackage = packageList[i - 1];
        //            var currentPackage = packageList[i];
        //            if (previusPackage.Item1 == currentPackage.Item1)
        //            {
        //                if(currentPackage.Item2 > 0)
        //                {
        //                    if (bufferSize >= queue.Count)
        //                    {
        //                        Console.WriteLine(currentPackage.Item1);
        //                    }
        //                }
        //                else
        //                {
        //                    Console.WriteLine(-1);
        //                }

        //            }
        //            else
        //            {
        //                Console.WriteLine(currentPackage.Item1);
        //                queue.Dequeue();
        //            }
        //        }
        //    }
        //}
        //static void simulationNetworkPackages()
        //{
        //    string[] line = Console.ReadLine().Split(' ');
        //    int bufferSize = int.Parse(line[0]);
        //    int packageCount = int.Parse(line[1]);
        //    List<Tuple<int, int>> packageList = new List<Tuple<int, int>>();
        //    for (int i = 0; i < packageCount; i++)
        //    {
        //        string[] package = Console.ReadLine().Split(' ');
        //        var arrival = int.Parse(package[0]);
        //        var duration = int.Parse(package[1]);
        //        packageList.Add(new Tuple<int, int>(arrival, duration));
        //    }
        //    Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
        //    var tempDuration = 0;
        //    if (packageList.Any())
        //    {
        //        for (int i = 0; i < packageList.Count; i++)
        //        {
        //            var currentPackage = packageList[i];
        //            if(queue.Count < bufferSize)
        //            {
        //                queue.Enqueue(currentPackage);
        //                if(currentPackage.Item2 > 0)
        //                {
        //                    var firstPackageWithOutDelete = queue.Peek();
        //                    Console.WriteLine(firstPackageWithOutDelete.Item1 + tempDuration);
        //                    tempDuration = firstPackageWithOutDelete.Item2;
        //                }
        //                else
        //                {
        //                    var firstPackageWithDelete = queue.Dequeue();
        //                    Console.WriteLine(firstPackageWithDelete.Item1 + tempDuration);
        //                    tempDuration = 0;
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine(-1);
        //                if(queue.Any())
        //                {
        //                    queue.Dequeue();
        //                }

        //            } 
        //        }
        //    }
        //}
        static void simulationNetworkPackages()
        {
            //string[] line = Console.ReadLine().Split(' ');
            //int bufferSize = int.Parse(line[0]);
            //int packageCount = int.Parse(line[1]);
            //List<Tuple<int, int, int>> packageList = new List<Tuple<int, int, int>>();
            //for (int i = 0; i < packageCount; i++)
            //{
            //    string[] package = Console.ReadLine().Split(' ');
            //    var arrival = int.Parse(package[0]);
            //    var duration = int.Parse(package[1]);
            //    packageList.Add(new Tuple<int, int, int>(arrival, duration, arrival + duration));
            //}
            Queue<int> q = new Queue<int>();


            //max test
            int bufferSize = 100000;
            int packageCount = 100000;
            List<Tuple<int, int, int>> packageList = new List<Tuple<int, int, int>>();
            for (int i = 0; i < packageCount; i++)
            {
                var arrival = 1000;
                var duration = 1000;
                packageList.Add(new Tuple<int, int, int>(arrival, duration, arrival + duration));
            }
            Stopwatch watch = new Stopwatch();
            watch.Start();
            StringBuilder sb = new StringBuilder();
            if (packageList.Any())
            {
                //Console.WriteLine(packageList[0].Item1);
                sb.Append(packageList[0].Item1.ToString() + "\n");
                bool firstIsNull = true;
                List<int> tempBufferList = new List<int>();
                if (packageList[0].Item2 > 0)
                {
                    firstIsNull = false;
                    tempBufferList.Add(packageList[0].Item1 + packageList[0].Item2);
                }
                for (int i = 1; i < packageList.Count; i++)
                {
                    var currentPackage = packageList[i];
                    var previusPackage = packageList[i - 1];
                    //tempBufferList = tempBufferList.Where(x => x > currentPackage.Item1).ToList();
                    //tempBufferList.RemoveAll(x => x <= currentPackage.Item1);
                    if (tempBufferList.Any() && tempBufferList.First() <= currentPackage.Item1)
                    {
                        tempBufferList.RemoveAt(0);
                    }

                    if (previusPackage.Item1 == currentPackage.Item1)
                    {
                        if (firstIsNull && currentPackage.Item2 == 0)
                        {
                            //Console.WriteLine(currentPackage.Item1);
                            sb.Append(currentPackage.Item1.ToString() + "\n");
                        }
                        else
                        {
                            firstIsNull = false;
                            if (tempBufferList.Count < bufferSize)
                            {
                                //Console.WriteLine(previusPackage.Item3);
                                sb.Append(previusPackage.Item3.ToString() + "\n");
                                tempBufferList.Add(previusPackage.Item3 + currentPackage.Item2);
                                packageList[i] = new Tuple<int, int, int>(currentPackage.Item1, currentPackage.Item2, previusPackage.Item3 + currentPackage.Item2);
                            }
                            else
                            {
                                //Console.WriteLine(-1);
                                sb.Append((-1).ToString() + "\n");
                                packageList[i] = new Tuple<int, int, int>(currentPackage.Item1, currentPackage.Item2, previusPackage.Item3);
                            }
                        }
                    }
                    else
                    {
                        firstIsNull = true;
                        if (currentPackage.Item2 > 0)
                        {
                            firstIsNull = false;
                        }
                        if (previusPackage.Item3 > currentPackage.Item1)
                        {
                            if (tempBufferList.Count < bufferSize)
                            {
                                //Console.WriteLine(previusPackage.Item3);
                                sb.Append(previusPackage.Item3.ToString() + "\n");
                                tempBufferList.Add(previusPackage.Item3 + currentPackage.Item2);
                                packageList[i] = new Tuple<int, int, int>(currentPackage.Item1, currentPackage.Item2, previusPackage.Item3 + currentPackage.Item2);
                            }
                            else
                            {
                                //Console.WriteLine(-1);
                                sb.Append((-1).ToString() + "\n");
                                packageList[i] = new Tuple<int, int, int>(currentPackage.Item1, currentPackage.Item2, previusPackage.Item3);
                            }
                        }
                        else
                        {
                            //Console.WriteLine(currentPackage.Item1);
                            sb.Append(currentPackage.Item1.ToString() + "\n");
                            tempBufferList.Add(currentPackage.Item3);
                        }
                    }
                }
            }
            Console.WriteLine(sb);
            Console.WriteLine("--------------------------");
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        //static void simulationNetworkPackages()
        //{
        //    string[] line = Console.ReadLine().Split(' ');
        //    int bufferSize = int.Parse(line[0]);
        //    int packageCount = int.Parse(line[1]);
        //    List<Tuple<int, int>> packageList = new List<Tuple<int, int>>();
        //    for (int i = 0; i < packageCount; i++)
        //    {
        //        string[] package = Console.ReadLine().Split(' ');
        //        var arrival = int.Parse(package[0]);
        //        var duration = int.Parse(package[1]);
        //        packageList.Add(new Tuple<int, int>(arrival, duration));
        //    }
        //    Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
        //    var tempDuration = 0;
        //    if (packageList.Any())
        //    {
        //        for (int i = 0; i < packageList.Count; i++)
        //        {
        //            var currentPackage = packageList[i];
        //            if (queue.Count < bufferSize)
        //            {
        //                queue.Enqueue(currentPackage);
        //                if (currentPackage.Item2 > 0)
        //                {
        //                    var firstPackageWithOutDelete = queue.Peek();
        //                    Console.WriteLine(firstPackageWithOutDelete.Item1 + tempDuration);
        //                    tempDuration = firstPackageWithOutDelete.Item2;
        //                }
        //                else
        //                {
        //                    var firstPackageWithDelete = queue.Dequeue();
        //                    Console.WriteLine(firstPackageWithDelete.Item1 + tempDuration);
        //                    tempDuration = 0;
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine(-1);
        //                if (queue.Any())
        //                {
        //                    queue.Dequeue();
        //                }

        //            }
        //        }
        //    }
        //}
        #endregion

        #region max stack
        static void maxStack()
        {
            Stack<int> stack = new Stack<int>();
            Stack<int> maxStack = new Stack<int>();
            var countQuery = int.Parse(Console.ReadLine());
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < countQuery; i++)
            {
                string[] query = Console.ReadLine().Split(' ');
                var nameQuery = query[0];
                int valueQuery = 0;
                if (query.Length > 1)
                {
                    valueQuery = int.Parse(query[1]);
                }
                switch (nameQuery)
                {
                    case "push":
                        stack.Push(valueQuery);
                        if (maxStack.Any())
                        {
                            var lastMaxValue = maxStack.Peek();
                            if (valueQuery > lastMaxValue)
                            {
                                maxStack.Push(valueQuery);
                            }
                            else
                            {
                                maxStack.Push(lastMaxValue);
                            }
                        }
                        else
                        {
                            maxStack.Push(valueQuery);
                        }
                        break;
                    case "pop":
                        stack.Pop();
                        maxStack.Pop();
                        break;
                    case "max":
                        var maxValue = maxStack.Peek();
                        sb.Append(maxValue.ToString() + "\n");
                        break;
                }
            }
            Console.WriteLine(sb);
        }
        #endregion

        #region Sliding window in array
        //static void SlidingWindow()
        //{
        //    var countArray = int.Parse(Console.ReadLine());
        //    string[] array = Console.ReadLine().Split(' ');
        //    var countSlidingWIndow = int.Parse(Console.ReadLine());
        //    Stack<int> leftStack = new Stack<int>();
        //    Stack<int> rightStack = new Stack<int>();
        //    StringBuilder sb = new StringBuilder();
        //    var currentmax = 0;
        //    if (countArray == array.Length)
        //    {
        //        foreach(var item in array.Reverse())
        //        {
        //            var el = int.Parse(item);
        //            rightStack.Push(el);
        //        }
        //        while(rightStack.Count >= countSlidingWIndow)
        //        {
        //            for (int i = 0; i < countSlidingWIndow; i++)
        //            {
        //                var rightPop = rightStack.Pop();
        //                if (rightPop > currentmax)
        //                {
        //                    currentmax = rightPop;
        //                }
        //                leftStack.Push(rightPop);
        //            }
        //            while (leftStack.Count > 0)
        //            {
        //                var leftPop = leftStack.Pop();
        //                rightStack.Push(leftPop);
        //            }
        //            sb.Append(currentmax.ToString() + " ");
        //            currentmax = 0;
        //            rightStack.Pop();
        //        }
        //        Console.WriteLine(sb);
        //    }
        //}
        static void SlidingWindow()
        {
            var countArray = int.Parse(Console.ReadLine());
            string[] array = Console.ReadLine().Split(' ');
            var countSlidingWIndow = int.Parse(Console.ReadLine());
            Stack<Tuple<int, int>> leftStack = new Stack<Tuple<int, int>>();
            Stack<Tuple<int, int>> rightStack = new Stack<Tuple<int, int>>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                var element = int.Parse(array[i]);
                Tuple<int, int> tuple = new Tuple<int, int>(element, element); ;
                if (leftStack.Any())
                {
                    var peek = leftStack.Peek();
                    if (element < peek.Item2)
                    {
                        tuple = new Tuple<int, int>(element, peek.Item2);
                    }
                }
                leftStack.Push(tuple);
                if ((leftStack.Count + rightStack.Count) == countSlidingWIndow)
                {
                    if (rightStack.Any())
                    {
                        var leftPeek = leftStack.Peek();
                        var rightPeek = rightStack.Peek();
                        sb.Append((leftPeek.Item2 >= rightPeek.Item2 ? leftPeek.Item2 : rightPeek.Item2) + " ");
                        rightStack.Pop();
                    }
                    else
                    {
                        var leftPeek = leftStack.Peek();
                        sb.Append(leftPeek.Item2 + " ");
                        var countLS = leftStack.Count;
                        for (int j = 0; j < countLS; j++)
                        {
                            var pop = leftStack.Pop();
                            pop = new Tuple<int, int>(pop.Item1, pop.Item1);
                            if (rightStack.Any())
                            {
                                var peek = rightStack.Peek();
                                if (pop.Item2 < peek.Item2)
                                {
                                    pop = new Tuple<int, int>(pop.Item1, peek.Item2);
                                }
                            }
                            rightStack.Push(pop);
                        }
                        rightStack.Pop();
                    }
                }
            }
            Console.WriteLine(sb);
        }
        #endregion

        #region Queue wih priority (heap)

        //static void Heap()
        //{
        //    var countArray = int.Parse(Console.ReadLine());
        //    string[] array = Console.ReadLine().Split(' ');
        //    var countSwap = 0;
        //    StringBuilder sb = new StringBuilder();
    
        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        var elementIndex = i;
        //        while (i > 0 && int.Parse(array[elementIndex]) < int.Parse(array[Parent(elementIndex)]))
        //        {
        //            countSwap += 1;
        //            var temp = array[elementIndex];
        //            array[elementIndex] = array[Parent(elementIndex)];
        //            array[Parent(elementIndex)] = temp;
        //            sb.Append(elementIndex.ToString() + " " + Parent(elementIndex).ToString() + "\n");
        //            elementIndex = Parent(elementIndex);
        //        }
        //    }
        //    Console.WriteLine(countSwap);
        //    if(countSwap > 0)
        //    {
        //        Console.WriteLine(sb);
        //    }
        //}
        static void Heap()
        {
            var countArray = int.Parse(Console.ReadLine());
            string[] array = Console.ReadLine().Split(' ');
            var countSwap = 0;
            StringBuilder sb = new StringBuilder();
            Tuple<int, StringBuilder> tuple = new Tuple<int, StringBuilder>(countSwap,sb);

            for (int i = (array.Length / 2) - 1; i >= 0; i--)
            {
                tuple = SiftDown(i, array, tuple);
            }
            Console.WriteLine(tuple.Item1);
            if (tuple.Item1 > 0)
            {
                Console.WriteLine(tuple.Item2);
            }
        }

        static int LeftChild(int i)
        {
            return 2*i + 1;
        }
        static int RightChild(int i)
        {
            return 2 * i + 2;
        }
        static Tuple<int, StringBuilder> SiftDown(int i,string[] array, Tuple<int, StringBuilder> tuple)
        {
            var minIndex = i;
            var left = LeftChild(i);
            if (left < array.Length && int.Parse(array[left]) < int.Parse(array[minIndex]))
            {
                minIndex = left;
            }
            var right = RightChild(i);
            if (right < array.Length && int.Parse(array[right]) < int.Parse(array[minIndex]))
            {
                minIndex = right;
            }
            if (i != minIndex)
            {
                var temp = array[i];
                array[i] = array[minIndex];
                array[minIndex] = temp;
                var countSwap = tuple.Item1 + 1;
                tuple = new Tuple<int, StringBuilder>(countSwap, tuple.Item2.Append(i.ToString() + " " + minIndex.ToString() + "\n"));
                tuple = SiftDown(minIndex,array,tuple);
            }
            return tuple;
        }
        #endregion

        #region Parallel tasks
        static void parallelTasks()
        {
            string[] line = Console.ReadLine().Split(' ');
            var countProcessors = int.Parse(line[0]);
            var countTasks = int.Parse(line[1]);
            string[] timeTasks = Console.ReadLine().Split(' ');
            StringBuilder sb = new StringBuilder();
            //max test
            //var countProcessors = 1;
            //var countTasks = 100000;
            //var timeTasks = Enumerable.Repeat(new Random().Next(1000000,1000000000).ToString(), countTasks).ToArray();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            if (countProcessors >= 1 && countProcessors <= 100000 && countTasks >= 1 && countTasks <= 100000 && countTasks == timeTasks.Length)
            {
                var b = Enumerable.Repeat(0, countProcessors).Select((x, i) => new Tuple<long, long>(i, x)).ToArray();
                for (int i = 0; i < timeTasks.Length; i++)
                {
                    var firstBinaryHeap = b[0];
                    sb.Append(firstBinaryHeap.Item1 + " " + firstBinaryHeap.Item2.ToString() + "\n");
                    var timeTask = timeTasks[i];
                    try
                    {
                        b[0] = checked(new Tuple<long, long>(b[0].Item1, long.Parse(timeTask) + b[0].Item2));
                        if (b[0].Item2 > 0)
                        {
                            SiftDown2(0, b);
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.Read();
                    }
                }
                Console.WriteLine(sb);
                Console.WriteLine("------------------------------");
                Console.WriteLine(watch.ElapsedMilliseconds);
            }
        }
        //static void SiftDown2(int i, Tuple<long, long>[] array)
        //{
        //    var minIndex = i;
        //    var left = LeftChild(i);
        //    if (left < array.Length && array[left].Item2 < array[minIndex].Item2)
        //    {
        //        minIndex = left;
        //    }
        //    var right = RightChild(i);
        //    if (right < array.Length && array[right].Item2 < array[minIndex].Item2)
        //    {
        //        minIndex = right;
        //    }

        //    if (i != minIndex)
        //    {
        //        var temp = array[i];
        //        array[i] = array[minIndex];
        //        array[minIndex] = temp;
        //        SiftDown2(minIndex, array);
        //    }
        //}
        static void SiftDown2(int i, Tuple<long, long>[] array)
        {
            var minIndex = i;
            var l = LeftChild(i);
            var r = RightChild(i);
            if (l < array.Length && r < array.Length && array[l].Item2 == array[i].Item2 && array[r].Item2 == array[i].Item2)
            {
                if(array[l].Item1 < array[i].Item1 || array[r].Item1 < array[i].Item1)
                {
                    minIndex = array[l].Item1 < array[r].Item1 ? l : r;
                }
            }
            else if (l < array.Length && r >= array.Length && array[l].Item2 == array[i].Item2 )
            {
                if(array[l].Item1 < array[i].Item1)
                {
                    minIndex = l;
                }
            }
            else if (l < array.Length && r < array.Length && array[l].Item2 == array[i].Item2 && array[r].Item2 > array[i].Item2)
            {
                if (array[l].Item1 < array[i].Item1)
                {
                    minIndex = l;
                }
            }
            else if (l < array.Length && r < array.Length && array[l].Item2 > array[i].Item2 && array[r].Item2 == array[i].Item2)
            {
                if (array[r].Item1 < array[i].Item1)
                {
                    minIndex = r;
                }
            }
            else if (l < array.Length && r < array.Length && array[l].Item2 < array[i].Item2 && array[r].Item2 < array[i].Item2)
            {
                if(array[l].Item2 < array[r].Item2)
                {
                    minIndex = l;
                }
                else if(array[l].Item2 > array[r].Item2)
                {
                    minIndex = r;
                }
                else
                {
                    minIndex = array[l].Item1 < array[r].Item1 ? l : r;
                }
            }
            else
            {
                if (l < array.Length && array[l].Item2 < array[minIndex].Item2)
                {
                    minIndex = l;
                }
                if (r < array.Length && array[r].Item2 < array[minIndex].Item2)
                {
                    minIndex = r;
                }
            }
            if (i != minIndex)
            {
                var temp = array[i];
                array[i] = array[minIndex];
                array[minIndex] = temp;
                SiftDown2(minIndex, array);
            }
        }
        //static void parallelTasks()
        //{
        //    //string[] line = Console.ReadLine().Split(' ');
        //    //var countProcessors = int.Parse(line[0]);
        //    //var countTasks = int.Parse(line[1]);
        //    //string[] timeTasks = Console.ReadLine().Split(' ');
        //    StringBuilder sb = new StringBuilder();
        //    //max test
        //    var countProcessors = 100000;
        //    var countTasks = 100000;
        //    //string[] timeTasks = Console.ReadLine().Split(' ');
        //    var timeTasks = Enumerable.Repeat("1000000000".ToString(),countProcessors).ToArray();
        //    Stopwatch watch = new Stopwatch();
        //    watch.Start();
        //    if (countProcessors <= 100000 && countTasks <= 100000 && countTasks == timeTasks.Length)
        //    { 
        //        //var binaryHeap = Enumerable.Repeat(0,countProcessors).ToArray();
        //        var b = Enumerable.Repeat(0, countProcessors).Select((x, i) => new Tuple<long, long>(i, x)).ToArray();
        //        for (int i = 0; i < timeTasks.Length; i++)
        //        {
        //            var firstBinaryHeap = b[0];
        //            sb.Append(firstBinaryHeap.Item1 + " " + firstBinaryHeap.Item2.ToString() + "\n");
        //            var timeTask = timeTasks[i];
        //            b[0] = new Tuple<long, long>(b[0].Item1, Convert.ToInt64(timeTask) + b[0].Item2);
        //            SiftDown2(0,b);
        //        }
        //        Console.WriteLine(sb);
        //        Console.WriteLine("------------------------------");
        //        Console.WriteLine(watch.ElapsedMilliseconds);
        //    }
        //}
        //static void SiftDown2(int i, Tuple<long, long>[] array)
        //{
        //    var minIndex = i;
        //    var left = LeftChild(i);
        //    if (left < array.Length && array[left].Item2 < array[minIndex].Item2)
        //    {
        //        minIndex = left;
        //    }
        //    var right = RightChild(i);
        //    if (right < array.Length && array[right].Item2 < array[minIndex].Item2)
        //    {
        //        minIndex = right;
        //    }

        //    if (i != minIndex)
        //    {
        //        var temp = array[i];
        //        array[i] = array[minIndex];
        //        array[minIndex] = temp;
        //        SiftDown2(minIndex, array);
        //    }
        //}

        #endregion

        #region union table
        static void DataBase()
        {
            string[] line = Console.ReadLine().Split(' ');
            var countTable = int.Parse(line[0]);
            var countQuery = int.Parse(line[1]);
            int[] sizeTables = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            Tuple<int,int>[] queries = new Tuple<int, int>[countQuery];
            for (int i = 0; i < queries.Length; i++)
            {
                var strQuery = Console.ReadLine().Split(' ');
                queries[i] = new Tuple<int, int>(int.Parse(strQuery[0]), int.Parse(strQuery[1]));
            }
            StringBuilder sb = new StringBuilder();
            if(countTable == sizeTables.Length)
            {
                var parents = Enumerable.Range(1,countTable).ToArray();
                var rank = Enumerable.Repeat(0, countTable).ToArray();
                var maxSizeTable = sizeTables.Max(x => x);
                for (int i = 0; i < queries.Length; i++)
                {
                    maxSizeTable = UnionTable(queries[i].Item1,queries[i].Item2,  sizeTables,  parents,  rank,maxSizeTable);
                    sb.Append(maxSizeTable.ToString() + "\n");
                }
                Console.WriteLine(sb);
            }
        }
        static int UnionTable(int dest, int source, int[] sizeTables, int[] parents, int[] rank, int maxSizeTable)
        {
            var destId = FindId(dest, parents);
            var sourseId = FindId(source, parents);
            if(destId == sourseId)
            {
                return maxSizeTable;
            }
            if (rank[sourseId - 1] > rank[destId - 1])
            {
                parents[destId - 1] = sourseId;
                sizeTables[sourseId - 1] = sizeTables[sourseId - 1] + sizeTables[destId - 1];
                if (maxSizeTable < sizeTables[sourseId - 1])
                {
                    return sizeTables[sourseId - 1];
                }
            }
            else
            {
                parents[sourseId - 1] = destId;
                if(rank[sourseId - 1] == rank[destId - 1])
                {
                    rank[destId - 1] += 1;
                };
                sizeTables[destId - 1] = sizeTables[sourseId - 1] + sizeTables[destId - 1];
                if(maxSizeTable < sizeTables[destId - 1])
                {
                    return sizeTables[destId - 1];
                }
            }
            return maxSizeTable;
        }
        static int FindId(int i, int[] parents)
        {
            while(i != parents[i - 1])
            {
                i = parents[i - 1];
            }
            return i;
        }
        #endregion

        #region automation analize programs
        static void AutomationAnalizeProgram()
        {
            string[] line = Console.ReadLine().Split(' ');
            var maxIndex = int.Parse(line[0]);
            var countEqualRow = int.Parse(line[1]);
            var countNotEqualRow = int.Parse(line[2]);
            Tuple<int, int>[] equals = new Tuple<int, int>[countEqualRow];
            Tuple<int, int>[] notEquals = new Tuple<int, int>[countNotEqualRow];
            for (int i = 0; i < equals.Length; i++)
            {
                var e = Console.ReadLine().Split(' ');
                equals[i] = new Tuple<int, int>(int.Parse(e[0]), int.Parse(e[1]));
            }
            for (int i = 0; i < notEquals.Length; i++)
            {
                var ne = Console.ReadLine().Split(' ');
                notEquals[i] = new Tuple<int, int>(int.Parse(ne[0]), int.Parse(ne[1]));
            }
            if (countNotEqualRow == 0)
            {
                Console.WriteLine(1);
                return;
            }
            var parents = Enumerable.Range(1, maxIndex).ToArray();
            var rank = Enumerable.Repeat(0, maxIndex).ToArray();
            for (int i = 0; i < equals.Length; i++)
            {
                Union(equals[i].Item1,equals[i].Item2,parents,rank);
            }
            for (int i = 0; i < notEquals.Length; i++)
            {
                var firstId = FindId(notEquals[i].Item1,parents);
                var secondId = FindId(notEquals[i].Item2, parents);
                if (firstId == secondId)
                {
                    Console.WriteLine(0);
                    return;
                }
            }
            Console.WriteLine(1);
        }
        static void Union(int dest, int source, int[] parents, int[] rank)
        {
            var destId = FindId(dest, parents);
            var sourseId = FindId(source, parents);
            if (destId == sourseId)
            {
                return;
            }
            if (rank[sourseId - 1] > rank[destId - 1])
            {
                parents[destId - 1] = sourseId;
            }
            else
            {
                parents[sourseId - 1] = destId;
                if (rank[sourseId - 1] == rank[destId - 1])
                {
                    rank[destId - 1] += 1;
                };
            }
        }
        #endregion

        #region Hash

        static void Hash()
        {
            var countQuery = int.Parse(Console.ReadLine());
            Dictionary<int, string> dict = new Dictionary<int, string>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < countQuery; i++)
            {
                var query = Console.ReadLine().Split(' ');
                var number = int.Parse(query[1]);
                switch (query[0])
                {
                    case "add":

                        if (dict.ContainsKey(number))
                        {
                            dict[number] = query[2];
                        }
                        else
                        {
                            dict.Add(number, query[2]);
                        }
                        break;
                    case "del":
                        dict.Remove(number);
                        break;
                    case "find":
                        if (dict.ContainsKey(number))
                        {
                            sb.Append(dict[number] + "\n");
                        }
                        else
                        {
                            sb.Append("not found" + "\n");
                        }
                        break;
                }
            }
            Console.WriteLine(sb);
        }

        #endregion

        #region Hash by chain
        static void HashByChain()
        {
            try
            {
                var sizeHashTable = int.Parse(Console.ReadLine());
                var countQuery = int.Parse(Console.ReadLine());
                Dictionary<long, List<string>> dict = new Dictionary<long, List<string>>();
                StringBuilder sb = new StringBuilder();
                var x = 263;
                for (int i = 0; i < countQuery; i++)
                {

                    var query = Console.ReadLine().Split(' ');
                    switch (query[0])
                    {
                        case "add":
                            var word = query[1];
                            var hash = word.ToCharArray();
                            long sumHash = 0;

                            for (int j = 0; j < hash.Length; j++)
                            {
                                var pow = CustomPow(x,j, 1000000007);
                                sumHash += (hash[j] * pow) % 1000000007;
                            }
                            sumHash = sumHash % 1000000007;
                            var key = sumHash % sizeHashTable;
                            if (dict.ContainsKey(key))
                            {
                                if (!dict[key].Contains(word))
                                {
                                    dict[key].Insert(0,word);
                                }
                            }
                            else
                            {
                                dict.Add(key, new List<string> { word });
                            }
                            break;
                        case "del":
                            var delWord = query[1];
                            var delHash = delWord.ToCharArray();
                            long sumDelHash = 0;

                            for (int j = 0; j < delHash.Length; j++)
                            {
                                var pow = CustomPow(x, j, 1000000007);
                                sumDelHash += (delHash[j] * pow) % 1000000007;
                            }
                            sumDelHash = sumDelHash % 1000000007;
                            var delKey = sumDelHash % sizeHashTable;
                            if (dict.ContainsKey(delKey))
                            {
                                if (dict[delKey].Contains(delWord))
                                {
                                    dict[delKey].Remove(delWord);
                                    if (!dict[delKey].Any())
                                    {
                                        dict.Remove(delKey);
                                    }
                                }
                            }

                            break;
                        case "find":
                            var findWord = query[1];
                            var findHash = findWord.ToCharArray();
                            long sumFindHash = 0;

                            for (int j = 0; j < findHash.Length; j++)
                            {
                                var pow = CustomPow(x, j, 1000000007);
                                sumFindHash += (findHash[j] * pow) % 1000000007;
                            }
                            sumFindHash = sumFindHash % 1000000007;
                            var findKey = sumFindHash % sizeHashTable;
                            if (dict.ContainsKey(findKey))
                            {
                                if (dict[findKey].Contains(findWord))
                                {
                                    sb.Append("yes" + "\n");
                                }
                                else
                                {
                                    sb.Append("no" + "\n");
                                }
                            }
                            else
                            {
                                sb.Append("no" + "\n");
                            }
                            break;
                        case "check":
                            var checkKey = int.Parse(query[1]);
                            if (dict.ContainsKey(checkKey))
                            {
                                if (dict[checkKey].Any())
                                {
                                    sb.Append(string.Join(" ", dict[checkKey]) + "\n");
                                }
                                else
                                {
                                    sb.Append("\n");
                                }
                            }
                            else
                            {
                                sb.Append("\n");
                            }
                            break;
                    }
                }
                Console.WriteLine(sb);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static long CustomPow(long b, int exp,int mod)
        {
            if(exp == 0)
            {
                return 1;
            }
            long result = b;
            for (int i = 1; i < exp; i++)
            {
                result = result % mod * b % mod;
            }
            return result;
        }
        #endregion

        #region Rabina-Karpa

        static void RabinaKarpa()
        {
            try
            {
                var pattern = Console.ReadLine().ToCharArray();
                var text = Console.ReadLine().ToCharArray();
                var x = 1;
                var mod = 1000000007;
                long hashPattern = 0;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < pattern.Length; i++)
                {
                    var pow = CustomPow(x, i, mod);
                    hashPattern += (pattern[i] * pow) % mod;
                }
                long last = CustomPow(x, pattern.Length - 1, mod);
                long[] arrHashWindow = new long[text.Length - pattern.Length + 1];
                long tempWindowHash = 0;
                var j = 0;
                var k = 0;
                Stack<int> stack = new Stack<int>();
                for (int i = text.Length - 1; i >= 0; i--)
                {
                    if (j < pattern.Length)
                    {
                        stack.Push(text[i]);
                        if (stack.Count == pattern.Length)
                        {
                            for (int g = 0; g < pattern.Length; g++)
                            {
                                var pow = CustomPow(x, g, mod);
                                var pop = stack.Pop();
                                tempWindowHash += (pop * pow) % mod;
                            }
                            if(tempWindowHash < 0)
                            {
                                tempWindowHash = (tempWindowHash + mod) % mod;
                            }
                            arrHashWindow[i] = tempWindowHash;
                        }
                        j++;
                    }
                    else
                    {
                        tempWindowHash = ((tempWindowHash - text[i + pattern.Length]  * last) * x  + text[i]) % mod;
                        if (tempWindowHash < 0)
                        {
                            tempWindowHash = (tempWindowHash + mod) % mod;
                        }
                        arrHashWindow[i] = tempWindowHash;
                        k++;
                    }
                }
                var tempResult = new List<int>();
                for (int i = 0; i < arrHashWindow.Length; i++)
                {
                    if (arrHashWindow[i] == hashPattern)
                    {
                        bool flag = false;
                        var index = i;
                        for (int m = 0; m < pattern.Length; m++)
                        {
                            if (pattern[m] != text[index])
                            {
                                flag = true;
                                break;
                            }
                            index++;
                        }
                        if (!flag)
                        {
                            tempResult.Add(i);
                        }
                    }
                }
                Console.WriteLine(string.Join(" ", tempResult));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region traversal tree
        static void TraversalTree()
        {
            var countQuery = int.Parse(Console.ReadLine());
            Tuple<int, int, int>[] tree = new Tuple<int, int, int>[countQuery];
            for (int i = 0; i < countQuery; i++)
            {
                var line = Console.ReadLine().Split(' ');
                var key = int.Parse(line[0]);
                var left = int.Parse(line[1]);
                var right = int.Parse(line[2]);
                tree[i] = new Tuple<int, int, int>(key,left,right);
            }
            List<int> inList = new List<int>();
            inList = InOrder(inList, tree[0],tree);
            Console.WriteLine(string.Join(" ", inList));
            List<int> preList = new List<int>();
            preList = PreOrder(preList, tree[0], tree);
            Console.WriteLine(string.Join(" ", preList));
            List<int> postList = new List<int>();
            postList = PostOrder(postList, tree[0], tree);
            Console.WriteLine(string.Join(" ", postList));
        }
        static List<int> InOrder(List<int> list, Tuple<int, int, int> v, Tuple<int, int, int>[] tree)
        {
            if (v.Item2 >= 0)
            {
                list = InOrder(list, tree[v.Item2], tree);
            }
            list.Add(v.Item1);
            if (v.Item3 >= 0)
            {
                list = InOrder(list, tree[v.Item3], tree);
            }
            return list;
        }
        static List<int> PreOrder(List<int> list, Tuple<int, int, int> v, Tuple<int, int, int>[] tree)
        {
            list.Add(v.Item1);
            if (v.Item2 >= 0)
            {
                list = PreOrder(list, tree[v.Item2], tree);
            }
            if (v.Item3 >= 0)
            {
                list = PreOrder(list, tree[v.Item3], tree);
            }
            return list;
        }
        static List<int> PostOrder(List<int> list, Tuple<int, int, int> v, Tuple<int, int, int>[] tree)
        {
            if (v.Item2 >= 0)
            {
                list = PostOrder(list, tree[v.Item2], tree);
            }
            if (v.Item3 >= 0)
            {
                list = PostOrder(list, tree[v.Item3], tree);
            }
            list.Add(v.Item1);
            return list;
        }
        #endregion

        #region correct tree

        static void CorrectTree2()
        {
            try
            {
                var countQuery = int.Parse(Console.ReadLine());
                NodeTree[] tree = new NodeTree[countQuery];
                for (int i = 0; i < countQuery; i++)
                {
                    var line = Console.ReadLine().Split(' ');
                    var key = int.Parse(line[0]);
                    var left = int.Parse(line[1]);
                    var right = int.Parse(line[2]);
                    if (left > 0)
                    {
                        tree[left] = new NodeTree { ParentIndex = i };
                    }
                    if (right > 0)
                    {
                        tree[right] = new NodeTree { ParentIndex = i };
                    }
                    if (tree[i] == null)
                    {
                        tree[i] = new NodeTree { Key = key, LeftIndex = left, RightIndex = right, CurrentIndex = i };
                    }
                    else
                    {
                        tree[i].Key = key;
                        tree[i].LeftIndex = left;
                        tree[i].RightIndex = right;
                        tree[i].CurrentIndex = i;
                    }
                }

                bool isCorrectTree = true;

                if (countQuery > 0)
                {
                    var node = MinNodeTree(tree[0],tree);
                    var nextNodeTree = GetNextNodeTree(node, tree);
                    var ind = 1;
                    while (node.Key < nextNodeTree.Key)
                    {
                        node = nextNodeTree;
                        nextNodeTree = GetNextNodeTree(node, tree);
                        ind++;  
                    }
                    if (ind != tree.Length)
                    {
                        isCorrectTree = false;
                    }
                }

                if (isCorrectTree)
                {
                    Console.WriteLine("CORRECT");
                }
                else
                {
                    Console.WriteLine("INCORRECT");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }
        }
        static NodeTree MinNodeTree(NodeTree node , NodeTree[] tree)
        {
            if (node.LeftIndex > 0)
            {
                node = MinNodeTree(tree[node.LeftIndex],tree);
            }
            return node;
        }
        static NodeTree GetNextNodeTree(NodeTree node, NodeTree[] tree)
        {
            if (node.RightIndex > 0)
            {
                node = MinNodeTree(tree[node.RightIndex],tree);
            }
            else
            {
                if (node.ParentIndex.HasValue)
                {
                    var parent = tree[node.ParentIndex.Value];
                    while (parent.RightIndex == node.CurrentIndex)
                    {
                        node = parent;
                        if (node.ParentIndex.HasValue)
                        {
                            parent = tree[node.ParentIndex.Value];
                        }
                    }
                    return parent;
                }
            }
            return node;
        }
        public class NodeTree
        {
            public int? ParentIndex { get; set; }
            public int CurrentIndex { get; set; }
            public int LeftIndex { get; set; }
            public int RightIndex { get; set; }
            public int Key { get; set; }
        }
        #endregion

        #region more general property tree
        static void GeneralPropertyTree()
        {
            var countQuery = int.Parse(Console.ReadLine());
            NodeTree2[] tree = new NodeTree2[countQuery];
            for (int i = 0; i < countQuery; i++)
            {
                var line = Console.ReadLine().Split(' ');
                var key = long.Parse(line[0]);
                var left = int.Parse(line[1]);
                var right = int.Parse(line[2]);
                if (left > 0)
                {
                    tree[left] = new NodeTree2 { ParentIndex = i };
                }
                if (right > 0)
                {
                    tree[right] = new NodeTree2 { ParentIndex = i };
                }
                if (tree[i] == null)
                {
                    tree[i] = new NodeTree2 { Key = key, LeftIndex = left, RightIndex = right, CurrentIndex = i };
                }
                else
                {
                    tree[i].Key = key;
                    tree[i].LeftIndex = left;
                    tree[i].RightIndex = right;
                    tree[i].CurrentIndex = i;
                }
            }

            bool isCorrectTree = true;

            if (countQuery > 0)
            {
                var node = MinNodeTree2(tree[0], tree);
                var nextNodeTree = GetNextNodeTree2(node, tree);
                var ind = 1;
                while (nextNodeTree != null && node.Key <= nextNodeTree.Key)
                {
                    node = nextNodeTree;
                    nextNodeTree = GetNextNodeTree2(node, tree);
                    ind++;
                    if (ind == tree.Length)
                    {
                        break;
                    }
                }
                if (ind != tree.Length)
                {
                    isCorrectTree = false;
                }
            }

            if (isCorrectTree)
            {
                Console.WriteLine("CORRECT");
            }
            else
            {
                Console.WriteLine("INCORRECT");
            }
        }
        static NodeTree2 MinNodeTree2(NodeTree2 node, NodeTree2[] tree)
        {
            if (node.LeftIndex > 0)
            {
                node = MinNodeTree2(tree[node.LeftIndex], tree);
            }
            return node;
        }
        static NodeTree2 GetNextNodeTree2(NodeTree2 node, NodeTree2[] tree)
        {
            if (node.RightIndex > 0)
            {
                node = MinNodeTree2(tree[node.RightIndex], tree);
            }
            else
            {
                if (node.ParentIndex.HasValue)
                {
                    var parent = tree[node.ParentIndex.Value];
                    while (parent.RightIndex == node.CurrentIndex)
                    {
                        node = parent;
                        if (node.ParentIndex.HasValue)
                        {
                            parent = tree[node.ParentIndex.Value];
                        }
                    }
                    if (parent.Key > node.Key)
                    {
                        return parent;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            return node;
        }
        public class NodeTree2
        {
            public int? ParentIndex { get; set; }
            public int CurrentIndex { get; set; }
            public int LeftIndex { get; set; }
            public int RightIndex { get; set; }
            public long Key { get; set; }
        }
        #endregion

        #region set with sum
        static void SetWithSum2()
        {
            var countQuery = int.Parse(Console.ReadLine());
            StringBuilder sb = new StringBuilder();
            AVLTree avlTree = new AVLTree();
            SplayTree splayTree = new SplayTree();
            long sum = 0;
            if (countQuery >=1 && countQuery <= 100000)
            {
                for (int i = 0; i < countQuery; i++)
                {
                    var line = Console.ReadLine().Split(' ');
                    var queryCase = line[0];
                    switch (queryCase)
                    {
                        case "+":
                            if (long.Parse(line[1]) >= 0)
                            {
                                var addNumber = Foo(long.Parse(line[1]), sum);
                                //avlTree.Add(addNumber);
                                splayTree.Add(addNumber);
                            }
                            break;
                        case "-":
                            if (long.Parse(line[1]) >= 0)
                            {
                                var deleteNumber = Foo(long.Parse(line[1]), sum);
                                //avlTree.Delete(deleteNumber);
                                splayTree.Delete(deleteNumber);
                            }
                            break;
                        case "?":
                            var searchNumber = Foo(long.Parse(line[1]), sum);
                            if (long.Parse(line[1]) >= 0)
                            {
                                //var search = avlTree.Search(searchNumber);
                                var search = splayTree.Search(searchNumber);
                                if (search)
                                {
                                    sb.Append("Found" + "\n");
                                }
                                else
                                {
                                    sb.Append("Not found" + "\n");
                                }
                            }
                            else
                            {
                                sb.Append("Not found" + "\n");
                            }
                            break;
                        case "s":
                            var leftNumber = Foo(long.Parse(line[1]), sum);
                            var rightNumber = Foo(long.Parse(line[2]), sum);
                            //sum = avlTree.Sum(leftNumber, rightNumber);
                            sum = splayTree.Sum(leftNumber, rightNumber);
                            sb.Append(sum + "\n");
                            break;
                    }
                }
                Console.WriteLine(sb);
            }
        }

        public class AVLTree
        {
            class AVLNode
            {
                public AVLNode Left { get; set; }
                public AVLNode Right { get; set; }
                public long Value { get; set; }
                //public long Sum { get; set; }
                public int Height { get; set; }
            }

            AVLNode RootNode { get; set; }

            #region add node
            public void Add(long value)
            {
                RootNode = CreateNode(RootNode, value);
            }

            AVLNode CreateNode(AVLNode node, long value)
            {
                if (node == null)
                {
                    node = new AVLNode
                    {
                        Left = null,
                        Right = null,
                        Value = value,
                        Height = 1,
                        //Sum = value
                    };
                    return node;
                }
                if (value < node.Value)
                {
                    node.Left = CreateNode(node.Left, value);
                }
                else if (value > node.Value)
                {
                    node.Right = CreateNode(node.Right, value);
                }
                else
                {
                    return node;
                }

                node.Height = maxInt(GetHeight(node.Left), GetHeight(node.Right)) + 1;
                int balance = GetBalance(node);

                if (balance > 1 && value < node.Left.Value)
                {
                    node = RightRotation(node);
                }
                if (balance < -1 && value > node.Right.Value)
                {
                    node = LeftRotation(node);
                }
                if (balance > 1 && value > node.Left.Value)
                {
                    node = LeftRightRotation(node);
                }
                if (balance < -1 && value < node.Right.Value)
                {
                    node = RightLeftRotation(node);
                }
                //node.Sum = node.Value + GetSum(node.Left) + GetSum(node.Right);
                return node;
            }
            #endregion

            #region delete node
            public void Delete(long value)
            {
                RootNode = RemoveNode(RootNode, value);
            }
            AVLNode RemoveNode(AVLNode node, long value)
            {
                if (node == null) return node;

                if (value < node.Value)
                {
                    node.Left = RemoveNode(node.Left, value);
                }
                else if (value > node.Value)
                {
                    node.Right = RemoveNode(node.Right, value);
                }
                else
                {
                    if (node.Left == null || node.Right == null)
                    {
                        var tempNode = node.Left != null ? node.Left : node.Right;
                        if (tempNode == null)
                        {
                            tempNode = node;
                            node = null;
                        }
                        else
                        {
                            node = tempNode;
                        }
                    }
                    else
                    {
                        var tempNode = MinNode(node.Right);
                        node.Value = tempNode.Value;
                        //node.Sum = tempNode.Sum;
                        node.Right = RemoveNode(node.Right, tempNode.Value);
                    }
                }

                if (node == null) return node;

                node.Height = maxInt(GetHeight(node.Left), GetHeight(node.Right)) + 1;
                int balance = GetBalance(node);

                if (balance > 1 && GetBalance(node.Left) >= 0)
                {
                    node = RightRotation(node);
                }
                if (balance < -1 && GetBalance(node.Right) <= 0)
                {
                    node = LeftRotation(node);
                }
                if (balance > 1 && GetBalance(node.Left) < 0)
                {
                    node = LeftRightRotation(node);
                }
                if (balance < -1 && GetBalance(node.Left) > 0)
                {
                    node = RightLeftRotation(node);
                }
                //node.Sum = node.Value + GetSum(node.Left) + GetSum(node.Right);
                return node;
            }
            #endregion

            #region search node
            public bool Search(long value)
            {
                return SeacrhNode(RootNode,value);
            }
            bool SeacrhNode(AVLNode node, long value)
            {
                while (node != null)
                {
                    if (value == node.Value)
                    {
                        return true;
                    }
                    else if (value < node.Value)
                    {
                        node = node.Left;
                    }
                    else if (value > node.Value)
                    {
                        node = node.Right;
                    }
                }
                return false;
            }
            #endregion

            #region sum of set
            public long Sum(long leftValue, long rightValue)
            {
                return SumOfSet2(RootNode,leftValue,rightValue);
            }
            long SumOfSet(AVLNode node, long leftValue, long rightValue)
            {
                AVLNode LeftMinusOne = null;
                AVLNode tempNode = node;
                AVLNode temp = node;
                AVLNode result = node;
                while (node != null)
                {
                    if (leftValue == node.Value)
                    {
                        if(node.Left != null)
                        {
                            LeftMinusOne = node.Left;
                        }
                        else if (node.Right != null)
                        {
                            LeftMinusOne = node.Right;
                        }
                        break;
                    }
                    else if (leftValue < node.Value)
                    {
                        node = node.Left;
                    }
                    else if (leftValue > node.Value)
                    {
                        node = node.Right;
                    }
                }

                if (LeftMinusOne != null)
                {
                    var tupleOne = Split(node, LeftMinusOne.Value);
                    tempNode = tupleOne.Item2;
                    temp = tupleOne.Item1;
                }
                var tupleTwo = Split(tempNode, rightValue);
                result = tupleTwo.Item1;

                var t = Merge(tupleTwo.Item1,tupleTwo.Item2);
                if(t != null)
                {
                    node = Merge(temp, t);
                }
                
                if (result != null)
                {
                    //return result.Sum;
                    return 0;
                }
                else
                {
                    return 0;
                }
            }

            long SumOfSet2(AVLNode root, long low, long high)
            {
                if (root == null) return 0;
                if (root.Value == high && root.Value == low)
                    return root.Value;

                if (root.Value <= high && root.Value >= low)
                    return root.Value + SumOfSet2(root.Left, low, high) +
                               SumOfSet2(root.Right, low, high);

                else if (root.Value < low)
                    return SumOfSet2(root.Right, low, high);

                else return SumOfSet2(root.Left, low, high);
            }
            #endregion

            #region count nodes
            public int Count(long low, long high)
            {
                return CountNodes(RootNode,low,high);
            }
            int CountNodes(AVLNode root, long low, long high)
            {
                if (root == null) return 0;
                if (root.Value == high && root.Value == low)
                    return 1;

                if (root.Value <= high && root.Value >= low)
                    return 1 + CountNodes(root.Left, low, high) +
                               CountNodes(root.Right, low, high);

                else if (root.Value < low)
                    return CountNodes(root.Right, low, high);

                else return CountNodes(root.Left, low, high);
            }
            #endregion

            #region utils

            Tuple<AVLNode,AVLNode> Split(AVLNode node, long value)
            {
                if (node == null)
                {
                    return new Tuple<AVLNode, AVLNode>(null,null);
                }
                if(value < node.Value)
                {
                    var tempTuple = Split(node.Left, value);
                    if (tempTuple.Item2 != null )
                    {
                        var tempNode = AVLMergeWithRoot(tempTuple.Item2, node.Right, node);
                        return new Tuple<AVLNode, AVLNode>(tempTuple.Item1, tempNode);
                    }
                    else
                    {
                        return new Tuple<AVLNode, AVLNode>(node, null);
                    }
                }
                else
                {
                    var tempTuple = Split(node.Right, value);
                    if (tempTuple.Item1 != null)
                    {
                        var tempNode = AVLMergeWithRoot(tempTuple.Item1, node.Left, node);
                        return new Tuple<AVLNode, AVLNode>(tempTuple.Item2, tempNode);
                    }
                    else
                    {
                        return new Tuple<AVLNode, AVLNode>(node, null);
                    }
                }
            }
            AVLNode MergeWithRoot(AVLNode v1, AVLNode v2, AVLNode t)
            {
                t.Left = v1;
                t.Right = v2;
                return t;
            }
            AVLNode AVLMergeWithRoot(AVLNode v1, AVLNode v2, AVLNode t)
            {
                var tempHeight = (v1.Height - v2.Height) < 0 ? (v1.Height - v2.Height) * -1 : (v1.Height - v2.Height);
                if (tempHeight <= 1)
                {
                    t = MergeWithRoot(v1,v2,t);
                    t.Height = maxInt(GetHeight(t.Left), GetHeight(t.Right)) + 1;
                    //t.Sum = t.Value + GetSum(t.Left) + GetSum(t.Right);
                    return t;
                }
                else if (v1.Height > v2.Height)
                {
                    var tempNode = AVLMergeWithRoot(v1.Right,v2,t);
                    v1.Right = tempNode;

                    v1.Height = maxInt(GetHeight(v1.Left), GetHeight(v1.Right)) + 1;
                    int balance = GetBalance(v1);
                    if (balance < -1 && GetBalance(v1.Right) <= 0)
                    {
                        v1 = LeftRotation(v1);
                    }
                    if (balance < -1 && GetBalance(v1.Left) > 0)
                    {
                        v1 = RightLeftRotation(v1);
                    }
                    //v1.Sum = v1.Value + GetSum(v1.Left) + GetSum(v1.Right);

                    return v1;
                }
                else 
                {
                    var tempNode = AVLMergeWithRoot(v1.Left, v1, t);
                    v2.Left = tempNode;

                    v2.Height = maxInt(GetHeight(v2.Left), GetHeight(v2.Right)) + 1;
                    int balance = GetBalance(v2);

                    if (balance > 1 && GetBalance(v2.Left) >= 0)
                    {
                        v2 = RightRotation(v2);
                    }
                    if (balance > 1 && GetBalance(v2.Left) < 0)
                    {
                        v2 = LeftRightRotation(v2);
                    }
                    //v2.Sum = v2.Value + GetSum(v2.Left) + GetSum(v2.Right);

                    return v2;
                }
            }
            AVLNode Merge(AVLNode v1, AVLNode v2)
            {
                if (v1 != null)
                {
                    var t = MaxNode(v1);
                    Delete(t.Value);
                    t = MergeWithRoot(v1, v2, t);
                    return t;
                }
                else
                {
                    return v2;
                }
                
            }
            int GetHeight(AVLNode node)
            {
                return node != null ? node.Height : 0;
            }
            int GetBalance(AVLNode node)
            {
                if (node == null) return 0;
                return GetHeight(node.Left) - GetHeight(node.Right);
            }
            //long GetSum(AVLNode node)
            //{
            //    return node != null ? node.Sum : 0;
            //}
            AVLNode RightRotation(AVLNode node)
            {
                //AVLNode temp = node.Left;
                //node.Left = temp.Right;
                //temp.Right = node;

                //node.Height = maxInt(GetHeight(node.Left), GetHeight(node.Right)) + 1;
                //temp.Height = maxInt(GetHeight(temp.Left), node.Height) + 1;
                //node.Sum = node.Value + GetSum(node.Left) + GetSum(node.Right);
                //return temp;
                AVLNode temp = node.Left;
                AVLNode temp1 = temp.Right;

                temp.Right = node;
                node.Left = temp1;

                node.Height = maxInt(GetHeight(node.Left), GetHeight(node.Right)) + 1;
                temp.Height = maxInt(GetHeight(temp.Left), GetHeight(temp.Right)) + 1;

                return temp;
            }
            AVLNode LeftRotation(AVLNode node)
            {
                //AVLNode temp = node.Right;
                //node.Right = temp.Left;
                //temp.Left = node;

                //node.Height = maxInt(GetHeight(node.Left), GetHeight(node.Right)) + 1;
                //temp.Height = maxInt(GetHeight(temp.Right), node.Height) + 1;
                //node.Sum = node.Value + GetSum(node.Left) + GetSum(node.Right);
                //return temp;

                AVLNode temp = node.Right;
                AVLNode temp1 = temp.Left;

                temp.Left = node;
                node.Right = temp1;

                node.Height = maxInt(GetHeight(node.Left), GetHeight(node.Right)) + 1;
                temp.Height = maxInt(GetHeight(temp.Left), GetHeight(temp.Right)) + 1;

                return temp;
            }
            AVLNode LeftRightRotation(AVLNode node)
            {
                node.Left = LeftRotation(node.Left);
                return RightRotation(node);
            }
            AVLNode RightLeftRotation(AVLNode node)
            {
                node.Right = RightRotation(node.Right);
                return LeftRotation(node);
            }
            AVLNode MaxNode(AVLNode node)
            {
                if (node.Right != null)
                {
                    node = MaxNode(node.Right);
                }
                return node;
            }
            AVLNode MinNode(AVLNode node)
            {
                //if (node.Left != null)
                //{
                //    node = MinNode(node.Left);
                //}
                //return node;
                AVLNode current = node;
 
                /* loop down to find the leftmost leaf */
                while (current.Left != null)
                    current = current.Left;
 
                return current;
            }
            int maxInt(int a, int b)
            {
                return a > b ? a : b;
            }
            #endregion
        }

        public class SplayTree
        {
            class SplayNode
            {
                public SplayNode Left { get; set; }
                public SplayNode Right { get; set; }
                public long Value { get; set; }
                public long Sum { get; set; }
            }

            SplayNode RootNode { get; set; }

            #region add node
            public void Add(long value)
            {
                RootNode = CreateNode(RootNode, value);
            }

            SplayNode CreateNode(SplayNode root, long value)
            {
                // Simple Case: If tree is empty
                if (root == null) return newNode(value);

                // Bring the closest leaf node to root
                root = splay(root, value);

                // If key is already present, then return
                if (root.Value == value) return root;

                // Otherwise allocate memory for new node
                SplayNode newnode  = newNode(value);
 
                // If root's key is greater, make root as right child
                // of newnode and copy the left child of root to newnode
                if (root.Value > value)
                {
                    newnode.Right = root;
                    newnode.Left = root.Left;
                    root.Left = null;
                }
 
                // If root's key is smaller, make root as left child
                // of newnode and copy the right child of root to newnode
                else
                {
                    newnode.Left = root;
                    newnode.Right = root.Right;
                    root.Right = null;
                }
 
                return newnode; // newnode becomes new root
            }
            #endregion

            #region delete node
            public void Delete(long value)
            {
                RootNode = RemoveNode(RootNode, value);
            }
            SplayNode RemoveNode(SplayNode root, long value)
            {
                root = splay(root, value);
                return merge(root.Left,root.Right);
            }
            #endregion

            #region search node
            public bool Search(long value)
            {
                var node = search(RootNode, value);
                
                return node.Value == value ? true : false;
            }
            SplayNode search(SplayNode root, long value)
            {
                return splay(root, value);
            }
            #endregion

            #region sum
            public long Sum(long leftValue, long rightValue)
            {
                return SumOfSet(RootNode,leftValue,rightValue);
            }
            long SumOfSet(SplayNode root, long leftValue, long rightValue)
            {
                SplayNode left = new SplayNode();
                SplayNode middle = new SplayNode();
                SplayNode right = new SplayNode();
                long result = 0;
                var splitLeft = split(root, leftValue);
                left = splitLeft.Item1;
                middle = splitLeft.Item2;

                var splitRight = split(middle,rightValue + 1);
                middle = splitRight.Item1;
                right = splitRight.Item2;

                if(middle != null)
                {
                    result += middle.Sum;
                }

                SplayNode newMiddle = merge(left, middle);
                RootNode = merge(newMiddle,right);

                return result;
            }
            #endregion

            #region utils
            SplayNode newNode(long value)
                {
                    SplayNode node = new SplayNode();
                    node.Value = value;
                    node.Left = null;
                    node.Right = null;
                    node.Sum = node.Value;
                    return (node);
                }
            SplayNode rightRotate(SplayNode node)
            {
                SplayNode temp = node.Left;
                node.Left = temp.Right;
                temp.Right = node;

                node.Sum = node.Sum + GetSum(node.Left) + GetSum(node.Right);

                return temp;
            }
            SplayNode leftRotate(SplayNode node)
            {
                SplayNode temp = node.Right;
                node.Right = temp.Left;
                temp.Left = node;

                node.Sum = node.Sum + GetSum(node.Left) + GetSum(node.Right);

                return temp;
            }
            SplayNode splay(SplayNode root, long value)
                {
                    // Base cases: root is NULL or key is present at root
                    if (root == null || root.Value == value)
                        return root;
 
                    // Key lies in left subtree
                    if (root.Value > value)
                    {
                        // Key is not in tree, we are done
                        if (root.Left == null) return root;
 
                        // Zig-Zig (Left Left)
                        if (root.Left.Value > value)
                        {
                            // First recursively bring the key as root of left-left
                            root.Left.Left = splay(root.Left.Left, value);

                            // Do first rotation for root, second rotation is done after else
                            root = rightRotate(root);
                        }
                        else if (root.Left.Value < value) // Zig-Zag (Left Right)
                        {
                            // First recursively bring the key as root of left-right
                            root.Left.Right = splay(root.Left.Right, value);
 
                            // Do first rotation for root->left
                            if (root.Left.Right != null)
                                root.Left = leftRotate(root.Left);
                        }
 
                        // Do second rotation for root
                        return (root.Left == null) ? root : rightRotate(root);
                    }
                    else // Key lies in right subtree
                    {
                        // Key is not in tree, we are done
                        if (root.Right == null) return root;
 
                        // Zag-Zig (Right Left)
                        if (root.Right.Value > value)
                        {
                            // Bring the key as root of right-left
                            root.Right.Left = splay(root.Right.Left, value);
 
                            // Do first rotation for root->right
                            if (root.Right.Left != null)
                                root.Right = rightRotate(root.Right);
                        }
                        else if (root.Right.Value < value)// Zag-Zag (Right Right)
                        {
                            // Bring the key as root of right-right and do first rotation
                            root.Right.Right = splay(root.Right.Right, value);
                            root = leftRotate(root);
                        }
 
                        // Do second rotation for root
                        return (root.Right == null) ? root : leftRotate(root);
                    }
                }
            Tuple<SplayNode,SplayNode> split(SplayNode root, long value)
            {
                if (root == null)
                    return new Tuple<SplayNode, SplayNode>(null,null);

                root = splay(root, value);

                if (root.Value == value)
                {
                    root.Sum = root.Sum + GetSum(root.Left) + GetSum(root.Right);
                    return new Tuple<SplayNode, SplayNode>(root.Left, root.Right);
                }
                else if (root.Value < value)
                {
                    var tempRight = root.Right;
                    root.Right = null;

                    root.Sum = root.Sum + GetSum(root.Left) + GetSum(root.Right);
                    if (tempRight != null)
                    {
                        tempRight.Sum = tempRight.Sum + GetSum(tempRight.Left) + GetSum(tempRight.Right);
                    }
                   
                    return new Tuple<SplayNode, SplayNode>(root, tempRight);
                }
                else
                {
                    var tempLeft = root.Left;
                    root.Left = null;

                    if (tempLeft != null)
                    {
                        tempLeft.Sum = tempLeft.Sum + GetSum(tempLeft.Left) + GetSum(tempLeft.Right);
                    }
                    root.Sum = root.Sum + GetSum(root.Left) + GetSum(root.Right);

                    return new Tuple<SplayNode, SplayNode>(tempLeft, root);
                }
            }
            SplayNode merge(SplayNode left, SplayNode right)
            {
                if (right == null) return left;
                if (left == null) return right;

                right = splay(right, left.Value);
                right.Left = left;

                right.Sum = right.Sum + GetSum(right.Left) + GetSum(right.Right);

                return right;
            }
            long GetSum(SplayNode node)
            {
                return node != null ? node.Sum : 0;
            }

            #endregion

        }

        //static void SetWithSum()
        //{
        //    var countQuery = int.Parse(Console.ReadLine());
        //    StringBuilder sb = new StringBuilder();
        //    List<long> tree = new List<long>();
        //    long sum = 0;
        //    for (int i = 0; i < countQuery; i++)
        //    {
        //        var line = Console.ReadLine().Split(' ');
        //        var queryCase = line[0];
        //        switch (queryCase)
        //        {
        //            case "+" :
        //                var addNumber = Foo(long.Parse(line[1]),sum);
        //                if (!tree.Contains(addNumber))
        //                {
        //                    var firstMore = tree.Select((x,j) => new { x, j}).FirstOrDefault(x => x.x > addNumber);
        //                    if(firstMore != null)
        //                    {
        //                        tree.Insert(firstMore.j, addNumber);
        //                    }
        //                    else
        //                    {
        //                        tree.Add(addNumber);
        //                    }
        //                    var ind = tree.BinarySearch(addNumber);
        //                    if (ind >= 0)
        //                    {
        //                        tree.Insert(ind, addNumber);
        //                    }
        //                    else
        //                    {
        //                        tree.Add(addNumber);
        //                        tree = tree.OrderBy(x => x).ToList();
        //                    }
        //                }
        //                break;
        //            case "-":
        //                var deleteNumber = Foo(long.Parse(line[1]), sum);
        //                if (tree.Contains(deleteNumber))
        //                {
        //                    tree.Remove(deleteNumber);
        //                }
        //                break;
        //            case "?":
        //                var searchNumber = Foo(long.Parse(line[1]), sum);
        //                if (tree.Contains(searchNumber))
        //                {
        //                    sb.Append("Found" + "\n");
        //                }
        //                else
        //                {
        //                    sb.Append("Not found" + "\n");
        //                }
        //                break;
        //            case "s":
        //                var leftNumber = Foo(long.Parse(line[1]), sum);
        //                var rightNumber = Foo(long.Parse(line[2]), sum);
        //                if (tree.Count > 0)
        //                {
        //                    if (leftNumber < tree[0])
        //                    {
        //                        if (rightNumber == tree[0])
        //                        {
        //                            sum = tree[0];
        //                        }
        //                        else if (rightNumber > tree[0])
        //                        {
        //                            sum = tree.Where(x => x <= rightNumber).Sum();
        //                        }
        //                        else if (rightNumber < tree[0])
        //                        {
        //                            sum = 0;
        //                        }
        //                    }
        //                    else if (rightNumber > tree[tree.Count - 1])
        //                    {
        //                        if (leftNumber == tree[tree.Count - 1])
        //                        {
        //                            sum = tree[tree.Count - 1];
        //                        }
        //                        else if (leftNumber < tree[tree.Count - 1])
        //                        {
        //                            sum = tree.Where(x => x >= leftNumber).Sum();
        //                        }
        //                        else if (leftNumber > tree[tree.Count - 1])
        //                        {
        //                            sum = 0;
        //                        }
        //                    }
        //                    else if (leftNumber >= tree[0] && rightNumber <= tree[tree.Count - 1])
        //                    {
        //                        sum = tree.Where(x => x >= leftNumber && x <= rightNumber).Sum();
        //                    }
        //                }
        //                else
        //                {
        //                    sum = 0;
        //                }
        //                sb.Append(sum + "\n");
        //                break;
        //        }
        //    }
        //    Console.WriteLine(sb);
        //}

        static long Foo(long x, long s)
        {
            long result = (x + s) % 1000000001;
            return result;
        }
        #endregion
    }
}