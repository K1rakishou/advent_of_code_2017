using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace andventOfCode {
    public class DayFourteen : Puzzle {
        public void run() {
            Console.WriteLine("Day Fourteen");
            PuzzleOne();
            PuzzleTwo();
        }

        public void PuzzleOne() {
            var input = "ljoxqyyw";
            var hashList = new List<List<char>>();
            var usedSquares = 0;

            for (var i = 0; i < 128; ++i) {
                hashList.Add(knotHash(input + "-" + i));
            }

            foreach (var hash in hashList) {
                foreach (var character in hash) {
                    for (var i = 0; i < 8; ++i) {
                        if ((character & (1 << i)) != 0) {
                            ++usedSquares;
                        }
                    }
                }
            }

            Console.WriteLine(String.Format("Puzzle One Answer: {0}", usedSquares));
        }

        public void PuzzleTwo() {
            var input = "ljoxqyyw";
            var listOfBinaryStrings = new List<StringBuilder>();

            for (var i = 0; i < 128; ++i) {
                listOfBinaryStrings.Add(convertToBinarySB(knotHash(input + "-" + i)));
            }

            var regionId = 1;
            var regionsCount = 0;
            
            for (var x = 0; x < 128; ++x) {
                for (var y = 0; y < 128; ++y) {
                    if (listOfBinaryStrings[x][y] != '1') {
                        continue;
                    }

                    markRegions(listOfBinaryStrings, x, y, regionId);
                    ++regionsCount;
                }
            }
            
            Console.WriteLine(String.Format("Puzzle Two Answer: {0}", regionsCount));
        }

        public void markRegions(List<StringBuilder> strings, int x, int y, int regionId) {
            if (x < 0 || x > 127 || y < 0 || y > 127) {
                return;
            }

            if (strings[x][y] == (char) regionId) {
                return;
            }
            
            if (strings[x][y] == '0') {
                return;
            }

            strings[x][y] = (char) regionId;
             
            markRegions(strings, x - 1, y, regionId);
            markRegions(strings, x, y + 1, regionId);
            markRegions(strings, x + 1, y, regionId);
            markRegions(strings, x, y - 1, regionId);
        }

        public StringBuilder convertToBinarySB(List<char> hash) {
            var sb = new StringBuilder();

            foreach (var character in hash) {
                for (var i = 7; i >= 0; i--) {
                    sb.Append((character & (1 << i)) == 0 ? '0' : '1');
                }
            }

            return sb;
        }

        public List<char> knotHash(string str) {
            var input = str.ToList();
            var offset = 0;
            var skip = 0;
            var list = new List<int>();

            input.Add('\x11');
            input.Add('\x1F');
            input.Add('\x49');
            input.Add('\x2f');
            input.Add('\x17');

            for (var i = 0; i < 256; ++i) {
                list.Add(i);
            }

            var listLen = list.Count;

            for (var round = 0; round < 64; ++round) {
                foreach (var len in input) {
                    var sublist = new List<int>();

                    for (var i = offset; i < len + offset; ++i) {
                        sublist.Add(list[i % listLen]);
                    }

                    if (sublist.Count <= 1) {
                        offset += len + skip;
                        ++skip;
                        continue;
                    }

                    sublist.Reverse();

                    var j = 0;
                    for (var i = offset; i < len + offset; ++i) {
                        var index = i % listLen;
                        list.RemoveAt(index);
                        list.Insert(index, sublist[j]);
                        ++j;
                    }

                    offset += len + skip;
                    ++skip;
                }
            }

            var denseHash = new List<char>();

            for (var i = 0; i < 16; ++i) {
                var xored = '\x0';

                for (var j = 0; j < 16; ++j) {
                    xored ^= (char) list[i * 16 + j];
                }

                denseHash.Add(xored);
            }

            return denseHash;
        }
    }
}