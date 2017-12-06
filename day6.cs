using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace andventOfCode {
    public class DaySix : Puzzle {
        public void run() {
            Console.WriteLine("Day Six");
            PuzzleOne();
            PuzzleTwo();
        }

        public void PuzzleOne() {
            var input = "14\t0\t15\t12\t11\t11\t3\t5\t1\t6\t8\t4\t9\t1\t8\t4";
            var banks = input.Split('\t').Select(num => Convert.ToInt32(num)).ToList();
            var len = banks.Count;
            var steps = 0;
            var loopDetection = new HashSet<List<int>>(new LoopDetectionComparer());

            while (true) {
                var bank = int.MinValue;
                var index = 0;
                
                for (var i = 0; i < len; ++i) {
                    if (banks[i] > bank) {
                        bank = banks[i];
                        index = i;
                    }
                }

                banks[index] = 0;

                for (var i = index + 1; i < bank + (index + 1); ++i) {
                    ++banks[i % len];
                }

                ++steps;
                
                if (!loopDetection.Add(new List<int>(banks))) {
                    break;
                }
            }
            
            Console.WriteLine(String.Format("Puzzle One Answer: {0}", steps));
        }

        public void PuzzleTwo() {
            var input = "14\t0\t15\t12\t11\t11\t3\t5\t1\t6\t8\t4\t9\t1\t8\t4";
            var banks = input.Split('\t').Select(num => Convert.ToInt32(num)).ToList();
            var len = banks.Count;
            var steps = 0;
            var loopDetection = new HashSet<List<int>>(new LoopDetectionComparer());
            var previousStates = new List<List<int>>();
            var cycles = 0;

            while (true) {
                var bank = int.MinValue;
                var index = 0;
                
                for (var i = 0; i < len; ++i) {
                    if (banks[i] > bank) {
                        bank = banks[i];
                        index = i;
                    }
                }

                banks[index] = 0;

                for (var i = index + 1; i < bank + (index + 1); ++i) {
                    ++banks[i % len];
                }

                ++steps;
                
                previousStates.Add(new List<int>(banks));
                if (!loopDetection.Add(new List<int>(banks))) {
                    cycles = GetCyclesCountToPrevState(previousStates, banks);
                    break;
                }
            }
            
            Console.WriteLine(String.Format("Puzzle Two Answer: {0}", cycles));
        }

        private static int GetCyclesCountToPrevState(List<List<int>> previousStates, List<int> banks) {
            int cycles = 0;
            for (var i = 0; i < previousStates.Count; ++i) {
                if (compareLists(banks, previousStates[i])) {
                    cycles = previousStates.Count - i - 1;
                    break;
                }
            }

            return cycles;
        }

        class LoopDetectionComparer : IEqualityComparer<List<int>> {
            public bool Equals(List<int> list1, List<int> list2) {
                return compareLists(list1, list2);
            }

            public int GetHashCode(List<int> list) {
                var hashcode = 0;

                foreach (var elem in list) {
                    hashcode += elem.GetHashCode();
                }

                return hashcode;
            }
        }

        public static bool compareLists(List<int> list1, List<int> list2) {
            if (list1.Count != list2.Count) {
                return false;
            }
                
            for (var index = 0; index < list1.Count; index++) {
                if (list1[index] != list2[index]) {
                    return false;
                }
            }

            return true;
        }
    }
}